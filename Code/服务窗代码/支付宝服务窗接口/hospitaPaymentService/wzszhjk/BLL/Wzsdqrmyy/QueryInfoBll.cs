using HospitaPaymentService.wzszhjk.DAL.Database.Wzsdqrmyy;
using HospitaPaymentService.Wzszhjk.Common.Xml;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Model;

namespace HospitaPaymentService.wzszhjk.BLL.Wzsdqrmyy
{
    /// <summary>
    /// 第七人民医院新增接口
    /// </summary>
    public class QueryInfoBll
    {
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="openid">用户标识(支付宝USERID或微信OPENID)</param>
        /// <param name="usertype">用户类型(1：支付宝用户 2：微信用户)</param>
        /// <returns></returns>
        public XmlDocument QueryConnectPerson(string openid, string usertype)
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
                int ret = dal.QueryConnectPerson(openid, usertype, out values, out error_msg);
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
                        eleMsg.AppendChild(eleValue);
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
        /// 获取病人基本信息
        /// </summary>
        /// <param name="id">缴费：jzid,退费：orderno</param>
        /// <param name="type">就诊id:jzid，收费id:sfid</param>
        /// <returns></returns>
        public XmlDocument QueryPatientInfo(string id, string type)
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
                int result = dal.QueryPatientInfo(id, type, out values, out error_msg);
                if (result == 1)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                    root.AppendChild(eleMsg);

