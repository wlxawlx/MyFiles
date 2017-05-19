package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.filter.auth.util.ThirdUserInfo;

import java.util.HashMap;
import java.util.Map;

/**
 * 获得支付宝个人信息
 */
public class PersonCenterProtocol_Alipay implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        ThirdUserInfo info = null;
        try {
            info = ServiceContext.getThirdUserInfo();

        } catch (Exception e) {
            throw new HospitalException(e.getMessage(), HospitalException.UN_USERINFO);
        }
        if (info == null) {
            throw new HospitalException("网络错误,请刷新重试", HospitalException.UN_USERINFO);
        }

        Map<String, String> user = new HashMap<String, String>();

        user.put("name", info.getName());
        user.put("headurl", info.getAvatar());
        user.put("idcardno", info.getCertNo());
        return user;
    }

}
