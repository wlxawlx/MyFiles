package com.jandar.alipay.core.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.impl.servicedefault.*;
import com.jandar.alipay.core.struct.*;
//import com.jandar.alipay.core.struct.Inspection;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingList;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.DateUtil;
import com.jandar.cloud.hospital.bean.*;
import jodd.util.StringUtil;
import org.apache.commons.collections.map.LinkedMap;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;


/**
 * Created by zzw on 16/2/23. 默认及全局的医院信息系统服务接口
 */
public class HISServiceHandlerDefault extends BaseHISService {

    /**
     * 用户注册
     *
     * @param info 用户信息,结构中的 就诊卡号 与 病人ID 可为空
     * @return 注册是否成功
     */
    @Override
    public boolean userRegister(HISUserInfo info) throws HospitalException {
        /***单元测试**/
        UserRegister userRegister = new UserRegister();
        Map<String, Object> parameters = userRegister.userRegisterRequest(info);
        List<Map<String, String>> process = process("UI010101", parameters);
        return userRegister.userRegisterResponse(process);
        /***单元测试**/
    }

    /**
     * 用户注册及绑卡操作 (三院用户注册时进行绑卡的功能不支持)
     *
     * @param info 用户信息,结构中的 就诊卡号 可为空 病人ID 不能为空
     * @return 注册及绑卡是否成功
     */
    @Override
    public boolean userRegisterAndBindCard(HISUserInfo info) throws HospitalException {
        /***单元测试**/
        UserRegister userRegister = new UserRegister();
        Map<String, Object> parameters = userRegister.userRegisterRequest(info);
        List<Map<String, String>> process = process("UI010102", parameters);
        return userRegister.userRegisterResponse(process);
        /***单元测试**/
    }

    /**
     * 用户登录,获得用户信息
     *
     * @param openid   用户标识
     * @param usertype 用户类型
     * @return 用户信息
     */
    @Override
    public HISUserInfo getUserInfo(String openid, PlatformType usertype) throws HospitalException {
        /***单元测试**/
        GetUserInfo getUserInfo = new GetUserInfo();
        Map<String, Object> parameters = getUserInfo.getUserInfoRequest(openid, usertype);
        List<Map<String, String>> process = null;
        try {
            process = process("UI010201", parameters);
        } catch (HospitalException e) {
            if (e.getMessage().contains("该登陆信息未注册") ||
                    e.getMessage().contains("未注册")) {
                throw new HospitalException(e.getMessage(), HospitalException.UNARCHIV);
            } else {
                throw e;
            }
        }
        HISUserInfo info = getUserInfo.getUserInfoResponse(process);
        return info;
        /***单元测试**/
    }

    /**
     * 用户信息修改
     *
     * @param openid   用户标识
     * @param name     姓名
     * @param phone    手机号码
     * @param idcardno 身份证号
     * @return 用户信息修改是否成功
     */
    @Override
    public boolean alterUserInfo(String openid, String name, String phone, String idcardno) throws HospitalException {
        /***单元测试**/
        AlterUserInfo alterUserInfo = new AlterUserInfo();
        Map<String, Object> parameters = alterUserInfo.alterUserInfoRequest(openid, name, phone, idcardno);
        /***单元测试**/

        List<Map<String, String>> process = process("UI010301", parameters);

        /***单元测试**/
        return alterUserInfo.alterUserInfoResponse(process);
        /***单元测试**/
    }

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
    @Override
    public ContactPeopleInfo addContact(String openid, String label, String name, String phone, String idcardno,
                                        String address) throws HospitalException {
        /***单元测试**/
        AddContact addContact = new AddContact();
        Map<String, Object> parameters = addContact.addContactRequest(openid, label, name, phone, idcardno, address);
        List<Map<String, String>> process = process("UI020101", parameters);
        ContactPeopleInfo cpinfo = new ContactPeopleInfo();
        cpinfo = addContact.addContactResponse(process);
        return cpinfo;
        /***单元测试**/
    }

    /**
     * 获得常用联系人列表
     *
     * @param openid    用户标识
     * @param linkmanid 联系人ID
     * @return 联系人列表
     */
    @Override
    public List<ContactPeopleInfo> getContactsList(String openid, String linkmanid) throws HospitalException {
        List<ContactPeopleInfo> result = new ArrayList<ContactPeopleInfo>();
        // 常用联系人第一个永远为自大,ID值为-1
        if (StringUtil.isBlank(linkmanid) || "-1".equals(linkmanid)) {
            HISUserInfo userInfo = getUserInfo(openid, PlatformType.Alipay);
            if (userInfo != null) {
                ContactPeopleInfo cpinfo = new ContactPeopleInfo();
                cpinfo.setLinkmanid("-1");
                cpinfo.setLabel("");
                cpinfo.setName(userInfo.getName());
                cpinfo.setPhone(userInfo.getPhone());
                cpinfo.setIdcardno(userInfo.getIdcardno());
                cpinfo.setAddress(userInfo.getAddress());
                cpinfo.setBindcardfalg(userInfo.getBkzt());
                cpinfo.setInpatentflag(userInfo.getInpatentflag());
                cpinfo.setPatientid(userInfo.getPatientid());
                cpinfo.setCardno(userInfo.getCardno());
                result.add(cpinfo);
            }
            if ("-1".equals(linkmanid)) {
                return result;
            }
        }

        /***单元测试**/
        GetContactsList getContactsList = new GetContactsList();
        Map<String, Object> parameters = getContactsList.getContactsListRequest(openid, linkmanid);
        /***单元测试**/
        try {
            List<Map<String, String>> contacts = process("UI020201", parameters);

            /***单元测试**/
            getContactsList.getContactsListResponse(contacts, linkmanid, result);
            /***单元测试**/
        } catch (HospitalException e) {
            // 没有联系人信息，请添加联系人！
            // 处理没有联系人也抛出异常的问题
            if (!e.getMessage().contains("没有联系人信息")) {
                throw e;
            }
        }

        return result;
    }

    /**
     * 删除常用联系人
     *
     * @param openid    用户标识
     * @param linkmanid 联系人id
     * @return 联系人姓名 和 联系人id
     */
    @Override
    public ContactPeopleInfo removeContact(String openid, String linkmanid) throws HospitalException {
        /***单元测试**/
        RemoveContact removeContact = new RemoveContact();
        Map<String, Object> parameters = removeContact.removeContactRequest(openid, linkmanid);
        /***单元测试**/

        List<Map<String, String>> process = process("UI020301", parameters);

        /***单元测试**/
        return removeContact.removeContactResponse(process);
        /***单元测试**/
    }

    /**
     * 获得门诊卡列表
     *
     * @param idcardno 身份证号
     * @param name     病人姓名
     * @return 门诊卡列表
     */
    @Override
    public List<OutPatientCardInfo> getOutpatientCardsList(String idcardno, String name) throws HospitalException {
        /***单元测试**/
        GetOutpatientCardsList getOutpatientCardsList = new GetOutpatientCardsList();
        Map<String, Object> parameters = getOutpatientCardsList.getOutpatientCardsListRequest(idcardno, name);
        /***单元测试**/

        List<Map<String, String>> process = process("CA010101", parameters);

        /***单元测试**/
        return getOutpatientCardsList.getOutpatientCardsListResponse(process);
        /***单元测试**/

    }
    /**
     * 根据预约流水号返回订单号,支付宝交易号
     */
    public  Map<String, String> getTradeNoAndBatchNoByOrderNo (String orderNo) throws HospitalException {
        Map<String, Object> parameters =new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        List<Map<String, String>> processResult = process("PO010102", parameters);
        Map<String, String> result= processResult.get(0);
        return result;
    }
    /**
     * 根据预约流水号返回订单号,支付宝交易号
     */
    public  Map<String, String> getTradeNoAndBatchNoByOrderNo (String orderNo,String payType) throws HospitalException {
        Map<String, Object> parameters =new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        parameters.put("paytype",payType);
        List<Map<String, String>> processResult = process("PO010102", parameters);
        Map<String, String> result= processResult.get(0);
        return result;
    }

