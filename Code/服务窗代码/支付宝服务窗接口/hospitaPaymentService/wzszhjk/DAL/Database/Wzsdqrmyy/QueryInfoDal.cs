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
using System.Data;
using HospitaPaymentService.Wzszhjk.Common;
using System.Data.SqlClient;
using HospitaPaymentService.Wzszhjk.Utils;
using System.Text;
using log4net;

namespace HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy
{
    public class QueryInfoDal : BaseDB
    {
        /// <summary>
        /// 获取缴费或退费联系人列表 （FY030101）
        /// </summary>
        /// <param name="openid">用户标识(支付宝USERID或微信OPENID)</param>
        /// <param name="usertype">用户类型1：支付宝用户  2：微信用户</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns>1=获取成功、其他代表失败</returns>
        public int QueryConnectPerson(string openid, string usertype, out ArrayList values, out  string msg)
        {
            int ret = -1;
            values = new ArrayList();
            msg = "";
            string sqlQuery = string.Format("select brid,label,name,jzkh from  yyy_lianxiren where openid = '{0}' and usertype='{1}'", openid, usertype);

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ConnectPerson entity = new ConnectPerson();
                        entity.brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        entity.label = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        entity.name = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        entity.jzkh = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        values.Add(entity);
                    }
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
        /// 获取病人基本信息（FY030102）
        /// </summary>
        /// <param name="id">缴费：jzid,退费：orderno</param>
        /// <param name="type">缴费:jf，退费:tf</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPatientInfo(string id, string type, out ArrayList values, out  string msg)
        {
            values = new ArrayList();
            msg = "";
            int ret = -1;
            string sqlQuery = string.Format(" select brid,label,name,jzkh,sfz from yyy_shoufeiryxx where id='{0}' and type='{1}' and rownum=1 ", id, type);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ConnectPerson entity = new ConnectPerson();
                        entity.brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        entity.label = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        entity.name = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        entity.jzkh = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        entity.sfz = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        values.Add(entity);
                    }
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
        /// 可退费清单FY030312
        /// </summary>
        /// <param name="orderno">订单id</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int GetChargeList(string orderno, out ArrayList values, out string msg)
        {
            msg = "";
            string sqlQuery = string.Format("select fpxmid,fpxmmc,fpxmje from yyy_tuifei_qd  where orderno='{0}'", orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        ChargeEntity entity = new ChargeEntity();
                        entity.fpxmid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.fpxmmc = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.fpxmje = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
                else
                {
                    msg = "没有查找到记录";
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
            return 1;
        }

        /// <summary>
        /// 缴费时获取处方单信息（FY030103）yyy_keshouflb_fpxmid
        /// </summary>
        /// <param name="jzid"></param>
        /// <returns></returns>
        public int QueryDetailChuFangInfoByJzId(string jzid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select fpxmid,fpxmmc,fpxmje from yyy_keshouflb_fpxmid where jzid='{0}'", jzid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        ChargeEntity entity = new ChargeEntity();
                        entity.fpxmid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.fpxmmc = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.fpxmje = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }
        /// <summary>
        /// 缴费时获取处方单药品详情信息（FY030201）yyy_keshouflb_sfxmmx
        /// </summary>
        /// <param name="fpxmid"></param>
        /// <param name="jzid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryChuFangYaoPinInfo(string fpxmid, string jzid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select sfxmmc,sfxmsl,sfxmje from yyy_keshouflb_sfxmmx where fpxmid='{0}' and jzid='{1}' ", fpxmid, jzid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        YaoPinInfo entity = new YaoPinInfo();
                        entity.sfxmmc = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.sfxmsl = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sfxmje = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 可退费明细（FY030313）yyy_tuifei_mx
        /// </summary>
        /// <param name="fpxmid">发票项目id</param>
        /// <param name="orderno">支付宝医院订单id</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryChargeListByOrderno(string fpxmid, string orderno, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select sfxmmc,sfxmsl,sfxmje from yyy_tuifei_mx where fpxmid='{0}' and orderno='{1}' ", fpxmid, orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HuaYanInfo entity = new HuaYanInfo();
                        entity.sfxmmc = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.sfxmsl = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sfxmje = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }



        /// <summary>
        /// 获取默认收件人信息（FY030301）yyy_shouhuodzxx
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReceiverInfo(string openid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select sjrxm,lxdh,sjdz,shdzid from yyy_shouhuodzxx where openid='{0}' ", openid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReceiverInfo entity = new ReceiverInfo();
                        entity.sjrxm = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.lxdh = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sjdz = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.shdzid = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 获取收件人信息（FY030302）yyy_shouhuodzxx
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="shdzid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryByReceiverInfo(string openid, string shdzid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select sjrxm,lxdh,sjdz,shdzid,mrbz from yyy_shouhuodzxx where openid='{0}' and shdzid='{1}' ", openid, shdzid);
            if (shdzid == "none")
            {
                sqlQuery = string.Format(" select sjrxm,lxdh,sjdz,shdzid,mrbz from yyy_shouhuodzxx where openid='{0}' ", openid);
            }
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReceiverInfo entity = new ReceiverInfo();
                        entity.sjrxm = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.lxdh = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sjdz = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.shdzid = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.mrbz = !dr.IsDBNull(4) ? dr.GetDecimal(4).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }





