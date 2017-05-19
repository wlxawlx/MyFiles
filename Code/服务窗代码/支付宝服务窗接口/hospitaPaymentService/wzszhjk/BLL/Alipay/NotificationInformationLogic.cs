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
    public class NotificationInformation : BaseLogic
    {

        /// <summary>
        /// 通知病人就诊信息
        /// <returns></returns>
        public XmlDocument InformPatient()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string msg = "";
                ArrayList values = new ArrayList();

                NotificationInformationDB pdb = new NotificationInformationDB();
                int ret = pdb.DB_InformPatient(out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (InformPatientInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);


                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
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
        /// 核对未到账的充值缴费信息
        /// <returns></returns>
        public XmlDocument CheckInformation()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string msg = "";
                ArrayList values = new ArrayList();

                NotificationInformationDB pdb = new NotificationInformationDB();
                int ret = pdb.DB_CheckInformation(out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);
                    foreach (CheckInformation ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
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
    }


}