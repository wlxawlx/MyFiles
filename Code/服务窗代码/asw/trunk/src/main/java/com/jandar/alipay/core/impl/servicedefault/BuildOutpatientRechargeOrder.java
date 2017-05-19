package com.jandar.alipay.core.impl.servicedefault;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 创建门诊充值订单
 * Created by yubj on 2016/4/20.
 */
public class BuildOutpatientRechargeOrder {

    public Map<String, Object> buildOutpatientRechargeOrderRequest(String openid, String patientname, String patientidcardno
            , String cardno, String patientid, String subject, String money) {
        Map<String, Object> params = new HashMap<>();
        params.put("cardno", cardno);
        if (money == null) {
            params.put("money", "00.00");
        } else {
            params.put("money", money);
        }
        params.put("openid", openid);
        params.put("patientid", patientid);
        params.put("patientidcardno", patientidcardno);
        params.put("patientname", patientname);
        params.put("subject", subject);
        return params;
    }

    public Map<String, Object> buildOutpatientRechargeOrderRequest(String openid, String patientname, String patientidcardno
            , String cardno, String patientid, String subject, String money,String preengageseq,String sourcetype) {
        Map<String, Object> params = new HashMap<>();
        params.put("cardno", cardno);
        if (money == null) {
            params.put("money", "00.00");
        } else {
            params.put("money", money);
        }
        params.put("openid", openid);
        params.put("patientid", patientid);
        params.put("patientidcardno", patientidcardno);
        params.put("patientname", patientname);
        params.put("subject", subject);
        params.put("preengageseq", preengageseq);
        params.put("sourcetype", 2);
        return params;
    }

    public String buildOutpatientRechargeOrderResponse(List<Map<String, String>> dataList) {
        if (dataList.size() > 0) {
            return dataList.get(0).get("tradeno");
        }
        return null;
    }
}
