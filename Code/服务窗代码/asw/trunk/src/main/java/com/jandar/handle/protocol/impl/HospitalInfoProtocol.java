package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.HospitalInfo;
import com.jandar.handle.protocol.Protocol;
/*
 * @author yubj
 * 
 * 医院信息
 */

public class HospitalInfoProtocol implements Protocol {
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
    	HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
    	HospitalInfo values = handler.getHospitalInfo("1");
        if (values!=null) {
           Map<String , String >result=new HashMap<String , String>();
           result.put("hospid", values.getHospid());
           result.put("yymc", values.getYymc());
           result.put("yyjc", values.getYyjc());
           result.put("yydj", values.getYydj());
           result.put("yydz", values.getYydz());
           result.put("yydh", values.getYydh());
           result.put("yyjj", values.getYyjj());
           result.put("jzxz", values.getJzxz());
           return result;
        } else {
            throw new HospitalException("获取医院信息失败");
        }
    }
}
