package com.jandar.alipay.core.impl;

import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.*;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingList;
import com.jandar.alipay.hospital.ServiceWindowFlag;
import com.jandar.cloud.hospital.bean.*;
import com.jandar.config.ConfigHandler;
import org.apache.commons.lang3.StringUtils;
import org.apache.log4j.Logger;

import java.util.List;
import java.util.Map;

/**
 * Created by zzw on 16/2/22. 医院服务接口,要求医院至少提供本接口相关功能
 */
public abstract class BaseHISService {

    static Logger logger = Logger.getLogger(BaseHISService.class);

    /**
     * 平台支付到账通知
     *
     * @param openId            支付人的用户ID
     * @param paymentObject     支付对象,如医人姓名
     * @param hospitalTradeNo   医院订单号
     * @param paymentTradeNo    支付平台订单号
     * @param money             支付金额,单位元
     * @param userData          用户数据,如门诊支付时的病人ID，住院支付时的住院号
     * @param paymentParameters 平台支付参数,帮助反查
     * @throws com.jandar.alipay.core.HospitalException
     */
    public boolean paymentArrivalNotify(RechargeOrderType orderType, String openId, String paymentObject, String hospitalTradeNo,
                                        String paymentTradeNo, String money, String userData,
                                        String paymentParameters) throws HospitalException {

        if (orderType == null || orderType == RechargeOrderType.OtherOrder) {
            logger.error("未知的支付订单类型." + hospitalTradeNo + paymentObject);
            throw new HospitalException("未知的支付订单类型");
        }

        RechargeOrderFinishInfo info = new RechargeOrderFinishInfo();
        info.setMoney(money);
        info.setOpenid(openId);
        info.setPatientname(paymentObject);
        info.setPaymentparameters(paymentParameters);
        info.setPaymenttradeno(paymentTradeNo);
        info.setTradeno(hospitalTradeNo);
        info.setUserdata(userData);
        if (orderType == RechargeOrderType.OutpatientOrder) {
            outpatientRechargeOrderFinish(info);
        } else if (orderType == RechargeOrderType.InHospitalOrder) {
            inhospitalRechargeOrderFinish(info);
        }
        return true;
    }
    /**
     * 向平台用户发送退费通知
     *
     * @param openId          支付人的用户ID
     * @param money           支付金额,单位元
     */
    public void refundPlatformNotifyMessage( String openId,
                                             String money,String tradeResult) {
        String title;
        StringBuilder message = new StringBuilder();
        ServiceWindowFlag serviceWindowFlag = ConfigHandler.getServiceWindowFlag();
        String hispital_code = serviceWindowFlag.toString().toLowerCase();
        String url = "http://" + ConfigHandler.getSelfServiceHost() + "/" + hispital_code + "/";

        if("success".equals(tradeResult)){
            title = "门诊结算退费成功";
            message.append("您成功退费" + money + "元");
        }else{
            title = "门诊结算退费失败";
            message.append("退费操作失败");
        }

        message.append(ConfigHandler.getHospitalName() + "\n");
        url += "accountRecord.html";
        if(StringUtils.isBlank(openId)){
            System.out.println("openid不能为空");
            return;
        }
        AlipayMsgSendUtil.sendSingleImgTextMsg(openId, title, message.toString(), url, "详情");
    }
    /**
     * 向平台用户发送到账通知
     *
     * @param openId          支付人的用户ID
     * @param paymentObject   支付对象,如医人姓名
     * @param money           支付金额,单位元
     * @param hospitalTradeNo 医院订单号
     * @param gmt_payment     到账时间
     */
    public void paymentPlatformNotifyMessage(RechargeOrderType orderType, String openId, String paymentObject,
                                             String money, String hospitalTradeNo, String gmt_payment) {
        String title;
        StringBuilder message = new StringBuilder();
        ServiceWindowFlag serviceWindowFlag = ConfigHandler.getServiceWindowFlag();
        String hispital_code = serviceWindowFlag.toString().toLowerCase();
        String url = "http://" + ConfigHandler.getSelfServiceHost() + "/" + hispital_code + "/";
        if (orderType == RechargeOrderType.InHospitalOrder) {
            title = "住院预缴成功";
            message.append("住院预缴信息:\n");
            message.append(ConfigHandler.getHospitalName() + "\n");
            message.append("姓名: " + paymentObject + "\n");
            message.append("金额: " + money + "元\n");
            message.append("时间: " + gmt_payment);
            url += "accountRecord.html";
        } else if (orderType == RechargeOrderType.OutpatientOrder) {
            title = "门诊预充成功";
            message.append("门诊预存信息:\n");
            message.append(ConfigHandler.getHospitalName() + "\n");
            message.append("姓名: " + paymentObject + "\n");
            message.append("金额: " + money + "元\n");
            message.append("时间: " + gmt_payment);
            url += "accountRecord.html";
        } else {
            title = "成功";
            message.append("您成功缴费" + money + "元");
        }
        AlipayMsgSendUtil.sendSingleImgTextMsg(openId, title, message.toString(), url, "详情");
    }

