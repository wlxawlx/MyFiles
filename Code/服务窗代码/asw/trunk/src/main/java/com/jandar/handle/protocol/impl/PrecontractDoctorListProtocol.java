package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DoctorInfo;
import com.jandar.alipay.hospital.Doctor;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author yubj
 * 
 * 预约医生列表
 */
public class PrecontractDoctorListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	String departcode=MapUtil.getString(params, "departid");
    	String scheduledate=MapUtil.getString(params, "scheduledate");
    	if(scheduledate==null){
    		scheduledate="";
    	}
        System.out.println("PrecontractDoctorListProtocol:departcode:"+departcode);
    	List<DoctorInfo> values = HospitalInfoService.getInstance().getDoctorsListForOrder(departcode, scheduledate);
        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<String, Object>();
            List<Doctor> doctors = new ArrayList<Doctor>();
            for(DoctorInfo per : values) {
                Doctor doctor = new Doctor(per.getCode(),per.getName(),per.getLevel(),
                        Integer.valueOf(per.getReserve()));
                doctors.add(doctor);
            }
            result.put("departid", departcode);
            result.put("list", doctors);
            return result;
        } else {
            throw new HospitalException("获取预约医生列表失败");
        }
    }
}
