using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Utils;

namespace HospitaPaymentService.Wzszhjk.DAL.Database
{
    public class QueryReportDB :BaseDB
    {
        private BuilderSql _builder;
        public QueryReportDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 查询报告单列表(根据病人ID）
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="values">报告单信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int QueryReportListByBRID(string brid, string brlx, out ArrayList values, out string msg)
        {

            string _oracleConStr = WebConfigParameter.ConnectionHisString;
            string hm = brid;
            values = new ArrayList();

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDSRMYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDERMYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZHTYY)
            {
                _oracleConStr = WebConfigParameter.ConnectionLisString;

                if (ConvertToBkhm(brid, brlx, out hm, out msg) != 0)
                {
                    return 3;
                }
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY && null != brlx && brlx.Equals("2"))
            {
                if (ConvertToMZBridForWzsdeyy(brid, brlx, out hm, out msg) != 0)
                {
                    return 3;
                }
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTSXZYY && null != brlx && brlx.Equals("2"))
            {
                if (ConvertToMZBridForWzstsxzyy(brid, out hm, out msg) != 0)
                {
                    return 3;
                }
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXFYBJYY ||
                     WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY ||
                     WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSRAZYY)
            {
                _oracleConStr = WebConfigParameter.ConnectionLisString;
            }

            OracleConnection connection = new OracleConnection(_oracleConStr);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;
                string sql = _builder.GetSqlReportList(hm, brlx, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -1;
                msg = "";
                dr = DbHelperOra.ExecuteReader(sql, connection);    
                if (null != dr && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReportInfo ri = new ReportInfo();
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSYJXDSRMYY)
                        {
                            ri.bgdh = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        }
                        else
                        {
                            ri.bgdh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        }
                        ri.sjmd = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.cjsj = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        ri.sjr = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        ri.jysj = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        ri.jyr = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        ri.shr = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        ri.jzch = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        ri.zdjg = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        ri.bbmc = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        ri.mzbz = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        ri.dyjb = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        ri.bz = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        ri.hzbh = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDYRMYY ||
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXFYBJYY ||
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTSXZYY)
                        {
                            ri.sbm = !dr.IsDBNull(14) ? Convert.ToString(dr.GetInt64(14)) : "";
                        }
                        else
                        {
                            ri.sbm = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        }
                        ri.brxm = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        ri.jgmc = WebConfigParameter.HospitalChinaName();

                        values.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到报告单";
                    ret = 2;

                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 根据条形码或者报告单号报告明细
        /// </summary>
        /// <param name="code">条形码或者报告单号</param>
        /// <param name="lx">号码类型 1：报告单号 2条码查询</param>
        /// <param name="values">报告信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int QueryReportListByCode(string code1, string lx, string brxm, out ArrayList values, out string msg)
        {
            string hm = code1;
            string _oraConStr = WebConfigParameter.ConnectionHisString;
            values = new ArrayList();
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY)
            {
                _oraConStr = WebConfigParameter.ConnectionLisString;

                if (lx.Equals("2"))
                {
                    hm = RealSbmForWZSZXYJHYY(hm);
                }
            }
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDSRMYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDERMYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSRAZYY||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZHTYY)
            {
                _oraConStr = WebConfigParameter.ConnectionLisString;
            }

            OracleConnection connection = new OracleConnection(_oraConStr);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;
                string sql = _builder.GetSqlReportDetail(hm, lx, brxm, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -1;
                msg = "";
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        ReportInfo ri = new ReportInfo();
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSYJXDSRMYY)
                        {
                            ri.bgdh = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        }
                        else
                        {
                            ri.bgdh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        }
                        ri.sjmd = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.cjsj = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        ri.sjr = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        ri.jysj = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        ri.jyr = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        ri.shr = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        ri.jzch = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        ri.zdjg = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        ri.bbmc = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        ri.mzbz = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        ri.dyjb = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        ri.bz = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        ri.hzbh = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDYRMYY ||
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXFYBJYY ||
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTSXZYY)
                        {
                            ri.sbm = !dr.IsDBNull(14) ? Convert.ToString(dr.GetInt64(14)) : "";
                        }
                        else
                        {
                            ri.sbm = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        }
                        ri.brxm = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        ri.jgmc = WebConfigParameter.HospitalChinaName();

