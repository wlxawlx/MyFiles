package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import java.util.HashMap;
import java.util.Map;

/**
 *@author yubj
 *         <p/>
 *         删除常用联系人
 */
public class DeleteContactPeopleProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	UserInfo info = ServiceContext.getHospitalUserInfo();
        ContactPeopleInfo values = HospitalInfoService.getInstance().removeContact(info.getAlipayUserId(),  MapUtil.getString(params, "linkmanid"));
        if (values != null) {
            Map<String, Object> result = new HashMap<>();
            result.put("linkmanid", MapUtil.getString(params, "linkmanid"));
            return result;
        } else {
            throw new HospitalException("删除常用联系人失败");
        }
    }


}
