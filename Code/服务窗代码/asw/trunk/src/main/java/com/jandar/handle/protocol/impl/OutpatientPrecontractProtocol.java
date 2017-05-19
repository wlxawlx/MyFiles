package com.jandar.handle.protocol.impl;

import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.OutpatientOrderRequest;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 门诊预约处理类
 */
public class OutpatientPrecontractProtocol implements Protocol {

    /**
     * 线程池
     */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        final Map<String, String> map = HospitalInfoService.getInstance().outpatientOrderResult(requiredParams(params));
        final String yymsg = map.get("ordermsg");
        final String wxmsg = "请您携带身份证、就诊卡、病历提前在支付宝上办理预约签到或挂号室报到。"
                + "因故不能来请提前在支付宝上取消预约，迟到者原预约号失效。" +
                "谢谢您的配合，祝您健康！";
        if (!StringUtils.isBlank(yymsg)) {
            Map<String, String> result = new HashMap<>();
            result.put("stats", "1");
            result.put("ordermsg", yymsg);
            result.put("orderid",map.get("orderid"));
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            final String alipayuserid = userInfo.getAlipayUserId();
            executors.execute(new Runnable() {
                @Override
                public void run() {
                    AlipayMsgSendUtil.sendSingleImgTextMsg(alipayuserid, "预约成功", yymsg);
                }
            });
            executors.execute(new Runnable() {
                @Override
                public void run() {
                    AlipayMsgSendUtil.sendSingleImgTextMsg(alipayuserid, "温馨提示", wxmsg);
                }
            });
            return result;
        } else {
            throw new HospitalException("预约失败");
        }

    }


    private OutpatientOrderRequest requiredParams(Map<String, Object> params) throws HospitalException {

        // 其它校验
        String orderseq = MapUtil.getString(params, "orderseq");
        String ordertime = MapUtil.getString(params, "ordertime");
        String orderendtime = MapUtil.getString(params, "orderendtime");
        // 在七院中 X 下午, S 上午
        String shiftcode = /*"X";*/ MapUtil.getString(params, "shiftcode");
        String scheduleseq = MapUtil.getString(params, "scheduleseq");
        String orderno = MapUtil.getString(params, "orderno");
        String doctorname = MapUtil.getString(params, "doctorname");
        int sourcetype = MapUtil.getString(params, "sourcetype")==null?8:Integer.parseInt( MapUtil.getString(params, "sourcetype"));
        System.out.println("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!传入sourcetype"+sourcetype);
        if (StringUtils.isBlank(orderseq)) {
            throw new HospitalException("orderseq不能为空");
        }

        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        OutpatientOrderRequest required = new OutpatientOrderRequest();
        required.setOpenid(userInfo.getAlipayUserId());
        required.setOrderseq(orderseq);
        required.setOrdertime(ordertime);
        required.setOrderendtime(orderendtime);
        required.setShiftcode(shiftcode);
        required.setScheduleseq(scheduleseq);
        required.setOrderno(orderno);
        required.setDoctorName(doctorname);
        required.setSourcetype(sourcetype);

        String linkmanid = MapUtil.getString(params, "linkmanid");
        if (StringUtils.isNotBlank(linkmanid)) {
            // 去数据库中查找联系人信息
            List<ContactPeopleInfo> values = HospitalInfoService.getInstance().getContactsList(userInfo.getAlipayUserId(), linkmanid);

            if (values.size() <= 0) {
                throw new HospitalException("获取常用联系人失败");
            }
            for (ContactPeopleInfo per : values) {
                if (linkmanid.equals(per.getLinkmanid())) {
                    required.setPatientname(per.getName());
                    required.setPatientidcardno(per.getIdcardno());
                    required.setPatientphone(per.getPhone());
                    required.setPatientid(per.getPatientid());
                    required.setCardno(per.getCardno());
                    required.setPatientaddress(per.getAddress());
                    return required;
                }
            }
        }

        // 这里key要对应接口的参数名
        required.setPatientname(userInfo.getYhxm());
        required.setPatientidcardno(userInfo.getSfzh());
        required.setPatientphone(userInfo.getLxdh());
        required.setPatientid(userInfo.getBrid());
        required.setPatientaddress(userInfo.getLxdz());

        return required;
    }
}
