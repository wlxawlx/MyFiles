using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

using HospitaPaymentService.Wzszhjk.Utils;

namespace HospitaPaymentService.Wzszhjk.Utils.Xml
{
    /// <summary>
    /// 用来获取返回报文的类
    /// </summary>
    public class ReplyXmlDoc
    {

        public static XmlDocument GetSuccessXML(int zt, string msg)
        {
            return GetNormalXml(AppUtils.Value_Return_Success, zt, msg);
        }

        public static XmlDocument GetExceptionXML(int zt, Exception ex)
        {
            return GetNormalXml(AppUtils.Value_Return_Exception, zt, ex.StackTrace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static XmlDocument GetExceptionXML(int zt, string msg)
        {
            return GetNormalXml(AppUtils.Value_Return_Exception, zt, msg);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="zt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static XmlDocument GetFailureXML(int zt, string msg)
        {
            return GetNormalXml(AppUtils.Value_Return_Failure, zt, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static XmlDocument GetFailXML(int zt, string msg)
        {
            return GetNormalXml("fail", zt, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static XmlDocument GetWaitXml(string remark)
        {
            XmlDocument rtdoc = new XmlDocument();
            XmlElement root = rtdoc.CreateElement(AppUtils.Tag_REXML_Root);
            rtdoc.AppendChild(root);

            XmlElement eleResult = rtdoc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = "success";
            root.AppendChild(eleResult);

            XmlElement message = rtdoc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(message);

            XmlElement value = rtdoc.CreateElement("value");
            message.AppendChild(value);

            XmlElement msg = rtdoc.CreateElement("msg");
            msg.InnerText = "wait";
            value.AppendChild(msg);

            XmlElement msg2 = rtdoc.CreateElement("msg2");
            msg2.InnerText = remark;
            value.AppendChild(msg2);

            return rtdoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static XmlDocument GetWaitXml2(string remark)
        {
            XmlDocument rtdoc = new XmlDocument();
            XmlElement root = rtdoc.CreateElement(AppUtils.Tag_REXML_Root);
            rtdoc.AppendChild(root);

            XmlElement eleResult = rtdoc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = "success";
            root.AppendChild(eleResult);

            XmlElement message = rtdoc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(message);

            XmlElement value = rtdoc.CreateElement("value");
            message.AppendChild(value);

            XmlElement msg = rtdoc.CreateElement("msg");
            msg.InnerText = "wait";
            value.AppendChild(msg);

            XmlElement msg2 = rtdoc.CreateElement("msg2");
            msg2.InnerText = remark;
            value.AppendChild(msg2);

            return rtdoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="zt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static XmlDocument GetNormalXml(string result, int zt, string msg)
        {
            XmlDocument rtdoc = new XmlDocument();
            XmlElement root = rtdoc.CreateElement(AppUtils.Tag_REXML_Root);
            rtdoc.AppendChild(root);

            XmlElement eleResult = rtdoc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = result;
            root.AppendChild(eleResult);

            XmlElement message = rtdoc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(message);

            XmlElement eleZt = rtdoc.CreateElement(AppUtils.Tag_REXML_ZT);
            eleZt.InnerText = zt.ToString();
            message.AppendChild(eleZt);

            XmlElement eleMs = rtdoc.CreateElement(AppUtils.Tag_REXML_MS);
            if (null != msg)
            {
                eleMs.InnerText = msg;
            }
            message.AppendChild(eleMs);

            return rtdoc;
        }

    }
}