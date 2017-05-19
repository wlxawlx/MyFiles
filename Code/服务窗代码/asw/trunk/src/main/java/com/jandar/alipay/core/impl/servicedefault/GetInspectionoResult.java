package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.InspectionoResult;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得检查单结果信息
 * Created by yubj on 2016/4/18.
 */
public class GetInspectionoResult {

    public Map<String, Object> getInspectionoResultRequest(String doctadviseno) {
        Map<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);
        return params;
    }

    public InspectionoResult getInspectionoResultResponse(List<Map<String, String>> dataList) {
        InspectionoResult result = null;
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            result = new InspectionoResult(dataMap.get("studyresult"),
                    dataMap.get("diagresult"));
        }
        return result;
    }
}
