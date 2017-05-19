package com.jandar.handle.protocol.impl;

import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DepartmentInfo;
import com.jandar.alipay.hospital.Hospital;
import com.jandar.handle.protocol.Protocol;

/*
 * @author yubj
 * 
 * 预约科室列表
 */

public class PrecontractSectionListProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {

		if (!Hospital.flag) {
			List<DepartmentInfo> values = HospitalInfoService.getInstance().getDepartmentsListForOrder(null);
			Hospital.getInstance().getSections(values);
		}

		return Hospital.getInstance().getSections(null);

	}
}
