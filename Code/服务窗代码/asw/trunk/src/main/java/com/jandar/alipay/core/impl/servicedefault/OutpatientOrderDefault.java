package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutpatientOrderReponse;
import com.jandar.alipay.core.struct.OutpatientOrderRequest;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 门诊预约   （三院该功能不支持）
 * Created by yubj on 2016/4/15.
 */
public class OutpatientOrderDefault {
    public Map<String, Object> outpatientOrderDefaultRequest(OutpatientOrderRequest outpatientOrderRequest) {
        Map<String, Object> params = new HashMap<>();
        params.put("doctorcode", outpatientOrderRequest.getDoctorcode());
        params.put("openid", outpatientOrderRequest.getOpenid());
        params.put("orderseq", outpatientOrderRequest.getOrderno());
        params.put("ordertime", outpatientOrderRequest.getOrdertime());
        params.put("orderendtime", outpatientOrderRequest.getOrderendtime());
        params.put("shiftcode", outpatientOrderRequest.getShiftcode());
        params.put("patientid", outpatientOrderRequest.getPatientid());
        params.put("patientaddress", outpatientOrderRequest.getPatientaddress());
        params.put("patientidcardno", outpatientOrderRequest.getPatientidcardno());
        params.put("patientname", outpatientOrderRequest.getPatientname());
        params.put("patientphone", outpatientOrderRequest.getPatientphone());
        params.put("scheduleseq", outpatientOrderRequest.getScheduleseq());
        params.put("sourcetype", outpatientOrderRequest.getSourcetype());
        return params;
    }

    public OutpatientOrderReponse outpatientOrderDefaultResponse(List<Map<String, String>> dataList) {
        if (dataList.size() > 0) {
            Map<String, String> dataMap = dataList.get(0);
            OutpatientOrderReponse result = new OutpatientOrderReponse();
            result.setDepartcode(dataMap.get("departcode"));
            result.setDepartname(dataMap.get("departname"));
            result.setDoctorcode(dataMap.get("doctorcode"));
            result.setDoctorname(dataMap.get("doctorname"));
            result.setPatientname(dataMap.get("patientname"));
            result.setPlace(dataMap.get("place"));
            result.setPreengagedate(dataMap.get("preengagedate"));
            result.setPreengageno(dataMap.get("preengageno"));
            result.setPreengageseq(dataMap.get("preengageseq"));
            result.setPreengagetime(dataMap.get("preengagetime"));
            return result;
        }

        return null;

    }
}
