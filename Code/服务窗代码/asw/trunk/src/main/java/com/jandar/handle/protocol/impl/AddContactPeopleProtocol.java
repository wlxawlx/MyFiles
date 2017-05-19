package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;

public class AddContactPeopleProtocol implements Protocol {

	/**
	 * @author yubj 添加常用联系人
	 */
	@Override
	public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
		UserInfo info = ServiceContext.getHospitalUserInfo();
		String label = MapUtil.getString(params, "label");
		if (label == null) {
			label = null;
		}
		String name = MapUtil.getString(params, "name");
		if (StringUtils.isBlank(name)) {
			throw new HospitalException("缺少用户名");
		}
		String phone = MapUtil.getString(params, "phone");
		if (StringUtils.isBlank(phone)) {
			throw new HospitalException("缺少手机号");
		}
		String idcardno = MapUtil.getString(params, "idcardno");
		if (StringUtils.isBlank(idcardno)) {
			throw new HospitalException("缺少身份证号");
		}
		String address = MapUtil.getString(params, "address");
		if (address == null) {
			address = null;
		}
		ContactPeopleInfo values = HospitalInfoService.getInstance().addContact(info.getAlipayUserId(), label, name,
				phone, idcardno, address);
		Map<String, String> result = new HashMap<String, String>();
		// result.put("label", values.getLabel());
		result.put("label", label);
		result.put("name", values.getName());
		result.put("linkmanid", values.getLinkmanid());
		return result;
	}
}
