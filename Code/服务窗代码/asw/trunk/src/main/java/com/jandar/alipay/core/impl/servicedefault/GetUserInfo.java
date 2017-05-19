package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import jodd.util.StringUtil;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 用户登录,获得用户信息
 * Created by yubj on 2016/4/8.
 */
public class GetUserInfo {
    public Map<String, Object> getUserInfoRequest(String openid, PlatformType usertype) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("usertype", usertype == PlatformType.Alipay ? "1" : "2");
        return parameters;
    }

    public HISUserInfo getUserInfoResponse(List<Map<String, String>> process) {
        HISUserInfo info = new HISUserInfo();
        Map<String, String> re = process.get(0);
        info.setName(re.get("name"));
        info.setPatientid(re.get("patientid"));
        info.setPhone(re.get("phone"));
        info.setIdcardno(re.get("idcardno"));
        info.setAddress(re.get("address"));
        info.setOpenid(re.get("openid"));
        info.setCardno(re.get("cardno"));
        info.setHeadurl(re.get("headurl"));
        info.setBkzt(StringUtil.isBlank(info.getCardno()) ? "0" : "1");
        info.setInpatentflag(re.get("inpatentflag"));
        return info;
    }
}
