package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DoctorScheduleInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得医生排班信息
 * Created by yubj on 2016/4/13.
 */
public class GetDoctorSchedulForOrder {
    public Map<String, Object> getDoctorSchedulForOrderRequest(String doctorcode, String departcode, String scheduledate) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("doctorcode", doctorcode);
        parameters.put("departcode", departcode);
        parameters.put("scheduledate", scheduledate);
        return parameters;

    }

    public List<DoctorScheduleInfo> getDoctorSchedulForOrderResponse(List<Map<String, String>> process) {
        List<DoctorScheduleInfo> dslist = new ArrayList<DoctorScheduleInfo>();
        for (Map<String, String> map : process) {
            DoctorScheduleInfo ds = new DoctorScheduleInfo();
            ds.setScheduleseq(map.get("scheduleseq"));
            ds.setDepartcode(map.get("departcode"));
            ds.setDepartname(map.get("departname"));
            ds.setDoctorcode(map.get("doctorcode"));
            ds.setDoctorname(map.get("doctorname"));
            ds.setSpecial(map.get("special"));
            ds.setRemain(map.get("remain"));
            ds.setTotal(map.get("total"));
            ds.setAddress(map.get("address"));
            ds.setScheduledate(map.get("scheduledate"));
            ds.setShiftcode(map.get("shiftcode"));
            ds.setShiftname(map.get("shiftname"));
            ds.setFee(map.get("fee"));

            dslist.add(ds);
        }
        return dslist;
    }
}
