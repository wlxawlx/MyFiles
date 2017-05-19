using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public class SQLUtils
    {
        //查询门诊病人信息
        public string mzbrQueryStr;
        //查询住院病人信息
        public string zybrQueryStr;

        //门诊病人绑卡信息
        public string mzbrBindCardstr;
        //住院病人绑卡信息
        public string zybrBindCardstr;

        //门诊病人绑卡号码是否有效
        public string mzbrValidBkhm;
        //住院病人绑卡号码是否有效
        public string zybrValidBkhm;

        //门诊病人创建订单
        public string mzbrForCreateOrder;
        //住院病人创建订单
        public string zybrForCreateOrder;

        //门诊病人查询余额
        public string mzbrQueryBalance;
        //住院病人查询余额
        public string zybrQueryBalance;

        //门诊病人id是否有效
        public string mzbrValidId;
        //住院病人id是否有效
        public string zybrValidId;

        //门诊病人报告列表
        public string mzbrReportList;
        //住院病人报告列表
        public string zybrReportList;

        //查询报告明细通过报告单号
        public string reportDetailByBgdh;
        //查询报告明细通过报告条码
        public string reportDetailBySbm;
        //查询报告明细
        public string reportDetailMX;

        //查询剩余床位
        public string remaindBeds;

        //通过汉字或拼音查询药品信息
        public string medicineByPydm;
        //分页通过汉字或拼音查询药品信息
        public string medicinePage;

        //通过汉字或拼音查询收费信息
        public string chargeByPydm;
        //分页通过汉字或拼音查询收费信息
        public string chargePage;

        //分页查询门诊病人账户信息
        public string accountListMzByPage;
        //门诊病人账户信息
        public string accountListMz;

        //分页查询住院病人账户信息
        public string accountListZyByPage;
        //住院病人账户信息
        public string accountListZy;

        //门诊病人检查单
        public string mzbrReportJC;
        //住院病人检查单
        public string zybrReportJC;
        //通过报告单号查询检查单
        public string reportJCDetailByBgdh;
        //通过条码查询检查单
        public string reportJCDetailBySbm;
        //查询检查单明细
        public string reportJCDetailMX;

        //用户注册
        public string userRegisterStr;
        //用户注册（绑卡）
        public string userRegisterBindCardStr;
        //用户信息
        public string landStr;
        //修改用户信息
        public string modifylandStr;
        //添加常用联系人
        public string addContactsStr;
        //常用联系人信息列表
        public string favoriteContactsListStr;
        //常用联系人信息列表(联系人ID)
        public string favoriteContactsListStrWithLinkmanid;
        //删除常用联系人
        public string deleteFavoriteContactsStr;
        //获得常用联系人信息
		public string findLinkManNameStr;
        //获得门诊卡列表
        public string getmzklistStr;
        //获得门诊卡信息_病人ID
        public string getmzkInforStr;
        //本人门诊卡绑定
        public string usermzkBindStr;
        //更新本人门诊卡绑定
        public string updateUsermzkBindInfoStr;
        //本人门诊卡解绑
        public string usermzkRelieveBindStr;
        //openid获取绑卡号码
        public string getCardNoFromIDStr;
        //根据openid,linkmanid获取常用联系人绑卡号码
        public string getLinkmanCardNoFromIDStr;
        //更新常用联系人门诊卡绑定
        public string updateLinkManBindInfoSqlStr;
        //常用联系人门诊卡解绑
        public string favoriteContactsrmzkRelieveBindStr;
        //化验报告单列表
        public string ailpaybrReportList;
        //一个化验报告单抬头信息
        public string laboratoryTestsReportNameInformation;
        //一个化验报告单详细列表信息
        public string getLaboratoryTestsReportDetailedListInformation;
        //检验报告单列表
        public string getInspectionReportList;
        //检验报告单列表(心电)
        public string getInspectionReportListWithECG;
        //一个检验报告单抬头信息
        public string getInspectionReportNameInformation;
        //一个检验报告单抬头信息(心电)
        public string getInspectionReportNameInformationWithECG;
        //一个检验报告单详细列表信息
        public string getDInspectionReportResultsInformation;
        //一个检验报告单详细列表信息(心电)
        public string getDInspectionReportResultsInformationWithECG;
        //医生信息列表_按姓名查
        public string dctorInfoXingming;
        //预约科室信息(科室代码)
        public string appointmentInforWithDepartcode;
        //预约科室信息
        public string appointmentInfor;
        //科室排班信息
        public string departmentSchedul;
        //预约医生信息(排班日期)
        public string reservationDoctorWithScheduledate;
        //预约医生信息
        public string reservationDoctor;
        //医生排班信息
        public string doctorSchedul;
        //医生排班信息(医生代码)
        public string doctorSchedulWithDoctorcode;
        //医生排班信息(医生代码，科室代码)
        public string doctorSchedulWithDoctorcodeAndDepartcode;
        //医生排班信息(科室代码，排班日期)
        public string doctorSchedulWithDoctorcodeAndScheduledate;
        //医生信息列表_按姓名拼音首字母查
        public string dctorInfoPinYin;
        //医生信息列表_按医生代码查
        public string doctorInfoByCode;
        //医生停诊信息列表
        public string doctorInfoTingZhen;
        //住院病人信息（带姓名）
        public string getZhuYuanPatientInfoStrWithName;
        //住院病人信息
        public string getZhuYuanPatientInfoStr;
        //住院费用信息
        public string getZhuYuanCostInfoStr;
        //住院费用信息(费用日期)
        public string getZhuYuanCostInfoStrWithCostdate;
        //门诊预约历史
        public string mzReservationHistory;
        //门诊号源信息
        public string mzReservationInforSql;
        //门诊号源时间信息
        public string mzReservationTimeInforSql;
        //门诊预约返回信息
        public string mzOrderRetInfoSql;
        //取消门诊预约返回信息
        public string cancelmzOrderRetInfoSql;
        //门诊病人卡号是否有效
        public string mzbrValidCardno;
        //核对未到账的充值缴费信息
        public string CheckInformation;
        //通知病人就诊信息
        public string InformPatient;
        //病人药品处方信息列表
        public string DrugPrescriptionInforList;
        //病人药品服用信息
        public string PatientsTakingDrugsInfor;
        //门诊就诊病历列表
        public string mzMedicalRecordsList;
        //电子病历内容
        public string ElectronicMedicalRecordt;
        //门诊指引单
        public string mzSingleGuideAndTakeMedicine;
        //检查预约报到排队列表
        public string CheckReservationReportList;
        //门诊预约报到排队列表
        public string mzReservationQueue;
        //门诊预约报到排队列表（排队流水号）
        public string mzReservationQueueWithQueueseq;
        //查询挂号ID
        public string Queueseq;
        //查询挂号ID(挂号ID)
        public string QueueseqWithQueueseq;
        //查询住院号码
        public string IsZYBrxxValid;
        //查询医生姓名，上下午
        public string SelectDoctorname;
        //查询排班医生
        public string SelectPaibanYS;
        //查询openid，预约流水号
        public string GetSelectOpenidsql;


        public SQLUtils()
        {
            mzbrQueryStr = "";
            zybrQueryStr = "";

            mzbrBindCardstr = "";
            zybrBindCardstr = "'";

            mzbrValidBkhm = "";
            zybrValidBkhm = "";

            mzbrForCreateOrder = "";
            zybrForCreateOrder = "";

            mzbrQueryBalance = "'";
            zybrQueryBalance = "";

            mzbrValidId = "";
            zybrValidId = "";

            mzbrReportList = "";
            zybrReportList = "";

            reportDetailByBgdh = "";
            reportDetailBySbm = "";

            reportDetailMX = "";


            remaindBeds = "";

            medicineByPydm = "";

            medicinePage = "";
            chargeByPydm = "";

            chargePage = "";

            accountListMzByPage = "";
            accountListMz = "";

            accountListZyByPage = "";
            accountListZy = "";

            mzbrReportJC = "";
            zybrReportJC = "";
            reportJCDetailByBgdh = "";
            reportJCDetailBySbm = "";
            reportJCDetailMX = "";

            userRegisterStr = "";
            userRegisterBindCardStr = "";
            landStr = "";
            modifylandStr = "";
            addContactsStr = "";
            favoriteContactsListStr = "";
            deleteFavoriteContactsStr = "";
			findLinkManNameStr = "";
            getmzklistStr = "";
            getmzkInforStr = "";
            usermzkBindStr = "";
            updateUsermzkBindInfoStr = "";
            usermzkRelieveBindStr = "";
            getCardNoFromIDStr = "";
            getLinkmanCardNoFromIDStr = ""; 
            updateLinkManBindInfoSqlStr = "";
            favoriteContactsrmzkRelieveBindStr = "";
            ailpaybrReportList = "";
            laboratoryTestsReportNameInformation = "";
            getLaboratoryTestsReportDetailedListInformation = "";
            getInspectionReportList = "";
            getInspectionReportListWithECG = "";
            getInspectionReportNameInformation = "";
            getInspectionReportNameInformationWithECG = "";
            getDInspectionReportResultsInformation = "";
            getDInspectionReportResultsInformationWithECG = "";
            dctorInfoXingming = "";
            appointmentInforWithDepartcode = "";
            appointmentInfor = "";
            departmentSchedul = "";
            reservationDoctorWithScheduledate = "";
            reservationDoctor = "";
            doctorSchedul = "";
            doctorSchedulWithDoctorcode = "";
            dctorInfoPinYin = "";
            doctorInfoByCode = "";
            doctorInfoTingZhen = "";
            getZhuYuanPatientInfoStrWithName = "";
            getZhuYuanPatientInfoStr = "";
            getZhuYuanCostInfoStr = "";
            getZhuYuanCostInfoStrWithCostdate = "";
            mzReservationHistory = "";
            mzReservationInforSql = "";
            mzReservationTimeInforSql = "";
            mzOrderRetInfoSql = "";
            cancelmzOrderRetInfoSql = "";
            mzbrValidCardno = "";
            doctorSchedulWithDoctorcodeAndDepartcode = "";
            doctorSchedulWithDoctorcodeAndScheduledate = "";
            CheckInformation = "";
            InformPatient = "";
            DrugPrescriptionInforList = "";
            PatientsTakingDrugsInfor = "";
            mzMedicalRecordsList = "";
            ElectronicMedicalRecordt = "";
            mzSingleGuideAndTakeMedicine = "";
            CheckReservationReportList = "";
            mzReservationQueue = "";
            mzReservationQueueWithQueueseq = "";
            Queueseq = "";
            QueueseqWithQueueseq = "";
            IsZYBrxxValid = "";
            favoriteContactsListStrWithLinkmanid = "";
            SelectDoctorname = "";
            SelectPaibanYS = "";
            GetSelectOpenidsql = "";
        }
    }
}