package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DoctorInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 得医生信息列表_按姓名拼音首字母查
 * Created by yubj on 2016/4/13.
 */
public class GetDoctorInfoAll {

    public List<DoctorInfo> getDoctorInfoByPyResponse(List<Map<String, String>> process) {
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        for (Map<String, String> result : process) {
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
            dcinfo.setReserve(result.get("reserve"));
            dcinfolist.add(dcinfo);
        }
        return dcinfolist;
    }
    public Map<String, Object> getDoctorInfoByPyRequest(String namepy, String pageindex, String pagesize,String hasKey) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("namepy", namepy);
        parameters.put("pagesize", pagesize); // 页大小
        parameters.put("pageindex", pageindex);// 页号
        parameters.put("haskey",hasKey);
        return parameters;
    }
}

