using System;

using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;

namespace HospitaPaymentService.Wzszhjk.Utils.SqlString
{
    /// <summary>
    /// 生成sql语句的类
    /// </summary>
    public class BuilderSql
    {
        private SQLUtils _sqlUtils;
        public BuilderSql()
        {
            SqlFactory factory = new SqlFactory();
            _sqlUtils = factory.Creat();
        }

        /// <summary>
        /// 获取查询卡信息的SQL
        /// </summary>
        /// <param name="brxm"></param>
        /// <param name="sfzh"></param>
        /// <param name="brlx"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetQueryCardSql(string brxm, string sfzh, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = brxm;
            _paramters[1] = sfzh;

            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrQueryStr, _paramters);
            }
            else if (brlx.Equals("2"))
            {
                result = string.Format(_sqlUtils.zybrQueryStr, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;

        }

        /// <summary>
        /// 获得绑卡的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="sfzh"></param>
        /// <param name="brxm"></param>
        /// <param name="bklx"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string GetBingCardSql(string brid, string sfzh, string brxm, string bklx, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[4];
            _paramters[0] = brid;
            _paramters[1] = sfzh;
            _paramters[2] = brxm;
            _paramters[3] = bklx;

            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrBindCardstr, _paramters);
            }
            else if (brlx.Equals("2"))
            {
                result = string.Format(_sqlUtils.zybrBindCardstr, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得卡是否有效的SQL
        /// </summary>
        /// <param name="brhm"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string GetBridByBkhmSql(string bkhm, string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = bkhm;
            _paramters[1] = brid;


            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrValidBkhm, _paramters);
            }
            else if (brlx.Equals("2"))
            {
                result = string.Format(_sqlUtils.zybrValidBkhm, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得门诊卡是否有效的SQL
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="cardno"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        public string mzGetBridByBkhmSql(string openid, string cardno, string patient, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[3];
            _paramters[0] = openid;
            _paramters[1] = cardno;
            _paramters[2] = patient;

            result = string.Format(_sqlUtils.mzbrValidBkhm, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得创建订单时用来查询信息的SQL
        /// </summary>
        /// <returns></returns>
        public string QueryInfoForCreateOrderSql(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = brid;


            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrForCreateOrder, _paramters);
            }
            else if (brlx.Equals("2"))
            {
                result = string.Format(_sqlUtils.zybrForCreateOrder, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得创建门诊预存订单时用来查询信息的SQL
        /// </summary>
        /// <returns></returns>
        public string mzQueryInfoForCreateOrderSql(string openid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = openid;

            result = string.Format(_sqlUtils.mzbrForCreateOrder, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询余额的sql
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string GetBalanceSql(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = brid;

            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrQueryBalance, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.zybrQueryBalance, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }


        /// <summary>
        /// 获得查询brid是否有效的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string GetSqlValidBrid(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = brid;


            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrValidId, _paramters);
            }
            else if (brlx.Equals("2"))
            {
                result = string.Format(_sqlUtils.zybrValidId, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }


        /// <summary>
        /// 获得查询报告单列表的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="brxm"></param>
        /// <returns></returns>
        public string GetSqlReportList(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = brid;

            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrReportList, _paramters);
            }
            else if (brlx.Equals("2"))
            {

                result = string.Format(_sqlUtils.zybrReportList, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询报告单详细的SQL
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lx"></param>
        /// <param name="brxm"></param>
        /// <returns></returns>
        public string GetSqlReportDetail(string code, string lx, string brxm, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = code;
            _paramters[1] = brxm;

            if (lx.Equals("1"))
            {
                result = string.Format(_sqlUtils.reportDetailByBgdh, _paramters);
            }
            else if (lx.Equals("2"))
            {
                result = string.Format(_sqlUtils.reportDetailBySbm, _paramters);
            }

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询报告单列表的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="brxm"></param>
        /// <returns></returns>
        public string GetSqlReportJCList(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = brid;

            if (brlx.Equals("1"))
            {
                result = string.Format(_sqlUtils.mzbrReportJC, _paramters);
            }
            else if (brlx.Equals("2"))
            {

                result = string.Format(_sqlUtils.zybrReportJC, _paramters);
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询报告单详细的SQL
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lx"></param>
        /// <param name="brxm"></param>
        /// <returns></returns>
        public string GetSqlReportJCDetail(string code, string lx, string brxm, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = code;
            _paramters[1] = brxm;

            if (lx.Equals("1"))
            {
                result = string.Format(_sqlUtils.reportJCDetailByBgdh, _paramters);
            }
            else if (lx.Equals("2"))
            {
                result = string.Format(_sqlUtils.reportJCDetailBySbm, _paramters);
            }

            flag = HasAction(result, out msg);

            return result;
        }


        /// <summary>
        /// 获得查询报告单详细的SQL
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lx"></param>
        /// <returns></returns>
        public string GetSqlRemaindBeds(out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = _sqlUtils.remaindBeds;

            flag = HasAction(result, out msg);

            return result;
        }

        public string GetReportDetailXM(string bgdh, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = bgdh;
            result = string.Format(_sqlUtils.reportDetailMX, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        public string GetReportJCDetailXM(string bgdh, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = bgdh;
            result = string.Format(_sqlUtils.reportJCDetailMX, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得根据拼音代码查询药品信息的SQL
        /// </summary>
        /// <param name="pydm"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetMedicineByPy(string pydm, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = pydm;
            result = string.Format(_sqlUtils.medicineByPydm, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 分页查询药品信息
        /// </summary>
        /// <param name="maxrow"></param>
        /// <param name="minrow"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetMedicinePage(long maxrow, long minrow, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            object[] _paramters = new object[2];
            _paramters[0] = maxrow;
            _paramters[1] = minrow;

            result = string.Format(_sqlUtils.medicinePage, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得根据拼音代码收费信息的SQL
        /// </summary>
        /// <param name="pydm"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetChargeByPy(string pydm, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = pydm;
            result = string.Format(_sqlUtils.chargeByPydm, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 分页查询收费信息
        /// </summary>
        /// <param name="maxrow"></param>
        /// <param name="minrow"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetChargePage(long maxrow, long minrow, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            object[] _paramters = new object[2];
            _paramters[0] = maxrow;
            _paramters[1] = minrow;

            result = string.Format(_sqlUtils.chargePage, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询账户变更明细的SQL 
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="querytype"></param>
        /// <param name="maxrow"></param>
        /// <param name="minrow"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetAccountListSql(string brid, string brlx, string querytype, long maxrow, long minrow,
            out bool flag, out string msg)
        {
            msg = "";

            string result = "";
            object[] _paramters = new object[3];
            _paramters[0] = brid;
            _paramters[1] = maxrow;
            _paramters[2] = minrow;

            if (brlx.Equals("1"))
            {
                if (querytype.Equals("1"))
                {
                    result = string.Format(_sqlUtils.accountListMz, _paramters);
                }
                else if (querytype.Equals("2"))
                {
                    result = string.Format(_sqlUtils.accountListMzByPage, _paramters);
                }
            }
            else if (brlx.Equals("2"))
            {
                if (querytype.Equals("1"))
                {
                    result = string.Format(_sqlUtils.accountListZy, _paramters);
                }
                else if (querytype.Equals("2"))
                {
                    result = string.Format(_sqlUtils.accountListZyByPage, _paramters);
                }
            }


            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得系统时间的SQL
        /// </summary>
        /// <returns></returns>
        public string SysdateSql()
        {
            string result = "select sysdate from dual";

            return result;
        }

        /// <summary>
        /// 获得订单列表的sql
        /// </summary>
        /// <param name="type">支付类型 1：返回未支付的订单 2：返回已经支付的订单</param>
        /// <param name="pageno"></param>
        /// <param name="pagenum"></param>
        /// <param name="querytype">查询类型 1：不分页查询 2：分页查询</param>
        /// <param name="brlx"></param>
        /// <param name="brid"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string OrderListSql(string type, int pageno, int pagenum, string querytype, string brlx, string brid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string sjzt = "";
            if (type.Equals("1"))
            {
                sjzt = " and sjczzt != '" + 1 + "'";
            }
            else
            {
                sjzt = " and sjczzt = '" + 1 + "'";
            }

            long maxrow, minrow;
            General.CalculatePage(pageno, pagenum, out maxrow, out minrow);

            string tableName = YlddTableName();

            result = "select yylsh, yhlsh, yhmc, cssj, czje, ddzt, sjczzt, paytype from " + tableName + " a where brid = '" + brid + "' and brlx = '" +
                brlx + "'" + sjzt + " order by cssj desc";
            if (querytype.Equals("2"))
            {
                result = "select yylsh, yhlsh,yhmc, cssj, czje, ddzt, sjczzt, paytype  from (select a.*,ROWNUM rn from(select * from " + tableName + " order  by cssj desc) a where  brid =  '" +
                    brid + "' and brlx = '" + brlx + "' " + sjzt + " and ROWNUM <= " +
                    maxrow + " ) where rn >= "
                    + minrow;
            }

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得支付宝服务窗订单列表的sql
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageno"></param>
        /// <param name="pagenum"></param>
        /// <param name="querytype"></param>
        /// <param name="brlx"></param>
        /// <param name="brid"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string OrderListForAlipaySql(string openid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "";
            string tableName = YlddTableName();

            //result = "select yylsh, yhlsh, yhmc, cssj, czje, ddzt, sjczzt, paytype from " + tableName + " a where brid = '" + brid + "' and brlx = '" +
            //    brlx + "'" + sjzt + " order by cssj desc";
            result = "select openid, yhlsh, yylsh, ddzt, paytype, czje, cssj, czsj, czsj, brid, brxm, sfzh from " + tableName + " t where openid = '" + openid +
                "' and brlx = '" + brlx + "' order by cssj desc";

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 创建订单的sql
        /// </summary>
        /// <param name="yylsh"></param>
        /// <param name="czje"></param>
        /// <param name="tkje"></param>
        /// <param name="brlx"></param>
        /// <param name="bkhm"></param>
        /// <param name="brid"></param>
        /// <param name="cssj"></param>
        /// <param name="sfzh"></param>
        /// <param name="lxdh"></param>
        /// <param name="brxm"></param>
        /// <returns></returns>
        public string CreateOrderSql(long yylsh, double czje, double tkje, string brlx, string bkhm, string brid, DateTime cssj,
            string sfzh, string lxdh, string brxm, string payType)
        {
            string result = "insert into " + YlddTableName() + "(yylsh, czje, tkje, ddzt, jyfs, bkhm, brid, cssj, czsj, brlx, brxm, sfzh, lxdh, paytype) values( '" +
                    yylsh + "', " + czje + ", " + tkje + ", 0, '" + brlx + "', '" + bkhm + "', '" + brid + "', " + "to_date('" +
                    cssj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')," +
                    "to_date('" + cssj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), '" + brlx +
                    "', '" + brxm + "', '" + sfzh + "', '" + lxdh + "', '" + payType + "')";

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
            {
                result = "insert into " + YlddTableName() + "(yylsh, czje, tkje, ddzt, jyfs, bkhm, brid, cssj, czsj, brlx, brxm, sfzh, lxdh, ddlx, paytype) values( '" +
                    yylsh + "', " + czje + ", " + tkje + ", 0, '" + brlx + "', '" + bkhm + "', '" + brid + "', " + "to_date('" +
                    cssj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')," +
                    "to_date('" + cssj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), '" + brlx +
                    "', '" + brxm + "', '" + sfzh + "', '" + lxdh + "', '1', '" + payType + "')";
            }

            return result;
        }

        /// <summary>
        /// 创建支付宝服务窗订单的sql
        /// </summary>
        /// <param name="yylsh"></param>
        /// <param name="czje"></param>
        /// <param name="tkje"></param>
        /// <param name="brlx"></param>
        /// <param name="bkhm"></param>
        /// <param name="brid"></param>
        /// <param name="cssj"></param>
        /// <param name="sfzh"></param>
        /// <param name="lxdh"></param>
        /// <param name="brxm"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string CreateOrderforAlipaySql(long yylsh, double czje, double tkje, string brlx, string bkhm, string brid, DateTime cssj, string sfzh, string lxdh, string brxm, string payType, string openid, string shdzid, string jzid, string qjfs)
        {
            string result = "insert into " + YlddTableName() + "(yylsh, czje, tkje, ddzt, jyfs, bkhm, brid, cssj, czsj, brlx, brxm, sfzh, lxdh, paytype, openid,shdzid,jzid,qjfs) values( '" +
                    yylsh + "', " + czje + ", " + tkje + ", 0, '" + brlx + "', '" + bkhm + "', '" + brid + "', " + "to_date('" +
                    cssj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')," +
                    "to_date('" + cssj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), '" + brlx +
                    "', '" + brxm + "', '" + sfzh + "', '" + lxdh + "', '" + payType + "', '" + openid + "','" + shdzid + "','" + jzid + "','" + qjfs + "')";

            return result;
        }

        /// <summary>
        /// 查询订单数的sql
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string QueryOrderCountSql(long yylsh, string payType)
        {
            string result = "select count(yylsh) from " + YlddTableName() + " where yylsh = '" +
                yylsh + "' and paytype = '" + payType + "'";
            return result;
        }

        /// <summary>
        /// 更新订单信息的sql
        /// </summary>
        /// <param name="yhlsh"></param>
        /// <param name="yylsh"></param>
        /// <param name="sjczzt"></param>
        /// <param name="czsj"></param>
        /// <returns></returns>
        public string UpdateOrderInfoSql(string yhlsh, long yylsh, string sjczzt, DateTime czsj)
        {
            string result = "update " + YlddTableName() + " set  yhlsh = '" +
                yhlsh + "', czsj = to_date('" +
                czsj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), sjczzt = '" + sjczzt + "' where yylsh = '" +
                yylsh + "'";

            return result;
        }

        /// <summary>
        /// 查询订单状态的sql
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string QueryOrderStatusSql(long yylsh)
        {
            string result = "select ddzt,czje from a_yl_dd where yylsh = '" + yylsh + "'";
            return result;
        }

        /// <summary>
        /// 查询手机充值状态的sql
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string QueryOrderSJStatusSql(long yylsh)
        {
            string result = "select sjczzt,czje from a_yl_dd where yylsh = '" + yylsh + "'";
            return result;
        }

        /// <summary>
        /// 查询当前订单记录的病人id（通过yylsh）
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string CurOrderBridSql(long yylsh)
        {
            string result = "select brid  from " + YlddTableName() + " where yylsh = '" + yylsh + "'";
            return result;
        }

        /// <summary>
        /// 取消订单的sql（通过yylsh）
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string CancelOrderSql(long yylsh)
        {
            string result = "update " + YlddTableName() + " set ddzt = -1 where yylsh = '" + yylsh + "'";
            return result;
        }

        /// <summary>
        /// 查询商户通知表记录数的sql（通过yylsh）
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string ShtzCountSql(long yylsh)
        {
            string result = "select count(yylsh) from " + YhjlTableName() + " where yylsh = '" + yylsh + "'";
            return result;
        }

        /// <summary>
        /// 查询支付宝商户通知表记录数的sql（通过tradeno）
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string AlipayShtzCountSql(long tradeno)
        {
            string result = "select count(tradeno) from zfb_shtz where tradeno = '" + tradeno + "'";
            return result;
        }

        /// <summary>
        /// 通过yylsh查询订单信息的SQL 
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string QueryOrderInfoSql(long yylsh)
        {
            string result = "select brid, czje, brlx from " + YlddTableName() + " where yylsh =  '" + yylsh + "'";
            return result;
        }

        /// <summary>
        /// 获取医院流水号的sql
        /// </summary>
        /// <returns></returns>
        public string YylshSql()
        {
            string result = "select seq_a_yl_yylsh.nextval from dual";
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
            {
                result = "SELECT DQZ + 1 FROM GY_IDENTITY WHERE BMC = 'YL_DDJL'";
            }
            return result;
        }

        /// <summary>
        /// 更新HIS表的余额的sql
        /// </summary>
        /// <param name="czje"></param>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string UpdateBalanceSql(double czje, string brid, string brlx)
        {
            string result = "";
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZXYJHYY)
            {
                result = "update gy_ckda set zhye=zhye+ " + czje + "  where id= " + brid + " and zfpb=0";

            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY)
            {

                result = "update ms_brzh set zhye=zhye+ " + czje + "  where brid= " + brid + " and zfpb=0";
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY)
            {

                //if (brlx.Equals("1"))
                //{
                result = "update gy_ckda set zhye=zhye+ " + czje + "  where id= " + brid + " and zfpb=0";
                //}
                //else if (brlx.Equals("2"))
                //{
                //    result = "update gy_ckda gy set gy.zhye= gy.zhye + " + czje + "  where id= " +
                //        "(select ms.brid from ms_brda ms, zy_brry zy where zy.mzhm = ms.mzhm and zy.brxm = ms.brxm " +
                //        " and zy.zyh = '" + brid + "') and zfpb=0";
                //}
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
            {
                result = "update gy_ckda set zhye=zhye+ " + czje + "  where id= " + brid + " and zfpb=0";
            }
            else if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY)
            {
                result = "update ms_brzh set zhye=zhye+ " + czje + "  where brid= " + brid + " and zfpb=0";
            }

            return result;
        }

        /// <summary>
        /// 更新MS_SZMX表的最大值
        /// </summary>
        /// <returns></returns>
        public string UpdateDqzSql()
        {
            string result = "update gy_identity  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY ||
                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY)
            {
                result = "update gy_identity_ms  set DQZ=DQZ+ 1 WHERE bmc='MS_SZMX'";
            }
            return result;
        }

        /// <summary>
        /// 医院更新his明细记录的sql
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="zhye"></param>
        /// <param name="czje"></param>
        /// <param name="jysj"></param>
        /// <param name="pzhm"></param>
        /// <returns></returns>
        public string UpdateHisSql(string brid, double zhye, double czje, DateTime jysj, string pzhm, YLReplyInfo info)
        {
            AppUtils.HOSPITALNAME hospitalName = WebConfigParameter.HospitalName();
            string result = "insert into MS_SZMX ( "
                            + "sbxh, "
                            + "brid, "
                            + "zy, "
                            + "zhye, "
                            + "jfje, "
                            + "dfje, "
                            + "czgh, "
                            + "szlb, "
                            + "pzhm, "
                            + "rq";
            // 多一个日期
            if (hospitalName == AppUtils.HOSPITALNAME.WZSZXYJHYY || hospitalName == AppUtils.HOSPITALNAME.WZSDSRMYY || hospitalName == AppUtils.HOSPITALNAME.WZSDEYY)
            {
                result += ", jzrq";
            }
            if (hospitalName == AppUtils.HOSPITALNAME.WZSTXRMYY)
            {
                result += ") values((select dqz from gy_identity_ms  where bmc='MS_SZMX'), ";
            }
            else
            {
                result += ") values((select dqz from gy_identity  where bmc='MS_SZMX'), ";
            }
            result += " '" + brid + "',";
            // 标题名称的不同
            if (info.paytype == "alipay")
            {
                result += " '手机支付宝充值', ";
            }
            else
            {
                if (AppUtils.HOSPITALNAME.WZSZXYJHYY == hospitalName ||
                    AppUtils.HOSPITALNAME.WZSZYY == hospitalName)
                {
                    result += " '智慧充值', ";
                }
                else if (AppUtils.HOSPITALNAME.WZSDEYY == hospitalName)
                {
                    result += " '手机银联充值', ";
                }
                else if (AppUtils.HOSPITALNAME.WZSDSRMYY == hospitalName)
                {
                    result += " '网银追缴', ";
                }
                else
                {
                    result += " '手机银联充值', ";
                }
            }

            result += zhye + ", ";
            result += czje + ", ";
            result += " 0,";
            if (AppUtils.HOSPITALNAME.WZSDSRMYY == hospitalName)
            {
                result += " '0001000', ";
            }
            else
            {
                result += " 'ZH001', ";  //WZSZXYJHYY WZSZYY  WZSDEYY
            }
            if (AppUtils.HOSPITALNAME.WZSZXYJHYY == hospitalName || AppUtils.HOSPITALNAME.WZSZYY == hospitalName)
            {
                result += " '28', ";     //WZSZXYJHYY WZSZYY
            }
            else
            {
                result += " '80', "; // WZSDEYY  WZSDSRMYY
            }
            result += " '" + pzhm + "', ";
            result += " to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
            // 多出来的日期处理
            if (hospitalName == AppUtils.HOSPITALNAME.WZSZXYJHYY || hospitalName == AppUtils.HOSPITALNAME.WZSDSRMYY || hospitalName == AppUtils.HOSPITALNAME.WZSDEYY)
            {
                result += ", to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
            }
            result += ")";
            // string result = "";
            // switch (WebConfigParameter.HospitalName())
            // {
            //     case AppUtils.HOSPITALNAME.WZSZXYJHYY:
            //         result = "insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq, jzrq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
            //           " '" + brid + "', '智慧充值', " + zhye + ",  " + czje + ",  0 , " +
            //           " 'ZH001', " + " '28', '" + pzhm + "',  to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), " +
            //        "to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            //         break;
            //     case AppUtils.HOSPITALNAME.WZSZYY:
            //         result = "insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq) values((select dqz from gy_identity_ms  where bmc='MS_SZMX'), " +
            //           " '" + brid + "', '智慧充值', " + zhye + ",  " + czje + ",  0 , " +
            //           " 'ZH001', " + " '28', '" + pzhm + "',  to_date('" +
            //           jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            //         break;
            //  case AppUtils.HOSPITALNAME.WZSDEYY:
            //    if (info.paytype == "alipay")
            //    {
            //        result = "insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq , jzrq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
            //          " '" + brid + "', '手机支付宝充值', " + zhye + ",  " + czje + ",  0 , " +
            //          " 'ZH001', " + " '80', '" + pzhm + "',  to_date('" +
            //                      jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), to_date('" +
            //                 jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            //    }
            //    else
            //    {
            //        result = "insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq , jzrq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
            //         " '" + brid + "', '手机银联充值', " + zhye + ",  " + czje + ",  0 , " +
            //         " 'ZH001', " + " '80', '" + pzhm + "',  to_date('" +
            //                     jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), to_date('" +
            //                 jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            //    }
            //    break;
            //                case AppUtils.HOSPITALNAME.WZSDSRMYY:
            //                    result = "insert into MS_SZMX (sbxh, brid, zy, zhye, jfje, dfje, czgh, szlb, pzhm , rq, jzrq) values((select dqz from gy_identity  where bmc='MS_SZMX'), " +
            //                      " '" + brid + "', '网银追缴', " + zhye + ",  " + czje + ",  0 , " +
            //                      " '0001000', " + " '80', '" + pzhm + "',  to_date('" +
            //                     jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'), to_date('" +
            //                     jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            //                    break;
            //                default:
            //                    result = "";
            //                    break;
            //            }
            return result;
        }

        /// <summary>
        /// 把医院的明细表的序号回写到订单表的sql
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public string UpdateDdmxcodeSql(long yylsh)
        {
            string result = "";
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY)
            {
                result = "update " + YlddTableName() + " set yymxid = (select dqz from gy_identity_ms  where bmc='MS_SZMX') where yylsh ='" +
                    yylsh + "'";
            }
            else
            {
                result = "update " + YlddTableName() + " set yymxid = (select dqz from gy_identity  where bmc='MS_SZMX') where yylsh ='" +
                                   yylsh + "'";
            }
            return result;
        }

        /// <summary>
        /// 插入商户通知的sql
        /// </summary>
        /// <param name="info"></param>
        /// <param name="jysj"></param>
        /// <returns></returns>
        public string InsertSHTZSql(YLReplyInfo info, DateTime jysj)
        {
            string result = "insert into " + YhjlTableName() + "(yylsh,accNo,accessType,bizType,certId,currencyCode,encoding,merId," +
                                   "orderId,queryId,respCode,respMsg,settleAmt,settleCurrencyCode,settleDate,signMethod," +
                                    "traceNo,traceTime,txnAmt,txnSubType,txnTime,txnType,version,signature,jysj, source, paytype) values (" +
                                    info.yylsh + ",'" + info.accNo + "','" + info.accessType + "','" + info.bizType +
                                    "','" + info.certId + "','" + info.currencyCode + "','" + info.encoding + "','" + info.merId + "','" +
                                    info.orderId + "','" + info.queryId + "','" + info.respCode + "','" + info.respMsg + "','" +
                                    info.settleAmt + "','" + info.settleCurrencyCode + "','" + info.settleDate + "','" + info.signMethod + "','" +
                                    info.traceNo + "','" + info.traceTime + "','" + info.txnAmt + "','" + info.txnSubType + "','" +
                                    info.txnTime + "','" + info.txnType + "','" + info.version + "','" + info.signature +
                                    "',to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'),'" +
                                    info.source + "', '" + info.paytype + "')";
            return result;
        }

        /// <summary>
        /// 插入支付宝商户通知的sql
        /// </summary>
        /// <param name="info"></param>
        /// <param name="jysj"></param>
        /// <returns></returns>
        public string InsertAlipaySHTZSql(AlipayReplyInfo info, DateTime jysj)
        {
            string result = "insert into zfb_shtz (openid,name,patientid,tradeno,paymenttradeno,money,paymentparameters,inpatientno,czsj) values ('" +
                                    info.openid + "','" + info.patientname + "','" + info.patientid + "','" + info.tradeno +
                                    "','" + info.paymenttradeno + "','" + info.money + "','" + info.paymentparameters + "','" + info.inpatientno +
                                    "',to_date('" + jysj.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'))";
            return result;
        }

        /// <summary>
        /// 更新订单状态的sql
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ylcode"></param>
        /// <param name="ylmc"></param>
        /// <returns></returns>
        public string UpdateDDStatusSql(YLReplyInfo info, string ylcode, string ylmc)
        {
            string result = "update " + YlddTableName() + " set ddzt = '" + 2 + "', yhdm = '" + ylcode + "', yhmc = '" +
                              ylmc + "' where yylsh ='" + info.yylsh + "'";
            if (!info.respCode.Equals("00"))
            {
                result = "update " + YlddTableName() + " set ddzt = '" + 3 + "', yhdm = '" + ylcode + "', yhmc = '" +
                          ylmc + "' where yylsh ='" + info.yylsh + "'";
            }
            return result;
        }

        /// <summary>
        /// 更新支付宝订单状态的sql
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ylcode"></param>
        /// <param name="ylmc"></param>
        /// <returns></returns>
        public string UpdateAlipayDDStatusSql(AlipayReplyInfo info, string ylcode, string ylmc)
        {
            //  string result = "update " + YlddTableName() + " set ddzt = '" + 2 + "', yhdm = '" + ylcode + "', yhmc = '" +  ylmc + "' where yylsh ='" + info.tradeno + "'";
            int status = new HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy.QueryInfoDal().GetDingDanStatus(info.tradeno);
            string statusConditional = string.Empty;
            if (status == 0)
            {
                statusConditional = string.Format("  ,ddzt = '{0}'  ", 1);
            }
            string result = "update " + YlddTableName() + " set yhdm = '" + ylcode + "', yhmc = '" +
                              ylmc + "'" + statusConditional + " where yylsh ='" + info.tradeno + "'";
            if (string.IsNullOrEmpty(info.paymenttradeno) || string.IsNullOrEmpty(info.paymentparameters))
            {
                result = "update " + YlddTableName() + " set ddzt = '" + 3 + "', yhdm = '" + ylcode + "', yhmc = '" +
                              ylmc + "' where yylsh ='" + info.tradeno + "'";
            }
            return result;
        }

        /// <summary>
        /// 温州市中西医院用来获取门诊号码的sql
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string QueryHMForZXYReport(string brid, string brlx)
        {
            string result = "select ms.mzhm from gy_ckda gy,ms_brda ms where ms.brid = gy.id and ms.brid = '" + brid + "'  and gy.zfpb = 0";
            if (brlx.Equals("2"))
            {
                result = "select zy.zyhm  from zy_brry zy, gy_ksdm ks " +
                   " where zyh = '" + brid + "' and zy.cypb = 0 ";
            }
            return result;
        }

        /// <summary>
        /// 温州市苍南县第三人民医院用来获取门诊号码的sql
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string QueryHMForCNSYReport(string brid, string brlx)
        {
            string result = "select mzhm from ms_brda  where brid = '" + brid + "'";
            if (brlx.Equals("2"))
            {
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZHTYY)
                {
                    result = "select zyhm  from zy_brry  where zyh = '" + brid + "'";
                }
                else
                {
                    result = "select zyhm  from zy_brry  where brid = '" + brid + "'";
                }
            }
            return result;
        }

        /// <summary>
        /// 温州市苍南县第二人民医院用来获取门诊号码的sql
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string QueryHMForCNEYReport(string brid, string brlx)
        {
            string result = "select mzhm from lghosp_portal_his.ms_brda  where brid = '" + brid + "'";
            if (brlx.Equals("2"))
            {
                result = "select zyhm  from lghosp_portal_his.zy_brry  where brid = '" + brid + "'";
            }
            return result;
        }





        /// <summary>
        /// 获得取药信息
        /// </summary>
        /// <param name="brxm"></param>
        /// <param name="lxdh"></param>
        /// <returns></returns>
        public string QueryPatientDrugInfoSql(string brxm, string lxdh)
        {
            string result = "select to_char(ms_cf01.kfrq,'yyyymmdd'),to_char(ms_cf01.qyxh)," +
                                 " case when ms_cf01.ffrq is not null then '4' " + //已取药
                                 " when  ms_cf01.pyrq is  not null then '3'" + //可取药
                                 " when  ms_cf01.scrq is  not  null then '2' " + //传送中
                                 " when ms_cf01.fyrq is not null then '1' " +//调配中 
                                 " else '9' end as STATUS " +  //未知状态
                                 " from ms_brda,ms_cf01,ms_mzxx where ms_brda.brid = ms_cf01.brid " +
                                 " and ms_cf01.fphm = ms_mzxx.fphm and ms_mzxx.zfpb = 0 " +
                                 " and ms_mzxx.sfrq >= sysdate - 7 and ms_brda.brxm = '" + brxm +
                                 "' and ms_brda.jtdh = '" + lxdh + "' and ms_cf01.cflx = 3 and ms_cf01.yfsb = 4 ";
            return result;
        }

        /// <summary>
        /// 获得多页医生信息
        /// </summary>
        /// <param name="pNumber"></param>
        /// <param name="pRows"></param>
        /// <returns></returns>
        public string QueryPageDoctorsSql(int pNumber, int pRows)
        {
            long maxrow = 0, minrow = 0;
            General.CalculatePage(pNumber, pRows, out maxrow, out minrow);

            string result = "select ygdm,ygxm,dmmc,d from (select ygdm,ygxm,"
                    + "(select dmmc from gy_dmzd where dmlb = 27 and dmsb = gy_ygdm.ygjb) dmmc,rownum d "
                    + "from gy_ygdm where rylb =1 and zfpb = 0 and rownum <= " + maxrow + " order by ygdm) nr where nr.d >= " + minrow;

            return result;
        }

        /// <summary>
        /// 获得医生信息
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public string QueryDoctorInfoSql(string queryName)
        {
            string result = "select ygdm,ygxm,(select dmmc from gy_dmzd where dmlb = 27 and dmsb = gy_ygdm.ygjb) from gy_ygdm where rylb =1 and zfpb = 0 and ( ygxm like'%"
                    + queryName + "%' or pydm like  '" + queryName + "%' ) and rownum <=10 order by ygdm";
            return result;
        }

        /// <summary>
        /// 获取医生信息的代码
        /// </summary>
        /// <param name="ysdm"></param>
        /// <returns></returns>
        public string QueryDoctorInfoByDmSql(string ysdm)
        {
            string result = "select ygjj,ygsc from gy_ygjj where  ygdm = '" + ysdm + "'";
            return result;
        }

        /// <summary>
        /// 用来查询病人信息的语句
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string QueryPainterInfoForWZSDSRMYY(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "select ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh " +
                " from ms_brda ms where  ms.brid = '" + brid + "' ";

            if (brlx.Equals("2"))
            {
                result = "";
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 查询中医院预约信息
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string QueryFzpdInfoForWZSZYYSql(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "SELECT PD_YYXX.PDHM,PD_YYXX.BRXM,MS_GHKS.KSMC,GY_YGDM.YGXM,   " +
            " CASE WHEN YYLX = 3 THEN '护士台'  WHEN YYLX = 5 THEN '诊间' " +
          "  WHEN YYLX = 4 AND CZGH = '51GH' AND BZXX IS NULL THEN '51gh网'  " +
          "  WHEN YYLX = 4 AND BZXX = '12580' THEN '12580'  " +
          "  WHEN YYLX = 4 AND BZXX = '114' THEN '114' " +
          "   WHEN YYLX = 4 AND BZXX = '电视预约' THEN '电视' " +
          "  WHEN YYLX = 4 AND BZXX = 'weix' THEN '微信' " +
          "  WHEN YYLX = 4 AND CZGH = '51GH' AND BZXX not in ('12580','114','电视预约','weix') THEN '51gh网'  " +
          "  WHEN YYLX = 6 THEN '自助机'  " +
          "  WHEN YYLX = 2 THEN '电话'  " +
          "  ELSE '其他' END AS YYTJ, PD_YYXX.JZSJDM  " +
          " FROM PD_YYXX, GY_YGDM, MS_GHKS,  ms_brda  " +
          " WHERE ( PD_YYXX.YSDM = GY_YGDM.YGDM ) and  " +
          " ( PD_YYXX.KSDM = MS_GHKS.KSDM ) and " +
          " ( PD_YYXX.SFZH = ms_brda.sfzh OR PD_YYXX.DHHM = ms_brda.jtdh ) and " +
          " ( trunc(PD_YYXX.YYJZRQ) = trunc(sysdate) ) and  " +
          " ( ms_brda.brid = '" + brid + "')";

            if (brlx.Equals("2"))
            {
                result = "";
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 查询中医院排队信息
        /// </summary>
        /// <returns></returns>
        public string QueryQueueInfoForWZSZYYSql(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "select CASE WHEN PD.PDZT = 1 THEN 1 WHEN PD.PDZT = 2 AND SYSDATE > PD.GHSJ THEN 2 ELSE 0 END ZT , " +
         " PD.BRXM as brxm, DA.SFZH as sfzh,  KS.KSMC as ksmc, '' as zsmc, YS.YGXM as doctor,  PD.PDHM as pdhm,  PD.GHSJ as sj, " +
         " pd.ysdm, pd.ksdm,  pd.jzsjdm  " +
         " from PD_PDLS PD, MS_BRDA DA , MS_GHKS KS, GY_YGDM YS WHERE PD.BRID = DA.BRID AND  PD.KSDM = KS.KSDM AND  " +
         " PD.YSDM = YS.YGDM AND  trunc(PD.ghsj)  = trunc(sysdate) and  PD.BRID = '" + brid + "' order by pd.ghsj";

            if (brlx.Equals("2"))
            {
                result = "";
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得中医院查询前面排队人数
        /// </summary>
        /// <returns></returns>
        public string QueryWaitCountForWZSZYYSql(string ksdm, string ysdm, string jzsjdm, int pdhm, string brlx,
            out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "select count(*) from pd_pdls where ysdm = '" + ysdm + "' and ksdm = '" + ksdm + "' and " +
            " jzsjdm = '" + jzsjdm + "' and pdhm < " + pdhm + " and pdzt <> 3 order by pdhm";

            if (brlx.Equals("2"))
            {
                result = "";
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询报告单列表的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="brxm"></param>
        /// <returns></returns>
        public string GetSqlReportJCListForWzscnxdyrmyy(string brid, string brlx, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = brid;

            if (brlx.Equals("1"))
            {
                string sql = "select jzkh as bgdh , jclx sjmd, kdrq  from av_jianchaxm  where brid = '{0}' order by kdrq;";
                result = string.Format(sql, _paramters);
            }
            else if (brlx.Equals("2"))
            {

                result = "";
            }

            flag = HasAction(result, brlx, out msg);

            return result;
        }

        /// <summary>
        /// 根据SQL判断该功能是否支持
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="brlx"></param>
        /// <param name="msg"></param>
        /// <returns>true:支持该功能 false:不支持该功能</returns>
        private bool HasAction(string sql, string brlx, out string msg)
        {
            msg = "";

            if (null == sql || sql.Equals(""))
            {
                string _brxx = "门诊病人";
                if (brlx.Equals("2"))
                {
                    _brxx = "住院病人";
                }

                msg = "医院暂时不支持" + _brxx + "该功能的使用";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据SQL判断该功能是否支持
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="brlx"></param>
        /// <param name="msg"></param>
        /// <returns>true:支持该功能 false:不支持该功能</returns>
        private bool HasAction(string sql, out string msg)
        {
            msg = "";

            if (string.IsNullOrEmpty(sql))
            {
                msg = "医院暂时不支持该功能的使用";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取银联订单表的名称
        /// </summary>
        /// <returns></returns>
        private static string YlddTableName()
        {
            string tableName = "A_YL_DD";
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
            {
                tableName = "YL_DDJL";
            }
            return tableName;
        }

        /// <summary>
        /// 获取银联商户通知的表名称
        /// </summary>
        /// <returns></returns>
        private static string YhjlTableName()
        {
            string tableName = "A_YL_SHTZ";
            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
            {
                tableName = "yl_ylxx";
            }
            return tableName;
        }

        /// <summary>
        /// 获得用户注册的Sql
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcardno"></param>
        /// <param name="address"></param>
        /// <param name="openid"></param>
        /// <param name="headurl"></param>
        /// <param name="uesrtype"></param>
        /// <returns></returns>
        public string GetUserRegisterSql(string name, string phone, string idcardno, string address, string openid, string headurl, string usertype, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[7];
            _paramters[0] = name;
            _paramters[1] = phone;
            _paramters[2] = idcardno;
            _paramters[3] = address;
            _paramters[4] = openid;
            _paramters[5] = headurl;
            _paramters[6] = usertype;

            result = string.Format(_sqlUtils.userRegisterStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得用户注册(绑卡)的Sql
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcardno"></param>
        /// <param name="address"></param>
        /// <param name="openid"></param>
        /// <param name="headurl"></param>
        /// <param name="cardno"></param>
        /// <param name="patientid"></param>
        /// <param name="uesrtype"></param>
        /// <returns></returns>
        public string GetUserRegisterBindCardSql(string name, string phone, string idcardno, string address, string openid, string headurl, string cardno, string patientid, string usertype, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[9];
            _paramters[0] = name;
            _paramters[1] = phone;
            _paramters[2] = idcardno;
            _paramters[3] = address;
            _paramters[4] = openid;
            _paramters[5] = headurl;
            _paramters[6] = cardno;
            _paramters[7] = patientid;
            _paramters[8] = usertype;

            result = string.Format(_sqlUtils.userRegisterBindCardStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }
        /// <summary>
        /// 获得用户信息的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="uesrtype"></param>
        /// <returns></returns>
        public string GetLandSql(string openid, string usertype, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = openid;
            _paramters[1] = usertype;

            result = string.Format(_sqlUtils.landStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }
        /// <summary>
        /// 获得修改用户信息的Sql
        /// </summary>
        /// <param name="openid">身份证号</param>
        /// <param name="name">姓名</param>
        /// <param name="phone">手机号码</param>
        /// <param name="idcardno">身份证号</param>
        /// <param name="address">地址</param>
        /// <param name="headurl">用户头像</param>
        /// <param name="usertype">用户类型</param>
        /// <returns></returns>
        public string GetModifyLandSql(string openid, string name, string phone, string idcardno, string address, string headurl, string usertype, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[7];
            _paramters[0] = openid;
            _paramters[1] = name;
            _paramters[2] = phone;
            _paramters[3] = idcardno;
            _paramters[4] = address;
            _paramters[5] = headurl;
            _paramters[6] = usertype;

            result = string.Format(_sqlUtils.modifylandStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得添加常用联系人的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="label"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcardno"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public string GetAddContactsSql(string openid, string label, string name, string phone, string idcardno, string address,
            string linkmanid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[7];
            _paramters[0] = openid;
            _paramters[1] = label;
            _paramters[2] = name;
            _paramters[3] = phone;
            _paramters[4] = idcardno;
            _paramters[5] = address;
            _paramters[6] = linkmanid;

            result = string.Format(_sqlUtils.addContactsStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得常用联系人信息列表的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="linkmanid"></param>
        /// <returns></returns>
        public string GetFavoriteContactsListSql(string openid, string linkmanid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = openid;
            _paramters[1] = linkmanid;
            result = string.Format(_sqlUtils.favoriteContactsListStr, _paramters);

            if (string.IsNullOrEmpty(linkmanid))
            {
                result = string.Format(_sqlUtils.favoriteContactsListStr, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.favoriteContactsListStrWithLinkmanid, _paramters);
            }

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得删除常用联系人的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="linkmanid"></param>
        /// <returns></returns>
        public string GetDeleteFavoriteContactsSql(string openid, string linkmanid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = openid;
            _paramters[1] = linkmanid;
            result = string.Format(_sqlUtils.deleteFavoriteContactsStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得常用联系人信息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="linkmanid"></param>
        /// <returns></returns>
        public string GetLinkManNameSql(string openid, string linkmanid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = openid;
            _paramters[1] = linkmanid;
            result = string.Format(_sqlUtils.findLinkManNameStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得门诊卡列表的Sql
        /// </summary>
        /// <param name="idcardno"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetGetmzkListSql(string idcardno, string name, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = idcardno;
            _paramters[1] = name;
            result = string.Format(_sqlUtils.getmzklistStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得门诊卡信息(病人ID)的Sql
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public string GetGetmzkInforStrSql(string patientid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = patientid;
            result = string.Format(_sqlUtils.getmzkInforStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得本人门诊卡绑定的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public string GetUsermzkBindSql(string cardno, string patientid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = cardno;
            _paramters[1] = patientid;
            result = string.Format(_sqlUtils.usermzkBindStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得更新本人门诊卡绑定的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public string UpdateUsermzkBindInfoSql(string openid, string cardno, string patientid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            string[] _paramters = new string[3];
            _paramters[0] = cardno;
            _paramters[1] = patientid;
            _paramters[2] = openid;
            result = string.Format(_sqlUtils.updateUsermzkBindInfoStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得本人门诊卡解绑的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetUsermzkRelieveBindSql(string openid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = openid;
            result = string.Format(_sqlUtils.usermzkRelieveBindStr, _paramters);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 根据openid获取绑卡号码的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetCardNoFromIDSql(string openid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "";

            string[] _paramters = new string[1];
            _paramters[0] = openid;
            result = string.Format(_sqlUtils.getCardNoFromIDStr, _paramters);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 根据openid,linkmanid获取常用联系人绑卡号码的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetLinkmanCardNoFromIDSql(string openid, string linkmanID, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = openid;
            _paramters[1] = linkmanID;
            result = string.Format(_sqlUtils.getLinkmanCardNoFromIDStr, _paramters);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 更新常用联系人门诊卡绑定的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="linkmanid"></param>
        /// <param name="cardno"></param>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public string UpdateLinkManBindInfoSql(string openid, string linkmanid, string cardno, string patientid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            //0：未绑卡；1：已绑卡
            int bindcardfalg = 1;

            object[] _paramters = new object[5];
            _paramters[0] = openid;
            _paramters[1] = linkmanid;
            _paramters[2] = cardno;
            _paramters[3] = patientid;
            _paramters[4] = bindcardfalg;
            result = string.Format(_sqlUtils.updateLinkManBindInfoSqlStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得常用联系人门诊卡解绑的Sql
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="linkmanid"></param>
        /// <returns></returns>
        public string GetFavoriteContactsrmzkRelieveBindSql(string openid, string linkmanid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            //0：未绑卡；1：已绑卡
            int bindcardfalg = 0;

            object[] _paramters = new object[3];
            _paramters[0] = openid;
            _paramters[1] = linkmanid;
            _paramters[2] = bindcardfalg;
            result = string.Format(_sqlUtils.favoriteContactsrmzkRelieveBindStr, _paramters);

            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得医生信息列表_按姓名查的sql
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string DctorInfoXingmingSql(string name, long maxrow, long minrow, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            object[] _paramters = new object[3];
            _paramters[0] = name;
            _paramters[1] = maxrow;
            _paramters[2] = minrow;

            result = string.Format(_sqlUtils.dctorInfoXingming, _paramters);

            return result;
        }

        /// <summary>
        /// 获得医生信息列表_按姓名拼音首字母查的sql
        /// </summary>
        /// <param name="namepy"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string DctorInfoPinYinSql(string namepy, long maxrow, long minrow, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            object[] _paramters = new object[3];
            _paramters[0] = namepy;
            _paramters[1] = maxrow;
            _paramters[2] = minrow;

            result = string.Format(_sqlUtils.dctorInfoPinYin, _paramters);

            return result;
        }

        /// <summary>
        /// 获取医生信息列表_按医生代码查的代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string DoctorInfoByCodeSql(string code, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = code;

            result = string.Format(_sqlUtils.doctorInfoByCode, _paramters);

            return result;
        }

        /// <summary>
        /// 获得医生停诊信息列表的信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public string DoctorInfoTingZhenSql(long maxrow, long minrow, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            object[] _paramters = new object[2];
            _paramters[0] = maxrow;
            _paramters[1] = minrow;

            result = string.Format(_sqlUtils.doctorInfoTingZhen, _paramters);

            return result;
        }

        /// <summary>
        /// 门诊就诊病历列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetmzMedicalRecordsListSql(string patientid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = string.Format(_sqlUtils.mzMedicalRecordsList, patientid);

            return result;
        }

        /// <summary>
        /// 电子病历内容
        /// </summary>
        /// <param name="jzxh"></param>
        /// <returns></returns>
        public string GetElectronicMedicalRecordtSql(string jzxh, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = string.Format(_sqlUtils.ElectronicMedicalRecordt, jzxh);

            return result;
        }

        /// <summary>
        /// 门诊指引单
        /// </summary>
        /// <param name="jzxh"></param>
        /// <returns></returns>
        public string GetmzSingleGuideAndTakeMedicineSql(string jzxh, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = string.Format(_sqlUtils.mzSingleGuideAndTakeMedicine, jzxh);

            return result;
        }

        /// <summary>
        /// 病人药品处方信息列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetDrugPrescriptionInforListSql(string patientid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = string.Format(_sqlUtils.DrugPrescriptionInforList, patientid);

            return result;
        }

        /// <summary>
        /// 病人药品服用信息
        /// </summary>
        /// <param name="cflsh"></param>
        /// <returns></returns>
        public string GetPatientsTakingDrugsInforSql(string cflsh, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = string.Format(_sqlUtils.PatientsTakingDrugsInfor, cflsh);

            return result;
        }


        /// <summary>
        /// 预约科室信息
        /// </summary>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public string AppointmentInforSql(string departcode, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = departcode;

            if (string.IsNullOrEmpty(departcode))
            {
                result = string.Format(_sqlUtils.appointmentInfor, _paramters);
            }
            else
            {
                result = _sqlUtils.appointmentInforWithDepartcode;
            }

            return result;
        }

        /// <summary>
        /// 科室排班信息
        /// </summary>
        /// <param name="departcode"></param>
        /// <returns></returns>
        public string DepartmentSchedulSql(string departcode, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = departcode;

            result = string.Format(_sqlUtils.departmentSchedul, _paramters);


            return result;
        }

        /// <summary>
        /// 预约医生信息
        /// </summary>
        /// <param name="departcode"></param>
        /// <param name="scheduledate"></param>
        /// <returns></returns>
        public string ReservationDoctor1Sql(string departcode, string scheduledate, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            object[] _paramters = new object[2];
            _paramters[0] = departcode;
            _paramters[1] = scheduledate;

            if (string.IsNullOrEmpty(scheduledate))
            {
                result = string.Format(_sqlUtils.reservationDoctor, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.reservationDoctorWithScheduledate, _paramters);
            }

            return result;
        }

        /// <summary>
        /// 医生排班信息
        /// </summary>
        /// <param name="doctorcode"></param>
        /// <param name="departcode"></param>
        /// <param name="scheduledate"></param>
        /// <returns></returns>
        public string DoctorSchedulSql(string doctorcode, string departcode, string scheduledate, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[3];
            _paramters[0] = doctorcode;
            _paramters[1] = departcode;
            _paramters[2] = scheduledate;


            if (string.IsNullOrEmpty(scheduledate) && string.IsNullOrEmpty(departcode))
            {
                result = string.Format(_sqlUtils.doctorSchedulWithDoctorcode, _paramters);
            }
            else if (string.IsNullOrEmpty(scheduledate) && (!string.IsNullOrEmpty(departcode) && !string.IsNullOrEmpty(doctorcode)))
            {
                result = string.Format(_sqlUtils.doctorSchedulWithDoctorcodeAndDepartcode, _paramters);
            }
            else if (string.IsNullOrEmpty(departcode) && (!string.IsNullOrEmpty(doctorcode) && !string.IsNullOrEmpty(scheduledate)))
            {
                result = string.Format(_sqlUtils.doctorSchedulWithDoctorcodeAndScheduledate, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.doctorSchedul, _paramters);
            }

            return result;
        }

        /// <summary>
        /// 门诊号源信息
        /// </summary>
        /// <param name="doctorcode"></param>
        /// <param name="scheduleseq"></param>
        /// <returns></returns>
        public string mzReservationInforSql(string doctorcode, string scheduleseq, string shiftcode, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[3];
            _paramters[0] = doctorcode;
            _paramters[1] = scheduleseq;
            _paramters[2] = shiftcode;

            result = string.Format(_sqlUtils.mzReservationInforSql, _paramters);


            return result;
        }

        /// <summary>
        /// 门诊号源时间信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string mzReservationTimeInforSql(AlipayQueryOrderSeq info, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[5];
            _paramters[0] = info.departcode;
            _paramters[1] = info.week;
            _paramters[2] = info.shiftcode;
            _paramters[3] = info.doctorcode;
            _paramters[4] = info.orderno;

            result = string.Format(_sqlUtils.mzReservationTimeInforSql, _paramters);
            return result;
        }

        /// <summary>
        /// 获得温州市第七人民医院预约信息sql
        /// </summary>>
        /// <param name="orderNo"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns><returns>
        public string GetmzOrderRetInfoSql(string orderNo, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = string.Format(_sqlUtils.mzOrderRetInfoSql, orderNo);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 门诊预约历史
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string mzReservationHistorySql(string openid, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = openid;

            result = string.Format(_sqlUtils.mzReservationHistory, _paramters);
            flag = HasAction(result, out msg);


            return result;
        }

        /// <summary>
        /// 取消门诊预约
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string CancelmzOrderRetInfoSql(string orderNo, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = string.Format(_sqlUtils.cancelmzOrderRetInfoSql, orderNo);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 门诊预约报到
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="preengageseq"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string mzReservationReportSql(string openid, string preengageseq, out bool flag, out string msg)
        {
            flag = true;

            msg = "";

            string reslut = "";
            string[] _paramters = new string[10];
            //reslut = string.Format();

            //flag = HasAction();

            return reslut;
        }


        /// <summary>
        ///  查询排班医生
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SelectPaibanYSSql(out bool flag, out string msg)
        {
            msg = "";
            flag = true;

            string result = string.Format(_sqlUtils.SelectPaibanYS);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 查询挂号ID
        /// </summary>
        /// <param name="queueseq"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetqueueseqSql(string openid, string queueseq, out bool flag, out string msg)
        {
            msg = "";
            flag = true;
            string result = "";

            string[] _paramters = new string[2];
            _paramters[0] = openid;
            _paramters[1] = queueseq;


            if (string.IsNullOrEmpty(queueseq))
            {
                result = string.Format(_sqlUtils.Queueseq, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.QueueseqWithQueueseq, _paramters);
            }

            flag = HasAction(result, out msg);
            return result;
        }

        /// <summary>
        /// 查询医生姓名，上下午
        /// </summary>
        /// <param name="queueseq"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SelectDoctorname(string queueseqStr, out bool flag, out string msg)
        {
            msg = "";
            flag = true;
            string result = "";

            result = string.Format(_sqlUtils.SelectDoctorname, queueseqStr);

            flag = HasAction(result, out msg);
            return result;
        }

        /// <summary>
        /// 门诊预约排队列表
        /// </summary>
        /// <param name="queueseq"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetmzReservationQueueSql(string doctorname, string shiftname, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = doctorname;
            _paramters[1] = shiftname;
            result = string.Format(_sqlUtils.mzReservationQueue, _paramters);

            flag = HasAction(result, out msg);
            return result;
        }

        /// <summary>
        /// 门诊预约所有排队前十人员列表信息
        /// </summary>
        /// <param name="queueseq"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetmzReservationListSql(string doctorname, string shiftname, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = doctorname;
            _paramters[1] = shiftname;
            result = string.Format(_sqlUtils.mzReservationQueue, _paramters);

            flag = HasAction(result, out msg);
            return result;
        }

        /// <summary>
        /// 查询openid，预约流水号
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="preengageseq"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetSelectOpenidsql(string queueseq, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            result = string.Format(_sqlUtils.GetSelectOpenidsql, queueseq);

            flag = HasAction(result, out msg);
            return result;
        }

        /// <summary>
        /// 检查预约所有排队前十人员列表信息
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string CheckReservationReportQueueListSql(out bool flag, out string msg)
        {
            msg = "";
            string reslut = "";
            flag = true;

            string[] _paramters = new string[1];
            //reslut = string.Format();

            //flag = HasAction();

            return reslut;
        }

        /// <summary>
        /// 检查预约报到排队列表
        /// </summary>
        /// <param name="queueseq"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetCheckReservationReportListSql(string queueseq, out bool flag, out string msg)
        {
            msg = "";
            flag = true;

            string result = string.Format(_sqlUtils.mzReservationQueue, queueseq);
            flag = HasAction(result, out msg);


            return result;
        }

        /// <summary>
        /// 通知病人就诊信息
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetInformPatientSql(int Preengagestate, out bool flag, out string msg)
        {
            msg = "";
            string reslut = "";
            flag = true;

            reslut = string.Format(_sqlUtils.InformPatient, Preengagestate);
            flag = HasAction(reslut, out msg);

            return reslut;
        }

        /// <summary>
        /// 核对未到账的充值缴费信息
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetCheckInformationSql(int ddzt, out bool flag, out string msg)
        {
            msg = "";
            string reslut = "";
            flag = true;

            reslut = string.Format(_sqlUtils.CheckInformation, ddzt);
            flag = HasAction(reslut, out msg);

            return reslut;
        }

        /// <summary>
        /// 获得化验报告单列表的SQL
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idcardno"></param>
        /// <returns></returns>
        public string GetSqlAilpayReportList(string name, string idcardno, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = name;
            _paramters[1] = idcardno;

            result = string.Format(_sqlUtils.ailpaybrReportList, _paramters);


            return result;
        }

        /// <summary>
        /// 获得一个化验报告单抬头信息的SQL
        /// </summary>
        /// <param name="doctadviseno"></param>
        /// <returns></returns>
        public string GetLaboratoryTestsReportNameInformation(double doctadviseno, out bool flag, out string msg)
        {
            flag = true;
            msg = "";
            string result = "";

            result = string.Format(_sqlUtils.laboratoryTestsReportNameInformation, doctadviseno);

            return result;
        }

        /// <summary>
        /// 获得一个化验报告单详细列表信息的SQL
        /// </summary>
        /// <param name="doctadviseno"></param>
        /// <returns></returns>
        public string GetLaboratoryTestsReportDetailedListInformation(double doctadviseno, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";

            result = string.Format(_sqlUtils.getLaboratoryTestsReportDetailedListInformation, doctadviseno);


            return result;
        }

        /// <summary>
        /// 获得检验报告单列表的SQL
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idcardno"></param>
        /// <returns></returns>
        public string GetInspectionReportList(string name, string idcardno, out string sqlstr, out string oraclestr, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = name;
            _paramters[1] = idcardno;

            sqlstr = string.Format(_sqlUtils.getInspectionReportListWithECG, _paramters);
            oraclestr = string.Format(_sqlUtils.getInspectionReportList, _paramters);
            return result;
        }

        /// <summary>
        /// 获得一个检验报告单抬头信息的SQL
        /// </summary>
        /// <param name="doctadviseno"></param>
        /// <returns></returns>
        public string GetInspectionReportNameInformation(string doctadviseno, out string sqlstr, out string oraclestr, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = doctadviseno;

            oraclestr = string.Format(_sqlUtils.getInspectionReportNameInformation, _paramters);
            sqlstr = string.Format(_sqlUtils.getInspectionReportNameInformationWithECG, _paramters);

            return result;
        }

        /// <summary>
        /// 获得一个检验报告单详细列表信息的SQL
        /// </summary>
        /// <param name="doctadviseno"></param>
        /// <returns></returns>
        public string GetDInspectionReportResultsInformation(string doctadviseno, out string sqlstr, out string oraclestr, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[1];
            _paramters[0] = doctadviseno;

            oraclestr = string.Format(_sqlUtils.getDInspectionReportResultsInformation, _paramters);
            sqlstr = string.Format(_sqlUtils.getDInspectionReportResultsInformationWithECG, _paramters);

            return result;
        }


        /// <summary>
        /// 获得住院病人信息的SQL
        /// </summary>
        /// <param name="idcardno"></param>
        /// <returns></returns>
        public string GetZhuYuanPatientInfo(string idcardno, string name, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = idcardno;
            _paramters[1] = name;

            if (string.IsNullOrEmpty(name))
            {
                result = string.Format(_sqlUtils.getZhuYuanPatientInfoStr, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.getZhuYuanPatientInfoStrWithName, _paramters);
            }
            return result;
        }

        /// <summary>
        /// 获得住院费用信息的SQL
        /// </summary>
        /// <param name="idcardno"></param>
        /// <returns></returns>
        public string GetZhuYuanCostInfo(string inpatientno, string costdate, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = inpatientno;
            _paramters[1] = costdate;
            if (string.IsNullOrEmpty(costdate))
            {
                result = string.Format(_sqlUtils.getZhuYuanCostInfoStr, _paramters);
            }
            else
            {
                result = string.Format(_sqlUtils.getZhuYuanCostInfoStrWithCostdate, _paramters);
            }

            return result;
        }

        /// <summary>
        /// 获得查询绑卡号码是否有效的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string GetSqlValidCardno(string brid, string cardno, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = brid;
            _paramters[1] = cardno;

            result = string.Format(_sqlUtils.mzbrValidCardno, _paramters);
            flag = HasAction(result, out msg);

            return result;
        }

        /// <summary>
        /// 获得查询住院号码的SQL
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public string GetIsZYBrxxValid(string idcardno, string name, out bool flag, out string msg)
        {
            flag = true;
            msg = "";

            string result = "";
            string[] _paramters = new string[2];
            _paramters[0] = idcardno;
            _paramters[1] = name;

            result = string.Format(_sqlUtils.IsZYBrxxValid, _paramters);
            flag = HasAction(result, out msg);

            return result;
        }
    }
}