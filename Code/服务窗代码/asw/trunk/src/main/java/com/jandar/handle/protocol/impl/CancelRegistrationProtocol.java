package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import java.util.HashMap;
import java.util.Map;

/**
 * 挂号退号
 * <p/>
 * Created by yubj on 2016/5/18.
 */
public class CancelRegistrationProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String thbs = MapUtil.getString(params, "thbs");
        boolean values = HospitalInfoService.getInstance().cancelRegistration(thbs);
        Map<String, String> result = new HashMap<String, String>();
        if (values) {
            result.put("state", "1");
        } else {
            result.put("state", "0");
        }
        return result;
    }
}
