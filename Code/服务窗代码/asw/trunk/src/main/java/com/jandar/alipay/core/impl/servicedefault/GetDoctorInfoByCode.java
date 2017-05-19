package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DoctorInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 得医生信息列表_按医生代码查
 * Created by yubj on 2016/4/13.
 */
public class GetDoctorInfoByCode {
    public Map<String, Object> getDoctorInfoByCodeRequest(String code) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("code", code);
        return parameters;
    }

    public List<DoctorInfo> getDoctorInfoByCodeResponse(List<Map<String, String>> process) {
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        if (process.size() > 0) {
            Map<String, String> result = process.get(0);
            DoctorInfo dcinfo = new DoctorInfo();
            dcinfo.setCode(result.get("code"));
            dcinfo.setName(result.get("name"));
            dcinfo.setSex(result.get("sex"));
            dcinfo.setPictureurl(result.get("pictureurl"));
            dcinfo.setLevel(result.get("level"));
            dcinfo.setRecommend(result.get("recommend"));
            dcinfo.setAdept(result.get("adept"));
            dcinfo.setDepartcode(result.get("departcode"));
            dcinfo.setDepartname(result.get("departname"));
            dcinfolist.add(dcinfo);
        }
        return dcinfolist;
    }
}
