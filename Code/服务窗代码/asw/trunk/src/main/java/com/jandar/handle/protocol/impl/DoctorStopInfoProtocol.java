package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DoctorInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author yubj
 * 
 * 医生停诊信息
 */

public class DoctorStopInfoProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	String  pageindex  =MapUtil.getString(params, "pageindex");
    	String   pagesize=MapUtil.getString(params, "pagesize");
    	List<DoctorInfo> values = HospitalInfoService.getInstance().getDoctorsStopInfo(pageindex, pagesize);
        if (values.size() > 0) {
        	List<Map<String , String >> list=new ArrayList<Map<String , String >>();
        	for(DoctorInfo map:values){
	            Map<String , String >result=new HashMap<String , String>();
	            result.put("ksmc", map.getDepartname());
	            result.put("ksbm", map.getDepartcode());
	            result.put("ysbm", map.getCode());
	            result.put("zcmc", map.getLevel());
	            result.put("bcmc", map.getStopshift());
	            result.put("tzrq", map.getStopdate());
	            result.put("ysxm", map.getName());
	            list.add(result);
        	}
        	return list;
        } else {
            throw new HospitalException("获取医生停诊信息失败");
        }
    }
}
