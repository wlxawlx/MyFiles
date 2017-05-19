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
 * 医生查询
 */

public class DoctorQueryProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	String namepy=MapUtil.getString(params, "pydm");
    	String pageindex=MapUtil.getString(params, "pageindex");
    	String pagesize=MapUtil.getString(params, "pagesize");
        if(pageindex.indexOf(".") != -1){
            pageindex=pageindex.substring(0,pageindex.indexOf("."));
        }
        if(pagesize.indexOf(".") != -1){
            pagesize=pagesize.substring(0,pagesize.indexOf("."));
        }
    	List<DoctorInfo> values = HospitalInfoService.getInstance().getDoctorInfoByPy(namepy, pageindex, pagesize);
    	Map<String , Object > over=new HashMap<String ,Object>();
    	List<Map<String , Object >>list=new ArrayList<Map<String , Object>>();
        for(DoctorInfo info:values) {
           Map<String , Object > result=new HashMap<String,Object>();
           result.put("ksmc", info.getDepartname());
           result.put("ysxb", info.getSex());
           result.put("ksbm", info.getDepartcode());
           result.put("ysbm", info.getCode());
           result.put("zcmc", info.getLevel());
           result.put("ysxm", info.getName());
           result.put("yysl", info.getReserve());
           list.add(result);
        } 
        over.put("list", list);
        return list;
    }
}
