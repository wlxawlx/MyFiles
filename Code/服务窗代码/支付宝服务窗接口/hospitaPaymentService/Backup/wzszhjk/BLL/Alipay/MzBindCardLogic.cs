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
    public class MzBindCardLogic : BaseLogic
    {
        /// <summary>
        /// 获得门诊卡列表
        /// </summary>
        /// <param name="idcardno">身份证号</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public XmlDocument GetmzkListStr(string idcardno, string name)
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
                MzBindCardDB pdb = new MzBindCardDB(); 
              
                int ret = pdb.DB_GetmzkListStr(idcardno, name, out list, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (PatientInfo patientInfo in list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml= XMLHelper.SerializeClassFileds(patientInfo.GetType(), patientInfo);
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
        /// 获得门诊卡信息_病人ID
        /// </summary>
        /// <param name="patientid">病人ID</param>
        /// <returns></returns>
        public XmlDocument GetmzkInforStr(string patientid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                PatientInfo patientInfo;
                //以下实现数据操作逻辑
                MzBindCardDB pdb = new MzBindCardDB();

                int ret = pdb.DB_GetmzkInforStr(patientid, out patientInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml= XMLHelper.SerializeClassFileds(patientInfo.GetType(), patientInfo);
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
        /// 本人门诊卡绑定
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="cardno">就诊卡号</param>
        /// <param name="patientid">病人ID</param>
        /// <returns></returns>
        public XmlDocument UsermzkBindStr(string openid, string cardno, string patientid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                PatientInfo patientInfo;

                //以下实现数据操作逻辑
                MzBindCardDB pdb = new MzBindCardDB();
                int ret = pdb.DB_UsermzkBindStr(openid, cardno, patientid, out patientInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml= XMLHelper.SerializeClassFileds(patientInfo.GetType(), patientInfo);
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
        /// 本人门诊卡解绑
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <returns></returns>
        public XmlDocument UsermzkRelieveBindStr(string openid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                PatientInfo patientInfo;

                //以下实现数据操作逻辑
                MzBindCardDB pdb = new MzBindCardDB();
                int ret = pdb.DB_UsermzkRelieveBindStr(openid, out patientInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eleCardno = doc.CreateElement(AppUtils.Tag_User_CardNo);
                    eleValue.AppendChild(eleCardno);
                    eleCardno.InnerText = patientInfo.bkhm;
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
        /// 常用联系人门诊卡绑定
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="linkmanid">联系人ID</param>
        /// <param name="cardno">就诊卡号</param>
        /// <param name="patientid">病人ID</param>
        /// <returns></returns>
        public XmlDocument FavoriteContactsBindStr(string openid, string linkmanid, string cardno, string patientid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                PatientInfo patientInfo;

                //以下实现数据操作逻辑
                MzBindCardDB pdb = new MzBindCardDB();
                int ret = pdb.DB_FavoriteContactsBindStr(openid, linkmanid, cardno, patientid, out patientInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eleCardtype = doc.CreateElement(AppUtils.Tag_Patient_CardType);
                    eleValue.AppendChild(eleCardtype);
                    eleCardtype.InnerText = patientInfo.bklx;

                    XmlElement eleCardname = doc.CreateElement(AppUtils.Tag_Patient_CardName);
                    eleValue.AppendChild(eleCardname);
                    eleCardname.InnerText = patientInfo.cardname;

                    XmlElement eleCardno = doc.CreateElement(AppUtils.Tag_User_CardNo);
                    eleValue.AppendChild(eleCardno);
                    eleCardno.InnerText = patientInfo.bkhm;
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
        /// 常用联系人门诊卡解绑
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="linkmanid">联系人ID</param>

        /// <returns></returns>
        public XmlDocument FavoriteContactsrmzkRelieveBindStr(string openid, string linkmanid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string error_msg;
                PatientInfo patientInfo;

                //以下实现数据操作逻辑
                MzBindCardDB pdb = new MzBindCardDB();
                int ret = pdb.DB_FavoriteContactsrmzkRelieveBindStr(openid, linkmanid, out patientInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eleCardno = doc.CreateElement(AppUtils.Tag_User_CardNo);
                    eleValue.AppendChild(eleCardno);
                    eleCardno.InnerText = patientInfo.bkhm;
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