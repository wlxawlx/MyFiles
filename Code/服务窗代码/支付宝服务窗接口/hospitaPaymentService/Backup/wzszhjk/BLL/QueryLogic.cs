using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using System.Collections;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Common.Webservice;

namespace HospitaPaymentService.Wzszhjk.BLL
{
    public class QueryLogic : BaseLogic
    {
        /// <summary>
        /// 查询药品(分页）
        /// </summary>
        /// <param name="pNumber">所在页数</param>
        /// <param name="pRows">每页显示的条数</param>
        /// <returns></returns>
        public XmlDocument PageMedicine(int pNumber, int pRows)
        {
            XmlDocument doc = new XmlDocument();

            try
            {

                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_PageMedicine(pNumber, pRows, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (MedicineDetail pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleYplx = doc.CreateElement(AppUtils.Tag_Payment_YPLX);
                        eleValue.AppendChild(eleYplx);
                        //以下实现数据操作逻辑
                        eleYplx.InnerText = pdt.lx;

                        XmlElement eleYpmc = doc.CreateElement(AppUtils.Tag_Payment_YPMC);
                        eleValue.AppendChild(eleYpmc);
                        //以下实现数据操作逻辑
                        eleYpmc.InnerText = pdt.mc;

                        XmlElement eleYpdw = doc.CreateElement(AppUtils.Tag_Payment_YPDW);
                        eleValue.AppendChild(eleYpdw);
                        //以下实现数据操作逻辑
                        eleYpdw.InnerText = pdt.dw;

                        XmlElement eleYpgg = doc.CreateElement(AppUtils.Tag_Payment_YPGG);
                        eleValue.AppendChild(eleYpgg);
                        //以下实现数据操作逻辑
                        eleYpgg.InnerText = pdt.gg;

                        XmlElement eleYpcd = doc.CreateElement(AppUtils.Tag_Payment_YPCD);
                        eleValue.AppendChild(eleYpcd);
                        //以下实现数据操作逻辑
                        eleYpcd.InnerText = pdt.cd;

                        XmlElement eleYpjg = doc.CreateElement(AppUtils.Tag_Payment_YPJG);
                        eleValue.AppendChild(eleYpjg);
                        //以下实现数据操作逻辑
                        eleYpjg.InnerText = pdt.jg.ToString("0.0000");
                    }
                    //eleResult.InnerText = "1";
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
        /// 查询药品(按拼音）
        /// </summary>
        /// <param name="pydm">拼音代码</param>
        /// <returns></returns>
        public XmlDocument Querymedicine(string pydm)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_QueryMedicine(pydm, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (MedicineDetail pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleYplx = doc.CreateElement(AppUtils.Tag_Payment_YPLX);
                        eleValue.AppendChild(eleYplx);
                        eleYplx.InnerText = pdt.lx;

                        XmlElement eleYpmc = doc.CreateElement(AppUtils.Tag_Payment_YPMC);
                        eleValue.AppendChild(eleYpmc);
                        eleYpmc.InnerText = pdt.mc;

                        XmlElement eleYpdw = doc.CreateElement(AppUtils.Tag_Payment_YPDW);
                        eleValue.AppendChild(eleYpdw);
                        eleYpdw.InnerText = pdt.dw;

                        XmlElement eleYpgg = doc.CreateElement(AppUtils.Tag_Payment_YPGG);
                        eleValue.AppendChild(eleYpgg);
                        eleYpgg.InnerText = pdt.gg;

                        XmlElement eleYpcd = doc.CreateElement(AppUtils.Tag_Payment_YPCD);
                        eleValue.AppendChild(eleYpcd);
                        eleYpcd.InnerText = pdt.cd;

                        XmlElement eleYpjg = doc.CreateElement(AppUtils.Tag_Payment_YPJG);
                        eleValue.AppendChild(eleYpjg);
                        eleYpjg.InnerText = pdt.jg.ToString("0.0000");
                    }
                    //eleResult.InnerText = "1";
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
        /// 查询收费项目(分页）
        /// </summary>
        /// <param name="pNumber">所在页数</param>
        /// <param name="pRows">每页显示的条数</param>
        /// <returns></returns>
        public XmlDocument PageCharge(int pNumber, int pRows)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_PageCharge(pNumber, pRows, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (ChargeDetail pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleFylx = doc.CreateElement(AppUtils.Tag_Payment_FYLX);
                        eleValue.AppendChild(eleFylx);
                        //以下实现数据操作逻辑
                        eleFylx.InnerText = pdt.lx;

                        XmlElement eleFymc = doc.CreateElement(AppUtils.Tag_Payment_FYMC);
                        eleValue.AppendChild(eleFymc);
                        //以下实现数据操作逻辑
                        eleFymc.InnerText = pdt.mc;

                        XmlElement eleFydw = doc.CreateElement(AppUtils.Tag_Payment_FYDW);
                        eleValue.AppendChild(eleFydw);
                        //以下实现数据操作逻辑
                        eleFydw.InnerText = pdt.dw;

                        XmlElement eleFyjg = doc.CreateElement(AppUtils.Tag_Payment_FYJG);
                        eleValue.AppendChild(eleFyjg);
                        //以下实现数据操作逻辑
                        eleFyjg.InnerText = pdt.jg.ToString("0.0000");
                    }
                    //eleResult.InnerText = "1";
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
        /// 查询收费项目(按拼音）
        /// </summary>
        /// <param name="pydm">拼音代码</param>
        /// <returns></returns>
        public XmlDocument Querycharge(string pydm)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_QueryCharge(pydm, out _list, out error_msg);

                if (ret == 0)
                {
                    doc = new XmlDocument();
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (ChargeDetail pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleFylx = doc.CreateElement(AppUtils.Tag_Payment_FYLX);
                        eleValue.AppendChild(eleFylx);
                        //以下实现数据操作逻辑
                        eleFylx.InnerText = pdt.lx;

                        XmlElement eleFymc = doc.CreateElement(AppUtils.Tag_Payment_FYMC);
                        eleValue.AppendChild(eleFymc);
                        //以下实现数据操作逻辑
                        eleFymc.InnerText = pdt.mc;

                        XmlElement eleFydw = doc.CreateElement(AppUtils.Tag_Payment_FYDW);
                        eleValue.AppendChild(eleFydw);
                        //以下实现数据操作逻辑
                        eleFydw.InnerText = pdt.dw;

                        XmlElement eleFyjg = doc.CreateElement(AppUtils.Tag_Payment_FYJG);
                        eleValue.AppendChild(eleFyjg);
                        //以下实现数据操作逻辑
                        eleFyjg.InnerText = pdt.jg.ToString("0.0000");
                    }
                    //eleResult.InnerText = "1";
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
        /// 医院专家查询(分页）
        /// </summary>
        /// <param name="pNumber">所在页数</param>
        /// <param name="pRows">每页显示条数</param>
        /// <returns></returns>
        public XmlDocument PageDoctor(int pNumber, int pRows)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_PageDoctor(pNumber, pRows, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);
                    if (null == _list)
                    {
                        doc = ReplyXmlDoc.GetFailureXML(-99, "空的返回values");
                        return doc;
                    }
                    foreach (DoctorInfo pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleYsxm = doc.CreateElement(AppUtils.Tag_Payment_YSXM);
                        eleValue.AppendChild(eleYsxm);
                        //以下实现数据操作逻辑
                        eleYsxm.InnerText = pdt.xm;

                        XmlElement eleYsdm = doc.CreateElement(AppUtils.Tag_Payment_YSDM);
                        eleValue.AppendChild(eleYsdm);
                        //以下实现数据操作逻辑
                        eleYsdm.InnerText = pdt.dm;

                        XmlElement eleYsjb = doc.CreateElement(AppUtils.Tag_Payment_YSJB);
                        eleValue.AppendChild(eleYsjb);
                        //以下实现数据操作逻辑
                        eleYsjb.InnerText = pdt.jb;

                    }
                    //eleResult.InnerText = "1";
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
        /// 医院专家查询(按拼音）
        /// </summary>
        /// <param name="pydm">医生名字拼音</param>
        /// <returns></returns>
        public XmlDocument Listdoctor(string pydm)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_ListDoctor(pydm, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (DoctorInfo pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleYsxm = doc.CreateElement(AppUtils.Tag_Payment_YSXM);
                        eleValue.AppendChild(eleYsxm);
                        eleYsxm.InnerText = pdt.xm;

                        XmlElement eleYsdm = doc.CreateElement(AppUtils.Tag_Payment_YSDM);
                        eleValue.AppendChild(eleYsdm);
                        eleYsdm.InnerText = pdt.dm;

                        XmlElement eleYsjb = doc.CreateElement(AppUtils.Tag_Payment_YSJB);
                        eleValue.AppendChild(eleYsjb);
                        eleYsjb.InnerText = pdt.jb;

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
        /// 查询医生信息
        /// </summary>
        /// <param name="ysdm">医生代码</param>
        /// <returns></returns>
        public XmlDocument Querydoctor(string ysdm)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_QueryDoctor(ysdm, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (DoctorInfo pdt in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleYsjj = doc.CreateElement(AppUtils.Tag_Payment_YSJJ);
                        eleValue.AppendChild(eleYsjj);
                        //以下实现数据操作逻辑
                        eleYsjj.InnerText = pdt.jj;

                        XmlElement eleYssc = doc.CreateElement(AppUtils.Tag_Payment_YSSC);
                        eleValue.AppendChild(eleYssc);
                        //以下实现数据操作逻辑
                        eleYssc.InnerText = pdt.sc;

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
        /// 取药查询
        /// </summary>
        /// <param name="brxm">病人</param>
        /// <param name="lxdh">联系电话</param>
        /// <returns></returns>
        public XmlDocument QueryPatientDrugInfo(string brxm, string lxdh)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                ArrayList _list = new ArrayList();
                string error_msg = "";

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_QueryPatientDrugInfo(brxm, lxdh, out _list, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (PatientDrugInfo pdi in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleKfrq = doc.CreateElement(AppUtils.Tag_Payment_KFRQ);
                        eleValue.AppendChild(eleKfrq);
                        //以下实现数据操作逻辑
                        eleKfrq.InnerText = pdi.rq;

                        XmlElement eleQyxh = doc.CreateElement(AppUtils.Tag_Payment_QYXH);
                        eleValue.AppendChild(eleQyxh);
                        //以下实现数据操作逻辑
                        eleQyxh.InnerText = pdi.xh;

                        XmlElement eleCfzt = doc.CreateElement(AppUtils.Tag_Payment_CFZT);
                        eleValue.AppendChild(eleCfzt);
                        //以下实现数据操作逻辑
                        eleCfzt.InnerText = pdi.zt;
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
        /// 查询剩余床位数
        /// </summary>
        /// <returns></returns>
        public XmlDocument QueryRemainBeds()
        {
            XmlDocument doc = new XmlDocument();

            try
            {

                string error_msg = "";
                ArrayList values = new ArrayList();

                QueryDB pdb = new QueryDB();
                int ret = pdb.DB_QueryRemainBeds(out values, out error_msg);

                if (ret == 0)
                {
                    XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
                    doc.AppendChild(root);

                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);
                    foreach (RemainBeds ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        XmlElement eleBqmc = doc.CreateElement(AppUtils.Tag_Query_Bqmc);
                        eleValue.AppendChild(eleBqmc);
                        eleBqmc.InnerText = ri.bqmc;

                        XmlElement eleSycw = doc.CreateElement(AppUtils.Tag_Query_Sycw);
                        eleValue.AppendChild(eleSycw);
                        eleSycw.InnerText = Convert.ToString(ri.sycw);

                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }
    }
}