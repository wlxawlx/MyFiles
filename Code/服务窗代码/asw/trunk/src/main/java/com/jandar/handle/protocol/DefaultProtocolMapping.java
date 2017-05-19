/**
 *
 */
package com.jandar.handle.protocol;

import com.jandar.cloud.hospital.protocol.CloudHospitalProtocol;
import com.jandar.handle.protocol.impl.*;
import com.jandar.handle.protocol.impl.wzsrmyy.*;
import com.jandar.handle.protocol.impl.wzstjzx.*;
import com.jandar.handle.protocol.proxy.BindCardValidationProxy;
import com.jandar.handle.protocol.proxy.LoginValidationProxy;
import com.jandar.util.SpringBeanUtil;

import java.util.HashMap;
import java.util.Map;


/**
 * @author Administrator
 */
class DefaultProtocolMapping extends ProtocolMapping {

    private static Map<String, Protocol> handlers = new HashMap<String, Protocol>();

    public DefaultProtocolMapping() {

        if (!handlers.isEmpty()) {
            return;
        }

        handlers.put("BA010301", new LoginValidationProxy(new PersonCenterProtocol()));//获得用户信息
        handlers.put("BA010302", new PersonCenterProtocol_Alipay());//获得用户信息_支付宝
        handlers.put("BA010101", new UserRegisterProtocol());//用户注册;
        handlers.put("BA020101", new LoginValidationProxy(new AddContactPeopleProtocol()));//添加常用联系人
        handlers.put("BA020201", new LoginValidationProxy(new ContactPeopleList()));//获得常用联系人信息  
        handlers.put("BA010201", new EditRegisterProtocol());//修改用户信息  #测通#  后台能修改信息  （页面没做）
        handlers.put("BA020301", new LoginValidationProxy(new DeleteContactPeopleProtocol()));//删除常用联系人
        handlers.put("BA020401", new LoginValidationProxy(new ContactPeopleBindCardProtocol()));//常用联系人绑卡
        handlers.put("BA020501", new LoginValidationProxy(new ContactPeopleRelieveCardProtocol()));//常用联系人就诊卡解绑
        handlers.put("BA030501", new LoginValidationProxy(new ContactPeopleAlreadyBindCardProtocol()));//获得常用联系人已绑卡信息
        handlers.put("EX010101", new LoginValidationProxy(new TestsListProtocol()));//化验单列表     亲，没有记录
        handlers.put("EX010201", new TestsHeaderProtocol());//化验单信息     00
        handlers.put("EX010301", new TestsDetailProtocol());//化验单明细信息  00
        handlers.put("EX020101", new LoginValidationProxy(new TestsCheckListProtocol()));//检查单列表    亲，没有记录
        handlers.put("EX020201", new TestsCheckHeadProtocol());//检查单信息   亲，没有记录
        handlers.put("EX020301", new TestCheckDetailProtocol());//检查单明细信息   亲，该检查号码不存在
//        handlers.put("PA010101", new LoginValidationProxy(new BindCardValidationProxy(new OutPatientCreateOrderProtocol())));//门诊充值-创建订单
        handlers.put("PA010102", new LoginValidationProxy(new OutPatientCreateOrderProtocol_Others()));//门诊充值-创建订单_他人00
        handlers.put("PA010201", new LoginValidationProxy(new OutPatientDeleteOrderProtocol()));//门诊充值-取消订单		测试结果数据异常，请联系系统管理人员00


        handlers.put("PA010301", new LoginValidationProxy(new BindCardValidationProxy(new OutpatientPrepaidOrderListProtocol())));//门诊充值-订单列表0.1版  测试结果 还未绑卡
        handlers.put("PA010302", new LoginValidationProxy(new OutpatientPrepaidOrderListProtocol_V2()));//门诊充值-订单列表0.2版  测试结果 还未绑卡


        handlers.put("BA030101", new LoginValidationProxy(new CardListProtocol()));//获取医院就诊卡信息
        handlers.put("BA030102", new LoginValidationProxy(new CardListProtocol_Others()));//获取医院就诊卡信息_他人
        handlers.put("BA030103", new CardListProtocol());//获取医院就诊卡信息
        handlers.put("BA030201", new LoginValidationProxy(new BindCardProtocol()));//绑定就诊卡协议
        handlers.put("BA030301", new LoginValidationProxy(new BindCardValidationProxy(new AlreadyBindCardProtocol())));//获得已绑卡信息协议
        handlers.put("BA030401", new LoginValidationProxy(new RelieveCardProtocol()));//就诊卡解绑协议
        handlers.put("YY010101", new PrecontractSectionListProtocol());//预约科室列表(二级科室列表)  #测通#00
        handlers.put("YY010102", new PrecontractSectionSingleListProtocol());//预约科室列表(一级科室列表)  #测通#00
        handlers.put("YYOLD010201", new PrecontractDoctorListProtocol());//预约医生列表     #测通#00
        handlers.put("YY010301", new DoctorScheduleProtocol());//医生排班列表0.1版  #测通#00
        handlers.put("YY010302", new DoctorScheduleProtocol_V2());//医生排班列表0.2版   #测通#00
        handlers.put("YY010303", new DoctorScheduleProtocol_V3());//医生排班列表0.3版  #测通#00
        handlers.put("YY010304", new DoctorScheduleProtocol_V4());//医生排班列表0.4版  #测通#00
        handlers.put("YY010401", new PrecontractSourceProtocol());//预约号源列表   测试通过，
        handlers.put("YY010501", new LoginValidationProxy(new OutpatientPrecontractProtocol()));//预约协议
        handlers.put("YY010601", new LoginValidationProxy(new PrecontractInfoListProtocol()));//预约信息列表	测试通过
        handlers.put("YY010701", new LoginValidationProxy(new PrecontractCancelProtocol()));//取消预约协议		测试结果 该预约记录不存在

        handlers.put("YY020101", new LoginValidationProxy(new BindCardValidationProxy(new OutpatientPrecontractCheckinProtocol())));//门诊预约报到协议
        handlers.put("YY020201", new LoginValidationProxy(new BindCardValidationProxy(new OutpatientQueueListProtocol())));//门诊排队信息列表协议
        handlers.put("YY020301", new OutpatientWaitingInfoProtocol());//门诊候诊信息  #没有排队记录
        handlers.put("YY020302", new LoginValidationProxy(new BindCardValidationProxy(new OutpatientWaitingInfoProtocol_V2())));//门诊候诊信息  没有排队记录
        handlers.put("YY020401", new CancelRegistrationProtocol());//挂号退号

        handlers.put("BA040101", new HospitalInfoProtocol());//医院信息协议   #测通 #显示为技能信息00页面显示正确
        handlers.put("BA040201", new DoctorQueryProtocol());//医生列表查询   #测通#00
        handlers.put("BA040301", new DoctorInfoProtocol());//医生信息   #测通#00
        handlers.put("BA040401", new DoctorStopInfoProtocol());//医生停诊信息    #测通#00

        handlers.put("IN010102", new InpatientPayInfoQuery());//住院信息-住院费用0.2版     00

        handlers.put("IN010201", new LoginValidationProxy(new InpatientPayDetailProtocol_V1()));//住院信息-住院费用明细0.1版   测试结果:该身份证号没有患者住院!
        handlers.put("IN010202", new InpatientPayDetailProtocol());// 住院信息-住院费用明细0.2版   //#测通#
        handlers.put("IN020101", new InpatientInfoListProtocol());//住院信息-病人信息列表0.1版本    //00
        handlers.put("IN020102", new LoginValidationProxy(new InpatientInfoListProtocol_V2()));//住院信息-病人信息列表0.2版本   // 00

//        handlers.put("PA020101", new LoginValidationProxy(new InpatientCreateTopUpOrderProtocol()));//住院充值-创建订单0.1版
        handlers.put("PA020102", new LoginValidationProxy(new InpatientCreateTopUpOrderProtocol_V2()));//住院充值-创建订单0.2版  测试通过  00
        handlers.put("PA020201", new LoginValidationProxy(new InpatientCancelTopUpOrderProtocol()));//住院充值-取消订单   00
        handlers.put("PA020301", new LoginValidationProxy(new InpatientTopUpOrderListProtocol()));//住院充值-订单列表   //亲,没有订单信息  00
        //handlers.put("PA020302", new InpatientTopUpOrderListProtocol_V2());//住院充值-订单列表0.2版

        handlers.put("IN010103", new LoginValidationProxy(new HospitalizationInformationCostsProtocol_V3()));//住院信息-住院费用0.3版  	//00
        handlers.put("IN010203", new LoginValidationProxy(new HospitalizationInformationDetailProtocol_V3()));//住院信息-住院费用明细0.3版		//00

        /** 温州市人民医院（三院）功能 begin*/
        handlers.put("SK010101", new TechnicalCenterCoursesClassifyListProtocol());//技能中心-培训课程分类列表
        handlers.put("SK010201", new TechnicalCenterCoursesListProtocol());//技能中心-培训课程列表
        handlers.put("SK010301", new LoginValidationProxy(new SkillApplyPayment())); // 技能中心-课程报名-缴费
        handlers.put("SK010501", new LoginValidationProxy(new CourseRegistrationListProtocol()));//技能中心-已课程报名列表
        handlers.put("SK010401", new CourseCancellationRegistrationProtocol());//技能中心-取消课程报名
        /** 温州市人民医院（三院）功能 end*/

        /** 体检中心功能 begin*/
        handlers.put("TJ010101", new PhysicalPackagesListProtocol());//体检套餐列表
        handlers.put("TJ010201", new PhysicalPackagesDetailProtocol());//体检套餐详情
        handlers.put("TJ010301", new LoginValidationProxy(new PhysicalOrderProtocol()));//体检预约
        handlers.put("TJ010401", new LoginValidationProxy(new PhysicalOrderListProtocol()));//体检已预约列表
        handlers.put("TJ010501", new LoginValidationProxy(new PhysicalReportProtocol()));//体检详细报告信息查询
        /** 体检中心功能 end*/

        handlers.put("SM010101", new GuideBodySubpartProtocol());//智能导诊-身体部位
        handlers.put("SM010201", new GuideSubpartSymptomClassificsProtocol());//智能导诊-部位症状分类
        // 测试数据	{"partcode":"0101","classificcode":"04","pageindex":"1","pagesize":"20"}
        handlers.put("SM010301", new GuideSubpartSymptomProtocol());//智能导诊-部位症状列表
        // 测试数据	{"partcode":"0101","classificcode":"04","pageindex":"1","pagesize":"20"}
        handlers.put("SM010401", new GuideSubpartSymptomDetailProtocol());//智能导诊-部位症状详细信息
        handlers.put("SM010501", new GuideSubpartSymptomOtherSymptomProtocol());//智能导诊-部位症状相关其它症状
        // 测试数据	{"partcode":"0401","classficcode":"09","sex":"1","symptomcode":"04289","correlate_codes":""}
        //			{"partcode":"0101","classficcode":"02","sex":"1","symptomcode":"00001","correlate_codes":"00006,00007,00009"}
        handlers.put("SM010601", new GuideSubpartSymptomSicknessProtocol());//智能导诊-部位症状相关疾病


        handlers.put("BD010101", new LoginValidationProxy(new BindCardValidationProxy(new CheckPrecontractReportedListProtocol())));//检查预约报道列表
        handlers.put("BD010201", new LoginValidationProxy(new CheckPrecontractReportedProtocol()));//检查预约报到
        handlers.put("BD010301", new LoginValidationProxy(new CheckWaitingInfoProtocol()));//检查候诊信息

        handlers.put("FY010101", new LoginValidationProxy(new BindCardValidationProxy(new MedicineUseInfoListProtocol())));//服药信息列表
        handlers.put("FY010201", new LoginValidationProxy(new MedicineUseInfoProtocol()));//服药信息

        handlers.put("JZ010101", new LoginValidationProxy(new BindCardValidationProxy(new OutpatientVisitRecordProtocol())));//门诊就诊记录列表
        handlers.put("JZ010201", new LoginValidationProxy(new OutpatientVisitInfoProtocol()));//门诊就诊信息（电子病历）
        handlers.put("JZ010301", new LoginValidationProxy(new OutpatientVisitGuideBillsProtocol()));//门诊就诊指引单

        handlers.put("DR010101", new LoginValidationProxy(new DepartmentsListForOrderofDayProtocol()));//当日预约挂号-科室列表
        handlers.put("DR010201", new LoginValidationProxy(new DoctorListofDayByDepartmentsProtocol()));//当日预约挂号-预约科室查医生列表
        handlers.put("DR010301", new LoginValidationProxy(new BindCardValidationProxy(new ConfirmRegistrationProtocol())));//当日预约挂号-确认挂号

        handlers.put("JB010101", new QuerySurplusBedProtocol());//剩余床位查询 - 查询基本功能
        handlers.put("JB010201", new QueryDrugPriceProtocol());//药品价格-无查询条件、分页查询 - 查询基本功能
        handlers.put("JB010202", new QueryDrugPriceByPyProtocol());//药品价格-按拼音码或名称查询 - 查询基本功能
        handlers.put("JB010301", new QueryFeeItemsProtocol());//收费项目-无查询条件，分页 - 查询基本功能
        handlers.put("JB010302", new QueryFeeItemsByPyProtocol());//收费项目-按拼音码或名称查询 - 查询基本功能

        registerCloudHospitalProtocol();
    }

    private void registerCloudHospitalProtocol() {
        Map<String, CloudHospitalProtocol> map = SpringBeanUtil.getBeansOfType(CloudHospitalProtocol.class);
        if (map != null) {
            for (Map.Entry<String, CloudHospitalProtocol> e : map.entrySet()) {
                handlers.put(e.getValue().getProtocolCode(), e.getValue());
            }
        }
    }

    public Protocol getHandler(String pcode, Map<String, Object> params) {
        return handlers.get(pcode);
    }

}
