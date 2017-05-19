package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.DoctorInfo;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 获得医生停诊信息
 * Created by yubj on 2016/4/13.
 */
public class GetDoctorsStopInfo {

    public List<DoctorInfo> getDoctorsStopInfoResponse(List<Map<String, String>> process) {
        List<DoctorInfo> dcinfolist = new ArrayList<DoctorInfo>();
        for (Map<String, String> map : process) {
            DoctorInfo docinfo = new DoctorInfo();
            docinfo.setCode(map.get("code"));
            docinfo.setName(map.get("name"));
            docinfo.setPictureurl(map.get("pictureurl"));
            docinfo.setDepartcode(map.get("departcode"));
            docinfo.setDepartname(map.get("departname"));
            docinfo.setStopdate(map.get("stopdate"));
            dcinfolist.add(docinfo);
        }
        return dcinfolist;
    }
}
