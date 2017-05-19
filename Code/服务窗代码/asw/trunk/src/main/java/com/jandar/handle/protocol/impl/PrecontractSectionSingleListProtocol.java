package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DepartmentInfo;
import com.jandar.handle.protocol.Protocol;

import java.util.*;

/*
 * @author yubj
 * 
 * 预约科室列表 只有一级科室的使用
 */

public class PrecontractSectionSingleListProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        List<DepartmentInfo> values = HospitalInfoService.getInstance().getDepartmentsListForOrder("");
        Map<String,Object> result = new HashMap<>();
        List<Map<String,String>> sections = new ArrayList<>();
        for(DepartmentInfo section:values){
            Map<String,String> sectionInfo = new HashMap<>();
            sectionInfo.put("departid",section.getDepartcode());
            sectionInfo.put("departname",section.getDepartname());
            sections.add(sectionInfo);
        }
        result.put("list",sections);
        return result;

    }
}
