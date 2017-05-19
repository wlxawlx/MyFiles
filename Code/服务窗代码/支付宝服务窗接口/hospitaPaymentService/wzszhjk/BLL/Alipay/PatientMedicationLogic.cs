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
    public class PatientMedicationLogic : BaseLogic
    {
        /// <summary>
        /// 病人药品处方信息列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <returns></returns>
        public XmlDocument DrugPrescriptionInforList(string patientid)
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
                PatientMedicationDB pdb = new PatientMedicationDB();

                int ret = pdb.DB_DrugPrescriptionInforList(patientid, out list, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (Patientmedication ri in list)
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
        /// 病人药品服用信息
        /// </summary>
        /// <param name="cflsh">处方流水号</param>
        /// <returns></returns>
        public XmlDocument PatientsTakingDrugsInfor(string cflsh)
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
                PatientMedicationDB pdb = new PatientMedicationDB();

                int ret = pdb.DB_PatientsTakingDrugsInfor(cflsh, out list, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (Patientmedication ri in list)
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