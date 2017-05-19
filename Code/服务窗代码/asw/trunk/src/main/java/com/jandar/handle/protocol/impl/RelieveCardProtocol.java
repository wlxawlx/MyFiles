package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;

/*
 * @author yubj
 * 
 * 解绑就诊卡。
 */
public class RelieveCardProtocol implements Protocol {
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		UserInfo userinfo=ServiceContext.getHospitalUserInfo();
		boolean values = HospitalInfoService.getInstance().unbindOutpatientCard(userinfo.getAlipayUserId());
		if (values) {
			
			/** 清空会话存储中的病人ID,防止出现没有重新登录时已经解了卡,还可以进行只有绑卡时才能的操作 */
			UserInfo info = ServiceContext.getHospitalUserInfo();
			info.setBrid("");
			
			Map<String , String >result=new HashMap<>();
			result.put("state", "1");
			return result;
		}
		else {
			throw new HospitalException("就诊卡解绑失败");
		}
	}
}
