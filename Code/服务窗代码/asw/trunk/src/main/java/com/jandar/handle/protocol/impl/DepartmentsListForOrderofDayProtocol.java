package com.jandar.handle.protocol.impl;

import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.DepartmentInfo;
import com.jandar.alipay.hospital.Hospital;
import com.jandar.handle.protocol.Protocol;
/*
 * 当日预约挂号-科室列表
 */
public class DepartmentsListForOrderofDayProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		if (!Hospital.flag) {
			HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
			List<DepartmentInfo> values =handler.getDepartmentsListForOrderofDay();
			Hospital.getInstance().getSections(values);
		}

		return Hospital.getInstance().getSections(null);
	}

}
