package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.util.InfoValidateUtil;
import com.jandar.util.StringUtil;

import java.text.ParseException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 用户信息修改
 * Created by yubj on 2016/4/8.
 */
public class AlterUserInfo {
    public Map<String, Object> alterUserInfoRequest(String openid, String name, String phone, String idcardno) throws HospitalException {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("name", name);
        parameters.put("phone", phone);
        parameters.put("idcardno", idcardno);
        return parameters;
    }

    public boolean alterUserInfoResponse(List<Map<String, String>> process){
        if (process.size() > 0) {
            if (!StringUtil.isBlank(process.get(0).get("openid"))) {
                return true;
            }
        }
        return false;
    }
}
