package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.SchedulingInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得科室排班信息（某一天科室下还有所有医生还有多少号源）（三院该功能不支持)
 * Created by yubj on 2016/4/13.
 */
public class GetDepartmentSchedulForOrder {

    public Map<String, Object> getDepartmentSchedulForOrderRequest(String departcode) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("departcode", departcode);
        return parameters;
    }

    public List<SchedulingInfo> getDepartmentSchedulForOrderResponse(List<Map<String, String>> process) {
        List<SchedulingInfo> scinfo = new ArrayList<SchedulingInfo>();
        for (Map<String, String> map : process) {
            SchedulingInfo sc = new SchedulingInfo();
            sc.setScheduledate(map.get("scheduledate"));
            sc.setRemain(map.get("remain"));
            sc.setTotal(map.get("total"));
            scinfo.add(sc);
        }
        return scinfo;
    }
}
