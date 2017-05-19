package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.InspectionInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得检查单信息
 * Created by yubj on 2016/4/18.
 */
public class GetInspectionInfo {
    public Map<String, Object> getInspectionInfoRequest(String doctadviseno) {
        Map<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);
        return params;
    }

    public InspectionInfo getInspectionInfoResponse(List<Map<String, String>> dataList) {
        InspectionInfo result = null;
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = new InspectionInfo(dataMap.get("doctadviseno"),
                    dataMap.get("requesttime"),
                    dataMap.get("requester"),
                    dataMap.get("executetime"),
                    dataMap.get("executer"),
                    dataMap.get("receivetime"),
                    dataMap.get("receiver"),
                    dataMap.get("stayhospitalmode"),
                    dataMap.get("patientid"),
                    dataMap.get("section"),
                    dataMap.get("bedno"),
                    dataMap.get("patientname"),
                    dataMap.get("sex"),
                    dataMap.get("age"),
                    dataMap.get("diagnostic"),
                    dataMap.get("sampletype"),
                    dataMap.get("examinaim"),
                    dataMap.get("requestmode"),
                    dataMap.get("checker"),
                    dataMap.get("checktime"),
                    dataMap.get("csyq"),
                    dataMap.get("profiletest"));
        }
        return result;
    }
}
