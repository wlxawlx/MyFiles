using System;
using System.Web.Services;
using System.Xml;

using HospitaPaymentService.Wzszhjk;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Common;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using log4net;

namespace HospitaPaymentService
{
    /// <summary>
    /// HPService 的摘要说明

    /// <summary>
    /// 建立一个委托来处理所有协议
    /// </summary>
    public delegate XmlDocument OperatorHandleProcessor(ParameterHandler handler);

    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class HPService : System.Web.Services.WebService
    {
        /// <summary>
        /// 保存所有的协议处理方法
        /// </summary>
        private static Dictionary<string, OperatorHandleProcessor> m_processors = new Dictionary<string, OperatorHandleProcessor>();

        /// <summary>
        /// 构造与初始化函数
        /// </summary>
        public HPService()
        {
            if (m_processors.Count != 0)
            {
                return;
            }
            m_processors.Add(AppUtils.Command_Operate_QueryCard, new OperatorHandleProcessor(OperatorHandle.QueryCard));
            m_processors.Add(AppUtils.Command_Operate_BindCard, new OperatorHandleProcessor(OperatorHandle.BindCard));
            m_processors.Add(AppUtils.Command_Payment_OrderQuery, new OperatorHandleProcessor(OperatorHandle.OrderQuery));
            m_processors.Add(AppUtils.Command_Payment_CreateOrder, new OperatorHandleProcessor(OperatorHandle.CreateOrder));
            m_processors.Add(AppUtils.Command_Payment_FinishOrder, new OperatorHandleProcessor(OperatorHandle.FinishOrder));
            m_processors.Add(AppUtils.Command_Payment_CancelOrder, new OperatorHandleProcessor(OperatorHandle.CancelOrder));
            m_processors.Add(AppUtils.Command_Payment_QueryBalance, new OperatorHandleProcessor(OperatorHandle.QueryBalance));
            m_processors.Add(AppUtils.Command_Payment_AccountRecord, new OperatorHandleProcessor(OperatorHandle.AccountRecord));
            m_processors.Add(AppUtils.Command_Payment_InsertCcb, new OperatorHandleProcessor(OperatorHandle.InsertCcb));
            m_processors.Add(AppUtils.Command_Query_PageMedicine, new OperatorHandleProcessor(OperatorHandle.PageMedicine));
            m_processors.Add(AppUtils.Command_Query_QueryMedicine, new OperatorHandleProcessor(OperatorHandle.QueryMedicine));
            m_processors.Add(AppUtils.Command_Query_PageCharge, new OperatorHandleProcessor(OperatorHandle.PageCharge));
            m_processors.Add(AppUtils.Command_Query_QueryCharge, new OperatorHandleProcessor(OperatorHandle.QueryCharge));
            m_processors.Add(AppUtils.Command_Query_ListDoctor, new OperatorHandleProcessor(OperatorHandle.ListDoctor));
            m_processors.Add(AppUtils.Command_Query_QueryDoctor, new OperatorHandleProcessor(OperatorHandle.QueryDoctor));
            m_processors.Add(AppUtils.Command_Query_RemainBeds, new OperatorHandleProcessor(OperatorHandle.QueryRemainBeds));
            m_processors.Add(AppUtils.Command_Query_ReportListByID, new OperatorHandleProcessor(OperatorHandle.QueryReportListByCode));
            m_processors.Add(AppUtils.Command_Query_ReportDetail, new OperatorHandleProcessor(OperatorHandle.QueryReportDetailByCode));
            m_processors.Add(AppUtils.Command_Query_ReportJCListByID, new OperatorHandleProcessor(OperatorHandle.QueryReportJCListByCode));
            m_processors.Add(AppUtils.Command_Query_ReportJCDetail, new OperatorHandleProcessor(OperatorHandle.QueryReportJCDetailByCode));
            m_processors.Add(AppUtils.Command_Feature_PatientDrugInfo, new OperatorHandleProcessor(OperatorHandle.PatientDrugInfo));
            m_processors.Add(AppUtils.Command_Feature_QuerySignInfo, new OperatorHandleProcessor(OperatorHandle.QuerySignInfo));
            m_processors.Add(AppUtils.Command_Feature_QueueInfo, new OperatorHandleProcessor(OperatorHandle.QueryQueueInfo));
            m_processors.Add(AppUtils.Command_Test, new OperatorHandleProcessor(OperatorHandle.Test));
            m_processors.Add(AppUtils.Command_Log_StartLog, new OperatorHandleProcessor(OperatorHandle.StartLog));
            m_processors.Add(AppUtils.Command_Log_EndLog, new OperatorHandleProcessor(OperatorHandle.EndLog));
            m_processors.Add(AppUtils.Command_Log_ReadLog, new OperatorHandleProcessor(OperatorHandle.ReadLog));
            m_processors.Add(AppUtils.Command_Alipay_UserRegister, new OperatorHandleProcessor(OperatorHandle.UserRegister));
            m_processors.Add(AppUtils.Command_Alipay_UserRegisterBindCard, new OperatorHandleProcessor(OperatorHandle.UserRegisterBindCard));
            m_processors.Add(AppUtils.Command_Alipay_Land, new OperatorHandleProcessor(OperatorHandle.UserInfor));
            m_processors.Add(AppUtils.Command_Alipay_ModifyInfor, new OperatorHandleProcessor(OperatorHandle.ModifyInfor));
            m_processors.Add(AppUtils.Command_Alipay_AddContacts, new OperatorHandleProcessor(OperatorHandle.AddContacts));
            m_processors.Add(AppUtils.Command_Alipay_FavoriteContactsListStr, new OperatorHandleProcessor(OperatorHandle.FavoriteContactsListStr));
            m_processors.Add(AppUtils.Command_Alipay_DeleteFavoriteContactsStr, new OperatorHandleProcessor(OperatorHandle.DeleteFavoriteContactsStr));
            m_processors.Add(AppUtils.Command_Alipay_GetmzkListStr, new OperatorHandleProcessor(OperatorHandle.GetmzklistStr));
            m_processors.Add(AppUtils.Command_Alipay_GetmzkInforStr, new OperatorHandleProcessor(OperatorHandle.GetmzkInforStr));
            m_processors.Add(AppUtils.Command_Alipay_UsermzkBindStr, new OperatorHandleProcessor(OperatorHandle.UsermzkBindStr));
            m_processors.Add(AppUtils.Command_Alipay_UsermzkRelieveBindStr, new OperatorHandleProcessor(OperatorHandle.UsermzkRelieveBindStr));
            m_processors.Add(AppUtils.Command_Alipay_FavoriteContactsBindStr, new OperatorHandleProcessor(OperatorHandle.FavoriteContactsBindStr));
            m_processors.Add(AppUtils.Command_Alipay_FavoriteContactsrmzkRelieveBindStr, new OperatorHandleProcessor(OperatorHandle.FavoriteContactsrmzkRelieveBindStr));
            m_processors.Add(AppUtils.Command_Alipay_DoctorInfoXingming, new OperatorHandleProcessor(OperatorHandle.DoctorInfoXingming));
            m_processors.Add(AppUtils.Command_Alipay_DoctorInfoPinYin, new OperatorHandleProcessor(OperatorHandle.DoctorInfoPinYin));
            m_processors.Add(AppUtils.Command_Alipay_DoctorInfoDaiMa, new OperatorHandleProcessor(OperatorHandle.DoctorInfoDaiMa));
            m_processors.Add(AppUtils.Command_Alipay_DoctorInfoTingZhen, new OperatorHandleProcessor(OperatorHandle.DoctorInfoTingZhen));
            m_processors.Add(AppUtils.Command_Alipay_mzMedicalRecordsList, new OperatorHandleProcessor(OperatorHandle.mzMedicalRecordsList));
            m_processors.Add(AppUtils.Command_Alipay_ElectronicMedicalRecordt, new OperatorHandleProcessor(OperatorHandle.ElectronicMedicalRecordt));
            m_processors.Add(AppUtils.Command_Alipay_mzSingleGuideAndTakeMedicine, new OperatorHandleProcessor(OperatorHandle.mzSingleGuideAndTakeMedicine));
            m_processors.Add(AppUtils.Command_Alipay_DrugPrescriptionInforList, new OperatorHandleProcessor(OperatorHandle.DrugPrescriptionInforList));
            m_processors.Add(AppUtils.Command_Alipay_PatientsTakingDrugsInfor, new OperatorHandleProcessor(OperatorHandle.PatientsTakingDrugsInfor));
            m_processors.Add(AppUtils.Command_Alipay_AppointmentInfor, new OperatorHandleProcessor(OperatorHandle.AppointmentInfor));
            m_processors.Add(AppUtils.Command_Alipay_DepartmentSchedul, new OperatorHandleProcessor(OperatorHandle.DepartmentSchedul));
            m_processors.Add(AppUtils.Command_Alipay_ReservationDoctor1, new OperatorHandleProcessor(OperatorHandle.ReservationDoctor1));

            m_processors.Add(AppUtils.Command_Alipay_DoctorSchedul, new OperatorHandleProcessor(OperatorHandle.DoctorSchedul));
            m_processors.Add(AppUtils.Command_Alipay_mzReservationInfor, new OperatorHandleProcessor(OperatorHandle.mzReservationInfor));

            m_processors.Add(AppUtils.Command_Alipay_mzReservation, new OperatorHandleProcessor(OperatorHandle.mzReservation));
            m_processors.Add(AppUtils.Command_Alipay_mzReservationHistory, new OperatorHandleProcessor(OperatorHandle.mzReservationHistory));
            m_processors.Add(AppUtils.Command_Alipay_CancelmzReservation, new OperatorHandleProcessor(OperatorHandle.CancelmzReservation));
            m_processors.Add(AppUtils.Command_Alipay_mzReservationReport, new OperatorHandleProcessor(OperatorHandle.mzReservationReport));
            m_processors.Add(AppUtils.Command_Alipay_RegistrationNumber, new OperatorHandleProcessor(OperatorHandle.RegistrationNumber));
            m_processors.Add(AppUtils.Command_Alipay_mzReservationQueue, new OperatorHandleProcessor(OperatorHandle.mzReservationQueue));
            m_processors.Add(AppUtils.Command_Alipay_mzReservationQueueInfo, new OperatorHandleProcessor(OperatorHandle.mzReservationQueueInfo));
            m_processors.Add(AppUtils.Command_Alipay_CheckReservationReportList, new OperatorHandleProcessor(OperatorHandle.CheckReservationReportList));
            m_processors.Add(AppUtils.Command_Alipay_CheckReservationReport, new OperatorHandleProcessor(OperatorHandle.CheckReservationReport));
            m_processors.Add(AppUtils.Command_Alipay_CheckReservationReportQueueList, new OperatorHandleProcessor(OperatorHandle.CheckReservationReportQueueList));
            m_processors.Add(AppUtils.Command_Alipay_LaboratoryTestsReportList, new OperatorHandleProcessor(OperatorHandle.LaboratoryTestsReportList));
            m_processors.Add(AppUtils.Command_Alipay_LaboratoryTestsReportNameInformation, new OperatorHandleProcessor(OperatorHandle.LaboratoryTestsReportNameInformation));
            m_processors.Add(AppUtils.Command_Alipay_LaboratoryTestsReportDetailedListInformation, new OperatorHandleProcessor(OperatorHandle.LaboratoryTestsReportDetailedListInformation));
            m_processors.Add(AppUtils.Command_Alipay_InspectionReportList, new OperatorHandleProcessor(OperatorHandle.InspectionReportList));
            m_processors.Add(AppUtils.Command_Alipay_InspectionReportNameInformation, new OperatorHandleProcessor(OperatorHandle.InspectionReportNameInformation));
            m_processors.Add(AppUtils.Command_Alipay_InspectionReportResultsInformation, new OperatorHandleProcessor(OperatorHandle.InspectionReportResultsInformation));
            m_processors.Add(AppUtils.Command_Alipay_mzCreateOrder, new OperatorHandleProcessor(OperatorHandle.mzCreateOrder));//创建门诊预存订单
            m_processors.Add(AppUtils.Command_Alipay_mzPredepositList, new OperatorHandleProcessor(OperatorHandle.mzPredepositList));
            m_processors.Add(AppUtils.Command_Alipay_CancelmzPredepositList, new OperatorHandleProcessor(OperatorHandle.CancelmzPredepositList));

            m_processors.Add(AppUtils.Command_Alipay_mzPredepositSuccess, new OperatorHandleProcessor(OperatorHandle.mzPredepositSuccess));

            m_processors.Add(AppUtils.Command_Alipay_zyCreateOrder, new OperatorHandleProcessor(OperatorHandle.zyCreateOrder));
            m_processors.Add(AppUtils.Command_Alipay_zyAdvanceOrderList, new OperatorHandleProcessor(OperatorHandle.zyAdvanceOrderList));
            m_processors.Add(AppUtils.Command_Alipay_CancelzyAdvanceOrders, new OperatorHandleProcessor(OperatorHandle.CancelzyAdvanceOrders));
            m_processors.Add(AppUtils.Command_Alipay_zyAdvanceSuccess, new OperatorHandleProcessor(OperatorHandle.zyAdvanceSuccess));
            m_processors.Add(AppUtils.Command_Alipay_zyPatientInfor, new OperatorHandleProcessor(OperatorHandle.zyPatientInfor));
            m_processors.Add(AppUtils.Command_Alipay_zyCostInfo, new OperatorHandleProcessor(OperatorHandle.zyCostInfo));
            m_processors.Add(AppUtils.Command_Alipay_InformPatient, new OperatorHandleProcessor(OperatorHandle.InformPatient));
            m_processors.Add(AppUtils.Command_Alipay_CheckInformation, new OperatorHandleProcessor(OperatorHandle.CheckInformation));

            m_processors.Add(AppUtils.Command_Yun_GetJyDetailInfo, new OperatorHandleProcessor(OperatorHandle.GetJyDetailInfo));
            m_processors.Add(AppUtils.Command_Yun_GetJyInfoList, new OperatorHandleProcessor(OperatorHandle.QueryJyOrJcInfo));

            m_processors.Add(AppUtils.Command_Yun_GetJyDetailInfo2, new OperatorHandleProcessor(OperatorHandle.GetJyDetailInfo2));
            m_processors.Add(AppUtils.Command_Yun_GetJyBasicInfo, new OperatorHandleProcessor(OperatorHandle.GetJyBasicInfo));
            m_processors.Add(AppUtils.Command_Yun_GetJyInfoDetailList, new OperatorHandleProcessor(OperatorHandle.GetJyInfoDetailList));

            m_processors.Add(AppUtils.Command_Yun_QueryConnectPerson, new OperatorHandleProcessor(OperatorHandle.QueryConnectPerson));
            m_processors.Add(AppUtils.Command_Yun_QueryPatientInfo, new OperatorHandleProcessor(OperatorHandle.QueryPatientInfo));
            m_processors.Add(AppUtils.Command_Yun_GetChargeList, new OperatorHandleProcessor(OperatorHandle.GetChargeList));
            m_processors.Add(AppUtils.Command_Yun_GetChuFangDetailInfo, new OperatorHandleProcessor(OperatorHandle.GetChuFangDetailList));
            m_processors.Add(AppUtils.Command_Yun_QueryChuFangYaoPinInfo, new OperatorHandleProcessor(OperatorHandle.QueryChuFangYaoPinInfo));
            m_processors.Add(AppUtils.Command_Yun_QueryChargeListByOrderno, new OperatorHandleProcessor(OperatorHandle.QueryChargeListByOrderno));
            m_processors.Add(AppUtils.Command_Yun_QueryReceiverInfo, new OperatorHandleProcessor(OperatorHandle.QueryReceiverInfo));
            m_processors.Add(AppUtils.Command_Yun_QueryByReceiverInfo, new OperatorHandleProcessor(OperatorHandle.QueryByReceiverInfo));

            m_processors.Add(AppUtils.Command_Yun_QueryAppointment, new OperatorHandleProcessor(OperatorHandle.QueryAppointment));
            m_processors.Add(AppUtils.Command_Yun_QueryDetailAppointment, new OperatorHandleProcessor(OperatorHandle.QueryDetailAppointmentInfo));

            m_processors.Add(AppUtils.Command_Yun_QueryPaymentInfo, new OperatorHandleProcessor(OperatorHandle.QueryPaymentInfo));///预提交缴费信息  FY030303
            m_processors.Add("FY030305", new OperatorHandleProcessor(OperatorHandle.QueryPaymentInfo2));  //联众退费  FY030305 
            m_processors.Add("FY030306", new OperatorHandleProcessor(OperatorHandle.QueryPaymentInfo3));  //联众提供结算交易接口  FY030306
            m_processors.Add("OR030205", new OperatorHandleProcessor(OperatorHandle.QueryPayMoneyList2)); //联众OR030205 挂号
            m_processors.Add(AppUtils.Command_Yun_QueryPayMoney, new OperatorHandleProcessor(OperatorHandle.QueryPayMoneyList));  //联众OR030204 预挂号
            m_processors.Add("OR030206", new OperatorHandleProcessor(OperatorHandle.QueryPayMoneyList3));  //联众OR030206 退号


            m_processors.Add(AppUtils.Command_Yun_QueryChargeNumber, new OperatorHandleProcessor(OperatorHandle.QueryChargeNumber));
            m_processors.Add(AppUtils.Command_Yun_QueryPaymentList, new OperatorHandleProcessor(OperatorHandle.QueryPaymentList));
            m_processors.Add(AppUtils.Command_Yun_QueryRefundList, new OperatorHandleProcessor(OperatorHandle.QueryRefundList));

            m_processors.Add(AppUtils.Command_Yun_EditOrderStatus, new OperatorHandleProcessor(OperatorHandle.EditOrderStatus));

            m_processors.Add(AppUtils.Command_Yun_QueryPatientByCode, new OperatorHandleProcessor(OperatorHandle.QueryPaitntInfoByBrid));
            m_processors.Add(AppUtils.Command_Yun_QueryPayPatientInfo, new OperatorHandleProcessor(OperatorHandle.QueryPayPatientInfo));//FY030309

            //   m_processors.Add(AppUtils.Command_Yun_QueryOrderList1, new OperatorHandleProcessor(OperatorHandle.QueryOrderList1));//FY线程获取缴费表的订单状态为0的订单（FY030314）
            m_processors.Add(AppUtils.Command_Yun_QueryOrderList2, new OperatorHandleProcessor(OperatorHandle.QueryOrderList2));//FY030315
            //  m_processors.Add(AppUtils.Command_Yun_QueryReturnList1, new OperatorHandleProcessor(OperatorHandle.QueryReturnList1));//FY030316
            m_processors.Add(AppUtils.Command_Yun_EditReturnOrderStatus, new OperatorHandleProcessor(OperatorHandle.EditReturnStatus));
            //   m_processors.Add(AppUtils.Command_Yun_QueryReturnList2, new OperatorHandleProcessor(OperatorHandle.QueryReturnList2));//FY030318       


            m_processors.Add(AppUtils.Command_Yun_QueryChargeList, new OperatorHandleProcessor(OperatorHandle.QueryChargeList));
            m_processors.Add(AppUtils.Command_Yun_UpdateOrInsertReceiverInfo, new OperatorHandleProcessor(OperatorHandle.UpdateOrInsertReceiverInfo));
            m_processors.Add(AppUtils.Command_Yun_DeleteReceiverInfo, new OperatorHandleProcessor(OperatorHandle.DeleteReceiverInfo));
            m_processors.Add(AppUtils.Command_Yun_GetChuFangListByQuery, new OperatorHandleProcessor(OperatorHandle.GetChuFangListByQuery));
            m_processors.Add(AppUtils.Command_Yun_GetChuFangDetailByQuery, new OperatorHandleProcessor(OperatorHandle.GetChuFangDetailByQuery));
            m_processors.Add(AppUtils.Command_Yun_QueryDoctorList, new OperatorHandleProcessor(OperatorHandle.QueryDoctorList));

            m_processors.Add("FY030331", new OperatorHandleProcessor(OperatorHandle.QueryChargeListByOrderno31));
            m_processors.Add("FY030332", new OperatorHandleProcessor(OperatorHandle.QueryChargeListByOrderno32));
            m_processors.Add("FY040408", new OperatorHandleProcessor(OperatorHandle.UpdateTfddStatus));
            m_processors.Add("FY090909", new OperatorHandleProcessor(OperatorHandle.GetSfInfo));
            m_processors.Add("FY030330", new OperatorHandleProcessor(OperatorHandle.IsTuiFeiState));

            m_processors.Add("123", new OperatorHandleProcessor(OperatorHandle.IsTuiFeiState));

        }

        /// <summary>
        /// 获得当前的执行路径
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            return System.Web.HttpContext.Current.Server.MapPath("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optype"></param>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        [WebMethod]
        public string accountService(string optype, string paramxml)
        {
            XmlDocument rtdoc = null;
            ParameterHandler handler = null;
            log4net.Config.XmlConfigurator.Configure();//系统日志配置
            ILog logger = LogManager.GetLogger("log");
            try
            {
                logger.Info(optype + ":" + paramxml.Replace("<![CDATA[", "").Replace("]]>", ""));
                handler = parameterHanding(paramxml);

                if (m_processors.ContainsKey(optype))
                {
                    OperatorHandleProcessor processor = m_processors[optype];
                    rtdoc = processor(handler);
                }
                else
                {
                    rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, "未知的操作请求，请确认后重新发送");
                }
            }
            catch (Exception ex)
            {
                rtdoc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, AppUtils.Content_Payment_Excp + "[" + ex.StackTrace + "]");
                logger.Error(rtdoc.OuterXml);
            }

            if (optype == "FY030315")
            {
                return "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><root><result>success</result><message>FY030315接口被调用</message></root>";
            }

            if (optype == "FY030306")
            {
                logger.Debug(ReplyXmlDoc.GetWaitXml("程序未响应，请等待...").OuterXml);
                return "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><message><value><msg>fail</msg><msg2>100抱歉,候诊时间已过！2017-5-4 16:35:00</msg2></value></message><result>success</result></root>";
            }

            if (optype == "FY030303" || optype == "FY030305" || optype == "FY030306" || optype == "OR030204" || optype == "OR030205" || optype == "OR030206")
            {
                logger.Debug(rtdoc.OuterXml);

                //XmlDocument xdoc = new XmlDocument();
                //xdoc.LoadXml(rtdoc.OuterXml);
                //XmlNode root = xdoc.SelectSingleNode("//result");//当节点Workflow带有属性是，使用SelectSingleNode无法读取          
                //string result = root.InnerText;
                //if (result == "fail")
                //{
                //    return "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><result>fail</result><message><status>-1</status><remark>操作失败</remark></message></root>";
                //}
                return rtdoc.OuterXml.Replace("<result>fail</result>", "<result>success</result>");
            }
            XmlDeclaration delar = rtdoc.CreateXmlDeclaration("1.0", "utf-8", "no");
            XmlElement droot = rtdoc.DocumentElement;
            rtdoc.InsertBefore(delar, droot);
            return rtdoc.OuterXml;
        }

        #region 请求参数解析

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramxml"></param>
        /// <returns></returns>
        private static ParameterHandler parameterHanding(string paramxml)
        {
            if (string.IsNullOrEmpty(paramxml))
            {
                return null;
            }
            XmlDocument doc = XmlHelper.CreateXmlDocument(paramxml);
            XmlNodeList childList = doc.DocumentElement.ChildNodes;

            Dictionary<String, String> dic = new Dictionary<string, string>();
            foreach (XmlNode child in childList)
            {
                string tagname = child.Name;
                string tagvalue = child.InnerText/*.Trim ()*/;
                if (tagvalue == "null" || tagvalue == "NULL")
                {
                    tagvalue = null;
                }
                dic.Add(tagname, tagvalue);
            }

            ParameterHandler handler = new ParameterHandler(dic);
            return handler;
        }

        #endregion 请求参数解析
    }
}
