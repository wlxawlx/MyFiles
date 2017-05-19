package com.jandar.handle.protocol.impl.wzstjzx;

import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author jinliangjin86
 * 
 * 预定体检套餐
 */

public class PhysicalReportProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params)
            throws HospitalException {
        HISServiceHandlerWZSRMYY handler = HospitalInfoService
                .getRealInstance(HISServiceHandlerWZSRMYY.class);
        Map<String, Object> values = handler.getPhysicalReport(MapUtil
                .getString(params, "tjbh"));
        return values;
    }
}
