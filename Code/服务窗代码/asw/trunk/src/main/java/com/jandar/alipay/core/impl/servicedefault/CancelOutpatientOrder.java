package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutpatientOrder;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 取消门诊预约
 * Created by yubj on 2016/4/15.
 */
public class CancelOutpatientOrder {

    public Map<String, Object> cancelOutpatientOrderRequest(String openid, String preengageseq) {
        Map<String, Object> params = new HashMap<>();
        params.put("openid", openid);
        params.put("preengageseq", preengageseq);
        return params;
    }

    public List<OutpatientOrder> cancelOutpatientOrderResponse(List<Map<String, String>> dataList) {
        List<OutpatientOrder> results = new ArrayList<>();
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            OutpatientOrder order = new OutpatientOrder(dataMap.get("preengageseq"),
                    dataMap.get("preengagedate"),
                    dataMap.get("preengagetime"),
                    dataMap.get("departname"),
                    dataMap.get("doctorname"),
                    dataMap.get("patientname"),
                    dataMap.get("preengageno"),
                    dataMap.get("place"));
            results.add(order);
        }
        return results;
    }
}
