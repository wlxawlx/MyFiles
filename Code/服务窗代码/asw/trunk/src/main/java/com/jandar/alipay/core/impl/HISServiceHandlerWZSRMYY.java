package com.jandar.alipay.core.impl;

import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.FunctionUnsupportException;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.*;
//import com.jandar.alipay.core.struct.Inspection;
import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.alipay.core.struct.wzsrmyy.*;
import com.jandar.alipay.dao.UserInfoService;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.AsteriskUtil;
import com.jandar.cloud.hospital.bean.*;
import com.jandar.config.ConfigHandler;
import com.jandar.handle.protocol.impl.wzsrmyy.CourseRegistrationListProtocol;
import com.jandar.util.StringUtil;
import net.sf.json.JSONObject;
import org.apache.commons.collections.map.LinkedMap;
import org.apache.commons.lang.StringUtils;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.JDOMException;
import org.jdom.input.SAXBuilder;
import org.xml.sax.InputSource;

import java.io.IOException;
import java.io.StringReader;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

/**
 * Created by zzw on 16/2/25. 温州市人民医院,温州市第三人民医院版本
 */
public class HISServiceHandlerWZSRMYY extends BaseHISService {
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
     * @throws HospitalException
     */
    @Override
    public boolean paymentArrivalNotify(RechargeOrderType orderType, String openId, String paymentObject,
                                        String hospitalTradeNo, String paymentTradeNo, String money, String userData, String paymentParameters)
            throws HospitalException {

        if (orderType == null) {
            /* 住院充值 */
            if (hospitalTradeNo.startsWith("Z")) {
                orderType = RechargeOrderType.InHospitalOrder;
            }
            /* 门诊充值 */
            else if (hospitalTradeNo.startsWith("M")) {
                orderType = RechargeOrderType.OutpatientOrder;
            }
            /* 技能中心 */
            else if (hospitalTradeNo.startsWith("Q")) {
                orderType = RechargeOrderType.OtherOrder;
            }
        }

        Map<String, Object> map = new HashMap<>();
        map.put("orderno", hospitalTradeNo);
        map.put("openid", openId);
        map.put("wxorderno", paymentTradeNo);
        map.put("orderm", money);

        String opCode = null;
        if (orderType == RechargeOrderType.InHospitalOrder) {
            opCode = "F010";
        } else if (orderType == RechargeOrderType.OutpatientOrder) {
            opCode = "F005";
        } else if (orderType == RechargeOrderType.OtherOrder) {
            opCode = "F015";
        } else {
            logger.error("未知的订单分类");
            return false;
        }

        List<Map<String, String>> values = process(opCode, map);
        System.out.println("通知医院已经到账返回的信息:" + values);
        if (values.size() < 1 || !"1".equals(values.get(0).get("state"))) {
            return false;
        }
        return true;
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
    @Override
    public void paymentPlatformNotifyMessage(RechargeOrderType orderType, String openId, String paymentObject,
                                             String money, String hospitalTradeNo, String gmt_payment) {
        if (orderType == null) {
            /* 住院充值 */
            if (hospitalTradeNo.startsWith("Z")) {
                orderType = RechargeOrderType.InHospitalOrder;
            }
            /* 门诊充值 */
            else if (hospitalTradeNo.startsWith("M")) {
                orderType = RechargeOrderType.OutpatientOrder;
            }
            /* 技能中心 */
            else if (hospitalTradeNo.startsWith("Q")) {
                orderType = RechargeOrderType.OtherOrder;
            }
        }

        if (orderType == RechargeOrderType.OtherOrder) {
            String title = "技能中心报名成功";
            StringBuilder message = new StringBuilder();
            String url = "http://" + ConfigHandler.getSelfServiceHost() + "/";
            message.append("报名信息:\n");
            message.append(ConfigHandler.getHospitalName() + "\n");
            message.append("姓名: " + paymentObject + "\n");
            try {
                List<Map<String, String>> process = new CourseRegistrationListProtocol().process(hospitalTradeNo,
                        openId);
                if (process.size() >= 1) {
                    Map<String, String> object = process.get(0);
                    message.append("课程: " + object.get("fymc") + "\n");
                    message.append("人数: " + object.get("fysl") + "人\n");
                    message.append("备注: " + object.get("usertext") + "\n");
                }
            } catch (Exception ex) {
                logger.warn("技能中心报名成功消息组织出错:" + ex.getMessage());
            }
            message.append("金额: " + money + "元\n");
            message.append("时间: " + gmt_payment);
            url += "trainItemList.html";
            AlipayMsgSendUtil.sendSingleImgTextMsg(openId, title, message.toString(), url, "详情");
        } else {
            super.paymentPlatformNotifyMessage(orderType, openId, paymentObject, money, hospitalTradeNo, gmt_payment);
        }
    }

    /**
     * 用户注册
     *
     * @param info 用户信息,结构中的 就诊卡号 与 病人ID 可为空
     * @return 注册是否成功
     */
    @Override
    public boolean userRegister(HISUserInfo info) throws HospitalException {
        if (info == null) {
            return false;
        }

        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yhxm", info.getName());
        parameters.put("dlmm", "000000");
        parameters.put("lxdh", info.getPhone());
        parameters.put("sfzh", info.getIdcardno());
        parameters.put("lxdz", info.getAddress() == null ? "" : info.getAddress());
        parameters.put("aliuserid", info.getOpenid());
        parameters.put("headurl", info.getHeadurl());
        List<Map<String, String>> process = process("B011", parameters);
        return process != null;
    }

    /**
     * 用户注册及绑卡操作
     *
     * @param info 用户信息,结构中的 就诊卡号 可为空 病人ID 不能为空
     * @return 注册及绑卡是否成功
     */
    @Override
    public boolean userRegisterAndBindCard(HISUserInfo info) throws HospitalException {
        //三院不支持注册时候绑卡,使用
        return userRegister(info);
    }

    /**
     * 用户登录,获得用户信息
     *
     * @param openid   用户标识
     * @param usertype 用户类型（可以为空）
     * @return 用户信息
     */
    @Override
    public HISUserInfo getUserInfo(String openid, PlatformType usertype) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("ljhm", openid);
        parameters.put("ljfs", usertype == PlatformType.Alipay ? "2" : "1");
        List<Map<String, String>> process = null;
        try {
            process = process("B012", parameters);
        } catch (HospitalException e) {
            if (e.getMessage().contains("该登陆信息未注册") ||
                    e.getMessage().contains("未注册")) {
                throw new HospitalException(e.getMessage(), HospitalException.UNARCHIV);
            } else {
                throw e;
            }
        }
        Map<String, String> map = process.get(0);
        HISUserInfo info = new HISUserInfo();
        info.setUserId(map.get("yhid"));
        info.setName(map.get("yhxm"));
        info.setPhone(map.get("lxdh"));
        info.setIdcardno(map.get("sfzh"));
        info.setAddress(map.get("lxdz"));
        info.setHeadurl(map.get("headurl"));
        info.setPatientid(map.get("brid"));
        info.setOpenid(map.get("aliuserid"));
        info.setBkzt(map.get("bkzt"));
        return info;
    }

