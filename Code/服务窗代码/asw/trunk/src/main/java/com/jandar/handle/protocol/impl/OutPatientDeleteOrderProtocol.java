package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang3.StringUtils;

import java.util.HashMap;
import java.util.Map;

public class OutPatientDeleteOrderProtocol implements Protocol {

    /**
     * @author zhou
     * 门诊充值---取消订单 F002
     */
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
    	System.out.println("OutPatientDeleteOrderHanlderprocess");
        String orderno = MapUtil.getString(params, "orderno");
        if (StringUtils.isBlank(orderno)) {
            throw new HospitalException("订单流水号不能为空");
        }

        UserInfo info = ServiceContext.getHospitalUserInfo();
        String tradeno = HospitalInfoService.getInstance().cancelOutpatientRechargeOrder(info.getAlipayUserId(), null, null, orderno);
        if (StringUtils.isNotBlank(tradeno)) {
            Map<String, String> map = new HashMap<>();
            map.put("state", "1");
            return map;
        } else {
            throw new HospitalException("门诊充值——取消订单失败");
        }
    }
}
