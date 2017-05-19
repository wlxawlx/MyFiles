package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.AsteriskUtil;

public class PersonCenterProtocol implements Protocol {
	
	/**
	 * @author yubj
	 * 
	 * 		用户信息
	 * */
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		UserInfo info = null;
		try {
			 info = ServiceContext.getHospitalUserInfo();
		} catch (Exception e) {
			// TODO: handle exception
		}
		if (info == null) {
			throw new HospitalException("还未建档");
		} else {
			Map<String, String> user = new HashMap<String, String>();
			user.put("name", AsteriskUtil.formatName(info.getYhxm()));
			user.put("phone", AsteriskUtil.formatPhone(info.getLxdh()));
			user.put("idcardno", AsteriskUtil.formatId(info.getSfzh()));
			user.put("address", info.getLxdz());
			user.put("headurl", info.getHeadUrl());
			user.put("cardbind", info.getBkzt());
			return user;
		}
	}

}