    /**
     * 用户信息修改
     *
     * @param openid(userid) 用户标识
     * @param name           姓名
     * @param phone          手机号码
     * @param idcardno       身份证号
     * @return openid
     */
    @Override
    public boolean alterUserInfo(String openid, String name, String phone, String idcardno) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userinfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userinfo.getYhid());
        parameters.put("yhxm", name);
        parameters.put("sfzh", idcardno);
        parameters.put("lxdh", phone);//因为后台数据库是主键所以改不了
        List<Map<String, String>> process = process("B009", parameters);
        return process != null;
    }

    ;

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
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userInfo.getYhid());
        parameters.put("cyxm", name);
        parameters.put("lxdh", phone);
        parameters.put("sfzh", idcardno);
        List<Map<String, String>> process = process("B005", parameters);
        ContactPeopleInfo info = new ContactPeopleInfo();
        Map<String, String> result = process.get(0);
        if (process.size() > 0) {
            // info.setLabel(result.get(""));
            info.setName(name);
            info.setLinkmanid(result.get("cyxh"));
        }
        return info;
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
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userInfo.getYhid());

        List<ContactPeopleInfo> list = new ArrayList<ContactPeopleInfo>();
        List<Map<String, String>> values = process("B007", parameters);
        for (Map<String, String> value : values) {
            String cyxh = value.get("cyxh");
            if ((linkmanid == null || "".equals(linkmanid)) || linkmanid.equals(cyxh)) {
                ContactPeopleInfo data = new ContactPeopleInfo();
                data.setLinkmanid(cyxh);
                // data.setLabel(value.get(""));
                data.setName(AsteriskUtil.formatName(value.get("cyxm")));
                data.setPhone(AsteriskUtil.formatPhone(value.get("lxdh")));
                data.setIdcardno(AsteriskUtil.formatId(value.get("sfzh")));
                Map<String, Object> required = new HashMap<String, Object>();
                required.put("sfzh", value.get("sfzh"));
                try {
                    process("G007", required);
                    data.setInpatentflag("1");
                } catch (Exception e) {
                }
                data.setBindcardfalg(value.get("bkzt"));
                data.setPatientid(value.get("brid"));
                list.add(data);
            }
            if (linkmanid != null && linkmanid.equals(cyxh)) {
                break;
            }
        }

        return list;
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
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userInfo.getYhid());
        parameters.put("cyxh", linkmanid);
        List<Map<String, String>> process = process("B006", parameters);
        ContactPeopleInfo info = new ContactPeopleInfo();
        info.setLinkmanid(linkmanid);
        return info;
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
        Map<String, Object> parameters = new HashMap<String, Object>();

        parameters.put("sfzh", idcardno);
        List<Map<String, String>> process = process("C003", parameters);
        List<OutPatientCardInfo> list = new ArrayList<OutPatientCardInfo>();
        for (Map<String, String> map : process) {
            OutPatientCardInfo opinfo = new OutPatientCardInfo();
            opinfo.setCardtype(map.get("ckbz"));
            opinfo.setCardname(map.get("cklx"));
            opinfo.setCardno(map.get("ckkh"));
            opinfo.setIdcardno(map.get("sfzh"));
            opinfo.setPatientid(map.get("brid"));
            opinfo.setPatientname(map.get("brxm"));
            opinfo.setBirthday(map.get("csny"));
            opinfo.setPhone(map.get("lxdh"));
            opinfo.setBalance(map.get("zhye"));
            opinfo.setCost(map.get("ljfy"));
            list.add(opinfo);
        }
        return list;

    }

    /**
     * 根据病人ID获得门诊卡信息
     *
     * @param patientid 病人id
     * @return 门诊卡信息_病人
     */
    @Override
    public OutPatientCardInfo getOutpatientCardInfoByPatientId(String patientid) throws HospitalException {
        throw new FunctionUnsupportException("该功能不支持");
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
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userInfo.getYhid());
        parameters.put("ckkh", cardno);
        parameters.put("brid", patientid);
        List<Map<String, String>> process = process("C001", parameters);
        OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            //Map<String, String> re = process.get(0);
            // ocinfo.setCardtype(re.get("cardtype"));
            // ocinfo.setCardname(re.get("cardname"));
            ocinfo.setCardno(cardno);
        }
        return ocinfo;
    }

    /**
     * 本人门诊卡解绑
     *
     * @param openid 用户标识
     * @return 解绑是否成功
     */
    @Override
    public boolean unbindOutpatientCard(String openid) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userInfo.getYhid());
        List<Map<String, String>> process = process("C002", parameters);
        //OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        return process != null;
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
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String yhid = userInfo.getYhid();
        if (StringUtil.isBlank(yhid)) {
            throw new HospitalException("用户id为空");
        }
        parameters.put("yhid", userInfo.getYhid());
        parameters.put("cyxh", linkmanid);
        parameters.put("ckkh", cardno);
        parameters.put("brid", patientid);
        List<Map<String, String>> process = process("C004", parameters);
        OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            //Map<String, String> result = process.get(0);
            // ocinfo.setCardtype(result.get("cardtype"));
            // ocinfo.setCardname(result.get("cardname"));
            // ocinfo.setCardno(result.get("cardno"));
            ocinfo.setPatientid(patientid);
        }
        return ocinfo;
    }

    /**
     * 联系人门诊卡解绑
     *
     * @param openid    用户标识
     * @param linkmanid 常用联系人序号
     * @return cardno 就诊卡号
     */
    @Override
    public OutPatientCardInfo unbindContactOutpatientCard(String openid, String linkmanid) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        parameters.put("yhid", userInfo.getYhid());
        parameters.put("cyxh", linkmanid);
        List<Map<String, String>> process = process("C005", parameters);
        OutPatientCardInfo cpinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            Map<String, String> result = process.get(0);
            cpinfo.setCardno(result.get("cardno"));
        }
        return cpinfo;
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
    public List<DoctorInfo> getDoctorInfoByName(String name, String pageindex, String pagesize)
            throws HospitalException {
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
    public List<DoctorInfo> getDoctorInfoByPy(String namepy, String pageindex, String pagesize)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("pydm", namepy);
        parameters.put("xssl", pagesize); // 页大小
        parameters.put("dqym", pageindex);// 页号
        List<Map<String, String>> process = process("D013", parameters);
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        for (Map<String, String> doct : process) {
            DoctorInfo dcinfo = new DoctorInfo();
            dcinfo.setCode(doct.get("ysbm"));
            dcinfo.setName(doct.get("ysxm"));
            dcinfo.setSex(doct.get("ysxb"));
            // dcinfo.setPictureurl(result.get("pictureurl"));
            dcinfo.setLevel(doct.get("zcmc"));
            // dcinfo.setRecommend(result.get("recommend"));
            // dcinfo.setAdept(result.get("adept"));
            dcinfo.setDepartcode(doct.get("ksbm"));
            dcinfo.setDepartname(doct.get("ksmc"));
            dcinfolist.add(dcinfo);
        }
        return dcinfolist;

    }

    /**
     * 获得医生信息列表_按医生代码查
     *
     * @param code 医生代码
     * @return list
     */
    @Override
    public List<DoctorInfo> getDoctorInfoByCode(String code) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("ysbm", code);
        List<Map<String, String>> process = process("D014", parameters);
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        if (process.size() > 0) {
            Map<String, String> result = process.get(0);
            DoctorInfo dcinfo = new DoctorInfo();
            // dcinfo.setCode(result.get("code"));
            dcinfo.setName(result.get("ysxm"));
            dcinfo.setSex(result.get("ysxb"));
            // dcinfo.setPictureurl(result.get("pictureurl"));
            dcinfo.setLevel(result.get("zcmc"));
            dcinfo.setRecommend(result.get("ysjs"));
            dcinfo.setAdept(result.get("scjb"));
            dcinfo.setDepartcode(result.get("ksbm"));
            dcinfo.setDepartname(result.get("ksmc"));
            dcinfolist.add(dcinfo);
        }
        return dcinfolist;
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
        parameters.put("dqym", pageindex);
        parameters.put("xssl", pagesize);
        List<Map<String, String>> process = process("D015", parameters);
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        for (Map<String, String> map : process) {
            DoctorInfo docinfo = new DoctorInfo();
            docinfo.setCode(map.get("ysbm"));
            docinfo.setName(map.get("ysxm"));
            // docinfo.setPictureurl(map.get("pictureurl"));
            docinfo.setStopshift(map.get("bcmc"));// bcmc
            docinfo.setLevel(map.get("zcmc"));
            docinfo.setDepartcode(map.get("ksbm"));
            docinfo.setDepartname(map.get("ksmc"));
            docinfo.setStopdate(map.get("tzrq"));
            dcinfolist.add(docinfo);
        }
        return dcinfolist;
    }

    /**
     * 获得预约科室列表
     *
     * @param departcode 一级科室代码
     * @return 科室列表
     */
    @Override
    public List<DepartmentInfo> getDepartmentsListForOrder(String departcode) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yyks", "0");
        List<Map<String, String>> process = process("D001", parameters);
        List<DepartmentInfo> deinfolist = new ArrayList<DepartmentInfo>();
        for (Map<String, String> map : process) {
            DepartmentInfo deinfo = new DepartmentInfo();
            deinfo.setDepartcode(map.get("deptid"));
            deinfo.setDepartname(map.get("ksfl"));
            deinfo.setSecondcode(map.get("ksbm"));
            deinfo.setSecondname(map.get("ksmc"));
            deinfo.setDescribe(map.get("ksms"));
            deinfolist.add(deinfo);
        }
        return deinfolist;
    }

    /**
     * 获得科室排班信息（某一天科室下还有所有医生还有多少号源）
     *
     * @param departcode 二级科室代码
     * @return list
     */
    @Override
    public List<SchedulingInfo> getDepartmentSchedulForOrder(String departcode) throws HospitalException {
        // Map<String, Object> parameters = new HashMap<String, Object>();
        // parameters.put("departcode", departcode);
        // List<Map<String, String>> process = process("OR010201", parameters);
        // List<SchedulingInfo> scinfo = new ArrayList<SchedulingInfo>();
        // for (Map<String, String> map : process) {
        // SchedulingInfo sc = new SchedulingInfo();
        // sc.setScheduledate(map.get("scheduledate"));
        // sc.setRemain(map.get("remain"));
        // sc.setTotal(map.get("total"));
        // scinfo.add(sc);
        // }
        // return scinfo;
        throw new FunctionUnsupportException("该功能不支持");
    }

    /**
     * 获得预约医生列表
     *
     * @param departcode   二级科室代码
     * @param scheduledate 排班日期
     * @return 医生信息list
     */
    @Override
    public List<DoctorInfo> getDoctorsListForOrder(String departcode, String scheduledate) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("deptid", departcode);
        List<Map<String, String>> process = process("D002", parameters);
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        for (Map<String, String> map : process) {
            DoctorInfo dc = new DoctorInfo();
            dc.setCode(map.get("ysbm"));
            dc.setName(map.get("ysxm"));
            // dc.setPictureurl(map.get("pictureurl"));
            dc.setLevel(map.get("ysjd"));
            // dc.setRecommend(map.get("recommend"));
            // dc.setAdept(map.get("adept"));
            dc.setReserve(map.get("kyrs"));
            // dc.setScheduledates(map.get("scheduledates"));
            dcinfolist.add(dc);
        }
        return dcinfolist;
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
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("ysbm", doctorcode);
        parameters.put("departcode", departcode);
        parameters.put("scheduledate", scheduledate);
        List<Map<String, String>> process = process("D003", parameters);
        List<DoctorScheduleInfo> dslist = new ArrayList<DoctorScheduleInfo>();
        for (Map<String, String> map : process) {
            DoctorScheduleInfo ds = new DoctorScheduleInfo();
            ds.setScheduleseq(map.get("pblsh"));
            ds.setDepartcode(map.get("ksbm"));
            ds.setDepartname(map.get("ksmc"));
            ds.setDoctorcode(map.get("ysbm"));
            ds.setDoctorname(map.get("ysxm"));
            ds.setSpecial(map.get("txbz"));
            ds.setRemain(map.get("kyrs"));
            ds.setTotal(map.get("yyzs"));
            ds.setAddress(map.get("jzdd"));
            ds.setScheduledate(map.get("pbrq"));
            ds.setShiftcode(map.get("bcbm"));
            ds.setShiftname(map.get("bcmc"));
            ds.setFee(map.get("ghf"));

            dslist.add(ds);
        }
        return dslist;
    }

    /**
     * 获得门诊预约号源
     *
     * @param doctorcode
     * @param scheduleseq
     * @return 门诊号源列表
     */
    @Override
    public List<OutpatientOrderNumber> getOutpatientOrderNumbers(String doctorcode, String scheduleseq, String shiftcode)
            throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("pblsh", scheduleseq);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("D004", params);

        List<OutpatientOrderNumber> results = new ArrayList<>();

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            OutpatientOrderNumber number = new OutpatientOrderNumber();
            number.setOrderno(dataMap.get("fzxh"));
            number.setOrderseq(dataMap.get("hylsh"));
            number.setOrdertime(dataMap.get("yysj"));

            results.add(number);
        }

        return results;
    }

    /**
     * 门诊预约
     *
     * @param outpatientOrderRequest 门诊预约请求信息
     * @return 门诊预约返回信息
     */
    @Override
    public OutpatientOrderReponse outpatientOrder(OutpatientOrderRequest outpatientOrderRequest)
            throws HospitalException {
        throw new FunctionUnsupportException("该功能不支持");
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
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        Map<String, Object> required = new HashMap<String, Object>();
        required.put("yhid", userInfo.getYhid());
        required.put("hylsh", outpatientOrderRequest.getOrderseq());
        required.put("brid", outpatientOrderRequest.getPatientid());
        required.put("brxm", outpatientOrderRequest.getPatientname());
        required.put("lxdh", outpatientOrderRequest.getPatientphone());
        required.put("sfzh", outpatientOrderRequest.getPatientidcardno());
        required.put("lxdz", outpatientOrderRequest.getPatientaddress());

        List<Map<String, String>> values = process("D005", required);
        return values.get(0).get("yymsg");
    }

    @Override
    public Map<String, String> outpatientOrderResult(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException {
        return null;
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
        params.put("yhid", openid);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("D006", params);

        List<OutpatientOrderHistory> results = new ArrayList<>();

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            OutpatientOrderHistory history = new OutpatientOrderHistory();
            history.setPreengageseq(dataMap.get("yylsh"));
            history.setDoctorname(dataMap.get("ysxm"));
            history.setDepartname(dataMap.get("ksmc"));
            history.setPatientname(dataMap.get("brxm"));
            history.setPreengageno(dataMap.get("fzxh"));
            history.setPreengagedate(dataMap.get("yysj"));
            history.setPreengagestate(dataMap.get("yyzt"));
            history.setDepartcode(dataMap.get("yylb")); // 在就诊地点中存储获取到的预约类别
            history.setDoctorcode(dataMap.get("bdzt")); // 报道状态存哪呢？
            history.setFee(dataMap.get("ghf"));
            history.setPatientphone(dataMap.get("lxdh"));
            history.setPlace(dataMap.get("jzdd"));

            results.add(history);
        }

        return results;
    }

    /**
     * 云医院预约记录，订单记录
     * @param openid 用户标识
     * @return
     * @throws HospitalException
     */
    @Override
    public List<Map<String, String>> cloudOrderList(String openid,String status) throws HospitalException {
        return null;
    }

    @Override
    public Map<String, String> cloudOrderInfo(String preengageseq) throws HospitalException {
        return null;
    }


    @Override
    public Map<String, String> updateCloudOrderStatus(String preengageseq) throws HospitalException {
        return null;
    }

    /**
     * 取消门诊预约 -- 这是无效的
     *
     * @param openid
     * @param preengageseq
     * @return 预约信息列表
     */
    @Override
    public List<OutpatientOrder> cancelOutpatientOrder(String openid, String preengageseq) throws HospitalException {
        return null;
    }

    /**
     * 取消门诊预约,只返回通知消息
     *
     * @param openid
     * @param preengageseq
     * @return 返回门诊预约的通知消息, 非详细信息
     * @throws HospitalException
     */
    public String cancelOutpatientOrderResultMessage(String openid, String preengageseq) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("yylsh", preengageseq);

        // 从数据库获取数据
        List<Map<String, String>> values = process("D007", params);
        return values.get(0).get("yymsg");
    }

