using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Common.Webservice;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.String;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;


namespace HospitaPaymentService.Wzszhjk.DAL.Database.Alipay
{
    public class NotificationInformationDB : BaseDB
    {
        private BuilderSql _builder;

        public NotificationInformationDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 通知病人就诊信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DB_InformPatient(out ArrayList values, out string msg)
        {
            int ret = -1;
            //1代表以预约
            int Preengagestate = 1;
            values = null;
            msg = "";
            values = new ArrayList();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;

                string interText = _builder.GetInformPatientSql(Preengagestate, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(interText, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        while (dr.Read())
                        {
                            InformPatientInfo ri = new InformPatientInfo();
                            ri.userid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                            ri.brxm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                            ri.ysxm = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            ri.jzdz = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                            ri.jzsj = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                            ri.jzrq = !dr.IsDBNull(5) ? dr.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : " ";
                            values.Add(ri);
                        }
                        msg = "获取成功";
                    }
                    ret = 0;
                    dr.Close();
                    return ret;
                }
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
        /// 核对未到账的充值缴费信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DB_CheckInformation(out ArrayList values, out string msg)
        {
            int ret = -1;
            //0代表以创建的订单
            int ddzt = 0;
            values = null;
            msg = "";
            values = new ArrayList();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;

                string interText = _builder.GetCheckInformationSql(ddzt, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }
                long yylsh = 0;
                dr = DbHelperOra.ExecuteReader(interText, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CheckInformation ri = new CheckInformation();
                        yylsh = !dr.IsDBNull(0) ? dr.GetInt64(0) : 0;
                        ri.out_trade_no = StringHelper.YylshHasPrefix(yylsh);
                        ri.brlx = !dr.IsDBNull(1) ?  Convert.ToString(dr.GetInt32(1)) : "";
                        if (ri.brlx == "1")
                        {
                            ri.patientid = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt64(2)) : "";
                        }
                        else if (ri.brlx == "2")
                        {
                            ri.inpatientno = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt64(2)) : "";
                        }
                        ri.patientname = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        ri.money = !dr.IsDBNull(4) ? Convert.ToString(dr.GetDouble(4)) : " ";
                        ri.openid = !dr.IsDBNull(5) ? dr.GetString(5) : " ";                  
                        values.Add(ri);
                    }
                    msg = "获取成功";
                }
                ret = 0;
                dr.Close();
                return ret;
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
        }
    }
}