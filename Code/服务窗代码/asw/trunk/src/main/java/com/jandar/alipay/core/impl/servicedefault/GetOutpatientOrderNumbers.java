package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutpatientOrderNumber;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得门诊预约号源
 * Created by yubj on 2016/4/15.
 */
public class GetOutpatientOrderNumbers {

    public Map<String, Object> getOutpatientOrderNumbersRequest(String doctorcode, String scheduleseq, String shiftcode) {
        Map<String, Object> params = new HashMap<>();
        params.put("doctorcode", doctorcode);
        params.put("scheduleseq", scheduleseq);
        params.put("shiftcode", shiftcode);
        return params;
    }

    public List<OutpatientOrderNumber> getOutpatientOrderNumbersResponse(List<Map<String, String>> dataList) {
        List<OutpatientOrderNumber> results = new ArrayList<>();
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
            OutpatientOrderNumber number = new OutpatientOrderNumber();
            number.setOrderno(dataMap.get("orderno"));
            number.setOrderseq(dataMap.get("orderseq"));
            number.setOrdertime(dataMap.get("ordertime"));
            number.setShiftcode(dataMap.get("shiftcode"));
            number.setOrderendtime(dataMap.get("orderendtime"));
            results.add(number);
        }

        return results;
    }
}
