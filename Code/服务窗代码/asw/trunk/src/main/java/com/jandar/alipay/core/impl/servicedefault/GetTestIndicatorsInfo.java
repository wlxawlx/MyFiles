package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.TestIndicator;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得化验单指标项信息列表
 * Created by yubj on 2016/4/18.
 */
public class GetTestIndicatorsInfo {

    public Map<String, Object> getTestIndicatorsInfoRequest(String doctadviseno) {
        Map<String, Object> params = new HashMap<>();
        params.put("doctadviseno", doctadviseno);
        return params;
    }

    public List<TestIndicator> getTestIndicatorsInfoResponse(List<Map<String, String>> dataList) {
        List<TestIndicator> results = new ArrayList<>();
        // 若dataList无信息，则results会返回Null,若有信息,则遍历，并加入到results中
        for (Map<String, String> dataMap : dataList) {
//            TestIndicator indicator = new TestIndicator(dataMap.get("jylx"),
//                    dataMap.get("xmmc"),
//                    dataMap.get("result"),
//                    dataMap.get("hint"),
//                    dataMap.get("jkfw"),
//                    dataMap.get("xmdw"));
            TestIndicator indicator = new TestIndicator();
            indicator.setJylx(dataMap.get("jylx"));
            indicator.setXmmc( dataMap.get("xmmc"));
            indicator.setResult( dataMap.get("result"));
            indicator.setHint( dataMap.get("hint"));
            indicator.setCkfw( dataMap.get("jkfw"));
            indicator.setXmdw(dataMap.get("xmdw"));
            results.add(indicator);
        }
        return results;
    }
}
