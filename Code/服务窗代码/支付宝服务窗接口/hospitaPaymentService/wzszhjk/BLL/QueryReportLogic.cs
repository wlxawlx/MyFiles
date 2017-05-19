using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.WZSDSRMYY;
using HospitaPaymentService.Wzszhjk.DAL.Webservice;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL
{
    public class QueryReportLogic : BaseLogic
    {
        /// <summary>
        /// 根据病人id查询报告单列表
        /// </summary>
        /// <param name="brid">病人id</param>
        /// <param name="brlx">病人类型 1-门诊病人 2-住院病人</param>
        /// <param name="brxm">病人姓名</param>
        /// <returns></returns>
        public XmlDocument QueryReportList(string brid, string brlx)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;

                //根据医院名字调用不同的接口
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
                {
                    ReportDBForWZSDSRMYY pdb = new ReportDBForWZSDSRMYY();
                    ret = pdb.QueryReportListByBRID(brid, brlx, out values, out error_msg);
                }
                else
                {

                    QueryReportDB pdb = new QueryReportDB();
                    ret = pdb.QueryReportListByBRID(brid, brlx, out values, out error_msg);
                }

                if (ret == 0 && null != values && values.Count > 0)
                {

                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (ReportInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        string[] tags = {AppUtils.Tag_Payment_BGDH,AppUtils.Tag_Payment_SJMD,AppUtils.Tag_Payment_CJSJ,AppUtils.Tag_Payment_SJR,
                                        AppUtils.Tag_Payment_JYSJ,AppUtils.Tag_Payment_JYR,AppUtils.Tag_Payment_SHR,AppUtils.Tag_Payment_JZCH,
                                        AppUtils.Tag_Payment_ZDJG,AppUtils.Tag_Payment_BBMC,AppUtils.Tag_Payment_MZBZ,AppUtils.Tag_Payment_DYJB,
                                        AppUtils.Tag_Payment_BZ,AppUtils.Tag_Payment_HZHB,AppUtils.Tag_Query_SBM, AppUtils.Tag_Query_JGMC};

                        foreach (string tag in tags)
                        {
                            string value = ri.getValue(tag);
                            XmlElement eleTag = doc.CreateElement(tag);
                            eleTag.InnerText = (null != value) ? value : "";
                            eleValue.AppendChild(eleTag);
                        }
                        eleMsg.AppendChild(eleValue);
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

        public XmlDocument QueryReportListFromWebservice(string brxm, string lxdh)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList values = new ArrayList();
                string error_msg = "";
                string endTime = DateTime.Now.AddDays(1).ToString(AppUtils.DateTime_FormatNO_YearMonthDay);
                string startTime = DateTime.Now.AddDays(-60).ToString(AppUtils.DateTime_FormatNO_YearMonthDay);
                //DateTime endTime = DateTime.Now;
                //DateTime startTime = DateTime.Now.AddDays(-60);

                ReportInfoHandleForWZSZYY pdb = new ReportInfoHandleForWZSZYY();
                int ret = pdb.ReportInfoList(brxm, lxdh, startTime, endTime, out values, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (ReportInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        string[] tags = {AppUtils.Tag_Payment_BGDH,AppUtils.Tag_Payment_SJMD,AppUtils.Tag_Payment_CJSJ,AppUtils.Tag_Payment_SJR,
                                        AppUtils.Tag_Payment_JYSJ,AppUtils.Tag_Payment_JYR,AppUtils.Tag_Payment_SHR,AppUtils.Tag_Payment_JZCH,
                                        AppUtils.Tag_Payment_ZDJG,AppUtils.Tag_Payment_BBMC,AppUtils.Tag_Payment_MZBZ,AppUtils.Tag_Payment_DYJB,
                                        AppUtils.Tag_Payment_BZ,AppUtils.Tag_Payment_HZHB,AppUtils.Tag_Query_SBM, AppUtils.Tag_Query_JGMC};

                        foreach (string tag in tags)
                        {
                            string value = ri.getValue(tag);
                            XmlElement eleTag = doc.CreateElement(tag);
                            eleTag.InnerText = (null != value) ? value : "";
                            eleValue.AppendChild(eleTag);
                        }
                        eleMsg.AppendChild(eleValue);
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

        public XmlDocument QueryReportJCList(string brid, string brlx)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
                {
                    ReportDBForWZSDSRMYY pdb = new ReportDBForWZSDSRMYY();
                    ret = pdb.QueryReportJCListByBRID(brid, brlx, out values, out error_msg);
                }
                else
                {
                    QueryReportDB pdb = new QueryReportDB();
                    ret = pdb.DB_QueryReportJCListByBRID(brid, brlx, out values, out error_msg);
                }

                if (ret == 0)
                {

                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (ReportInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        string[] tags = {AppUtils.Tag_Payment_BGDH,AppUtils.Tag_Payment_SJMD,AppUtils.Tag_Payment_CJSJ,AppUtils.Tag_Payment_SJR,
                                        AppUtils.Tag_Payment_JYSJ,AppUtils.Tag_Payment_JYR,AppUtils.Tag_Payment_SHR,AppUtils.Tag_Payment_JZCH,
                                        AppUtils.Tag_Payment_ZDJG,AppUtils.Tag_Payment_BBMC,AppUtils.Tag_Payment_MZBZ,AppUtils.Tag_Payment_DYJB,
                                        AppUtils.Tag_Payment_BZ,AppUtils.Tag_Payment_HZHB,AppUtils.Tag_Query_SBM, AppUtils.Tag_Query_JGMC};

                        foreach (string tag in tags)
                        {
                            string value = ri.getValue(tag);
                            XmlElement eleTag = doc.CreateElement(tag);
                            eleTag.InnerText = (null != value) ? value : "";
                            eleValue.AppendChild(eleTag);
                        }
                        eleMsg.AppendChild(eleValue);
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
        /// 根据号码查询明细
        /// </summary>
        /// <param name="code">号码</param>
        /// <param name="lx">类型  1-按报告单号查询 2-按条码查</param>
        /// <param name="brxm">病人姓名</param>
        /// <returns></returns>
        public XmlDocument QueryReportDetailByCode(string code, string lx, string brxm)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
                {
                    ReportDBForWZSDSRMYY pdb = new ReportDBForWZSDSRMYY();
                    ret = pdb.QueryReportListByCode(code, lx, brxm, out values, out error_msg);
                }
                else
                {
                    QueryReportDB pdb = new QueryReportDB();
                    ret = pdb.QueryReportListByCode(code, lx, brxm, out values, out error_msg);
                }

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);
                    foreach (ReportInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        string[] tags = {AppUtils.Tag_Payment_BGDH,AppUtils.Tag_Payment_SJMD,AppUtils.Tag_Payment_CJSJ,AppUtils.Tag_Payment_SJR,
                                        AppUtils.Tag_Payment_JYSJ,AppUtils.Tag_Payment_JYR,AppUtils.Tag_Payment_SHR,AppUtils.Tag_Payment_JZCH,
                                        AppUtils.Tag_Payment_ZDJG,AppUtils.Tag_Payment_BBMC,AppUtils.Tag_Payment_MZBZ,AppUtils.Tag_Payment_DYJB,
                                        AppUtils.Tag_Payment_BZ,AppUtils.Tag_Payment_HZHB,AppUtils.Tag_Query_SBM};

                        foreach (string tag in tags)
                        {
                            string value = ri.getValue(tag);
                            XmlElement eleTag = doc.CreateElement(tag);
                            eleTag.InnerText = (null != value) ? value : "";
                            eleValue.AppendChild(eleTag);
                        }

                        XmlElement eleItem = doc.CreateElement(AppUtils.Tag_REXML_ITEM);
                        eleValue.AppendChild(eleItem);


                        ICollection<ReportDetail> col = ri.details;
                        if (null != col && col.Count > 0)
                        {

                            XmlAttribute attrZB = doc.CreateAttribute("code");
                            attrZB.InnerText = "zb";
                            eleItem.Attributes.Append(attrZB);

                            foreach (ReportDetail rd in col)
                            {
                                XmlElement eleRD = GetReportDetailElement(doc, rd, AppUtils.Tag_REXML_Value);
                                eleItem.AppendChild(eleRD);
                            }
                        }


                        eleMsg.AppendChild(eleValue);
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
        /// 根据号码查询明细
        /// </summary>
        /// <param name="code">号码</param>
        /// <param name="lx">类型  1-按报告单号查询 2-按条码查</param>
        /// <param name="brxm">病人姓名</param>
        /// <returns></returns>
        public XmlDocument QueryReportJCDetailByCode(string code, string lx, string brxm)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList values = new ArrayList();
                string error_msg = "";

                int ret = -1;
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDSRMYY)
                {
                    ReportDBForWZSDSRMYY pdb = new ReportDBForWZSDSRMYY();
                    ret = pdb.QueryReportJCByCode(code, lx, out values, out error_msg);
                }
                else
                {
                    QueryReportDB pdb = new QueryReportDB();
                    ret = pdb.DB_QueryReportJCListByCode(code, lx, brxm, out values, out error_msg);
                }

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);
                    foreach (ReportInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        string[] tags = {AppUtils.Tag_Payment_BGDH,AppUtils.Tag_Payment_SJMD,AppUtils.Tag_Payment_CJSJ,AppUtils.Tag_Payment_SJR,
                                        AppUtils.Tag_Payment_JYSJ,AppUtils.Tag_Payment_JYR,AppUtils.Tag_Payment_SHR,AppUtils.Tag_Payment_JZCH,
                                        AppUtils.Tag_Payment_ZDJG,AppUtils.Tag_Payment_BBMC,AppUtils.Tag_Payment_MZBZ,AppUtils.Tag_Payment_DYJB,
                                        AppUtils.Tag_Payment_BZ,AppUtils.Tag_Payment_HZHB,AppUtils.Tag_Query_SBM};

                        foreach (string tag in tags)
                        {
                            string value = ri.getValue(tag);
                            XmlElement eleTag = doc.CreateElement(tag);
                            eleTag.InnerText = (null != value) ? value : "";
                            eleValue.AppendChild(eleTag);
                        }

                        XmlElement eleItem = doc.CreateElement(AppUtils.Tag_REXML_ITEM);
                        eleValue.AppendChild(eleItem);


                        ICollection<ReportDetail> col = ri.details;
                        if (null != col && col.Count > 0)
                        {

                            XmlAttribute attrZB = doc.CreateAttribute("code");
                            attrZB.InnerText = "zb";
                            eleItem.Attributes.Append(attrZB);

                            foreach (ReportDetail rd in col)
                            {
                                XmlElement eleRD = GetReportDetailElement(doc, rd, AppUtils.Tag_REXML_Value);
                                eleItem.AppendChild(eleRD);
                            }
                        }

                        eleMsg.AppendChild(eleValue);
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

        public XmlDocument QueryReportDetailByCodeFromWebservice(string bgdh, string brxm, string lxdh)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList values = new ArrayList();
                string error_msg = "";
                string endTime = DateTime.Now.ToString(AppUtils.DateTime_FormatNO_YearMonthDay);
                string startTime = DateTime.Now.AddDays(-60).ToString(AppUtils.DateTime_FormatNO_YearMonthDay);
                //DateTime endTime = DateTime.Now;
                //DateTime startTime = DateTime.Now.AddDays(-60);

                int ret = -1;
                if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSZYY)
                {
                    ReportInfoHandleForWZSZYY pdb = new ReportInfoHandleForWZSZYY();
                    ret = pdb.ReportDetailInfoList(bgdh, brxm, lxdh, startTime, endTime, out values, out error_msg);
                }

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);
                    foreach (ReportInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        string[] tags = {AppUtils.Tag_Payment_BGDH,AppUtils.Tag_Payment_SJMD,AppUtils.Tag_Payment_CJSJ,AppUtils.Tag_Payment_SJR,
                                        AppUtils.Tag_Payment_JYSJ,AppUtils.Tag_Payment_JYR,AppUtils.Tag_Payment_SHR,AppUtils.Tag_Payment_JZCH,
                                        AppUtils.Tag_Payment_ZDJG,AppUtils.Tag_Payment_BBMC,AppUtils.Tag_Payment_MZBZ,AppUtils.Tag_Payment_DYJB,
                                        AppUtils.Tag_Payment_BZ,AppUtils.Tag_Payment_HZHB,AppUtils.Tag_Query_SBM};

                        foreach (string tag in tags)
                        {
                            string value = ri.getValue(tag);
                            XmlElement eleTag = doc.CreateElement(tag);
                            eleTag.InnerText = (null != value) ? value : "";
                            eleValue.AppendChild(eleTag);
                        }

                        XmlElement eleItem = doc.CreateElement(AppUtils.Tag_REXML_ITEM);
                        eleValue.AppendChild(eleItem);


                        ICollection<ReportDetail> col = ri.details;
                        if (null != col && col.Count > 0)
                        {

                            XmlAttribute attrZB = doc.CreateAttribute("code");
                            attrZB.InnerText = "zb";
                            eleItem.Attributes.Append(attrZB);

                            foreach (ReportDetail rd in col)
                            {
                                XmlElement eleRD = GetReportDetailElement(doc, rd, AppUtils.Tag_REXML_Value);
                                eleItem.AppendChild(eleRD);
                            }
                        }
                        eleMsg.AppendChild(eleValue);
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

        //私有--创建详细节点
        private XmlElement GetReportDetailElement(XmlDocument doc, ReportDetail rd, string tagName)
        {
            XmlElement eleTag = doc.CreateElement(tagName);

            string[] tags = { AppUtils.Tag_Payment_MC, AppUtils.Tag_Payment_DW, AppUtils.Tag_Payment_CKJG, 
                                AppUtils.Tag_Payment_TS, AppUtils.Tag_Payment_JG };
            foreach (string tag in tags)
            {
                string value = rd.getValue(tag);
                XmlElement eleValue = doc.CreateElement(tag);
                eleValue.InnerText = (null != value) ? value : "";
                eleTag.AppendChild(eleValue);
            }

            return eleTag;
        }
    }
}