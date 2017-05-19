package unittestdefault;

import junit.framework.Test;
import junit.framework.TestSuite;

/**
 * 七院所有单元测试
 * Created by yubj on 2016/4/8.
 */
public class AllTest {
    public static Test suite() {
        TestSuite suite = new TestSuite("TestAll");
        //单元测试所有的类
//        suite.addTestSuite(GetUserInfoTest.class);  //测试用户登录,获得用户信息
//        suite.addTestSuite(UserRegisterTest.class);  //测试用户注册
//        suite.addTestSuite(AlterUserInfoTest.class);  //测试用户信息修改
//        suite.addTestSuite(AddContactTest.class);  //测试添加常用联系人
//        suite.addTestSuite(GetContactsListTest.class);//获得常用联系人列表
//        suite.addTestSuite(RemoveContactTest.class);//删除常用联系人
//        suite.addTestSuite(GetOutpatientCardsListTest.class);//获得门诊卡列表
//        suite.addTestSuite(GetOutpatientCardInfoByPatientIdTest.class);//根据病人ID获得门诊卡信息
//        suite.addTestSuite(BindOutpatientCardTest.class);//本人门诊卡绑定
//        suite.addTestSuite(UnbindOutpatientCardTest.class);//本人门诊卡解绑
//        suite.addTestSuite(BindContactOutpatientCardTest.class);//联系人门诊卡绑定
//        suite.addTestSuite(UnbindContactOutpatientCardTest.class);//联系人门诊卡解绑
//        suite.addTestSuite(GetDoctorInfoByPyTest.class);//得医生信息列表_按姓名拼音首字母查
//        suite.addTestSuite(GetDoctorInfoByCodeTest.class);//得医生信息列表_按医生代码查
//        suite.addTestSuite(GetDoctorsStopInfoTest.class);//获得医生停诊信息
//        suite.addTestSuite(GetDepartmentsListForOrderTest.class);//获得预约科室列表
//        suite.addTestSuite(GetDepartmentSchedulForOrderTest.class);//获得科室排班信息（某一天科室下还有所有医生还有多少号源）（三院该功能不支持)
//        suite.addTestSuite(GetDoctorsListForOrderTest.class);//获得预约医生列表
//        suite.addTestSuite(GetDoctorSchedulForOrderTest.class);//获得医生排班信息
//        suite.addTestSuite(GetOutpatientOrderDefaultNumbersTest.class);//获得门诊预约号源
//        suite.addTestSuite(OutpatientOrderDefaultTest.class);//门诊预约   （三院该功能不支持）
//        suite.addTestSuite(OutpatientOrderHistoryDefaultTest.class);//门诊预约历史
//        suite.addTestSuite(CancelOutpatientOrderTest.class);//取消门诊预约
//        suite.addTestSuite(GetTestsListTest.class);//获得化验单列表
//        suite.addTestSuite(GetTestInfoTest.class);//获得化验单抬头信息
//        suite.addTestSuite(GetTestIndicatorsInfoTest.class);//获得化验单指标项信息列表
//        suite.addTestSuite(GetInspectionsListTest.class);//获得检查单列表
//        suite.addTestSuite(GetInspectionInfoTest.class);//获得检查单列表
//        suite.addTestSuite(GetInspectionoResultTest.class);//获得检查单结果信息
//        suite.addTestSuite(BuildOutpatientRechargeOrderTest.class);//创建门诊充值订单
//        suite.addTestSuite(GetOutpatientRechargeOrdersListTest.class);//获得门诊充值订单列表
        suite.addTestSuite(CancelOutpatientRechargeOrderTest.class);//取消门诊充值订单
        suite.addTestSuite(OutpatientRechargeOrderFinishTest.class);//门诊充值订单完成并到账
        suite.addTestSuite(BuildInhospitalRechargeOrderTest.class);//创建住院充值订单
        suite.addTestSuite(GetInhospitalRechargeOrdersListTest.class);//获得住院充值订单列表

//        suite.addTestSuite(QuerySurplusBedTest.class);//剩余床位查询 - 查询基本功能
//        suite.addTestSuite(QueryDrugPriceTest.class);//药品价格-无查询条件、分页查询 - 查询基本功能
//        suite.addTestSuite(QueryDrugPriceByPyTest.class);//药品价格-按拼音码或名称查询 - 查询基本功能
//        suite.addTestSuite(QueryFeeItemsTest.class);//收费项目-无查询条件，分页 - 查询基本功能
//        suite.addTestSuite(QueryFeeItemsByPyTest.class);//收费项目-无查询条件，分页 - 查询基本功能
        //+ suite.addTestSuite();
        return suite;
    }
}
