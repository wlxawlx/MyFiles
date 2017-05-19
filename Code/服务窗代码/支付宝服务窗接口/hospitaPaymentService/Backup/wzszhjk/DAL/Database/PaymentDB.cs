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

namespace HospitaPaymentService.Wzszhjk.DAL.Database
{
    public class PaymentDB : BaseDB
    {
        private BuilderSql _builder;

        public PaymentDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="curBrid">病人ID</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="type">支付类型 1：返回未支付的订单 2：返回已经支付的订单</param>
        /// <param name="querytype">查询类型 1：不分页查询 2：分页查询</param>
        /// <param name="pageno">所在页数</param>
        /// <param name="pagenum">每页显示的条数</param>
        /// <param name="values">返回订单信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryQrderList(string brid, string brlx, string type, string querytype, int pageno, int pagenum,
            out ArrayList values, out string msg)
        {
            int ret = -1;
            values = null;
            msg = "";

            string curBrid = brid;
            if (AppUtils.HOSPITALNAME.WZSDEYY == WebConfigParameter.HospitalName() && brlx.Equals("2"))
            {
                ret = ConvertZYHTOBRIDForWZSDEYY(curBrid, out curBrid, out msg);
                if (0 != ret)
                {
                    return ret;
                }
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool flag = false;

                string exeSql = _builder.OrderListSql(type, pageno, pagenum, querytype, brlx, curBrid,
                  out  flag, out  msg);
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
                        ReportOrder pd = new ReportOrder();

                        pd.yylsh = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        pd.yhlsh = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.yhmc = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.cssj = !dr.IsDBNull(3) ? dr.GetDateTime(3).ToString(AppUtils.DateTime_Format_All) : "";
                        pd.czje = !dr.IsDBNull(4) ? dr.GetDouble(4) : 0;
                        pd.ddzt = !dr.IsDBNull(5) ? Convert.ToString(dr.GetInt32(5)) : "";
                        pd.sjczzt = !dr.IsDBNull(6) ? Convert.ToString(dr.GetInt32(6)) : "";
                        pd.paytype = !dr.IsDBNull(7) ? dr.GetString(7) : "";

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
        /// 创建订单
        /// </summary>
        /// <param name="curBrid">病人ID</param>
        /// <param name="bkhm">卡号</param>
        /// <param name="czje">充值金额</param>
        /// <param name="tkje">退款金额</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_CreateOrder(string brid, string bkhm, double czje, double tkje, string brlx, string payType,
            out long yylsh, out string msg)
        {
            msg = "";
            yylsh = -1;

            int ret = -1;

            string curBrid = brid;
            if (AppUtils.HOSPITALNAME.WZSDEYY == WebConfigParameter.HospitalName() && brlx.Equals("2"))
            {
                ret = ConvertZYHTOBRIDForWZSDEYY(brid, out curBrid, out msg);
                if (0 != ret)
                {
                    return ret;
                }
            }
            if (AppUtils.HOSPITALNAME.WZSTXRMYY == WebConfigParameter.HospitalName())
            {
                ret = ConvertTOBRIDForWZSTXRMYY(bkhm, out curBrid, out msg);
                if (0 != ret)
                {
                    return ret;
                }
            }

            if (!IsSupportPayment(brlx))
            {
                msg = "医院暂时不支持该功能的使用";
                ret = 11;
                return ret;
            }

            BindCardDB _bindCardDb = new BindCardDB();
            if (!_bindCardDb.ValidBrid(brid, brlx, out msg) || string.IsNullOrEmpty(curBrid))
            {
                msg += "病人ID不存在";
                return 1;
            }

            if (!IsAmountAbnormal(brlx, czje))
            {
                msg = "充值金额异常或超过限定额度";
                return 2;
            }

            //产生日期
            DateTime _cssj;
            if (DB_Sysdate(out _cssj) == false)
            {
                msg = "读取服务器系统时间出错";
                return 3;
            }

            //判断绑卡号码是否正确
            if (WebConfigParameter.HospitalName() != AppUtils.HOSPITALNAME.WZSDQRMYY)
            {
                if (_bindCardDb.DB_ValidCard(brid, bkhm, brlx, out msg) != 0)
                {
                    msg = "卡号校验失败";
                    return 4;
                }
            }

            //产生医院订单流水号
            yylsh = YylshSeq();
            if (yylsh <= 0)
            {
                msg = "订单流水号产生失败";
                return 5;
            }

            string _brxm = null, _sfzh = null, _lxdh = null;

            bool _flag = false;

            string _querySql = _builder.QueryInfoForCreateOrderSql(brid, brlx, out _flag, out msg);
            UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, _querySql);

            if (!_flag)
            {
                return 10;
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            dr = DbHelperOra.ExecuteReader(_querySql, connection);

            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    _brxm = !dr.IsDBNull(0) ? dr.GetString(0).ToUpper() : "";
                    _sfzh = !dr.IsDBNull(1) ? dr.GetString(1).ToUpper() : "";
                    _lxdh = !dr.IsDBNull(2) ? dr.GetString(2).ToUpper() : "";
                }
                else
                {
                    msg = "该病人ID无效";
                    ret = 6;
                    return ret;
                }
            }
            else
            {
                msg = "医院没有找到病人信息";
                ret = 7;
                return ret;
            }

