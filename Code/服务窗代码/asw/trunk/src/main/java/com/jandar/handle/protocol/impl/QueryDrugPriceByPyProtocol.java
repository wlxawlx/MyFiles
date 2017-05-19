package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DrugPriceInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 药品价格-按拼音码或名称查询 - 查询基本功能
 * Created by yubj on 2016/4/11.
 */
public class QueryDrugPriceByPyProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String pydm = MapUtil.getString(params, "pydm");
        List<DrugPriceInfo> values = HospitalInfoService.getInstance().queryDrugPriceByPy(pydm);
        List<Map<String, String>> list = new ArrayList<Map<String, String>>();
        for (DrugPriceInfo map : values) {
            Map<String, String> result = new HashMap<String, String>();
            result.put("yplx", map.getYplx());
            result.put("ypmc", map.getYpmc());
            result.put("ypdw", map.getYpdw());
            result.put("ypgg", map.getYpgg());
            result.put("ypcd", map.getYpcd());
            result.put("ypjg", map.getYpjg());
            list.add(result);
        }
        return list;
    }
}
