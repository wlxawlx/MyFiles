using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using HospitaPaymentService.Wzszhjk.Utils;

namespace HospitaPaymentService.Wzszhjk.Utils.Xml
{
    public class XmlHelper
    {
        /// <summary>
        /// 构造xml文档
        /// </summary>
        /// <param name="str">序列化的xml语句</param>
        /// <returns></returns>
        public static XmlDocument CreateXmlDocument(string str)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(str);
                return doc;
            }
            catch (Exception)
            {
                //可以设置日志
                return null;
            }
        }
    }
}