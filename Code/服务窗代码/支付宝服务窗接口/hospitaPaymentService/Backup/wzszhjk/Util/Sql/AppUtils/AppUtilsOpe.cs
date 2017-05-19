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
        //完成订单
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
        //用户信息
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
        //医生排班信息（医生所有排班信息)
        public const string Command_Alipay_DoctorSchedul = "OR020201";
        //门诊预约号源信息
        public const string Command_Alipay_mzReservationInfor = "OR020301";
        //门诊预约
        public const string Command_Alipay_mzReservation = "OR030101";
        //门诊预约历史
        public const string Command_Alipay_mzReservationHistory = "OR030201";
        //取消门诊预约
        public const string Command_Alipay_CancelmzReservation = "OR030301";
        //门诊报到
        //门诊预约报到
        public const string Command_Alipay_mzReservationReport = "PD010101";
        //挂号退号
        public const string Command_Alipay_RegistrationNumber = "PD010301";
        //门诊预约排队列表
        public const string Command_Alipay_mzReservationQueue = "PD010201";
        //门诊预约所有排队前十人员列表信息
        public const string Command_Alipay_mzReservationQueueInfo = "PD010001";
        //检查报到
        //检查预约报到排队列表
        public const string Command_Alipay_CheckReservationReportList = "PD020101";
        //检查预约报到
        public const string Command_Alipay_CheckReservationReport = "PD020201";
        //检查预约所有排队前十人员列表信息
        public const string Command_Alipay_CheckReservationReportQueueList = "PD020001";
        //检查检验
        //化验报告单列表
        public const string Command_Alipay_LaboratoryTestsReportList = "EX010101";
        //一个化验报告单抬头信息
        public const string Command_Alipay_LaboratoryTestsReportNameInformation = "EX010201";
        //一个化验报告单详细列表信息
        public const string Command_Alipay_LaboratoryTestsReportDetailedListInformation = "EX010301";
        //检验报告单列表
        public const string Command_Alipay_InspectionReportList = "EX020101";
        //一个检验报告单抬头信息
        public const string Command_Alipay_InspectionReportNameInformation = "EX020201";
        //一个检验报告单结果信息
        public const string Command_Alipay_InspectionReportResultsInformation = "EX020301";
        //门诊预存
        //创建门诊预存订单
        public const string Command_Alipay_mzCreateOrder = "PO010101";
        //门诊预存订单列表
        public const string Command_Alipay_mzPredepositList = "PO010201";
        //取消门诊预存订单
        public const string Command_Alipay_CancelmzPredepositList = "PO010301";
        //商户通知门诊预存成功
        public const string Command_Alipay_mzPredepositSuccess = "PO010401";
        //住院预缴
        //创建住院预存订单
        public const string Command_Alipay_zyCreateOrder = "PI010101";
        //住院预缴订单列表
        public const string Command_Alipay_zyAdvanceOrderList = "PI010201";
        //取消住院预缴订单
        public const string Command_Alipay_CancelzyAdvanceOrders = "PI010301";
        //商户通知住院预缴成功
        public const string Command_Alipay_zyAdvanceSuccess = "PI010401";
        //住院病人信息
        public const string Command_Alipay_zyPatientInfor = "IP010101";
        //住院费用列表
        public const string Command_Alipay_zyCostInfo = "IP020101";
        //通知病人就诊信息
        public const string Command_Alipay_InformPatient = "NF010101";
        //核对未到账的充值缴费信息
        public const string Command_Alipay_CheckInformation = "NF010201";


        /// <summary>
        /// 是否有资格使用云医院
        /// </summary>
        public const string Command_Alipay_CheckYunHospital = "CA040101";

        /// <summary>
        /// 
        /// </summary>
        public const string Command_Alipay_QueryConnectPerson = "FY030101";

        /// <summary>
        /// 获取退费处方
        /// </summary>
        public const string Command_Alipay_GetChuFangInfo = "FY030312";
    }
}