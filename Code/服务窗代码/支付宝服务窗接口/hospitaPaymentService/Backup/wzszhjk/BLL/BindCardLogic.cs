using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Model;

namespace HospitaPaymentService.Wzszhjk.BLL
{
    public class BindCardLogic : BaseLogic
    {
        /// <summary>
        /// 查询绑卡信息
        /// </summary>
        /// <param name="brxm">病人姓名</param>
        /// <param name="sfzh">身份证号</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <returns></returns>
        public XmlDocument QueryCard(string brxm, string sfzh, string brlx)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement root = xDoc.CreateElement(AppUtils.Tag_REXML_Root);
            xDoc.AppendChild(root);

            try
            {
                XmlElement eleMsg = xDoc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList _list;
                string msg;

                BindCardDB pdb = new BindCardDB();
                int ret = pdb.DB_QueryCard(brxm, sfzh, brlx, out _list, out msg);

                if (ret == 0)
                {
                    XmlElement eleResult = xDoc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (PatientInfo patientInfo in _list)
                    {
                        XmlElement eleValue = xDoc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleBrid = xDoc.CreateElement(AppUtils.Tag_Patient_BRID);
                        eleValue.AppendChild(eleBrid);
                        eleBrid.InnerText = patientInfo.brid;

                        XmlElement eleBkhm = xDoc.CreateElement(AppUtils.Tag_Patient_BKHM);
                        eleValue.AppendChild(eleBkhm);
                        eleBkhm.InnerText = patientInfo.bkhm;

                        XmlElement eleBklx = xDoc.CreateElement(AppUtils.Tag_Patient_BKLX);
                        eleValue.AppendChild(eleBklx);
                        eleBklx.InnerText = patientInfo.bklx;

                        XmlElement eleBrxm = xDoc.CreateElement(AppUtils.Tag_Patient_BRXM);
                        eleValue.AppendChild(eleBrxm);
                        eleBrxm.InnerText = patientInfo.brxm;

                        XmlElement eleSfzh = xDoc.CreateElement(AppUtils.Tag_Patient_SFZH);
                        eleValue.AppendChild(eleSfzh);
                        eleSfzh.InnerText = patientInfo.sfzh;

                        XmlElement eleJlsj = xDoc.CreateElement(AppUtils.Tag_Patient_JLSJ);
                        eleValue.AppendChild(eleJlsj);
                        if (patientInfo.jlsj.Equals(DateTime.MinValue))
                        {
                            eleJlsj.InnerText = "";
                        }
                        else
                        {
                            eleJlsj.InnerText = patientInfo.jlsj.ToString(AppUtils.DateTime_Format_All);
                        }

                        XmlElement eleLxdh = xDoc.CreateElement(AppUtils.Tag_Patient_LXDH);
                        eleValue.AppendChild(eleLxdh);
                        eleLxdh.InnerText = patientInfo.lxdh;

                        XmlElement eleJtdz = xDoc.CreateElement(AppUtils.Tag_Patient_JTDZ);
                        eleValue.AppendChild(eleJtdz);
                        eleJtdz.InnerText = patientInfo.jtdz;

                        XmlElement eleSzbq = xDoc.CreateElement(AppUtils.Tag_Patient_SZBQ);
                        eleValue.AppendChild(eleSzbq);
                        eleSzbq.InnerText = patientInfo.szbq;

                        XmlElement eleSzcw = xDoc.CreateElement(AppUtils.Tag_Patient_SZCW);
                        eleValue.AppendChild(eleSzcw);
                        eleSzcw.InnerText = patientInfo.szcw;

                        XmlElement eleZyh = xDoc.CreateElement(AppUtils.Tag_Patient_ZYH);
                        eleValue.AppendChild(eleZyh);
                        eleZyh.InnerText = patientInfo.zyh;
                    }
                }
                else
                {
                    xDoc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                xDoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return xDoc;
        }

        /// <summary>
        /// 绑卡操作
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="sfzh">身份证号</param>
        /// <param name="brxm">病人姓名</param>
        /// <param name="bklx">病人类别 "byk"-本院卡  "smk"-市民" "ybk"-医保卡</param>
        /// <param name="lxdh">联系电话</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <returns></returns>
        public XmlDocument BindCard(string brid, string sfzh, string brxm, string bklx, string lxdh, string brlx)
        {
            XmlDocument doc;
            try
            {
                //以下实现数据操作逻辑
                BindCardDB pdb = new BindCardDB();
                string error_msg;
                int ret = pdb.DB_BindCard(brid, brxm, sfzh, bklx, lxdh, brlx, out error_msg);
                if (ret == 0)
                {
                    doc = ReplyXmlDoc.GetSuccessXML(ret, error_msg);
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
        /// 卡是否有效
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="bkhm">卡号</param>
        /// <param name="brlx">病人类型 1-门诊  2-住院</param>
        /// <returns></returns>
        public XmlDocument VaildCard(string brid, string bkhm, string brlx)
        {
            XmlDocument doc;
            try
            {
                //以下实现数据操作逻辑
                BindCardDB pdb = new BindCardDB();
                string error_msg;
                int ret = pdb.DB_ValidCard(brid, bkhm, brlx, out error_msg);
                if (ret == 0)
                {
                    doc = ReplyXmlDoc.GetSuccessXML(ret, error_msg);
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