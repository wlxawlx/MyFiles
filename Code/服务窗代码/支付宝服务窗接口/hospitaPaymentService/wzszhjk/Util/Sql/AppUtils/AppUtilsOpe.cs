using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public partial class AppUtils
    {
        //测试记录日志类
        public const string Command_Test = "TEST";
        public const string Command_Log_StartLog = "Log";
        public const string Command_Log_EndLog = "NoLog";
        public const string Command_Log_ReadLog = "ReadLog";

        //卡操作操作
        //卡查询
        public const string Command_Operate_QueryCard = "C001";//"A001";
        //卡绑定
        public const string Command_Operate_BindCard = "C002";//"A002";
        //判断卡是否有效
        public const string Command_Operate_VaildCard = "C003";//"A001";

        //支付类
        //订单查询
        public const string Command_Payment_OrderQuery = "P001";
        //生成订单
        public const string Command_Payment_CreateOrder = "P002";//"A003"; 
        //完成订单(订单支付)
        public const string Command_Payment_FinishOrder = "P003";//"A004";
        //作废订单
        public const string Command_Payment_CancelOrder = "P004";//"A005";
        //账户余额查询
        public const string Command_Payment_QueryBalance = "P005";//"A009";
        //账户变更查询
        public const string Command_Payment_AccountRecord = "P006";//"A009";
        //插入银行记录--(商户通知）
        public const string Command_Payment_InsertCcb = "P007";//"A007";

        ////订单状态
        public const string Command_Payment_StatusOrder = "P010";//"A006";


        //查询类
        //药品列表(增补）
        public const string Command_Query_PageMedicine = "Q001";//"A017";
        //药品查询(按拼音码）
        public const string Command_Query_QueryMedicine = "Q002";//"A013";
        //费用列表(增补）
        public const string Command_Query_PageCharge = "Q003";//"A018";
        //费用查询(按拼音码）
        public const string Command_Query_QueryCharge = "Q004";//"A014";
        //专家列表(增补）
        public const string Command_Query_PageDoctor = "Q005";//"A019";
        //专家列表(按拼音码）
        public const string Command_Query_ListDoctor = "Q006";//"A015";
        //专家介绍
        public const string Command_Query_QueryDoctor = "Q007";//"A016";
        //查询剩余床位数
        public const string Command_Query_RemainBeds = "Q008";//"A016";

        //查询检验检查报告单
        //查询报告单
        public const string Command_Query_ReportListByID = "B001";
        //查询报告单详细信息
        public const string Command_Query_ReportDetail = "B002";
        //查询检查单
        public const string Command_Query_ReportJCListByID = "B003";
        //查询检查单详细
        public const string Command_Query_ReportJCDetail = "B004";

        //中医院特色
        //取药信息查询
        public const string Command_Feature_PatientDrugInfo = "S_47052552233030211A2101_01";//"A020";
        //可预约签到信息查询
        public const string Command_Feature_QuerySignInfo = "S_47052552233030211A2101_02";
        //签到
        public const string Command_Feature_ToSign = "S_47052552233030211A2101_03";
        //排队信息查询
        public const string Command_Feature_QueueInfo = "S_47052552233030211A2101_04";

        //第三方支付平台接口
        //用户信息
        //用户注册
        public const string Command_Alipay_UserRegister = "UI010101";
        //用户注册_带绑卡
        public const string Command_Alipay_UserRegisterBindCard = "UI010102";
        

        /// <summary>
        /// 获得用户信息
        /// </summary>
        public const string Command_Alipay_Land = "UI010201";


        //用户信息修改
        public const string Command_Alipay_ModifyInfor = "UI010301";
        //添加常用联系人 
        public const string Command_Alipay_AddContacts = "UI020101";
        //获得常用联系人信息列表
        public const string Command_Alipay_FavoriteContactsListStr = "UI020201";
        //删除常用联系人
        public const string Command_Alipay_DeleteFavoriteContactsStr = "UI020301";
        //门诊卡
        //获得门诊卡列表
        public const string Command_Alipay_GetmzkListStr = "CA010101";
        //获得门诊卡信息_病人ID
        public const string Command_Alipay_GetmzkInforStr = "CA010102";
        //本人门诊卡绑定
        public const string Command_Alipay_UsermzkBindStr = "CA010201";
        //本人门诊卡解绑 
        public const string Command_Alipay_UsermzkRelieveBindStr = "CA010301";
        //常用联系人门诊卡绑定
        public const string Command_Alipay_FavoriteContactsBindStr = "CA020101";
        //常用联系人门诊卡解绑
        public const string Command_Alipay_FavoriteContactsrmzkRelieveBindStr = "CA020201";
        //医生信息
        //医生信息列表_按姓名查
        public const string Command_Alipay_DoctorInfoXingming = "DO010101";
        //医生信息列表_按姓名拼音首字母查
        public const string Command_Alipay_DoctorInfoPinYin = "DO010102";
        //医生信息列表_按医生代码查
        public const string Command_Alipay_DoctorInfoDaiMa = "DO010103";
        //医生停诊信息列表
        public const string Command_Alipay_DoctorInfoTingZhen = "DO010201";
        //门诊病历
        //门诊就诊病历列表，电子病历
        public const string Command_Alipay_mzMedicalRecordsList = "BL010101";
        //电子病历内容
        public const string Command_Alipay_ElectronicMedicalRecordt = "BL010201";
        //门诊指引单，取药
        public const string Command_Alipay_mzSingleGuideAndTakeMedicine = "BL010301";
        //病人服药
        //病人药品处方信息列表
        public const string Command_Alipay_DrugPrescriptionInforList = "FY010101";
        //病人药品服用信息
        public const string Command_Alipay_PatientsTakingDrugsInfor = "FY010201";
        //门诊预约
        //预约科室信息
        public const string Command_Alipay_AppointmentInfor = "OR010101";
        //科室排班信息（某一天科室下还有所有医生还有多少号源）
        public const string Command_Alipay_DepartmentSchedul = "OR010201";
        //预约医生信息0.1版本（科室所有医生）
        public const string Command_Alipay_ReservationDoctor1 = "OR020101";


        /// <summary>
        /// 医生排班信息（OR020201)
        /// </summary>
        public const string Command_Alipay_DoctorSchedul = "OR020201";

        /// <summary>
        /// 门诊预约号源信息(OR020301)
        /// </summary>
        public const string Command_Alipay_mzReservationInfor = "OR020301";

        /// <summary>
        /// 门诊预约接口
        /// </summary>
        public const string Command_Alipay_mzReservation = "OR030101";


        /// <summary>
        /// 门诊预约历史
        /// </summary>
        public const string Command_Alipay_mzReservationHistory = "OR030201";

        /// <summary>
        /// 取消门诊预约
        /// </summary>
        public const string Command_Alipay_CancelmzReservation = "OR030301";

        //门诊报到
        //门诊预约报到
        public const string Command_Alipay_mzReservationReport = "PD010101";

        /// <summary>
        /// 挂号退号
        /// </summary>
        public const string Command_Alipay_RegistrationNumber = "PD010301";

        /// <summary>
        /// 门诊预约排队列表
        /// </summary>
        public const string Command_Alipay_mzReservationQueue = "PD010201";

        /// <summary>
        /// 门诊预约所有排队前十人员列表信息
        /// </summary>
        public const string Command_Alipay_mzReservationQueueInfo = "PD010001";
        //检查报到
        //检查预约报到排队列表
        public const string Command_Alipay_CheckReservationReportList = "PD020101";

        /// <summary>
        /// 检查预约报到
        /// </summary>
        public const string Command_Alipay_CheckReservationReport = "PD020201";

        /// <summary>
        /// 检查预约所有排队前十人员列表信息
        /// </summary>
        public const string Command_Alipay_CheckReservationReportQueueList = "PD020001";
        //检查检验
        //化验报告单列表
        public const string Command_Alipay_LaboratoryTestsReportList = "EX010101";

        /// <summary>
        /// 一个化验报告单抬头信息
        /// </summary>
        public const string Command_Alipay_LaboratoryTestsReportNameInformation = "EX010201";
        /// <summary>
        /// 一个化验报告单详细列表信息
        /// </summary>
        public const string Command_Alipay_LaboratoryTestsReportDetailedListInformation = "EX010301";

        /// <summary>
        /// 检验报告单列表
        /// </summary>
        public const string Command_Alipay_InspectionReportList = "EX020101";

        /// <summary>
        /// 一个检验报告单抬头信息
        /// </summary>
        public const string Command_Alipay_InspectionReportNameInformation = "EX020201";

        /// <summary>
        /// 一个检验报告单结果信息
        /// </summary>
        public const string Command_Alipay_InspectionReportResultsInformation = "EX020301";

        /// <summary>
        /// 门诊预存、创建门诊预存订单
        /// </summary>
        public const string Command_Alipay_mzCreateOrder = "PO010101";

        /// <summary>
        /// 门诊预存订单列表
        /// </summary>
        public const string Command_Alipay_mzPredepositList = "PO010201";

        /// <summary>
        /// 取消门诊预存订单
        /// </summary>
        public const string Command_Alipay_CancelmzPredepositList = "PO010301";

        /// <summary>
        /// 商户通知门诊预存成功(门诊住院充值)
        /// </summary>
        public const string Command_Alipay_mzPredepositSuccess = "PO010401";

        //住院预缴
        //创建住院预存订单
        public const string Command_Alipay_zyCreateOrder = "PI010101";
        //住院预缴订单列表
        public const string Command_Alipay_zyAdvanceOrderList = "PI010201";
        //取消住院预缴订单
        public const string Command_Alipay_CancelzyAdvanceOrders = "PI010301";

        /// <summary>
        /// 商户通知住院预缴成功
        /// </summary>
        public const string Command_Alipay_zyAdvanceSuccess = "PI010401";


        /// <summary>
        /// 住院病人信息
        /// </summary>
        public const string Command_Alipay_zyPatientInfor = "IP010101";
        //住院费用列表
        public const string Command_Alipay_zyCostInfo = "IP020101";
        //通知病人就诊信息
        public const string Command_Alipay_InformPatient = "NF010101";
        //核对未到账的充值缴费信息
        public const string Command_Alipay_CheckInformation = "NF010201";


        /// <summary>
        /// 获取缴费或退费联系人列表 （FY030101）
        /// </summary>
        public const string Command_Yun_QueryConnectPerson = "FY030101";

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        public const string Command_Yun_QueryPatientInfo = "FY030102";

        /// <summary>
        /// 可退费清单
        /// </summary>
        public const string Command_Yun_GetChargeList = "FY030312";

        /// <summary>
        /// 缴费时获取处方单信息
        /// </summary>
        public const string Command_Yun_GetChuFangDetailInfo = "FY030103";

        /// <summary>
        /// 缴费时获取处方单药品详情信息
        /// </summary>
        public const string Command_Yun_QueryChuFangYaoPinInfo = "FY030201";

        /// <summary>
        /// 可退费明细
        /// </summary>
        public const string Command_Yun_QueryChargeListByOrderno = "FY030313";

        /// <summary>
        /// 获取默认收件人信息
        /// </summary>
        public const string Command_Yun_QueryReceiverInfo = "FY030301";

        /// <summary>
        /// 获取收件人信息
        /// </summary>
        public const string Command_Yun_QueryByReceiverInfo = "FY030302";

        /// <summary>
        /// 预提交缴费信息
        /// </summary>
        public const string Command_Yun_QueryPaymentInfo = "FY030303";

        /// <summary>
        /// 预约列表(OR030202)
        /// </summary>
        public const string Command_Yun_QueryAppointment = "OR030202";

        /// <summary>
        /// 预约详情接口(OR030203)
        /// </summary>
        public const string Command_Yun_QueryDetailAppointment = "OR030203";

        /// <summary>
        /// 根据预约流水号进行预挂号获取支付金额
        /// </summary>
        public const string Command_Yun_QueryPayMoney = "OR030204";

        /// <summary>
        /// 据支付宝医院订单号返回支付宝交易号 （PO010102）
        /// </summary>
        public const string Command_Yun_QueryChargeNumber = "PO010102";

        /// <summary>
        ///可缴费列表
        /// </summary>
        public const string Command_Yun_QueryPaymentList = "FY030307";

        /// <summary>
        ///可退费列表
        /// </summary>
        public const string Command_Yun_QueryRefundList = "FY030311";

        /// <summary>
        /// 更新订单状态 FY030308
        /// </summary>
        public const string Command_Yun_EditOrderStatus = "FY030308";

        /// <summary>
        ///获取病人信息（UI020202）yyy_bingrenxx_im
        /// </summary>
        public const string Command_Yun_QueryPatientByCode = "UI020202";

        /// <summary>
        /// 获取病人信息
        /// </summary>
        public const string Command_Yun_QueryPayPatientInfo = "FY030309";

        /// <summary>
        /// 线程获取缴费表的订单状态为0的订单（FY030314）
        /// </summary>
        public const string Command_Yun_QueryOrderList1 = "FY030314";

        /// <summary>
        /// 线程获取缴费表的订单状态为1的订单（FY030315）
        /// </summary>
        public const string Command_Yun_QueryOrderList2 = "FY030315";

        /// <summary>
        /// 线程获取退费表的订单状态为0的订单（FY030316）
        /// </summary>
        public const string Command_Yun_QueryReturnList1 = "FY030316";

        /// <summary>
        ///线程获取退费表的订单状态为1的订单（FY030318）
        /// </summary>
        public const string Command_Yun_QueryReturnList2 = "FY030318";

        /// <summary>
        /// 更新退费表的订单状态（FY030317）
        /// </summary>
        public const string Command_Yun_EditReturnOrderStatus = "FY030317";

        /// <summary>
        /// 获取收费记录列表（FY030320）
        /// </summary>
        public const string Command_Yun_QueryChargeList = "FY030320";

        /// <summary>
        /// 插入或更新收货人信息（FY030321）
        /// </summary>
        public const string Command_Yun_UpdateOrInsertReceiverInfo = "FY030321";

        /// <summary>
        /// 删除收货人信息（FY030322）
        /// </summary>
        public const string Command_Yun_DeleteReceiverInfo = "FY030322";

        /// <summary>
        /// 查询模块-处方单列表（FY030323）
        /// </summary>
        public const string Command_Yun_GetChuFangListByQuery = "FY030323";

        /// <summary>
        /// 查询模块-处方单详情（FY030324）
        /// </summary>
        public const string Command_Yun_GetChuFangDetailByQuery = "FY030324";

        /// <summary>
        /// 查询模块-检验单或检查单列表（FY030325）
        /// </summary>
        public const string Command_Yun_GetJyInfoList = "FY030325";

        /// <summary>
        /// 查询模块-检验单详情基本信息（FY030326）
        /// </summary>
        public const string Command_Yun_GetJyDetailInfo = "FY030326";

        /// <summary>
        /// 查询模块-检验单详情检验项目列表（FY030327）
        /// </summary>
        public const string Command_Yun_GetJyDetailInfo2 = "FY030327";

        /// <summary>
        /// 查询模块-检查单详情基本信息（FY030328）
        /// </summary>
        public const string Command_Yun_GetJyBasicInfo = "FY030328";

        /// <summary>
        ///查询模块-检查单详情检查项目列表（FY030329）
        /// </summary>
        public const string Command_Yun_GetJyInfoDetailList = "FY030329";

        /// <summary>
        /// 判断是否可退费（FY030330）
        /// </summary>
        public const string Command_Yun_IsTuiFeiState = "FY030329";

        /// <summary>
        /// 医生信息列表_按医生代码查(DO010104)
        /// </summary>
        public const string Command_Yun_QueryDoctorList = "DO010104";
    }
}