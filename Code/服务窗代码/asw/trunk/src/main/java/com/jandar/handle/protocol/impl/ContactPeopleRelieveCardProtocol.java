package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author yubj
 * 
 * 常用联系人就诊卡解绑
 */
public class ContactPeopleRelieveCardProtocol implements Protocol {
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	UserInfo userInfo=ServiceContext.getHospitalUserInfo();
    	String linkmanid=MapUtil.getString(params, "linkmanid");
    	OutPatientCardInfo values = HospitalInfoService.getInstance().unbindContactOutpatientCard(userInfo.getAlipayUserId(), linkmanid);
        if (values!=null) {
        	Map<String , String >result=new HashMap<String,String>();
        	result.put("state", "1");
        	return result;
        } else {
            throw new HospitalException("常用联系人解绑失败");
        }
    }

}
