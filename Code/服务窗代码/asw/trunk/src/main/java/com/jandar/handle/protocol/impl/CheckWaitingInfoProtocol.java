package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/**
 * 检查候诊信息
 * 
 * @param pdlsh 排队流水号
 * 
 * @return hzxx 候诊信息
 */
public class CheckWaitingInfoProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		// TODO Auto-generated method stub
		String pdlsh=MapUtil.getString(params, "pdlsh");
		if(StringUtils.isBlank(pdlsh)){
            throw new HospitalException("缺少排队号");
		}
		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
		OutpatientWaitingInfo values=handler.checkWaitingInfo(pdlsh);
		if(values!=null){
			Map<String , String >result=new HashMap<String, String >();
			result.put("message", values.getHzxx());
			return result;
		}else{
			throw new HospitalException("候诊信息为空");
		}
	}

}
