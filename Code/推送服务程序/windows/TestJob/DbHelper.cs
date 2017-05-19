using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

public class DbHelper
{
    //private static DbConnection conn;
    //public static DbConnection Conn
    //{
    //    get
    //    {
    //        if (conn == null)
    //        {
    //            string connStr = ConnectionString;
    //            conn = new SqlConnection(connStr);
    //        }

    //        return conn;
    //    }        
    //}

    private enum DbConnectionOwnership
    {
        /// <summary>由BaseDbHelper提供连接</summary>
        Internal,
        /// <summary>由调用者提供连接</summary>
        External
    }

    #region ExecuteReader

    public static DbDataReader ExecuteReader(string commandText, string conn)
    {
        return ExecuteReader(CommandType.Text, commandText, conn, (DbParameter[])null);
    }

    public static DbDataReader ExecuteReader(CommandType commandType, string commandText, string conn)
    {
        return ExecuteReader(commandType, commandText, conn, (DbParameter[])null);
    }

    public static DbDataReader ExecuteReader(CommandType commandType, string commandText, string conn, params DbParameter[] commandParameters)
    {
        DbConnection connection = null;
        try
        {
            connection = new SqlConnection(conn);
            connection.Open();

            return ExecuteReader(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
        }
        catch
        {
            // If we fail to return the SqlDatReader, we need to close the connection ourselves
            if (connection != null) connection.Close();
            throw;
        }

    }

    private static DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, DbConnectionOwnership connectionOwnership)
    {
        if (connection == null) throw new ArgumentNullException("connection");

        bool mustCloseConnection = false;
        // 创建命令
        DbCommand cmd = SqlClientFactory.Instance.CreateCommand();
        try
        {
            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            // 创建数据阅读器
            DbDataReader dataReader;

            if (connectionOwnership == DbConnectionOwnership.External)
            {
                dataReader = cmd.ExecuteReader();
            }
            else
            {
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            // 清除参数,以便再次使用..
            // HACK: There is a problem here, the output parameter values are fletched 
            // when the reader is closed, so if the parameters are detached from the command
            // then the SqlReader can磘 set its values. 
            // When this happen, the parameters can磘 be used again in other command.
            bool canClear = true;
            foreach (DbParameter commandParameter in cmd.Parameters)
            {
                if (commandParameter.Direction != ParameterDirection.Input)
                    canClear = false;
            }

            if (canClear)
            {
                //cmd.Dispose();
                cmd.Parameters.Clear();
            }


            return dataReader;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    #endregion

    #region ExecuteNonQuery

    public static int ExecuteNonQuery(string commandText, string conn)
    {
        return ExecuteNonQuery(CommandType.Text, commandText, conn, null);
    }

    public static int ExecuteNonQuery(CommandType commandType, string commandText, string conn)
    {
        return ExecuteNonQuery(commandType, commandText, conn, null);
    }

    public static int ExecuteNonQuery(CommandType commandType, string commandText, string conn, params DbParameter[] commandParameters)
    {
        DbConnection connection = new SqlConnection(conn);

        if (connection == null) throw new ArgumentNullException("connection");

        // 创建DbCommand命令,并进行预处理
        DbCommand cmd = SqlClientFactory.Instance.CreateCommand();
        bool mustCloseConnection = false;
        PrepareCommand(cmd, connection, (DbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

        // Finally, execute the command
        int retval = cmd.ExecuteNonQuery();

        // 清除参数,以便再次使用.
        cmd.Parameters.Clear();
        if (mustCloseConnection)
            connection.Close();
        return retval;
    }

    #endregion

    #region ExecuteScalar

    public static object ExecuteScalar(string commandText, string conn)
    {
        return ExecuteScalar(CommandType.Text, commandText, conn, null);
    }

    public static object ExecuteScalar(CommandType commandType, string commandText, string conn)
    {
        return ExecuteScalar(commandType, commandText,conn, null);
    }

    public static object ExecuteScalar(CommandType commandType, string commandText, string conn, params DbParameter[] commandParameters)
    {
        DbConnection connection = new SqlConnection(conn);

        if (connection == null) throw new ArgumentNullException("connection");

        // 创建DbCommand命令,并进行预处理
        DbCommand cmd = SqlClientFactory.Instance.CreateCommand();

        bool mustCloseConnection = false;
        PrepareCommand(cmd, connection, (DbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

        // 执行DbCommand命令,并返回结果.
        object retval = cmd.ExecuteScalar();

        // 清除参数,以便再次使用.
        cmd.Parameters.Clear();

        if (mustCloseConnection)
            connection.Close();

        return retval;
    }

    #endregion

    private static void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, out bool mustCloseConnection)
    {
        if (command == null) throw new ArgumentNullException("command");
        if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
        {
            mustCloseConnection = false;
        }

        // 给命令分配一个数据库连接.
        command.Connection = connection;

        // 设置命令文本(存储过程名或SQL语句)
        command.CommandText = commandText;

        // 分配事务
        if (transaction != null)
        {
            if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            command.Transaction = transaction;
        }

        // 设置命令类型.
        command.CommandType = commandType;

        // 分配命令参数
        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }
        return;
    }

    private static void AttachParameters(DbCommand command, DbParameter[] commandParameters)
    {
        if (command == null) throw new ArgumentNullException("command");
        if (commandParameters != null)
        {
            foreach (DbParameter p in commandParameters)
            {
                if (p != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                        (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    command.Parameters.Add(p);
                }
            }
        }
    }
}