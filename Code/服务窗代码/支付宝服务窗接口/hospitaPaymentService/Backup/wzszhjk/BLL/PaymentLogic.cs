using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Common.Webservice;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.String;


namespace HospitaPaymentService.Wzszhjk.BLL
{
    public class PaymentLogic : BaseLogic
    {

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <param name="type">1-返回状态为未支付的订单 2-返回状态为已完成的订单</param>
        /// <param name="querytype">1-查询所有不分页  2-分页</param>
        /// <param name="pageno">查询页码</param>
        /// <param name="pagenum">每页展示多少条记录</param>
        /// <returns></returns>
        public XmlDocument OrderQuery(string brid, string brlx, string type, string querytype,
            int pageno, int pagenum)
        {
            PaymentDB pdb = new PaymentDB();
            string msg;
            ArrayList values;
            int ret = pdb.DB_QueryQrderList(brid, brlx, type, querytype, pageno, pagenum, out values, out msg);
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                doc.AppendChild(root);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);
                    foreach (ReportOrder ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        XmlElement eleYylsh = doc.CreateElement(AppUtils.Tag_Order_Yylsh);
                        eleValue.AppendChild(eleYylsh);
                        eleYylsh.InnerText = ri.yylsh;

                        XmlElement eleYhlsh = doc.CreateElement(AppUtils.Tag_Order_Yhlsh);
                        eleValue.AppendChild(eleYhlsh);
                        eleYhlsh.InnerText = ri.yhlsh;

                        XmlElement eleYhmc = doc.CreateElement(AppUtils.Tag_Order_Yhmc);
                        eleValue.AppendChild(eleYhmc);
                        eleYhmc.InnerText = ri.yhmc;

                        XmlElement eleCssj = doc.CreateElement(AppUtils.Tag_Order_Cssj);
                        eleValue.AppendChild(eleCssj);
                        eleCssj.InnerText = ri.cssj;

                        XmlElement eleCzje = doc.CreateElement(AppUtils.Tag_Order_Czje);
                        eleValue.AppendChild(eleCzje);
                        eleCzje.InnerText = Convert.ToString(ri.czje);

                        XmlElement eleDdzt = doc.CreateElement(AppUtils.Tag_Order_Ddzt);
                        eleValue.AppendChild(eleDdzt);
                        eleDdzt.InnerText = ri.ddzt;

                        XmlElement eleSjczzt = doc.CreateElement(AppUtils.Tag_Order_Sjczzt);
                        eleValue.AppendChild(eleSjczzt);
                        eleSjczzt.InnerText = ri.sjczzt;

                        XmlElement elePayType = doc.CreateElement(AppUtils.Tag_Order_PayType);
                        eleValue.AppendChild(elePayType);
                        elePayType.InnerText = ri.paytype;

                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }


        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="bkhm">卡号</param>
        /// <param name="brid">病人ID</param>
        /// <param name="czje">充值金额</param>
        /// <param name="tkje">退款金额</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <param name="payType">支付类型 alipay-支付宝 unionpay-银联</param>
        /// <returns></returns>
        public XmlDocument CreateOrder(string brid, string bkhm, double czje, double tkje, string brlx, 
            string payType)
        {
            XmlDocument doc = new XmlDocument();

            if (WebConfigParameter.StopCharge)
            {
                doc = ReplyXmlDoc.GetFailureXML(20, "医院内部维护中，暂停该功能");
                return doc;
            }

            try
            {
                string error_msg = "";
                long yylsh = -1;

                PaymentDB pdb = new PaymentDB();
                int ret = pdb.DB_CreateOrder(brid, bkhm, czje, tkje, brlx, payType, 
                    out yylsh, out error_msg);

                if (ret == 0)
                {
                    //正常返回
                    doc = new XmlDocument();
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eleYylsh = doc.CreateElement(AppUtils.Tag_Payment_YYLSH);
                    eleValue.AppendChild(eleYylsh);

                    eleYylsh.InnerText = StringHelper.YylshHasPrefix(yylsh) ;
                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="yylsh">银行流水号</param>
        /// <param name="yydm">银行代码</param>
        /// <param name="yhmc">银行名称</param>
        /// <param name="yhlsh">医院流水号</param>
        /// <param name="czje">充值金额</param>
        /// <param name="tkje">退款给金额</param>
        /// <param name="czsj">操作时间</param>
        /// <param name="zzlx">充值类型 01-银联充值</param>
        /// <returns></returns>
        public XmlDocument FinishOrder(long yylsh, string yhlsh, string sjczzt, DateTime czsj,
            string payType)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                string msg = "";

                PaymentDB pdb = new PaymentDB();
                int ret = pdb.DB_FinishOrder(yylsh, yhlsh, sjczzt, czsj, payType, out msg);

                if (ret == 0)
                {
                    doc = ReplyXmlDoc.GetSuccessXML(ret, msg);
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <returns></returns>
        public XmlDocument CancelOrder(long yylsh)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                string msg = "";

                PaymentDB pdb = new PaymentDB();
                int ret = pdb.DB_CancelOrder(yylsh, out msg);

                if (ret == 0)
                {
                    doc = ReplyXmlDoc.GetSuccessXML(ret, msg);
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }

            return doc;
        }

        /// <summary>
        /// 订单状态查询
        /// </summary>
        /// <param name="yylsh">医院流水号</param>
        /// <returns></returns>
        public XmlDocument StatusOrder(long yylsh)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                doc.AppendChild(root);
                string msg = "";
                string _ztmc = "";
                long _ddzt = 0;
                double _czje = 0;

                PaymentDB pdb = new PaymentDB();
                int dbout = pdb.DB_StatusOrder(yylsh, out _ddzt, out _ztmc, out _czje, out msg);

                if (dbout == 0)
                {
                    doc = ReplyXmlDoc.GetExceptionXML(dbout, msg);
                }
                else
                {
                    doc = ErrorReturnXml(dbout, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 查询账户余额
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1：门诊  2：住院</param>
        /// <returns></returns>
        public XmlDocument QueryBalance(string brid, string brlx)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                string error_msg = "";

                BalanceInfo info = new BalanceInfo();
                PaymentDB pdb = new PaymentDB();
                int ret = pdb.DB_QueryBalance(brid, brlx, out info, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eleZhye = doc.CreateElement(AppUtils.Tag_Balance_Zhye);
                    eleValue.AppendChild(eleZhye);
                    eleZhye.InnerText = info.zhye.ToString();
                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 账户变更明细(包含所有的账户记录）
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <param name="querytype">1-查询所有不分页  2-分页</param>
        /// <param name="pageno">查询页码</param>
        /// <param name="pagenum">每页展示多少条记录</param>
        /// <returns></returns>
        public XmlDocument AccountList(string brid, string brlx, string querytype,
            int pageno, int pagenum)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                string msg = "";
                ArrayList values = new ArrayList();

                PaymentDB pdb = new PaymentDB();
                int rt = pdb.DB_AccountRecordList(brid, brlx, querytype, pageno, pagenum, out values, out msg);

                if (rt == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);
                    foreach (AccountInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        XmlElement eleZffs = doc.CreateElement(AppUtils.Tag_Payment_ZFFC);
                        eleValue.AppendChild(eleZffs);
                        if (ri.zffs.Equals("智慧充值"))
                        {
                            eleZffs.InnerText = "手机充值";
                        }
                        else
                        {
                            eleZffs.InnerText = ri.zffs;
                        }

                        XmlElement eleJkje = doc.CreateElement(AppUtils.Tag_Payment_JKJE);
                        eleValue.AppendChild(eleJkje);
                        eleJkje.InnerText = Convert.ToString(ri.jkje);

                        XmlElement eleJkrq = doc.CreateElement(AppUtils.Tag_Payment_JKRQ);
                        eleValue.AppendChild(eleJkrq);
                        eleJkrq.InnerText = ri.jkrq;

                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(rt, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }

            return doc;
        }

        /// <summary>
        /// 插入银联记录
        /// </summary>
        /// <param name="info">银联信息</param>
        /// <returns></returns>
        public XmlDocument InsertCcb(YLReplyInfo info)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                string msg = "";

                PaymentDB pdb = new PaymentDB();
                int ret = pdb.DB_InsertCcb(info, out msg);

                if (ret == 0)
                {
                    doc = ReplyXmlDoc.GetSuccessXML(ret, msg);
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }

            return doc;
        }


        /// <summary>
        /// 交易明细
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型 1：门诊  2：住院</param>
        /// <returns></returns>
        public XmlDocument QueryDetail(string brid, string brlx)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                PaymentDB pdb = new PaymentDB();
                int rtint = pdb.DB_QueryDetail(brid, brlx, out _list, out error_msg);

                if (rtint == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = Convert.ToString(rtint);
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (PaymentDetail pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleOpdate = doc.CreateElement(AppUtils.Tag_Payment_OPDATE);
                        eleValue.AppendChild(eleOpdate);
                        //以下实现数据操作逻辑
                        eleOpdate.InnerText = pdt.rq.ToString("yyyy-MM-dd");

                        XmlElement eleJe = doc.CreateElement(AppUtils.Tag_Payment_CZJE);
                        eleValue.AppendChild(eleJe);
                        //以下实现数据操作逻辑
                        eleJe.InnerText = pdt.je.ToString("0.00");

                        XmlElement eleItem = doc.CreateElement(AppUtils.Tag_REXML_ITEM);
                        eleValue.AppendChild(eleItem);
                        //以下实现数据操作逻辑
                        eleItem.InnerText = pdt.item;
                    }
                    //eleResult.InnerText = "1";
                }
                else
                {
                    doc = ErrorReturnXml(rtint, error_msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }

            return doc;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// 更新HIS充值记录
        /// </summary>
        /// <param name="yhlsh">银行流水号</param>
        /// <param name="je">金额</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <param name="msg">成功、错处或异常信息</param>
        /// <returns>0-成功   大于0-失败   小于0-异常</returns>
        private int DB_updateHisCharge(string yhlsh, string je, string brlx, out string msg)
        {
            int ret = -1;
            msg = "";

            if (brlx.Equals("1"))
            {
                //TODO  更新数据表里面的门诊病人的充值记录

            }
            else if (brlx.Equals("2"))
            {
                //TODO  更新数据表里面的住院病人的充值记录
            }
            else
            {
                ret = 1;
                msg = "病人类型错误。";
            }

            return ret;
        }

        /// <summary>
        /// 病人类型是否有效
        /// </summary>
        /// <param name="brlx">病人类型</param>
        /// <param name="msg">出错信息</param>
        /// <returns>true-病人类型有效  false-病人类型无效</returns>
        private bool ValidBrlx(string brlx, out string msg)
        {
            bool ret = false;
            msg = "";
            if (!string.IsNullOrEmpty(brlx) && (brlx.Equals("1") || brlx.Equals("2")))
            {
                ret = true;
            }
            else
            {
                msg = "病人类型无效";
            }

            return ret;
        }

    }
}