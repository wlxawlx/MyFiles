using System;
using System.Collections.Generic;
using System.Web;
using HospitaPaymentService.Wzszhjk.BLL;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using System.Collections;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Common.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL.Alipay
{
    public class DoctorInfoLogic : BaseLogic
    {
        /// <summary>
        /// 医生信息列表_按姓名查
        /// </summary>
        /// <param name="name">医生姓名</param>
        /// <param name="pageindex">访问页码</param>
        /// <param name="pagesize">每页行数</param>
        /// <returns></returns>
        public XmlDocument DctorInfoXingming(string name, int pageindex, int pagesize)
        {

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);


            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                DoctorInforDB pdb = new DoctorInforDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_DctorInfoXingming(name, pageindex, pagesize, out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayDoctorInfo ri in values)
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
        /// 医生信息列表_按姓名拼音首字母查
        /// </summary>
        /// <param name="namepy">医生姓名拼音</param>
        /// <param name="pageindex">访问页码</param>
        /// <param name="pagesize">每页行数</param>
        /// <returns></returns>
        public XmlDocument DoctorInfoPinYin(string namepy, int pageindex, int pagesize)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                DoctorInforDB pdb = new DoctorInforDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_DoctorInfoPinYin(namepy, pageindex, pagesize, out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayDoctorInfo ri in values)
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
        /// 医生信息列表_按医生代码查
        /// </summary>
        /// <param name="code">医生代码</param>
        /// <returns></returns>
        public XmlDocument DoctorInfoDaiMa(string code)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList _list = new ArrayList();
                string error_msg = "";

                DoctorInforDB pdb = new DoctorInforDB();
                int ret = pdb.DB_DoctorInfoDaiMa(code, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayDoctorInfo ri in _list)
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
        /// 医生停诊信息列表
        /// </summary>
        /// <param name="pageindex">访问页码</param>
        /// <param name="pagesize">每页行数</param>
        /// <returns></returns>
        public XmlDocument DoctorInfoTingZhen(int pageindex, int pagesize)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList _list = new ArrayList();
                string error_msg = "";

                DoctorInforDB pdb = new DoctorInforDB();
                int ret = pdb.DB_DoctorInfoTingZhen(pageindex, pagesize, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayDoctorInfoTingZhen ri in _list)
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