package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.BedQueryInfo;
import com.jandar.handle.protocol.Protocol;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 剩余床位查询 - 查询基本功能
 * Created by yubj on 2016/4/11.
 */
public class QuerySurplusBedProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        List<BedQueryInfo> values = HospitalInfoService.getInstance().querySurplusBed();
        List<Map<String, String>> list = new ArrayList<Map<String, String>>();
        for (BedQueryInfo bedQueryInfo : values) {
            Map<String, String> map = new HashMap<>();
            map.put("bqmc", bedQueryInfo.getBqmc());
            map.put("sycw", bedQueryInfo.getSycw());
            list.add(map);
        }
        return list;
    }
}
