using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HospitaPaymentService.Wzszhjk.Common.Xml
{
    public class XMLHelper
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToDocument(Type type, Object obj)
        {
            string result = "";

            try
            {
                MemoryStream stream = new MemoryStream();
                XmlSerializer xml = new XmlSerializer(type);
                //序列化对象
                xml.Serialize(stream, obj);

                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();

                reader.Dispose();
                stream.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        /// <summary>
        /// 序列化类的所有公有字段
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeClassFileds(Type type, Object obj)
        {
            string result = "";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(SerializeToDocument(type, obj));

                XmlNode node = doc.SelectSingleNode(obj.GetType().Name);
                result = node.InnerXml.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}