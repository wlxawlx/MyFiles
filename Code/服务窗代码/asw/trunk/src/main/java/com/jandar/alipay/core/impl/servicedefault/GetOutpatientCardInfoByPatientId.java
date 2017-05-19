package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutPatientCardInfo;
import jodd.util.StringUtil;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 根据病人ID获得门诊卡信息
 * Created by yubj on 2016/4/13.
 */
public class GetOutpatientCardInfoByPatientId {

    public Map<String, Object> getOutpatientCardInfoByPatientIdRequest(String patientid) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("patientid", patientid);
        return parameters;

    }

    public OutPatientCardInfo getOutpatientCardInfoByPatientIdResponse(List<Map<String, String>> process) {
        OutPatientCardInfo opinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            Map<String, String> result = process.get(0);
            opinfo.setCardtype(result.get("bklx"));
            opinfo.setCardname(result.get("cardname"));
            opinfo.setCardno(result.get("bkhm"));
            opinfo.setIdcardno(result.get("sfzh"));
            opinfo.setPatientid(result.get("brid"));
            opinfo.setPatientname(result.get("brxm"));
            opinfo.setBirthday(result.get("birthday"));
            opinfo.setPhone(result.get("lxdh"));
//            opinfo.setBalance(result.get("balance"));
//            opinfo.setCost(result.get("cost"));
            if (StringUtil.isBlank(result.get("balance"))) {
                opinfo.setBalance("0.00");
            } else {
                opinfo.setBalance(result.get("balance"));
            }
            if (StringUtil.isBlank(result.get("cost"))) {
                opinfo.setCost("0.00");
            } else {
                opinfo.setCost(result.get("cost"));
            }
        }
        return opinfo;

    }
}
