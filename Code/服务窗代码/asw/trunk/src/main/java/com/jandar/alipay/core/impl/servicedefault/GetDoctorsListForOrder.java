package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DoctorInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得预约医生列表
 * Created by yubj on 2016/4/13.
 */
public class GetDoctorsListForOrder {
    public Map<String, Object> getDoctorsListForOrderRequest(String departcode, String scheduledate) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("departcode", departcode);
        parameters.put("scheduledate", scheduledate);
        return parameters;
    }

    public List<DoctorInfo> getDoctorsListForOrderResponse(List<Map<String, String>> process) {
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        for (Map<String, String> map : process) {
            DoctorInfo dc = new DoctorInfo();
            dc.setCode(map.get("doctorcode"));
            dc.setName(map.get("doctorname"));
            dc.setPictureurl(map.get("pictureurl"));
            dc.setLevel(map.get("level"));
            dc.setRecommend(map.get("recommend"));
            dc.setAdept(map.get("adept"));
            dc.setReserve(map.get("reserve"));
            dc.setScheduledates(map.get("scheduledates"));
            dcinfolist.add(dc);
        }
        return dcinfolist;
    }
}
