package com.jandar.handle.protocol.impl;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import com.jandar.util.StringUtil;
import org.apache.commons.lang3.StringUtils;

import java.util.HashMap;
import java.util.Map;

/*
 * 门诊充值-创建订单_他人
 */
public class OutPatientCreateOrderProtocol_Others implements Protocol {
    final static String m_orderName = "支付宝门诊预存";

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        String patientid = MapUtil.getString(params, "patientid");
        String patientName = MapUtil.getString(params, "patientname");
        String patientIdCard = MapUtil.getString(params, "patientidcard");
        String cardno = MapUtil.getString(params, "cardno");
        String money = MapUtil.getString(params, "money");
        if (StringUtils.isBlank(money)) {
            throw new HospitalException("充值金额不能为空");
        }
        String orderNo = HospitalInfoService.getInstance().buildOutpatientRechargeOrder(
                info.getAlipayUserId(),
                patientName,
                patientIdCard,
                cardno,
                patientid,
                m_orderName,
                money);
        if (!StringUtil.isBlank(orderNo)) {
            Map<String, String> result = new HashMap<>();
            result.put("orderno", orderNo);
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.OutpatientOrder,
                    userInfo.getAlipayUserId(),
                    patientName,
                    orderNo,
                    m_orderName,
                    money,
                    MapUtil.getString(params, "showurl"),
                    null,
                    "",
                    "",
                    "",
                    patientid);
            System.out.println(aliwappayurl);
            result.put("aliwappayurl", aliwappayurl);
            return result;
        } else {
            throw new HospitalException("门诊预充——创建订单失败");
        }
    }
}
