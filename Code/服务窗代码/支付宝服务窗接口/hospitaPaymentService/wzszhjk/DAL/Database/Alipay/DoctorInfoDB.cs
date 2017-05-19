using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;


namespace HospitaPaymentService.Wzszhjk.DAL.Database.Alipay
{
    public class DoctorInforDB : BaseDB
    {
        private BuilderSql _builder;

        public DoctorInforDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 医生信息列表_按姓名查
        /// </summary>
        /// <param name="name">医生姓名</param>
        /// <param name="pageindex">访问页码</param>
        /// <param name="pagesize">每页行数</param>
        /// <param name="values">返回订单信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DctorInfoXingming(string name, int pageindex, int pagesize,
            out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                long maxrow;
                long minrow;
                General.CalculatePage(pageindex, pagesize, out maxrow, out minrow);

                bool flag = false;

                string exeSql = _builder.DctorInfoXingmingSql(name, maxrow, minrow, out  flag, out  msg);
                if (!flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exeSql);

                dr = DbHelperOra.ExecuteReader(exeSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        AlipayDoctorInfo pd = new AlipayDoctorInfo();

                        pd.code = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.name = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.sex = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.pictureurl = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.level = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.recommend = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.adept = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.departcode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.departname = !dr.IsDBNull(8) ? dr.GetString(8) : "";

                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 00;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 16;
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                values = null;
                return -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }

        /// <summary>
        /// 医生信息列表_按姓名拼音首字母查
        /// </summary>
        /// <param name="namepy">医生姓名拼音</param>
        /// <param name="pageindex">访问页码</param>
        /// <param name="pagesize">每页行数</param>
        /// <param name="values">返回订单信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DoctorInfoPinYin(string namepy, int pageindex, int pagesize,
            out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                long maxrow;
                long minrow;
                General.CalculatePage(pageindex, pagesize, out maxrow, out minrow);

                bool flag = false;

                string exeSql = _builder.DctorInfoPinYinSql(namepy, maxrow, minrow, out flag, out msg);
                if (!flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exeSql);

                dr = DbHelperOra.ExecuteReader(exeSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        AlipayDoctorInfo pd = new AlipayDoctorInfo();

                        pd.code = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.name = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.sex = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.pictureurl = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.level = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.recommend = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.adept = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.departcode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.departname = !dr.IsDBNull(8) ? dr.GetString(8) : "";

                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 00;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 16;
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                values = null;
                return -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }

        /// <summary>
        /// 医生信息列表_按医生代码查
        /// </summary>
        /// <param name="code">医生代码</param>
        /// <param name="values">医生信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DoctorInfoDaiMa(string code, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;
                int ret = -99;

                string findId1 = _builder.DoctorInfoByCodeSql(code, out flag, out msg);

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        AlipayDoctorInfo pd = new AlipayDoctorInfo();

                        pd.code = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.name = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.sex = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.pictureurl = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.level = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.recommend = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.adept = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.departcode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.departname = !dr.IsDBNull(8) ? dr.GetString(8) : "";

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 16;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                values = null;
                return -99;
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
        /// 医生停诊信息列表
        /// </summary>
        /// <param name="pageindex">所在页数</param>
        /// <param name="pagesize">每页显示条数</param>
        /// <param name="values">医生信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DoctorInfoTingZhen(int pageindex, int pagesize, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {
                bool flag = false;
                long maxrow;
                long minrow;
                General.CalculatePage(pageindex, pagesize, out maxrow, out minrow);

                string findId1 = _builder.DoctorInfoTingZhenSql(maxrow, minrow, out flag, out msg);

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        AlipayDoctorInfoTingZhen pd = new AlipayDoctorInfoTingZhen();

                        pd.code = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.name = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.pictureurl = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.departcode = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.departname = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.stopdate = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.stopshift = !dr.IsDBNull(6) ? dr.GetString(6) : "";

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 16;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                values = null;
                return -99;
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

    }
}