                    foreach (var item in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(result, error_msg);
                }

            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 可退费清单
        /// </summary>
        /// <param name="sfid">订单id</param>
        /// <returns></returns>
        public XmlDocument GetChargeList(string orderno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetChargeList(orderno, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 缴费时获取处方单信息
        /// </summary>
        /// <param name="jzid">就诊id</param>
        /// <returns></returns>
        public XmlDocument QueryDetailChuFangInfoByJzId(string jzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryDetailChuFangInfoByJzId(jzid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 缴费时获取处方单药品详情信息
        /// </summary>
        /// <param name="fpxmid">发票项目id</param>
        /// <param name="jzid">就诊id</param>
        /// <returns></returns>
        public XmlDocument QueryChuFangYaoPinInfo(string fpxmid, string jzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);
            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryChuFangYaoPinInfo(fpxmid, jzid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }


        /// <summary>
        /// 可退费明细
        /// </summary>
        /// <param name="fapiaoxmid">发票项目id</param>
        /// <param name="orderno">费用id</param>
        /// <returns></returns>
        public XmlDocument QueryChargeListByOrderno(string fapiaoxmid, string orderno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryChargeListByOrderno(fapiaoxmid, orderno, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 获取默认收件人信息
        /// </summary>
        /// <param name="openid">用户标识(支付宝USERID或微信OPENID)</param>
        /// <returns></returns>
        public XmlDocument QueryReceiverInfo(string openid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;

            int result = dal.QueryReceiverInfo(openid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 获取收件人信息
        /// </summary>
        /// <param name="openid">用户标识(支付宝USERID或微信OPENID)</param>
        /// <returns></returns>
        public XmlDocument QueryByReceiverInfo(string openid, string shdzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;

            int result = dal.QueryByReceiverInfo(openid, shdzid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 预提交缴费信息（FY030303）联众webservice接口
        /// </summary>
        /// <param name="jzid">就诊id</param>
        /// <param name="qjfs">取件方式(1:快递;2:自取)</param>
        /// <param name="shdzid">收获地址id</param>
        /// <returns></returns>
        public XmlDocument QueryPaymentInfo(string jzid, string qjfs, string shdzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);



            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            //  int result = dal.QueryPaymentInfo(jzid, qjfs, shdzid, out values, out error_msg);
            if (1 == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("xzfje");
                msg.InnerText = "success";
                eleValue.AppendChild(msg);

                XmlElement jfje = doc.CreateElement("jfje");
                jfje.InnerText = "0.01";
                eleValue.AppendChild(jfje);

                XmlElement ybkcje = doc.CreateElement("ybkcje");
                ybkcje.InnerText = "0.01";
                eleValue.AppendChild(ybkcje);

                XmlElement ynzhje = doc.CreateElement("ynzhje");
                ynzhje.InnerText = "0.01";
                eleValue.AppendChild(ynzhje);

                XmlElement xzfje = doc.CreateElement("xzfje");
                xzfje.InnerText = "0.01";
                eleValue.AppendChild(xzfje);

            }
            else
            {
                doc = ErrorReturnXml(1, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// FY030305
        /// </summary>
        /// <param name="jzid"></param>
        /// <returns></returns>
        public XmlDocument QueryPaymentInfo2(string jzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = AppUtils.Value_Return_Success;
            root.AppendChild(eleResult);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
            eleMsg.AppendChild(eleValue);

            XmlElement msg = doc.CreateElement("msg");
            msg.InnerText = "success";
            eleValue.AppendChild(msg);

            XmlElement zffs = doc.CreateElement("zffs");
            zffs.InnerText = "支付宝支付";
            eleValue.AppendChild(zffs);

            XmlElement xzfje = doc.CreateElement("xzfje");
            xzfje.InnerText = "0.01";
            eleValue.AppendChild(xzfje);

            return doc;
        }

        /// <summary>
        /// FY030306
        /// </summary>
        /// <param name="jzid"></param>
        /// <returns></returns>
        public XmlDocument QueryPaymentInfo3(string jzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = AppUtils.Value_Return_Success;
            root.AppendChild(eleResult);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
            eleMsg.AppendChild(eleValue);

            XmlElement msg = doc.CreateElement("msg");
            msg.InnerText = "success";
            eleValue.AppendChild(msg);

            return doc;
        }


        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="openid">用户标识(支付宝USERID或微信OPENID)</param>
        /// <param name="status">预约状态状态： 0：已预约待付款、1：已预约已付款、2：已就诊、3：已失效（包含3:已取消和4:已失约</param>
        /// <param name="preengagestyle">9：云医院预约  其他：非云医院预约（值待定，需要联众确认）</param>
        /// <returns></returns>
        public XmlDocument QueryAppointment(string openid, string status, string preengagestyle)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryAppointment(openid, status, preengagestyle, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 预约详情接口
        /// </summary>
        /// <param name="preengageseq">预约流水号</param>
        /// <returns></returns>
        public XmlDocument QueryDetailAppointment(string preengageseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);


            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryDetailAppointmentInfo(preengageseq, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 根据订单号返回支付宝交易号和批次号(PO010102)
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public XmlDocument QueryChargeNumber(string orderno, string paytype)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryChargeNumber(orderno, paytype, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 根据预约流水号进行预挂号获取支付金额(OR030204)
        /// </summary>
        /// <param name="preengageseq"></param>
        /// <returns></returns>
        public XmlDocument QueryPayMoneyList(string preengageseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryPayMoneyList(preengageseq, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("msg");
                msg.InnerText = "success";
                eleValue.AppendChild(msg);
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        ///OR030205
        /// </summary>
        /// <param name="preengageseq"></param>
        /// <returns></returns>
        public XmlDocument QueryPayMoneyList2()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = AppUtils.Value_Return_Success;
            root.AppendChild(eleResult);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
            eleMsg.AppendChild(eleValue);

            XmlElement msg = doc.CreateElement("msg");
            msg.InnerText = "success";
            eleValue.AppendChild(msg);

            XmlElement xzfje = doc.CreateElement("xzfje");
            xzfje.InnerText = "0.01";
            eleValue.AppendChild(xzfje);

            XmlElement countmoney = doc.CreateElement("countmoney");
            countmoney.InnerText = "0.01";
            eleValue.AppendChild(countmoney);

            XmlElement totalmoney = doc.CreateElement("totalmoney");
            totalmoney.InnerText = "0.01";
            eleValue.AppendChild(totalmoney);

            return doc;
        }


        /// <summary>
        /// OR030206
        /// </summary>
        /// <returns></returns>
        public XmlDocument QueryPayMoneyList3()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
            eleResult.InnerText = AppUtils.Value_Return_Success;
            root.AppendChild(eleResult);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
            eleMsg.AppendChild(eleValue);

            XmlElement msg = doc.CreateElement("msg");
            msg.InnerText = "success";
            eleValue.AppendChild(msg);

            XmlElement orderno = doc.CreateElement("orderno");
            orderno.InnerText = "1000001990";
            eleValue.AppendChild(orderno);

            XmlElement xzfje = doc.CreateElement("xzfje");
            xzfje.InnerText = "0.01";
            eleValue.AppendChild(xzfje);

            XmlElement paymethod = doc.CreateElement("paymethod");
            paymethod.InnerText = "支付宝";
            eleValue.AppendChild(paymethod);

            return doc;
        }

        /// <summary>
        /// 可缴费列表（FY030307）yyy_keshouflb_jzid
        /// </summary>
        /// <param name="brid"></param>
        /// <returns></returns>
        public XmlDocument QueryPaymentList(string brid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryPaymentList(brid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 可退费列表（FY030311）
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public XmlDocument QueryRefundList(string brid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryRefundList(brid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 更新订单状态（FY030308）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public XmlDocument EditOrderStatus(string orderno, string status)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            int result = dal.EditOrderStatus(orderno, status, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("msg");
                msg.InnerText = error_msg;
                eleValue.AppendChild(msg);
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="brid"></param>
        /// <returns></returns>
        public XmlDocument QueryPatientInfoByBrid(string brid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);


            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryPatientInfoByBrid(brid, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);


                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 获取缴费病人信息（FY030309）
        /// </summary>
        /// <param name="patientcode"></param>
        /// <returns></returns>
        public XmlDocument QueryPayPatientInfo(string orderno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryPayPatientInfo(orderno, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 线程获取缴费表的订单状态为0的订单（FY030314）
        /// </summary>
        /// <returns></returns>
        public XmlDocument QueryOrderList1()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryOrderList1(out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 线程获取缴费表的订单状态为1的订单（FY030315）
        /// </summary>
        /// <returns></returns>
        public XmlDocument QueryOrderList2()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryOrderList2(out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 线程获取退费表的订单状态为0的订单（FY030316）
        /// </summary>
        /// <returns></returns>
        public XmlDocument QueryReturnList1()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryReturnList1(out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 线程获取退费表的订单状态为1的订单（FY030318）
        /// </summary>
        /// <returns></returns>
        public XmlDocument QueryReturnList2()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryReturnList2(out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 更新退费表的订单状态（FY030317）
        /// </summary>
        /// <param name="batchno">批次号</param>
        /// <param name="status">订单状态</param>
        /// <param name="notifytime">支付宝通知时间</param>
        /// <param name="successnum">成功笔数</param>
        /// <param name="tradestatus">交易状态</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public XmlDocument EditReturnStatus(string batchno, string status, string notifytime, string successnum, string paymentbatchno, string paymentparameters, string money)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            int result = dal.EditReturnStatus(batchno, status, notifytime, successnum, paymentbatchno, paymentparameters, money, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("msg");
                msg.InnerText = error_msg;
                eleValue.AppendChild(msg);
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 更新退费订单状态
        /// </summary>
        /// <param name="batchno"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public XmlDocument UpdateTfddStatus(string batchno, string status)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            int result = dal.UpdateTFStatus(batchno, status);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("msg");
                msg.InnerText = "success";
                eleValue.AppendChild(msg);
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 获取收费记录列表（FY030320）
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public XmlDocument QueryChargeList(string brid, string openid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
            root.AppendChild(eleMsg);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryChargeList(brid, openid, out values, out error_msg);
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
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }


        /// <summary>
        /// 插入或更新收货人信息（FY030321）
        /// </summary>
        /// <param name="type">操作类型(插入：insert，更新：update)</param>
        /// <param name="shdzid">收货地址id(插入：shdzid为空，更新：shdzid不能为空)</param>
        /// <param name="sjrxm">收件人姓名</param>
        /// <param name="lxdh">联系电话</param>
        /// <param name="sjdz">收件地址</param>
        /// <param name="mrbz">默认标志</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public XmlDocument UpdateOrInsertReceiverInfo(string type, string openid, string shdzid, string sjrxm, string lxdh, string sjdz, string mrbz)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            int result = dal.UpdateOrInsertReceiverInfo(type, openid, shdzid, sjrxm, lxdh, sjdz, mrbz, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("msg");
                msg.InnerText = error_msg;
                eleValue.AppendChild(msg);
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 删除收货人信息（FY030322）
        /// </summary>
        /// <param name="shdzid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public XmlDocument DeleteReceiverInfo(string shdzid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            int result = dal.DeleteReceiverInfo(shdzid, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                eleMsg.AppendChild(eleValue);

                XmlElement msg = doc.CreateElement("msg");
                msg.InnerText = error_msg;
                eleValue.AppendChild(msg);
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-处方单列表（FY030323）
        /// </summary>
        /// <param name="brid">病人id</param>
        /// <param name="msg"></param>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        public XmlDocument GetChuFangListByQuery(string brid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetChuFangListByQuery(brid, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }


        /// <summary>
        /// 查询模块-处方单详情（FY030324）
        /// </summary>
        /// <param name="cflsh"></param>
        /// <returns></returns>
        public XmlDocument GetChuFangDetailByQuery(string cflsh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetChuFangDetailByQuery(cflsh, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检验单或检查单列表（FY030325）
        /// </summary>
        /// <param name="brid">病人id</param>
        /// <param name="type">查询类型 检查单：jcd，检验单：jyd</param>
        /// <returns></returns>
        public XmlDocument GetJyInfoList(string brid, string type)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetJyInfoList(brid, type, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检验单详情基本信息（FY030326）
        /// </summary>
        /// <param name="jydlsh">检验单流水号</param>
        /// <returns></returns>
        public XmlDocument GetJyDetailInfo(string jydlsh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetJyDetailInfo(jydlsh, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检验单详情检验项目列表（FY030327）
        /// </summary>
        /// <param name="jydlsh">检验单流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument GetJyDetailInfo2(string jydlsh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetJyDetailInfo2(jydlsh, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检查单详情基本信息（FY030328）
        /// </summary>
        /// <param name="jydlsh">检查单流水号</param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument GetJyBasicInfo(string jcdlsh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetJyBasicInfo(jcdlsh, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块-检查单详情检查项目列表（FY030329）
        /// </summary>
        /// <param name="jcdlsh"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument GetJyInfoDetailList(string jcdlsh)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetJyInfoDetailList(jcdlsh, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 判断是否可退费（FY030330）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument IsTuiFeiState(string orderno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.IsTuiFeiState(orderno, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }



        /// <summary>
        /// 医生信息列表_按医生代码查(DO010104)
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument QueryDoctorList(string namepy, string haskey, int pageindex, int pagesize)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryDoctorList(namepy, haskey, pageindex, pagesize, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块——缴费退费清单列表（FY030331）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument QueryChargeListByOrderno31(string orderno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryChargeListByOrderno31(orderno, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="ysid"></param>
        /// <returns></returns>
        public XmlDocument GetSfInfo(string brid, string ysid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.GetSfInfo(brid, ysid, out error_msg, out values);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 查询模块——缴费退费清单明细（FY030332）
        /// </summary>
        /// <param name="orderno"></param>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public XmlDocument QueryChargeListByOrderno32(string fpxmid, string orderno)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryInfoDal dal = new QueryInfoDal();
            string error_msg = "";
            ArrayList values;
            int result = dal.QueryChargeListByOrderno32(fpxmid, orderno, out values, out error_msg);
            if (result == 1)
            {
                XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                eleResult.InnerText = AppUtils.Value_Return_Success;
                root.AppendChild(eleResult);

                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                foreach (var item in values)
                {
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(item.GetType(), item);
                }
            }
            else
            {
                doc = ErrorReturnXml(result, error_msg);
            }
            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public DingDanInfo GetInfoByOrderno(string orderno)
        {
            QueryInfoDal dal = new QueryInfoDal();
            return dal.GetInfoByOrderno(orderno);
        }

        /// <summary>
        /// 更新退费订单状态
        /// </summary>
        /// <param name="batchno"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateTFStatusByOrderno(string orderNo, string status)
        {
            QueryInfoDal dal = new QueryInfoDal();
            return dal.UpdateTFStatusByOrderno(orderNo, status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shdzid"></param>
        /// <returns></returns>
        public string GetShdzInfoById(string shdzid)
        {
            QueryInfoDal dal = new QueryInfoDal();
            return dal.GetShdzInfoById(shdzid);
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

        /// <summary>
        /// 根据挂号id获取交易号
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public string GetOrderNoByRegisterId(string registerId)
        {
            QueryInfoDal dal = new QueryInfoDal();
            return dal.GetOrderNoByRegisterId(registerId);
        }

        /// <summary>
        /// 根据订单号获取退款批次号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public string GetBatchNoByOrderNo(string orderNo)
        {
            QueryInfoDal dal = new QueryInfoDal();
            return dal.GetBatchNoByOrderNo(orderNo);
        }


    }
}