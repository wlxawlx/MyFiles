package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
/*
 * @author yubj
 * 
 *  门诊候诊信息
 */

public class OutpatientWaitingInfoProtocol implements Protocol {
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
    	 HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
    	 String pdlsh = MapUtil.getString(params, "pdlsh");
    	 OutpatientWaitingInfo values = handler.getOutpatientWaitingInfo(pdlsh);
        		
        if (values!=null) {
        	Map<String , String >result=new HashMap<>();
        	result.put("message", values.getMessage());
            return result;
        } else {
            throw new HospitalException("获取门诊候诊信息失败");
        }
    }
}