    /**
     * 和医院进行通讯
     *
     * @param operate    操作代码
     * @param parameters 参数
     * @return 返回的参数信息
     */
    protected static List<Map<String, String>> process(String operate, Map<String, Object> parameters)
            throws HospitalException {
        return WebService.invoke(operate, parameters);
    }

    /**
     * 兼容旧的代码,兼容三院还没有改过来的代码
     *
     * @param operate
     * @param parameters
     * @return
     * @throws com.jandar.alipay.core.HospitalException
     */
    @Deprecated
    public static List<Map<String, String>> oldProcess(OpCodeContext operate, Map<String, Object> parameters)
            throws HospitalException {
        return process(operate.toString(), parameters);
    }

    /**
     * 用户注册
     *
     * @param info 用户信息,结构中的 就诊卡号 与 病人ID 可为空
     * @return 注册是否成功
     */
    public abstract boolean userRegister(HISUserInfo info) throws HospitalException;

    /**
     * 用户注册及绑卡操作
     *
     * @param info 用户信息,结构中的 就诊卡号 可为空 病人ID 不能为空
     * @return 注册及绑卡是否成功
     */
    public abstract boolean userRegisterAndBindCard(HISUserInfo info) throws HospitalException;

    /**
     * 用户登录,获得用户信息
     *
     * @param openid   用户ID
     * @param usertype 用户类型（可以为空）
     * @return 用户信息
     */
    public abstract HISUserInfo getUserInfo(String openid, PlatformType usertype) throws HospitalException;

    /**
     * 用户信息修改
     *
     * @param openid   用户标识
     * @param name     姓名
     * @param phone    手机号码
     * @param idcardno 身份证号
     * @return 用户信息修改是否成功
     */
    public abstract boolean alterUserInfo(String openid, String name, String phone, String idcardno) throws HospitalException;


    /**
     * 添加常用联系人
     *
     * @param openid   用户标识
     * @param label    联系人标签
     * @param name     姓名
     * @param phone    手机号码
     * @param idcardno 身份证号
     * @param address  地址
     * @return 联系人信息
     */
    public abstract ContactPeopleInfo addContact(String openid, String label, String name, String phone, String idcardno,
                                                 String address) throws HospitalException;

    /**
     * 获得常用联系人列表
     *
     * @param openid    用户标识
     * @param linkmanid 联系人ID
     * @return 联系人列表
     */
    public abstract List<ContactPeopleInfo> getContactsList(String openid, String linkmanid) throws HospitalException;

    /**
     * 删除常用联系人
     *
     * @param openid    用户标识
     * @param linkmanid 联系人id
     * @return 联系人姓名 和 联系人id
     */
    public abstract ContactPeopleInfo removeContact(String openid, String linkmanid) throws HospitalException;

    /**
     * 获得门诊卡列表
     *
     * @param idcardno 身份证号
     * @param name     病人姓名（必选否）
     * @return 门诊卡列表
     */
    public abstract List<OutPatientCardInfo> getOutpatientCardsList(String idcardno, String name) throws HospitalException;

    /**
     * 根据病人ID获得门诊卡信息
     *
     * @param patientid 病人id
     * @return 门诊卡信息_病人
     */
    public abstract OutPatientCardInfo getOutpatientCardInfoByPatientId(String patientid) throws HospitalException;

