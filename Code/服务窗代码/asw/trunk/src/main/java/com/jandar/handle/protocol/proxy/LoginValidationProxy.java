/**
 * 文件：LoginValidationProxy.java 2015年12月29日
 * <p/>
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.handle.protocol.proxy;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;

import java.util.Map;

/**
 * 检查当前用户是否已在医院建档，需要用户已建档才能访问的接口都应该用该类包装。
 *
 * @author dys
 * @version 1.0 2015年12月29日
 *
 */
public class LoginValidationProxy implements Protocol {

    private Protocol target;

    public LoginValidationProxy(Protocol target) {
        this.target = target;
    }

    /*
     * (non-Javadoc)
     *
     * @see com.jandar.alipay.dao.Protocol#process(java.lang.String,
     * java.util.Map)
     */
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        if (userInfo == null) {
            throw new HospitalException("还未建档", HospitalException.UNARCHIV);
        }
        return target.process(pcode, params);
    }

}
