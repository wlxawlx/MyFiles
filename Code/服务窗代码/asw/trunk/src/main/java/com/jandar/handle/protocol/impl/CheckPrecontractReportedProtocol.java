package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/**
 * 检查预约报到
 * 
 * @param yhid 用户ID
 * 
 * @param brid 病人ID
 * 
 * @param yylsh 预约流水号
 * 
 * @return 检查是否预约报到
 */
public class CheckPrecontractReportedProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		// TODO Auto-generated method stub
		String yylsh=MapUtil.getString(params, "yylsh");
		if(StringUtils.isBlank(yylsh)){
            throw new HospitalException("缺少预约号");
		}
		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
		boolean values=handler.checkPrecontractReported(null, null, yylsh);
		if(values){
			Map<String , String >map=new HashMap<>();
			map.put("state", "1");
			return map;
		}else{
			throw new HospitalException("还未预约报到");
		}
	}

}
