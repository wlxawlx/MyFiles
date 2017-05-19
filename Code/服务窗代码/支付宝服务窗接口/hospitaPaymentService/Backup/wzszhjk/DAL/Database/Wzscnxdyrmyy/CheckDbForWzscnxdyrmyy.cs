using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database.Wzscnxdyrmyy
{
    public class CheckDbForWzscnxdyrmyy : BaseDB
    {
        private BuilderSql _builder;

        public CheckDbForWzscnxdyrmyy()
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
        public int DB_queryReportJCListByBRID(string brid, string brlx, out ArrayList values, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();
            try
            {
                bool _flag = false;

                string sql = _builder.GetSqlReportJCListForWzscnxdyrmyy(brid, brlx, out _flag, out msg);

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
                        ri.bgdh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        ri.sjmd = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.jysj = !dr.IsDBNull(2) ? dr.GetString(2) : "";                 
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
        public int DB_queryReportJCListByCode(string code, string lx, string brxm, out ArrayList values, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
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
                        ri.bgdh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
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
                        int rtDetail = DB_queryReportJCDetail(ri.bgdh, out rds, out child_msg);
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
        public int DB_queryReportDetail(string bgdh, out ICollection<ReportDetail> rds, out string msg)
        {
            string _oraConStr = WebConfigParameter.ConnectionHisString;
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY)
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
        public int DB_queryReportJCDetail(string bgdh, out ICollection<ReportDetail> rds, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
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
    }
}