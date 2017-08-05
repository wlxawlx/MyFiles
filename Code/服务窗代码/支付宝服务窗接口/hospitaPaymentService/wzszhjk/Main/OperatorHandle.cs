using System;
using System.Xml;
using HospitaPaymentService.Wzszhjk.BLL;
using HospitaPaymentService.Wzszhjk.BLL.Wzszyy;
using HospitaPaymentService.Wzszhjk.BLL.Alipay;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Utils.String;
using System.Collections.Generic;
using HospitaPaymentService.wzszhjk.BLL.Wzsdqrmyy;
using System.Collections;
using HospitaPaymentService.Wzszhjk.Common.Xml;
using log4net;

namespace HospitaPaymentService.Wzszhjk
{
    /// <summary>
    /// 用来处理wsdl操作并把返回信息组织成xml结构返回
    /// </summary>
    public class OperatorHandle
    {

        #region  绑卡相关操作

        /// <summary>
        /// 卡查询
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryCard(ParameterHandler handler)
        {
            string brxm = null, sfzh = null, brlx = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRXM:
                        brxm = tagValue;
                        break;
                    case AppUtils.Tag_Patient_SFZH:
                        sfzh = tagValue.ToUpper();
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        brlx = tagValue;
                        break;
                }
            }

            XmlDocument rtInfo = null;
            if (!string.IsNullOrEmpty(brxm) && !string.IsNullOrEmpty(sfzh) && !string.IsNullOrEmpty(brlx))
            {
                BindCardLogic pm = new BindCardLogic();
                rtInfo = pm.QueryCard(brxm, sfzh, brlx);
            }
            else
            {
                rtInfo = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Operate_QueryCard + "参数不完整，无法提交");
            }

            return rtInfo;
        }

        /// <summary>
        ///  卡绑定
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument BindCard(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string brid = null, sfzh = null, brxm = null, bklx = null, lxdh = null,
            brlx = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRID:
                        brid = tagValue;
                        break;
                    case AppUtils.Tag_Patient_SFZH:
                        sfzh = tagValue.ToUpper();
                        break;
                    case AppUtils.Tag_Patient_BRXM:
                        brxm = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BKLX:
                        bklx = tagValue;
                        break;
                    case AppUtils.Tag_Patient_LXDH:
                        lxdh = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        brlx = tagValue;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(brid) && !string.IsNullOrEmpty(sfzh) && !string.IsNullOrEmpty(brxm) &&
                !string.IsNullOrEmpty(bklx) && !string.IsNullOrEmpty(lxdh) && !string.IsNullOrEmpty(brlx))
            {
                BindCardLogic pm = new BindCardLogic();
                rtdoc = pm.BindCard(brid, sfzh, brxm, bklx, lxdh, brlx);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Operate_BindCard + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        #endregion

        #region 订单相关操作

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument OrderQuery(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _brid = null, _brlx = null, _type = null, _querytype = null;
            int _pageno = 0, _pagerow = 0;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRID:
                        _brid = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        _brlx = tagValue;
                        break;
                    case AppUtils.Tag_Payment_Type:
                        _type = tagValue;
                        break;
                    case AppUtils.Tag_Payment_QueryPage:
                        _querytype = tagValue;
                        break;
                    case AppUtils.Tag_Payment_PageNumber:
                        _pageno = Convert.ToInt32(tagValue);
                        break;
                    case AppUtils.Tag_Payment_PageRow:
                        _pagerow = Convert.ToInt32(tagValue);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_brid) && !string.IsNullOrEmpty(_brlx) && !string.IsNullOrEmpty(_type) &&
                !string.IsNullOrEmpty(_querytype))
            {
                //querytype为2是必须分页   
                if (_querytype.Equals("2") && (_pageno <= 0 || _pagerow <= 0))
                {
                    rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_OrderQuery + "参数不完整，无法提交");
                }
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.OrderQuery(_brid, _brlx, _type, _querytype, _pageno, _pagerow);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_OrderQuery + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CreateOrder(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string bkhm = null;
            string brid = null;
            double czje = 0;
            string brlx = null;
            string payType = "unionpay";
            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BKHM:
                        bkhm = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRID:
                        brid = tagValue;
                        break;
                    case AppUtils.Tag_Payment_CZJE:
                        czje = Convert.ToDouble(tagValue);
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        brlx = tagValue;
                        break;
                    case AppUtils.Tag_Payment_PayType:
                        payType = tagValue;
                        break;
                }
            }

            if (WebConfigParameter.HospitalName() != AppUtils.HOSPITALNAME.WZSDQRMYY && string.IsNullOrEmpty(bkhm))
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_CreateOrder + "参数不完整，无法提交");
            }
            else if (!string.IsNullOrEmpty(brid) && !string.IsNullOrEmpty(brlx) && !string.IsNullOrEmpty(payType) && czje > 0)
            {
                PaymentLogic pm = new PaymentLogic();
                double tkje = 0;
                rtdoc = pm.CreateOrder(brid, bkhm, czje, tkje, brlx, payType);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_CreateOrder + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 手机确认订单(订单支付)
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument FinishOrder(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            long yylsh = -1;
            string yhlsh = null, sjczzt = null;
            string payType = "unionpay";
            DateTime czsj = System.DateTime.Now;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_YYLSH:
                        yylsh = StringHelper.YylshNoPrefix(tagValue);
                        break;
                    case AppUtils.Tag_Payment_YHLSH:
                        yhlsh = tagValue;
                        break;
                    case AppUtils.Tag_Payment_Sjczzt:
                        sjczzt = tagValue;
                        break;
                    case AppUtils.Tag_Payment_PayType:
                        payType = tagValue;
                        break;
                }
            }

            if (yylsh > 0 && (!string.IsNullOrEmpty(yhlsh) || "unionpay" != payType) && !string.IsNullOrEmpty(sjczzt) &&
                !string.IsNullOrEmpty(payType))
            {
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.FinishOrder(yylsh, yhlsh, sjczzt, czsj, payType);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_FinishOrder + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CancelOrder(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            long yylsh = -1;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_YYLSH:
                        yylsh = StringHelper.YylshNoPrefix(tagValue);
                        break;
                }
            }

            if (yylsh > 0)
            {
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.CancelOrder(yylsh);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_CancelOrder + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 订单状态查询
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument statusOrder(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            long yylsh = 0;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_YYLSH:
                        yylsh = Convert.ToInt64(tagValue);
                        break;
                }
            }

            if (yylsh > 0)
            {
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.StatusOrder(yylsh);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_StatusOrder + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryBalance(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _brid = null;
            string _brlx = null;
            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRID:
                        _brid = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        _brlx = tagValue;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_brid) && !string.IsNullOrEmpty(_brlx))
            {
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.QueryBalance(_brid, _brlx);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_QueryBalance + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 账户变更记录(也叫账户明细）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument AccountRecord(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _brid = null;
            string _brlx = null;
            string _querytype = null;
            int _pageno = 0;
            int _pagerow = 0;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRID:
                        _brid = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        _brlx = tagValue;
                        break;
                    case AppUtils.Tag_Payment_QueryPage:
                        _querytype = tagValue;
                        break;
                    case AppUtils.Tag_Payment_PageNumber:
                        _pageno = Convert.ToInt32(tagValue);
                        break;
                    case AppUtils.Tag_Payment_PageRow:
                        _pagerow = Convert.ToInt32(tagValue);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_brid) && !string.IsNullOrEmpty(_brlx) &&
                !string.IsNullOrEmpty(_querytype))
            {
                if (_querytype.Equals("2") && (_pageno <= 0 || _pagerow <= 0))
                {
                    rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_AccountRecord + "参数不完整，无法提交");
                }
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.AccountList(_brid, _brlx, _querytype, _pageno, _pagerow);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_AccountRecord + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 插入银行记录(商户通知）P007
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument InsertCcb(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            YLReplyInfo info = new YLReplyInfo();
            info.paytype = "unionpay";

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_YYLSH:
                        info.yylsh = StringHelper.YylshNoPrefix(tagValue);
                        break;
                    case AppUtils.Tag_Payment_ACCNO:
                        info.accNo = tagValue;
                        break;
                    case AppUtils.Tag_Payment_ACCESSTYPE:
                        info.accessType = tagValue;
                        break;
                    case AppUtils.Tag_Payment_BIZTYPE:
                        info.bizType = tagValue;
                        break;
                    case AppUtils.Tag_Payment_CERTID:
                        info.certId = tagValue;
                        break;
                    case AppUtils.Tag_Payment_CURRENCYCODE:
                        info.currencyCode = tagValue;
                        break;
                    case AppUtils.Tag_Payment_ENCODING:
                        info.encoding = tagValue;
                        break;
                    case AppUtils.Tag_Payment_MERID:
                        info.merId = tagValue;
                        break;
                    case AppUtils.Tag_Payment_ORDERID:
                        info.orderId = tagValue;
                        break;
                    case AppUtils.Tag_Payment_QUERYID:
                        info.queryId = tagValue;
                        break;
                    case AppUtils.Tag_Payment_RESPCODE:
                        info.respCode = tagValue;
                        break;
                    case AppUtils.Tag_Payment_RESPMSG:
                        info.respMsg = tagValue;
                        break;
                    case AppUtils.Tag_Payment_SETTLEAMT:
                        info.settleAmt = tagValue;
                        break;
                    case AppUtils.Tag_Payment_SETTLECURRENCYCODE:
                        info.settleCurrencyCode = tagValue;
                        break;
                    case AppUtils.Tag_Payment_SETTLEDATE:
                        info.settleDate = tagValue;
                        break;
                    case AppUtils.Tag_Payment_SIGNMETHOD:
                        info.signMethod = tagValue;
                        break;
                    case AppUtils.Tag_Payment_TRACENO:
                        info.traceNo = tagValue;
                        break;
                    case AppUtils.Tag_Payment_TRACETIME:
                        info.traceTime = tagValue;
                        break;
                    case AppUtils.Tag_Payment_TXNAMT:
                        info.txnAmt = tagValue;
                        break;
                    case AppUtils.Tag_Payment_TXNSUBTYPE:
                        info.txnSubType = tagValue;
                        break;
                    case AppUtils.Tag_Payment_TXNTIME:
                        info.txnTime = tagValue;
                        break;
                    case AppUtils.Tag_Payment_TXNTYPE:
                        info.txnType = tagValue;
                        break;
                    case AppUtils.Tag_Payment_VERSION:
                        info.version = tagValue;
                        break;
                    case AppUtils.Tag_Payment_SIGNATURE:
                        info.signature = tagValue;
                        break;
                    case AppUtils.Tag_Payment_SOURCE:
                        info.source = tagValue;
                        break;
                    case AppUtils.Tag_Payment_PayType:
                        info.paytype = tagValue;
                        break;
                }
            }

            if (info.yylsh > 0 && !string.IsNullOrEmpty(info.queryId) && !string.IsNullOrEmpty(info.paytype) && Convert.ToDouble(info.txnAmt) > 0 && !string.IsNullOrEmpty(info.respCode))
            {
                PaymentLogic pm = new PaymentLogic();
                rtdoc = pm.InsertCcb(info);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Payment_InsertCcb + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        #endregion

        #region 查询操作

        /// <summary>
        /// 药品列表(分页 增补）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument PageMedicine(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            int pNumber = 0;
            int pRows = 0;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_PageNumber:
                        pNumber = Convert.ToInt32(tagValue);
                        break;
                    case AppUtils.Tag_Payment_PageRow:
                        pRows = Convert.ToInt32(tagValue);
                        break;
                }
            }

            if (pRows > 0 && pNumber > 0)
            {
                QueryLogic pm = new QueryLogic();
                rtdoc = pm.PageMedicine(pNumber, pRows);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_PageMedicine + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 药品查询(按拼音）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryMedicine(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string pydm = handler.getString("pydm");
            if (!string.IsNullOrEmpty(pydm))
            {
                QueryLogic pm = new QueryLogic();
                pydm = pydm.ToUpper();
                rtdoc = pm.Querymedicine(pydm);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_QueryMedicine + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 费用列表(分页 增补）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument PageCharge(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            int pNumber = 0;
            int pRows = 0;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_PageNumber:
                        pNumber = Convert.ToInt32(tagValue);
                        break;
                    case AppUtils.Tag_Payment_PageRow:
                        pRows = Convert.ToInt32(tagValue);
                        break;
                }
            }

            if (pRows > 0 && pNumber > 0)
            {
                QueryLogic pm = new QueryLogic();
                rtdoc = pm.PageCharge(pNumber, pRows);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_PageCharge + "参数不完整，无法提交");
            }


            return rtdoc;
        }

        /// <summary>
        /// 费用查询(按拼音）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryCharge(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string pydm = handler.getString("pydm");

            if (!string.IsNullOrEmpty(pydm))
            {
                QueryLogic pm = new QueryLogic();
                pydm = pydm.ToUpper();
                rtdoc = pm.Querycharge(pydm);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_QueryCharge + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 专家列表(分页增补）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument PageDoctor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            int pNumber = 0;
            int pRows = 0;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Payment_PageNumber:
                        pNumber = Convert.ToInt32(tagValue);
                        break;
                    case AppUtils.Tag_Payment_PageRow:
                        pRows = Convert.ToInt32(tagValue);
                        break;
                }
            }

            if (pRows > 0 && pNumber > 0)
            {
                QueryLogic pm = new QueryLogic();
                rtdoc = pm.PageDoctor(pNumber, pRows);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_PageDoctor + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 专家(医生）列表 (按拼音）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument ListDoctor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string pydm = handler.getString("pydm");

            if (!string.IsNullOrEmpty(pydm))
            {
                QueryLogic pm = new QueryLogic();
                pydm = pydm.ToUpper();
                rtdoc = pm.Listdoctor(pydm);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_ListDoctor + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 医生介绍
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryDoctor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string ysdm = handler.getString("ysdm");

            if (!string.IsNullOrEmpty(ysdm))
            {
                QueryLogic pm = new QueryLogic();
                ysdm = ysdm.ToUpper();
                rtdoc = pm.Querydoctor(ysdm);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_QueryDoctor + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 查询剩余床位数
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryRemainBeds(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            try
            {
                QueryLogic pm = new QueryLogic();
                rtdoc = pm.QueryRemainBeds();

            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }

        #region   中医院特色

        /// <summary>
        /// 中医院特色(取药查询）
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument PatientDrugInfo(ParameterHandler handler)
        {
            XmlDocument rtInfo = null;

            string pName = null;
            string pTelehphone = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRXM:
                        pName = tagValue;
                        break;
                    case AppUtils.Tag_Payment_LXDH:
                        pTelehphone = tagValue;
                        break;
                }
            }


            if (null != pName && !pName.Equals("") && null != pTelehphone && !pTelehphone.Equals(""))
            {
                QueryLogic pm = new QueryLogic();
                rtInfo = pm.QueryPatientDrugInfo(pName, pTelehphone);
            }
            else
            {
                rtInfo = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Feature_PatientDrugInfo + "参数不完整，无法提交");
            }

            return rtInfo;
        }

        //查询预约签到信息
        public static XmlDocument QuerySignInfo(ParameterHandler handler)
        {
            XmlDocument retDoc = null;

            string brid = handler.getString("brid");
            string brlx = handler.getString("brlx");

            if (!string.IsNullOrEmpty(brid) && !string.IsNullOrEmpty(brlx))
            {
                QueryLogicForWZSZYY pd = new QueryLogicForWZSZYY();
                retDoc = pd.QuerySignInfo(brid, brlx);
            }
            else
            {
                retDoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Feature_QuerySignInfo +
                "参数不完整, 无法提交");
            }

            return retDoc;
        }

        //查询排队信息
        public static XmlDocument QueryQueueInfo(ParameterHandler handler)
        {
            XmlDocument retDoc = null;

            string brid = handler.getString("brid");
            string brlx = handler.getString("brlx");

            if (!string.IsNullOrEmpty(brid) && !string.IsNullOrEmpty(brlx))
            {
                QueryLogicForWZSZYY pd = new QueryLogicForWZSZYY();
                retDoc = pd.QueryQueueInfo(brid, brlx);
            }
            else
            {
                retDoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Feature_QuerySignInfo +
                "参数不完整, 无法提交");
            }

            return retDoc;
        }

        #endregion

        #endregion

        #region 查询报告

        /// <summary>
        /// 根据病人id查询所有所有报告单信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryReportListByCode(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _brid = null;
            string _brlx = null;
            string _brxm = null;
            string _lxdh = null;
            //string _startTime = null;
            //string _endTime = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRID:
                        _brid = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        _brlx = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRXM:
                        _brxm = tagValue;
                        break;
                    case AppUtils.Tag_Patient_LXDH:
                        _lxdh = tagValue;
                        break;
                    //case AppUtils.Tag_Query_STARTTIME:
                    //    _startTime = tagValue;
                    //    break;
                    //case AppUtils.Tag_Query_ENDTIME:
                    //    _endTime = tagValue;
                    //    break;
                }
            }

            QueryReportLogic pm = new QueryReportLogic();
            if (AppUtils.HOSPITALNAME.WZSZYY == WebConfigParameter.HospitalName() &&
                !string.IsNullOrEmpty(_brxm) &&
                !string.IsNullOrEmpty(_lxdh))
            {
                rtdoc = pm.QueryReportListFromWebservice(_brxm, _lxdh);
            }
            else if (!string.IsNullOrEmpty(_brid) && !string.IsNullOrEmpty(_brlx))
            {
                rtdoc = pm.QueryReportList(_brid, _brlx);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_ReportListByID +
                "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 根据条码或者报告单号查询报告单信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryReportDetailByCode(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _code = null;
            string _lx = null;
            string _brxm = null;
            string _lxdh = null;
            //string _startTime = null;
            //string _endTime = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Query_CXHM:
                        _code = tagValue;
                        break;
                    case AppUtils.Tag_Query_CXLX:
                        _lx = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRXM:
                        _brxm = tagValue;
                        break;
                    case AppUtils.Tag_Patient_LXDH:
                        _lxdh = tagValue;
                        break;
                    //case AppUtils.Tag_Query_STARTTIME:
                    //    _startTime = tagValue;
                    //    break;
                    //case AppUtils.Tag_Query_ENDTIME:
                    //    _endTime = tagValue;
                    //    break;
                }
            }

            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY && !string.IsNullOrEmpty(_code) && !string.IsNullOrEmpty(_brxm) &&
                !string.IsNullOrEmpty(_lxdh))
            {
                QueryReportLogic pm = new QueryReportLogic();
                rtdoc = pm.QueryReportDetailByCodeFromWebservice(_code, _brxm, _lxdh);
            }
            //else if (WebConfigParameter.hospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY && !string.IsNullOrEmpty(_code))
            //{
            //    QueryReportLogic pm = new QueryReportLogic();
            //    rtdoc = pm.queryReportDetailByCodeFromWebservice(_code, "", "");
            //}
            else if (!string.IsNullOrEmpty(_code) && !string.IsNullOrEmpty(_lx))
            {
                QueryReportLogic pm = new QueryReportLogic();
                rtdoc = pm.QueryReportDetailByCode(_code, _lx, _brxm);

            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_ReportDetail + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 根据病人id查询所有所有检查单信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryReportJCListByCode(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _brid = null;
            string _brlx = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Patient_BRID:
                        _brid = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRLX:
                        _brlx = tagValue;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_brid) && !string.IsNullOrEmpty(_brlx))
            {
                QueryReportLogic pm = new QueryReportLogic();
                rtdoc = pm.QueryReportJCList(_brid, _brlx);
            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_ReportJCListByID +
                "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        /// 根据条码或者报告单号查询报告单信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument QueryReportJCDetailByCode(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string _code = null;
            string _lx = null;
            string _brxm = null;

            foreach (KeyValuePair<string, string> child in handler.getParams())
            {
                string name = child.Key;
                string tagValue = child.Value;
                switch (name)
                {
                    case AppUtils.Tag_Query_CXHM:
                        _code = tagValue;
                        break;
                    case AppUtils.Tag_Query_CXLX:
                        _lx = tagValue;
                        break;
                    case AppUtils.Tag_Patient_BRXM:
                        _brxm = tagValue;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_code) && !string.IsNullOrEmpty(_lx))
            {
                QueryReportLogic pm = new QueryReportLogic();
                rtdoc = pm.QueryReportJCDetailByCode(_code, _lx, _brxm);

            }
            else
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Query_ReportJCDetail + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        #endregion

        #region 日志相关

        /// <summary>
        /// 测试日志文件函数----无用
        /// </summary>
        public static XmlDocument Test(ParameterHandler handler)
        {
            XmlDocument rtdoc = new XmlDocument();
            rtdoc = ReplyXmlDoc.GetSuccessXML(100, WebConfigParameter.ConnectionHisString);

            return rtdoc;
        }

        #region Log 记录错误日志

        public static XmlDocument StartLog(ParameterHandler handler)
        {
            XmlDocument rtdoc = new XmlDocument();
            try
            {
                UtilLog.GetInstance().WriteProgramLogFlag = true;
                rtdoc = ReplyXmlDoc.GetSuccessXML(0, "成功开启日志记录文件");
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }

        public static XmlDocument EndLog(ParameterHandler handler)
        {
            XmlDocument rtdoc = new XmlDocument();
            try
            {
                UtilLog.GetInstance().WriteProgramLogFlag = false;
                rtdoc = ReplyXmlDoc.GetSuccessXML(0, "成功关闭日志记录文件");
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }

        public static XmlDocument ReadLog(ParameterHandler handler)
        {
            XmlDocument rtdoc = new XmlDocument();
            try
            {
                rtdoc = ReplyXmlDoc.GetSuccessXML(0, UtilLog.GetInstance().ReadProgramLog());
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }


        #endregion Log

        #endregion

        #region 支付宝服务窗相关

        /// <summary>
        ///  用户注册
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument UserRegister(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            UserInfo info = new UserInfo();
            try
            {
                info.name = handler.getNotNullString(AppUtils.Tag_User_Name);
                info.phone = handler.getNotNullString(AppUtils.Tag_User_Phone);
                info.idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo).ToUpper();
                info.address = handler.getString(AppUtils.Tag_User_Address);
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.headurl = handler.getNotNullString(AppUtils.Tag_User_HeadURL);
                info.usertype = handler.getNotNullString(AppUtils.Tag_User_UserType);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_UserRegister + ex + "参数不完整，无法提交");
            }


            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.UserRegister(info);

            return rtdoc;
        }

        /// <summary>
        ///  用户注册_带绑卡
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument UserRegisterBindCard(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            UserInfo info = new UserInfo();
            try
            {
                info.name = handler.getNotNullString(AppUtils.Tag_User_Name);
                info.phone = handler.getNotNullString(AppUtils.Tag_User_Phone);
                info.idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo).ToUpper();
                info.address = handler.getString(AppUtils.Tag_User_Address);
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.headurl = handler.getNotNullString(AppUtils.Tag_User_HeadURL);
                info.cardno = handler.getString(AppUtils.Tag_User_CardNo);
                info.patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
                info.usertype = handler.getNotNullString(AppUtils.Tag_User_UserType);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_UserRegisterBindCard + ex + "参数不完整，无法提交");
            }
            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.UserRegisterBindCard(info);

            return rtdoc;
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument UserInfor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, usertype = null;

            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                usertype = handler.getString(AppUtils.Tag_User_UserType);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_Land + ex + "参数不完整，无法提交");
            }

            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.UserInfo(openid, usertype);

            return rtdoc;
        }

        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument ModifyInfor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            UserInfo info = new UserInfo();

            try
            {
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.name = handler.getString(AppUtils.Tag_User_Name);
                info.phone = handler.getNotNullString(AppUtils.Tag_User_Phone);
                string idcardno = handler.getString(AppUtils.Tag_User_IDCardNo);
                if (!string.IsNullOrEmpty(idcardno))
                {
                    info.idcardno = idcardno.ToUpper();
                }
                info.address = handler.getString(AppUtils.Tag_User_Address);
                info.headurl = handler.getString(AppUtils.Tag_User_HeadURL);
                info.usertype = handler.getString(AppUtils.Tag_User_UserType);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_ModifyInfor + ex + "参数不完整，无法提交");
            }

            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.ModifyInfo(info);

            return rtdoc;
        }

        /// <summary>
        /// 添加常用联系人
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument AddContacts(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            UserInfo info = new UserInfo();
            try
            {
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.label = handler.getString(AppUtils.Tag_User_Label);
                info.name = handler.getNotNullString(AppUtils.Tag_User_Name);
                info.phone = handler.getNotNullString(AppUtils.Tag_User_Phone);
                info.idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo).ToUpper();
                info.address = handler.getString(AppUtils.Tag_User_Address);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_AddContacts + ex + "参数不完整，无法提交");
            }


            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.AddContacts(info);

            return rtdoc;
        }

        /// <summary>
        /// 获得常用联系人信息列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument FavoriteContactsListStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, linkmanid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                linkmanid = handler.getString(AppUtils.Tag_User_LinkManID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_FavoriteContactsListStr + ex + "参数不完整，无法提交");
            }

            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.FavoriteContactsListStr(openid, linkmanid);

            return rtdoc;
        }

        /// <summary>
        /// 删除常用联系人
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DeleteFavoriteContactsStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, linkmanid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                linkmanid = handler.getNotNullString(AppUtils.Tag_User_LinkManID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DeleteFavoriteContactsStr + ex + "参数不完整，无法提交");
            }

            UserRegisterLogic pm = new UserRegisterLogic();
            rtdoc = pm.DeleteFavoriteContactsStrr(openid, linkmanid);

            return rtdoc;
        }

        /// <summary>
        /// 获得门诊卡列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument GetmzklistStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string idcardno = null, name = null;

            try
            {
                idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo);
                idcardno = idcardno.ToUpper();
                name = handler.getString(AppUtils.Tag_User_Name);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_GetmzkListStr + ex + "参数不完整，无法提交");
            }

            MzBindCardLogic pm = new MzBindCardLogic();
            rtdoc = pm.GetmzkListStr(idcardno, name);

            return rtdoc;
        }

        /// <summary>
        /// 获得门诊卡信息_病人ID
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument GetmzkInforStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string patientid = null;
            try
            {
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_GetmzkInforStr + ex + "参数不完整，无法提交");
            }

            MzBindCardLogic pm = new MzBindCardLogic();
            rtdoc = pm.GetmzkInforStr(patientid);

            return rtdoc;
        }

        /// <summary>
        ///  本人门诊卡绑定
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument UsermzkBindStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, cardno = null, patientid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                cardno = handler.getNotNullString(AppUtils.Tag_User_CardNo);
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_UsermzkBindStr + ex + "参数不完整，无法提交");
            }

            MzBindCardLogic pm = new MzBindCardLogic();
            rtdoc = pm.UsermzkBindStr(openid, cardno, patientid);

            return rtdoc;
        }

        /// <summary>
        ///  本人门诊卡解绑
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument UsermzkRelieveBindStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_UsermzkRelieveBindStr + ex + "参数不完整，无法提交");
            }

            MzBindCardLogic pm = new MzBindCardLogic();
            rtdoc = pm.UsermzkRelieveBindStr(openid);

            return rtdoc;
        }

        /// <summary>
        ///  常用联系人门诊卡绑定
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument FavoriteContactsBindStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, linkmanid = null, cardno = null, patientid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                linkmanid = handler.getNotNullString(AppUtils.Tag_User_LinkManID);
                cardno = handler.getNotNullString(AppUtils.Tag_User_CardNo);
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_FavoriteContactsBindStr + ex + "参数不完整，无法提交");
            }

            MzBindCardLogic pm = new MzBindCardLogic();
            rtdoc = pm.FavoriteContactsBindStr(openid, linkmanid, cardno, patientid);

            return rtdoc;
        }

        /// <summary>
        ///  常用联系人门诊卡解绑
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument FavoriteContactsrmzkRelieveBindStr(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, linkmanid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                linkmanid = handler.getNotNullString(AppUtils.Tag_User_LinkManID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_FavoriteContactsrmzkRelieveBindStr + ex + "参数不完整，无法提交");
            }

            MzBindCardLogic pm = new MzBindCardLogic();
            rtdoc = pm.FavoriteContactsrmzkRelieveBindStr(openid, linkmanid);

            return rtdoc;
        }

        /// <summary>
        /// 医生信息列表_按姓名查
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DoctorInfoXingming(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string name = null, pageindex = null, pagesize = null;
            int index = 1, size = 20;

            try
            {
                name = handler.getNotNullString(AppUtils.Tag_User_Name);
                pageindex = handler.getString(AppUtils.Tag_User_PageIndex);
                if (!string.IsNullOrEmpty(pageindex))
                {
                    index = Convert.ToInt32(pageindex);
                }
                pagesize = handler.getString(AppUtils.Tag_User_PageSize);
                if (!string.IsNullOrEmpty(pageindex))
                {
                    size = Convert.ToInt32(pagesize);
                }
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DoctorInfoXingming + ex + "参数不完整，无法提交");
            }

            DoctorInfoLogic pm = new DoctorInfoLogic();
            rtdoc = pm.DctorInfoXingming(name, index, size);

            return rtdoc;
        }

        /// <summary>
        /// 医生信息列表_按姓名拼音首字母查
        /// </summary>
        /// <param namepy="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DoctorInfoPinYin(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string namepy = null, pageindex = null, pagesize = null;
            int index = 1, size = 20;

            try
            {
                namepy = handler.getNotNullString(AppUtils.Tag_User_NamePY);
                pageindex = handler.getString(AppUtils.Tag_User_PageIndex);
                if (!string.IsNullOrEmpty(pageindex))
                {
                    index = Convert.ToInt32(pageindex);
                }
                pagesize = handler.getString(AppUtils.Tag_User_PageSize);
                if (!string.IsNullOrEmpty(pageindex))
                {
                    size = Convert.ToInt32(pagesize);
                }
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DoctorInfoPinYin + ex + "参数不完整，无法提交");
            }

            DoctorInfoLogic pm = new DoctorInfoLogic();
            rtdoc = pm.DoctorInfoPinYin(namepy, index, size);

            return rtdoc;
        }

        /// <summary>
        /// 医生信息列表_按医生代码查
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DoctorInfoDaiMa(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string code = null;

            try
            {
                code = handler.getNotNullString(AppUtils.Tag_User_Code);
                code = code.ToUpper();
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DoctorInfoDaiMa + ex + "参数不完整，无法提交");
            }
            DoctorInfoLogic pm = new DoctorInfoLogic();
            rtdoc = pm.DoctorInfoDaiMa(code);

            return rtdoc;
        }

        /// <summary>
        /// 医生停诊信息列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DoctorInfoTingZhen(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string pageindex = null, pagesize = null;
            int index = 1, size = 20;

            try
            {
                pageindex = handler.getString(AppUtils.Tag_User_PageIndex);
                if (!string.IsNullOrEmpty(pageindex))
                {
                    index = Convert.ToInt32(pageindex);
                }
                pagesize = handler.getString(AppUtils.Tag_User_PageSize);
                if (!string.IsNullOrEmpty(pageindex))
                {
                    size = Convert.ToInt32(pagesize);
                }
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DoctorInfoTingZhen + ex + "参数不完整，无法提交");
            }

            DoctorInfoLogic pm = new DoctorInfoLogic();
            rtdoc = pm.DoctorInfoTingZhen(index, size);

            return rtdoc;
        }

        /// <summary>
        ///  门诊就诊病历列表，电子病历
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzMedicalRecordsList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string patientid = null;
            try
            {
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzMedicalRecordsList + ex + "参数不完整，无法提交");
            }

            MzMedicalRecordsLogic pm = new MzMedicalRecordsLogic();
            rtdoc = pm.mzMedicalRecordsList(patientid);

            return rtdoc;
        }

        /// <summary>
        ///  电子病历内容
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument ElectronicMedicalRecordt(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string jzxh = null;
            try
            {
                jzxh = handler.getNotNullString(AppUtils.Tag_User_Jzxh);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_ElectronicMedicalRecordt + ex + "参数不完整，无法提交");
            }

            MzMedicalRecordsLogic pm = new MzMedicalRecordsLogic();
            rtdoc = pm.ElectronicMedicalRecordt(jzxh);

            return rtdoc;
        }

        /// <summary>
        ///  门诊指引单
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzSingleGuideAndTakeMedicine(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string jzxh = null;
            try
            {
                jzxh = handler.getNotNullString(AppUtils.Tag_User_Jzxh);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzSingleGuideAndTakeMedicine + ex + "参数不完整，无法提交");
            }

            MzMedicalRecordsLogic pm = new MzMedicalRecordsLogic();
            rtdoc = pm.mzSingleGuideAndTakeMedicine(jzxh);

            return rtdoc;
        }

        /// <summary>
        ///  病人药品处方信息列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DrugPrescriptionInforList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string patientid = null;
            try
            {
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DrugPrescriptionInforList + ex + "参数不完整，无法提交");
            }

            PatientMedicationLogic pm = new PatientMedicationLogic();
            rtdoc = pm.DrugPrescriptionInforList(patientid);

            return rtdoc;
        }

        /// <summary>
        ///  病人药品服用信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument PatientsTakingDrugsInfor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string cflsh = null;
            try
            {
                cflsh = handler.getNotNullString(AppUtils.Tag_User_Cflsh);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_PatientsTakingDrugsInfor + ex + "参数不完整，无法提交");
            }

            PatientMedicationLogic pm = new PatientMedicationLogic();
            rtdoc = pm.PatientsTakingDrugsInfor(cflsh);

            return rtdoc;
        }



        /// <summary>
        ///  预约科室信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument AppointmentInfor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string departcode = null;
            try
            {
                departcode = handler.getString(AppUtils.Tag_User_DepartCode);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_AppointmentInfor + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.AppointmentInfor(departcode);

            return rtdoc;
        }

        /// <summary>
        ///  科室排班信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DepartmentSchedul(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string departcode = null;
            try
            {
                departcode = handler.getNotNullString(AppUtils.Tag_User_DepartCode);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DepartmentSchedul + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.DepartmentSchedul(departcode);

            return rtdoc;
        }

        /// <summary>
        ///  预约医生信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument ReservationDoctor1(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string departcode = null, scheduledate = null;
            try
            {
                departcode = handler.getNotNullString(AppUtils.Tag_User_DepartCode);
                scheduledate = handler.getString(AppUtils.Tag_User_ScheduleDate);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_ReservationDoctor1 + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.ReservationDoctor1(departcode, scheduledate);

            return rtdoc;
        }

        /// <summary>
        ///  医生排班信息(OR020201)
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument DoctorSchedul(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string doctorcode = null, departcode = null, scheduledate = null;
            try
            {
                doctorcode = handler.getNotNullString(AppUtils.Tag_User_Doctorcode);
                departcode = handler.getString(AppUtils.Tag_User_DepartCode);
                scheduledate = handler.getString(AppUtils.Tag_User_ScheduleDate);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_DoctorSchedul + ex + "参数不完整，无法提交");
            }
            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.DoctorSchedul(doctorcode, departcode, scheduledate);

            return rtdoc;
        }

        /// <summary>
        ///  门诊号源信息(OR020301)
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzReservationInfor(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string doctorcode = null, scheduleseq = null, shiftcode = null;
            try
            {
                doctorcode = handler.getString(AppUtils.Tag_User_Doctorcode);
                scheduleseq = handler.getNotNullString(AppUtils.Tag_User_Scheduleseq);
                shiftcode = handler.getNotNullString(AppUtils.Tag_User_ShiftCode);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzReservationInfor + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.mzReservationInfor(doctorcode, scheduleseq, shiftcode);

            return rtdoc;
        }

        /// <summary>
        ///  门诊预约
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzReservation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string sourcetype = handler.getString("sourcetype");
            AlipaymzOrderNeedInfo info = new AlipaymzOrderNeedInfo();
            try
            {
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.doctorcode = handler.getString(AppUtils.Tag_User_Doctorcode);
                info.scheduleseq = handler.getNotNullString(AppUtils.Tag_User_Scheduleseq);
                info.orderseq = handler.getString(AppUtils.Tag_User_Orderseq);
                info.ordertime = handler.getNotNullString(AppUtils.Tag_User_OrderTime);
                info.orderendtime = handler.getNotNullString(AppUtils.Tag_User_Orderendtime);
                info.shiftcode = handler.getNotNullString(AppUtils.Tag_User_ShiftCode);
                info.patientid = handler.getString(AppUtils.Tag_User_PatientID);
                info.patientname = handler.getNotNullString(AppUtils.Tag_User_PatientName);
                info.patientidcardno = handler.getNotNullString(AppUtils.Tag_User_PatientIDCardNO);
                info.patientphone = handler.getNotNullString(AppUtils.Tag_User_PatientPhone);
                info.patientaddress = handler.getString(AppUtils.Tag_User_PatientAddress);
                info.scourcetype = sourcetype;

            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzReservation + ex + "参数不完整，无法提交");
            }
            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.mzReservation(info);
            return rtdoc;
        }

        /// <summary>
        ///  门诊预约历史
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzReservationHistory(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzReservationHistory + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.mzReservationHistory(openid);
            return rtdoc;
        }

        /// <summary>
        ///  取消门诊预约
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CancelmzReservation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, preengageseq = null;
            try
            {
                openid = handler.getString(AppUtils.Tag_User_OpenID);
                preengageseq = handler.getNotNullString(AppUtils.Tag_User_Preengageseq);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_CancelmzReservation + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.CancelmzReservation(openid, preengageseq);

            return rtdoc;
        }

        /// <summary>
        ///  门诊预约报到
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzReservationReport(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, preengageseq = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                preengageseq = handler.getNotNullString(AppUtils.Tag_User_Preengageseq);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzReservationReport + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.mzReservationReport(openid, preengageseq);

            return rtdoc;
        }

        /// <summary>
        /// 挂号退号
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument RegistrationNumber(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string guahaoID = null;
            try
            {
                guahaoID = handler.getNotNullString(AppUtils.Tag_User_guahaoID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_RegistrationNumber + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.RegistrationNumber(guahaoID);

            return rtdoc;
        }

        /// <summary>
        ///  门诊预约排队列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzReservationQueue(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null, queueseq = null;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                queueseq = handler.getString(AppUtils.Tag_User_Queueseq);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzReservationQueue + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.mzReservationQueue(openid, queueseq);

            return rtdoc;
        }

        /// <summary>
        ///  门诊预约所有排队前十人员列表信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzReservationQueueInfo(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            try
            {
                QueryOrderLogic pm = new QueryOrderLogic();
                rtdoc = pm.mzReservationQueueInfo();
            }
            catch (ArgErrorException ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }

        /// <summary>
        /// FY090909
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetSfInfo(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string brid = null, ysid = null;
            try
            {
                brid = handler.getNotNullString("brid");
                ysid = handler.getString("ysid");
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.GetSfInfo(brid, ysid);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzReservationQueue + ex + "参数不完整，无法提交");
            }

            return rtdoc;
        }

        /// <summary>
        ///  检查预约报到排队列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CheckReservationReportList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string queueseq = null;
            try
            {
                queueseq = handler.getString(AppUtils.Tag_User_Queueseq);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_CheckReservationReportList + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.CheckReservationReportList(queueseq);

            return rtdoc;
        }

        /// <summary>
        /// 检查预约报到
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CheckReservationReport(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string patientid = null, preengageseq = null;
            try
            {
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
                preengageseq = handler.getNotNullString(AppUtils.Tag_User_Preengageseq);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_CheckReservationReport + ex + "参数不完整，无法提交");
            }

            QueryOrderLogic pm = new QueryOrderLogic();
            rtdoc = pm.mzReservationReport(patientid, preengageseq);

            return rtdoc;
        }

        /// <summary>
        /// 检查预约所有排队前十人员列表信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CheckReservationReportQueueList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            try
            {
                QueryOrderLogic pm = new QueryOrderLogic();
                rtdoc = pm.CheckReservationReportQueueList();
            }
            catch (ArgErrorException ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }

        /// <summary>
        ///  化验报告单列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument LaboratoryTestsReportList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string name = null, idcardno = null;
            try
            {
                name = handler.getNotNullString(AppUtils.Tag_User_Name);
                idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo);
                idcardno = idcardno.ToUpper();
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_LaboratoryTestsReportList + ex + "参数不完整，无法提交");
            }

            InspectionCheckLogic pm = new InspectionCheckLogic();
            rtdoc = pm.LaboratoryTestsReportList(name, idcardno);

            return rtdoc;
        }

        /// <summary>
        ///  一个化验报告单的抬头信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument LaboratoryTestsReportNameInformation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string doctadviseno = null;
            try
            {
                doctadviseno = handler.getNotNullString(AppUtils.Tag_User_Doctadviseno);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_LaboratoryTestsReportNameInformation + ex + "参数不完整，无法提交");
            }

            InspectionCheckLogic pm = new InspectionCheckLogic();
            rtdoc = pm.LaboratoryTestsReportNameInformation(doctadviseno);

            return rtdoc;
        }

        /// <summary>
        ///  一个化验报告单详细列表信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument LaboratoryTestsReportDetailedListInformation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string doctadviseno = null;
            try
            {
                doctadviseno = handler.getNotNullString(AppUtils.Tag_User_Doctadviseno);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_LaboratoryTestsReportDetailedListInformation + ex + "参数不完整，无法提交");
            }

            InspectionCheckLogic pm = new InspectionCheckLogic();
            rtdoc = pm.LaboratoryTestsReportDetailedListInformation(doctadviseno);

            return rtdoc;
        }

        /// <summary>
        ///  检验报告单列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument InspectionReportList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string name = null, idcardno = null;
            try
            {
                name = handler.getNotNullString(AppUtils.Tag_User_Name);
                idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo);
                idcardno = idcardno.ToUpper();
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_InspectionReportList + ex + "参数不完整，无法提交");
            }

            InspectionCheckLogic pm = new InspectionCheckLogic();
            rtdoc = pm.InspectionReportList(name, idcardno);

            return rtdoc;
        }

        /// <summary>
        ///  一个检验报告单的抬头信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument InspectionReportNameInformation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string doctadviseno = null;
            try
            {
                doctadviseno = handler.getNotNullString(AppUtils.Tag_User_Doctadviseno);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_InspectionReportNameInformation + ex + "参数不完整，无法提交");
            }

            InspectionCheckLogic pm = new InspectionCheckLogic();
            rtdoc = pm.InspectionReportNameInformation(doctadviseno);

            return rtdoc;
        }

        /// <summary>
        ///  一个检验报告单的结果信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument InspectionReportResultsInformation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string doctadviseno = null;
            try
            {
                doctadviseno = handler.getNotNullString(AppUtils.Tag_User_Doctadviseno);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_InspectionReportResultsInformation + ex + "参数不完整，无法提交");
            }

            InspectionCheckLogic pm = new InspectionCheckLogic();
            rtdoc = pm.InspectionReportResultsInformation(doctadviseno);

            return rtdoc;
        }

        /// <summary>
        /// 创建门诊预存订单
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument mzCreateOrder(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string openid = null;
            string patientname = null;
            string patientcardno = null;
            string cardno = null;
            string patientid = null;
            string subject = "alipay_mz";
            string jzje = null;
            double money = 0;
            string jzid = "";
            string shdzid = "";
            string qjfs = "";
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                patientname = handler.getString(AppUtils.Tag_User_PatientName);
                patientcardno = handler.getString(AppUtils.Tag_User_PatientCardNo);
                cardno = handler.getString(AppUtils.Tag_User_CardNo);
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
                jzje = handler.getNotNullString(AppUtils.Tag_User_Money);
                money = Convert.ToDouble(jzje);
                subject = handler.getNotNullString(AppUtils.Tag_User_SubJect);
                jzid = handler.getString("jzid");
                shdzid = handler.getString("shdzid");
                qjfs = handler.getString("qjfs");
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzCreateOrder + ex + "参数不完整，无法提交");
            }
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            double tkje = 0;
            string patienttype = "1";
            rtdoc = pm.CreateOrder(shdzid, jzid, qjfs, openid, patientname, patientcardno, cardno, patientid, subject, money, tkje, patienttype);
            return rtdoc;
        }

        /// <summary>
        /// 门诊预存订单列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument mzPredepositList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = "", brlx = "1";

            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzPredepositList + ex + "参数不完整，无法提交");
            }

            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            rtdoc = pm.PredepositList(openid, brlx);

            return rtdoc;
        }

        /// <summary>
        /// 取消门诊预缴订单
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CancelmzPredepositList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null;
            string patientname = null;
            string patientid = null;
            string tradeno = null;
            long yylsh = -1;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                patientname = handler.getString(AppUtils.Tag_User_PatientName);
                patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
                tradeno = handler.getNotNullString(AppUtils.Tag_User_Tradeno);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_CancelmzPredepositList + ex + "参数不完整，无法提交");
            }
            yylsh = StringHelper.YylshNoPrefix(tradeno);
            if (yylsh > 0)
            {
                AlipayPaymentLogic pm = new AlipayPaymentLogic();
                rtdoc = pm.CancelPredepositList(openid, patientname, patientid, yylsh);
            }

            return rtdoc;
        }

        /// <summary>
        /// 商户通知门诊预缴成功(PO010401)
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument mzPredepositSuccess(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string brlx = "1";
            AlipayReplyInfo info = new AlipayReplyInfo();
            try
            {
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.patientname = handler.getString(AppUtils.Tag_User_PatientName);
                info.patientid = handler.getNotNullString(AppUtils.Tag_User_PatientID);
                string tradeno = handler.getNotNullString(AppUtils.Tag_User_Tradeno);
                info.tradeno = StringHelper.YylshNoPrefix(tradeno);
                info.money = handler.getNotNullString(AppUtils.Tag_User_Money);
                info.paymenttradeno = handler.getNotNullString(AppUtils.Tag_User_PaymentTradeno);
                info.paymentparameters = handler.getNotNullString(AppUtils.Tag_User_PaymentParameters);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzPredepositSuccess + ex + "参数不完整，无法提交");
            }
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            rtdoc = pm.PredepositSuccess(info, brlx);

            return rtdoc;
        }

        /// <summary>
        /// 商户通知住院预缴成功 (PI010401)
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument zyAdvanceSuccess(ParameterHandler handler)
        {
            string brlx = "2";
            AlipayReplyInfo info = new AlipayReplyInfo();
            try
            {
                info.openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                info.patientname = handler.getString(AppUtils.Tag_User_PatientName);
                info.inpatientno = handler.getNotNullString(AppUtils.Tag_User_InPatientNo);
                string tradeno = handler.getNotNullString(AppUtils.Tag_User_Tradeno);
                info.tradeno = StringHelper.YylshNoPrefix(tradeno);
                info.money = handler.getNotNullString(AppUtils.Tag_User_Money);
                info.paymenttradeno = handler.getNotNullString(AppUtils.Tag_User_PaymentTradeno);
                info.paymentparameters = handler.getNotNullString(AppUtils.Tag_User_PaymentParameters);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyAdvanceSuccess + ex + "参数不完整，无法提交");
            }

            XmlDocument rtdoc = null;
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            rtdoc = pm.PredepositSuccess(info, brlx);

            return rtdoc;
        }

        /// <summary>
        /// 创建住院预存订单
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument zyCreateOrder(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string openid = null;
            string patientname = null;
            string patientcardno = null;
            string inpatientno = null;
            string subject = "alipay_zy";
            string jzje = null;
            double money = 0;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                patientname = handler.getString(AppUtils.Tag_User_PatientName);
                patientcardno = handler.getString(AppUtils.Tag_User_PatientCardNo);
                inpatientno = handler.getNotNullString(AppUtils.Tag_User_InPatientNo);
                jzje = handler.getNotNullString(AppUtils.Tag_User_Money);
                money = Convert.ToDouble(jzje);
                subject = handler.getNotNullString(AppUtils.Tag_User_SubJect);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyCreateOrder + ex + "参数不完整，无法提交");
            }
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            double tkje = 0;
            string patienttype = "2";
            rtdoc = pm.CreateOrder("", "", "", openid, patientname, patientcardno, "", inpatientno, subject, money, tkje, patienttype);
            return rtdoc;
        }

        /// <summary>
        ///  住院预缴订单列表
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument zyAdvanceOrderList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = "", brlx = "2";
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyAdvanceOrderList + ex + "参数不完整，无法提交");
            }
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            rtdoc = pm.PredepositList(openid, brlx);
            return rtdoc;
        }

        /// <summary>
        /// 取消住院预缴订单
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CancelzyAdvanceOrders(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string openid = null;
            string patientname = null;
            string inpatientno = null;
            string tradeno = null;
            long yylsh = -1;
            try
            {
                openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
                patientname = handler.getString(AppUtils.Tag_User_PatientName);
                inpatientno = handler.getNotNullString(AppUtils.Tag_User_InPatientNo);
                tradeno = handler.getNotNullString(AppUtils.Tag_User_Tradeno);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_CancelzyAdvanceOrders + ex + "参数不完整，无法提交");
            }

            yylsh = StringHelper.YylshNoPrefix(tradeno);
            if (yylsh > 0)
            {
                AlipayPaymentLogic pm = new AlipayPaymentLogic();
                rtdoc = pm.CancelPredepositList(openid, patientname, inpatientno, yylsh);
            }

            return rtdoc;
        }



        /// <summary>
        /// 住院病人信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument zyPatientInfor(ParameterHandler handler)
        {
            string idcardno = null;
            string name = null;
            try
            {
                idcardno = handler.getNotNullString(AppUtils.Tag_User_IDCardNo);
                idcardno = idcardno.ToUpper();
                name = handler.getString(AppUtils.Tag_User_Name);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }

            XmlDocument rtdoc = null;
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            rtdoc = pm.zyPatientInfor(idcardno, name);

            return rtdoc;
        }

        /// <summary>
        /// 住院费用信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument zyCostInfo(ParameterHandler handler)
        {
            string inpatientno = null;
            string costdate = null;
            try
            {
                inpatientno = handler.getNotNullString(AppUtils.Tag_User_InPatientNo);
                costdate = handler.getString(AppUtils.Tag_User_CostDate);
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }

            XmlDocument rtdoc = null;
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            rtdoc = pm.zyCostInfo(inpatientno, costdate);

            return rtdoc;
        }

        /// <summary>
        /// 通知病人就诊信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument InformPatient(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            try
            {
                NotificationInformation pm = new NotificationInformation();
                rtdoc = pm.InformPatient();
            }
            catch (ArgErrorException ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }

        /// <summary>
        /// 核对未到账的充值缴费信息
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        public static XmlDocument CheckInformation(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            try
            {
                NotificationInformation pm = new NotificationInformation();
                rtdoc = pm.CheckInformation();
            }
            catch (ArgErrorException ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.Message + "]");
            }

            return rtdoc;
        }


        #endregion 支付宝服务窗相关

        #region 支付宝服务窗新功能

        /// <summary>
        /// 获取联系人列表 （FY030101）yyy_lianxiren
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryConnectPerson(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            string usertype = handler.getNotNullString(AppUtils.Tag_User_UserType);
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.QueryConnectPerson(openid, usertype);
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }

        /// <summary>
        /// 获取病人基本信息（FY030102） 获取病人基本信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPatientInfo(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string id = handler.getNotNullString("id");
            string type = handler.getNotNullString("type");

            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.QueryPatientInfo(id, type);
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }


        /// <summary>
        /// 退费清单处方单信息 退费清单处方单信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetChargeList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string orderno = handler.getNotNullString(AppUtils.Tag_User_OrderNo);
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.GetChargeList(orderno);

            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }

        /// <summary>
        /// 缴费时获取处方单信息（FY030103）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetChuFangDetailList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string jzid = handler.getNotNullString("jzid");

            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.QueryDetailChuFangInfoByJzId(jzid);

            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }

        /// <summary>
        /// 缴费时获取处方单药品详情信息FY030201 
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryChuFangYaoPinInfo(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string fpxmid = handler.getNotNullString("fpxmid");
            string jzid = handler.getNotNullString("jzid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.QueryChuFangYaoPinInfo(fpxmid, jzid);
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }

        /// <summary>
        /// 可退费明细
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryChargeListByOrderno(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            string fpxmid = handler.getNotNullString("fpxmid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryChargeListByOrderno(fpxmid, orderno);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 获取默认收件人信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryReceiverInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryReceiverInfo(openid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 获取收件人信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryByReceiverInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            string shdzid = handler.getString("shdzid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryByReceiverInfo(openid, shdzid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        ///预提交缴费信息（FY030303）预缴费联众webservice接口
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPaymentInfo(ParameterHandler handler)
        {
            XmlDocument doc = new XmlDocument();
            string jzid = handler.getNotNullString("jzid");
            try
            {
                ServiceReference1.TradeServerGGJKClient ggjk = new ServiceReference1.TradeServerGGJKClient();

                string aa = "";
                string outS = "";
                bool ret = false;
                int outInt = 0;
                string CAOZUOYDM = "ZFB";
                string CAOZUOYXM = "ZFB";
                string f = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><YUJIESUAN_IN><BASEINFO><CAOZUOYDM>{1}</CAOZUOYDM><CAOZUOYXM>{2}</CAOZUOYXM><CAOZUORQ>2017-02-06 14:35:30</CAOZUORQ><XITONGBS>0102</XITONGBS><FENYUANDM>1</FENYUANDM></BASEINFO><JZID>{0}</JZID></YUJIESUAN_IN>", jzid, CAOZUOYDM, CAOZUOYXM);
                string moth = "HIS4.YUJIESUAN";
                ggjk.RunService(moth, f, ref aa);
                doc.LoadXml(aa);

            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            //XmlDocument doc = null;
            //string jzid = "1";
            //string qjfs = "1";
            //string shdzid = "1";
            //try
            //{
            //    QueryInfoBll queryInfoBll = new QueryInfoBll();
            //    doc = queryInfoBll.QueryPaymentInfo(jzid, qjfs, shdzid);
            //}
            //catch (Exception ex)
            //{
            //    doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            //}
            return doc;
        }

        /// <summary>
        /// 退费  FY030305
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPaymentInfo2(ParameterHandler handler)
        {
            XmlDocument doc = new XmlDocument();
            string orderno = handler.getNotNullString("orderno");
            string CAOZUOYDM = "ZFB";
            string CAOZUOYXM = "ZFB";
            try
            {
                ServiceReference1.TradeServerGGJKClient ggjk = new ServiceReference1.TradeServerGGJKClient();
                string aa = "";
                string outS = "";
                bool ret = false;
                int outInt = 0;
                string f = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><TUIFEI_IN><BASEINFO><CAOZUOYDM>{2}</CAOZUOYDM><CAOZUOYXM>{3}</CAOZUOYXM><CAOZUORQ>2017-02-06 14:35:30</CAOZUORQ><XITONGBS>0102</XITONGBS><FENYUANDM>1</FENYUANDM></BASEINFO><orderno>{0}</orderno><batchno>{1}</batchno></TUIFEI_IN>", orderno, new QueryInfoBll().GetBatchNoByOrderNo(orderno), CAOZUOYDM, CAOZUOYXM);
                string moth = "HIS4.TUIFEI";
                ggjk.RunService(moth, f, ref aa);
                doc.LoadXml(aa);

                log4net.Config.XmlConfigurator.Configure();//系统日志配置
                ILog logger = LogManager.GetLogger("log");

                logger.Error(doc.OuterXml);

                QueryInfoBll queryInfoBll = new QueryInfoBll();
                if (doc.SelectSingleNode("//msg").InnerText == "success")
                {
                    queryInfoBll.UpdateTFStatusByOrderno(orderno, "1");
                    if (doc.SelectSingleNode("//xzfje").InnerText == "0" || doc.SelectSingleNode("//xzfje").InnerText == "0.00")
                    {
                        queryInfoBll.UpdateTFStatusByOrderno(orderno, "2");
                    }
                }
                else
                {
                    queryInfoBll.UpdateTFStatusByOrderno(orderno, "-1");
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 确认缴费-His（FY030306）联众webservice接口
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPaymentInfo3(ParameterHandler handler)
        {
            string orderno = handler.getNotNullString("orderno");
            string paymenttype = handler.getNotNullString("paytype");

            orderno = orderno.ToUpper().Replace("APP", "");
            DingDanInfo entity = new QueryInfoBll().GetInfoByOrderno(orderno);
            XmlDocument doc = new XmlDocument();
            string jzid = "";
            string shdzid = "";
            string xzfje = "";
            string qjfs = "";
            if (entity != null)
            {
                jzid = entity.Jzid;
                shdzid = entity.shdzid;
                xzfje = entity.Je;
                qjfs = entity.Qjfs;
            }
            else
            {
            }
            try
            {

                string CAOZUOYDM = "ZFB";
                string CAOZUOYXM = "ZFB";
                ServiceReference1.TradeServerGGJKClient ggjk = new ServiceReference1.TradeServerGGJKClient();
                string aa = "";
                string outS = "";
                bool ret = false;
                int outInt = 0;
                string f = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><JIESUAN_IN><BASEINFO><CAOZUOYDM>{6}</CAOZUOYDM><CAOZUOYXM>{7}</CAOZUOYXM><CAOZUORQ>2017-02-06 14:35:30</CAOZUORQ><XITONGBS>0102</XITONGBS><FENYUANDM>1</FENYUANDM></BASEINFO><JZID>{0}</JZID><shdzid>{1}</shdzid><orderno>{2}</orderno><paymenttype>{3}</paymenttype><xzfje>{4}</xzfje><qjfs>{5}</qjfs></JIESUAN_IN>", jzid, new QueryInfoBll().GetShdzInfoById(shdzid), orderno, paymenttype, xzfje, qjfs, CAOZUOYDM, CAOZUOYXM);
                string moth = "HIS4.JIESUAN";
                ggjk.RunService(moth, f, ref aa);
                doc.LoadXml(aa);
                QueryInfoBll queryInfoBll = new QueryInfoBll();

                if (doc.SelectSingleNode("//msg").InnerText == "success")
                {
                    queryInfoBll.EditOrderStatus(orderno, "2");
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetWaitXml("程序未响应，请等待...");
            }
            return doc;
        }

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryAppointment(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string openid = handler.getNotNullString("openid");
            string status = handler.getNotNullString("status");
            string sourcetype = handler.getNotNullString("sourcetype");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryAppointment(openid, status, sourcetype);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 预约详情接口
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryDetailAppointmentInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string preengageseq = handler.getNotNullString("preengageseq");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryDetailAppointment(preengageseq);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 根据支付宝医院订单号返回支付宝交易号(PO010102)  退费his
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryChargeNumber(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            string paytype = handler.getString("paytype");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryChargeNumber(orderno.ToUpper().Replace("APP", ""), paytype);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 根据预约流水号进行预挂号获取支付金额( OR030204  )联众webservice接口
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPayMoneyList(ParameterHandler handler)
        {
            XmlDocument doc = new XmlDocument();
            string preengageseq = handler.getNotNullString("preengageseq");
            try
            {
                ServiceReference1.TradeServerGGJKClient ggjk = new ServiceReference1.TradeServerGGJKClient();
                string aa = "";
                string outS = "";
                bool ret = false;
                int outInt = 0;
                string CAOZUOYDM = "ZFB";
                string CAOZUOYXM = "ZFB";

                string f = "<?xml version=\"1.0\" encoding=\"utf-8\"?><YUGUAHAO_IN><BASEINFO><CAOZUOYDM>" + CAOZUOYDM + "</CAOZUOYDM><CAOZUOYXM>" + CAOZUOYXM + "</CAOZUOYXM><CAOZUORQ>2017-02-06 14:35:30</CAOZUORQ><XITONGBS>0102</XITONGBS><FENYUANDM>1</FENYUANDM></BASEINFO><preengageseq>" + preengageseq + "</preengageseq></YUGUAHAO_IN>";
                string moth = "HIS4.YUGUAHAO";
                ggjk.RunService(moth, f, ref aa);
                doc.LoadXml(aa.Replace("alipaymoney", "xzfje"));
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        ///根据医院订单号进行挂号落地(OR030205)联众webservice接口
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPayMoneyList2(ParameterHandler handler)
        {
            XmlDocument doc = new XmlDocument();
            string preengageseq = handler.getNotNullString("preengageseq");
            string orderno = handler.getNotNullString("orderno");
            string paymenttype = handler.getNotNullString("paymenttype");
            string xzfje = handler.getNotNullString("xzfje");
            try
            {
                ServiceReference1.TradeServerGGJKClient ggjk = new ServiceReference1.TradeServerGGJKClient();
                string aa = "";
                string outS = "";
                bool ret = false;
                int outInt = 0;
                string CAOZUOYDM = "ZFB";
                string CAOZUOYXM = "ZFB";
                string yunyiybz = "1";


                string f = "<?xml version=\"1.0\" encoding=\"utf-8\"?><GUAHAO_IN><BASEINFO><CAOZUOYDM>" + CAOZUOYDM + "</CAOZUOYDM><CAOZUOYXM>" + CAOZUOYXM + "</CAOZUOYXM><CAOZUORQ>2017-02-06 14:35:30</CAOZUORQ><XITONGBS>0102</XITONGBS><FENYUANDM>1</FENYUANDM></BASEINFO><preengageseq>" + preengageseq + "</preengageseq><orderno>" + orderno.ToUpper().Replace("APP", "") + "</orderno><paymenttype>" + paymenttype + "</paymenttype><xzfje>" + xzfje + "</xzfje><yunyiybz>" + yunyiybz + "</yunyiybz></GUAHAO_IN>";
                string moth = "HIS4.GUAHAO";
                ggjk.RunService(moth, f, ref aa);
                doc.LoadXml(aa);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }


        /// <summary>
        ///根据挂号id进行(取消已付款预约记录)退号(OR030206)
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPayMoneyList3(ParameterHandler handler)
        {
            XmlDocument doc = new XmlDocument();
            string registerid = handler.getNotNullString("registerid");
            string orderNo = new QueryInfoBll().GetOrderNoByRegisterId(registerid);
            string batchno = new QueryInfoBll().GetBatchNoByOrderNo(orderNo);
            try
            {
                ServiceReference1.TradeServerGGJKClient ggjk = new ServiceReference1.TradeServerGGJKClient();
                string aa = "";
                string outS = "";
                bool ret = false;
                int outInt = 0;
                string CAOZUOYDM = "ZFB";
                string CAOZUOYXM = "ZFB";

                string f = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><TUIHAO_IN><BASEINFO><CAOZUOYDM>" + CAOZUOYDM + "</CAOZUOYDM><CAOZUOYXM>" + CAOZUOYXM + "</CAOZUOYXM><CAOZUORQ>2017-02-06 14:35:30</CAOZUORQ><XITONGBS>0102</XITONGBS><FENYUANDM>1</FENYUANDM></BASEINFO><preengageseq>1234</preengageseq><registerid>{0}</registerid><batchno>{1}</batchno></TUIHAO_IN>", registerid, batchno);
                string moth = "HIS4.TUIHAO";
                ggjk.RunService(moth, f, ref aa);
                doc.LoadXml(aa);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            QueryInfoBll queryInfoBll = new QueryInfoBll();
            if (doc.SelectSingleNode("//msg").InnerText == "success")
            {
                if (doc.SelectSingleNode("//xzfje").InnerText == "0" || doc.SelectSingleNode("//xzfje").InnerText == "0.00")
                {
                    queryInfoBll.UpdateTfddStatus(batchno, "2");
                }
                else
                {
                    queryInfoBll.UpdateTfddStatus(batchno, "1");
                }
            }
            else
            {
                queryInfoBll.UpdateTfddStatus(batchno, "-1");
            }
            return doc;
        }

        /// <summary>
        /// 可缴费列表（FY030307）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPaymentList(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string brid = handler.getNotNullString("brid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryPaymentList(brid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }


        /// <summary>
        /// 可退费列表（FY030311）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryRefundList(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string brid = handler.getNotNullString("brid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryRefundList(brid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 更新订单状态（FY030308）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns>0：修改失败，1：修改成功</returns>
        public static XmlDocument EditOrderStatus(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            string status = handler.getNotNullString("status");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.EditOrderStatus(orderno, status);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 获取病人信息（UI020202）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPaitntInfoByBrid(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string brid = handler.getNotNullString("brid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryPatientInfoByBrid(brid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }


        /// <summary>
        /// 获取缴费病人信息（FY030309）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryPayPatientInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryPayPatientInfo(orderno);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 线程获取缴费表的订单状态为0的订单（FY030314）
        /// </summary>
        /// <returns></returns>
        public static XmlDocument QueryOrderList1(ParameterHandler handler)
        {
            XmlDocument doc = null;
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryOrderList1();
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 线程获取缴费表的订单状态为1的订单（FY030315）
        /// </summary>
        /// <returns></returns>
        public static XmlDocument QueryOrderList2(ParameterHandler handler)
        {
            XmlDocument doc = null;
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryOrderList2();
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 线程获取退费表的订单状态为0的订单（FY030316）
        /// </summary>
        /// <returns></returns>
        public static XmlDocument QueryReturnList1(ParameterHandler handler)
        {
            XmlDocument doc = null;
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryReturnList1();
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 线程获取退费表的订单状态为1的订单（FY030318）
        /// </summary>
        /// <returns></returns>
        public static XmlDocument QueryReturnList2(ParameterHandler handler)
        {
            XmlDocument doc = null;
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryReturnList2();
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 更新退费表的订单状态（FY030317）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns>0：修改失败，1：修改成功</returns>
        public static XmlDocument EditReturnStatus(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string batchno = handler.getNotNullString("batchno");
            string status = handler.getNotNullString("status");
            string notifytime = handler.getNotNullString("notifytime");
            string successnum = handler.getNotNullString("successnum");
            string paymentparameters = handler.getNotNullString("paymentparameters");
            string paymentbatchno = handler.getNotNullString("paymentbatchno");
            string money = handler.getNotNullString("money");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.EditReturnStatus(batchno, status, notifytime, successnum, paymentbatchno, paymentparameters, money);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 获取收费记录列表（FY030320）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryChargeList(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string brid = handler.getNotNullString("brid");
            string openid = handler.getNotNullString("openid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryChargeList(brid, openid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
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
        public static XmlDocument UpdateOrInsertReceiverInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string type = handler.getNotNullString("type");
            string shdzid = "";
            string openid = "";
            if (type == "update")
            {
                shdzid = handler.getNotNullString("shdzid");
            }
            openid = handler.getNotNullString("openid");
            string sjrxm = handler.getNotNullString("sjrxm");
            string lxdh = handler.getNotNullString("lxdh");
            string sjdz = handler.getNotNullString("sjdz");
            string mrbz = handler.getString("mrbz");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.UpdateOrInsertReceiverInfo(type, openid, shdzid, sjrxm, lxdh, sjdz, mrbz);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 删除收货人信息（FY030322）
        /// </summary>
        /// <param name="shdzid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static XmlDocument DeleteReceiverInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string shdzid = handler.getNotNullString("shdzid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.DeleteReceiverInfo(shdzid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-处方单列表（FY030323）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetChuFangListByQuery(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string brid = handler.getNotNullString("brid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetChuFangListByQuery(brid);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-处方单详情（FY030324）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetChuFangDetailByQuery(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string cflsh = handler.getNotNullString("cflsh");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetChuFangDetailByQuery(cflsh);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检验单详情基本信息（FY030326）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetJyDetailInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string jydlsh = handler.getNotNullString("jydlsh");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetJyDetailInfo(jydlsh);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检验单详情检验项目列表（FY030327）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetJyDetailInfo2(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string jydlsh = handler.getNotNullString("jydlsh");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetJyDetailInfo2(jydlsh);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检查单详情基本信息（FY030328）
        /// </summary>
        /// <param name="jydlsh">检查单流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static XmlDocument GetJyBasicInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string jcdlsh = handler.getNotNullString("jcdlsh");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetJyBasicInfo(jcdlsh);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检查单详情检查项目列表（FY030329）
        /// </summary>
        /// <param name="jcdlsh"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static XmlDocument GetJyInfoDetailList(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string jcdlsh = handler.getNotNullString("jcdlsh");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetJyInfoDetailList(jcdlsh);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 判断是否可退费（FY030330）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static XmlDocument IsTuiFeiState(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.IsTuiFeiState(orderno);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 更新退费订单状态(FY040408)
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument UpdateTfddStatus(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string batchno = handler.getNotNullString("batchno");
            string status = handler.getNotNullString("status");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.UpdateTfddStatus(batchno, status);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }


        /// <summary>
        /// 医生信息列表_按医生代码查(DO010104)
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static XmlDocument QueryDoctorList(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string namepy = "";
            string haskey = handler.getNotNullString("haskey");
            if (haskey == "0")
            {
                namepy = handler.getNotNullString("namepy");
            }
            int pageindex = 1;
            int pagesize = 10;
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryDoctorList(namepy, haskey, pageindex, pagesize);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检验单或检查单列表（FY030325）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryJyOrJcInfo(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string brid = handler.getNotNullString("brid");
            string type = handler.getNotNullString("type");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.GetJyInfoList(brid, type);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }


        /// <summary>
        ///  查询模块——缴费退费清单列表（FY030331）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryChargeListByOrderno31(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryChargeListByOrderno31(orderno);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }


        /// <summary>
        ///  查询模块——缴费退费清单明细（FY030332）
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryChargeListByOrderno32(ParameterHandler handler)
        {
            XmlDocument doc = null;
            string orderno = handler.getNotNullString("orderno");
            string fpxmid = handler.getNotNullString("fpxmid");
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                doc = queryInfoBll.QueryChargeListByOrderno32(fpxmid, orderno);
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        private static ParameterHandler parameterHanding(string paramxml)
        {
            if (string.IsNullOrEmpty(paramxml))
            {
                return null;
            }
            XmlDocument doc = XmlHelper.CreateXmlDocument(paramxml);
            XmlNodeList childList = doc.DocumentElement.ChildNodes;

            Dictionary<String, String> dic = new Dictionary<string, string>();
            foreach (XmlNode child in childList)
            {
                string tagname = child.Name;
                string tagvalue = child.InnerText/*.Trim ()*/;
                if (tagvalue == "null" || tagvalue == "NULL")
                {
                    tagvalue = null;
                }
                dic.Add(tagname, tagvalue);
            }

            ParameterHandler handler = new ParameterHandler(dic);
            return handler;
        }

        #endregion

    }

}