//    /**
//     * 获得化验单列表
//     *
//     * @param name
//     * @param idcardno
//     * @return
//     */
//    @Override
    public List<Inspection> getTestsList(String name, String idcardno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("brxm", name);
        params.put("sfzh", idcardno);
//
//        // 从数据库获取数据
        List<Map<String, String>> dataList = process("E001", params);
//
        List<Inspection> results = new ArrayList<>();
//
//        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            Inspection inspection = new Inspection(dataMap.get("doctadviseno"), dataMap.get("examinaim"),
                    dataMap.get("requesttime"), dataMap.get("requester"));
            results.add(inspection);
        }
//
        return results;
    }

    /**
     * 获得化验单抬头信息
     *
     * @param doctadviseno 条码号
     * @return 化验单抬头信息
     */
    @Override
    public InspectionInfo getTestInfo(String doctadviseno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("E002", params);

        InspectionInfo result = null;

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = new InspectionInfo(dataMap.get("doctadviseno"), dataMap.get("requesttime"),
                    dataMap.get("requester"), dataMap.get("executetime"), dataMap.get("executer"),
                    dataMap.get("receivetime"), dataMap.get("receiver"), dataMap.get("stayhospitalmode"),
                    dataMap.get("patientid"), dataMap.get("section"), dataMap.get("bedno"), dataMap.get("patientname"),
                    dataMap.get("sex"), dataMap.get("age"), dataMap.get("diagnostic"), dataMap.get("sampletype"),
                    dataMap.get("examinaim"), dataMap.get("requestmode"), dataMap.get("checker"),
                    dataMap.get("checktime"), null, null);
        }

        return result;
    }

    /**
     * 获得化验单指标项信息列表
     *
     * @param doctadviseno 条码号
     * @return 化验单指标信息列表
     */
    @Override
    public List<TestIndicator> getTestIndicatorsInfo(String doctadviseno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("E003", params);

        List<TestIndicator> results = new ArrayList<>();

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            TestIndicator indicator = new TestIndicator(dataMap.get("jylx"), dataMap.get("xmmc"), dataMap.get("result"),
                    dataMap.get("hint"), dataMap.get("ckfw"), dataMap.get("xmdw"));
            results.add(indicator);
        }

        return results;
    }

