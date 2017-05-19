package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DrugPriceInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 药品价格-按拼音码或名称查询 - 查询基本功能
 * Created by yubj on 2016/4/12.
 */
public class QueryDrugPriceByPy {
    public Map<String, Object> qqueryDrugPriceByPyRequest(String pydm) {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("pydm", pydm);
        return parameters;
    }

    public List<DrugPriceInfo> queryDrugPriceByPyResponse(List<Map<String, String>> values) {
        List<DrugPriceInfo> list = new ArrayList<DrugPriceInfo>();
        for (Map<String, String> result : values) {
            DrugPriceInfo drugPriceInfo = new DrugPriceInfo();
            drugPriceInfo.setYplx(result.get("yplx"));
            drugPriceInfo.setYpmc(result.get("ypmc"));
            drugPriceInfo.setYpdw(result.get("ypdw"));
            drugPriceInfo.setYpgg(result.get("ypgg"));
            drugPriceInfo.setYpcd(result.get("ypcd"));
            drugPriceInfo.setYpjg(result.get("ypjg"));
            list.add(drugPriceInfo);
        }
        return list;
    }
}
