package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DepartmentInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得预约科室列表
 * Created by yubj on 2016/4/13.
 */
public class GetDepartmentsListForOrder {

    public Map<String, Object> getDepartmentsListForOrderRequest(String departcode) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("departcode", departcode);
        return parameters;
    }

    public List<DepartmentInfo> getDepartmentsListForOrderResponse(List<Map<String, String>> process) {
        List<DepartmentInfo> deinfolist = new ArrayList<DepartmentInfo>();
        if (process.size() > 0) {
            for (Map<String, String> map : process) {
                DepartmentInfo deinfo = new DepartmentInfo();
                deinfo.setDepartcode(map.get("departcode"));
                deinfo.setDepartname(map.get("departname"));
                deinfo.setSecondcode(map.get("secondcode"));
                deinfo.setSecondname(map.get("secondname"));
                deinfo.setDescribe(map.get("describe"));

                deinfolist.add(deinfo);
            }
        }
        return deinfolist;
    }
}
