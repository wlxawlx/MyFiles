package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutpatientOrderHistory;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 门诊预约历史
 * Created by yubj on 2016/4/15.
 */
public class OutpatientOrderHistoryDefault {
//    public Map<String, Object> outpatientOrderHistoryDefaultRequest(String openid) throws HospitalException {
//        Map<String, Object> params = new HashMap<>();
////        UserInfo info = ServiceContext.getHospitalUserInfo();
//        params.put("openid",openid);
//        return params;
//    }

    public List<OutpatientOrderHistory> outpatientOrderHistoryDefaultResponse(List<Map<String, String>> dataList) {
        List<OutpatientOrderHistory> results = new ArrayList<>();
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            String preengageState = dataMap.get("preengagestate");
            try {
                preengageState = String.valueOf(Integer.valueOf(preengageState) - 1);
            } catch (Exception ignored) {
            }
            String preengagedate=dataMap.get("preengagedate");
            String preengageTime =dataMap.get("preengagetime");
            OutpatientOrderHistory history = new OutpatientOrderHistory();
            history.setPreengageseq(dataMap.get("preengageseq"));
            history.setPreengagedate(preengagedate);
            history.setPreengagetime(preengageTime);
            history.setDepartcode(dataMap.get("departcode"));
            history.setDepartname(dataMap.get("departname"));
            history.setDoctorcode(dataMap.get("doctorcode"));
            history.setDoctorname(dataMap.get("doctorname"));
            history.setPatientname(dataMap.get("patientname"));
            history.setPatientidcardno(dataMap.get("patientidcardno"));
            history.setPatientphone(dataMap.get("patientphone"));
            history.setPatientaddress(dataMap.get("patientaddress"));
            history.setPreengageno(dataMap.get("preengageno"));
            history.setPlace(dataMap.get("place"));
            history.setFee(dataMap.get("fee"));
            history.setPreengagestate(preengageState);
            results.add(history);
        }
        return results;
    }
}
