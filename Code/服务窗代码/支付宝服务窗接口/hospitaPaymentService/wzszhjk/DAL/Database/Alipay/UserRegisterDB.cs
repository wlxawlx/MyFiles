using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database.Alipay
{
    public class UserRegisterDB : BaseDB
    {
        private BuilderSql _builder;
        public UserRegisterDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="info">用户信息</param>
        public int DB_UserRegister(UserInfo info, out string msg)
        {
            msg = "";
            int ret = -1;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                bool isRegister = IsUserRegister(info.openid, out msg);
                if (isRegister)
                {
                    msg = "亲，您已注册！";
                    return 06;
                }

                bool _flag = false;

                string insertText = _builder.GetUserRegisterSql(info.name, info.phone, info.idcardno, info.address,
                    info.openid, info.headurl, info.usertype, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, insertText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                int row = DbHelperOra.ExecuteSql(insertText, connection);
                //row:影响的记录数
                if (row <= 0)
                {
                    ret = 07;
                    msg = "注册失败！";
                }
                else
                {
                    ret = 0;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != connection && ConnectionState.Open == connection.State)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 用户注册_带绑卡
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_UserRegisterBindCard(UserInfo info, out string msg)
        {
            msg = "";
            int ret = -1;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                bool isRegister = IsUserRegister(info.openid, out msg);
                if (isRegister)
                {
                    msg = "亲，您已注册！";
                    return 99;
                }

                bool _flag = false;

                string insertText = _builder.GetUserRegisterBindCardSql(info.name, info.phone, info.idcardno, info.address, info.openid, info.headurl, info.cardno, info.patientid, info.usertype, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, insertText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                int row = DbHelperOra.ExecuteSql(insertText, connection);
                //row:影响的记录数
                if (row <= 0)
                {
                    ret = 99;
                    msg = "注册失败！";
                }
                else
                {
                    ret = 0;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != connection && ConnectionState.Open == connection.State)
                {
                    connection.Close();
                }
            }
        }

        private bool IsUserRegister(string openid, out string msg)
        {
            msg = "";
            bool isRegister = false;
            string name = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                string result = "";
                string isUserRegisterSql = "select openid, name, idcardno from zfb_yonghuzc_bk where openid = '{0}'";

                string[] _paramters = new string[1];
                _paramters[0] = openid;

                result = string.Format(isUserRegisterSql, _paramters);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, result);

                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        name = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    }
                    isRegister = true;
                }

                return isRegister;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                return isRegister;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="usertype">用户类型 1-支付宝用户 2-微信用户</param>
        /// <param name="info">用户信息</param>
        /// <param name="msg">成功、错误或异常信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_UserInfo(string openid, string usertype, out UserInfo info, out string msg)
        {
            msg = "";
            info = new UserInfo();
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {

                bool _flag = false;

                string findText = _builder.GetLandSql(openid, usertype, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        info.openid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        info.name = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        info.phone = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        info.idcardno = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        info.address = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        info.headurl = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        info.cardno = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        info.patientid = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                    }
                    //如果已绑卡判断绑卡是否失效
                    if (!string.IsNullOrEmpty(info.cardno) && info.cardno != " ")
                    {
                        if (!IsCardnoValid(info.patientid, info.cardno, out msg))
                        {
                            info.cardno = "";
                            info.patientid = "";
                        }
                    }
                    if (IsZYBrxxValid(info.idcardno, info.name, out msg))
                    {
                        info.inpatentflag = "1";
                    }
                    else
                    {
                        info.inpatentflag = "0";
                    }
                    ret = 0;
                }
                else
                {
                    msg = "该登录信息未注册！";
                    ret = 1;
                }
                if (null != dr)
                {
                    dr.Close();
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="msg">成功、错误或异常信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_ModifyInfo(UserInfo info, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {
                bool _flag = false;

                string updateText = _builder.GetModifyLandSql(info.openid, info.name, info.phone, info.idcardno,
                    info.address, info.headurl, info.usertype, out _flag, out msg);
                ArrayList listSql = new ArrayList();
                listSql.Add(updateText);

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, updateText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }


                DbHelperOra.ExecuteSqlTran(listSql, connection);
                //ret:影响的记录数
                msg = "修改用户信息成功";
                ret = 0;

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// 添加常用联系人
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <param name="linkInfo">常用联系人信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_AddContacts(UserInfo info, out UserInfo linkInfo, out string msg)
        {
            msg = "";
            linkInfo = new UserInfo();
            int ret = -1;
            string linkmanid = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                bool _flag = false;
                ret = DB_GetLinkmanid(info.openid, out linkmanid, out msg);
                if (ret != 0)
                {
                    ret = 03;
                    msg = "获取联系人ID失败！";
                    return ret;
                }

                string insertText = _builder.GetAddContactsSql(info.openid, info.label, info.name, info.phone,
                    info.idcardno, info.address, linkmanid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, insertText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }
                ArrayList listSql = new ArrayList();
                listSql.Add(insertText);

                DbHelperOra.ExecuteSqlTran(listSql, connection);

                linkInfo.label = info.label;
                linkInfo.name = info.name;
                linkInfo.linkmanid = linkmanid;

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != connection && ConnectionState.Open == connection.State)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 查看常用联系人ID
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="name">姓名</param>
        /// <param name="idcardno">身份证号</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        private int DB_GetLinkmanid(string openid, out string linkmanid, out string msg)
        {
            msg = "";
            linkmanid = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {

                string result = "";
                string getLinkmanidSql = "Select fun_get_linkmanid('{0}') From dual";

                string[] _paramters = new string[1];
                _paramters[0] = openid;

                result = string.Format(getLinkmanidSql, _paramters);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, result);

                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.Read())
                {
                    linkmanid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    ret = 0;
                }

                if (null != dr)
                {
                    dr.Close();
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }
        /// <summary>
        /// 获得常用联系人信息列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="linkmanid">联系人ID</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_FavoriteContactsListStr(string openid, string linkmanid, out ArrayList list, out string msg)
        {
            msg = "";
            list = new ArrayList();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {

                bool _flag = false;

                string findText = _builder.GetFavoriteContactsListSql(openid, linkmanid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                while (dr.Read())
                {
                    UserInfo info = new UserInfo();
                    info.linkmanid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    info.label = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    info.name = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    info.phone = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    info.idcardno = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    info.address = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    int bindcardfalag = dr.GetInt32(6);
                    if (bindcardfalag == 1)
                    {
                        info.bindcardfalg = "已绑卡";
                    }
                    else
                    {
                        info.bindcardfalg = "未绑卡";
                    }
                    info.patientid = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                    info.cardno = !dr.IsDBNull(8) ? dr.GetString(8) : "";

                    if (new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().HasPatientInfo(info.patientid, info.cardno) == 0)
                    {
                        info.patientid = "";
                    }


                    //如果已绑卡判断绑卡是否失效
                    if (bindcardfalag == 1)
                    {
                        if (!IsCardnoValid(info.patientid, info.cardno, out msg))
                        {
                            info.bindcardfalg = "未绑卡";
                        }
                    }

                    if (IsZYBrxxValid(info.idcardno, info.name, out msg))
                    {
                        info.inpatentflag = "1";
                    }
                    else
                    {
                        info.inpatentflag = "0";
                    }
                    list.Add(info);
                }

                if (list.Count > 0)
                {
                    msg = "找到联系人信息";
                    ret = 00;
                }
                else
                {
                    msg = "没有联系人信息，请添加联系人！";
                    list = null;
                    ret = 03;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        ///  删除常用联系人
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="linkmanid">联系人ID</param>
        /// <param name="info">用户信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_DeleteFavoriteContactsStrr(string openid, string linkmanid, out UserInfo info, out string msg)
        {
            msg = "";
            info = new UserInfo();
            int ret = -1;

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            OracleDataReader dr = null;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            OracleTransaction tx = connection.BeginTransaction();
            cmd.Transaction = tx;

            try
            {
                bool _flag = false;

                string querySql = _builder.GetLinkManNameSql(openid, linkmanid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, querySql);
                cmd.CommandText = querySql;
                dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    info.linkmanid = !dr.IsDBNull(0) ? dr.GetString(0).ToUpper() : "";
                    info.name = !dr.IsDBNull(1) ? dr.GetString(1).ToUpper() : "";
                }
                else
                {
                    msg = "无该联系人信息";
                    ret = 03;
                    dr.Close();
                    tx.Commit();
                    return ret;
                }

                string deletText = _builder.GetDeleteFavoriteContactsSql(openid, linkmanid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, deletText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                cmd.CommandText = deletText;
                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    ret = 0;
                }
                else
                {
                    ret = 09;
                    msg = "删除常用联系人异常！";
                    tx.Rollback();
                }

                dr.Close();
                tx.Commit();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                tx.Rollback();
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != connection && ConnectionState.Open == connection.State)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 查看常用联系人ID
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="name">姓名</param>
        /// <param name="idcardno">身份证号</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        private int DB_FindLinkmanid(string openid, string linkmanid, out string[] paramters, out string msg)
        {
            msg = "";
            paramters = new string[2];

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {

                string result = "";
                string findLinkmanidSql = "select linkmanid, name from ui_table where openid = '{0}' and linkmanid = '{1}'";

                string[] _paramters = new string[2];
                _paramters[0] = openid;
                _paramters[1] = linkmanid;

                result = string.Format(findLinkmanidSql, _paramters);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, result);

                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.Read())
                {
                    paramters[0] = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    paramters[1] = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                }

                if (null != dr)
                {
                    dr.Close();
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// 绑卡号码是否有效
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="cardno">绑卡号码</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        private bool IsCardnoValid(string brid, string cardno, out string msg)
        {
            bool result = false;
            msg = "";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            string getCardno = "";
            try
            {
                bool _flag = false;

                string findId1 = _builder.GetSqlValidCardno(brid, cardno, out _flag, out msg);
                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        getCardno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        result = true;
                    }
                }
                else
                {
                    msg = "该病人卡号已失效！";
                    result = false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return result;
        }


        private bool IsZYBrxxValid(string idcardno, string name, out string msg)
        {
            bool result = false;
            msg = "";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            string inpatientno = "";
            try
            {
                bool _flag = false;

                string findId1 = _builder.GetIsZYBrxxValid(idcardno, name, out _flag, out msg);
                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        inpatientno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        result = true;
                    }
                }
                else
                {
                    msg = "未找到住院号码！";
                    result = false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return result;
        }

    }
}