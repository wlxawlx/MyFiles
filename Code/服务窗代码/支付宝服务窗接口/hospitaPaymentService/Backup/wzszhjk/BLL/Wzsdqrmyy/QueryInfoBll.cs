using HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy;
using HospitaPaymentService.Wzszhjk.Common.Xml;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace HospitaPaymentService.wzszhjk.BLL.Wzsdqrmyy
{
    /// <summary>
    /// 第七人民医院新增接口
    /// </summary>
    public class QueryInfoBll
    {
        /// <summary>
        /// 是否具有云医院的资格
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public XmlDocument QuesyPatientYunHospital(string patientId)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                //以下实现数据操作逻辑
                QueryInfoDal dal = new QueryInfoDal();
                string error_msg;
                int ret = dal.QueryIsHasYunHospital(patientId, out error_msg);
                if (ret > 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    root.AppendChild(eleValue);

                    XmlElement eleIsRepeat = doc.CreateElement("isrepeat");
                    eleIsRepeat.InnerText = error_msg;
                    eleMsg.AppendChild(eleIsRepeat);
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
        /// 获取联系人信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public XmlDocument QueryConnectPerson(string openid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                //以下实现数据操作逻辑
                QueryInfoDal dal = new QueryInfoDal();
                ArrayList values = null;
                string error_msg = "";
                int ret = dal.QueryConnectPerson(openid, out values, out error_msg);
                if (ret == 1)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (var item in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        root.AppendChild(eleValue);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
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
        /// 
        /// </summary>
        /// <param name="sfid"></param>
        /// <returns></returns>
        public XmlDocument GetChuFangList(string sfid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetChuFangList(sfid, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);
                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            return doc;
        }

        /// <summary>
        /// 错误或者异常返回 信息
        /// </summary>
        /// <param name="result">返回值 不等于0</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        protected XmlDocument ErrorReturnXml(int result, string errorMsg)
        {
            XmlDocument rtDoc = new XmlDocument();

            if (result > 0)
            {
                rtDoc = ReplyXmlDoc.GetFailureXML(result, errorMsg);
            }
            else
            {
                rtDoc = ReplyXmlDoc.GetExceptionXML(result, errorMsg);
            }

            return rtDoc;
        }
    }
}