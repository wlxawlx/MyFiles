using System;
using System.Collections;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database.Wzszyy
{
    public class FZPDInfoForWZSZYY : BaseDB
    {
        private BuilderSql _builder;

        public FZPDInfoForWZSZYY()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 查询医院预约信息
        /// </summary>
        /// <returns></returns>
        public int QuerySignInfo(string brid, string brlx, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection con = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                bool flag = false;
                string sql = _builder.QueryFzpdInfoForWZSZYYSql(brid, brlx, out flag, out msg);
                if (!flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(sql, con);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        RegHospitalInfo info = new RegHospitalInfo();
                        info.pdhm = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        info.brxm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        info.ksmc = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        info.doctor = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        info.yyly = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        info.yysj = !dr.IsDBNull(5) ? dr.GetString(5) : "";

                        values.Add(info);
                    }

                    if (null == values || values.Count < 1)
                    {
                        msg = "没有病人当天的预约信息";
                        ret = 1;
                    }
                    else
                    {
                        ret = 0;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                values = null;
                msg = GetExceptionInfo(ex);
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != con)
                {
                    con.Close();
                }
            }

            return ret;
        }

        /// <summary>
        /// 查询医院排队信息
        /// </summary>
        /// <returns></returns>
        public int QueryQueueInfo(string brid, string brlx, out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            OracleConnection con = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                bool flag = false;
                string sql = _builder.QueryQueueInfoForWZSZYYSql(brid, brlx, out flag, out msg);
                if (!flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(sql, con);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        string ksdm = "", ysdm = "", jzsjdm = "";
                        PainterQueueInfo info = new PainterQueueInfo();
                        info.zt = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        info.brxm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        info.sfzh = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        info.ksmc = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        info.zsmc = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        info.doctor = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        info.pdhm = !dr.IsDBNull(6) ? dr.GetString(6) : "-1";
                        info.yjjzsj = !dr.IsDBNull(7) ? dr.GetDateTime(7).ToString(AppUtils.DateTime_Format_All) : "";
                        ksdm = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        ysdm = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        jzsjdm = !dr.IsDBNull(10) ? dr.GetString(10) : "";

                        if (string.IsNullOrEmpty(ksdm) || string.IsNullOrEmpty(ysdm) || string.IsNullOrEmpty(jzsjdm) ||
                            Convert.ToInt32(info.pdhm) < 0 || Convert.ToInt64(info.pdhm) < 0)
                        {
                            msg = "后台排队信息出错";
                            ret = 3;
                            return ret;
                        }



                        int waitCount = -1;
                        ret = QueryQueueInfoWaitInfo(ksdm, ysdm, jzsjdm, brlx, Convert.ToInt32(info.pdhm), out waitCount, out msg);
                        if (ret != 0)
                        {
                            return ret;
                        }

                        if (waitCount < 0)
                        {
                            msg = "获取排在前面人数出错";
                            ret = 3;
                            return ret;
                        }

                        info.waitCount = Convert.ToString(waitCount);
                        values.Add(info);
                    }

                    if (null == values || values.Count < 1)
                    {
                        msg = "没有病人当天的排在前面人数";
                        ret = 1;
                    }
                    else
                    {
                        ret = 0;
                    }
                }
                else
                {
                    msg = "数据库中没有找到排在前面人数";
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                values = null;
                msg = GetExceptionInfo(ex);
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != con)
                {
                    con.Close();
                }
            }

            return ret;
        }

        /// <summary>
        /// 查询当前排队人数
        /// </summary>
        /// <parameter name = 'ksdm'>科室代码</parameter>
        /// <parameter name = 'ysdm'>医生代码</parameter>
        /// <parameter name = 'jzsjdm'>就诊时间代码</parameter>
        /// <returns></returns>
        public int QueryQueueInfoWaitInfo(string ksdm, string ysdm, string jzsjdm, string brlx,  
            int pdhm, out int value, out string msg)
        {
            int ret = -1;
            value =  -1;
            msg = "";

            OracleConnection con = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                bool flag = false;
                string sql = _builder.QueryWaitCountForWZSZYYSql(ksdm, ysdm, jzsjdm, pdhm, brlx, out flag, out msg);
                if (!flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(sql, con);

                if (dr.HasRows)
                {
                    if(dr.Read())
                    {
                        value = !dr.IsDBNull(0) ? dr.GetInt32(0) : -1;                                              
                    }

                    if (value < 0)
                    {
                        msg = "数据库中排队信息错误";
                        ret = 1;
                    }
                    else
                    {
                        ret = 0;
                    }
                }
                else
                {
                    msg = "数据库中没有找到排队信息";
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                value = -1;
                msg = GetExceptionInfo(ex);
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != con)
                {
                    con.Close();
                }
            }

            return ret;
        }
    }
}