//    /**
//     * 获得检查单列表
//     *
//     * @param name
//     * @param idcardno
//     * @return 检查单的集合
//     */
//    @Override
    public List<Inspection> getInspectionsList(String name, String idcardno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("brxm", name);
        params.put("sfzh", idcardno);
//
//        // 从数据库获取数据
        List<Map<String, String>> dataList = process("E004", params);
//
        List<Inspection> results = new ArrayList<>();
//
//        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
       for (Map<String, String> dataMap : dataList) {
            Inspection inspection = new Inspection(dataMap.get("doctadviseno"), dataMap.get("examinaim"),
                    dataMap.get("requesttime"), dataMap.get("requester"));
            results.add(inspection);
        }
//
        return results;
    }

    /**
     * 获得检查单信息
     *
     * @param doctadviseno 条码号
     * @return InspectionInfo 检查单信息
     */
    @Override
    public InspectionInfo getInspectionInfo(String doctadviseno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("E005", params);

        InspectionInfo result = null;

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = new InspectionInfo(dataMap.get("doctadviseno"), dataMap.get("requesttime"),
                    dataMap.get("requester"), dataMap.get("executetime"), dataMap.get("executer"),
                    dataMap.get("receivetime"), dataMap.get("receiver"), dataMap.get("stayhospitalmode"),
                    dataMap.get("patientid"), dataMap.get("section"), dataMap.get("bedno"), dataMap.get("patientname"),
                    dataMap.get("sex"), dataMap.get("age"), dataMap.get("diagnostic"), dataMap.get("sampletype"),
                    dataMap.get("examinaim"), dataMap.get("requestmode"), dataMap.get("checker"),
                    dataMap.get("checktime"), dataMap.get("csyq"), dataMap.get("profiletest"));
        }

        return result;
    }

    /**
     * 获得检查单结果信息
     *
     * @param doctadviseno 条码号
     * @return 检查单结果信息
     */
    @Override
    public InspectionoResult getInspectionoResult(String doctadviseno) throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("E006", params);

        InspectionoResult result = null;

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = new InspectionoResult(dataMap.get("studyresult"), dataMap.get("diagresult"));
        }

        return result;
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
    public String buildOutpatientRechargeOrder(String openid, String patientname,
                                               String patientidcardno, String cardno,
                                               String patientid, String subject, String money)
            throws HospitalException {
        HashMap<String, Object> required = new HashMap<>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        String yhid = info.getYhid();
        required.put("yhid", yhid);
        required.put("openid", info.getAlipayUserId());
        required.put("ordername", subject);
        required.put("orderm", money);
        required.put("brid", patientid);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("F001", required);
        if (dataList.size() > 0) {
            return dataList.get(0).get("orderno");
        }

        return null;
    }


    /**
     * 获得门诊充值订单列表
     *
     * @param openid 用户标识
     * @return RechargeOrderHistoryInfo 用户充值订单信息
     */
    @Override
    public List<RechargeOrderHistoryInfo> getOutpatientRechargeOrdersList(String openid) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        required.put("yhid", userInfo.getYhid());
        required.put("brid", userInfo.getBrid());

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("F003", required);

        List<RechargeOrderHistoryInfo> result = new ArrayList<>();

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> valueMap : dataList) {
            RechargeOrderHistoryInfo info = new RechargeOrderHistoryInfo();
            info.setOpenid(valueMap.get("openid"));
            info.setPaymenttradeno(valueMap.get("wxorderno"));
            info.setTradeno(valueMap.get("orderno"));
            info.setStatus(valueMap.get("orderstatus"));
            info.setSubject(valueMap.get("ordername"));
            info.setMoney(valueMap.get("orderm"));
            info.setCtime(valueMap.get("ctime"));
            info.setPaytime(valueMap.get("paytime"));
            info.setRtntime(valueMap.get("rtntime"));
            info.setPatientid(valueMap.get("patientid"));
            info.setPatientname(valueMap.get("brxm"));
            info.setPatientidcardno(valueMap.get("sfzh"));
            result.add(info);
        }

        return result;
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
    public String cancelOutpatientRechargeOrder(String openId, String patientName, String patientId, String tradeno)
            throws HospitalException {
        HashMap<String, Object> params = new HashMap<>();
        params.put("orderno", tradeno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("F002", params);

        return tradeno;
    }

    /**
     * 门诊充值订单完成并到账
     *
     * @param info 用户订单信息
     * @return tradeno 订单号
     */
    @Override
    public String outpatientRechargeOrderFinish(RechargeOrderFinishInfo info) throws HospitalException {
        Map<String, Object> map = new HashMap<>();
        map.put("orderno", info.getTradeno());
        map.put("openid", info.getOpenid());
        map.put("wxorderno", info.getPaymenttradeno());
        map.put("orderm", info.getMoney());

        List<Map<String, String>> values = process("F005", map);
        System.out.println("通知医院已经到账返回的信息:" + values);
        if (values.size() < 1 || !"1".equals(values.get(0).get("state"))) {
            throw new HospitalException("订单完成失败");
        }
        return info.getTradeno();
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
        Map<String, Object> required = new HashMap<String, Object>();

        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        required.put("yhid", userInfo.getYhid());
        required.put("openid", userInfo.getAlipayUserId());
        required.put("zyhm", inpatientNo);
        required.put("orderm", money);
        required.put("ordername", subject);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("F006", required);

        return dataList.get(0).get("orderno");
    }

    /**
     * 获得住院充值订单列表
     *
     * @param openid 用户标识
     * @return 住院充值订单列表
     */
    @Override
    public List<RechargeOrderHistoryInfo> getInhospitalRechargeOrdersList(String openid) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        required.put("yhid", userInfo.getYhid());

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("F008", required);

        List<RechargeOrderHistoryInfo> result = new ArrayList<>();

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> valueMap : dataList) {
            RechargeOrderHistoryInfo info = new RechargeOrderHistoryInfo();
            info.setOpenid(valueMap.get("openid"));
            info.setPaymenttradeno(valueMap.get("wxorderno"));
            info.setTradeno(valueMap.get("orderno"));
            info.setStatus(valueMap.get("orderstatus"));
            info.setSubject(valueMap.get("ordername"));
            info.setMoney(valueMap.get("orderm"));
            info.setCtime(valueMap.get("ctime"));
            info.setPaytime(valueMap.get("paytime"));
            info.setRtntime(valueMap.get("rtntime"));
            info.setInpatientno(valueMap.get("inpatientno"));
            info.setPatientname(valueMap.get("brxm"));
            info.setPatientidcardno(valueMap.get("sfzh"));
            result.add(info);
        }

        return result;
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
    public String cancelInhospitalRechargeOrder(String openId, String patientName, String inpatientNo, String tradeno)
            throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        // 其它校验
        if (StringUtils.isBlank(tradeno)) {
            throw new HospitalException("订单流水号不能为空");
        }

        required.put("orderno", tradeno);

        // 从数据库获取数据
        List<Map<String, String>> dataList = process("F007", required);

        return tradeno;
    }

    /**
     * 住院充值订单完成并到账
     *
     * @param info 用户订单信息
     * @return 订单号
     */
    @Override
    public String inhospitalRechargeOrderFinish(RechargeOrderFinishInfo info) throws HospitalException {
        Map<String, Object> map = new HashMap<>();
        map.put("orderno", info.getTradeno());
        map.put("openid", info.getOpenid());
        map.put("wxorderno", info.getPaymenttradeno());
        map.put("orderm", info.getMoney());

        List<Map<String, String>> values = process("F009", map);
        System.out.println("通知医院已经到账返回的信息:" + values);
        if (values.size() < 1 || !"1".equals(values.get(0).get("state"))) {
            throw new HospitalException("订单完成失败");
        }
        return info.getTradeno();
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

        Map<String, Object> required = new HashMap<String, Object>();
        required.put("sfzh", idcardno);
        List<Map<String, String>> values = process("G007", required);

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        Map<String, String> dataMap = values.get(0);

        InhospitalPatientInfo result = new InhospitalPatientInfo();
        result.setZyh(dataMap.get("zyh"));// zyh 住院号
        result.setInpatientno(dataMap.get("zyhm"));// zyhm 住院号码
        result.setPatientname(dataMap.get("brxm"));
        result.setMzhm(dataMap.get("mzhm"));// mzhm 门诊号码
        result.setPatientidcardno(dataMap.get("sfzh"));
        result.setSex(dataMap.get("brxb"));
        result.setBirthday(dataMap.get("csny"));
        result.setAddress(dataMap.get("lxdz"));
        result.setPhone(dataMap.get("lxdh"));
        result.setAdmitdate(dataMap.get("ryrq"));
        result.setDischargedate(dataMap.get("cyrq"));
        try {
            result.setStayday(Integer.valueOf(dataMap.get("zyts")));
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        result.setXzmc(dataMap.get("xzmc"));
        result.setEndemicarea(dataMap.get("bqmc"));
        result.setBrch(dataMap.get("brch"));
        result.setDepartname(dataMap.get("ksmc"));
        result.setYlhj(dataMap.get("ylhj"));
        result.setLwhj(dataMap.get("lwhj"));
        result.setZfje(dataMap.get("zfje"));
        result.setJkje(dataMap.get("jkje"));
        result.setZyzt(dataMap.get("zyzt"));
        result.setDoctorname(dataMap.get("ysxm"));

        return result;
    }

    /**
     * 住院病人信息_按病人床号来查询
     *
     * @param brch 病人床号
     * @return 住院病人信息
     */
    public InhospitalPatientInfo getinhospitalPatientInfo(String brch) throws HospitalException {

        Map<String, Object> required = new HashMap<String, Object>();
        required.put("brch", brch);
        List<Map<String, String>> values = process("G006", required);

        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        Map<String, String> dataMap = values.get(0);

        InhospitalPatientInfo result = new InhospitalPatientInfo();
        result.setInpatientno(dataMap.get("zyhm"));
        // zyhm 住院号码
        result.setPatientname(dataMap.get("brxm"));
        // mzhm 门诊号码
        result.setPatientidcardno(dataMap.get("sfzh"));
        result.setSex(dataMap.get("brxb"));
        result.setBirthday(dataMap.get("csny"));
        result.setAddress(dataMap.get("lxdz"));
        result.setPhone(dataMap.get("lxdh"));
        result.setAdmitdate(dataMap.get("ryrq"));
        result.setDischargedate(dataMap.get("cyrq"));
        try {
            result.setStayday(Integer.valueOf(dataMap.get("zyts")));
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        result.setXzmc(dataMap.get("xzmc"));
        result.setEndemicarea(dataMap.get("bqmc"));
        result.setBrch(dataMap.get("brch"));
        result.setDepartname(dataMap.get("ksmc"));
        result.setYlhj(dataMap.get("ylhj"));
        result.setLwhj(dataMap.get("lwhj"));
        result.setZfje(dataMap.get("zfje"));
        result.setJkje(dataMap.get("jkje"));
        result.setZyzt(dataMap.get("zyzt"));
        result.setDoctorname(dataMap.get("ysxm"));

        return result;
    }

    /**
     * 住院费用列表
     *
     * @param inpatientno 住院号码
     * @return 住院费用信息
     */
    @Override
    public List<InHospitalOutlays> getInhospitalOutlaysList(String inpatientno, String costdate)
            throws HospitalException {

        // 获得住院费用
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("zyh", inpatientno);
        // fyrq 费用日期 string 是 格式：yyyy-MM-dd，如果为空时，获得所有费用列表
        if (costdate != null) {
            items.put("fyrq", costdate);
        } else {
            items.put("fyrq", "");
        }

        List<Map<String, String>> sales = process("G002", items);
        List<InHospitalOutlays> result = new ArrayList<>();
        for (Map<String, String> sale : sales) {
            InHospitalOutlays info = new InHospitalOutlays();
            info.setCostCode(sale.get("fyxm"));
            info.setCostName(sale.get("xmmc"));
            info.setTotalFee(sale.get("zjje"));
            info.setPayFee(sale.get("zfje"));
            result.add(info);
        }

        return result;
    }

    /** =======================技能中心 begin======================== */

    /**
     * 获得课程分类列表
     *
     * @param classifyCode 要获得哪个具体分类的信息,为空时返回所有课程
     * @return
     * @throws HospitalException
     */
    public List<CourseClassifyInfo> getCourseClassifyList(String classifyCode) throws HospitalException {
        Map<String, Object> map = new HashMap<String, Object>();
        map.put("kcdm", "0");
        List<Map<String, String>> values = process("T021", map);
        List<CourseClassifyInfo> result = new ArrayList<>();
        for (Map<String, String> value : values) {
            if (StringUtils.isBlank(classifyCode) || classifyCode.equals(value.get("kcdm"))) {
                CourseClassifyInfo item = new CourseClassifyInfo();
                item.setClassifyCode(value.get("kcdm"));
                item.setClassifyName(value.get("kcmc"));
                item.setExplain(value.get("bmsm"));
                item.setPrompt(value.get("bmxz"));
                result.add(item);
            }
            if (!StringUtils.isBlank(classifyCode) && classifyCode.equals(value.get("kcdm"))) {
                break;
            }
        }
        return result;
    }

    /**
     * 获得某个分类下的课程列表
     *
     * @param classifyCode 分类代码
     * @param courseCode   课程代码,不为空时查询这个具体的课程信息
     * @return
     * @throws HospitalException
     */
    public List<CourseInfo> getCourseList(String classifyCode, String courseCode) throws HospitalException {
        Map<String, Object> required = new HashMap<>();
        required.put("kcdm", classifyCode);
        List<Map<String, String>> values = process("T022", required);
        List<CourseInfo> result = new ArrayList<>();
        for (Map<String, String> value : values) {
            if (StringUtils.isBlank(courseCode) || courseCode.equals(value.get("fyxh"))) {
                CourseInfo item = new CourseInfo();
                item.setClassifyCode(classifyCode);
                item.setCourseCode(value.get("fyxh"));
                item.setCoursename(value.get("fymc"));
                item.setCoursefee(value.get("fydj"));
                item.setCourseremark(value.get("bzxx"));
                result.add(item);
            }
            if (!StringUtils.isBlank(courseCode) && courseCode.equals(value.get("fyxh"))) {
                break;
            }
        }
        return result;
    }

    /**
     * 课程报名
     *
     * @param courseCode 课程代码
     * @param people     报告人数
     * @param orderm     订单金额
     * @param ordeName   订单名
     * @param userRemark 客户备注
     * @return 订单号
     * @throws HospitalException
     */
    public String courseRegisterFor(String courseCode, Integer people, String orderm, String ordeName,
                                    String userRemark) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        required.put("yhid", userInfo.getYhid());
        required.put("openid", userInfo.getAlipayUserId());

        required.put("fyxh", courseCode);
        required.put("fysl", people);
        required.put("orderm", orderm);
        required.put("ordername", ordeName);
        required.put("usertext", userRemark);
        List<Map<String, String>> values = process("F011", required);
        if (values.size() > 0) {
            Map<String, String> value = values.get(0);
            return value.get("orderno");
        }
        return null;
    }

    /**
     * 获得课程订单列表
     *
     * @param openId 用户标识
     * @return 课程订单列表
     * @throws HospitalException
     */
    public List<CourseOrderHistoryInfo> getCourseOrderList(String openId) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        if (StringUtils.isEmpty(openId)) {
            System.out.println("传入的支付宝用户ID为空,使用会话中的用户信息.");
            UserInfo info = ServiceContext.getHospitalUserInfo();
            required.put("yhid", info.getYhid());
        } else {
            UserInfo userInfo = UserInfoService.getUserInfo(openId);
            if (userInfo == null) {
                throw new HospitalException("获得用户信息错误");
            }
            required.put("yhid", userInfo.getYhid());
        }
        final List<Map<String, String>> values = process("F013", required);
        if (values.size() <= 0) {
            throw new HospitalException("没有报名记录");
        }

        List<CourseOrderHistoryInfo> result = new ArrayList<>();
        for (Map<String, String> valueMap : values) {
            CourseOrderHistoryInfo info = new CourseOrderHistoryInfo();
            info.setOpenid(valueMap.get("openid"));
            info.setPaymenttradeno(valueMap.get("wxorderno"));
            info.setTradeno(valueMap.get("orderno"));
            info.setStatus(valueMap.get("orderstatus"));
            info.setSubject(valueMap.get("ordername"));
            info.setMoney(valueMap.get("orderm"));
            info.setCtime(valueMap.get("ctime"));
            info.setPaytime(valueMap.get("paytime"));
            info.setRtntime(valueMap.get("rtntime"));
            info.setCourseCode(valueMap.get("fyxh"));
            info.setCoursename(valueMap.get("fymc"));
            info.setPeople(valueMap.get("fysl"));
            info.setPrice(valueMap.get("fydj"));
            info.setPatientname(valueMap.get("brxm"));
            info.setPatientidcardno(valueMap.get("sfzh"));
            info.setUserRemark(valueMap.get("usertext"));
            result.add(info);
        }
        return result;
    }

    /**
     * 取消课程报告订单
     *
     * @param orderNo 订单号
     * @return 是否取消成功
     * @throws HospitalException
     */
    public boolean cancelCourseOrder(String orderNo) throws HospitalException {
        Map<String, Object> required = new HashMap<>();
        required.put("orderno", orderNo);
        List<Map<String, String>> values = process("F012", required);
        return values != null && values.size() > 0;
    }

    /**
     * =======================技能中心 end========================
     */

	/*
     * 门诊候诊信息
	 * 
	 * @param pdlsh 排队序号
	 * 
	 * @return 候诊信息
	 */
    public OutpatientWaitingInfo getOutpatientWaitingInfo(String pdlsh) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        required.put("pdlsh", pdlsh);
        List<Map<String, String>> values = process("D010", required);
        OutpatientWaitingInfo info = new OutpatientWaitingInfo();
        info.setMessage(values.get(0).get("hzxx"));
        return info;
    }

    /*
     * 门诊已预约报道
     *
     * @param yhid 用户id
     *
     * @param brid 病人ID
     *
     * @return 预约信息
     */
    public List<OutpatientOrderReponse> getAlreadyOutpatientOrder(String yhid, String brid) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        required.put("yhid", yhid);
        required.put("brid", brid);
        List<Map<String, String>> values = process("D017", required);
        List<OutpatientOrderReponse> list = new ArrayList<OutpatientOrderReponse>();
        for (Map<String, String> map : values) {
            OutpatientOrderReponse info = new OutpatientOrderReponse();
            info.setPreengageseq(map.get("yylsh"));
            info.setPdlsh(map.get("pdlsh"));
            list.add(info);
        }
        return list;
    }

    /*
     * 门诊排队列表
     *
     *
     * @return 门诊排队列表
     */
    public List<OutpatientWaitingList> getOutpatientWaitingList() throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        required.put("yhid", info.getYhid());
        required.put("brid", info.getBrid());
        List<Map<String, String>> values = process("D017", required);
        List<OutpatientWaitingList> list = new ArrayList<OutpatientWaitingList>();
        for (Map<String, String> map : values) {
            OutpatientWaitingList result = new OutpatientWaitingList();
            result.setBrid(map.get("brid"));
            result.setYylsh(map.get("yylsh"));
            result.setPdlsh(map.get("pdlsh"));
            result.setBrxm(map.get("brxm"));
            result.setFzxh(map.get("fzxh"));
            result.setYysj(map.get("yysj"));
            result.setBdzt(map.get("bdzt"));
            result.setYylb(map.get("yylb"));
            result.setPdhm(map.get("pdhm"));
            result.setPdrq(map.get("pdrq"));
            result.setBcmc(map.get("bcmc"));
            result.setKsmc(map.get("ksmc"));
            result.setYsxm(map.get("ysxm"));
            result.setZsmc(map.get("zsmc"));
            result.setZswz(map.get("zswz"));
            result.setFjhm(map.get("fjhm"));
            result.setPdzt(map.get("pdzt"));
            list.add(result);
        }
        return list;
    }

    @Override
    public boolean cancelRegistration(String thbs) throws HospitalException {
        return false;
    }

    /**
     * 门诊预约报到
     *
     * @param yylsh 预约流水号
     * @return 门诊预约返回信息
     */
    public boolean getoutpatientOrder(String yylsh) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        required.put("yhid", info.getYhid());
        required.put("brid", info.getBrid());
        required.put("yylsh", yylsh);
        List<Map<String, String>> values = process("D008", required);
        return values != null;
    }

    /**
     * 医院信息
     *
     * @param hospid 医院id 固定值：1
     * @return 医院信息
     */
    public HospitalInfo getHospitalInfo(String hospid) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        required.put("hospid", hospid);
        List<Map<String, String>> values = process("D012", required);
        HospitalInfo ho = new HospitalInfo();
        Map<String, String> info = values.get(0);
        ho.setHospid(info.get("hospid"));
        ho.setYymc(info.get("yymc"));
        ho.setYyjc(info.get("yyjc"));
        ho.setYydj(info.get("yydj"));
        ho.setYydz(info.get("yydz"));
        ho.setYydh(info.get("yydh"));
        ho.setYyjj(info.get("yyjj"));
        ho.setJzxz(info.get("jzxz"));
        return ho;
    }

    /**
     * 住院病人费用明细
     *
     * @param inhospitalcode 住院号
     * @param fyxm           费用项目
     * @param fyrq           费用日期
     * @return 住院费用信息
     */
    public List<InHospitalOutlays> getInhospitalOutlaysDetail(String inhospitalcode, String fyxm, String fyrq)
            throws HospitalException {
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("zyh", inhospitalcode);
        items.put("fyxm", fyxm);
        // fyrq 费用日期 string 是 格式：yyyy-MM-dd，如果为空时，获得所有费用列表
        if (fyrq != null) {
            items.put("fyrq", fyrq);
        } else {
            items.put("fyrq", "");
        }

        List<Map<String, String>> sales = process("G003", items);
        List<InHospitalOutlays> result = new ArrayList<InHospitalOutlays>();
        for (Map<String, String> sale : sales) {
            if (StringUtils.isBlank(fyrq) || "".equals(fyrq) || StringUtils.isNotBlank(fyrq)) {
                InHospitalOutlays info = new InHospitalOutlays();
                info.setFyxh(sale.get("fyxh"));
                info.setFyxm(sale.get("fyxm"));
                info.setFymc(sale.get("fymc"));
                info.setFygg(sale.get("fygg"));
                info.setFydw(sale.get("fydw"));
                info.setYbdm(sale.get("ybdm"));
                info.setZfbl(sale.get("zfbl"));
                info.setYplx(sale.get("yplx"));
                info.setFysl(sale.get("fysl"));
                info.setFydj(sale.get("fydj"));
                info.setZjje(sale.get("zfje"));
                result.add(info);
            }
            if (StringUtils.isNotBlank(fyrq)) {
                break;
            }
        }
        return result;
    }

    /**
     * 检查预约报道列表
     *
     * @param yhid 用户id
     * @param brid 病人ID
     * @return 病人信息
     */
    public List<OutpatientWaitingList> checkPrecontractReportedList(String yhid, String brid) throws HospitalException {
        Map<String, Object> map = new HashMap<String, Object>();
        UserInfo info = ServiceContext.getHospitalUserInfo();
        map.put("yhid", info.getYhid());
        map.put("brid", info.getBrid());
        List<Map<String, String>> result = process("D018", map);
        List<OutpatientWaitingList> list = new ArrayList<OutpatientWaitingList>();
        for (Map<String, String> value : result) {
            OutpatientWaitingList ou = new OutpatientWaitingList();
            ou.setBrid(value.get("brid"));
            ou.setYylsh(value.get("yylsh"));
            ou.setPdlsh(value.get("pdlsh"));
            ou.setBrxm(value.get("brxm"));
            ou.setFzxh(value.get("fzxh"));
            ou.setYysj(value.get("yysj"));
            ou.setBdzt(value.get("bdzt"));
            ou.setPdhm(value.get("pdhm"));
            ou.setPdrq(value.get("pdrq"));
            ou.setBcmc(value.get("bcmc"));
            ou.setDlxm(value.get("dlxm"));
            ou.setDlmc(value.get("dlmc"));
            ou.setJcxm(value.get("jcxm"));
            ou.setZsmc(value.get("zsmc"));
            ou.setZswz(value.get("zswz"));
            ou.setFjhm(value.get("fjhm"));
            ou.setPdzt(value.get("pdzt"));
            list.add(ou);
        }
        return list;
    }

    /**
     * 取药信息
     *
     * @param yhid 用户ID
     * @param brid 病人ID
     * @return 取药信息
     */
    public List<MedicineInfoTake> getTakeMedicineInfoDetail(String yhid, String brid)
            throws HospitalException {
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("yhid", yhid);
        items.put("brid", brid);

        List<Map<String, String>> values = process("D021", items);
        List<MedicineInfoTake> result = new ArrayList<MedicineInfoTake>();
        for (Map<String, String> value : values) {
            MedicineInfoTake info = new MedicineInfoTake();
            info.setBrid(value.get("brid"));
            info.setBrxm(value.get("brxm"));
            info.setFphm(value.get("fphm"));
            info.setFyck(value.get("fyck"));
            info.setLxmc(value.get("lxmc"));
            info.setQywz(value.get("qywz"));
            info.setSfrq(value.get("sfrq"));
            info.setYfmc(value.get("yfmc"));
            info.setZjje(value.get("zjje"));
            result.add(info);
        }
        return result;
    }

    /**
     * 服药信息列表
     *
     * @param openid 用户ID
     * @param brid   病人ID
     * @return 取药信息
     */
    @Override
    public List<MedicineUseInfoList> getMedicineUseInfoListDetail(String openid, String brid)
            throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("yhid", userInfo.getYhid());
        items.put("brid", userInfo.getBrid());

        List<Map<String, String>> values = process("D022", items);
        List<MedicineUseInfoList> result = new ArrayList<MedicineUseInfoList>();
        for (Map<String, String> value : values) {
            MedicineUseInfoList info = new MedicineUseInfoList();
            info.setBrid(value.get("brid"));
            info.setBrxm(value.get("brxm"));
            info.setCflsh(value.get("cflsh"));
            info.setCflx(value.get("cflx"));
            info.setFphm(value.get("fphm"));
            info.setKfrq(value.get("kfrq"));
            info.setKsbm(value.get("ksbm"));
            info.setKsmc(value.get("ksmc"));
            info.setLxmc(value.get("lxmc"));
            info.setSfrq(value.get("sfrq"));
            info.setYsbm(value.get("ysbm"));
            info.setYsxm(value.get("ysxm"));
            info.setZjje(value.get("zjje"));
            info.setCfje(value.get("cfje"));
            info.setTzybz(value.get("tzybz"));
            info.setZfzt(value.get("zfzt"));
            result.add(info);
        }
        return result;
    }

    /**
     * 服药信息
     *
     * @param cflsh 处方流水号
     * @return 取药信息
     */
    public List<MedicineUseInfo> getMedicineUseInfoDetail(String cflsh)
            throws HospitalException {
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("cflsh", cflsh);

        List<Map<String, String>> values = process("D023", items);

        List<MedicineUseInfo> result = new ArrayList<MedicineUseInfo>();
        for (Map<String, String> value : values) {
            MedicineUseInfo info = new MedicineUseInfo();
            info.setGytj(value.get("gytj"));
            info.setSync(value.get("sypc"));
            info.setYcyl(value.get("ycyl"));
            info.setYpgg(value.get("ypgg"));
            info.setYpmc(value.get("ypmc"));
            info.setYpsl(value.get("ypsl"));
            info.setTzybz(value.get("tzybz"));
            result.add(info);
        }
        return result;
    }

    /**
     * 检查预约报到
     *
     * @param yhid  用户ID
     * @param brid  病人ID
     * @param yylsh 预约流水号
     * @return 检查是否预约报到
     */
    public boolean checkPrecontractReported(String yhid, String brid, String yylsh) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        Map<String, Object> map = new HashMap<String, Object>();
        map.put("yhid", info.getYhid());
        map.put("brid", info.getBrid());
        map.put("yylsh", yylsh);
        List<Map<String, String>> value = process("D009", map);
        return value != null;
    }

    /**
     * 检查候诊信息
     *
     * @param pdlsh 排队流水号
     * @return hzxx 候诊信息
     */
    public OutpatientWaitingInfo checkWaitingInfo(String pdlsh) throws HospitalException {
        Map<String, Object> map = new HashMap<String, Object>();
        map.put("pdlsh", pdlsh);
        List<Map<String, String>> value = process("D011", map);
        OutpatientWaitingInfo result = new OutpatientWaitingInfo();
        if (value.size() > 0) {
            result.setHzxx(value.get(0).get("hzxx"));
        }
        return result;
    }

    /**
     * 检查预约报到注意事项
     *
     * @param yylsh 预约流水号
     * @rerturn zysx 注意事项
     */
    public OutpatientWaitingList checkPrecontractReportedNotice(String yylsh) throws HospitalException {
        Map<String, Object> map = new HashMap<String, Object>();
        map.put("yylsh", yylsh);
        List<Map<String, String>> value = process("D020", map);
        OutpatientWaitingList info = new OutpatientWaitingList();
        if (value.size() > 0) {
            info.setZysx(value.get(0).get("zysx"));
        }
        return info;
    }

    /**
     * 门诊就诊记录列表
     *
     * @param yhid 用户ID
     * @param brid 病人ID
     * @return 门诊就诊记录
     */
    @Override
    public List<OutpatientVisitRecord> getOutpatientCaseList(String yhid, String brid)
            throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("yhid", info.getYhid());
        items.put("brid", info.getBrid());

        List<Map<String, String>> values = process("D030", items);
        List<OutpatientVisitRecord> result = new ArrayList<OutpatientVisitRecord>();
        for (Map<String, String> value : values) {
            OutpatientVisitRecord outpatientVisitRecord = new OutpatientVisitRecord();
            outpatientVisitRecord.setJzrq(value.get("jzrq"));
            outpatientVisitRecord.setJzxh(value.get("jzxh"));
            outpatientVisitRecord.setKsmc(value.get("ksmc"));
            outpatientVisitRecord.setYsxm(value.get("ysxm"));
            outpatientVisitRecord.setZdxx(value.get("zdxx"));
            result.add(outpatientVisitRecord);
        }
        return result;
    }

    /**
     * 门诊就诊信息（电子病历）
     *
     * @param jzxh 就诊序号
     * @return 就诊信息
     */
    @Override
    public OutpatientVisitInfo getOutpatientVisitInfoDetail(String jzxh)
            throws HospitalException {
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("jzxh", jzxh);

        List<Map<String, String>> values = process("D031", items);
        OutpatientVisitInfo info = new OutpatientVisitInfo();
        if (values.size() > 0) {
            Map<String, String> value = values.get(0);
            info.setClyj(value.get("clyj"));
            info.setFzjc(value.get("fzjc"));
            info.setGms(value.get("gms"));
            info.setGrs(value.get("grs"));
            info.setHys(value.get("hys"));
            info.setJws(value.get("jws"));
            info.setJzs(value.get("jzs"));
            info.setMzzs(value.get("mzzs"));
            info.setTgjc(value.get("tgjc"));
            info.setXbs(value.get("xbs"));
        }
        return info;
    }

    /**
     * 门诊就诊指引单
     *
     * @param jzxh 就诊序号
     * @return 取药信息
     */
    @Override
    public OutpatientVisitGuideBills getOutpatientVisitGuideBillsDetail(String jzxh)
            throws HospitalException {
        Map<String, Object> items = new HashMap<String, Object>();
        items.put("jzxh", jzxh);

        List<Map<String, String>> values = process("D032", items);
        if (values.size() == 0) {
            return null;
        }
        Map<String, String> value = values.get(0);
        OutpatientVisitGuideBills info = new OutpatientVisitGuideBills();
        info.setCflx(value.get("cflx"));
        info.setCfxh(value.get("cfxh"));
        info.setFphm(value.get("fphm"));
        info.setFyck(value.get("fyck"));
        info.setKfks(value.get("kfks"));
        info.setKsmc(value.get("ksmc"));
        info.setKfrq(value.get("kfrq"));
        info.setYjpb(value.get("yjpb"));
        info.setYsxm(value.get("ysxm"));
        info.setZjje(value.get("zjje"));
        info.setZsks(value.get("zsks"));
        info.setZsksmc(value.get("zsksmc"));
        info.setZspb(value.get("zspb"));
        info.setZynr(value.get("zynr"));
        info.setZywz(value.get("zywz"));
        return info;
    }


    @Override
    public List<OutpatientWaitingList> getOutpatientWaitingListBeforeTen() throws HospitalException {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public boolean checkPrecontractReported(String openid, String preengageseq) throws HospitalException {
        // TODO Auto-generated method stub
        return false;
    }


    @Override
    public List<OutpatientWaitingList> checkOutpatientWaitingListBeforeTen() throws HospitalException {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public List<BedQueryInfo> querySurplusBed() throws HospitalException {
        return null;
    }

    @Override
    public List<DrugPriceInfo> queryDrugPrice(String pageno, String pagerow) throws HospitalException {
        return null;
    }

    @Override
    public List<DrugPriceInfo> queryDrugPriceByPy(String pydm) throws HospitalException {
        return null;
    }

    @Override
    public List<FeeItemsInfo> queryFeeItems(String pageno, String pagerow) throws HospitalException {
        return null;
    }

    @Override
    public List<FeeItemsInfo> queryFeeItemsByPy(String pydm) throws HospitalException {
        return null;
    }


    @Override
    public List<VisitInfo> InformVisitInfo() throws HospitalException {
        return null;
    }

    @Override
    public List<RechargeOrderFinishInfo> CheckNotAccountInfo() throws HospitalException {
        return null;
    }

    /**
     * 当日预约挂号-科室列表
     *
     * @param
     * @return list
     */
    public List<DepartmentInfo> getDepartmentsListForOrderofDay() throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yyks", "0");
        List<Map<String, String>> process = process("D033", parameters);
        List<DepartmentInfo> deinfolist = new ArrayList<DepartmentInfo>();
        for (Map<String, String> map : process) {
            DepartmentInfo deinfo = new DepartmentInfo();
            deinfo.setDepartcode(map.get("deptid"));
            deinfo.setDepartname(map.get("ksfl"));
            deinfo.setSecondcode(map.get("ksbm"));
            deinfo.setSecondname(map.get("ksmc"));
            deinfo.setDescribe(map.get("ksms"));
            deinfolist.add(deinfo);
        }
        return deinfolist;
    }

    /**
     * 当日预约挂号-预约科室查医生列表
     *
     * @param departid 科室代码
     * @return 医生列表list
     */
    public List<TodayDoctorInfo> getDoctorListofDayByDepartments(String departid) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("deptid", departid);
        List<Map<String, String>> process = process("D034", parameters);
        List<TodayDoctorInfo> dcinfolist = new ArrayList<TodayDoctorInfo>();
        for (Map<String, String> doct : process) {
            TodayDoctorInfo dcinfo = (TodayDoctorInfo) JSONObject.toBean(JSONObject.fromObject(doct), TodayDoctorInfo.class);
            ;
            dcinfolist.add(dcinfo);
        }
        return dcinfolist;
    }

    /**
     * 当日预约挂号-确认挂号
     *
     * @param outpatientOrderRequest(sessionid会话ID,linkmanid 联系人ID,orderseq 号源流水号)
     * @return orderid    预约流水号   ordermsg   预约信息
     */
    public OutpatientOrder confirmRegistration(OutpatientOrderRequest outpatientOrderRequest) throws HospitalException {
        UserInfo userinfo = ServiceContext.getHospitalUserInfo();
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yhid", userinfo.getYhid());
        parameters.put("pblsh", outpatientOrderRequest.getOrderseq());
        parameters.put("brxm", outpatientOrderRequest.getPatientname());
        parameters.put("lxdh", outpatientOrderRequest.getPatientphone());
        parameters.put("sfzh", outpatientOrderRequest.getPatientidcardno());
        parameters.put("lxdz", outpatientOrderRequest.getPatientaddress());
        List<Map<String, String>> process = process("D035", parameters);
        OutpatientOrder info = new OutpatientOrder();
        info.setPreengageseq(process.get(0).get("yylsh"));
        info.setState(process.get(0).get("state"));
        info.setOrdermsg(process.get(0).get("yymsg"));
        return info;
    }

    /**
     * 体检套餐列表
     *
     * @return packagesID 套餐ID packagesNake 套餐名称 packagePrice套餐价格
     * packagesStandard 套餐标准 packagesCategory 套餐类别
     */
    public List<PhysicalPackages> getPhysicalPackagesList()
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("tcid", "0");
        List<Map<String, String>> process = process("T001", parameters);

        List<PhysicalPackages> list = new ArrayList<PhysicalPackages>();
        for (Map<String, String> m : process) {
            PhysicalPackages p = new PhysicalPackages();
            p.setPackagesID(m.get("tcid"));
            p.setPackagesName(m.get("tcmc"));
            p.setPackagesPrice(m.get("tcjg"));
            p.setPackagesStandard(m.get("tcbz"));
            p.setPackagesCategory(m.get("tclb"));
            list.add(p);
        }
        return list;
    }

    /**
     * 体检套餐明细
     *
     * @param packagesID 套餐ID
     * @return xmfl 项目分类 xmmc 项目名称
     */
    public List<PhysicalItem> getPhysicalPackagesDetail(String packagesID)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("tcid", packagesID);
        List<Map<String, String>> process = process("T002", parameters);
        List<PhysicalItem> list = new ArrayList<PhysicalItem>();
        for (Map<String, String> m : process) {
            PhysicalItem p = new PhysicalItem();
            p.setItemCategory(m.get("xmfl"));
            p.setItemName(m.get("xmmc"));
            list.add(p);
        }
        return list;
    }

    /**
     * 体检状态信息(手机号码)
     *
     * @param phone 手机号码
     * @return tjbh 体检编号 xm 姓名 xb 性别 nl 年龄 tcmc 套餐名称 tjrq 体检日期 tjzt 体检状态
     */
    public List<PhysicalStatusInfo> getPhysicalStatusInfo(String phone)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("sjhm", phone);
        List<Map<String, String>> process = process("T003", parameters);
        List<PhysicalStatusInfo> list = new ArrayList<PhysicalStatusInfo>();
        for (Map<String, String> m : process) {
            PhysicalStatusInfo p = new PhysicalStatusInfo();
            p.setPhysicalID(m.get("tjbh"));
            p.setName(m.get("xm"));
            p.setSex(m.get("xb"));
            p.setAge(m.get("nl"));
            p.setPackagesName(m.get("tcmc"));
            p.setPhysicalDate(m.get("tjrq"));
            p.setPhysicalStatus(m.get("tjzt"));
            list.add(p);
        }
        return list;
    }

    /**
     * 体检查询报告
     *
     * @param physicalID 体检编号
     * @return jbxx基本信息 ：tjbh 体检编号，name姓名，sex性别，age年龄，sfzh身份证号；
     * data体检情况：ksmc科室名称：xiangmu1主项目：xiangmu2子项目：jg 结果,dw
     * 单位,ckfw参考范围,ycts异常提示,ycbz异常标志； xj小结：xjqk小结情况，xjys小结医生，xjrq小结日期；
     * zj 总检 ：zs 综述，jy建议，zsys综述医生，zsrq综述日期；
     */
    @SuppressWarnings("unchecked")
    public Map<String, Object> getPhysicalReport(String physicalID)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("tjbh", physicalID);
