package com.jandar.alipay.core.impl.servicedefault;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 本人门诊卡解绑
 * Created by yubj on 2016/4/13.
 */
public class UnbindOutpatientCard {

    public Map<String, Object> unbindOutpatientCardRequest(String openid) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        return parameters;
    }

    public boolean unbindOutpatientCardResponse(List<Map<String, String>> process) {
        return process != null;
    }
}
