package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import com.jandar.util.StringUtil;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by zhufengxiang on 2016-01-04.
 * 住院充值-取消订单
 */
public class InpatientCancelTopUpOrderProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        String orderno = MapUtil.getString(params, "orderno");
        if (StringUtil.isBlank(orderno)) {
            throw new HospitalException("订单流水号不能为空");
        }

        UserInfo info = ServiceContext.getHospitalUserInfo();

        String tradeno = HospitalInfoService.getInstance().cancelInhospitalRechargeOrder(info.getAlipayUserId(), null, null, orderno);
        if (StringUtil.isNotBlank(tradeno)) {
           Map<String , Object >result=new HashMap<String , Object>();
           result.put("state", 1);
           return result;
        } else {
            throw new HospitalException("住院充值-取消订单失败");
        }
    }
}