                        ICollection<ReportDetail> rds;
                        string child_msg;
                        int rtDetail = DB_QueryReportDetail(ri.bgdh, out rds, out child_msg);
                        if (rtDetail == 0)
                        {
                            ri.details = rds;
                        }
                        else
                        {
                            msg += "[单号" + ri.bgdh + "详细查询错误]" + child_msg + ";";
                            ret = 3;
                        }
                        values.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到该编号的报告单，请检查编号";
                    ret = 2;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 获得真实的条码
        /// 规则  当前日期的前面3位 + HM + 校验位
        /// 校验位求取规则： hm中所有数字的和 除以43得到的余数 V
        ///               V在10~35时  V+55转换成char
        ///               V=36 "-" V=37 "."  V=38 " "
        ///               V=39 "$" V=40 "/"  V=41 "+" 
        ///               V=42 "%"
        ///               其他不变
        /// </summary>
        /// <param name="hm"></param>
        /// <returns></returns>
        private string RealSbmForWZSZXYJHYY(string hm)
        {

            string result = "";
            int value = 0;
            for (int i = 0; i < hm.Length; i++)
            {
                value += Convert.ToInt32(hm[i].ToString());
            }

            value = value % 43;
            string s;
            if (value > 9 && value < 36)
            {
                s = Convert.ToChar(value + 55).ToString();
            }
            else if (value == 36)
            {
                s = "-";
            }
            else if (value == 37)
            {
                s = ".";
            }
            else if (value == 38)
            {
                s = " ";
            }
            else if (value == 39)
            {
                s = "$";
            }
            else if (value == 40)
            {
                s = "/";
            }
            else if (value == 41)
            {
                s = "+";
            }
            else if (value == 42)
            {
                s = "%";
            }
            else
            {
                s = Convert.ToString(value);
            }

            result = DateTime.Today.ToString(AppUtils.DateTime_Format_Year).Substring(0, 3) + hm + s;
            return result;
        }

        /// <summary>
        /// 查询报告单列表(根据病人ID）
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="values">报告单信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryReportJCListByBRID(string brid, string brlx, out ArrayList values, out string msg)
        {
            string oracleConStr = GetJCReportDBConStr();
            string hm = brid;
            values = new ArrayList();

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY || 
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDSRMYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDERMYY)
            {
                if (ConvertToBkhm(brid, brlx, out hm, out msg) != 0)
                {
                    return 3;
                }
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY && null != brlx && brlx.Equals("2"))
            {
                if (ConvertToMZBridForWzsdeyy(brid, brlx, out hm, out msg) != 0)
                {
                    return 3;
                }
            }

            OracleConnection connection = new OracleConnection(oracleConStr);
            OracleDataReader dr = null;
            values = new ArrayList();

