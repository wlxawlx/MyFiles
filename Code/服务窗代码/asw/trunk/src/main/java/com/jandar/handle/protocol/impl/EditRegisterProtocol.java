package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;

public class EditRegisterProtocol implements Protocol {

    /**
     * 修改用户注册信息
     * 不能修改姓名和身份证号
     */
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String name = userInfo.getYhxm();
        String idcardno = MapUtil.getString(params, "idcardno");
        String phone = MapUtil.getString(params,"phone");
        boolean values = HospitalInfoService.getInstance().alterUserInfo(userInfo.getAlipayUserId(), name, phone, idcardno);
        if (values) {
            Map<String, String> result = new HashMap<String, String>();
            userInfo.setLxdh(phone);
            result.put("state", "1");
            return result;
        } else {
            throw new HospitalException("修改用户注册信息失败");
        }
    }
}