//		parameters.put("tjbh", "1505150493");// 测试数据
        List<Map<String, String>> process = process("T004", parameters);
        String result = process.get(0).get("tjbh");
        Map<String, Object> map = new HashMap<String, Object>();
        StringReader reader = new StringReader(result);
        InputSource source = new InputSource(reader);
        SAXBuilder sax = new SAXBuilder();
        Document doc = null;
        try {
            doc = sax.build(source);
        } catch (JDOMException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        Element root = doc.getRootElement();
        // Element grtj = root.getChild("root");
        List<Element> jbxx = (List<Element>) root.getChildren("jbxx");
        Map<String, String> jbxxMap = new HashMap<String, String>();
        jbxxMap.put("tjbh", jbxx.get(0).getChildText("tjbh"));
        jbxxMap.put("name", jbxx.get(0).getChildText("name"));
        jbxxMap.put("sex", jbxx.get(0).getChildText("sex"));
        jbxxMap.put("age", jbxx.get(0).getChildText("age"));
        jbxxMap.put("sfzh", jbxx.get(0).getChildText("sfzh"));
        map.put("jbxx", jbxxMap);

        List<Element> data = (List<Element>) root.getChildren("data");
        Map<String, Object> dataMap = new HashMap<String, Object>();
        List<Element> ksmc = (List<Element>) data.get(0).getChildren("ksmc");
        for (Element ksmcE : ksmc) {
            Map<String, Object> xmMap = new HashMap<String, Object>();
            List<Element> xiangmu1 = (List<Element>) ksmcE
                    .getChildren("xiangmu1");
            Map<String, Object> ksmcMap = new HashMap<String, Object>();
            for (Element xiangmu1E : xiangmu1) {
                List<Element> xiangmu2 = (List<Element>) xiangmu1E
                        .getChildren("xiangmu2");
                Map<String, Object> xiangmu1Map = new HashMap<String, Object>();
                for (Element xiangmu2E : xiangmu2) {
                    Map<String, String> xiangmu2Map = new HashMap<String, String>();
                    xiangmu2Map.put("jg", xiangmu2E.getChildText("jg"));
                    xiangmu2Map.put("dw", xiangmu2E.getChildText("dw"));
                    xiangmu2Map.put("ckfw", xiangmu2E.getChildText("ckfw"));
                    xiangmu2Map.put("ycts", xiangmu2E.getChildText("ycts"));
                    xiangmu2Map.put("ycbz", xiangmu2E.getChildText("ycbz"));
                    xiangmu1Map.put(xiangmu2E.getAttributeValue("text"),
                            xiangmu2Map);
                }
                xmMap.put(xiangmu1E.getAttributeValue("text"), xiangmu1Map);
            }
            List<Element> xj = (List<Element>) ksmcE.getChildren("xj");
            Map<String, String> xjMap = new HashMap<String, String>();
            xjMap.put("xjqk", xj.get(0).getChildText("xjqk"));
            xjMap.put("xjys", xj.get(0).getChildText("xjys"));
            xjMap.put("xjrq", xj.get(0).getChildText("xjrq"));
            ksmcMap.put("xj", xjMap);
            ksmcMap.put("xm", xmMap);
            dataMap.put(ksmcE.getAttributeValue("text"), ksmcMap);
        }
        map.put("data", dataMap);

        List<Element> zj = (List<Element>) root.getChildren("zj");
        Map<String, String> zjMap = new HashMap<String, String>();
        zjMap.put("zs", zj.get(0).getChildText("zs"));
        zjMap.put("jy", zj.get(0).getChildText("jy"));
        zjMap.put("zjys", zj.get(0).getChildText("zjys"));
        zjMap.put("zjrq", zj.get(0).getChildText("zjrq"));
        map.put("zj", zjMap);
        return map;
    }

    /**
     * 体检预约
     *
     * @param physicalOrderRequest 体检预约请求
     * @return orderid 预约流水号 state 标志
     */
    public PhysicalOrder physicalOrder(PhysicalOrderRequest physicalOrderRequest)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yhid", physicalOrderRequest.getUserID());
        parameters.put("brxm", physicalOrderRequest.getPatientName());
        parameters.put("brxb", physicalOrderRequest.getPatientSex());
        parameters.put("csny", physicalOrderRequest.getBirthday());
        parameters.put("sfzh", physicalOrderRequest.getCardID());
        parameters.put("lxdh", physicalOrderRequest.getPhone());
        parameters.put("hyzk", physicalOrderRequest.getIsMarried());
        parameters.put("yyrq", physicalOrderRequest.getOrderDate());
        parameters.put("tcid", physicalOrderRequest.getPackagesID());
        List<Map<String, String>> process = process("T005", parameters);
        PhysicalOrder p = new PhysicalOrder();
        p.setOrderId(process.get(0).get("yylsh"));
        p.setState(process.get(0).get("state"));
        return p;
    }

    /**
     * 体检预约作废
     *
     * @param orderID 预约流水号
     * @return state 标志
     */
    public String physicalOrderCancel(String orderID) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yylsh", orderID);
        List<Map<String, String>> process = process("T006", parameters);
        return process.get(0).get("state");
    }

    /**
     * 体检预约信息查询
     *
     * @param userID 用户id
     * @return
     */
    public List<PhysicalOrderInfo> physicalOrderList(String userID)
            throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("yhid", userID);
        List<Map<String, String>> process = process("T007", parameters);
        List<PhysicalOrderInfo> list = new ArrayList<PhysicalOrderInfo>();
        for (Map<String, String> m : process) {
            PhysicalOrderInfo p = new PhysicalOrderInfo();
            p.setCardID(m.get("sfzh"));
            p.setOrderDate(m.get("yyrq"));
            p.setOrderID(m.get("yylsh"));
            p.setOrderStatus(m.get("yyzt"));
            p.setPackagesName(m.get("tcmc"));
            p.setPackagesPrice(m.get("tcjg"));
            p.setPatientName(m.get("brxm"));
            p.setPhone(m.get("lxdh"));
            p.setUserID(m.get("yhid"));
            p.setPhysicalID("tjbh");
            list.add(p);
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

    @Override
    public List<Map<String, String>> getPrescriptionList(String patientId) throws HospitalException {
        return null;
    }

    @Override
    public List<Map<String, String>> getPrescriptionDetail(String lsCode) throws HospitalException {
        return null;
    }

    @Override
    public List<Map<String, String>> getContactPersonList(String openId) throws HospitalException {
        return null;
    }

    @Override
    public List<Map<String, String>> commonService(String hCode, Map<String, Object> parameters) throws HospitalException {
        return null;
    }

    @Override
    public Map<String, String> getAppoint(String doctorCode, String patientCode) {
        return null;
    }

    @Override
    public Map<String, String> getPatientInfo(String patientCode) throws HospitalException {
        return null;
    }


}
