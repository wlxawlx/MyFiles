using System;
using System.Collections.Generic;
using System.Web;
using HospitaPaymentService.Wzszhjk.BLL;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.DAL.Database.Wzszyy;
using System.Collections;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL.Wzszyy
{
    public class QueryLogicForWZSZYY : BaseLogic
    {
        /// <summary>
        /// 查询预约签到信息
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public XmlDocument QuerySignInfo(string brid, string brlx)
        {
            XmlDocument rtDoc = new XmlDocument();
            XmlElement root = rtDoc.CreateElement(AppUtils.Tag_REXML_Root);
            rtDoc.AppendChild(root);

            try
            {
                XmlElement eleMsg = rtDoc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList _list;
                string msg;

                FZPDInfoForWZSZYY pdb = new FZPDInfoForWZSZYY();
                int ret = pdb.QuerySignInfo(brid, brlx, out _list, out msg);

                if (ret == 0)
                {
                    XmlElement eleResult = rtDoc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (RegHospitalInfo info in _list)
                    {
                        XmlElement eleValue = rtDoc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement elePdhm = rtDoc.CreateElement(AppUtils.Tag_Yyxx_Pdhm);
                        eleValue.AppendChild(elePdhm);
                        elePdhm.InnerText = info.pdhm;

                        XmlElement eleBrxm = rtDoc.CreateElement(AppUtils.Tag_Yyxx_Brxm);
                        eleValue.AppendChild(eleBrxm);
                        eleBrxm.InnerText = info.brxm;

                        XmlElement eleKsmc = rtDoc.CreateElement(AppUtils.Tag_Yyxx_Ksmc);
                        eleValue.AppendChild(eleKsmc);
                        eleKsmc.InnerText = info.ksmc;

                        XmlElement eleDoctor = rtDoc.CreateElement(AppUtils.Tag_Yyxx_Doctor);
                        eleValue.AppendChild(eleDoctor);
                        eleDoctor.InnerText = info.doctor;

                        XmlElement eleYysj = rtDoc.CreateElement(AppUtils.Tag_Yyxx_Yysj);
                        eleValue.AppendChild(eleYysj);
                        eleYysj.InnerText = info.yysj;

                        XmlElement eleYyly = rtDoc.CreateElement(AppUtils.Tag_Yyxx_Yyly);
                        eleValue.AppendChild(eleYyly);
                        eleYyly.InnerText = info.yyly;
                    }
                }
                else
                {
                    rtDoc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                rtDoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return rtDoc;
        }

        /// <summary>
        /// 查询排队信息
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <returns></returns>
        public XmlDocument QueryQueueInfo(string brid, string brlx)
        {
            XmlDocument rtDoc = new XmlDocument();
            XmlElement root = rtDoc.CreateElement(AppUtils.Tag_REXML_Root);
            rtDoc.AppendChild(root);

            try
            {
                XmlElement eleMsg = rtDoc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                ArrayList _list;
                string msg;

                FZPDInfoForWZSZYY pdb = new FZPDInfoForWZSZYY();
                int ret = pdb.QueryQueueInfo(brid, brlx, out _list, out msg);

                if (ret == 0)
                {
                    XmlElement eleResult = rtDoc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (PainterQueueInfo info in _list)
                    {
                        XmlElement eleValue = rtDoc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);

                        XmlElement eleZt = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Zt);
                        eleValue.AppendChild(eleZt);
                        eleZt.InnerText = info.zt;

                        XmlElement eleBrxm = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Brxm);
                        eleValue.AppendChild(eleBrxm);
                        eleBrxm.InnerText = info.brxm;

                        XmlElement eleSfzh = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Sfzh);
                        eleValue.AppendChild(eleSfzh);
                        eleSfzh.InnerText = info.sfzh;

                        XmlElement eleKsmc = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Ksmc);
                        eleValue.AppendChild(eleKsmc);
                        eleKsmc.InnerText = info.ksmc;

                        XmlElement eleZsmc = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Zsmc);
                        eleValue.AppendChild(eleZsmc);
                        eleZsmc.InnerText = info.zsmc;

                        XmlElement eleDoctor = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Doctor);
                        eleValue.AppendChild(eleDoctor);
                        eleDoctor.InnerText = info.doctor;
                        
                        XmlElement elePdhm = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Pdhm);
                        eleValue.AppendChild(elePdhm);
                        elePdhm.InnerText = info.pdhm;

                        XmlElement eleWaitCount = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_WaitCount);
                        eleValue.AppendChild(eleWaitCount);
                        eleWaitCount.InnerText = info.waitCount;

                        XmlElement eleSpecialWaitCount = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_SpecialWaitCount);
                        eleValue.AppendChild(eleSpecialWaitCount);
                        eleSpecialWaitCount.InnerText = info.specialWaitCount;

                        XmlElement eleYjjzsj = rtDoc.CreateElement(AppUtils.Tag_QueueInfo_Yjjzsj);
                        eleValue.AppendChild(eleYjjzsj);
                        eleYjjzsj.InnerText = info.yjjzsj;
                    }
                }
                else
                {
                    rtDoc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                rtDoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return rtDoc;
        }
    }
}