package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutPatientCardInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 联系人门诊卡解绑
 * Created by yubj on 2016/4/13.
 */
public class UnbindContactOutpatientCard {

    public Map<String, Object> unbindContactOutpatientCardRequest(String openid, String linkmanid) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("linkmanid", linkmanid);
        return parameters;
    }

    public OutPatientCardInfo unbindContactOutpatientCardResponse(List<Map<String, String>> process) {
        OutPatientCardInfo cpinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            Map<String, String> result = process.get(0);
            cpinfo.setCardno(result.get("cardno"));
        }
        return cpinfo;
    }
}
