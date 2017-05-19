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
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database.Alipay
{
    public class AilpayPaymentDB : BaseDB
    {
        private BuilderSql _builder;

        public AilpayPaymentDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 创建门诊预存订单
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="patientname">病人姓名</param>
        /// <param name="patientidcardno">病人身份证号</param>
        /// <param name="cardno">就诊卡卡号</param>
        /// <param name="subject">标题</param>
        /// <param name="money">金额</param>
        /// <param name="tradeno">订单号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_CreateOrder(string shdzid, string jzid, string qjfs, string openid, string patientname, string patientidcardno, string cardno, string patientid, string subject, double money, double tkje, string patienttype, out long tradeno, out string msg)
        {
            msg = "";
            tradeno = -1;
            int ret = 99;

            if (patienttype == "1")
            {
                if (!IsValidCardno(patientid, cardno, out msg))
                {
                    return ret;
                }
            }
            if (!IsSupportPayment(patienttype))
            {
                msg = "医院暂时不支持该功能的使用";
                ret = 18;
                return ret;
            }
            if (!IsAmountAbnormal(patienttype, money))
            {
                msg = "充值金额异常或超过限定额度";
                return 19;
            }

            //产生日期
            DateTime _cssj;
            if (DB_Sysdate(out _cssj) == false)
            {
                msg = "读取服务器系统时间出错";
                return 20;
            }
            //产生医院订单流水号
            tradeno = YylshSeq();
            if (tradeno <= 0)
            {
                msg = "订单流水号产生失败";
                return 21;
            }
            string logMsg = " yylsh : " + tradeno + " czje : " + money + " tkje : " + tkje + " ddzt : " + 0 + " jyfs : " + 10 +
            " bkhm : " + cardno + " brid : " + patientid + " cssj : " + _cssj.ToString("yyyy-MM-dd HH:mm:ss") +
            " czsj : " + _cssj.ToString("yyyy-MM-dd HH:mm:ss") + " brlx : " + patienttype + " 。";
            UtilLog.GetInstance().WriteOrderLog("创建订单成功: ", logMsg);
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            try
            {
                string insertSql = _builder.CreateOrderforAlipaySql(tradeno, money, tkje, patienttype, cardno, patientid, _cssj, patientidcardno, "", patientname, subject, openid, shdzid, jzid, qjfs);
                ArrayList listSql = new ArrayList();
                listSql.Add(insertSql);
                DbHelperOra.ExecuteSqlTran(listSql, connection);
                msg = "订单已经成功创建";
                ret = 00;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                logMsg += " ， 异常信息： " + msg + " 。";
                UtilLog.GetInstance().WriteOrderLog("创建订单失败: ", logMsg);
                return -1;
            }
            finally
            {
                connection.Close();
            }

            return ret;
        }

        //读取系统时间
        public Boolean DB_Sysdate(out DateTime sysdate)
        {
            sysdate = Convert.ToDateTime(AppUtils.DateTime_Format);
            Boolean _value = false;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                string findId = _builder.SysdateSql();
                dr = DbHelperOra.ExecuteReader(findId, connection);
                if (dr.Read())
                {
                    sysdate = !dr.IsDBNull(0) ? dr.GetDateTime(0) : Convert.ToDateTime(AppUtils.DateTime_Format);
                    _value = true;
                }
                else
                {
                    _value = false;
                }
                dr.Close();
                return _value;

            }
            catch (Exception)
            {
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
        }

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="values">列表</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_PredepositList(string openid, string brlx, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.OrderListForAlipaySql(openid, brlx, out  flag, out  msg);
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
                        OrderInfoForAlipay pd = new OrderInfoForAlipay();

                        pd.openid = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                        pd.paymenttradeno = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                        pd.tradeno = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt64(2)) : " ";
                        pd.status = !dr.IsDBNull(3) ? Convert.ToString(dr.GetInt64(3)) : " ";
                        pd.subject = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.money = !dr.IsDBNull(5) ? Convert.ToString(dr.GetDouble(5)) : " ";
                        pd.ctime = !dr.IsDBNull(6) ? dr.GetDateTime(6).ToString(AppUtils.DateTime_Format_All) : " ";
                        pd.paytime = !dr.IsDBNull(7) ? dr.GetDateTime(7).ToString(AppUtils.DateTime_Format_All) : " ";
                        pd.rtntime = !dr.IsDBNull(8) ? dr.GetDateTime(8).ToString(AppUtils.DateTime_Format_All) : " ";
                        if (brlx == "1")
                        {
                            pd.patientid = !dr.IsDBNull(9) ? Convert.ToString(dr.GetInt64(9)) : " ";
                        }
                        else
                        {
                            pd.inpatientno = !dr.IsDBNull(9) ? Convert.ToString(dr.GetInt64(9)) : " ";
                        }

                        pd.patientname = !dr.IsDBNull(10) ? dr.GetString(10) : " ";
                        pd.patientidcardno = !dr.IsDBNull(11) ? dr.GetString(11) : " ";

                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 0;
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
        /// 取消订单
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_CancelPredepositList(string openid, string patientname, string patientid, long yylsh, out string msg)
        {
            msg = "";
            long _sjczzt = -99;
            string _ztmc = "";
            double _czje = 0;

            int phoneOrderStatus = DB_AlipayStatusOrder(yylsh, out _sjczzt, out _ztmc, out _czje, out msg);
            if (phoneOrderStatus == 1)
            {
                msg = "手机订单已完成，无法被修改";
                return 22;
            }
            else if (phoneOrderStatus == -1)
            {
                msg = "手机订单已作废，无法被修改";
                return 23;
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                int ret = -1;

                //作废订单
                string updateSql = _builder.CancelOrderSql(yylsh);

                ArrayList listSql = new ArrayList();
                listSql.Add(updateSql);

                string logMsg = " yylsh = '" + yylsh + "' 。";
                UtilLog.GetInstance().WriteOrderLog("取消订单成功: ", logMsg);

                DbHelperOra.ExecuteSqlTran(listSql, connection);
                msg = "订单作废成功";
                ret = 00;

                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
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

        /// <summary>
        /// 商户通知
        /// </summary>  
        /// <param name="info">通知信息</param>
        /// <param name="brlx">病人类型</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_InsertAlipay(AlipayReplyInfo info, string brlx, out string msg)
        {
            UtilLog.GetInstance().WriteOrderLog("Order: ", info.AlipayInfoToString());
            msg = "";
            DateTime jysj;
            if (DB_Sysdate(out jysj) == false)
            {
                msg = "读取系统时间出错";
                return 24;
            }
            int ret = -1;
            try
            {
                //记录支付宝返回信息到数据库
                ret = LogAlipayRetInfo(info, jysj, out msg);
                if (ret != 0)
                {
                    return ret;
                }
                if (!new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().GetPayType2(info.tradeno.ToString()).Contains("云医院") && new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().GetDingDanStatus2(info.tradeno.ToString()) == "0")
                {
                    if (string.IsNullOrEmpty(info.paymenttradeno) || string.IsNullOrEmpty(info.paymentparameters))
                    {
                        msg = "银联返回充值不成功";
                        return 25;
                    }
                    //判断订单是否存在
                    double czje = 0;
                    string brid = null, brlx2 = null;
                    ret = ExistAlipayOrder(info, out brid, out brlx2, out czje, out msg);
                    if (ret != 0)
                    {
                        return ret;
                    }
                    if (brlx != brlx2)
                    {
                        msg = "订单病人类型异常";
                        return 26;
                    }

                    //充值金额是否正常
                    ret = AlipayValidAmount(info, czje, out  msg);
                    if (ret != 0)
                    {
                        return ret;
                    }

                    //更新HIS
                    ret = DB_InsertAlipayForHis(info, brid, brlx, czje, jysj, out msg);
                    if (ret == 36)
                    {
                        new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().EditOrderStatus(info.tradeno.ToString(), "3", out msg);
                        new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().InsertTuiFeiDDInfo(info.tradeno.ToString());
                        msg = "HIS更新数据不成功。";

                    }
                    if (ret == 0 || ret == 00)
                    {
                        new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().EditOrderStatus(info.tradeno.ToString(), "2", out msg);
                        msg = "充值成功。";
                    }

                    return ret;
                }

            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                return -1;
            }
            return ret;
        }

        /// <summary>
        /// 订单状态查询
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="ddzt">订单状态</param>
        /// <param name="ztmc">状态名称</param>
        /// <param name="czje">充值金额</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_AlipayStatusOrder(long yylsh, out long ddzt, out string ztmc, out double czje, out string msg)
        {
            msg = "";
            ddzt = -1;
            ztmc = "";
            czje = 0;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                int ret = -99;

                string findId = _builder.QueryOrderStatusSql(yylsh);

                dr = DbHelperOra.ExecuteReader(findId, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ddzt = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                        czje = !dr.IsDBNull(1) ? dr.GetDouble(1) : 0;
                        switch (ddzt)
                        {
                            case -1:
                                ztmc = "订单已经作废";
                                ret = 27;
                                break;
                            case 0:
                                ztmc = "订单已经生成";
                                ret = 28;
                                break;
                            case 1:
                                ztmc = "订单已经完成";
                                ret = 29;
                                break;
                            case 2:
                                ztmc = "订单已经充值";
                                ret = 30;
                                break;
                            default:
                                ztmc = "未知的订单状态";
                                ret = 31;
                                break;
                        }
                    }
                    else
                    {
                        msg = "没有找到订单，请核实";
                        ret = 32;
                    }
                }
                else
                {
                    msg = "没有找到订单，请核实";
                    ret = 32;
                }
                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
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


        /// <summary>
        /// 住院病人信息
        /// </summary>  
        /// <param name="idcardno">身份证号</param>
        /// <param name="name">病人姓名</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_zyPatientInfo(string idcardno, string name, out zyPatientInfo zyPatientInfo, out string msg)
        {
            int ret = -1;
            msg = "";
            zyPatientInfo = new zyPatientInfo();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string querySql = _builder.GetZhuYuanPatientInfo(idcardno, name, out  flag, out  msg);
                if (!flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, querySql);

                dr = DbHelperOra.ExecuteReader(querySql, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        zyPatientInfo.inpatientno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        zyPatientInfo.patientname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        zyPatientInfo.patientidcardno = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        zyPatientInfo.sex = !dr.IsDBNull(3) ? Convert.ToString(dr.GetInt32(3)) : "";
                        zyPatientInfo.birthday = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        zyPatientInfo.address = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        zyPatientInfo.phone = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        zyPatientInfo.admitdate = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        zyPatientInfo.dischargedate = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        zyPatientInfo.stayday = !dr.IsDBNull(9) ? dr.GetInt32(9) : 0;
                        zyPatientInfo.xzmc = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        zyPatientInfo.endemicarea = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        zyPatientInfo.brch = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        zyPatientInfo.departcode = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        zyPatientInfo.departname = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        zyPatientInfo.ylhj = !dr.IsDBNull(15) ? Convert.ToString(dr.GetDouble(15)) : "";
                        zyPatientInfo.lwhj = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        zyPatientInfo.zfje = !dr.IsDBNull(17) ? Convert.ToString(dr.GetDouble(17)) : "";
                        zyPatientInfo.jkje = !dr.IsDBNull(18) ? Convert.ToString(dr.GetDouble(18)) : "";
                        zyPatientInfo.zyzt = !dr.IsDBNull(19) ? dr.GetString(19) : "";
                        zyPatientInfo.doctorcode = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        zyPatientInfo.doctorname = !dr.IsDBNull(21) ? dr.GetString(21) : "";
                        ret = 0;
                    }
                    else
                    {
                        msg = "亲，未找到您的信息！";
                        ret = 33;
                    }
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
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

        /// <summary>
        /// 获得住院费用列表
        /// </summary>
        /// <param name="needcount">需要条数</param>
        /// <param name="maxnum">最大值</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_zyCostInfo(string inpatientno, string costdate, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string querySql = _builder.GetZhuYuanCostInfo(inpatientno, costdate, out  flag, out  msg);
                if (!flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, querySql);

                dr = DbHelperOra.ExecuteReader(querySql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        zyCostInfo cost = new zyCostInfo();
                        cost.costcode = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        cost.costname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        cost.totalfee = !dr.IsDBNull(2) ? Convert.ToString(dr.GetDouble(2)) : "";
                        cost.payfee = !dr.IsDBNull(3) ? Convert.ToString(dr.GetDouble(3)) : "";
                        values.Add(cost);
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

        /// <summary>
        /// 获得最大医院流水号
        /// </summary>
        /// <param name="needcount">需要条数</param>
        /// <param name="maxnum">最大值</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public Boolean DB_Get_Yylshmax(int needcount, out long maxnum)
        {
            maxnum = -1;
            Boolean _value = false;
            if (needcount < 1)
            {
                return _value;
            }
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string selectSql;
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
                selectSql = _builder.YylshSql();
                cmd.CommandText = selectSql;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    maxnum = !dr.IsDBNull(0) ? dr.GetInt64(0) : -1;
                    _value = true;
                }
                else
                {
                    maxnum = -1;
                    tx.Rollback();
                    _value = false;
                }
                dr.Close();
                tx.Commit();
                return _value;
            }
            catch (Exception)
            {
                tx.Rollback();
                return false;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != connection && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 读取系统表里最大值
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="needcount">条数</param>
        /// <param name="maxnum">最大值</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public Boolean DB_Get_Max(string tablename, int needcount, out long maxnum)
        {
            maxnum = -1;
            Boolean _value = false;
            if (needcount < 1)
            {
                return _value;
            }
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            string selectSql;
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
                selectSql = _builder.YylshSql();
                cmd.CommandText = selectSql;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    maxnum = !dr.IsDBNull(0) ? dr.GetInt64(0) : -1;
                    _value = true;
                }
                else
                {
                    maxnum = -1;
                    tx.Rollback();
                    _value = false;
                }
                dr.Close();
                tx.Commit();
                return _value;
            }
            catch (Exception)
            {
                tx.Rollback();
                return false;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != connection && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// 是否支持充值功能
        /// </summary>
        /// <returns></returns>
        private bool IsSupportPayment(string brlx)
        {
            bool result = false;

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY)
            {
                result = true;
            }
            else if (brlx.Equals("1") &&
                  (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY))
            {
                result = true;
            }
            else if (brlx.Equals("2") &&
            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 充值金额是否异常
        /// </summary>
        /// <returns></returns>
        private bool IsAmountAbnormal(string brlx, double je)
        {
            bool result = true;

            if (brlx.Equals("1") && je > WebConfigParameter.MzbrMaxAmount)
            {
                result = false;
            }
            else if (brlx.Equals("2") && je > WebConfigParameter.ZybrMaxAmount)
            {
                result = false;
            }

            return result;
        }


        /// <summary>
        /// 获得医院流水号
        /// </summary>
        /// <returns></returns>
        private long YylshSeq()
        {
            long yylshSeq = -1;
            if (DB_Get_Yylshmax(1, out yylshSeq) == false)
            {
                yylshSeq = -1;
            }

            return yylshSeq;
        }

        /// <summary>
        /// 更新HIS充值记录(门诊充值)
        /// </summary>
        /// <param name="tradeno">医院订单号</param>
        /// <param name="connection"></param>
        /// <param name="tx"></param>
        /// <param name="yhlsh">银行流水号</param>
        /// <param name="je">金额</param>
        /// <param name="brid">病人id</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <param name="jysj">交易日期</param>
        /// <param name="msg">成功、错处或异常信息</param>
        /// <returns>0-成功   大于0-失败   小于0-异常</returns>
        private int DB_AlipayUpdateHisCharge(string tradeno, OracleConnection connection, OracleTransaction tx, string yhlsh, double je, string brid, string brlx, DateTime jysj, out string msg)
        {
            int ret = -1;
            msg = "";
            OracleDataReader dataReader = null;
            int result = -99;

            if (brlx.Equals("1"))
            {
                UserJFInfo jfinfo;
                ret = DB_GetMZCZInfo(brid, out jfinfo, out msg);
                if (ret == 0)
                {
                    if (!new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().GetPayType2(tradeno).Contains("云医院"))//如果是云医院就不用开通
                    {
                        //账户状态不为1未开通缴费，进行开通
                        if (jfinfo.zhanghuzt != "1")
                        {
                            IDataParameter[] parameters1 ={
                                          new OracleParameter("prm_msg", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_outbuffer", OracleType.VarChar, 400) 
                                       };

                            //就诊卡号|就诊卡类型|操作日期|操作员工号 00102009070801114983|L|2016-3-21 10:02:44|ZFB
                            parameters1[0].Value = jfinfo.cardno + "|" + jfinfo.cardtype + "|" + jysj.ToString(AppUtils.DateTime_Format_All) + "|ZFB";
                            parameters1[0].Direction = ParameterDirection.Input;
                            parameters1[1].Direction = ParameterDirection.Output;
                            parameters1[2].Direction = ParameterDirection.Output;
                            //pkg_gy_yinyijk.PRC_ZZSF_DIANZIZHCZ
                            dataReader = DbHelperOra.ExeProcedureUseTran(connection, tx, WebConfigParameter.KTChongzhiProcedureName, parameters1, out result);

                            if (result != 0)
                            {
                                msg = "开通账户出错。";
                                ret = 34;
                                return ret;
                            }
                            tx.Commit();
                        }
                    }
                    string s_jiaoyirc = "<?xml version='1.0' encoding='utf-8'?><CHONGZHI_IN><JIUZHENKLX>1</JIUZHENKLX><JIUZHENKH>" +
                            jfinfo.cardno + "</JIUZHENKH><XINGMING>" + jfinfo.brxm + "</XINGMING><ZHENGJIANHM>" + jfinfo.sfzh + "</ZHENGJIANHM>" +
                            "<LIANXIDH>" + jfinfo.lxdh + "</LIANXIDH><CHONGZHIJE>" + je + "</CHONGZHIJE><ZHIFUFS>20</ZHIFUFS><ORDERNO>" + tradeno + "</ORDERNO><BASEINFO>" +
                            "<CAOZUOYDM>ZFB</CAOZUOYDM><CAOZUOYXM>ZFB</CAOZUOYXM><CAOZUORQ>" + jysj.ToString(AppUtils.DateTime_Format_All) +
                            "</CAOZUORQ></BASEINFO></CHONGZHI_IN>";
                    string postData = "<x:Envelope xmlns:x=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\"><x:Header/><x:Body><tem:RunService>" +
                                    "<tem:TradeType>HIS4.CHONGZHI</tem:TradeType>" +
                                    "<tem:TradeMsg><![CDATA[" + s_jiaoyirc +
                                    "]]></tem:TradeMsg><tem:TradeOut>string.Empty</tem:TradeOut></tem:RunService></x:Body></x:Envelope>";

                    string postResult = HttpPost(WebConfigParameter.WebserviceUrl, postData);

                    XmlDocument doc = XmlHelper.CreateXmlDocument(postResult);
                    string tagname = "TradeOut";
                    XmlNodeList tradeOuts = doc.DocumentElement.GetElementsByTagName(tagname);
                    string tradeOutContent = "";
                    if (tradeOuts.Count > 0)
                    {
                        tradeOutContent = tradeOuts[0].InnerText;
                    }
                    XmlDocument tradeOutDoc = XmlHelper.CreateXmlDocument(tradeOutContent);
                    tagname = "ERRNO";
                    string tagname1 = "ERRMSG";
                    string tradeOutContent1 = "";
                    tradeOuts = tradeOutDoc.DocumentElement.GetElementsByTagName(tagname);
                    XmlNodeList tradeOuts1 = tradeOutDoc.DocumentElement.GetElementsByTagName(tagname1);
                    if (tradeOuts.Count > 0)
                    {
                        tradeOutContent = tradeOuts[0].InnerText;
                    }

                    if (tradeOuts1.Count > 0)
                    {
                        tradeOutContent1 = tradeOuts1[0].InnerText;
                    }

                    if (tradeOutContent == "0")
                    {
                        msg = tradeOutContent1;
                        ret = 0;
                    }
                    else if (tradeOutContent == "-1")
                    {
                        msg = tradeOutContent1;
                        ret = -1;
                    }


                    //IDataParameter[] parameters ={
                    //                      new OracleParameter("prm_msg", OracleType.VarChar),
                    //                      new OracleParameter("prm_appcode", OracleType.Number), 
                    //                      new OracleParameter("prm_outbuffer", OracleType.VarChar, 400) 
                    //                   };

                    ////就诊卡号|就诊卡类型|病人姓名|证件号码|联系电话|充值金额|支付方式|交款日期|操作员工号|校验码|银行卡扣款信息  
                    ////00102009070801114983|L|孙遥|9879|8678|100.00|20|2016-3-21 15:16:40|ZFB|ZXCV|没有信息
                    //parameters[0].Value = cardno + "|L|" + name + "|9879|8678|" + Convert.ToString(je) + "|20|" + jysj.ToString(AppUtils.DateTime_Format_All) + "|ZFB|ZXCV||";
                    //parameters[0].Direction = ParameterDirection.Input;
                    //parameters[1].Direction = ParameterDirection.Output;
                    //parameters[2].Direction = ParameterDirection.Output;

                    ////pkg_gy_yinyijk.PRC_ZZSF_DIANZIZHCZ

                    //dataReader = DbHelperOra.ExeProcedureUseTran(connection, tx,
                    //    "his3.pkg_gy_yinyijk.PRC_ZZSF_DIANZIZHCZ",
                    //    parameters, out result);

                    //if (result != 0)
                    //{
                    //    msg = "插入医院HIS表数据出错。";
                    //    ret = 35;
                    //}
                    //else
                    //{
                    //    msg = "插入医院HIS成功。";
                    //    ret = 00;
                    //}
                }
            }
            else if (brlx.Equals("2"))
            {
                //TODO  更新数据表里面的住院病人的充值记录
                IDataParameter[] parameters ={
                                          new OracleParameter("prm_msg", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_databuffer", OracleType.VarChar, 400) 
                                       };

                //病人id|工号|费用|充值方式统一为10|交款日期||||0||
                parameters[0].Value = brid + "|ZFB|" + Convert.ToString(je) + "|20|" + jysj.ToString(AppUtils.DateTime_Format_All) + "||||0||" + tradeno + "|";
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;

                //PKG_GY_YINYIJK.PRC_ZY_YUJIAOKUAN
                dataReader = DbHelperOra.ExeProcedureUseTran(connection, tx, WebConfigParameter.ProcedureName, parameters, out result);
                if (result != 0)
                {
                    msg = "插入医院HIS表数据出错。";
                    ret = 35;
                }
                else
                {
                    msg = "插入医院HIS成功。";
                    ret = 00;
                }
            }
            return ret;
        }

        /// <summary>
        /// 更新HIS充值记录
        /// </summary>
        /// <param name="info"></param>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="czje"></param>
        /// <param name="jysj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int DB_InsertAlipayForHis(AlipayReplyInfo info, string brid, string brlx, double czje, DateTime jysj, out string msg)
        {
            int ret = -1;
            msg = "";

            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                //更新HIS表记录
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY)
                {
                    int otherResult = -99;

                    if (!string.IsNullOrEmpty(info.paymenttradeno) && !string.IsNullOrEmpty(info.paymentparameters))
                    {
                        otherResult = DB_AlipayUpdateHisCharge(info.tradeno.ToString(), conn, tx, info.paymenttradeno, czje, brid, brlx, jysj, out msg);
                        if (otherResult != 0)
                        {
                            tx.Rollback();
                            msg = "HIS更新数据不成功。";
                            return 36;
                        }
                        msg = "充值成功。";
                        ret = 00;

                    }
                    else
                    {
                        msg = "银联返回失败";
                        ret = 37;
                    }
                }

                tx.Commit();
                ret = 0;

                return ret;
            }
            catch (Exception ex)
            {
                tx.Rollback();

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != conn && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 记录支付宝返回信息到数据库
        /// </summary>
        /// <param name="info"></param>
        /// <param name="jysj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int LogAlipayRetInfo(AlipayReplyInfo info, DateTime jysj, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            msg = "";

            long count = 0;
            try
            {
                string sql = _builder.AlipayShtzCountSql(info.tradeno);
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.Read())
                {
                    count = !dr.IsDBNull(0) ? dr.GetInt64(0) : 0;
                }
                if (count > 0)
                {
                    msg = "已经交易记录,无需插入";
                    ret = 38;
                    return ret;
                }
                //记录支付宝返回记录
                ret = WriteAlipayRetInfoToDB(info, jysj, out msg);
                if (ret != 0)
                {
                    msg += "记录日志失败";
                    ret = 39;
                }
                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                return -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }

                if (null != connection && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        /// <summary>
        /// 支付宝返回信息写入到数据库
        /// </summary>
        /// <param name="info"></param>
        /// <param name="jysj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int WriteAlipayRetInfoToDB(AlipayReplyInfo info, DateTime jysj, out string msg)
        {
            int ret = -1;

            OracleConnection conn1 = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn1.State != ConnectionState.Open)
            {
                conn1.Open();
            }
            OracleTransaction tx1 = conn1.BeginTransaction();

            try
            {
                string ylcode = "", ylmc = "支付宝服务窗";

                string insertSql = _builder.InsertAlipaySHTZSql(info, jysj);

                // todo:
               // string updateStatus = _builder.UpdateAlipayDDStatusSql(info, ylcode, ylmc);

                ArrayList listSql = new ArrayList();
                int exeSql;
                listSql.Add(insertSql);
                //to do
              //  listSql.Add(updateStatus);

                exeSql = -1;

                DbHelperOra.ExecuteSqlTran(conn1, tx1, listSql, out exeSql, out msg);

                if (exeSql != 0)
                {
                    tx1.Rollback();
                    return exeSql;
                }

                tx1.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != conn1 && ConnectionState.Open == conn1.State)
                {
                    conn1.Close();
                }
            }
        }


        /// <summary>
        /// 订单表中是否存在订单
        /// </summary>
        /// <param name="info"></param>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="czje"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int ExistAlipayOrder(AlipayReplyInfo info, out string brid, out string brlx, out double czje, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            int ret = -1;

            msg = "";
            brid = "";
            brlx = "";
            czje = 0;

            try
            {
                string findId = _builder.QueryOrderInfoSql(info.tradeno);
                dr = DbHelperOra.ExecuteReader(findId, connection);
                if (dr.Read())
                {
                    brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                    czje = !dr.IsDBNull(1) ? dr.GetDouble(1) : 0;
                    brlx = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt32(2)) : "";
                    ret = 0;
                }
                else
                {
                    msg = "没有该订单，数据非法";
                    ret = 40;
                }

                return ret;
            }
            catch (Exception ex)
            {
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
                if (null != connection && ConnectionState.Open == connection.State)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 充值金额是否正常
        /// </summary>
        /// <param name="info"></param>
        /// <param name="czje"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int AlipayValidAmount(AlipayReplyInfo info, double czje, out string msg)
        {
            int ret = -1;
            msg = "";

            if (czje <= 0 || czje > 10000 || (Convert.ToDouble(info.money) != czje))
            {
                msg = "充值金额异常";
                ret = 41;
            }
            else
            {
                ret = 0;
            }

            return ret;
        }


        /// <summary>
        ///  获取门诊充值信息
        /// </summary>
        /// <param name="patientid">病人ID</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        private int DB_GetMZCZInfo(string patientid, out UserJFInfo info, out string msg)
        {
            info = new UserJFInfo();
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {
                string findText = "select patientname, cardno, zhanghuzt, cardtype, idcardno, phone from ZFB_MENZHENKLB t where patientid = '" + patientid + "'";
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                dr = DbHelperOra.ExecuteReader(findText, connection);
                if (dr.Read())
                {
                    info.brxm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    info.cardno = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    info.zhanghuzt = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt32(2)) : "";
                    info.cardtype = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    info.sfzh = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    info.lxdh = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    ret = 0;
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    ret = 10;
                }
                return ret;
            }
            catch (Exception ex)
            {
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
                if (null != connection && ConnectionState.Open == connection.State)
                {
                    connection.Close();
                }
            }
        }

        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.Headers.Add("SOAPAction", "http://tempuri.org/ITradeServerGGJK/RunService");
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Flush();
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }


        /// <summary>
        /// 绑卡号码是否有效
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="cardno">绑卡号码</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        private bool IsValidCardno(string brid, string cardno, out string msg)
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
    }
}