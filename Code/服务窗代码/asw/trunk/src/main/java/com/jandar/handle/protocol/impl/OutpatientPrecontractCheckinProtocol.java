package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
/*
 * @author yubj
 * 门诊预约报到
 */

public class OutpatientPrecontractCheckinProtocol implements Protocol {

    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {

        String yylsh = MapUtil.getString(params, "orderid");
        if (StringUtils.isBlank(yylsh)) {
            throw new HospitalException("预约号不能为空");
        }
        boolean values = HospitalInfoService.getInstance().getoutpatientOrder(yylsh);
        if (values) {
            Map<String, String> result = new HashMap<String, String>();
            result.put("state", "1");
            return result;
        } else {
            throw new HospitalException("门诊预约报到失败");
        }
    }

}
