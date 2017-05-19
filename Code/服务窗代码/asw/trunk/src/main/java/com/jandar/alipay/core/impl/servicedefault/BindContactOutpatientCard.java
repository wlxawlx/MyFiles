package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutPatientCardInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 联系人门诊卡绑定
 * Created by yubj on 2016/4/13.
 */
public class BindContactOutpatientCard {

    public Map<String, Object> bindContactOutpatientCardRequest(String openid, String linkmanid, String cardno,
                                                                String patientid){
        Map<String, Object> parameter = new HashMap<String, Object>();
        parameter.put("openid", openid);
        parameter.put("linkmanid", linkmanid);
        parameter.put("cardno", cardno);
        parameter.put("patientid", patientid);
        return parameter;
    }

    public OutPatientCardInfo bindContactOutpatientCardResponse(List<Map<String, String>> process,String patientid ){
        OutPatientCardInfo ocinfo = new OutPatientCardInfo();
        if (process.size() > 0) {
            Map<String, String> result = process.get(0);
            ocinfo.setCardtype(result.get("cardtype"));
            ocinfo.setCardname(result.get("cardname"));
            ocinfo.setCardno(result.get("cardno"));
            ocinfo.setPatientid(patientid);
        }
        return ocinfo;
    }
}