        /// <summary>
        /// 预提交缴费信息
        /// </summary>
        /// <param name="jzid"></param>
        /// <param name="qjfs">1：快递；2：自取</param>
        /// <param name="shdzid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPaymentInfo(string jzid, string qjfs, string shdzid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select * from FY030303 where jzid='{0}'  and qjfs='{1}' and shdzid='{2}'", jzid, qjfs, shdzid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PaymentInfo entity = new PaymentInfo();
                        entity.zfje = !dr.IsDBNull(0) ? Convert.ToString(dr.GetDouble(0)) : " ";
                        entity.xzfje = !dr.IsDBNull(1) ? Convert.ToString(dr.GetDouble(1)) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 预约列表(OR030202)yyy_yueyue
        /// </summary>
        /// <param name="openid">用户标识(支付宝USERID或微信OPENID)</param>
        /// <param name="status">预约状态状态： 0：已预约待付款、1：已预约已付款、2：已就诊、3：已失效（包含3:已取消和4:已失约</param>
        /// <param name="sourcetype">9：云医院预约  其他：非云医院预约（值待定，需要联众确认）</param>
        /// <param name="values"></param>
        /// <param name="emsg"></param>
        /// <returns></returns>
        public int QueryAppointment(string openid, string status, string sourcetype, out ArrayList values, out string msg)
        {
            string orderBy = "";
            if (status == "0")
            {
                orderBy = " order by  preengagetime desc  ";
            }
            string statusConditional = string.Empty;
            statusConditional = string.Format(" and status='{0}'", status);
            if (status == "3")
            {
                statusConditional = " and (status='3' or status='4')";
            }
            string sqlQuery = string.Format(" select preengageseq,preengagedate,preengagetime,departcode,departname,doctorcode,doctorname,patientname,patientidcardno,patientphone,patientaddress,preengageno,place,status,fee,registerid from yyy_yueyue where openid='{0}'  {1}  and sourcetype='{2}'  {3}", openid, statusConditional, sourcetype, orderBy);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AppointmentInfo entity = new AppointmentInfo();
                        entity.preengageseq = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.preengagedate = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString("yyyy-MM-dd") : " ";
                        entity.preengagetime = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.departcode = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.departname = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.doctorcode = !dr.IsDBNull(5) ? dr.GetString(5) : " ";
                        entity.doctorname = !dr.IsDBNull(6) ? dr.GetString(6) : " ";
                        entity.patientname = !dr.IsDBNull(7) ? dr.GetString(7) : " ";
                        entity.patientidcardno = !dr.IsDBNull(8) ? dr.GetString(8) : " ";
                        entity.patientphone = !dr.IsDBNull(9) ? dr.GetString(9) : " ";
                        entity.patientaddress = !dr.IsDBNull(10) ? dr.GetString(10) : " ";
                        entity.preengageno = !dr.IsDBNull(11) ? dr.GetString(11) : " ";
                        entity.place = !dr.IsDBNull(12) ? dr.GetString(12) : " ";
                        entity.status = !dr.IsDBNull(13) ? dr.GetDecimal(13).ToString() : " ";
                        entity.fee = !dr.IsDBNull(14) ? dr.GetString(14) : " ";
                        entity.registerno = !dr.IsDBNull(15) ? dr.GetString(15) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 预约详情接口(OR030203)
        /// </summary>
        /// <param name="preengageseq">预约流水号</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryDetailAppointmentInfo(string preengageseq, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select preengageseq,preengagedate,preengagetime,departcode,departname,doctorcode,doctorname,patientid,cardno,patientname,patientidcardno,patientphone,patientaddress,preengageno,place,status,fee,registerid,orderno from yyy_yueyue where  preengageseq='{0}' and rownum=1 ", preengageseq);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AppointmentDetailInfo entity = new AppointmentDetailInfo();
                        entity.preengageseq = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.preengagedate = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString("yyyy-MM-dd") : " ";
                        entity.preengagetime = !dr.IsDBNull(2) ? dr.GetString(2).ToString() : " ";
                        entity.departcode = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.departname = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.doctorcode = !dr.IsDBNull(5) ? dr.GetString(5) : " ";
                        entity.doctorname = !dr.IsDBNull(6) ? dr.GetString(6) : " ";
                        entity.patientid = !dr.IsDBNull(7) ? dr.GetString(7) : " ";
                        entity.cardno = !dr.IsDBNull(8) ? dr.GetString(8) : " ";
                        entity.patientname = !dr.IsDBNull(9) ? dr.GetString(9) : " ";
                        entity.patientidcardno = !dr.IsDBNull(10) ? dr.GetString(10) : " ";
                        entity.patientphone = !dr.IsDBNull(11) ? dr.GetString(11) : " ";
                        entity.patientaddress = !dr.IsDBNull(12) ? dr.GetString(12) : " ";
                        entity.preengageno = !dr.IsDBNull(13) ? dr.GetString(13) : " ";
                        entity.place = !dr.IsDBNull(14) ? dr.GetString(14) : " ";
                        entity.status = !dr.IsDBNull(15) ? dr.GetDecimal(15).ToString() : " ";
                        entity.fee = !dr.IsDBNull(16) ? dr.GetString(16) : " ";
                        entity.registerno = !dr.IsDBNull(17) ? dr.GetString(17) : " ";
                        entity.orderno = !dr.IsDBNull(18) ? dr.GetString(18) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public PatientInfo2 QueryUserInfo(string orderNo)
        {
            string sqlQuery = string.Format("select czje,brxm,openid,brid from A_YL_DD where YYLSH='{0}'", orderNo);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            PatientInfo2 entity = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entity = new PatientInfo2();
                        entity.czje = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.brxm = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.openid = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.brid = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : " ";

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public int HisStatus(string orderNo)
        {
            string sqlQuery = string.Format("select count(1)  from yyy_xiancheng_orderno t3 where t3.Orderno ='{0}'", orderNo);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            int result = -1;
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
            return result;

        }

        /// <summary>
        /// 根据订单号返回支付宝交易号和批次号（PO010102）
        /// </summary>
        /// <param name="orderno">医院订单号</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryChargeNumber(string orderNo, string paytype, out ArrayList values, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string nextval = "";
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            string msg2 = "";
            try
            {
                PatientInfo2 entity2 = QueryUserInfo(orderNo);
                if (entity2 == null)
                {
                    msg = "无数据";
                    values = null;
                    return -1;
                }
                if (GetDingDanStatus2(orderNo) == "1")
                {
                    EditOrderStatus(orderNo, "3", out msg2);
                }
                int status = 0;
                if (HisStatus(orderNo) == 0)
                {
                    status = 2;
                }
                nextval = DateTime.Now.ToString("yyyyMMddHHmmsssss");
                string insertSql = string.Format("insert into A_YL_TFDD(TRADENO,BATCHNO,YHMC,JYZT,TKJE,BRXM,PAYTYPE,OPENID,BRID,ZFBZ) values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}')", orderNo.ToLower().Trim('a').Trim('p').Trim('p'), nextval, "云医院支付宝", status, entity2.czje, entity2.brxm, paytype, entity2.openid, entity2.brid, 0);
                int insertResult = DbHelperOra.ExecuteSql(insertSql, connection);
                string sqlQuery = string.Format("select PAYMENTTRADENO,MONEY from ZFB_SHTZ where TRADENO='{0}' ", orderNo.ToUpper().Replace("APP", ""));
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TuiKuanModel entity = new TuiKuanModel();
                        entity.tradeno = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.xzfje = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.batchno = nextval;
                        values.Add(entity);
                    }
                }
                else
                {
                    if (GetCzjzByOrderNo(orderNo.ToUpper().Replace("APP", "")) == "0")
                    {
                        TuiKuanModel entity = new TuiKuanModel();
                        entity.batchno = nextval;
                        entity.tradeno = "0";
                        values.Add(entity);
                    }
                    else
                    {

                        TuiKuanModel entity = new TuiKuanModel();
                        entity.batchno = nextval;
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 根据预约流水号进行预挂号获取支付金额(OR030204)
        /// </summary>
        /// <param name="preengageseq">预约流水号</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPayMoneyList(string preengageseq, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select alipaymoney,countmoney,totalmoney from OR030204 where preengageseq='{0}'", preengageseq);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PayMoneyModel entity = new PayMoneyModel();
                        entity.alipaymoney = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.countmoney = !dr.IsDBNull(1) ? dr.GetDecimal(1).ToString() : " ";
                        entity.totalmoney = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 可缴费列表（FY030307）
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPaymentList(string brid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select jzid,zzd,rq,ys from yyy_keshouflb_jzid where brid='{0}'", brid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PaymentDetailInfo entity = new PaymentDetailInfo();
                        entity.jzid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.zzd = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.rq = !dr.IsDBNull(2) ? dr.GetDateTime(2).ToString() : " ";
                        entity.ys = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 可退费列表（FY030311）
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryRefundList(string brid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select orderno,zzd,sfrq,je from YYY_YISHOUFLB   where brid='{0}'", brid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        RefundInfo entity = new RefundInfo();
                        entity.orderno = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.zzd = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.rq = !dr.IsDBNull(2) ? dr.GetDateTime(2).ToString() : " ";
                        entity.je = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 更新订单状态（FY030308）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int EditOrderStatus(string orderno, string status, out string msg)
        {
            int result = -1;
            string updateSql = string.Format("update A_YL_DD set DDZT='{0}' where YYLSH='{1}' ", status, orderno.ToUpper().Replace("APP", ""));
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                OracleCommand cmd = connection.CreateCommand();
                cmd.CommandText = updateSql;
                connection.Open();
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                }
            }
            catch (Exception ex)
            {
                msg = "fail";
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 获取病人信息（UI020202）yyy_bingrenxx_im
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPatientInfoByBrid(string brid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select brid,name,sex,age,zjjzsj from yyy_bingrenxx_im where brid='{0}'", brid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PatientInfoByCode entity = new PatientInfoByCode();
                        entity.patientcode = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.name = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sex = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.age = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : " ";
                        entity.firstvisit = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }


        /// <summary>
        /// 获取病人信息(FY030309)
        /// </summary>
        /// <param name="patientcode"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryPayPatientInfo(string orderno, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select * from FY030309 where orderno='{0}'", orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PatientInfoByOrderNumber entity = new PatientInfoByOrderNumber();
                        entity.shdzid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.jzid = !dr.IsDBNull(1) ? dr.GetString(0) : " ";
                        entity.paymenttype = !dr.IsDBNull(2) ? dr.GetString(0) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 线程获取缴费表的订单状态为0的订单（FY030314）
        /// </summary>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryOrderList1(out ArrayList values, out string msg)
        {
            string sqlQuery = "select t1.yylsh,t2.paymenttradeno from A_YL_DD t1,ZFB_SHTZ t2 where t1.yylsh=t2.tradeno(+) and t1.ddzt=0  and t1.cssj<= sysdate-1/24 ";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OrderModel entity = new OrderModel();
                        entity.orderno = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.tradeno = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">医院订单号</param>
        /// <param name="jyzt">-1=退his失败、0=默认状态、1=退his成功、2=退支付宝成功、3=退支付宝失败</param>
        /// <returns></returns>
        public int InsertTuiFeiDDInfo(string orderNo, int jyzt = 0)
        {
            PatientInfo2 entity2 = QueryUserInfo(orderNo);
            if (entity2 == null)
            {
                return -1;
            }
            string nextval = DateTime.Now.ToString("yyyyMMddHHmmsssss");
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string insertSql = string.Format("insert into A_YL_TFDD(TRADENO,BATCHNO,YHMC,JYZT,TKJE,BRXM,PAYTYPE,OPENID,BRID,ZFBZ) values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}')", orderNo.ToLower().Trim('a').Trim('p').Trim('p'), nextval, "云医院支付宝", jyzt, entity2.czje, entity2.brxm, GetPayType(orderNo), entity2.openid, entity2.brid, 0);
            try
            {
                int result = DbHelperOra.ExecuteSql(insertSql, connection);
                if (result > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 根据正交易的paytype找到退费的paytype
        /// </summary>
        /// <param name="orderNo">医院订单号</param>
        /// <returns></returns>
        public string GetPayType(string orderNo)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string sqlQuery = string.Format("Select Nvl(b.Paytypetfddmc, '其他交易')  From a_Yl_Dd a, Yyy_Paytype b Where a.Paytype = b.Paytypeddmc   And a.Yylsh = '{0}'", orderNo);
            object result;
            try
            {
                result = DbHelperOra.GetSingle(sqlQuery, connection);
                if (result == null)
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                connection.Close();
            }
            return result.ToString();
        }

        /// <summary>
        /// 线程获取缴费表的订单状态为1的订单（FY030315）  (-1=支付宝失败、0=初始状态、1=支付宝成功his未操作、2=his成功、3=支付宝成功his失败)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryOrderList2(out ArrayList values, out string msg)
        {
            int result = -1;
            string sqlQuery = "select t1.yylsh,t2.paymenttradeno,(select count(1)  from yyy_xiancheng_orderno t3 where t3.Orderno = to_char(t1.yylsh))  as shuliang,czje from A_YL_DD t1,ZFB_SHTZ t2  where t1.yylsh=t2.tradeno(+) and t1.ddzt=1  and t1.cssj<= sysdate-1/24";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OrderModel entity = new OrderModel();
                        entity.orderno = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.tradeno = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.HisResult = !dr.IsDBNull(2) ? (dr.GetDecimal(2).ToString() == "0" ? "fail" : "success") : " ";
                        entity.Czje = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : "0";
                        values.Add(entity);
                    }
                }
                foreach (OrderModel item in values)
                {
                    string s;
                    if (item.HisResult == "success")//his返回结果是成功
                    {
                        EditOrderStatus(item.orderno, "2", out s);
                    }
                    else
                    {
                        EditOrderStatus(item.orderno, "3", out s);//his返回结果失败
                        if (item.Czje == "0" || item.Czje == "0.00")
                        {
                            InsertTuiFeiDDInfo(item.orderno, 2);
                        }
                        else
                        {
                            InsertTuiFeiDDInfo(item.orderno, 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 线程获取退费表的订单状态为0的订单（FY030316）
        /// </summary>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReturnList1(out ArrayList values, out string msg)
        {
            string sqlQuery = "select t1.tradeno,t1.batchno,(select count(1)  from yyy_xiancheng_orderno t3 where t3.Orderno = to_char(t1.batchno))  as shuliang from A_YL_TFDD t1 where t1.jyzt = 0   and t1.cjsj <= sysdate - 1 / 24";//his状态从yyy_xiancheng_orderno
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReturnModel1 entity = new ReturnModel1();
                        entity.hisstatus = !dr.IsDBNull(2) ? (dr.GetDecimal(2).ToString() == "0" ? "fail" : "success") : " ";
                        entity.batchno = !dr.IsDBNull(1) ? dr.GetDecimal(1).ToString() : " ";
                        entity.tradeno = !dr.IsDBNull(0) ? dr.GetString(0).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            foreach (ReturnModel1 item in values)
            {
                string ddzt = GetDingDanStatus2(item.tradeno);
                if (ddzt == "2")
                {
                    if (item.hisstatus == "success")
                    {
                        UpdateTFStatus(item.batchno, "1");//已经
                    }
                    else
                    {
                        UpdateTFStatus(item.batchno, "-1");//
                    }
                }
                if (ddzt == "3")
                {
                    UpdateTFStatus(item.batchno, "1");//已经
                }
                if (ddzt == "-1")
                {
                    UpdateTFStatus(item.batchno, "-1");//已经
                }

            }
            return 1;
        }



        /// <summary>
        /// 线程获取退费表的订单状态为1的订单（FY030318）
        /// </summary>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReturnList2(out ArrayList values, out string msg)
        {
            string sqlQuery = "select t1.tkje,t1.batchno,t2.paymenttradeno from A_YL_TFDD t1,ZFB_SHTZ t2 where t1.tradeno=t2.tradeno(+) and t1.jyzt=1  and t1.cjsj<= sysdate-1/24";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReturnModel2 entity = new ReturnModel2();
                        entity.totalfee = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.batchno = !dr.IsDBNull(1) ? dr.GetDecimal(1).ToString() : " ";
                        entity.tradeno = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }


        /// <summary>
        /// 更新退费订单状态
        /// </summary>
        /// <param name="batchno"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateTFStatus(string batchno, string status)
        {
            int result = 0;
            string updateSql = string.Format("update A_YL_TFDD set JYZT={0} where BATCHNO={1} ", status, batchno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                connection.Open();
                OracleCommand cmd1 = connection.CreateCommand();
                cmd1.CommandText = updateSql;
                result = cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }


        /// <summary>
        /// 更新退费订单状态
        /// </summary>
        /// <param name="batchno"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateTFStatusByOrderno(string orderNo, string status)
        {
            int result = 0;
            string updateSql = string.Format("update A_YL_TFDD set JYZT={0} where TRADENO={1} ", status, orderNo);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                connection.Open();
                OracleCommand cmd1 = connection.CreateCommand();
                cmd1.CommandText = updateSql;
                result = cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        /// <summary>
        /// 获取退费商户通知
        /// </summary>
        /// <param name="batchno"></param>
        /// <returns></returns>
        public int TfShtzCount(string batchno)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            int count = 0;
            string sql = string.Format(" select count(*) from ZFB_SHTZ_TF where  batchno=", batchno);
            dr = DbHelperOra.ExecuteReader(sql, connection);
            if (dr.Read())
            {
                count = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
            }
            return count;
        }

        /// <summary>
        /// 更新退费表的订单状态（FY030317）
        /// </summary>
        /// <param name="batchno">批次号</param>
        /// <param name="status">订单状态</param>
        /// <param name="notifytime">支付宝通知时间</param>
        /// <param name="successnum">成功笔数</param>
        /// <param name="tradestatus">交易状态</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int EditReturnStatus(string batchno, string status, string notifytime, string successnum, string paymentbatchno, string paymentparameters, string money, out string msg)
        {
            int result1 = -1;
            int result2 = -1;
            TfDingDanInfo entity = GetInfoByBatchno(batchno);
            string openid = "";
            string name = "";
            string patientId = "";
            if (entity != null)
            {
                openid = entity.OpenId;
                name = entity.Name;
                patientId = entity.PatientId;
            }

            int count = TfShtzCount(batchno);
            if (count > 0)
            {
                msg = "退费商户通知已经存在";
                return 1;
            }

            string insertSql = string.Format("insert into ZFB_SHTZ_TF(BATCHNO,STATUS,NOTIFYTIME,SUCCESSNUM,OPENID,NAME,PATIENTID,PAYMENTBATCHNO,MONEY,PAYMENTPARAMETERS,INPATIENTNO) values('{0}',{1},to_date('{2}','yyyy/mm/dd HH24:MI:SS'),{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}')", batchno, status, notifytime, successnum, openid, name, patientId, paymentbatchno, money, paymentparameters, "INIENTNO");
            string updateSql = string.Format("update A_YL_TFDD set JYZT='{0}' where BATCHNO='{1}' ", status, batchno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                connection.Open();
                OracleTransaction mySqlTransaction = connection.BeginTransaction();
                OracleCommand cmd1 = connection.CreateCommand();
                cmd1.Transaction = mySqlTransaction;
                cmd1.CommandText = insertSql;
                result1 = cmd1.ExecuteNonQuery();

                cmd1.CommandText = updateSql;
                result2 = cmd1.ExecuteNonQuery();
                mySqlTransaction.Commit();
                if (result1 >= 0 && result2 >= 0)
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                msg = "fail";
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return 1;
        }

        /// <summary>
        /// 获取收费记录列表（FY030320）
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryChargeList(string brid, string openid, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format("select orderno,jztime,je,paytype,brname,status from yyy_shoufeijl where brid='{0}' and openid='{1}'", brid, openid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ChargeModel entity = new ChargeModel();
                        entity.orderno = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.jztime = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString() : " ";
                        entity.je = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        entity.paytype = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.brname = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.status = !dr.IsDBNull(5) ? dr.GetString(5) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 插入或更新收货人信息（FY030321）
        /// </summary>
        /// <param name="type">操作类型(插入：insert，更新：update)</param>
        /// <param name="shdzid">收货地址id(插入：shdzid为空，更新：shdzid不能为空)</param>
        /// <param name="sjrxm">收件人姓名</param>
        /// <param name="lxdh">联系电话</param>
        /// <param name="sjdz">收件地址</param>
        /// <param name="mrbz">默认标志</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int UpdateOrInsertReceiverInfo(string type, string openid, string shdzid, string sjrxm, string lxdh, string sjdz, string mrbz, out string msg)
        {
            string sql = "";
            msg = "";
            int result = -1;

            if (type == "insert")
            {
                if (mrbz == "1")
                {
                    UpdateReceiverMrbz(openid);
                }
                sql = string.Format("insert into yyy_shouhuodzxx(openid,sjrxm,lxdh,sjdz,shdzid,mrbz) values('{0}','{1}','{2}','{3}',SEQ_YYY_SHOUHUODZ.nextval,'{4}') ", openid, sjrxm, lxdh, sjdz, mrbz);
            }

            if (type == "update")
            {
                //收货地址id
                if (mrbz == "1")
                {
                    UpdateReceiverMrbz(openid);
                    sql = string.Format("update  yyy_shouhuodzxx set sjrxm='{0}',lxdh='{1}',sjdz='{2}',mrbz='{3}' where shdzid='{4}'", sjrxm, lxdh, sjdz, mrbz, shdzid);
                }
                else
                {
                    sql = string.Format("update  yyy_shouhuodzxx set sjrxm='{0}',lxdh='{1}',sjdz='{2}',mrbz='{3}' where shdzid='{4}'", sjrxm, lxdh, sjdz, mrbz, shdzid);
                }

            }
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                connection.Open();
                OracleCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                }
            }
            catch (Exception ex)
            {
                msg = "fail";
            }
            finally
            {
                connection.Close();
            }
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UpdateReceiverMrbz(string openid)
        {
            int result = -1;
            string updateSql = string.Format("update yyy_shouhuodzxx set mrbz='0' where openid='{0}' ", openid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                OracleCommand cmd = connection.CreateCommand();
                cmd.CommandText = updateSql;
                connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 删除收货人信息（FY030322）
        /// </summary>
        /// <param name="shdzid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DeleteReceiverInfo(string shdzid, out string msg)
        {
            string sqlQuery = string.Format("delete from yyy_shouhuodzxx where shdzid='{0}'", shdzid);
            int result = -1;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                connection.Open();
                OracleCommand cmd = connection.CreateCommand();
                cmd.CommandText = sqlQuery;
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    msg = "success";
                }
                else
                {
                    msg = "fail";
                }
            }
            catch (Exception ex)
            {
                msg = "fail";
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 查询模块-处方单列表（FY030323）
        /// </summary>
        /// <param name="brid">病人id</param>
        /// <param name="msg"></param>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        public int GetChuFangListByQuery(string brid, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format("select cflsh,ksmc,Lxmc,zjje,ysxm,kfrq from zfb_chufang where brid='{0}'", brid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ChuFangModel entity = new ChuFangModel();
                        entity.cflsh = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.jzks = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.cflx = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.zjje = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : " ";
                        entity.ysxm = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.jzrq = !dr.IsDBNull(5) ? dr.GetString(5) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 查询模块-处方单详情（FY030324）
        /// </summary>
        /// <param name="cflsh">处方流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetChuFangDetailByQuery(string cflsh, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format("select ypmc,ypxx,sync,gytj,ycyl,yishengjianyi from zfb_chufang_mx where cflsh='{0}'", cflsh);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ChuFangDetailModel entity = new ChuFangDetailModel();
                        entity.ypmc = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.ypxx = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sypc = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.gytj = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.yl1 = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.yl2 = !dr.IsDBNull(5) ? dr.GetString(5) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 查询模块-检验单或检查单列表（FY030325）
        /// </summary>
        /// <param name="brid">病人id</param>
        /// <param name="type">查询类型 检查单：jcd，检验单：jyd</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetJyInfoList(string brid, string type, out string msg, out ArrayList values)
        {
            msg = "";
            values = new ArrayList();
            if (type == "jyd")
            {
                string sqlQuery = string.Format(" select jyjcdlsh,jyjcdmc,jyjcrq from yyy_jianyan_qd where brid='{0}'", brid);
                OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
                OracleDataReader dr = null;
                try
                {
                    dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            JianYanModel entity = new JianYanModel();
                            entity.jyjcdlsh = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                            entity.jyjcdmc = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                            entity.jyjcrq = !dr.IsDBNull(2) ? dr.GetDateTime(2).ToString() : " ";
                            values.Add(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
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


            if (type == "jcd")
            {
                string sqlQuery = string.Format(" select jyjcdlsh,jyjcdmc,jyjcrq from yyy_jiancha_qd where brid='{0}'", brid); ;
                OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
                OracleDataReader dr = null;
                try
                {
                    dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            JianYanModel entity = new JianYanModel();
                            entity.jyjcdlsh = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                            entity.jyjcdmc = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                            entity.jyjcrq = !dr.IsDBNull(2) ? dr.GetDateTime(2).ToString() : " ";
                            values.Add(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
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

                #region 正式
                //string sqlStr = string.Format(" select jyjcdlsh,jyjcdmc,jyjcrq from  YYY_jclb where brid='{0}'", brid);
                //SqlConnection sqlConn = new SqlConnection(WebConfigParameter.ConnectionLisString);
                //SqlDataReader sqlDr = null;
                //try
                //{
                //    sqlDr = DbHelperSQL.ExecuteReader(sqlStr, sqlConn);
                //    if (sqlDr.HasRows)
                //    {
                //        while (sqlDr.Read())
                //        {
                //            JianYanModel entity = new JianYanModel();
                //            entity.jyjcdlsh = !sqlDr.IsDBNull(0) ? sqlDr.GetString(0) : " ";
                //            entity.jyjcdmc = !sqlDr.IsDBNull(1) ? sqlDr.GetString(1) : " ";
                //            entity.jyjcrq = !sqlDr.IsDBNull(2) ? sqlDr.GetDateTime(2).ToString() : " ";
                //            values.Add(entity);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    msg = GetExceptionInfo(ex);
                //    values = null;
                //    return -1;
                //}
                //finally
                //{
                //    if (null != sqlDr)
                //    {
                //        sqlDr.Close();
                //    }
                //    sqlConn.Close();
                //}
                #endregion

            }
            return 1;
        }

        /// <summary>
        /// 查询模块-检验单详情基本信息（FY030326）
        /// </summary>
        /// <param name="jydlsh">检验单流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetJyDetailInfo(string jydlsh, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format(" select sqks,jynr,jyr,sjr,shr,cjsj,jysj,bbmc from   yyy_jianyan_qd where jydlsh='{0}'", jydlsh);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        JianYanDetailModel entity = new JianYanDetailModel();
                        entity.sqks = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.jynr = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.jyr = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.sjr = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.shr = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.cjsj = !dr.IsDBNull(5) ? dr.GetDateTime(5).ToString() : " ";
                        entity.jysj = !dr.IsDBNull(6) ? dr.GetDateTime(6).ToString() : " ";
                        entity.bbmc = !dr.IsDBNull(7) ? dr.GetString(7) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 查询模块-检验单详情检验项目列表（FY030327）
        /// </summary>
        /// <param name="jydlsh">检验单流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetJyDetailInfo2(string jydlsh, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format("select jyxmmc,ckfw,jyxmz,bz from yyy_jianyan_mx where jydlsh='{0}'", jydlsh);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        JianYanDetailModel2 entity = new JianYanDetailModel2();
                        entity.jyxmmc = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.ckfw = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.jyxmz = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.bz = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 查询模块-检查单详情基本信息（FY030328）
        /// </summary>
        /// <param name="jcdlsh">检查单流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetJyBasicInfo(string jcdlsh, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format("select sqks,jynr,jyr,sjr,shr,cjsj,jysj from  yyy_jiancha_mx where jcdlsh='{0}'", jcdlsh);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        JianYanBasicModel entity = new JianYanBasicModel();
                        entity.sqks = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.jynr = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.jyr = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.sjr = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                        entity.shr = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                        entity.cjsj = !dr.IsDBNull(5) ? dr.GetString(5) : " ";
                        entity.jysj = !dr.IsDBNull(6) ? dr.GetDateTime(6).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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

            #region 正式
            //SqlConnection sqlConn = new SqlConnection(WebConfigParameter.ConnectionLisString);
            //SqlDataReader sqlDr = null;
            //string sqlStr = string.Format("select sqks,jynr,jyr,sjr,shr,cjsj,jysj from   YYY_jcjg where jydlsh='{0}'", jcdlsh);
            //values = new ArrayList();
            //try
            //{
            //    sqlDr = DbHelperSQL.ExecuteReader(sqlQuery, sqlConn);
            //    if (sqlDr.HasRows)
            //    {
            //        while (sqlDr.Read())
            //        {
            //            JianYanBasicModel entity = new JianYanBasicModel();
            //            entity.sqks = !sqlDr.IsDBNull(0) ? sqlDr.GetString(0) : " ";
            //            entity.jynr = !sqlDr.IsDBNull(1) ? sqlDr.GetString(1) : " ";
            //            entity.jyr = !sqlDr.IsDBNull(2) ? sqlDr.GetString(2) : " ";
            //            entity.sjr = !sqlDr.IsDBNull(3) ? sqlDr.GetString(3) : " ";
            //            entity.shr = !sqlDr.IsDBNull(4) ? sqlDr.GetString(4) : " ";
            //            entity.cjsj = !sqlDr.IsDBNull(5) ? sqlDr.GetString(5) : " ";
            //            entity.jysj = !sqlDr.IsDBNull(6) ? sqlDr.GetDateTime(6).ToString() : " ";
            //            values.Add(entity);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    msg = GetExceptionInfo(ex);
            //    values = null;
            //    return -1;
            //}
            //finally
            //{
            //    if (null != sqlDr)
            //    {
            //        sqlDr.Close();
            //    }
            //    sqlConn.Close();
            //}
            #endregion
            return 1;
        }

        /// <summary>
        /// 查询模块-检查单详情检查项目列表（FY030329）
        /// </summary>
        /// <param name="jcdlsh"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetJyInfoDetailList(string jcdlsh, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format("select jcsj,jczd from  yyy_jiancha_mx where jcdlsh='{0}'", jcdlsh);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        JianYanInfoDetailModel entity = new JianYanInfoDetailModel();
                        entity.jcsj = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.jczd = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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

            #region 正式
            //SqlConnection sqlConn = new SqlConnection(WebConfigParameter.ConnectionLisString);
            //SqlDataReader sqlDr = null;
            //string sqlStr = string.Format("select jcsj,jczd from  yyy_jiancha_mx where jydlsh='{0}'", jcdlsh);
            //values = new ArrayList();
            //try
            //{
            //    sqlDr = DbHelperSQL.ExecuteReader(sqlQuery, sqlConn);
            //    if (dr.HasRows)
            //    {
            //        while (sqlDr.Read())
            //        {
            //            JianYanInfoDetailModel entity = new JianYanInfoDetailModel();
            //            entity.jcsj = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
            //            entity.jczd = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
            //            values.Add(entity);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    msg = GetExceptionInfo(ex);
            //    values = null;
            //    return -1;
            //}
            //finally
            //{
            //    if (null != sqlDr)
            //    {
            //        sqlDr.Close();
            //    }
            //    sqlConn.Close();
            //}
            #endregion

            return 1;
        }

        /// <summary>
        /// 判断是否可退费（FY030330）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int IsTuiFeiState(string orderno, out string msg, out ArrayList values)
        {
            int result = -1;
            values = new ArrayList();
            msg = "";
            try
            {
                using (OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString))
                {
                    conn.Open();
                    OracleCommand ocm = conn.CreateCommand();
                    ocm.CommandType = CommandType.StoredProcedure;
                    ocm.CommandText = "PRC_MZ_SHOUFEIFHPD";
                    ocm.Parameters.AddWithValue("Prm_orderno", orderno).Direction = ParameterDirection.Input;
                    ocm.Parameters.Add("Prm_Appcode", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                    ocm.Parameters.Add("Prm_Databuffer", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                    ocm.ExecuteNonQuery();
                    TuiFeiState entity = new TuiFeiState();
                    entity.flag = ocm.Parameters["Prm_Appcode"].Value.ToString();
                    entity.msg = ocm.Parameters["Prm_Databuffer"].Value.ToString();
                    values.Add(entity);
                    conn.Close();
                }
                result = 1;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                values = null;
                return -1;
            }
            return result;
        }

        /// <summary>
        /// 医生信息列表_按医生代码查(DO010104)
        /// </summary>
        /// <param name="namepy"></param>
        /// <param name="haskey"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryDoctorList(string namepy, string haskey, int pageindex, int pagesize, out ArrayList values, out string msg)
        {
            long maxrow;
            long minrow;
            General.CalculatePage(pageindex, pagesize, out maxrow, out minrow);
            string sqlQuery = "";
            if (haskey == "0")
            {
                sqlQuery = string.Format("Select Doctorcode, Doctorname,Sex,Pictureurl, Level1,Recommend, Adept, Departcode,Departname,sum(Reserve) Yysl,xuhao From Zfb_Yuyue_Ys t where (namepy like upper('%{0}%') or doctorname like '%{0}%') Group By xuhao,Doctorcode,Doctorname,Sex,Pictureurl,Level1,Recommend,Adept,Departcode,Departname Order By xuhao,doctorname ", namepy);

            }
            if (haskey == "1")
            {
                sqlQuery = string.Format("Select Doctorcode, Doctorname,Sex,Pictureurl, Level1,Recommend, Adept, Departcode,Departname,sum(Reserve) Yysl,xuhao From Zfb_Yuyue_Ys t Group By xuhao,Doctorcode,Doctorname,Sex,Pictureurl,Level1,Recommend,Adept,Departcode,Departname Order By xuhao,doctorname");
            }
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
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
                        pd.reserve = !dr.IsDBNull(9) ? dr.GetDecimal(9).ToString() : "";
                        values.Add(pd);

                    }
                    msg = "查找到记录";
                }
                else
                {
                    msg = "亲，没有记录";
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
            return 1;
        }


        /// <summary>
        /// 查询模块——缴费退费清单列表（FY030331）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryChargeListByOrderno31(string orderno, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select fpxmid,fpxmmc,fpxmje from yyy_shoufeijl_qd   where orderno='{0}' ", orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ChargeEntity entity = new ChargeEntity();
                        entity.fpxmid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.fpxmmc = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.fpxmje = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 查询模块——缴费退费清单明细（FY030332）
        /// </summary>
        /// <param name="fpxmid">发票项目id</param>
        /// <param name="orderno">支付宝医院订单id</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryChargeListByOrderno32(string fpxmid, string orderno, out ArrayList values, out string msg)
        {
            string sqlQuery = string.Format(" select sfxmmc,sfxmsl,sfxmje from yyy_shoufeijl_mx where fpxmid='{0}' and orderno='{1}' ", fpxmid, orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HuaYanInfo entity = new HuaYanInfo();
                        entity.sfxmmc = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.sfxmsl = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.sfxmje = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 根据订单号获取订单状态值
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public int GetDingDanStatus(long orderno)
        {
            int result = -100;
            string sqlQuery = string.Format("select DDZT from a_yl_dd where yylsh={0}", orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception)
            {

                return -1;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 根据订单号获取订单状态值
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public string GetDingDanStatus2(string orderno)
        {
            string result = "";
            string sqlQuery = string.Format("select DDZT from a_yl_dd where yylsh={0}", orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = Convert.ToString(obj);
                }
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// 是否含有病人信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public int HasPatientInfo(string patientId, string cardno)
        {
            int result = 0;
            string sqlQuery = string.Format(" select count(*) from zfb_menzhenklb where patientid='{0}' and cardno='{1}' ", patientId, cardno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public DingDanInfo GetInfoByOrderno(string orderno)
        {
            string sqlQuery = string.Format("select jzid,shdzid,paytype,czje,qjfs from A_YL_DD where yylsh='{0}'", orderno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            DingDanInfo entity = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entity = new DingDanInfo();
                        entity.Jzid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.shdzid = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.PayType = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                        entity.Je = !dr.IsDBNull(3) ? dr.GetDecimal(3).ToString() : " ";
                        entity.Qjfs = !dr.IsDBNull(4) ? dr.GetDecimal(4).ToString() : " ";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
            return entity;
        }


        /// <summary>
        /// 根据退订单好获取各人信息
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public TfDingDanInfo GetInfoByBatchno(string batchno)
        {
            string sqlQuery = string.Format("select openid,brxm,brid from A_YL_TFDD where batchno='{0}'", batchno);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            TfDingDanInfo entity = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entity = new TfDingDanInfo();
                        entity.OpenId = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.Name = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.PatientId = !dr.IsDBNull(2) ? dr.GetString(2) : " ";

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
            return entity;
        }

        /// <summary>
        /// 根据订单号获取订单状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public string GetPayType2(string orderNo)
        {
            string sqlQuery = string.Format(" select paytype from A_YL_DD where YYLSH='{0}' ", orderNo);
            OracleDataReader dr = null;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string result = string.Empty;
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <returns></returns>
        public string GetCzjzByOrderNo(string tradeNo)
        {
            string sqlQuery = string.Format(" select czje from A_YL_DD where YYLSH='{0}' ", tradeNo);
            OracleDataReader dr = null;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string result = string.Empty;
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shdzid"></param>
        /// <returns></returns>
        public string GetShdzInfoById(string shdzid)
        {
            string sqlQuery = string.Format(" select SJRXM,LXDH,SJDZ from  YYY_SHOUHUODZXX where shdzid='{0}'", shdzid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            ShdzInfo entity = null;
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entity = new ShdzInfo();
                        entity.SJRXM = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        entity.LXDH = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        entity.SJDZ = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                    }

                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
            if (entity == null)
            {
                return "";
            }
            return entity.SJRXM + "|" + entity.LXDH + "|" + entity.SJDZ;
        }

        /// <summary>
        /// 根据挂号id获取交易号
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public string GetOrderNoByRegisterId(string registerId)
        {
            string sqlQuery = string.Format("select orderno from YYY_YUEYUE where registerId='{0}' and rownum=1 ", registerId);
            string result = string.Empty;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = Convert.ToString(obj);
                }
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 根据订单号获取退款批次号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public string GetBatchNoByOrderNo(string orderNo)
        {
            string sqlQuery = string.Format(" select batchno from A_YL_TFDD where tradeno='{0}' and rownum=1 order by cjsj desc ", orderNo);
            string result = string.Empty;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                object obj = DbHelperOra.GetSingle(sqlQuery, connection);
                if (obj != null)
                {
                    result = Convert.ToString(obj);
                }
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 根据排序号获取医生的排班信息
        /// </summary>
        /// <param name="scheduleseq">排班序号</param>
        /// <param name="shiftcode">1=上午，2=下午</param>
        /// <param name="orderDate">预约时间</param>
        /// <returns></returns>
        public int GetNeededOrderCount(string scheduleseq, string shiftcode, string orderDate)
        {
            string sqlQuery = string.Format("select orderno, yiyongyuyuehao, orderseq, Doctorcode, Shiftcode, Week, Departcode, scheduledate from zfb_yuyue_mzhy t where Scheduleseq = '{0}' and shiftcode = {1} and rowNum=1 ", scheduleseq, shiftcode);
            int result = 0;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
            string totalOrderno = "";
            string yyOrderno = "";
            AlipayQueryOrderSeq info = new AlipayQueryOrderSeq();

            if (null != dr && dr.HasRows)
            {
                while (dr.Read())
                {
                    totalOrderno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    yyOrderno = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    info.orderseq = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    info.doctorcode = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    info.shiftcode = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    info.week = !dr.IsDBNull(5) ? Convert.ToString(dr.GetInt32(5)) : "";
                    info.departcode = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                    info.scheduledate = !dr.IsDBNull(7) ? dr.GetDateTime(7).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                    info.orderno = GetOrderseqString(totalOrderno, yyOrderno);
                    break;
                }
            }
            if (null != dr)
            {
                dr.Close();
            }
            connection.Close();

            string dateConditional = " 1=1";
            if (DateTime.Now.ToString("yyyy-MM-dd") == orderDate)
            {
                dateConditional = " ordertime>=to_char(Sysdate+1/48 ,'HH24:MI')";
            }
            string countSql = string.Format("select count(*) from zfb_yuyue_xhsj t where Departcode = '{0}' and week = {1} and shiftcode = {2} and doctorcode = '{3}' and orderno in ({4}) and {5} order by ordertime", info.departcode, info.week, info.shiftcode, info.doctorcode, info.orderno, dateConditional);
            if (string.IsNullOrEmpty(info.orderno))
            {
                return 0;
            }
            try
            {
                object obj = DbHelperOra.GetSingle(countSql, connection);
                if (obj != null)
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="date"></param>
        /// <param name="shiftCode"></param>
        /// <returns></returns>
        public bool IsYuYue(string openid, string date, string shiftCode)
        {
            string querySql = string.Format("Select ORDERNO From yyy_yueyue a Where a.OPENID='{0}' And to_char(a.PREENGAGEDATE,'yyyy-MM-dd')='{1}' And a.SHIFTCODE='{2}' And a.sourcetype='{3}' and a.status!=3 and a.status!=4", openid, date, shiftCode, 9);
            string orderNo = string.Empty;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                object obj = DbHelperOra.GetSingle(querySql, connection);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    orderNo = obj.ToString();
                }
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                connection.Close();
            }

            string openid2 = string.Empty;
            string sqlQuery2 = string.Format("select openid from A_YL_DD where yylsh='{0}'", orderNo);
            try
            {
                object obj2 = DbHelperOra.GetSingle(sqlQuery2, connection);
                if (obj2 == null)
                {
                    return true;
                }
                else
                {
                    openid2 = obj2.ToString();
                    if (openid == openid2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="ysid"></param>
        /// <returns></returns>
        public int GetSfInfo(string brid, string ysid, out string msg, out ArrayList values)
        {
            string sqlQuery = string.Format("select zfy,sflx from yyy_keshouflb_jftz where brid='{0}' and ysid='{1}' and rownum=1 ", brid, ysid);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            msg = "";
            try
            {
                dr = DbHelperOra.ExecuteReader(sqlQuery, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        KeShouFei entity = new KeShouFei();
                        entity.Zfy = !dr.IsDBNull(0) ? dr.GetDecimal(0).ToString() : " ";
                        entity.Sflx = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        values.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
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
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalOrderseq"></param>
        /// <param name="yyOrderseq"></param>
        /// <returns></returns>
        private string GetOrderseqString(string totalOrderseq, string yyOrderseq)
        {
            string[] arr1 = totalOrderseq.Split("^".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            string kyOrderseq = "";
            if (string.IsNullOrEmpty(yyOrderseq))
            {
                foreach (string s in arr1)
                {
                    sb.AppendFormat("'{0}',", s);
                }
            }
            else
            {
                string[] arr2 = yyOrderseq.Split("^".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string s1 in arr1)
                {
                    bool hasStr = false;
                    foreach (string s2 in arr2)
                    {
                        if (s1 == s2)
                        {
                            hasStr = true;
                        }
                    }
                    if (!hasStr)
                    {
                        sb.AppendFormat("'{0}',", s1);
                    }
                }
            }
            kyOrderseq = sb.ToString().TrimEnd(',');
            return kyOrderseq;
        }


    }
}