using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Model
{
    //检验列表
    public struct Inspection
    {
        public string doctadviseno; //条码号
        public string examinaim; //检查项目
        public string requesttime;//申请时间
        public string requester; //申请医生
    }

    //检验病人信息
    public struct InspectionHead
    {
        public string doctadviseno; //条码号
        public string requesttime; //申请时间
        public string requester;//申请医生
        public string executetime;//采集时间
        public string executer;//采集人
        public string receivetime;//接收时间
        public string receiver;//接收人
        public string stayhospitalmode;//标本来源
        public string patientid;//病人编号
        public string section;//申请科室
        public string bedno;//床号
        public string patientname;//病人姓名
        public string sex;//性别
        public string age;//年龄
        public string diagnostic;//诊断
        public string sampletype;//标本类型
        public string examinaim;//检查项目
        public string requestmode;//平/急诊
        public string checker;//审核人
        public string checktime;//审核时间
        public string csyq;//测试仪器
        public string profiletest;//测试方法
    }

    //检验结果
    public struct InspectionDetial
    {
        public string jylx;   //检验类型
        public string xmmc;   //项目名称
        public string result; //结果
        public string hint;   //异常提示
        public string ckfw;   //参考范围
        public string xmdw;   //项目单位
        public string micmc;  //细菌项目名称
        public string mictpjg; //细菌涂培养结果
        public string micjg;   //细菌结果
        public string mickss;  //细菌抗生素
        public string micdl;   //细菌定量
        public string micdx;   //细菌定性
    }

    //检查列表
    public struct CheckReportInfo
    {
        public string doctadviseno; //条码号
        public string examinaim; //检查项目
        public string requesttime;//申请时间
        public string requester; //申请医生
    }
    //检查病人信息
    public struct CheckHead
    {
        public string doctadviseno; //条码号
        public string requesttime; //申请时间
        public string requester;//申请医生
        public string executetime;//采集时间
        public string executer;//采集人
        public string receivetime;//接收时间
        public string receiver;//接收人
        public string stayhospitalmode;//标本来源
        public string patientid;//病人编号
        public string section;//申请科室
        public string bedno;//床号
        public string patientname;//病人姓名
        public string sex;//性别
        public string age;//年龄
        public string diagnostic;//诊断
        public string sampletype;//标本类型
        public string examinaim;//检查项目
        public string requestmode;//平/急诊
        public string checker;//审核人
        public string checktime;//审核时间
        public string csyq;//测试仪器
        public string profiletest;//测试方法
    }

    //检查结果
    public struct CheckDetial
    {
        public string studyresult;   //检查所见
        public string diagresult;   //检查诊断
    }

}