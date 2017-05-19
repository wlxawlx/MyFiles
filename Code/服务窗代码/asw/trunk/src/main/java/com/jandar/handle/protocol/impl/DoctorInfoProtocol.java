package com.jandar.handle.protocol.impl;

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
 * 医生信息
 */

public class DoctorInfoProtocol implements Protocol {
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
    	String code=MapUtil.getString(params, "ysbm");
    	List<DoctorInfo> values = HospitalInfoService.getInstance().getDoctorInfoByCode(code);
        if (values.size() > 0) {
        	Map<String , String > result=new HashMap<String,String >();
        	DoctorInfo info=values.get(0);
        	result.put("ysjs", info.getRecommend());
        	result.put("ksmc", info.getDepartname());
        	result.put("scjb", info.getAdept());
        	result.put("ysxb", info.getSex());
        	result.put("ksbm", info.getDepartcode());
        	result.put("zcmc", info.getLevel());
        	result.put("ysxm", info.getName());
            return result;
        } else {
            throw new HospitalException("获取医生信息失败");
        }
    }
}