    /**
     * 根据病人ID获得门诊卡信息  （三院该功能不支持）
     *
     * @param patientid 病人id
     * @return 门诊卡信息_病人
     */
    @Override
    public OutPatientCardInfo getOutpatientCardInfoByPatientId(String patientid) throws HospitalException {
        /***单元测试**/
        GetOutpatientCardInfoByPatientId getOutpatientCardInfoByPatientId = new GetOutpatientCardInfoByPatientId();
        Map<String, Object> parameters = getOutpatientCardInfoByPatientId.getOutpatientCardInfoByPatientIdRequest(patientid);
        /***单元测试**/

        List<Map<String, String>> process = process("CA010102", parameters);

        /***单元测试**/
        OutPatientCardInfo opinfo = new OutPatientCardInfo();
        opinfo = getOutpatientCardInfoByPatientId.getOutpatientCardInfoByPatientIdResponse(process);
        return opinfo;
        /***单元测试**/
    }

    /**
     * 本人门诊卡绑定
     *
     * @param openid    用户标识
     * @param cardno    就诊卡号
     * @param patientid 病人ID
     * @return cardtype 就诊卡类型 cardname 就诊卡名称 cardno 就诊卡号
     */
    @Override
    public OutPatientCardInfo bindOutpatientCard(String openid, String cardno, String patientid)
            throws HospitalException {
        /***单元测试**/
        BindOutpatientCard bindOutpatientCard = new BindOutpatientCard();
        Map<String, Object> parameters = bindOutpatientCard.bindOutpatientCardRequest(openid, cardno, patientid);
        /***单元测试**/

        List<Map<String, String>> process = process("CA010201", parameters);

        /***单元测试**/
        OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        ocinfo = bindOutpatientCard.bindOutpatientCardResponse(process);
        return ocinfo;
        /***单元测试**/
    }

    /**
     * 本人门诊卡解绑
     *
     * @param openid 用户标识
     * @return 解绑是否成功
     */
    @Override
    public boolean unbindOutpatientCard(String openid) throws HospitalException {

        /***单元测试**/
        UnbindOutpatientCard unbindOutpatientCard = new UnbindOutpatientCard();
        Map<String, Object> parameters = unbindOutpatientCard.unbindOutpatientCardRequest(openid);
        /***单元测试**/

        List<Map<String, String>> process = process("CA010301", parameters);

        /***单元测试**/
        return unbindOutpatientCard.unbindOutpatientCardResponse(process);
        /***单元测试**/
    }

    /**
     * 联系人门诊卡绑定
     *
     * @param openid    用户标识
     * @param linkmanid 联系人ID
     * @param cardno    就诊卡号
     * @param patientid 病人ID
     * @return cardtype 就诊卡类型 cardname 就诊卡名称 cardno 就诊卡号
     */
    @Override
    public OutPatientCardInfo bindContactOutpatientCard(String openid, String linkmanid, String cardno,
                                                        String patientid) throws HospitalException {

        /***单元测试**/
        BindContactOutpatientCard bindContactOutpatientCard = new BindContactOutpatientCard();
        Map<String, Object> parameter = bindContactOutpatientCard.bindContactOutpatientCardRequest(openid, linkmanid, cardno, patientid);
        /***单元测试**/

        List<Map<String, String>> process = process("CA020101", parameter);

        /***单元测试**/
        OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        ocinfo = bindContactOutpatientCard.bindContactOutpatientCardResponse(process, patientid);
        return ocinfo;
        /***单元测试**/
    }

    /**
     * 联系人门诊卡解绑
     *
     * @param openid    用户标识
     * @param linkmanid 联系人ID
     * @return cardno 就诊卡号
     */
    @Override
    public OutPatientCardInfo unbindContactOutpatientCard(String openid, String linkmanid) throws HospitalException {
        /***单元测试**/
        UnbindContactOutpatientCard unbindContactOutpatientCard = new UnbindContactOutpatientCard();
        Map<String, Object> parameters = unbindContactOutpatientCard.unbindContactOutpatientCardRequest(openid, linkmanid);
        /***单元测试**/

        List<Map<String, String>> process = process("CA020201", parameters);

        /***单元测试**/
        OutPatientCardInfo cpinfo = unbindContactOutpatientCard.unbindContactOutpatientCardResponse(process);
        return cpinfo;
        /***单元测试**/
    }

    /**
     * 获得医生信息列表_按姓名查
     *
     * @param name      医生姓名
     * @param pageindex 访问页码
     * @param pagesize  每页行数
     * @return list
     */
    @Override
    public List<DoctorInfo> getDoctorInfoByName(String name, String pageindex, String pagesize) throws HospitalException {

        return getDoctorInfoByPy(name, pageindex, pagesize);
    }

    /**
     * 获得医生信息列表_按姓名拼音首字母查
     *
     * @param namepy    医生姓名拼音
     * @param pageindex 访问页码
     * @param pagesize  每页行数
     * @return list
     */
    @Override
    public List<DoctorInfo> getDoctorInfoByPy(String namepy, String pageindex, String pagesize) throws HospitalException {

        /***单元测试**/
        GetDoctorInfoByPy getDoctorInfoByPy = new GetDoctorInfoByPy();
        Map<String, Object> parameters = getDoctorInfoByPy.getDoctorInfoByPyRequest(namepy, pageindex, pagesize);
        /***单元测试**/

        List<Map<String, String>> process = process("DO010102", parameters);

        /***单元测试**/
        List<DoctorInfo> dcinfolist = getDoctorInfoByPy.getDoctorInfoByPyResponse(process);
        return dcinfolist;
        /***单元测试**/
    }

    /**
     * 获得医生信息列表_按医生代码查
     *
     * @param code 医生代码
     * @return list
     */
    @Override
    public List<DoctorInfo> getDoctorInfoByCode(String code) throws HospitalException {

        /***单元测试**/
        GetDoctorInfoByCode getDoctorInfoByCode = new GetDoctorInfoByCode();
        Map<String, Object> parameters = getDoctorInfoByCode.getDoctorInfoByCodeRequest(code);
        /***单元测试**/

        List<Map<String, String>> process = process("DO010103", parameters);

        /***单元测试**/
        List<DoctorInfo> dcinfolist = getDoctorInfoByCode.getDoctorInfoByCodeResponse(process);
        return dcinfolist;
        /***单元测试**/
    }

    /**
     * 获得医生停诊信息
     *
     * @param pageindex 访问页码
     * @param pagesize  每页行数
     * @return list
     */
    @Override
    public List<DoctorInfo> getDoctorsStopInfo(String pageindex, String pagesize) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("pageindex",pageindex);
        parameters.put("pagesize",pagesize);
        List<Map<String, String>> process = process("DO010201", parameters);

