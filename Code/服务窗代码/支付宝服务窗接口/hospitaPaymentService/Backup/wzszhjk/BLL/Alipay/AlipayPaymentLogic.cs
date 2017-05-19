using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Common.Xml;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Common.Webservice;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.String;


namespace HospitaPaymentService.Wzszhjk.BLL.Alipay
{
    public class AlipayPaymentLogic : BaseLogic
    {
        /// <summary>
        /// 创建预存订单
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="patientname">病人姓名</param>
        /// <param name="patientidcardno">病人身份证号</param>
        /// <param name="cardno">就诊卡卡号</param>
        /// <param name="patientid">病人ID</param>
        /// <param name="subject">标题</param>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public XmlDocument CreateOrder(string openid, string patientname, string patientidcardno, string cardno, string patientid, string subject,
            double money, double tkje, string patienttype)
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
                long tradeno = -1;

                AilpayPaymentDB pdb = new AilpayPaymentDB();
                int ret = pdb.DB_CreateOrder(openid, patientname, patientidcardno, cardno, patientid, subject, money, tkje, patienttype,
                    out tradeno, out error_msg);

                if (ret == 0)
                {
                    //正常返回
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eletradeno = doc.CreateElement(AppUtils.Tag_User_Tradeno);
                    eleValue.AppendChild(eletradeno);
                    eletradeno.InnerText = StringHelper.YylshHasPrefix(tradeno);
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
        /// 预存订单列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <returns></returns>
        public XmlDocument PredepositList(string openid, string brlx)
        {
            AilpayPaymentDB pdb = new AilpayPaymentDB();
            string msg;
            ArrayList values;
            int ret = pdb.DB_PredepositList(openid, brlx, out values, out msg);
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

                    foreach (OrderInfoForAlipay ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);                    
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
        /// 取消预存订单
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="patientname">病人姓名</param>
        /// <param name="patientid">病人ID</param>
        /// <param name="tradeno">订单号</param>
        /// <returns></returns>
        public XmlDocument CancelPredepositList(string openid, string patientname, string patientid, long yylsh)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                string msg = "";

                AilpayPaymentDB pdb = new AilpayPaymentDB();
                int ret = pdb.DB_CancelPredepositList(openid, patientname, patientid, yylsh, out msg);

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

                    XmlElement eleYylsh = doc.CreateElement(AppUtils.Tag_User_Tradeno);
                    eleValue.AppendChild(eleYylsh);
                    eleYylsh.InnerText = StringHelper.YylshHasPrefix(yylsh);
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
        /// 商户通知预存成功
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="patientname">病人姓名</param>
        /// <param name="patientid">病人ID</param>
        /// <param name="tradeno">订单号</param>
        /// <param name="paymenttradeno">支付平台订单号</param>
        /// <param name="money">金额</param>
        /// <param name="paymentparameters">通知参数</param>
        /// <returns></returns>
        public XmlDocument PredepositSuccess(AlipayReplyInfo info, string brlx)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                //以下实现数据操作逻辑
                AilpayPaymentDB pdb = new AilpayPaymentDB();
                string error_msg;
                int ret = pdb.DB_InsertAlipay(info, brlx, out error_msg);
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

                    XmlElement eleYylsh = doc.CreateElement(AppUtils.Tag_User_Tradeno);
                    eleValue.AppendChild(eleYylsh);
                    eleYylsh.InnerText = StringHelper.YylshHasPrefix(info.tradeno);
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
        /// 住院病人信息
        /// </summary>
        /// <param name="idcardno">身份证号</param>
        /// <param name="name">病人姓名</param>
        /// <param name="paymentparameters">通知参数</param>
        /// <returns></returns>
        public XmlDocument zyPatientInfor(string idcardno, string name)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                zyPatientInfo patientInfo;

                //以下实现数据操作逻辑
                AilpayPaymentDB pdb = new AilpayPaymentDB();
                int ret = pdb.DB_zyPatientInfo(idcardno, name, out patientInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(patientInfo.GetType(), patientInfo);
                    eleMsg.AppendChild(eleValue);
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
        /// 住院费用信息
        /// </summary>
        /// <param name="inpatientno ">住院号码</param>
        /// <param name="costdate ">费用日期 </param>
        /// <param name="paymentparameters">通知参数</param>
        /// <returns></returns>
        public XmlDocument zyCostInfo(string inpatientno, string costdate)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                ArrayList values;

                //以下实现数据操作逻辑
                AilpayPaymentDB pdb = new AilpayPaymentDB();
                int ret = pdb.DB_zyCostInfo(inpatientno, costdate, out values, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (zyCostInfo cost in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(cost.GetType(), cost);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }
    }
}