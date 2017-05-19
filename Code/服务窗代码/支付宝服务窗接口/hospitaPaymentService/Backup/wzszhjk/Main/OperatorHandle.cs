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
            else if (!string.IsNullOrEmpty(brid) && !string.IsNullOrEmpty(brlx) &&
                     !string.IsNullOrEmpty(payType) && czje > 0)
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
        /// 手机确认订单
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
        /// 插入银行记录(商户通知）
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

            if (info.yylsh > 0 && !string.IsNullOrEmpty(info.queryId) &&
                !string.IsNullOrEmpty(info.paytype) && Convert.ToDouble(info.txnAmt) > 0 &&
                !string.IsNullOrEmpty(info.respCode))
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
        /// 用户信息
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
        ///  医生排班信息
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
        ///  门诊号源信息
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
        /// <param name="paramxml"></param>
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
            }
            catch (ArgErrorException ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_mzCreateOrder + ex + "参数不完整，无法提交");
            }
            AlipayPaymentLogic pm = new AlipayPaymentLogic();
            double tkje = 0;
            string patienttype = "1";
            rtdoc = pm.CreateOrder(openid, patientname, patientcardno, cardno, patientid, subject, money, tkje, patienttype);

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
        /// 商户通知门诊预缴成功
        /// </summary>
        /// <param name="paramxml"></param>
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
            rtdoc = pm.CreateOrder(openid, patientname, patientcardno, "", inpatientno, subject, money, tkje, patienttype);
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
        /// 商户通知住院预缴成功
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
        /// 判断是否有云医院资格
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument IsHasYunHospital(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            try
            {
                string patientId = handler.getNotNullString(AppUtils.Tag_Patient_BRID);
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.QuesyPatientYunHospital(patientId);
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }


        /// <summary>
        /// 根据openid获取联系人信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument QueryConnectPerson(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;
            string openid = handler.getNotNullString(AppUtils.Tag_User_OpenID);
            try
            {
                QueryInfoBll queryInfoBll = new QueryInfoBll();
                rtdoc = queryInfoBll.QueryConnectPerson(openid);
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Command_Alipay_zyPatientInfor + ex + "参数不完整，无法提交");
            }
            return rtdoc;
        }

        /// <summary>
        /// 退费清单处方单信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XmlDocument GetChuFangList(ParameterHandler handler)
        {
            XmlDocument rtdoc = null;

            string chufangId = handler.getNotNullString(AppUtils.Tag_User_ChuFangID);
            QueryInfoBll queryInfoBll = new QueryInfoBll();

            rtdoc = queryInfoBll.GetChuFangList(chufangId);

            return rtdoc;
        }
        #endregion

    }

}