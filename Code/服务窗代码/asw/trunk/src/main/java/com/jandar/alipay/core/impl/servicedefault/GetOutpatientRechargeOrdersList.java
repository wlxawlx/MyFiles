package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.RechargeOrderHistoryInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得门诊充值订单列表
 * Created by yubj on 2016/4/20.
 */
public class GetOutpatientRechargeOrdersList {
    public Map<String, Object> getOutpatientRechargeOrdersListRequest(String openid) {
        Map<String, Object> params = new HashMap<>();
        params.put("openid", openid);
        return params;
    }

    public List<RechargeOrderHistoryInfo> getOutpatientRechargeOrdersListResponse(List<Map<String, String>> dataList) {
        List<RechargeOrderHistoryInfo> result = new ArrayList<>();
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            RechargeOrderHistoryInfo info = new RechargeOrderHistoryInfo();
            info.setOpenid(dataMap.get("openid"));
            info.setPaymenttradeno(dataMap.get("paymenttradeno"));
            info.setTradeno(dataMap.get("tradeno"));
            info.setStatus("2".equals(dataMap.get("status")) ? "1" : "0");
            info.setSubject(dataMap.get("subject"));
            info.setMoney(dataMap.get("money"));
            info.setCtime(dataMap.get("ctime"));
            info.setPaytime(dataMap.get("paytime"));
            info.setRtntime(dataMap.get("rtntime"));
            info.setPatientid(dataMap.get("patientid"));
            info.setPatientname(dataMap.get("patientname"));
            info.setPatientidcardno(dataMap.get("patientidcardno"));
            result.add(info);
        }
        return result;
    }
}
