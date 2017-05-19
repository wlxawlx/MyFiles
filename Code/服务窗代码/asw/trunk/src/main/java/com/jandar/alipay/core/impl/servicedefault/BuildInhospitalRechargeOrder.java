package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.UserInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 创建住院充值订单
 * Created by yubj on 2016/4/20.
 */
public class BuildInhospitalRechargeOrder {

    public Map<String, Object> buildInhospitalRechargeOrderRequest(String inpatientNo, String money,String openid, String subject, String patientName, String parientIdCardNo) throws HospitalException {
        Map<String, Object> params = new HashMap<>();
        params.put("inpatientno", inpatientNo);
        params.put("money", money);
        params.put("openid", openid);
        params.put("patientidcardno", parientIdCardNo);
        params.put("patientname", patientName);
        params.put("subject", subject);
        return params;
    }

    public String buildInhospitalRechargeOrderResponse(List<Map<String, String>> dataList) {
        String result = null;
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = dataMap.get("tradeno");
        }
        return result;
    }
}
