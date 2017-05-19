using HospitaPaymentService.Wzszhjk.DAL;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Web;

namespace HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy
{
    public class QueryInfoDal : BaseDB
    {
        /// <summary>
        /// 根据病人id判断病人是否有资格使用云医院
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryIsHasYunHospital(string brid, out string msg)
        {
            string querySql = string.Format("select fuzhenbz from  zfb_menzhenklb where patientid='{0}'", brid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);

            int sqlResult = 0, result = 0;
            try
            {
                object obj = DbHelperOra.GetSingle(querySql, connection);
                msg = "";
                if (obj != null)
                {

                    sqlResult = Convert.ToInt32(obj);
                    if (sqlResult == 1)
                    {
                        msg = "是";
                        result = 1;
                    }
                    if (sqlResult == 2)
                    {
                        msg = "否";
                        result = 2;
                    }
                }

            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                return 1;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }


        /// <summary>
        /// 获取缴费或退费联系人列表 （FY030101）
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns>1=获取成功、其他代表失败</returns>
        public int QueryConnectPerson(string openid, out ArrayList values, out  string msg)
        {
            int ret = -1;
            values = new ArrayList();
            msg = "";
            string sqlQuery = string.Format("select linkmanid, label, name from ZFB_TIANJIACYLXR " +
                 "where openid = '{0}'", openid);

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    ConnectPerson entity = new ConnectPerson();
                    entity.brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    entity.label = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    entity.name = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    values.Add(entity);
                }
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
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPatientInfo(string id, string type, out ArrayList values, out  string msg)
        {
            values = new ArrayList();
            msg = "";
            int ret = -1;
            string sqlQuery = string.Format(" select * from FY030102 where id={0} and type={1} ", id, type);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    ConnectPerson entity = new ConnectPerson();
                    entity.brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    entity.label = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    entity.name = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    values.Add(entity);
                }
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
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int GetChuFangList(string sfid, out ArrayList values, out string msg)
        {
            int result = -1;
            msg = "";
            string sqlQuery = string.Format("select cflsh,cfje from yyy_chufang_tf where sfid='{0}'", sfid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        ChuFangInfo entity = new ChuFangInfo();
                        entity.CFLSH = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.CFJE = !dr.IsDBNull(1) ? Convert.ToString(dr.GetDouble(1)) : " ";
                        values.Add(entity);
                    }
                    result = 1;
                }
                else
                {
                    msg = "没有查找到记录";
                    result = 0;
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
            return result;
        }
    }
}