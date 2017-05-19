package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.MedicineUseInfoList;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author jinliangjin
 * 
 * 服药信息列表
 */
public class MedicineUseInfoListProtocol implements Protocol {
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		UserInfo info = ServiceContext.getHospitalUserInfo();
//		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
//		List<MedicineUseInfoList> values = handler.getMedicineUseInfoListDetail(info.getYhid(),info.getBrid());
		String brid=MapUtil.getString(params,"brid");
		List<MedicineUseInfoList> values =HospitalInfoService.getInstance().getMedicineUseInfoListDetail(info.getAlipayUserId(),brid);
		if (values.size() > 0) {


			Map<String, Object> result = new HashMap<>();
			List<Map<String, Object>> list = new ArrayList<>();
			for (MedicineUseInfoList value : values) {
				Map<String, Object> item = new HashMap<>();
				
				item.put("fphm", value.getFphm());
				item.put("brxm", value.getBrxm());
				item.put("cflsh", value.getCflsh());
				item.put("ksbm", value.getKsbm());
				item.put("ksmc", value.getKsmc());
				item.put("ysbm", value.getYsbm());
				item.put("ysxm", value.getYsxm());
				item.put("brid", value.getBrid());
				item.put("kfrq", value.getKfrq());
				item.put("sfrq", value.getSfrq());
				item.put("zjje", value.getZjje());
				item.put("cflx", value.getCflx());
				item.put("lxmc", value.getLxmc());
				list.add(item);	
			}
			result.put("list", list);
			return result;
				
		}else{
			throw new HospitalException("无可用的服药信息");
		}
	}
}