package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.util.InfoValidateUtil;
import com.jandar.alipay.util.MapUtil;

import java.util.List;
import java.util.Map;

/**
 * 用户注册
 * Created by yubj on 2016/4/8.
 */

public class UserRegister {

//    public UserRegister() {
//
//    }

    public Map<String, Object> userRegisterRequest(HISUserInfo info) throws HospitalException {
        if (info == null) {
            throw new HospitalException("信息参数不完整");
        }
        //验证手机号格式是否是11位
        boolean phonevalidate = InfoValidateUtil.isMobile(info.getPhone());
        if (phonevalidate == false) {
            throw new HospitalException("手机号码输入有误,请重新输入");
        }
        Map<String, Object> parameters = MapUtil.bean2Map(info);
        if (info.getUsertype() == null) {
            throw new HospitalException(HospitalException.ARG_ERROR, "用户类型不能为空");
        }
        parameters.put("usertype", info.getUsertype() == PlatformType.Alipay ? "1" : "2");
        return parameters;
    }

    public boolean userRegisterResponse(List<Map<String, String>> values) {
        return values != null;
    }
}