    /**
     * 本人门诊卡绑定
     *
     * @param openid    用户标识
     * @param cardno    就诊卡号
     * @param patientid 病人ID
     * @return cardtype 就诊卡类型 cardname 就诊卡名称 cardno 就诊卡号
     */
    public abstract OutPatientCardInfo bindOutpatientCard(String openid, String cardno, String patientid) throws HospitalException;

    /**
     * 本人门诊卡解绑
     *
     * @param openid 用户标识
     * @return 解绑是否成功
     */
    public abstract boolean unbindOutpatientCard(String openid) throws HospitalException;

    /**
     * 联系人门诊卡绑定
     *
     * @param openid    用户标识
     * @param linkmanid 联系人ID(必选否)
     * @param cardno    就诊卡号
     * @param patientid 病人ID
     * @return cardtype 就诊卡类型 cardname 就诊卡名称 cardno 就诊卡号
     */
    public abstract OutPatientCardInfo bindContactOutpatientCard(String openid, String linkmanid, String cardno,
                                                                 String patientid) throws HospitalException;

    /**
     * 联系人门诊卡解绑
     *
     * @param openid    用户标识
     * @param linkmanid 联系人ID
     * @return cardno 就诊卡号
     */
    public abstract OutPatientCardInfo unbindContactOutpatientCard(String openid, String linkmanid) throws HospitalException;

    /**
     * 获得医生信息列表_按姓名查
     *
     * @param name      医生姓名
     * @param pageindex 访问页码
     * @param pagesize  每页行数
     * @return list
     */
    public abstract List<DoctorInfo> getDoctorInfoByName(String name, String pageindex, String pagesize) throws HospitalException;

    /**
     * 获得医生信息列表_按姓名拼音首字母查
     *
     * @param namepy    医生姓名拼音
     * @param pageindex 访问页码
     * @param pagesize  每页行数
     * @return list
     */
    public abstract List<DoctorInfo> getDoctorInfoByPy(String namepy, String pageindex, String pagesize) throws HospitalException;

    /**
     * 获得医生信息列表_按医生代码查
     *
     * @param code 医生代码
     * @return list
     */
    public abstract List<DoctorInfo> getDoctorInfoByCode(String code) throws HospitalException;

    /**
     * 获得医生停诊信息
     *
     * @param pageindex 访问页码
     * @param pagesize  每页行数
     * @return list
     */
    public abstract List<DoctorInfo> getDoctorsStopInfo(String pageindex, String pagesize) throws HospitalException;

    /**
     * 获得预约科室列表
     *
     * @param departcode 一级科室代码
     * @return 科室列表
     */
    public abstract List<DepartmentInfo> getDepartmentsListForOrder(String departcode) throws HospitalException;

    /**
     * 获得科室排班信息（某一天科室下还有所有医生还有多少号源）
     *
     * @param departcode 二级科室代码
     * @return list
     */
    public abstract List<SchedulingInfo> getDepartmentSchedulForOrder(String departcode) throws HospitalException;

    /**
     * 获得预约医生列表
     *
     * @param departcode   二级科室代码
     * @param scheduledate 排班日期
     * @return 医生信息list
     */
    public abstract List<DoctorInfo> getDoctorsListForOrder(String departcode, String scheduledate) throws HospitalException;

    /**
     * 获得医生排班信息
     *
     * @param doctorcode    医生代码
     * @param departcode    二级科室代码s
     * @param scheduledates 排班日期
     * @return 排班信息list
     */
    public abstract List<DoctorScheduleInfo> getDoctorSchedulForOrder(String doctorcode, String departcode, String scheduledates)
            throws HospitalException;


    /**
     * 获得门诊预约号源
     *
     * @param doctorcode,scheduleseq 医生代码,排班流水号
     * @param shiftcode              上午下午标志 可以为空
     * @return 门诊号源列表
     */
    public abstract List<OutpatientOrderNumber> getOutpatientOrderNumbers(String doctorcode, String scheduleseq, String shiftcode)
            throws HospitalException;

