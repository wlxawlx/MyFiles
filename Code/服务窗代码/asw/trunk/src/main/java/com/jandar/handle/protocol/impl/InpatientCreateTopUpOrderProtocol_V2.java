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

import java.util.HashMap;
import java.util.Map;

/**
 * Created by zhufengxiang on 2016-01-04.
 * 住院充值， 创建订单02版,可以给别人充
 */


public class InpatientCreateTopUpOrderProtocol_V2 implements Protocol {

    final static String m_orderName = "支付宝住院预缴";

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        String inhospitalId = MapUtil.getString(params, "inhospitalid");
        String money = MapUtil.getString(params, "money");
        if (StringUtil.isBlank(inhospitalId)) {
            throw new HospitalException("住院号码不能为空");
        }
        if (StringUtil.isBlank(money)) {
            throw new HospitalException("充值金额不能为空");
        }

        String patientName = MapUtil.getString(params, "patientname");
        if (StringUtil.isBlank(patientName)) {
            throw new HospitalException("病人姓名不能为空");
        }

        String patientIdCard = MapUtil.getString(params, "patientidcard");
        String tradeno = HospitalInfoService.getInstance().buildInhospitalRechargeOrder(inhospitalId, money, m_orderName, patientName, patientIdCard);
        if (StringUtil.isNotBlank(tradeno)) {
            Map<String, String> result = new HashMap<>();
            result.put("orderno", tradeno);
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.InHospitalOrder,
                    userInfo.getAlipayUserId(),
                    patientName,
                    tradeno,
                    m_orderName,
                    money,
                    MapUtil.getString(params, "showurl"),
                    null,
                    "",
                    "",
                    "",
                    inhospitalId);
            result.put("aliwappayurl", aliwappayurl);
//            result.put("orderno", orderno);
            return result;
        } else {
            throw new HospitalException("住院预缴-创建订单失败");
        }
    }
}
