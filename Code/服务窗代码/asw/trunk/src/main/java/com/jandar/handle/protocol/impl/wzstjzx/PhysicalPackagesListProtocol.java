package com.jandar.handle.protocol.impl.wzstjzx;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.PhysicalPackages;
import com.jandar.handle.protocol.Protocol;

/*
 * @author jinliangjin86
 * 
 * 体检套餐列表
 */

public class PhysicalPackagesListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params)
            throws HospitalException {
        HISServiceHandlerWZSRMYY handler = HospitalInfoService
                .getRealInstance(HISServiceHandlerWZSRMYY.class);
        List<PhysicalPackages> values = handler.getPhysicalPackagesList();
        Map<String, Object> over = new HashMap<String, Object>();
        List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
        for (PhysicalPackages p : values) {
            Map<String, Object> result = new HashMap<String, Object>();
            result.put("tcid", p.getPackagesID());
            result.put("tcmc", p.getPackagesName());
            result.put("tcjg", p.getPackagesPrice());
            result.put("tcbz", p.getPackagesStandard());
            result.put("tclb", p.getPackagesCategory());
            list.add(result);
        }
        over.put("list", list);
        return list;
    }
}