        /***单元测试**/
        GetDoctorsStopInfo getDoctorsStopInfo = new GetDoctorsStopInfo();
        List<DoctorInfo> dcinfolist = getDoctorsStopInfo.getDoctorsStopInfoResponse(process);
        return dcinfolist;
        /***单元测试**/
    }

    /**
     * 获得预约科室列表
     *
     * @param departcode 一级科室代码
     * @return 科室列表
     */
    @Override
    public List<DepartmentInfo> getDepartmentsListForOrder(String departcode) throws HospitalException {

        /***单元测试**/
        GetDepartmentsListForOrder getDepartmentsListForOrder = new GetDepartmentsListForOrder();
        Map<String, Object> parameters = getDepartmentsListForOrder.getDepartmentsListForOrderRequest(departcode);
        /***单元测试**/

        List<Map<String, String>> process = process("OR010101", parameters);

        /***单元测试**/
        List<DepartmentInfo> deinfolist = getDepartmentsListForOrder.getDepartmentsListForOrderResponse(process);
        return deinfolist;
        /***单元测试**/
    }

    /**
     * 获得科室排班信息（某一天科室下还有所有医生还有多少号源）（三院该功能不支持)
     *
     * @param departcode 二级科室代码
     * @return list
     */
    public List<SchedulingInfo> getDepartmentSchedulForOrder(String departcode) throws HospitalException {

        /***单元测试**/
        GetDepartmentSchedulForOrder getDepartmentSchedulForOrder = new GetDepartmentSchedulForOrder();
        Map<String, Object> parameters = getDepartmentSchedulForOrder.getDepartmentSchedulForOrderRequest(departcode);
        /***单元测试**/

        List<Map<String, String>> process = process("OR010201", parameters);

        /***单元测试**/
        List<SchedulingInfo> scinfo = getDepartmentSchedulForOrder.getDepartmentSchedulForOrderResponse(process);
        return scinfo;
        /***单元测试**/
    }

    /**
     * 获得预约医生列表
     *
     * @param departcode   二级科室代码
     * @param scheduledate 排班日期
     * @return 医生信息list
     */
    public List<DoctorInfo> getDoctorsListForOrder(String departcode, String scheduledate) throws HospitalException {

        /***单元测试**/
        GetDoctorsListForOrder getDoctorsListForOrder = new GetDoctorsListForOrder();
        Map<String, Object> parameters = getDoctorsListForOrder.getDoctorsListForOrderRequest(departcode, scheduledate);
        /***单元测试**/

        List<Map<String, String>> process = process("OR020101", parameters);

        /***单元测试**/
        List<DoctorInfo> dcinfolist = getDoctorsListForOrder.getDoctorsListForOrderResponse(process);
        return dcinfolist;
        /***单元测试**/
    }

    /**
     * 获得医生排班信息
     *
     * @param doctorcode   医生代码
     * @param departcode   二级科室代码
     * @param scheduledate 排班日期
     * @return 排班信息list
     */
    @Override
    public List<DoctorScheduleInfo> getDoctorSchedulForOrder(String doctorcode, String departcode, String scheduledate)
            throws HospitalException {

        /***单元测试**/
        GetDoctorSchedulForOrder getDoctorSchedulForOrder = new GetDoctorSchedulForOrder();
        Map<String, Object> parameters = getDoctorSchedulForOrder.getDoctorSchedulForOrderRequest(doctorcode, departcode, scheduledate);
        /***单元测试**/

        List<Map<String, String>> process = process("OR020201", parameters);

        /***单元测试**/
        List<DoctorScheduleInfo> dslist = getDoctorSchedulForOrder.getDoctorSchedulForOrderResponse(process);
        return dslist;
        /***单元测试**/
    }


    /**
     * 获得门诊预约号源
     *
     * @param doctorcode,scheduleseq 医生代码,排班流水号
     * @return 门诊号源列表
     */
    @Override
    public List<OutpatientOrderNumber> getOutpatientOrderNumbers(String doctorcode, String scheduleseq, String shiftcode)
            throws HospitalException {
        /***单元测试**/
        GetOutpatientOrderNumbers getOutpatientOrderNumbers = new GetOutpatientOrderNumbers();
        Map<String, Object> params = getOutpatientOrderNumbers.getOutpatientOrderNumbersRequest(doctorcode, scheduleseq, shiftcode);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("OR020301", params);


        /***单元测试**/
        List<OutpatientOrderNumber> results = getOutpatientOrderNumbers.getOutpatientOrderNumbersResponse(dataList);
        return results;
        /***单元测试**/
    }

    /**
     * 门诊预约   （三院该功能不支持）
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 门诊预约返回信息
     */
    @Override
    public OutpatientOrderReponse outpatientOrder(OutpatientOrderRequest outpatientOrderRequest)
            throws HospitalException {


        /***单元测试**/
        OutpatientOrderDefault outpatientOrderDefault = new OutpatientOrderDefault();
        Map<String, Object> params = outpatientOrderDefault.outpatientOrderDefaultRequest(outpatientOrderRequest);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("OR030101", params);


        /***单元测试**/
        OutpatientOrderReponse result = outpatientOrderDefault.outpatientOrderDefaultResponse(dataList);
        return result;
        /***单元测试**/
    }

    /**
     * 门诊预约,只返回通知消息
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws HospitalException
     */
    @Override
    public String outpatientOrderResultMessage(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException {
        OutpatientOrderReponse reponse = outpatientOrder(outpatientOrderRequest);
        String result = reponse.getPatientname() + "您好,"
                + "您已成功预约(" + reponse.getPreengagedate() + "/" + DateUtil.translateDate2Week(reponse.getPreengagedate()) + ")"
                + "\n" + reponse.getDepartname() +
                "--" + outpatientOrderRequest.getDoctorName() + "医生门诊,"
                + "预计就诊时间" + "大概在" + "\n"
                + reponse.getPreengagetime() + "!";
        return result;
    }

    /**
     * 门诊预约,返回订单流水号和通知消息
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws HospitalException
     */
    @Override
    public Map<String, String> outpatientOrderResult(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException {
        Map<String, String> map =  new HashMap<String, String>();
        OutpatientOrderReponse reponse = outpatientOrder(outpatientOrderRequest);
        String result = reponse.getPatientname() + "您好,"
                + "您已成功预约(" + reponse.getPreengagedate() + "/" + DateUtil.translateDate2Week(reponse.getPreengagedate()) + ")"
                + "\n" + reponse.getDepartname() +
                "--" + reponse.getDoctorname() + "医生门诊,"
                + "预计就诊时间" + "大概在" + "\n"
                + reponse.getPreengagetime() + "!";
        map.put("ordermsg",result);
        map.put("orderid",reponse.getPreengageseq());
        return map;
    }

    /**
     * 门诊预约历史
     *
     * @param openid 用户标识
     * @return 门诊预约历史列表
     */
    @Override
    public List<OutpatientOrderHistory> outpatientOrderHistory(String openid) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        params.put("openid", info.getAlipayUserId());


        // 从数据库获取数据
        List<Map<String, String>> dataList = process("OR030201", params);

        /***单元测试**/
        OutpatientOrderHistoryDefault outpatientOrderHistoryDefault = new OutpatientOrderHistoryDefault();
        List<OutpatientOrderHistory> results = outpatientOrderHistoryDefault.outpatientOrderHistoryDefaultResponse(dataList);
        return results;
        /***单元测试**/
    }

    /**
     * 云医院预约记录，订单记录
     *
     * @param openid 用户标识
     * @return 云医院预约记录列表
     */
    @Override
    public List<Map<String, String>> cloudOrderList(String openid,String status) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        params.put("openid", info.getAlipayUserId());
        params.put("status", status);
        // 从数据库获取数据
        List<Map<String, String>> orderList = process("OR030202", params);
        return orderList;
    }

    public List<Map<String, String>> cloudOrderList(String openid,String status,String sourcetype) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        params.put("openid", info.getAlipayUserId());
        params.put("status", status);
        params.put("sourcetype", sourcetype);
        // 从数据库获取数据
        List<Map<String, String>> orderList = process("OR030202", params);
        return orderList;
    }

    /**
     * 预约详情
     */
    @Override
    public Map<String, String> cloudOrderInfo(String preengageseq) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("preengageseq", preengageseq);
        // 从数据库获取数据
        List<Map<String, String>> orderList = process("OR030203", params);
        return orderList.get(0);
    }

    /**
     * 预挂号,获取金额
     * @param preengageseq
     * @return
     * @throws HospitalException
     */
    public Map<String, String> iniOrder(String preengageseq,String patientid) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("preengageseq", preengageseq);
        params.put("patientid", patientid);
        // 从数据库获取数据
        List<Map<String, String>> orderList = process("OR030204", params);
        return orderList.get(0);
    }

    /**
     * 云医院预约记录付款成功后，修改该预约记录
     */
    @Override
    public Map<String, String> updateCloudOrderStatus(String preengageseq) throws HospitalException{
        HashMap<String, Object> params = new HashMap<>();
        params.put("preengageseq", preengageseq);
        // 从数据库获取数据
        List<Map<String, String>> orderList = process("OR030203", params);
        return orderList.get(0);
    }

    /**
     * 取消门诊预约
     *
     * @param openid,preengageseq 用户标识,预约流水号
     * @return 预约信息列表
     */
    @Override
    public List<OutpatientOrder> cancelOutpatientOrder(String openid, String preengageseq) throws HospitalException {


        /***单元测试**/
        CancelOutpatientOrder cancelOutpatientOrder = new CancelOutpatientOrder();
        Map<String, Object> params = cancelOutpatientOrder.cancelOutpatientOrderRequest(openid, preengageseq);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("OR030301", params);

        /***单元测试**/
        List<OutpatientOrder> results = cancelOutpatientOrder.cancelOutpatientOrderResponse(dataList);
        return results;
        /***单元测试**/
    }

    /**
     * 取消门诊预约,只返回通知消息
     *
     * @param openid
     * @param preengageseq
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws HospitalException
     */
    @Override
    public String cancelOutpatientOrderResultMessage(String openid, String preengageseq) throws HospitalException {
        List<OutpatientOrder> reponse = cancelOutpatientOrder(openid, preengageseq);
        OutpatientOrder info = reponse.get(0);
        String result = info.getPatientname() + " 您好,您的预约("
                + info.getPreengagedate() + "/" + DateUtil.translateDate2Week(info.getPreengagedate()) +
                ")" + info.getDepartname() + "--" + info.getDoctorname()
                + "医生" + info.getPreengageno() + "位门诊,预约时间" + info.getPreengagetime() + "已作废!";
        return result;
    }

    /**
     * 取消已付款的预约（取消挂号）
     * @param registerId
     * @return 返回门诊预约的通知消息, 非详细信息
     * * @throws HospitalException
     */
    public Map<String, String> cancelYY(String registerId,String batchno) throws HospitalException {
        Map<String, Object> params = new HashMap<>();
        params.put("registerid", registerId);
        List<Map<String, String>> reponseList = process("OR030206", params);
        return reponseList.get(0);
    }

    /**
     * 获得化验单列表
     *
     * @param name,idcardno 姓名,身份证
     * @return
     */
