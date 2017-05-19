package com.jandar.alipay.core.impl.servicedefault;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 取消门诊充值订单
 * Created by yubj on 2016/4/20.
 */
public class CancelOutpatientRechargeOrder {

    public Map<String, Object> cancelOutpatientRechargeOrderRequest(String openId, String patientName, String patientId, String tradeno) {
        Map<String, Object> params = new HashMap<>();
        params.put("openid", openId);
        params.put("patientid", patientId);
        params.put("patientname", patientName);
        params.put("tradeno", tradeno);
        return params;
    }

    public String cancelOutpatientRechargeOrderResponse(List<Map<String, String>> dataList) {
        String result = null;
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = dataMap.get("tradeno");
        }
        return result;
    }
}
