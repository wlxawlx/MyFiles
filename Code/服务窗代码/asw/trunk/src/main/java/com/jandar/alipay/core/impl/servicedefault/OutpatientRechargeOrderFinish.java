package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.RechargeOrderFinishInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 门诊充值订单完成并到账
 * Created by yubj on 2016/4/20.
 */
public class OutpatientRechargeOrderFinish {

    public Map<String, Object> outpatientRechargeOrderFinishRequest(RechargeOrderFinishInfo rechargeOrderFinishInfo) {
        Map<String, Object> params = new HashMap<>();
        params.put("money", rechargeOrderFinishInfo.getMoney());
        params.put("openid", rechargeOrderFinishInfo.getOpenid());
        params.put("patientid", rechargeOrderFinishInfo.getUserdata());
        params.put("patientname", rechargeOrderFinishInfo.getPatientname());
        params.put("paymentparameters", rechargeOrderFinishInfo.getPaymentparameters());
        params.put("paymenttradeno", rechargeOrderFinishInfo.getPaymenttradeno());
        params.put("tradeno", rechargeOrderFinishInfo.getTradeno());
        return params;
    }

    public String outpatientRechargeOrderFinishResponse(List<Map<String, String>> dataList) {
        String result = null;
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = dataMap.get("tradeno");
        }
        return result;
    }
}