//    @Override
    public List<Inspection> getTestsList(String name, String idcardno) throws HospitalException {
//
//        /***单元测试**/
        GetTestsList getTestsList = new GetTestsList();
        Map<String, Object> params = getTestsList.getTestsListRequest(name, idcardno);
//        /***单元测试**/
//        // 从数据库获取数据
        List<Map<String, String>> dataList = process("EX010101", params);
//
//        /***单元测试**/
        List<Inspection> results = getTestsList.getTestsListResponse(dataList);
        return results;
//        /***单元测试**/
    }

    /**
     * 获得化验单抬头信息
     *
     * @param doctadviseno 条码号
     * @return 化验单抬头信息
     */
    @Override
    public InspectionInfo getTestInfo(String doctadviseno) throws HospitalException {

        /***单元测试**/
        GetTestInfo getTestInfo = new GetTestInfo();
        Map<String, Object> params = getTestInfo.getTestInfoRequest(doctadviseno);
        /***单元测试**/

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("EX010201", params);

        System.out.println("==========getTestInfo============"+dataList.size());

        /***单元测试**/
        InspectionInfo result = getTestInfo.getTestInfoResponse(dataList);
        return result;
        /***单元测试**/
    }

    /**
     * 获得化验单指标项信息列表
     *
     * @param doctadviseno 条码号
     * @return 化验单指标信息列表
     */
    @Override
    public List<TestIndicator> getTestIndicatorsInfo(String doctadviseno) throws HospitalException {

        /***单元测试**/
        GetTestIndicatorsInfo getTestIndicatorsInfo = new GetTestIndicatorsInfo();
        Map<String, Object> params = getTestIndicatorsInfo.getTestIndicatorsInfoRequest(doctadviseno);
        /***单元测试**/

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("EX010301", params);

        /***单元测试**/
        List<TestIndicator> results = getTestIndicatorsInfo.getTestIndicatorsInfoResponse(dataList);
        return results;
        /***单元测试**/
    }

    /**
     * 获得检查单列表
     *
     * @param name,idcardno 姓名, 身份证
     * @return 检查单的集合
     */
