package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.Inspection;
import com.jandar.alipay.core.struct.OutpatientOrder;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得检查单列表
 * Created by yubj on 2016/4/18.
 */
public class GetInspectionsList {

    public Map<String, Object> getInspectionsListRequest(String name, String idcardno) {
        Map<String, Object> params = new HashMap<>();
        params.put("name", name);
        params.put("idcardno", idcardno);
        return params;
    }

    public List<Inspection> getInspectionsListResponse(List<Map<String, String>> dataList) {
        List<Inspection> results = new ArrayList<>();
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            Inspection inspection = new Inspection(dataMap.get("doctadviseno"),
                    dataMap.get("examinaim"),
                    dataMap.get("requesttime") + " 00:00:00",
                    dataMap.get("requester"));
            results.add(inspection);
        }
        return results;
    }
}
