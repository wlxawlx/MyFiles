package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutPatientCardInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 本人门诊卡绑定
 * Created by yubj on 2016/4/13.
 */
public class BindOutpatientCard {

    public Map<String, Object> bindOutpatientCardRequest(String openid, String cardno, String patientid) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("cardno", cardno);
        parameters.put("patientid", patientid);
        return parameters;
    }

    public OutPatientCardInfo bindOutpatientCardResponse(List<Map<String, String>> process) {
        OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            Map<String, String> re = process.get(0);
            ocinfo.setCardtype(re.get("bklx"));
            ocinfo.setCardname(re.get("cardname"));
            ocinfo.setCardno(re.get("bkhm"));
        }
        return ocinfo;
    }
}