    /**
     * 门诊预约
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 门诊预约返回信息
     */
    public abstract OutpatientOrderReponse outpatientOrder(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException;

    /**
     * 门诊预约,只返回通知消息
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws com.jandar.alipay.core.HospitalException
     */
    public abstract String outpatientOrderResultMessage(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException;


    /**
     * 门诊预约,门诊预约,返回订单流水号和通知消息
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws com.jandar.alipay.core.HospitalException
     */
    public abstract Map<String, String> outpatientOrderResult(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException;

    /**
     * 门诊预约历史
     *
     * @param openid 用户标识
     * @return 门诊预约历史列表
     */
    public abstract List<OutpatientOrderHistory> outpatientOrderHistory(String openid) throws HospitalException;

    /**
     * 云医院预约记录，订单记录
     *
     * @param openid 用户标识
     * @return 云医院预约记录列表
     */
    public abstract List<Map<String, String>> cloudOrderList(String openid, String status) throws HospitalException;

    /**
     * 预约详情
     */
    public abstract Map<String, String> cloudOrderInfo(String preengageseq) throws HospitalException;


    /**
     * 修改预约状态为付款完成
     * @param preengageseq
     * @return
     * @throws HospitalException
     */
    public abstract Map<String, String> updateCloudOrderStatus(String preengageseq) throws HospitalException;
    /**
     * 取消门诊预约
     *
     * @param openid,preengageseq 用户标识,预约流水号
     * @return 预约信息列表
     */
    public abstract List<OutpatientOrder> cancelOutpatientOrder(String openid, String preengageseq) throws HospitalException;

    /**
     * 取消门诊预约,只返回通知消息
     *
     * @param preengageseq 门诊预约请求信息
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws com.jandar.alipay.core.HospitalException
     */
    public String cancelOutpatientOrderResultMessage(String openid, String preengageseq) throws HospitalException {
        List<OutpatientOrder> reponse = cancelOutpatientOrder(openid, preengageseq);
        // TODO
        String result = "";//reponse;
        return result;
    }

    /**
     * 获得化验单列表
     *
     * @param name,idcardno 姓名,身份证
     * @return
     */
   public abstract List<Inspection> getTestsList(String name, String idcardno) throws HospitalException;

    /**
     * 获得化验单抬头信息
     *
     * @param doctadviseno 条码号
     * @return 化验单抬头信息
     */
    public abstract InspectionInfo getTestInfo(String doctadviseno) throws HospitalException;

    /**
     * 获得化验单指标项信息列表
     *
     * @param doctadviseno 条码号
     * @return 化验单指标信息列表
     */
    public abstract List<TestIndicator> getTestIndicatorsInfo(String doctadviseno) throws HospitalException;

    /**
     * 获得检查单列表
     *
     * @param name,idcardno 姓名, 身份证
     * @return 检查单的集合
     */
    public abstract List<Inspection> getInspectionsList(String name, String idcardno) throws HospitalException;

    /**
     * 获得检查单信息
     *
     * @param doctadviseno 条码号
     * @return InspectionInfo 检查单信息
     */
    public abstract InspectionInfo getInspectionInfo(String doctadviseno) throws HospitalException;

    /**
     * 获得检查单结果信息
     *
     * @param doctadviseno 条码号
     * @return 检查单结果信息
     */
    public abstract InspectionoResult getInspectionoResult(String doctadviseno) throws HospitalException;

    /**
     * 创建门诊充值订单
     *
     * @param openid
     * @param patientname
     * @param patientidcardno
     * @param cardno
     * @param patientid
     * @param subject
     * @param money
     * @return tradeno 订单号
     */
    public abstract String buildOutpatientRechargeOrder(String openid, String patientname,
                                                        String patientidcardno, String cardno,
                                                        String patientid, String subject,
                                                        String money) throws HospitalException;


    /**
     * 获得门诊充值订单列表
     *
     * @param openid 用户标识
     * @return RechargeOrderHistoryInfo 用户充值订单信息列表
     */
    public abstract List<RechargeOrderHistoryInfo> getOutpatientRechargeOrdersList(String openid) throws HospitalException;

    /**
     * 取消门诊充值订单
     *
     * @param openId      支付宝USERID或微信OPENID
     * @param patientName 为哪个病人充值
     * @param patientId   病人ID
     * @param tradeno     医院订单号
     * @return
     * @throws com.jandar.alipay.core.HospitalException
     */
    public abstract String cancelOutpatientRechargeOrder(String openId, String patientName, String patientId, String tradeno) throws HospitalException;

    /**
     * 门诊充值订单完成并到账
     *
     * @param info 用户订单信息
     * @return tradeno 订单号
     */
    public abstract String outpatientRechargeOrderFinish(RechargeOrderFinishInfo info) throws HospitalException;

    /**
     * 创建住院充值订单
     *
     * @return tradeno 订单号
     */
    public abstract String buildInhospitalRechargeOrder(String inpatientNo, String money, String subject, String patientName, String parientIdCardNo) throws HospitalException;

    /**
     * 获得住院充值订单列表
     *
     * @param openid 用户标识
     * @return 住院充值订单列表
     */
    public abstract List<RechargeOrderHistoryInfo> getInhospitalRechargeOrdersList(String openid) throws HospitalException;

    /**
     * 取消住院充值订单
     *
     * @param openId      支付宝USERID或微信OPENID
     * @param patientName 为哪个病人充值
     * @param inpatientNo 住院号码
     * @param tradeno     医院订单号
     * @return
     * @throws com.jandar.alipay.core.HospitalException
     */
    public abstract String cancelInhospitalRechargeOrder(String openId, String patientName, String inpatientNo, String tradeno) throws HospitalException;

    /**
     * 住院充值订单完成并到账
     *
     * @param RechargeOrderFinishInfo 用户订单信息
     * @return 订单号
     */
    public abstract String inhospitalRechargeOrderFinish(RechargeOrderFinishInfo RechargeOrderFinishInfo) throws HospitalException;

    /**
     * 住院病人信息
     *
     * @param idcardno 身份证号
     * @param name     病人姓名
     * @return 住院病人信息
     */
    public abstract InhospitalPatientInfo inhospitalPatientInfo(String idcardno, String name) throws HospitalException;

    /**
     * 住院费用列表
     *
     * @param inpatientno 住院号码
     * @param costdate    查询哪一天的费用
     * @return 住院费用信息
     * @throws com.jandar.alipay.core.HospitalException
     */
    public abstract List<InHospitalOutlays> getInhospitalOutlaysList(String inpatientno, String costdate) throws HospitalException;

    /**
     * 门诊预约报到
     *
     * @param yylsh 预约流水号
     * @return 门诊预约报到是否成功
     */
    public abstract boolean getoutpatientOrder(String yylsh) throws HospitalException;

    /**
     * 门诊预约排队列表
     *
     * @return list
     */
    public abstract List<OutpatientWaitingList> getOutpatientWaitingList() throws HospitalException;


    /**
     * 挂号退号
     *
     * @param thbs 挂号标识
     * @return state 状态
     */
    public abstract boolean cancelRegistration(String thbs) throws HospitalException;


    /**
     * 门诊预约所有排队前十人员列表信息
     *
     * @return list
     */
    public abstract List<OutpatientWaitingList> getOutpatientWaitingListBeforeTen() throws HospitalException;

    /**
     * 门诊就诊病历列表
     *
     * @param openid 用户标识
     * @param brid
     * @return list
     */
    public abstract List<OutpatientVisitRecord> getOutpatientCaseList(String openid, String brid) throws HospitalException;

    /**
     * 电子病历内容
     *
     * @param jzxh 就诊序号
     * @return 电子病历内容
     */
    public abstract OutpatientVisitInfo getOutpatientVisitInfoDetail(String jzxh) throws HospitalException;

    /**
     * 门诊指引单
     *
     * @param jzxh 就诊序号
     * @return 门诊指引单内容
     */
    public abstract OutpatientVisitGuideBills getOutpatientVisitGuideBillsDetail(String jzxh) throws HospitalException;

    /**
     * 病人药品处方信息列表(服药信息列表)
     *
     * @param openid 用户标识
     * @return list
     */
    public abstract List<MedicineUseInfoList> getMedicineUseInfoListDetail(String openid, String brid) throws HospitalException;

    /**
     * 病人药品服用信息(服药信息)
     *
     * @param cflsh 处方流水号
     * @return list
     */
    public abstract List<MedicineUseInfo> getMedicineUseInfoDetail(String cflsh) throws HospitalException;

    /**
     * 检查预约报到排队列表
     *
     * @param openid   用户标识
     * @param queueseq 排队流水号
     * @return list
     */
    public abstract List<OutpatientWaitingList> checkPrecontractReportedList(String openid, String queueseq) throws HospitalException;

    /**
     * 检查预约报到
     *
     * @param openid       用户标识
     * @param preengageseq 预约流水号
     * @return boolean
     */
    public abstract boolean checkPrecontractReported(String openid, String preengageseq) throws HospitalException;

    /**
     * 检查预约所有排队前十人员列表信息
     *
     * @return list
     */
    public abstract List<OutpatientWaitingList> checkOutpatientWaitingListBeforeTen() throws HospitalException;

    /**
     * 剩余床位查询 - 查询基本功能
     *
     * @param
     * @return bqmc     病区名称
     * sycw     剩余床位
     */
    public abstract List<BedQueryInfo> querySurplusBed() throws HospitalException;

    /**
     * 药品价格-无查询条件、分页查询 - 查询基本功能
     *
     * @param pageno  访问页码
     * @param pagerow 每页显示行数
     * @return 药品价格
     */
    public abstract List<DrugPriceInfo> queryDrugPrice(String pageno, String pagerow) throws HospitalException;

    /**
     * 药品价格-按拼音码或名称查询 - 查询基本功能
     *
     * @param pydm 拼音代码
     */
    public abstract List<DrugPriceInfo> queryDrugPriceByPy(String pydm) throws HospitalException;

    /**
     * 收费项目-无查询条件，分页 - 查询基本功能
     *
     * @param pageno  访问页码
     * @param pagerow 每页显示行数
     */
    public abstract List<FeeItemsInfo> queryFeeItems(String pageno, String pagerow) throws HospitalException;

    /**
     * 收费项目-按拼音码或名称查询 - 查询基本功能
     *
     * @param pydm 拼音代码
     */
    public abstract List<FeeItemsInfo> queryFeeItemsByPy(String pydm) throws HospitalException;

    /**
     * 通知病人就诊信息
     *
     * @param
     * @return userid 用户ID
     * brxm  病人姓名
     * ysxm 医生姓名
     * jzdz 就诊地址
     * jzsj 就诊时间
     */
    public abstract List<VisitInfo> InformVisitInfo() throws HospitalException;

    /**
     * 核对未到账的充值缴费信息
     *
     * @param
     * @return out_trade_no 订单支付时传入的商户订单号,和支付宝交易号不能同时为空。trade_no,out_trade_no如果同时存在优先取trade_no
     * trade_no 支付宝交易号，和商户订单号不能同时为空
     */
    public abstract List<RechargeOrderFinishInfo> CheckNotAccountInfo() throws HospitalException;



    /***
     * --------------------------------下面是云医院的接口---------------------
     * */

    /**
     * 根据openId,查询是否是复诊病人
     * @param patientId
     * @return
     * @throws com.jandar.alipay.core.HospitalException
     * @return  true 是 false 否
     */
    public abstract boolean getIsRepeat(String patientId) throws HospitalException;

    /**
     * 根据patientId查询处方列表
     * @param patientId
     * @return
     * @throws HospitalException
     */
   public abstract List<Map<String, String>> getPrescriptionList(String patientId) throws HospitalException;

    /**
     * 根据lsCode查询处方详情
     * @param lsCode
     * @return
     * @throws HospitalException
     */
    public  abstract List<Map<String, String>> getPrescriptionDetail(String lsCode) throws HospitalException;
    /**
     * 根据openId获取联系人列表
     * @param openId
     * @return
     * @throws HospitalException
     */
   public abstract List<Map<String, String>> getContactPersonList(String openId) throws HospitalException;
    /**
     * 通用方法
     * @param hCode
     * @param parameters
     * @return
     * @throws HospitalException
     */
    public abstract List<Map<String, String>> commonService(String hCode,Map<String, Object> parameters) throws HospitalException;

    /**
     * 根据医生代码和病人代码，获取预约日期为当天的预约及录
     * @param doctorCode
     * @param patientCode
     */
    public abstract Map<String, String> getAppoint(String doctorCode, String patientCode) throws HospitalException;

    /**
     * 根据病人代码查看病人信息
     */
    public abstract Map<String, String> getPatientInfo(String patientCode) throws HospitalException;
}
