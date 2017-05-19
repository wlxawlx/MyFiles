using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using HospitaPaymentService.Wzszhjk.DAL.Webservice;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Common.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL.Alipay
{
    public class InspectionCheckLogic : BaseLogic
    {
        /// <summary>
        /// 化验报告单列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idcardno">身份证号</param>
        /// <returns></returns>
        public XmlDocument LaboratoryTestsReportList(string name, string idcardno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;

                AilpayQueryReportDB pdb = new AilpayQueryReportDB();
                ret = pdb.DB_AilpayQueryReport(name, idcardno, out values, out error_msg);

                if (ret == 0 && null != values && values.Count > 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayReportList ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
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
        /// 一个化验报告单的抬头信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <returns></returns>
        public XmlDocument LaboratoryTestsReportNameInformation(string doctadviseno)
        {

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                AilpayQueryReportDB pdb = new AilpayQueryReportDB();
                string error_msg;
                AlipayReportInfo alipayReportInfo;
                int ret = pdb.DB_LaboratoryTestsReportNameInformation(doctadviseno, out alipayReportInfo, out error_msg);
                if (ret == 0)
                {

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(alipayReportInfo.GetType(), alipayReportInfo);
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
        /// 一个化验报告单详细列表信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <returns></returns>
        public XmlDocument LaboratoryTestsReportDetailedListInformation(string doctadviseno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;

                AilpayQueryReportDB pdb = new AilpayQueryReportDB();
                ret = pdb.DB_LaboratoryTestsReportDetailedListInformation(doctadviseno, out values, out error_msg);

                if (ret == 0 && null != values && values.Count > 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayReportdetailInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
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
        /// 检验报告单列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idcardno">身份证号</param>
        /// <returns></returns>
        public XmlDocument InspectionReportList(string name, string idcardno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;

                AilpayQueryReportDB pdb = new AilpayQueryReportDB();
                ret = pdb.DB_InspectionReportList(name, idcardno, out values, out error_msg);

                if (ret == 0 && null != values && values.Count > 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayReportList ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
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
        /// 一个检验报告单的抬头信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <returns></returns>
        public XmlDocument InspectionReportNameInformation(string doctadviseno)
        {

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                AilpayQueryReportDB pdb = new AilpayQueryReportDB();
                string error_msg;
                AlipayReportInfo alipayReportInfo;
                int ret = pdb.DB_InspectionReportNameInformation(doctadviseno, out alipayReportInfo, out error_msg);
                if (ret == 0)
                {

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(alipayReportInfo.GetType(), alipayReportInfo);

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
        /// 一个检验报告单详细列表信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <returns></returns>
        public XmlDocument InspectionReportResultsInformation(string doctadviseno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                AilpayQueryReportDB pdb = new AilpayQueryReportDB();
                string error_msg;
                AlipayInspectionReport alipayInspectionReport;
                int ret = pdb.DB_InspectionReportResultsInformation(doctadviseno, out alipayInspectionReport, out error_msg);
                if (ret == 0)
                {

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(alipayInspectionReport.GetType(), alipayInspectionReport);

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

    }
}