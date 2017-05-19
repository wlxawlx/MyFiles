using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using HospitaPaymentService.Wzszhjk.BLL;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Common.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL.Alipay
{
    public class MzMedicalRecordsLogic : BaseLogic
    {
        /// <summary>
        /// 门诊就诊病历列表，电子病历
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <returns></returns>
        public XmlDocument mzMedicalRecordsList(string patientid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                ArrayList list;
                //以下实现数据操作逻辑
                MzMedicalRecordsDB pdb = new MzMedicalRecordsDB();
                int ret = pdb.DB_mzMedicalRecordsList(patientid, out list, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (mzMedicalRecords ri in list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
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
        /// 电子病历内容
        /// </summary>
        /// <param name="jzxh">就诊序号</param>
        /// <returns></returns>
        public XmlDocument ElectronicMedicalRecordt(string jzxh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                mzMedicalRecords ri;
                //以下实现数据操作逻辑
                MzMedicalRecordsDB pdb = new MzMedicalRecordsDB();
                int ret = pdb.DB_ElectronicMedicalRecordt(jzxh, out ri, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
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
        /// 门诊指引单
        /// </summary>
        /// <param name="jzxh">就诊序号</param>
        /// <returns></returns>
        public XmlDocument mzSingleGuideAndTakeMedicine(string jzxh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                ArrayList list;
                //以下实现数据操作逻辑
                MzMedicalRecordsDB pdb = new MzMedicalRecordsDB();
                int ret = pdb.DB_mzSingleGuideAndTakeMedicine(jzxh, out list, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (mzMedicalRecords ri in list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
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
    }
}