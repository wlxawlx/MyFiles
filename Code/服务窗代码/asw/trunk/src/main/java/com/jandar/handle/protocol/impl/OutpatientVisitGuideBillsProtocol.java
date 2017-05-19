package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutpatientVisitGuideBills;
import com.jandar.alipay.util.MapUtil;


/*
 * @author jinliangjin
 * 
 * 就诊信息-指引单（取药）
 */
public class OutpatientVisitGuideBillsProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		if(StringUtils.isBlank(MapUtil.getString(params,"jzxh" ))){
			throw new HospitalException("就诊序号不能为空");
		}
//		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
//		OutpatientVisitGuideBills value = handler.getOutpatientVisitGuideBillsDetail(MapUtil.getString(params,"jzxh" ));
		OutpatientVisitGuideBills value =HospitalInfoService.getInstance().getOutpatientVisitGuideBillsDetail(MapUtil.getString(params,"jzxh" ));
		if (value!= null) {

				Map<String, Object> item = new HashMap<>();
				
				item.put("cfxh", value.getCfxh());
				item.put("fphm", value.getFphm());
				item.put("kfrq", value.getKfrq());
				item.put("ksmc", value.getKsmc());
				item.put("zynr", value.getZynr());
				item.put("zywz", value.getZywz());
				item.put("ysxm", value.getYsxm());
				item.put("zjje", value.getZjje());

			return item;
				
		}else{
			throw new HospitalException("没有找到该就诊记录");
		}
	}
}
