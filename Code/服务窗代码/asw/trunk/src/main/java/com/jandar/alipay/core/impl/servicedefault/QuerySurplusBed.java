package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.BedQueryInfo;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 剩余床位查询 - 查询基本功能
 * Created by yubj on 2016/4/12.
 */
public class QuerySurplusBed {
    public List<BedQueryInfo> querySurplusBedResponse(List<Map<String, String>> values) {
        List<BedQueryInfo> result = new ArrayList<BedQueryInfo>();
        for (Map<String, String> map : values) {
            BedQueryInfo bedQueryInfo = new BedQueryInfo();
            bedQueryInfo.setBqmc(map.get("bqmc"));
            bedQueryInfo.setSycw(map.get("sycw"));
            result.add(bedQueryInfo);
        }
        return result;
    }
}