            try
            {
                bool _flag = false;

                string sql = _builder.GetSqlReportJCList(hm, brlx, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -99;
                msg = "";
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        ReportInfo ri = new ReportInfo();
                        ri.bgdh = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt32(0)) : " ";
                        ri.sjmd = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.cjsj = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        ri.sjr = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        ri.jysj = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        ri.jyr = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        ri.shr = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        ri.jzch = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        ri.zdjg = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        ri.bbmc = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        ri.mzbz = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        ri.dyjb = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        ri.bz = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        ri.hzbh = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        ri.sbm = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        ri.brxm = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        ri.jgmc = WebConfigParameter.HospitalChinaName();

                        values.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到报告单";
                    ret = 2;

                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 获得检查报告的数据库的连接字符串
        /// </summary>
        /// <returns></returns>
        private  string GetJCReportDBConStr()
        {
            string oracleConStr = WebConfigParameter.ConnectionHisString;

            if (AppUtils.HOSPITALNAME.WZSZXYJHYY == WebConfigParameter.HospitalName()||
                AppUtils.HOSPITALNAME.WZSYJXDSRMYY == WebConfigParameter.HospitalName())
            {
                oracleConStr = WebConfigParameter.ConnectionLisString;
            }

            else if (AppUtils.HOSPITALNAME.WZSCNXDSRMYY == WebConfigParameter.HospitalName()
                  ||AppUtils.HOSPITALNAME.WZSCNXDYRMYY == WebConfigParameter.HospitalName()
                  ||AppUtils.HOSPITALNAME.WZSCNXDERMYY == WebConfigParameter.HospitalName()
                  || AppUtils.HOSPITALNAME.WZSTXRMYY == WebConfigParameter.HospitalName()
                  || AppUtils.HOSPITALNAME.WZSRAZYY == WebConfigParameter.HospitalName()
                  || AppUtils.HOSPITALNAME.WZSTSXZYY == WebConfigParameter.HospitalName()
                  || AppUtils.HOSPITALNAME.WZSRARMYY == WebConfigParameter.HospitalName())
            {
                oracleConStr = WebConfigParameter.ConnectionPacsString;
            }

            return oracleConStr;
        }

        /// <summary>
        /// 根据条形码或者报告单号报告明细
        /// </summary>
        /// <param name="code">条形码或者报告单号</param>
        /// <param name="lx">号码类型 1：报告单号 2条码查询</param>
        /// <param name="values">报告信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryReportJCListByCode(string code, string lx, string brxm, out ArrayList values, out string msg)
        {
            string oracleConStr = GetJCReportDBConStr();
            OracleConnection connection = new OracleConnection(oracleConStr);
            OracleDataReader dr = null;
            values = new ArrayList();
            try
            {
                bool _flag = false;
                string sql = _builder.GetSqlReportJCDetail(code, lx, brxm, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -99;
                msg = "";
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReportInfo ri = new ReportInfo();
                        ri.bgdh = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt32(0)) : "";
                        ri.sjmd = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.cjsj = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        ri.sjr = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        ri.jysj = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        ri.jyr = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        ri.shr = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        ri.jzch = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        ri.zdjg = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        ri.bbmc = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        ri.mzbz = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        ri.dyjb = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        ri.bz = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        ri.hzbh = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        ri.sbm = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        ri.brxm = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        ri.jgmc = WebConfigParameter.HospitalChinaName();

                        ICollection<ReportDetail> rds;
                        string child_msg;
                        int rtDetail;
                        if (AppUtils.HOSPITALNAME.WZSTXRMYY == WebConfigParameter.HospitalName()||
                            AppUtils.HOSPITALNAME.WZSRAZYY == WebConfigParameter.HospitalName())
                        {
                            rtDetail = DB_QueryReportJCDetail(ri.hzbh, out rds, out child_msg);
                        }
                        else
                        {
                            rtDetail = DB_QueryReportJCDetail(ri.bgdh, out rds, out child_msg);
                        }
                        if (rtDetail == 0)
                        {
                            ri.details = rds;
                        }
                        else
                        {
                            msg += "[单号" + ri.bgdh + "详细查询错误]" + child_msg + ";";
                            ret = 3;
                        }
                        values.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到该编号的报告单，请检查编号";
                    ret = 2;

                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 根据报告单号查询报告详细
        /// </summary>
        /// <param name="bgdh">报告单号</param>       
        /// <param name="rds">报告信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryReportDetail(string bgdh, out ICollection<ReportDetail> rds, out string msg)
        {
            string _oraConStr = WebConfigParameter.ConnectionHisString;

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDSRMYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDERMYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXFYBJYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSRAZYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY)
            {
                _oraConStr = WebConfigParameter.ConnectionLisString;
            }

            OracleConnection connection = new OracleConnection(_oraConStr);
            OracleDataReader dr = null;


            try
            {
                bool _flag = false;

                string sql = _builder.GetReportDetailXM(bgdh, out _flag, out msg);

                if (!_flag)
                {
                    rds = new List<ReportDetail>();
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    rds = new List<ReportDetail>();
                    while (dr.Read())
                    {
                        ReportDetail rd = new ReportDetail();
                        rd.mc = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        rd.dw = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        rd.ckjg = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        rd.ts = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        rd.jg = !dr.IsDBNull(4) ? dr.GetString(4) : "";

                        rds.Add(rd);
                    }


                    msg = "找到报告明细";
                    return 0;
                }
                else
                {
                    rds = null;
                    msg = "未找到报告详细内容";
                    return 2;
                }
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                rds = null;
                return -2;
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
        /// 根据报告单号查询报告详细
        /// </summary>
        /// <param name="bgdh">报告单号</param>       
        /// <param name="rds">报告信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryReportJCDetail(string bgdh, out ICollection<ReportDetail> rds, out string msg)
        {

            string oracleConStr = GetJCReportDBConStr();
            OracleConnection connection = new OracleConnection(oracleConStr);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;

                string sql = _builder.GetReportJCDetailXM(bgdh, out _flag, out msg);

                if (!_flag)
                {
                    rds = new List<ReportDetail>();
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    rds = new List<ReportDetail>();
                    while (dr.Read())
                    {
                        ReportDetail rd = new ReportDetail();
                        rd.mc = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        rd.dw = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        rd.ckjg = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        rd.ts = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        rd.jg = !dr.IsDBNull(4) ? dr.GetString(4) : "";

                        rds.Add(rd);
                    }


                    msg = "找到报告明细";
                    return 0;
                }
                else
                {
                    rds = null;
                    msg = "未找到报告详细内容";
                    return 2;
                }
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                rds = null;
                return -2;
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
        /// //温州市中西医结合医院和温州市苍南县第三人民医院(JC)
        /// // 新增温州市苍南县第二人民医院
        /// 转换brid成mzhm
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="mzhm"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int ConvertToBkhm(string brid, string brlx, out string mzhm, out string msg)
        {
            int ret = -1;
            mzhm = "";
            msg = "";
            string sql = "";

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY )
            {
                sql = _builder.QueryHMForZXYReport(brid, brlx);
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDSRMYY
                || WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZHTYY) 
            {
                sql = _builder.QueryHMForCNSYReport(brid, brlx);
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDERMYY)
            {
                sql = _builder.QueryHMForCNEYReport(brid, brlx);
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        mzhm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    }

                    ret = 0;
                }
                else
                {
                    msg = "没有找到报告信息";
                    ret = 2;
                }
                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
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
        /// 转换zyh门诊brid
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="mzhm"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int ConvertToMZBridForWzsdeyy(string brid, string brlx, out string mzhm, out string msg)
        {
            int ret = -1;
            mzhm = "";
            msg = "";
           

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            string sql = "select ms.brid from ms_brda ms, zy_brry zy where zy.mzhm = ms.mzhm and zy.brxm =ms.brxm and zy.zyh = '" + brid + "'";

            try
            {
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        mzhm = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                    }

                    ret = 0;
                }
                else
                {
                    msg = "没有找到报告信息";
                    ret = 2;
                }
                return ret;
            }
            catch (Exception ex)
            {
                msg = ex.StackTrace;
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

        private int ConvertToMZBridForWzstsxzyy(string brid, out string mzhm, out string msg)
        {
            int ret = -1;
            mzhm = "";
            msg = "";


            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            string sql = "select zy.binglih from zy_bingrenxx zy where  zy.bingrenbh = '" + brid + "'";

            try
            {
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        mzhm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    }

                    ret = 0;
                }
                else
                {
                    msg = "没有找到报告信息";
                    ret = 2;
                }
                return ret;
            }
            catch (Exception ex)
            {
                msg = ex.StackTrace;
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
    }
}