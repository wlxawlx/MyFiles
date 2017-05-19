package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.MedicineInfoTake;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;

/*
 * @author jinliangjin
 * 
 * 取药信息。
 */
public class MedicineInfoTakeProtocol implements Protocol {
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		UserInfo info = ServiceContext.getHospitalUserInfo();
		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
		List<MedicineInfoTake> values = handler.getTakeMedicineInfoDetail(info.getYhid(),info.getBrid());
		if (values.size() > 0) {


			Map<String, Object> result = new HashMap<>();
			List<Map<String, Object>> list = new ArrayList<>();
			for (MedicineInfoTake value : values) {
				Map<String, Object> item = new HashMap<>();
				
				item.put("brid", value.getBrid());
				item.put("brxm", value.getBrxm());
				item.put("fphm", value.getFphm());
				item.put("fyck", value.getFyck());
				item.put("lxmc", value.getLxmc());
				item.put("qywz", value.getQywz());
				item.put("sfrq", value.getSfrq());
				item.put("yfmc", value.getYfmc());
				item.put("zjje", value.getZjje());
				list.add(item);	
			}
			result.put("list", list);
			return result;
				
		}else{
			throw new HospitalException("没有最新的取药信息");
		}
	}
}