//    @Override
    public List<Inspection> getInspectionsList(String name, String idcardno) throws HospitalException {
//
//        /***单元测试**/
        GetInspectionsList getInspectionsList = new GetInspectionsList();
        Map<String, Object> params = getInspectionsList.getInspectionsListRequest(name, idcardno);
//        /***单元测试**/
//
//        // 从数据库获取数据
        List<Map<String, String>> dataList = process("EX020101", params);
//
//        /***单元测试**/
        List<Inspection> results = getInspectionsList.getInspectionsListResponse(dataList);
        return results;
//        /***单元测试**/
    }

    /**
     * 获得检查单信息
     *
     * @param doctadviseno 条码号
     * @return InspectionInfo 检查单信息
     */
    @Override
    public InspectionInfo getInspectionInfo(String doctadviseno) throws HospitalException {

        /***单元测试**/
        GetInspectionInfo getInspectionInfo = new GetInspectionInfo();
        Map<String, Object> params = getInspectionInfo.getInspectionInfoRequest(doctadviseno);
        /***单元测试**/

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("EX020201", params);

        /***单元测试**/
        InspectionInfo result = getInspectionInfo.getInspectionInfoResponse(dataList);
        return result;
        /***单元测试**/
    }

    /**
     * 获得检查单结果信息
     *
     * @param doctadviseno 条码号
     * @return 检查单结果信息
     */
    @Override
    public InspectionoResult getInspectionoResult(String doctadviseno) throws HospitalException {

        /***单元测试**/
        GetInspectionoResult getInspectionoResult = new GetInspectionoResult();
        Map<String, Object> params = getInspectionoResult.getInspectionoResultRequest(doctadviseno);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("EX020301", params);

        /***单元测试**/
        InspectionoResult result = getInspectionoResult.getInspectionoResultResponse(dataList);
        return result;
        /***单元测试**/
    }

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
    @Override
    public String buildOutpatientRechargeOrder(String openid, String patientname, String patientidcardno
            , String cardno, String patientid, String subject, String money)
            throws HospitalException {

        /***单元测试**/
        BuildOutpatientRechargeOrder buildOutpatientRechargeOrder = new BuildOutpatientRechargeOrder();
        Map<String, Object> params = buildOutpatientRechargeOrder.buildOutpatientRechargeOrderRequest(openid, patientname, patientidcardno, cardno, patientid, subject, money);
        /***单元测试**/

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PO010101", params);


        /***单元测试**/
        return buildOutpatientRechargeOrder.buildOutpatientRechargeOrderResponse(dataList);
        /***单元测试**/
    }
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
    public String buildOutpatientRechargeOrder(String openid, String patientname, String patientidcardno
            , String cardno, String patientid, String subject, String money,String preengageseq,String sourcetype)
            throws HospitalException {

        /***单元测试**/
        BuildOutpatientRechargeOrder buildOutpatientRechargeOrder = new BuildOutpatientRechargeOrder();
        Map<String, Object> params = buildOutpatientRechargeOrder.buildOutpatientRechargeOrderRequest(openid, patientname, patientidcardno, cardno, patientid, subject, money, preengageseq,sourcetype);
        /***单元测试**/

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PO010101", params);


        /***单元测试**/
        return buildOutpatientRechargeOrder.buildOutpatientRechargeOrderResponse(dataList);
        /***单元测试**/
    }
    /**
     * 根据预约订单号进行挂号落地
     *
     * @param orderNo
     * @param payMoney
     * @return registerid 挂号id
     */
    public String buildRegister(String orderNo,String preengageseq,String payMoney)
            throws HospitalException {
        Map<String, Object> params=new HashMap<>();
        params.put("orderno",orderNo);
        params.put("xzfje",payMoney);
        params.put("paymenttype","20");
        params.put("preengageseq",preengageseq);
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("OR030205", params);
        if(dataList.size()>0){
            return dataList.get(0).get("msg");
        }
        return "fail";
    }
    /**
     * 获得门诊充值订单列表
     *
     * @param openid 用户标识
     * @return RechargeOrderHistoryInfo 用户充值订单信息
     */
    @Override
    public List<RechargeOrderHistoryInfo> getOutpatientRechargeOrdersList(String openid) throws HospitalException {

        /***单元测试**/
        GetOutpatientRechargeOrdersList getOutpatientRechargeOrdersList = new GetOutpatientRechargeOrdersList();
        Map<String, Object> params = getOutpatientRechargeOrdersList.getOutpatientRechargeOrdersListRequest(openid);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PO010201", params);

        /***单元测试**/
        List<RechargeOrderHistoryInfo> result = getOutpatientRechargeOrdersList.getOutpatientRechargeOrdersListResponse(dataList);
        return result;
        /***单元测试**/
    }

    /**
     * 取消门诊充值订单
     *
     * @param openId      支付宝USERID或微信OPENID
     * @param patientName 为哪个病人充值
     * @param patientId   病人ID
     * @param tradeno     医院订单号
     * @return
     * @throws HospitalException
     */
    @Override
    public String cancelOutpatientRechargeOrder(String openId, String patientName, String patientId, String tradeno) throws HospitalException {
        /***单元测试**/
        CancelOutpatientRechargeOrder cancelOutpatientRechargeOrder = new CancelOutpatientRechargeOrder();
        Map<String, Object> params = cancelOutpatientRechargeOrder.cancelOutpatientRechargeOrderRequest(openId, patientName, patientId, tradeno);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PO010301", params);
        /***单元测试**/
        String result = cancelOutpatientRechargeOrder.cancelOutpatientRechargeOrderResponse(dataList);
        return result;
        /***单元测试**/

    }


    /**
     * 门诊充值订单完成并到账
     *
     * @param rechargeOrderFinishInfo 用户订单信息
     * @return tradeno 订单号
     */
    @Override
    public String outpatientRechargeOrderFinish(RechargeOrderFinishInfo rechargeOrderFinishInfo)
            throws HospitalException {

        /***单元测试**/
        OutpatientRechargeOrderFinish outpatientRechargeOrderFinish = new OutpatientRechargeOrderFinish();
        Map<String, Object> params = outpatientRechargeOrderFinish.outpatientRechargeOrderFinishRequest(rechargeOrderFinishInfo);
        /***单元测试**/
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PO010401", params);

        /***单元测试**/
        String result = outpatientRechargeOrderFinish.outpatientRechargeOrderFinishResponse(dataList);
        return result;
        /***单元测试**/
    }

    /**
     * 创建住院充值订单
     *
     * @param inpatientNo
     * @param money
     * @param subject
     * @param patientName
     * @param parientIdCardNo
     * @return tradeno 订单号
     */
    @Override
    public String buildInhospitalRechargeOrder(String inpatientNo, String money, String subject, String patientName, String parientIdCardNo) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        Map<String, Object> params = new HashMap<>();
        params.put("inpatientno", inpatientNo);
        params.put("money", money);
        params.put("openid", userInfo.getAlipayUserId());
        params.put("patientidcardno", parientIdCardNo);
        params.put("patientname", patientName);
        params.put("subject", subject);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PI010101", params);

        /***单元测试**/
        BuildInhospitalRechargeOrder buildInhospitalRechargeOrder = new BuildInhospitalRechargeOrder();
        String result = buildInhospitalRechargeOrder.buildInhospitalRechargeOrderResponse(dataList);
        return result;
        /***单元测试**/
    }

    /**
     * 获得住院充值订单列表
     *
     * @param openid 用户标识
     * @return 住院充值订单列表
     */
    @Override
    public List<RechargeOrderHistoryInfo> getInhospitalRechargeOrdersList(String openid) throws HospitalException {
        /***单元测试**/
        GetInhospitalRechargeOrdersList getInhospitalRechargeOrdersList = new GetInhospitalRechargeOrdersList();
        Map<String, Object> params = getInhospitalRechargeOrdersList.getInhospitalRechargeOrdersListRequest(openid);
        /***单元测试**/

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PI010201", params);

        /***单元测试**/
        List<RechargeOrderHistoryInfo> results = getInhospitalRechargeOrdersList.getInhospitalRechargeOrdersListResponse(dataList);
        return results;
        /***单元测试**/
    }

    /**
     * 取消住院充值订单
     *
     * @param openId      支付宝USERID或微信OPENID
     * @param patientName 为哪个病人充值
     * @param inpatientNo 住院号码
     * @param tradeno     医院订单号
     * @return
     * @throws HospitalException
     */
    @Override
    public String cancelInhospitalRechargeOrder(String openId, String patientName, String inpatientNo, String tradeno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("openid", openId);
        params.put("patientname", patientName);
        params.put("inpatientno", inpatientNo);
        params.put("tradeno", tradeno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PI010301", params);

        if (dataList.size() > 0) {
            return dataList.get(0).get("tradeno");
        }

        return null;
    }

    /**
     * 住院充值订单完成并到账
     *
     * @param RechargeOrderFinishInfo 用户订单信息
     * @return 订单号
     */
    @Override
    public String inhospitalRechargeOrderFinish(RechargeOrderFinishInfo RechargeOrderFinishInfo) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("openid", RechargeOrderFinishInfo.getOpenid());
        params.put("patientname", RechargeOrderFinishInfo.getPatientname());
        params.put("inpatientno", RechargeOrderFinishInfo.getUserdata());
        params.put("tradeno", RechargeOrderFinishInfo.getTradeno());
        params.put("money", RechargeOrderFinishInfo.getMoney());
        params.put("paymentparameters", RechargeOrderFinishInfo.getPaymentparameters());
        params.put("paymenttradeno", RechargeOrderFinishInfo.getPaymenttradeno());

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("PI010401", params);

        if (dataList.size() > 0) {
            return dataList.get(0).get("tradeno");
        }

        return null;
    }

    /**
     * 住院病人信息
     *
     * @param idcardno 身份证号
     * @param name     病人姓名
     * @return 住院病人信息
     */
    @Override
    public InhospitalPatientInfo inhospitalPatientInfo(String idcardno, String name) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("idcardno", idcardno);
        params.put("name", name);
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("IP010101", params);

        if (dataList.size() > 0) {
            Map<String, String> dataMap = dataList.get(0);
            InhospitalPatientInfo result = new InhospitalPatientInfo();
            result.setZyh(dataMap.get("inpatientno"));// zyh 住院号
            result.setInpatientno(dataMap.get("inpatientno"));// zyhm 住院号码
            result.setPatientname(dataMap.get("patientname"));
//            result.setMzhm(dataMap.get("mzhm"));// mzhm 门诊号码
            result.setPatientidcardno(dataMap.get("patientidcardno"));
            result.setSex(dataMap.get("sex"));
            result.setBirthday(dataMap.get("birthday"));
            result.setAddress(dataMap.get("address"));
            result.setPhone(dataMap.get("phone"));
            result.setAdmitdate(dataMap.get("admitdate"));
            result.setDischargedate(dataMap.get("dischargedate"));
            try {
                result.setStayday(Integer.valueOf(dataMap.get("stayday")));
            } catch (Exception ex) {
                ex.printStackTrace();
            }
            result.setXzmc(dataMap.get("xzmc"));
            result.setEndemicarea(dataMap.get("endemicarea"));
            result.setBrch(dataMap.get("brch"));
            result.setDepartcode(dataMap.get("departcode"));
            result.setDepartname(dataMap.get("departname"));
            result.setYlhj(dataMap.get("ylhj"));
            result.setLwhj(dataMap.get("lwhj"));
            result.setZfje(dataMap.get("zfje"));
            result.setJkje(dataMap.get("jkje"));
            result.setZyzt(dataMap.get("zyzt"));
            result.setDoctorcode(dataMap.get("doctorcode"));
            result.setDoctorname(dataMap.get("doctorname"));

            return result;
        }

        return null;
    }

    /**
     * 住院费用列表
     *
     * @param inpatientno 住院号码
     * @return 住院费用信息
     */
    @Override
    public List<InHospitalOutlays> getInhospitalOutlaysList(String inpatientno, String costdate) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("inpatientno", inpatientno);
        // 从数据库获取数据
        List<Map<String, String>> dataList = process("IP020101", params);

        List<InHospitalOutlays> results = new ArrayList<>();

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            InHospitalOutlays info = new InHospitalOutlays();
            info.setCostCode(dataMap.get("costcode"));
            info.setCostName(dataMap.get("costname"));
            info.setTotalFee(dataMap.get("totalfee"));
            info.setPayFee(dataMap.get("payfee"));
            results.add(info);
        }

        return results;
    }

    /**
     * 门诊预约报到
     *
     * @param preengageseq 预约流水号
     * @return 门诊预约报到是否成功
     */
    @Override
    public boolean getoutpatientOrder(String preengageseq) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("openid", info.getAlipayUserId());
        parameters.put("preengageseq", preengageseq);
        List<Map<String, String>> values = process("PD010101", parameters);
        return values != null;
    }

    /**
     * 门诊预约排队列表
     *
     * @return list
     */
    @Override
    public List<OutpatientWaitingList> getOutpatientWaitingList()
            throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        String op = info.getAlipayUserId();
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("openid", op);
        List<Map<String, String>> values = process("PD010201", parameters);
        List<OutpatientWaitingList> list = new ArrayList<>();
        String queuestate = "";
        for (Map<String, String> map : values) {
            OutpatientWaitingList result = new OutpatientWaitingList();
            result.setYylsh(map.get("preengageseq"));
            result.setFzxh(map.get("preengageno"));
            result.setPdlsh(map.get("queueseq"));
            result.setPdhm(map.get("queueid"));
            result.setPdrq(map.get("queuetime"));
            result.setBcmc(map.get("shiftname"));
            result.setKsmc(map.get("departname"));
            result.setYsxm(map.get("doctorname"));
            result.setZsmc(map.get("departname"));//zsmc 科室名称
            result.setZswz(map.get("roompos"));
            result.setFjhm(map.get("roomno"));
            result.setDqjzh(map.get("queueid"));//dqjzh 当前排到第几个
            queuestate = map.get("queuestate");
            if ("-1".equals(queuestate)) {
                queuestate = "-1";
            } else if ("0".equals(queuestate)) {
                queuestate = "4";
            } else if ("1".equals(queuestate)) {
                queuestate = "5";
            } else if ("2".equals(queuestate)) {
                queuestate = "2";
            } else if ("3".equals(queuestate)) {
                queuestate = "3";
            }
            result.setPdzt(queuestate);
            list.add(result);
        }
        return list;
    }

    /**
     * 挂号退号
     *
     * @param thbs 挂号标识
     * @return state 状态
     */
    @Override
    public boolean cancelRegistration(String thbs) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("guahaoID", thbs);
        List<Map<String, String>> values = process("PD010301", parameters);
        return values != null;
    }


    /**
     * 门诊预约所有排队前十人员列表信息
     *
     * @param
     * @return list
     */
    @Override
    public List<OutpatientWaitingList> getOutpatientWaitingListBeforeTen() throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        List<Map<String, String>> values = process("PD010001", parameters);
        List<OutpatientWaitingList> list = new ArrayList<>();
        String queuestate = "";
        for (Map<String, String> map : values) {
            OutpatientWaitingList result = new OutpatientWaitingList();
            result.setYylsh(map.get("preengageseq"));
            result.setFzxh(map.get("preengageno"));
            result.setPdlsh(map.get("queueseq"));
            result.setPdhm(map.get("queueid"));
            result.setPdrq(map.get("queuetime"));
            result.setBcmc(map.get("shiftname"));
            result.setKsmc(map.get("departname"));
            result.setYsxm(map.get("doctorname"));
            result.setZsmc(map.get("roomname"));
            result.setZswz(map.get("roompos"));
            result.setFjhm(map.get("roomno"));
            result.setDqjzh(map.get("currentid"));
            queuestate = map.get("queuestate");
            if ("-1".equals(queuestate)) {
                queuestate = "-1";
            } else if ("0".equals(queuestate)) {
                queuestate = "4";
            } else if ("1".equals(queuestate)) {
                queuestate = "5";
            } else if ("2".equals(queuestate)) {
                queuestate = "2";
            } else if ("3".equals(queuestate)) {
                queuestate = "3";
            }
            result.setPdzt(queuestate);
            list.add(result);
        }
        return list;
    }
    public List<Map<String,String>> getLineBefore3() throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        List<Map<String, String>> values = process("PD020003", parameters);
       return values;

    }

    /**
     * 检查预约报到排队列表
     *
     * @param openid   用户标识
     * @param queueseq 排队流水号
     * @return list
     */
    @Override
    public List<OutpatientWaitingList> checkPrecontractReportedList(String openid, String queueseq)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("openid", openid);
        parameters.put("queueseq", queueseq);
        List<Map<String, String>> values = process("PD020101", parameters);
        List<OutpatientWaitingList> list = new ArrayList<>();
        String queuestate = "";
        for (Map<String, String> map : values) {
            OutpatientWaitingList result = new OutpatientWaitingList();
            result.setYylsh(map.get("preengageseq"));
            result.setFzxh(map.get("preengageno"));
            result.setPdlsh(map.get("queueseq"));
            result.setPdhm(map.get("queueid"));
            result.setPdrq(map.get("queuetime"));
            result.setBcmc(map.get("shiftname"));
            result.setKsmc(map.get("departname"));
            result.setYsxm(map.get("doctorname"));
            result.setZsmc(map.get("roomname"));
            result.setZswz(map.get("roompos"));
            result.setFjhm(map.get("roomno"));
            result.setDqjzh(map.get("currentid"));
            queuestate = map.get("queuestate");
            if ("-1".equals(queuestate)) {
                queuestate = "-1";
            } else if ("0".equals(queuestate)) {
                queuestate = "4";
            } else if ("1".equals(queuestate)) {
                queuestate = "5";
            } else if ("2".equals(queuestate)) {
                queuestate = "2";
            } else if ("3".equals(queuestate)) {
                queuestate = "3";
            }
            result.setPdzt(queuestate);
            list.add(result);
        }
        return list;
    }

    /**
     * 检查预约报到
     *
     * @param openid       用户标识
     * @param preengageseq 预约流水号
     * @return boolean
     */
    @Override
    public boolean checkPrecontractReported(String openid, String preengageseq) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("openid", openid);
        parameters.put("preengageseq", preengageseq);
        List<Map<String, String>> values = process("PD020201", parameters);
        return values != null;
    }

    /**
     * 检查预约所有排队前十人员列表信息
     *
     * @param
     * @return list
     */
    @Override
    public List<OutpatientWaitingList> checkOutpatientWaitingListBeforeTen() throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        List<Map<String, String>> values = process("PD020001", parameters);
        List<OutpatientWaitingList> list = new ArrayList<OutpatientWaitingList>();
        String queuestate = "";
        for (Map<String, String> map : values) {
            OutpatientWaitingList result = new OutpatientWaitingList();
            result.setYylsh(map.get("preengageseq"));
            result.setFzxh(map.get("preengageno"));
            result.setPdlsh(map.get("queueseq"));
            result.setPdhm(map.get("queueid"));
            result.setPdrq(map.get("queuetime"));
            result.setBcmc(map.get("shiftname"));
            result.setKsmc(map.get("departname"));
            result.setYsxm(map.get("doctorname"));
            result.setZsmc(map.get("roomname"));
            result.setZswz(map.get("roompos"));
            result.setFjhm(map.get("roomno"));
            result.setDqjzh(map.get("currentid"));
            queuestate = map.get("queuestate");
            if ("-1".equals(queuestate)) {
                queuestate = "-1";
            } else if ("0".equals(queuestate)) {
                queuestate = "4";
            } else if ("1".equals(queuestate)) {
                queuestate = "5";
            } else if ("2".equals(queuestate)) {
                queuestate = "2";
            } else if ("3".equals(queuestate)) {
                queuestate = "3";
            }
            result.setPdzt(queuestate);
            list.add(result);
        }
        return list;
    }

    /**
     * 剩余床位查询 - 查询基本功能
     *
     * @param
     * @return bqmc     病区名称
     * sycw     剩余床位
     */
    @Override
    public List<BedQueryInfo> querySurplusBed() throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        List<Map<String, String>> values = process("Q008", parameters);

        /***单元测试**/
        QuerySurplusBed querySurplusBed = new QuerySurplusBed();
        return querySurplusBed.querySurplusBedResponse(values);
        /***单元测试**/
    }

    /**
     * 药品价格-无查询条件、分页查询 - 查询基本功能
     *
     * @param pageno  访问页码
     * @param pagerow 每页显示行数
     * @return 药品价格
     */
    @Override
    public List<DrugPriceInfo> queryDrugPrice(String pageno, String pagerow) throws HospitalException {
        /***单元测试**/
        QueryDrugPrice queryDrugPrice = new QueryDrugPrice();
        Map<String, Object> parameters = queryDrugPrice.queryDrugPriceRequest(pageno, pagerow);
        /***单元测试**/
        List<Map<String, String>> values = process("Q001", parameters);

        /***单元测试**/
        return queryDrugPrice.queryDrugPriceResponse(values);
        /***单元测试**/
    }

    /**
     * 药品价格-按拼音码或名称查询 - 查询基本功能
     *
     * @param pydm 拼音代码
     */
    @Override
    public List<DrugPriceInfo> queryDrugPriceByPy(String pydm) throws HospitalException {
        /***单元测试**/
        QueryDrugPriceByPy queryDrugPriceByPy = new QueryDrugPriceByPy();
        Map<String, Object> parameters = queryDrugPriceByPy.qqueryDrugPriceByPyRequest(pydm);
        /***单元测试**/
        List<Map<String, String>> values = process("Q002", parameters);

        /***单元测试**/
        return queryDrugPriceByPy.queryDrugPriceByPyResponse(values);
        /***单元测试**/
    }

    /**
     * 收费项目-无查询条件，分页 - 查询基本功能
     *
     * @param pageno  访问页码
     * @param pagerow 每页显示行数
     */
    @Override
    public List<FeeItemsInfo> queryFeeItems(String pageno, String pagerow) throws HospitalException {

        /***单元测试**/
        QueryFeeItems queryFeeItems = new QueryFeeItems();
        Map<String, Object> parameters = queryFeeItems.queryFeeItemsRequest(pageno, pagerow);
        /***单元测试**/
        List<Map<String, String>> values = process("Q003", parameters);

        /***单元测试**/
        return queryFeeItems.queryFeeItemsResponse(values);
        /***单元测试**/
    }


    /**
     * 收费项目-按拼音码或名称查询 - 查询基本功能
     *
     * @param pydm 拼音代码
     */
    @Override
    public List<FeeItemsInfo> queryFeeItemsByPy(String pydm) throws HospitalException {

        /***单元测试**/
        QueryFeeItemsByPy queryFeeItemsByPy = new QueryFeeItemsByPy();
        Map<String, Object> parameters = queryFeeItemsByPy.queryFeeItemsByPyRequest(pydm);
        /***单元测试**/
        List<Map<String, String>> values = process("Q004", parameters);

        /***单元测试**/
        return queryFeeItemsByPy.queryFeeItemsByPyResponse(values);
        /***单元测试**/
    }

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
    @Override
    public List<VisitInfo> InformVisitInfo() throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        List<Map<String, String>> values = process("NF010101", parameters);
        List<VisitInfo> result = new ArrayList<VisitInfo>();
        for (Map<String, String> map : values) {
            VisitInfo visitInfo = new VisitInfo();
            visitInfo.setUserid(map.get("userid"));
            visitInfo.setBrxm(map.get("brxm"));
            visitInfo.setYsxm(map.get("ysxm"));
            visitInfo.setJzdz(map.get("jzdz"));
            visitInfo.setJzsj(map.get("jzsj"));
            visitInfo.setJzrq(DateUtil.dateFormat(map.get("jzrq"), "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd"));
            result.add(visitInfo);
        }
        return result;
    }

    /**
     * 核对未到账的充值缴费信息
     *
     * @param
     * @return out_trade_no 订单支付时传入的商户订单号,和支付宝交易号不能同时为空。trade_no,out_trade_no如果同时存在优先取trade_no
     * trade_no 支付宝交易号，和商户订单号不能同时为空
     */
    @Override
    public List<RechargeOrderFinishInfo> CheckNotAccountInfo() throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        List<Map<String, String>> values = process("NF010201", parameters);
        List<RechargeOrderFinishInfo> result = new ArrayList<>();
        for (Map<String, String> map : values) {
            RechargeOrderFinishInfo rechargeOrderFinishInfo = new RechargeOrderFinishInfo();
            rechargeOrderFinishInfo.setPaymenttradeno(map.get("out_trade_no"));
            rechargeOrderFinishInfo.setTradeno(map.get("trade_no"));
            result.add(rechargeOrderFinishInfo);
        }
        return result;
    }

    /**
     * 门诊就诊病历列表
     *
     * @param openid 用户标识
     * @return list
     */
    @Override
    public List<OutpatientVisitRecord> getOutpatientCaseList(String openid, String brid) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("openid", openid);
        parameters.put("patientid", brid);
        List<Map<String, String>> values = process("BL010101", parameters);
        List<OutpatientVisitRecord> list = new ArrayList<>();
        for (Map<String, String> map : values) {
            OutpatientVisitRecord result = new OutpatientVisitRecord();
            result.setJzxh(map.get("jzxh"));
            result.setJzrq(map.get("jzrq"));
            result.setKsmc(map.get("ksmc"));
            result.setYsxm(map.get("ysxm"));
            result.setZdxx(map.get("zdxx"));
            list.add(result);
        }
        return list;
    }

    /**
     * 电子病历内容
     *
     * @param jzxh 就诊序号
     * @return 电子病历内容
     */
    @Override
    public OutpatientVisitInfo getOutpatientVisitInfoDetail(String jzxh) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("jzxh", jzxh);
        List<Map<String, String>> values = process("BL010201", parameters);
        OutpatientVisitInfo info = new OutpatientVisitInfo();
        if (values.size() > 0) {
            info.setMzzs(values.get(0).get("mzzs"));
            info.setXbs(values.get(0).get("xbs"));
            info.setJws(values.get(0).get("jws"));
            info.setGrs(values.get(0).get("grs"));
            info.setGms(values.get(0).get("gms"));
            info.setHys(values.get(0).get("hys"));
            info.setJzs(values.get(0).get("jzs"));
            info.setTgjc(values.get(0).get("tgjc"));
            info.setFzjc(values.get(0).get("fzjc"));
            info.setClyj(values.get(0).get("clyj"));
        }
        return info;
    }

    /**
     * 门诊指引单
     *
     * @param jzxh 就诊序号
     * @return 门诊指引单内容
     */
    @Override
    public OutpatientVisitGuideBills getOutpatientVisitGuideBillsDetail(String jzxh) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("jzxh", jzxh);
        List<Map<String, String>> values = process("BL010301", parameters);
        if (values.size() == 0) {
            return null;
        }
        OutpatientVisitGuideBills info = new OutpatientVisitGuideBills();
        if (values.size() > 0) {
            info.setCfxh(values.get(0).get("cfxh"));
            info.setFphm(values.get(0).get("fphm"));
            info.setKfrq(values.get(0).get("kfrq"));
            info.setKsmc(values.get(0).get("ksmc"));
            info.setZynr(values.get(0).get("zynr"));
            info.setZywz(values.get(0).get("zywz"));
            info.setYsxm(values.get(0).get("ysxm"));
            info.setZjje(values.get(0).get("zjje"));
        }
        return info;
    }

    /**
     * 病人药品处方信息列表
     *
     * @param openid 用户标识
     * @param brid   病人ID
     * @return list
     */
    @Override
    public List<MedicineUseInfoList> getMedicineUseInfoListDetail(String openid, String brid) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("openid", openid);
        parameters.put("patientid", brid);
        List<Map<String, String>> values = process("FY010101", parameters);
        List<MedicineUseInfoList> list = new ArrayList<MedicineUseInfoList>();
        for (Map<String, String> map : values) {
            MedicineUseInfoList result = new MedicineUseInfoList();
            result.setCflsh(map.get("cflsh"));
            result.setKsbm(map.get("ksbm"));
            result.setKsmc(map.get("ksmc"));
            result.setYsbm(map.get("ysbm"));
            result.setYsxm(map.get("ysxm"));
            result.setBrid(map.get("brid"));
            result.setKfrq(map.get("kfrq"));
            result.setSfrq(map.get("sfrq"));
            result.setZjje(map.get("zjje"));
            result.setLxmc(map.get("lxmc"));
            result.setCfje(map.get("cfje"));
            result.setTzybz(map.get("tzybz"));
            result.setZfzt(map.get("zfzt"));
            list.add(result);
        }
        return list;
    }

    /**
     * 病人药品服用信息
     *
     * @param cflsh 处方流水号
     * @return list
     */
    @Override
    public List<MedicineUseInfo> getMedicineUseInfoDetail(String cflsh) throws HospitalException {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("cflsh", cflsh);
        List<Map<String, String>> values = process("FY010201", parameters);
        List<MedicineUseInfo> list = new ArrayList<MedicineUseInfo>();
        for (Map<String, String> map : values) {
            MedicineUseInfo result = new MedicineUseInfo();
            result.setYpmc(map.get("ypmc"));
            result.setYpgg(map.get("ypgg"));
            result.setYpsl(map.get("ypsl"));
            result.setYcyl(map.get("ycyl"));
            result.setYsjy(map.get("ysjy"));
            result.setSync(map.get("sync"));
            result.setGytj(map.get("gytj"));
            result.setTzybz(map.get("tzybz"));
            list.add(result);
        }
        return list;
    }

    /***
     * --------------------------------下面是云医院的接口---------------------
     * */
    /**
     * 根据openId,查询是否是复诊病人
     * @param patientId
     * @return
     * @throws HospitalException
     */
    public  boolean getIsRepeat(String patientId) throws HospitalException
    {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("brid", patientId);
        List<Map<String, String>> process = process("CA040101", parameters);
        if(process==null||process.isEmpty())
        {
            return false;
        }
        Map<String, String>  rtnMap=process.get(0);//第1个
        String isRepeart=rtnMap.get("isrepeat");

        System.out.println("===============getIsRepeat============isRepeart:"+isRepeart);
        return "1".equals(isRepeart)?true:false;

    }
    /**
     * 根据patientId查询处方列表
     * @param patientId
     * @return
     * @throws HospitalException
     */
    public  List<Map<String, String>> getPrescriptionList(String patientId) throws HospitalException
    {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("patientid", patientId);
        List<Map<String, String>> values = process("CA040101", parameters);

        return values;

    }
    /**
     * 根据lsCode查询处方详情
     * @param lsCode
     * @return
     * @throws HospitalException
     */
    public  List<Map<String, String>> getPrescriptionDetail(String lsCode) throws HospitalException
    {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("lscode", lsCode);
        List<Map<String, String>> values = process("CA040101", parameters);

        return values;

    }
    /**
     * 根据openId获取联系人列表
     * @param openId
     * @return
     * @throws HospitalException
     */
    public  List<Map<String, String>> getContactPersonList(String openId) throws HospitalException
    {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openId);
        List<Map<String, String>> values = process("FY030101", parameters);

        return values;
    }
    /**
     * 通用方法
     * @param hCode
     * @param parameters
     * @return
     * @throws HospitalException
     */
    @Override
    public List<Map<String, String>> commonService(String hCode,Map<String, Object> parameters) throws HospitalException {
        return process(hCode, parameters);
    }

    /**
     * 根据医生代码和病人代码，获取云医院预约日期为当天的预约及录
     * @param doctorCode
     * @param patientCode
     */
    @Override
    public Map<String, String> getAppoint(String doctorCode, String patientCode) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("doctorcode", doctorCode);
        parameters.put("patientcode", patientCode);
        List<Map<String, String>> values = process("PI010001", parameters);
        return values.get(0);
    }

    @Override
    public Map<String, String> getPatientInfo(String patientCode) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("brid", patientCode);
        List<Map<String, String>> values = process("UI020202", parameters);
        return values.get(0);
    }

    public List<DoctorInfo> getDoctorAll(String namepy,String pageindex,String pagesize,String hasKey) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        /***单元测试**/
        GetDoctorInfoAll getDoctorInfoAll = new GetDoctorInfoAll();
        /***单元测试**/
        parameters=getDoctorInfoAll.getDoctorInfoByPyRequest(namepy, pageindex, pagesize,hasKey);
        List<Map<String, String>> process = process("DO010104", parameters);

        /***单元测试**/
        List<DoctorInfo> dcinfolist = getDoctorInfoAll.getDoctorInfoByPyResponse(process);
        return dcinfolist;
    }
}