            string logMsg = " yylsh : " + yylsh + " czje : " + czje + " tkje : " + tkje + " ddzt : " + 0 + " jyfs : " + 10 +
" bkhm : " + bkhm + " brid : " + curBrid + " cssj : " + _cssj.ToString("yyyy-MM-dd HH:mm:ss") +
" czsj : " + _cssj.ToString("yyyy-MM-dd HH:mm:ss") + " brlx : " + brlx + " 。";
            UtilLog.GetInstance().WriteOrderLog("创建订单成功: ", logMsg);

            try
            {
                string insertSql = _builder.CreateOrderSql(yylsh, czje, tkje, brlx, bkhm, curBrid,
                    _cssj, _sfzh, _lxdh, _brxm, payType);

                ArrayList listSql = new ArrayList();
                listSql.Add(insertSql);
                DbHelperOra.ExecuteSqlTran(listSql, connection);

                msg = "订单已经成功创建";
                ret = 0;
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
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="yhlsh">银行流水号</param>
        /// <param name="sjczzt">手机支付状态</param>
        /// <param name="czsj">操作时间</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_FinishOrder(long yylsh, string yhlsh, string sjczzt, DateTime czsj,
            string payType, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int count = 0;
            try
            {
                string sqlStr = _builder.QueryOrderCountSql(yylsh, payType);

                dr = DbHelperOra.ExecuteReader(sqlStr, connection);
                if (dr.Read())
                {
                    count = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;

                    if (count != 1)
                    {
                        msg = "记录数据不对应没有原始记录";
                        return 1;
                    }
                }
                else
                {
                    msg = "无法找到该记录原始记录";
                    return 1;
                }


                ArrayList listSql = new ArrayList();
                string lsStr = "";

                lsStr = _builder.UpdateOrderInfoSql(yhlsh, yylsh, sjczzt, czsj);

                listSql.Clear();
                listSql.Add(lsStr);

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, lsStr);

                DbHelperOra.ExecuteSqlTran(listSql, connection);


                string _brid = "";
                sqlStr = _builder.CurOrderBridSql(yylsh);
                dr = DbHelperOra.ExecuteReader(sqlStr, connection);
                if (dr.Read())
                {
                    if (dr.HasRows)
                    {
                        _brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                    }
                }

                string logSuccessMsg = " brid = '" + _brid + "', czsj = to_date('" +
                czsj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), yhlsh = '" + yhlsh +
                "' where yylsh = '" + yylsh + "' 。";
                UtilLog.GetInstance().WriteOrderLog("手机订单支付成功: ", logSuccessMsg);

