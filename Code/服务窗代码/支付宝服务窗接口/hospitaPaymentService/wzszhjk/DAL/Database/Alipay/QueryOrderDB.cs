using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
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
    public class QueryOrderDB : BaseDB
    {
        private BuilderSql _builder;

        public QueryOrderDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 预约科室信息
        /// </summary>
        /// <param name="departcode">一级科室代码</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_AppointmentInfor(string departcode, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.AppointmentInforSql(departcode, out  flag, out  msg);
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
                        AlipayAppointmentInfo pd = new AlipayAppointmentInfo();

                        pd.departcode = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.departname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.secondcode = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.secondname = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.describe = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 1;
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
        /// 科室排班信息
        /// </summary>
        /// <param name="departcode">二级科室代码</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DepartmentSchedul(string departcode, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.DepartmentSchedulSql(departcode, out  flag, out  msg);
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
                        AlipayDepartmentSchedul pd = new AlipayDepartmentSchedul();

                        pd.scheduledate = !dr.IsDBNull(0) ? dr.GetDateTime(0).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        pd.remain = !dr.IsDBNull(1) ? Convert.ToString(dr.GetInt64(1)) : "";
                        pd.total = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt64(2)) : "";
                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 1;
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
        /// 预约医生信息
        /// </summary>
        /// <param name="departcode">二级科室代码</param>
        /// <param name="scheduledate">排班日期</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_ReservationDoctor1(string departcode, string scheduledate, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.ReservationDoctor1Sql(departcode, scheduledate, out  flag, out  msg);
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
                        AlipayReservationDoctor1 pd = new AlipayReservationDoctor1();

                        pd.doctorcode = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.doctorname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.pictureurl = !dr.IsDBNull(2) ? dr.GetString(1) : "";
                        pd.level = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.recommend = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.adept = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.reserve = !dr.IsDBNull(6) ? Convert.ToString(dr.GetInt64(6)) : "";
                        pd.scheduledates = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 1;
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
        /// 医生排班信息
        /// </summary>
        /// <param name="doctorcode">医生代码</param>
        /// <param name="departcode">二级科室代码</param>
        /// <param name="scheduledate">排班日期</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DoctorSchedul(string doctorcode, string departcode, string scheduledate, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.DoctorSchedulSql(doctorcode, departcode, scheduledate, out  flag, out  msg);
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
                        AlipayReservationDoctor1 pd = new AlipayReservationDoctor1();

                        pd.scheduleseq = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.departcode = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.departname = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.doctorcode = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.doctorname = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.special = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.total = !dr.IsDBNull(7) ? Convert.ToString(dr.GetInt64(7)) : "";
                        pd.address = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.scheduledate = !dr.IsDBNull(9) ? dr.GetDateTime(9).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        pd.shiftcode = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        pd.shiftname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        pd.fee = !dr.IsDBNull(12) ? Convert.ToString(dr.GetInt64(12)) : "";

                        pd.remain = new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().GetNeededOrderCount(pd.scheduleseq, pd.shiftcode, pd.scheduledate).ToString();

                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 1;
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
        /// 门诊号源信息
        /// </summary>
        /// <param name="doctorcode">医生代码</param>
        /// <param name="scheduleseq">排班流水号</param>
        /// <param name="shiftcode">1=上午、2=下午</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_mzReservationInfor(string doctorcode, string scheduleseq, string shiftcode, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = new ArrayList();
            ArrayList list = new ArrayList();
            msg = "";
            string totalOrderno = "";
            string yyOrderno = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;
                DateTime bdsj, hysj;
                string timefomate = "";
                if (DB_GetSysdate(out bdsj) == false)
                {
                    msg = "读取系统时间出错";
                    return 24;
                }

                string findsql = _builder.mzReservationInforSql(doctorcode, scheduleseq, shiftcode, out  flag, out  msg);
                if (!flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findsql);

                dr = DbHelperOra.ExecuteReader(findsql, connection);
                if (null != dr && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AlipayQueryOrderSeq info = new AlipayQueryOrderSeq();
                        totalOrderno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        yyOrderno = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        info.orderseq = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        info.doctorcode = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        info.shiftcode = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        info.week = !dr.IsDBNull(5) ? Convert.ToString(dr.GetInt32(5)) : "";
                        info.departcode = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        info.scheduledate = !dr.IsDBNull(7) ? dr.GetDateTime(7).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        info.orderno = GetOrderseqString(totalOrderno, yyOrderno);
                        list.Add(info);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 1;
                    dr.Close();
                    return ret;
                }

                foreach (AlipayQueryOrderSeq findinfo in list)
                {
                    findsql = _builder.mzReservationTimeInforSql(findinfo, out  flag, out  msg);
                    if (!flag)
                    {
                        return 10;
                    }

                    UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findsql);

                    dr = DbHelperOra.ExecuteReader(findsql, connection);
                    if (null != dr && dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            AlipayQueryOrderSeq ri = new AlipayQueryOrderSeq();
                            ri.ordertime = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                            ri.orderno = !dr.IsDBNull(1) ? Convert.ToString(dr.GetInt32(1)) : "";
                            ri.orderendtime = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            ri.shiftcode = !dr.IsDBNull(3) ? Convert.ToString(dr.GetInt32(3)) : "";
                            ri.orderseq = findinfo.orderseq;
                            hysj = Convert.ToDateTime(ri.ordertime).AddMinutes(-30);
                            timefomate = findinfo.scheduledate + " " + hysj.ToString(AppUtils.DateTime_Format_Time);
                            hysj = Convert.ToDateTime(timefomate);
                            if (hysj > bdsj)
                            {
                                values.Add(ri);
                            }
                        }
                        ret = 0;
                    }
                    if (values.Count <= 0)
                    {
                        msg = "亲，没有记录";
                        values = null;
                        ret = 1;
                    }
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
        /// 门诊预约
        /// </summary>
        /// <param name="info">预约结构体信息</param>
        /// /// <param name="msg">成功、错误或异常信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int mzOrder(AlipaymzOrderNeedInfo info, out AlipaymzReservation pd, out string msg)
        {
            msg = "";
            int ret = 99;
            pd = new AlipaymzReservation();
            OracleDataReader dataReader = null;
            int result = -99;

            //msg = info.openid + '|' + info.doctorcode + '|' + info.scheduleseq + '|' + info.orderseq + '|' + info.ordertime + '|' + info.orderendtime + '|' + info.shiftcode + '|' +
            //    info.patientid + '|' + info.patientname + '|' + info.patientsex + '|' + info.patientidcardno + '|' + '|' + info.patientphone +
            //    '|' + info.patientaddress + '|' + info.cardname + '|' + info.cardno + '|' + info.birthday + '|';
            //UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, msg);

            if (info.scourcetype == "9")
            {
                //根据排班流水号到ZFB_YUYUE_YSPB查询排班日期
                string sqlQueryDate = string.Format("select to_char(Scheduledate,'yyyy-MM-dd') from  ZFB_YUYUE_YSPB where Scheduleseq='{0}' and rowNum=1", info.scheduleseq);
                string date1 = "";
                object obj = DbHelperOra.GetSingle(sqlQueryDate, new OracleConnection(WebConfigParameter.ConnectionHisString));
                if (obj != null)
                {
                    date1 = obj.ToString();
                }
                //int pbCount = 0;
                //string querySql = string.Format("Select count(*) From yyy_yueyue a Where a.OPENID='{0}' And to_char(a.PREENGAGEDATE,'yyyy-MM-dd')='{1}' And a.SHIFTCODE='{2}' And a.sourcetype='{3}' and a.status!=3 and a.status!=4", info.openid, date1, info.shiftcode, 9);
                //object obj2 = DbHelperOra.GetSingle(querySql, new OracleConnection(WebConfigParameter.ConnectionHisString));
                //if (obj2 != null)
                //{
                //    pbCount = Convert.ToInt32(obj2);
                //}
                //if (pbCount > 0)
                //{
                if (new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().IsYuYue(info.openid, date1, info.shiftcode))
                {
                    msg = "预约失败！原因：一个支付宝账户在同一个上午或下午只支持一次云医院预约！";
                    return -1;
                }
            }


            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                string scheduledate;
                ret = DB_GetScheduledate(info.scheduleseq, out scheduledate, out msg);
                if (ret != 0)
                {
                    msg = "读取预约时间出错";
                    return ret;
                }

                AlipaymzOrderNeedInfo outInfo = new AlipaymzOrderNeedInfo();
                if (!string.IsNullOrEmpty(info.patientid))
                {
                    ret = GetOrderInfo(info.patientname, info.patientidcardno, info.patientid, out outInfo, out msg);

                    if (ret != 0)
                    {
                        return ret;
                    }
                }
                string ybcardno = "";
                if (outInfo.cardname != "医院就诊卡")
                {
                    ybcardno = outInfo.cardno;
                }
                string shifcode = "";
                if (info.shiftcode == "1")
                {
                    shifcode = "S";
                }
                else if (info.shiftcode == "2")
                {
                    shifcode = "X";
                }

                IDataParameter[] parameters ={
                                          new OracleParameter("prm_jiaoyizfc", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_databuffer", OracleType.VarChar, 400) 
                                       };

                /*外部用户ID 对应HIS的UserID|就诊卡号|医保卡号|姓名|性别|身份证号|出生日期|血型|婚姻|职业|国籍|民族|既往史|
                 * 过敏史|家庭地址|联系电话|联系人|手机|排班ID|排班模式|预约日期|预约类型|记录来源|挂号序号|申请人|申请人姓名|
                 * 操作员|预约号|家庭电话|单位电话|候诊开始时间|候诊结束时间|预约来源|
                 * 
                 * 35407|00102009070801114911||袁建飞|男|33032419780301332X|1978-03-01||||||||永嘉县沙头镇东章村|
                 * 13757750397|袁建飞||S1000050387|2|2016-03-25|2|8|3|||ZFB||||8:05|8:35||*/
                parameters[0].Value = info.openid + "|" + outInfo.cardno + "|" + ybcardno + "|" + info.patientname + "|" +
                    outInfo.patientsex + "|" + info.patientidcardno + "|" + outInfo.birthday + "||||||||" + outInfo.patientaddress +
                    "|" + outInfo.patientphone + "|" + info.patientname + "|" + info.patientphone + "|" + shifcode + info.scheduleseq +
                    "|2|" + scheduledate + "|2|" + (info.scourcetype == "" ? "8" : info.scourcetype) + "|" + info.orderseq + "|||ZFB||||" + info.ordertime +
                    "|" + info.orderendtime + "||";
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;

                //pkg_gy_yinyijk.PRC_ZZSF_DIANZIZHCZ
                dataReader = DbHelperOra.ExeProcedureUseTran(conn, tx,
                    WebConfigParameter.YuYueProcedureName,
                    parameters, out result);

                string retInfo = Convert.ToString(parameters[2].Value);
                string[] arr1 = retInfo.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result != 0 || (arr1[0] != "OK"))
                {
                    tx.Rollback();
                    msg = arr1[1];
                    ret = 1;
                    return ret;
                }
                tx.Commit();

                string orderNo = arr1[1];
                bool _flag = false;
                string findsql = _builder.GetmzOrderRetInfoSql(orderNo, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findsql);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                OracleDataReader dr = null;
                dr = DbHelperOra.ExecuteReader(findsql, conn);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        pd.preengageseq = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.preengagedate = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        pd.preengagetime = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.departcode = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.departname = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.doctorcode = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.doctorname = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.patientname = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.preengageno = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.place = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        ret = 0;
                        if (pd.preengagedate == "")
                        {
                            msg = "预约日期错误";
                            ret = 11;
                        }
                        else if (pd.preengagetime == "")
                        {
                            msg = "预约时间错误";
                            ret = 12;
                        }
                        else if (pd.departcode == "")
                        {
                            msg = "预约的二科室代码错误";
                            ret = 13;
                        }
                        else if (pd.departname == "")
                        {
                            msg = "预约的二科室错误";
                            ret = 14;
                        }
                        else if (pd.doctorcode == "")
                        {
                            msg = "预约医生的编码错误";
                            ret = 15;
                        }
                        else if (pd.doctorname == "")
                        {
                            msg = "预约医生的姓名错误";
                            ret = 16;
                        }
                    }
                }

                if (ret != 0)
                {
                    ret = 99;
                    msg = "预约失败！";
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != conn && ConnectionState.Open == conn.State)
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 门诊预约历史
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_mzReservationHistory(string openid, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.mzReservationHistorySql(openid, out  flag, out  msg);
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
                        AlipaymzReservation pd = new AlipaymzReservation();

                        pd.preengageseq = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.preengagedate = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        pd.preengagetime = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.departcode = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.departname = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.doctorcode = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.doctorname = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.patientname = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.patientidcardno = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.patientphone = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        pd.patientaddress = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        pd.preengageno = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        pd.place = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        pd.fee = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        pd.preengagestate = !dr.IsDBNull(14) ? Convert.ToString(dr.GetInt64(14)) : "";
                        values.Add(pd);

                    }
                    msg = "查找到记录";
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 1;
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = ex.Message;
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
        /// 取消门诊预约
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="preengageseq">预约流水号</param>
        /// <param name="valves">返回值</param>
        /// <param name="msg">错误信息</param>
        /// <returns>0：成功，大于0：错误，小于0：异常</returns>
        public int DB_CancelmzReservation(string openid, string preengageseq, out AlipaymzReservation pd, out string msg)
        {
            msg = "";
            pd = new AlipaymzReservation();
            int ret = 99;
            OracleDataReader dataReader = null;
            int result = -99;

            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                IDataParameter[] parameters =
                {
                                          new OracleParameter("prm_yuyuehao", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_databuffer", OracleType.VarChar, 400) 
                };
                //预约号 
                parameters[0].Value = preengageseq;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;

                //pkg_gy_yinyijk.PRC_ZZSF_DIANZIZHCZ
                dataReader = DbHelperOra.ExeProcedureUseTran(conn, tx, WebConfigParameter.QXYuYueProcedureName, parameters, out result);

                string retInfo = Convert.ToString(parameters[2].Value);
                string[] arr1 = retInfo.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result != 0 || (arr1[0] != "OK"))
                {
                    tx.Rollback();
                    msg = "取消门诊预约出错。";
                    ret = 1;
                    return ret;
                }

                tx.Commit();

                bool _flag = false;
                string findtext = _builder.CancelmzOrderRetInfoSql(preengageseq, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findtext);

                dataReader = DbHelperOra.ExecuteReader(findtext, conn);
                if (dataReader.HasRows && dataReader != null)
                {
                    if (dataReader.Read())
                    {
                        pd.preengageseq = !dataReader.IsDBNull(0) ? dataReader.GetString(0) : "";
                        pd.preengagedate = !dataReader.IsDBNull(1) ? dataReader.GetDateTime(1).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        pd.preengagetime = !dataReader.IsDBNull(2) ? dataReader.GetString(2) : "";
                        pd.departname = !dataReader.IsDBNull(3) ? dataReader.GetString(3) : "";
                        pd.doctorname = !dataReader.IsDBNull(4) ? dataReader.GetString(4) : "";
                        pd.patientname = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : "";
                        pd.preengageno = !dataReader.IsDBNull(6) ? dataReader.GetString(6) : "";
                        pd.preengageno = !dataReader.IsDBNull(7) ? dataReader.GetString(7) : "";
                    }
                    msg = "已取消";
                    ret = 0;
                }
                else
                {
                    msg = "取消失败";
                    ret = 1;
                }
                dataReader.Close();
                return ret;
            }
            catch (Exception ex)
            {
                tx.Rollback();

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = ex.Message;
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
        /// 门诊预约报到
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="preengageseq">预约流水号</param>
        /// <param name="msg"></param>
        /// <returns>0：成功，大于0：错误，小于0：异常</returns>
        public int DB_mzReservationReport(string openid, string preengageseq, out mzreporter pd, out string msg)
        {
            msg = "";
            int ret = 99;
            OracleDataReader dataReader = null;
            int result = -99;
            pd = new mzreporter();

            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                AlipaymzReservation outInfo;
                ret = GetmzreportInfo(openid, preengageseq, out outInfo, out msg);
                if (ret != 0)
                {
                    return ret;
                }

                IDataParameter[] parameters ={
                                          new OracleParameter("prm_msg", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_outbuffer", OracleType.VarChar, 400) 
                                       };
                /*病人ID|排班id|操作员|操作员姓名|上下午标志|交易时间|银行交易流水号|银行卡号|银行交易类型|实付金额|预约id
                32445|1000050974|ZFB|ZFB|1|sysdate|||||1000031602*/
                parameters[0].Value = outInfo.patientid + "|" + outInfo.paibanid + "|ZFB|ZFB|" + outInfo.shiftcode + "|" + outInfo.Preengagetime + "|||||" + outInfo.yuyueid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;

                //PKG_GY_YINYIJK.prc_guahaozf
                dataReader = DbHelperOra.ExeProcedureUseTran(conn, tx, WebConfigParameter.GuaHaoProcedureName, parameters, out result);

                string retInfo = Convert.ToString(parameters[2].Value);
                int retappcode = Convert.ToInt32(parameters[1].Value);
                string[] arr1 = retInfo.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result != 0 || retappcode != 1)
                {
                    tx.Rollback();
                    msg = "挂号失败";
                    ret = 53;
                    return ret;
                }
                tx.Commit();

                pd.guahaoID = arr1[7];
                pd.state = "挂号成功";
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
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
        /// 挂号退号
        /// </summary>
        /// <param name="guahaoID">挂号标识</param>
        /// <param name="msg"></param>
        /// <returns>0：成功，大于0：错误，小于0：异常</returns>
        public int DB_RegistrationNumber(string guahaoID, out mzreporter pd, out string msg)
        {
            msg = "";
            int ret = 99;
            OracleDataReader dataReader = null;
            int result = -99;
            pd = new mzreporter();

            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                IDataParameter[] parameters ={
                                          new OracleParameter("prm_guahaoid", OracleType.VarChar),
                                          new OracleParameter("prm_shoufeiren", OracleType.VarChar),
                                          new OracleParameter("prm_yingyongid", OracleType.VarChar),
                                          new OracleParameter("prm_yuanquid", OracleType.VarChar),
                                          new OracleParameter("prm_gongzuozid", OracleType.VarChar),
                                          new OracleParameter("prm_quantuibz", OracleType.VarChar),
                                          new OracleParameter("prm_tuihaoyy", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_databuffer", OracleType.VarChar, 400) 
                                       };

                parameters[0].Value = guahaoID;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Value = "ZFB";
                parameters[1].Direction = ParameterDirection.Input;
                parameters[2].Value = "0101";
                parameters[2].Direction = ParameterDirection.Input;
                parameters[3].Value = "1";
                parameters[3].Direction = ParameterDirection.Input;
                parameters[4].Value = "0000000273";
                parameters[4].Direction = ParameterDirection.Input;
                parameters[5].Value = "0";
                parameters[5].Direction = ParameterDirection.Input;
                parameters[6].Value = "支付宝退号";
                parameters[6].Direction = ParameterDirection.Input;
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8].Direction = ParameterDirection.Output;

                //PKG_GY_YINYIJK.prc_guahaozf
                dataReader = RunProcedureUseTran(conn, tx,
                WebConfigParameter.TuiHaoProcedureName,
                parameters, out result);

                string retInfo = Convert.ToString(parameters[8].Value);
                int retappcode = Convert.ToInt32(parameters[7].Value);
                string[] arr1 = retInfo.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result != 0 || retappcode != 1)
                {
                    tx.Rollback();
                    msg = "取消挂号失败";
                    ret = 54;
                    return ret;
                }
                ret = 0;
                tx.Commit();

                pd.shoufeiID = arr1[0];
                pd.state = "取消挂号成功";
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
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
        /// 门诊预约排队列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="queueseq">排队流水号</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DB_mzReservationQueue(string openid, string queueseq, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = new ArrayList();
            msg = "";

            string _sqlConStr = WebConfigParameter.ConnectionPacsString;
            SqlConnection sqlConnection = new SqlConnection(_sqlConStr);
            SqlDataReader myReader = null;
            string _OracleConStr = WebConfigParameter.ConnectionHisString;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            string queueseqStr = "";
            ArrayList list = new ArrayList();

            try
            {
                bool _flag = false;

                string OracleText = _builder.GetqueueseqSql(openid, queueseq, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, OracleText);
                if (!_flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(OracleText, connection);
                if (dr.HasRows)
                {
                    string guahaoid = "";
                    StringBuilder sb = new StringBuilder();
                    while (dr.Read())
                    {
                        guahaoid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        if (!string.IsNullOrEmpty(guahaoid))
                        {
                            sb.AppendFormat("'{0}',", guahaoid);
                        }
                    }
                    queueseqStr = sb.ToString().TrimEnd(',');
                }
                else
                {
                    values = null;
                    msg = "获取失败";
                    ret = 17;
                    return ret;
                }

                string interText = _builder.SelectDoctorname(queueseqStr, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, interText);
                if (!_flag)
                {
                    return 10;
                }

                if (null != myReader)
                {
                    myReader.Close();
                }
                myReader = DbHelperSQL.ExecuteReader(interText, sqlConnection);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        mzreporter info = new mzreporter();
                        info.doctorname = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                        info.shiftname = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                        info.queueseq = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";
                        list.Add(info);
                    }
                    myReader.Close();
                }
                else
                {
                    values = null;
                    msg = "获取失败";
                    ret = 17;
                    return ret;
                }

                foreach (mzreporter ri in list)
                {
                    string mysqlText = _builder.GetmzReservationQueueSql(ri.doctorname, ri.shiftname, out _flag, out msg);
                    UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, mysqlText);
                    if (!_flag)
                    {
                        return 10;
                    }

                    if (null != myReader)
                    {
                        myReader.Close();
                    }
                    myReader = DbHelperSQL.ExecuteReader(mysqlText, sqlConnection);
                    if (myReader.HasRows)
                    {
                        int paiduixuhao = 1;
                        string currentid = "";
                        while (myReader.Read())
                        {
                            mzreporter pd = new mzreporter();
                            //pd.openid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                            pd.preengageseq = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                            pd.preengageno = !myReader.IsDBNull(2) ? Convert.ToString(myReader.GetDouble(2)) : "";
                            pd.queueseq = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                            //pd.queueid = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                            pd.queuetime = !myReader.IsDBNull(5) ? myReader.GetDateTime(5).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                            pd.shiftname = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                            pd.departname = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                            pd.doctorname = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                            pd.roomname = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                            pd.roompos = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                            pd.roomno = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                            //pd.currentid = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                            pd.queuestate = !myReader.IsDBNull(13) ? myReader.GetString(13) : "";
                            pd.patientname = !myReader.IsDBNull(14) ? myReader.GetString(14) : "";
                            if (pd.queuestate == "3")
                            {
                                currentid = pd.queueseq;
                            }
                            pd.currentid = currentid;
                            if (pd.queuestate == "0" || pd.queuestate == "3")
                            {
                                pd.queueid = Convert.ToString(paiduixuhao);
                                paiduixuhao++;
                            }
                            if (pd.queueseq == ri.queueseq)
                            {
                                values.Add(pd);
                            }
                        }
                        ret = 0;
                    }
                }

                if (values.Count <= 0)
                {
                    msg = "获取失败";
                    ret = 1;
                }
                myReader.Close();
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
                if (null != myReader)
                {
                    myReader.Close();
                }
                sqlConnection.Close();
                connection.Close();
            }
        }

        /// <summary>
        /// 门诊预约所有排队前十人员列表信息
        /// </summary>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DB_mzReservationQueueInfo(out ArrayList values, out string msg)
        {
            int ret = -1;
            values = new ArrayList();
            msg = "";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();

            string _sqlConStr = WebConfigParameter.ConnectionPacsString;
            SqlConnection sqlConnection = new SqlConnection(_sqlConStr);
            SqlDataReader myReader = null;
            string _OracleConStr = WebConfigParameter.ConnectionHisString;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;
                string findText = _builder.SelectPaibanYSSql(out _flag, out msg);
                if (!_flag)
                {
                    return 10;
                }

                myReader = DbHelperSQL.ExecuteReader(findText, sqlConnection);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        mzreporter info = new mzreporter();
                        info.doctorname = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                        info.shiftname = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                        list.Add(info);
                    }
                    myReader.Close();
                }

                foreach (mzreporter ri in list)
                {
                    findText = _builder.GetmzReservationListSql(ri.doctorname, ri.shiftname, out _flag, out msg);
                    UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);
                    if (!_flag)
                    {
                        return 10;
                    }

                    myReader = DbHelperSQL.ExecuteReader(findText, sqlConnection);
                    if (myReader.HasRows)
                    {
                        int paiduixuhao = 1;
                        string currentid = "";
                        while (myReader.Read())
                        {
                            mzreporter pd = new mzreporter();
                            //pd.openid = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                            //pd.preengageseq = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                            pd.preengageno = !myReader.IsDBNull(2) ? Convert.ToString(myReader.GetDouble(2)) : "";
                            pd.queueseq = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                            pd.queueid = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                            pd.queuetime = !myReader.IsDBNull(5) ? myReader.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : "";
                            pd.shiftname = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                            pd.departname = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                            pd.doctorname = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                            pd.roomname = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                            pd.roompos = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                            pd.roomno = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                            pd.currentid = !myReader.IsDBNull(12) ? myReader.GetString(12) : "";
                            pd.queuestate = !myReader.IsDBNull(13) ? myReader.GetString(13) : "";
                            if (pd.queuestate == "3")
                            {
                                currentid = pd.queueseq;
                            }
                            pd.currentid = currentid;
                            if (pd.queuestate == "0" || pd.queuestate == "3")
                            {
                                pd.queueid = Convert.ToString(paiduixuhao);
                                paiduixuhao++;
                            }
                            if (paiduixuhao < 12 && paiduixuhao > 2)
                            {
                                list2.Add(pd);
                            }
                        }
                        myReader.Close();
                    }
                }

                //if (values.Count <= 0)
                //{
                //    msg = "获取失败";
                //    ret = 1;
                //}

                foreach (mzreporter information in list2)
                {
                    findText = _builder.GetSelectOpenidsql(information.queueseq, out _flag, out msg);
                    UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);
                    if (!_flag)
                    {
                        return 10;
                    }
                    dr = DbHelperOra.ExecuteReader(findText, connection);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            mzreporter paiduiInfo = new mzreporter();
                            paiduiInfo = information;
                            paiduiInfo.openid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                            paiduiInfo.preengageseq = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            values.Add(paiduiInfo);
                        }
                    }
                }
                if (values.Count <= 0)
                {
                    values = null;
                    msg = "获取失败";
                    ret = 17;
                    return ret;
                }
                else
                {
                    ret = 0;
                }
                myReader.Close();
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
                if (null != myReader)
                {
                    myReader.Close();
                }
                if (null != dr)
                {
                    dr.Close();
                }
                sqlConnection.Close();
                connection.Close();
            }
        }

        /// <summary>
        /// 检查预约报到排队列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="queueseq">排队流水号</param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DB_CheckReservationReportList(string queueseq, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = new ArrayList();
            msg = "";

            string _sqlConStr = WebConfigParameter.ConnectionPacsString;
            SqlConnection sqlConnection = new SqlConnection(_sqlConStr);
            SqlDataReader myReader = null;

            try
            {
                bool _flag = false;

                string interText = _builder.GetCheckReservationReportListSql(queueseq, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                myReader = DbHelperSQL.ExecuteReader(interText, sqlConnection);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        CheckReservationReportList pd = new CheckReservationReportList();
                        //pd.openid = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                        //pd.preengageseq = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                        pd.preengageno = !myReader.IsDBNull(2) ? Convert.ToString(myReader.GetDouble(2)) : "";
                        pd.queueseq = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                        pd.queueid = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                        pd.queuetime = !myReader.IsDBNull(5) ? myReader.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : "";
                        pd.shiftname = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                        pd.departname = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                        pd.doctorname = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                        pd.roomname = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                        pd.roompos = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                        pd.roomno = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                        pd.currentid = !myReader.IsDBNull(12) ? myReader.GetString(12) : "";
                        pd.queuestate = !myReader.IsDBNull(13) ? myReader.GetString(13) : "";
                        values.Add(pd);
                    }
                }
                if (values.Count > 0)
                {
                    msg = "获取成功";
                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "获取失败";
                    ret = 17;
                }
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
                if (null != myReader)
                {
                    myReader.Close();
                }
                sqlConnection.Close();
            }

        }

        /// <summary>
        /// 检查预约报到
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="preengageseq">预约流水号</param>
        /// <param name="msg"></param>
        /// <returns>0：成功，大于0：错误，小于0：异常</returns>
        public int DB_CheckReservationReport(string patientid, string preengageseq, out mzreporter pd, out string msg)
        {
            msg = "";
            int ret = 99;
            OracleDataReader dataReader = null;
            int result = -99;
            pd = new mzreporter();

            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                AlipaymzReservation outInfo;
                ret = GetmzreportInfo(patientid, preengageseq, out outInfo, out msg);
                if (ret != 0)
                {
                    return ret;
                }

                IDataParameter[] parameters ={
                                          new OracleParameter("prm_msg", OracleType.VarChar),
                                          new OracleParameter("prm_appcode", OracleType.Number), 
                                          new OracleParameter("prm_outbuffer", OracleType.VarChar, 400) 
                                       };
                /*病人ID|排班id|操作员|操作员姓名|上下午标志|交易时间|银行交易流水号|银行卡号|银行交易类型|实付金额|预约id
                32445|1000050974|ZFB|ZFB|1|sysdate|||||1000031602*/
                parameters[0].Value = outInfo.patientid + "|" + outInfo.paibanid + "|ZFB|ZFB|" + outInfo.shiftcode + "|" + outInfo.Preengagetime + "|||||" + outInfo.yuyueid;
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;

                //PKG_GY_YINYIJK.prc_guahaozf
                dataReader = DbHelperOra.ExeProcedureUseTran(conn, tx,
                WebConfigParameter.GuaHaoProcedureName,
                parameters, out result);

                string retInfo = Convert.ToString(parameters[2].Value);
                int retappcode = Convert.ToInt32(parameters[1].Value);
                string[] arr1 = retInfo.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result != 0 || retappcode != 1)
                {
                    tx.Rollback();
                    msg = "挂号失败";
                    ret = 53;
                    return ret;
                }
                tx.Commit();

                pd.guahaoID = arr1[7];
                pd.state = "挂号成功";
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
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
        /// 检查预约所有排队前十人员列表信息
        /// </summary>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DB_CheckReservationReportQueueList(out ArrayList values, out string msg)
        {
            int ret = -1;
            values = new ArrayList();
            msg = "";

            string _sqlConStr = WebConfigParameter.ConnectionPacsString;
            SqlConnection sqlConnection = new SqlConnection(_sqlConStr);
            SqlDataReader myReader = null;

            try
            {
                bool _flag = false;

                string interText = _builder.CheckReservationReportQueueListSql(out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                myReader = DbHelperSQL.ExecuteReader(interText, sqlConnection);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        CheckReservationReportList pd = new CheckReservationReportList();
                        //pd.openid = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                        //pd.preengageseq = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                        pd.preengageno = !myReader.IsDBNull(2) ? Convert.ToString(myReader.GetDouble(2)) : "";
                        pd.queueseq = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                        pd.queueid = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                        pd.queuetime = !myReader.IsDBNull(5) ? myReader.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : "";
                        pd.shiftname = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                        pd.departname = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                        pd.doctorname = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                        pd.roomname = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                        pd.roompos = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                        pd.roomno = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                        pd.currentid = !myReader.IsDBNull(12) ? myReader.GetString(12) : "";
                        pd.queuestate = !myReader.IsDBNull(13) ? myReader.GetString(13) : "";
                        values.Add(pd);
                    }
                }
                if (values.Count > 0)
                {
                    msg = "获取成功";
                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "获取失败";
                    ret = 17;
                }
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
                if (null != myReader)
                {
                    myReader.Close();
                }
                sqlConnection.Close();
            }
        }


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idcardno"></param>
        /// <param name="info"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int GetOrderInfo(string name, string idcardno, string patientid, out AlipaymzOrderNeedInfo info, out string msg)
        {
            int ret = 99;
            msg = "";
            info = new AlipaymzOrderNeedInfo();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                string getInfoSql = "select cardname, cardno, patientid, birthday, phone, address, sex from ZFB_MENZHENKLB t " +
                "where idcardno = '{1}' and patientname = '{0}' and patientid =" + patientid;

                string[] _paramters = new string[2];
                _paramters[0] = name;
                _paramters[1] = idcardno;

                string result = string.Format(getInfoSql, _paramters);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, result);
                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        info.cardname = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        info.cardno = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        info.patientid = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        info.birthday = !dr.IsDBNull(3) ? dr.GetDateTime(3).ToString(AppUtils.DateTime_Format_YearMonthDay) : "";
                        info.patientphone = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        info.patientaddress = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        info.patientsex = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                    }
                    ret = 0;
                }
                else
                {
                    msg = "预约病人信息本院获取失败！";
                }
                if (dr != null)
                {
                    dr.Close();
                }
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
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


        //读取系统时间
        private Boolean DB_GetSysdate(out DateTime sysdate)
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


        private int DB_GetScheduledate(string scheduleSeq, out string scheduleDate, out string msg)
        {
            int ret = 99;
            scheduleDate = "";
            msg = "";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                string findId = "select scheduledate from ZFB_YUYUE_MZHY t where scheduleseq = '{0}'";
                string result = string.Format(findId, scheduleSeq);
                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.Read())
                {
                    scheduleDate = !dr.IsDBNull(0) ? dr.GetDateTime(0).ToString(AppUtils.DateTime_Format_YearMonthDay) : "1900-01-01";
                    ret = 0;
                }

                dr.Close();
                return ret;

            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
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

        private int GetmzreportInfo(string openid, string preengageseq, out AlipaymzReservation info, out string msg)
        {
            int ret = 99;
            msg = "";
            info = new AlipaymzReservation();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                string getInfoSql = "select patientid, paibanid, shiftcode, yuyueid from ZFB_YUYUE_YYLS t " +
                "where openid = '{0}' and preengageseq = '{1}' ";

                string[] _paramters = new string[2];
                _paramters[0] = openid;
                _paramters[1] = preengageseq;

                string result = string.Format(getInfoSql, _paramters);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, result);
                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        info.patientid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        info.paibanid = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        info.shiftcode = !dr.IsDBNull(2) ? Convert.ToString(dr.GetInt32(2)) : "";
                        info.yuyueid = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        DateTime jysj;
                        if (DB_getSysdate(out jysj) == false)
                        {
                            msg = "读取系统时间出错";
                            return 24;
                        }
                        info.Preengagetime = jysj.ToString(AppUtils.DateTime_Format_YearMonthDay);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "获取挂号信息失败！";
                }
                if (dr != null)
                {
                    dr.Close();
                }
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.Message;
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

        //读取系统时间
        private Boolean DB_getSysdate(out DateTime sysdate)
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
        /// 执行存储过程带事务处理  并且通过返回值来进行回滚
        /// </summary>
        /// <param name="connection">数据库连接参数</param>
        /// <param name="storedProcName">存储过程的名字</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        private static OracleDataReader RunProcedureUseTran(OracleConnection connection, OracleTransaction tx,
            string storedProcName, IDataParameter[] parameters, out int result)
        {
            OracleDataReader returnReader;
            result = -99;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            OracleCommand command = DbHelperOra.BuildQueryCommand(connection, storedProcName, parameters);
            command.Transaction = tx;
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                result = 0;
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                result = -1;
                throw new Exception(E.Message);
            }

            return returnReader;
        }
    }
}