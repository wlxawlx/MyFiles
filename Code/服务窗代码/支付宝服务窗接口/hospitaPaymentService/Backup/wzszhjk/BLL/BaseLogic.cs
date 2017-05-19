using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Utils.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL
{
    public class BaseLogic
    {
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