package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DoctorScheduleInfo;
import com.jandar.alipay.hospital.Schedule;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
/*
 * @author yubj
 * 
 * 医生排班表0.1
 */

public class DoctorScheduleProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	String doctorid=MapUtil.getString(params, "doctorid");
        String departcode=MapUtil.getString(params, "departcode");
        if(departcode==null){
        	departcode=null;
        }
        String scheduledate=MapUtil.getString(params, "scheduledate");
        if(scheduledate==null){
        	scheduledate=null;
        }
    	List<DoctorScheduleInfo> values = HospitalInfoService.getInstance().getDoctorSchedulForOrder(doctorid, departcode, scheduledate);
        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<>();
            result.put("doctorid", params.get("doctorid"));
            List<Schedule> list = new ArrayList<>();
            for (DoctorScheduleInfo per : values) {
                Schedule schedule = new Schedule(
//                        per.get("pblsh"), per.get("ksbm"), per.get("ksmc"), per.get("ysbm"), per.get("ysxm"),
//                        per.get("txbz"), per.get("kyrs"), per.get("yyzs"), per.get("jzdd"), per.get("pbrq"),
//                        per.get("bcbm"), per.get("bcmc"), per.get("ghf")
                		per.getScheduledate(),per.getDepartcode(),per.getDepartname(),per.getDoctorcode(),per.getDoctorname(),
                		per.getSpecial(),per.getRemain(),per.getTotal(),per.getAddress(),per.getScheduledate(),
                		per.getShiftcode(),per.getShiftname(),per.getFee()
                		
                );
                list.add(schedule);
            }
            result.put("list", list);
            return result;
        } else {
            throw new HospitalException("获取医生排班表失败");
        }
    }
}
