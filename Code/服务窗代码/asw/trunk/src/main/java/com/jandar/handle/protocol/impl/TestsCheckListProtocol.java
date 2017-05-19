package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.Inspection;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;

public class TestsCheckListProtocol implements Protocol {

	/**
	 * @author zhou
	 * 
	 *         获取检测单列表
	 */
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        System.out.println("TestsCheckListProtocol:process:start");
		StringBuffer name = new StringBuffer();
		List<Inspection>  values = HospitalInfoService.getInstance().getInspectionsList(requiredParams(params,name),requiredParams(params));
		 Map<String, Object> result = new HashMap<>();
	        result.put("name", name.toString());
	        List<Map<String, Object>> list = new ArrayList<>();
	        for (Inspection value : values) {
	            Map<String, Object> items = new HashMap<String, Object>();
	            items.put("doctadviseno", value.getDoctadviseno());
	            items.put("examinaim", value.getExaminaim());
	            items.put("requesttime",
	                    DateUtil.dateFormat(value.getRequesttime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
	            items.put("requester", value.getRequester());
//
	            list.add(items);
	        }
	        result.put("list", list);
	        return result;
	}

	private String requiredParams(Map<String, Object> params) throws HospitalException {
		  UserInfo info = ServiceContext.getHospitalUserInfo();
	         String linkmanid = MapUtil.getString(params, "linkmanid");
	         if (StringUtils.isBlank(linkmanid)) {
	             return info.getSfzh();
	         } else {
	             List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);
	             String Idcardno=null;
	             if (contactsList.size() > 0) {
	                 ContactPeopleInfo value = contactsList.get(0);
	                 Idcardno=value.getIdcardno();
	                 return Idcardno;
	             }
	             if (StringUtils.isBlank(Idcardno)) {
	                 throw new HospitalException("联系人不存在");
	             }
	         }
	         return null;
	}

	private String requiredParams(Map<String, Object> params, StringBuffer name) throws HospitalException {
		  UserInfo info = ServiceContext.getHospitalUserInfo();
	        String linkmanid = MapUtil.getString(params, "linkmanid");
	        if (StringUtils.isBlank(linkmanid)) {
	        	name.append(info.getYhxm());
	            return info.getYhxm();
	        } else {
	            List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);
	            if (contactsList.size() > 0) {
	                ContactPeopleInfo value = contactsList.get(0);
	                name.append(value.getName());
	                return value.getName();
	            }
	            if (StringUtils.isBlank(name.toString())) {
	                throw new HospitalException("联系人不存在");
	            }
	        }
			return null;
	}
}