                msg = "手机订单支付成功";
                return 0;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);

                string logMsg = msg + "' 。";

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
        /// 取消订单
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_CancelOrder(long yylsh, out string msg)
        {
            msg = "";
            long _sjczzt = -99;
            string _ztmc = "";
            double _czje = 0;

            int phoneOrderStatus = DB_StatusSjOrder(yylsh, out _sjczzt, out _ztmc, out _czje, out msg);
            if (phoneOrderStatus == 1)
            {
                msg = "手机订单已完成，无法被修改";
                return 6;
            }
            else if (phoneOrderStatus == -1)
            {
                msg = "手机订单已作废，无法被修改";
                return 6;
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
                ret = 0;

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
        /// 订单状态查询
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="ddzt">订单状态</param>
        /// <param name="ztmc">状态名称</param>
        /// <param name="czje">充值金额</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_StatusOrder(long yylsh, out long ddzt, out string ztmc, out double czje, out string msg)
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
                                ret = -1;
                                break;
                            case 0:
                                ztmc = "订单已经生成";
                                ret = 0;
                                break;
                            case 1:
                                ztmc = "订单已经完成";
                                ret = 1;
                                break;
                            case 2:
                                ztmc = "订单已经充值";
                                ret = 2;
                                break;
                            default:
                                ztmc = "未知的订单状态";
                                ret = 9;
                                break;
                        }
                    }
                    else
                    {
                        msg = "没有找到订单，请核实";
                        ret = 6;
                    }
                }
                else
                {
                    msg = "没有找到订单，请核实";
                    ret = 3;
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
        /// 订单手机充值状态查询
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <param name="ddzt">订单状态</param>
        /// <param name="ztmc">状态名称</param>
        /// <param name="czje">充值金额</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_StatusSjOrder(long yylsh, out long sjczzt, out string ztmc, out double czje, out string msg)
        {
            msg = "";
            sjczzt = -1;
            ztmc = "";
            czje = 0;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                int ret = -99;

                string findId = _builder.QueryOrderSJStatusSql(yylsh);
                dr = DbHelperOra.ExecuteReader(findId, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        sjczzt = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                        czje = !dr.IsDBNull(1) ? dr.GetDouble(1) : 0;
                        switch (sjczzt)
                        {
                            case -1:
                                ztmc = "订单已经作废";
                                ret = -1;
                                break;
                            case 0:
                                ztmc = "订单已经生成";
                                ret = 0;
                                break;
                            case 1:
                                ztmc = "手机端成功支付";
                                ret = 1;
                                break;
                            case 2:
                                ztmc = "手机端支付失败";
                                ret = 2;
                                break;
                            case 3:
                                ztmc = "手机用户取消";
                                ret = 3;
                                break;
                            default:
                                ztmc = "未知状态";
                                ret = -99;
                                break;
                        }
                    }
                    else
                    {
                        msg = "订单数据无法读取";
                        ret = 7;
                    }
                }
                else
                {
                    msg = "没有找到订单";
                    ret = 8;
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
        /// 查询账户余额
        /// </summary>
        /// <param name="curBrid">病人ID</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="info">余额信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryBalance(string brid, string brlx, out BalanceInfo info, out string msg)
        {
            msg = "";
            info = new BalanceInfo();
            int ret = -1;

            string curBrid = brid;
            if (AppUtils.HOSPITALNAME.WZSDEYY == WebConfigParameter.HospitalName() && brlx.Equals("2"))
            {
                ret = ConvertZYHTOBRIDForWZSDEYY(curBrid, out curBrid, out msg);
                if (0 != ret)
                {
                    return ret;
                }
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;

                string findId = _builder.GetBalanceSql(curBrid, brlx, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findId);

                if (!_flag)
                {
                    return 10;
                }


                dr = DbHelperOra.ExecuteReader(findId, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        info.zhye = !dr.IsDBNull(0) ? dr.GetDouble(0) : 0;

                        ret = 0;
                    }
                    else
                    {
                        msg = "信息找到，但未找到余额信息";
                        ret = 2;
                    }
                }
                else
                {
                    msg = "信息没找到，确认卡号信息正确";
                    ret = 3;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = ex.StackTrace;
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
        /// 查询账户明细
        /// </summary>
        /// <param name="curBrid">病人ID</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="querytype">分页参数 1：不分页 2分页查询</param>
        /// <param name="pageno">所在页数</param>
        /// <param name="pagenum">每页显示条数</param>
        /// <param name="values">账户变更记录</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_AccountRecordList(string brid, string brlx, string querytype, int pageno, int pagenum,
           out ArrayList values, out string msg)
        {
            //select yhmc, cssj, czje from (select a.*,ROWNUM rn from(A_YL_DD) a where ROWNUM<=(11) and  brid =  '10160915' and brlx = '2'  order by cssj desc ) where rn > 8
            int ret = -1;
            values = null;
            msg = "";

            //string curBrid = brid;
            //if (AppUtils.HOSPITALNAME.WZSDEYY == WebConfigParameter.HospitalName() && brlx.Equals("2"))
            //{
            //    ret = ConvertZYHTOBRIDForWZSDEYY(curBrid, out curBrid, out msg);
            //    if (0 != ret)
            //    {
            //        return ret;
            //    }
            //}

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                long maxrow = 0;
                long minrow = 0;
                General.CalculatePage(pageno, pagenum, out maxrow, out minrow);

                bool _flag = false;

                string findId = _builder.GetAccountListSql(brid, brlx, querytype, maxrow, minrow, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findId);

                dr = DbHelperOra.ExecuteReader(findId, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        AccountInfo pd = new AccountInfo();
                        pd.jkje = !dr.IsDBNull(0) ? dr.GetDouble(0) : 0;
                        pd.jkrq = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString("yyyy-MM-dd HH:mm") : DateTime.MinValue.ToString("yyyy-MM-dd HH:mm");
                        pd.zffs = !dr.IsDBNull(2) ? dr.GetString(2) : "";

                        values.Add(pd);
                    }
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
                if (null != connection && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return ret;
        }

        /// <summary>
        /// 商户通知
        /// </summary>       
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_InsertCcb(YLReplyInfo info, out string msg)
        {
            UtilLog.GetInstance().WriteOrderLog("Order: ", info.YLInfoToString());

            msg = "";

            DateTime jysj;
            if (DB_Sysdate(out jysj) == false)
            {
                msg = "读取系统时间出错";
                return -1;
            }

            int ret = -1;

            try
            {
                //记录银联信息到数据表
                ret = LogYLRetInfo(info, jysj, out msg);
                if (ret != 0)
                {
                    return ret;
                }

                if (!info.respCode.Equals("00"))
                {
                    msg = "银联返回充值不成功";
                    return 0;
                }

                //判断订单是否存在
                double czje = 0;
                string brid = null, brlx = null;
                ret = ExistOrder(info, out brid, out brlx, out czje, out msg);
                if (ret != 0)
                {
                    return ret;
                }

                //充值金额是否正常
                ret = ValidAmount(info, czje, out  msg);
                if (ret != 0)
                {
                    return ret;
                }

                //更新HIS
                ret = DB_InsertCcbForHis(info, brid, brlx, czje, jysj, out msg);
                return ret;

            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                return -1;
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
        private int ExistOrder(YLReplyInfo info, out string brid, out string brlx, out double czje, out string msg)
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
                string findId = _builder.QueryOrderInfoSql(info.yylsh);
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
                    ret = 3;
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
        private int ValidAmount(YLReplyInfo info, double czje, out string msg)
        {
            int ret = -1;
            msg = "";

            if (czje <= 0 || czje > 10000 || (Convert.ToDouble(info.txnAmt) / 100 != czje))
            {
                msg = "充值金额异常";
                ret = 2;
            }
            else
            {
                ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// 记录银联返回信息到数据库
        /// </summary>
        /// <param name="info"></param>
        /// <param name="jysj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int LogYLRetInfo(YLReplyInfo info, DateTime jysj, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;

            long count = 0;
            try
            {
                string sql = _builder.ShtzCountSql(info.yylsh);
                dr = DbHelperOra.ExecuteReader(sql, connection);

                if (dr.Read())
                {
                    count = !dr.IsDBNull(0) ? dr.GetInt64(0) : 0;
                }

                if (count > 0)
                {
                    msg = "已经交易记录,无需插入";
                    ret = 1;
                    return ret;
                }

                //记录银联返回记录
                ret = WriteYLRetInfoToDB(info, jysj, out msg);
                if (ret != 0)
                {
                    msg += "记录日志失败";
                    ret = 1;
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
        /// 查询交易明细
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="values">明细记录</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryDetail(string brid, string brlx, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            return -1;
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
        /// 更新HIS充值记录
        /// </summary>
        /// <param name="yhlsh">银行流水号</param>
        /// <param name="je">金额</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <param name="msg">成功、错处或异常信息</param>
        /// <returns>0-成功   大于0-失败   小于0-异常</returns>
        private int DB_UpdateHisCharge(OracleConnection connection, OracleTransaction tx,
            string yhlsh, double je, string brid, string brlx,
            DateTime jysj, out string msg)
        {
            int ret = -1;
            msg = "";

            if (brlx.Equals("1"))
            {
                //TODO  更新数据表里面的门诊病人的充值记录
                ret = 0;

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
                parameters[0].Value = brid + "|APP|" + Convert.ToString(je) + "|10|" + jysj.ToString() + "||||0||";
                parameters[0].Direction = ParameterDirection.Input;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;

                //PKG_GY_YINYIJK.PRC_ZY_YUJIAOKUAN
                int result = -99;
                OracleDataReader dataReader = DbHelperOra.ExeProcedureUseTran(connection, tx,
                    WebConfigParameter.ProcedureName,
                    parameters, out result);

                if (result != 0)
                {
                    msg = "插入医院HIS表数据出错。";
                    ret = 1;
                }
                else
                {
                    msg = "插入医院HIS成功。";
                    ret = 0;
                }
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


        private int DB_InsertCcbForHis(YLReplyInfo info, string brid, string brlx, double czje, DateTime jysj, out string msg)
        {
            int ret = -1;

            OracleConnection conn = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OracleTransaction tx = conn.BeginTransaction();

            try
            {
                double _zhye = 0;

                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY)
                {
                    _zhye = GetHisZhye(brid, brlx, out msg);

                    if (_zhye < 0)
                    {
                        msg = "账户金额异常";
                        return 1;
                    }
                    _zhye += czje;
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY)
                {
                    _zhye = GetHisZhye(brid, brlx, out msg);

                    if (_zhye < 0)
                    {
                        msg = "账户金额异常";
                        return 1;
                    }
                    _zhye += czje;
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY)
                {
                    _zhye = GetHisZhye(brid, brlx, out msg);

                    if (_zhye < 0)
                    {
                        msg = "账户金额异常";
                        return 1;
                    }
                    _zhye += czje;
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
                {
                    _zhye = GetHisZhye(brid, brlx, out msg);

                    if (_zhye < 0)
                    {
                        msg = "账户金额异常";
                        return 1;
                    }
                    _zhye += czje;
                }

                ArrayList listSql = new ArrayList();

                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY)
                {
                    string updateZhyeSql = _builder.UpdateBalanceSql(czje, brid, brlx);
                    listSql.Add(updateZhyeSql);

                    string updateDqzSql = _builder.UpdateDqzSql();
                    //"update gy_identity  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
                    listSql.Add(updateDqzSql);

                    string _pzhm = "ZH" + info.yylsh;
                    string updateHisSql = _builder.UpdateHisSql(brid, _zhye, czje, jysj, _pzhm, info);
                    //"insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq, jzrq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
                    //   " '" + brid + "', '智慧充值', " + _zhye + ",  " + czje + ",  0 , " +
                    //   " 'ZH001', " + " '28', '" + _pzhm + "',  to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), " +
                    //"to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";

                    listSql.Add(updateHisSql);
                    string updateDdmxcode = _builder.UpdateDdmxcodeSql(info.yylsh);
                    listSql.Add(updateDdmxcode);
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY)
                {
                    string updateZhyeSql = _builder.UpdateBalanceSql(czje, brid, brlx);
                    listSql.Add(updateZhyeSql);

                    string updateDqzSql = _builder.UpdateDqzSql();
                    //"update gy_identity  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
                    listSql.Add(updateDqzSql);

                    string _pzhm = "ZH" + info.queryId;
                    string updateHisSql = _builder.UpdateHisSql(brid, _zhye, czje, jysj, _pzhm, info);
                    //"insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
                    //   " '" + brid + "', '智慧充值', " + _zhye + ",  " + czje + ",  0 , " +
                    //   " 'ZH001', " + " '28', '" + _pzhm + "',  to_date('" +
                    //   jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
                    listSql.Add(updateHisSql);
                    string updateDdmxcode = _builder.UpdateDdmxcodeSql(info.yylsh);
                    listSql.Add(updateDdmxcode);
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY)
                {
                    string updateZhyeSql = _builder.UpdateBalanceSql(czje, brid, brlx);

                    listSql.Add(updateZhyeSql);

                    string updateDqzSql = _builder.UpdateDqzSql();
                    //"update gy_identity  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
                    listSql.Add(updateDqzSql);

                    string _pzhm = "ZH" + info.yylsh;
                    string updateHisSql = _builder.UpdateHisSql(brid, _zhye, czje, jysj, _pzhm, info);
                    //"insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
                    //   " '" + brid + "', '智慧充值', " + _zhye + ",  " + czje + ",  0 , " +
                    //   " 'ZH001', " + " '80', '" + _pzhm + "',  to_date('" +
                    //   jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
                    listSql.Add(updateHisSql);
                    string updateDdmxcode = _builder.UpdateDdmxcodeSql(info.yylsh);
                    listSql.Add(updateDdmxcode);
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
                {
                    long _pzxh = DB_GetMaxForWZSDSRMYY("ZZJ_JKHM", 1);
                    if (_pzxh < 0)
                    {
                        msg = "获得ZJJ_JKHM的主键出错";
                        return -2;
                    }

                    string updateZhyeSql = _builder.UpdateBalanceSql(czje, brid, brlx);
                    listSql.Add(updateZhyeSql);

                    string updateDqzSql = _builder.UpdateDqzSql();
                    //"update gy_identity  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
                    listSql.Add(updateDqzSql);

                    string _pzhm = "YL" + _pzxh.ToString();
                    string updateHisSql = _builder.UpdateHisSql(brid, _zhye, czje, jysj, _pzhm, info);
                    //   "insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
                    //  " '" + brid + "', '智慧充值', " + _zhye + ",  " + czje + ",  0 , " +
                    //   " 'ZH001', " + " '80', '" + _pzhm + "',  to_date('" +
                    //   jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
                    listSql.Add(updateHisSql);
                    string updateDdmxcode = _builder.UpdateDdmxcodeSql(info.yylsh);
                    listSql.Add(updateDdmxcode);
                }
                else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY)
                {
                    string updateZhyeSql = _builder.UpdateBalanceSql(czje, brid, brlx);
                    listSql.Add(updateZhyeSql);

                    string updateDqzSql = _builder.UpdateDqzSql();
                    //"update gy_identity  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
                    listSql.Add(updateDqzSql);

                    string _pzhm = "ZH" + info.yylsh;
                    string updateHisSql = _builder.UpdateHisSql(brid, _zhye, czje, jysj, _pzhm, info);
                    //"insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq, jzrq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
                    //   " '" + brid + "', '智慧充值', " + _zhye + ",  " + czje + ",  0 , " +
                    //   " 'ZH001', " + " '28', '" + _pzhm + "',  to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), " +
                    //"to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";

                    listSql.Add(updateHisSql);
                    string updateDdmxcode = _builder.UpdateDdmxcodeSql(info.yylsh);
                    listSql.Add(updateDdmxcode);
                }


                int sqlResult = -99;

                DbHelperOra.ExecuteSqlTran(conn, tx, listSql, out sqlResult, out msg);

                if (sqlResult != 0)
                {
                    tx.Rollback();
                    return 2;
                }

                //更新HIS表记录
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY)
                {
                    int otherResult = -99;

                    if (info.respCode.Equals("00"))
                    {
                        otherResult = DB_UpdateHisCharge(conn, tx, info.queryId, czje, brid, brlx, jysj, out msg);

                        if (otherResult != 0)
                        {
                            tx.Rollback();
                            msg += "HIS更新数据不成功。";
                            return 3;
                        }

                        msg = "充值成功。";
                        ret = 0;
                    }
                    else
                    {
                        msg = "银联返回失败";
                        ret = 3;
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
        /// 银联返回信息写入到数据库
        /// </summary>
        /// <param name="info"></param>
        /// <param name="jysj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int WriteYLRetInfoToDB(YLReplyInfo info, DateTime jysj, out string msg)
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
                string ylcode = "", ylmc = "";
                if (info.paytype == "unionpay")
                {
                    ylcode = (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY ?
                        "10005" : "01");
                    ylmc = "银联";
                }

                string insertSql = _builder.InsertSHTZSql(info, jysj);

                string updateStatus = _builder.UpdateDDStatusSql(info, ylcode, ylmc);

                ArrayList listSql = new ArrayList();
                int exeSql;
                listSql.Add(insertSql);
                listSql.Add(updateStatus);

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
        /// 获得当前HIS账户的余额
        /// </summary>
        /// <param name="brid"></param>
        /// <returns></returns>
        private double GetHisZhye(string brid, string brlx, out string msg)
        {
            double result = 0;
            msg = "";

            try
            {
                OracleDataReader dr = null;

                OracleConnection selectCon = new OracleConnection(WebConfigParameter.ConnectionHisString);
                if (selectCon.State != ConnectionState.Open)
                {
                    selectCon.Open();
                }
                string curYeSql = "";
                bool flag = false;
                curYeSql = _builder.GetBalanceSql(brid, brlx, out flag, out msg);

                if (!flag)
                {
                    msg = "帐户金额异常";
                    return -1;
                }

                dr = DbHelperOra.ExecuteReader(curYeSql, selectCon);
                if (dr.Read())
                {
                    result = !dr.IsDBNull(0) ? dr.GetDouble(0) : 0;
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="needcount"></param>
        /// <returns></returns>
        public long DB_GetMaxForWZSDSRMYY(string tablename, int needcount)
        {
            long dqz = -1;

            if (needcount < 1)
            {
                return -1;
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                string updateSql = "UPDATE GY_IDENTITY SET DQZ = DQZ + " + needcount + " WHERE BMC = '" + tablename + "'";
                ArrayList listSql = new ArrayList();
                listSql.Add(updateSql);
                DbHelperOra.ExecuteSqlTran(listSql, connection);

                string getdqz = "SELECT DQZ FROM GY_IDENTITY WHERE BMC = '" + tablename + "'";
                dr = DbHelperOra.ExecuteReader(getdqz, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        dqz = !dr.IsDBNull(0) ? dr.GetInt64(0) : -1;
                        return dqz;
                    }
                    else
                    {
                        return -1;

                    }
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
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// 是否支持充值功能
        /// </summary>
        /// <returns></returns>
        private bool IsSupportPayment(string brlx)
        {
            bool result = false;

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY)
            {
                result = true;
            }
            else if (brlx.Equals("1") &&
                  (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY ||
                  WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY ||
                  WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY))
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
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
            {
                yylshSeq = DB_GetMaxForWZSDSRMYY("YL_DDJL", 1);
            }
            else if (DB_Get_Yylshmax(1, out yylshSeq) == false)
            {
                yylshSeq = -1;
            }

            return yylshSeq;
        }

        /// <summary>
        /// 转化住院号成brid  
        /// </summary>
        private int ConvertZYHTOBRIDForWZSDEYY(string zyh, out string brid, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            brid = "";
            msg = "";
            int ret = -1;

            try
            {
                string sql = "select ms.brid from ms_brda ms, zy_brry zy, gy_ckda gy where zy.mzhm = ms.mzhm and zy.brxm = ms.brxm and gy.zfpb = 0" +
                    " and zy.mzhm is not null and gy.ckhm = zy.knxx and zyh = '" + zyh + "'";
                dr = DbHelperOra.ExecuteReader(sql, connection);

                int count = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        count++;
                        brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                    }

                    if (!string.IsNullOrEmpty(brid) && 1 == count)
                    {
                        ret = 0;
                    }
                    else
                    {
                        msg = "没有找到病人的充值账户或找到多个充值账户";
                        ret = 13;
                    }

                    return ret;
                }
                else
                {
                    msg = "病人信息无效或者病人已经出院";
                    return 6;
                }
            }
            catch (Exception e)
            {
                msg = GetExceptionInfo(e);
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
        /// 塘下医院通过绑卡号码获得BRID
        /// </summary>
        /// <param name="brlx"></param>
        /// <param name="bkhm"></param>
        /// <param name="brid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int ConvertTOBRIDForWZSTXRMYY(string bkhm, out string brid, out string msg)
        {
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            brid = "";
            msg = "";
            int ret = -1;

            try
            {
                string sql = "select brid from ms_brzh where brkh='"+bkhm+"'";
                    
                dr = DbHelperOra.ExecuteReader(sql, connection);

                int count = 0;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        count++;
                        brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                    }

                    if (!string.IsNullOrEmpty(brid) && 1 == count)
                    {
                        ret = 0;
                    }
                    else
                    {
                        msg = "没有找到病人的充值账户或找到多个充值账户";
                        ret = 13;
                    }

                    return ret;
                }
                else
                {
                    msg = "病人信息无效或者病人已经出院";
                    return 6;
                }
            }
            catch (Exception e)
            {
                msg = GetExceptionInfo(e);
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