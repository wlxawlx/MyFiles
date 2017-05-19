package com.jandar.handle.protocol.impl.wzsrmyy;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.Map;

/*
 * 技能中心-取消课程报名
 */
public class CourseCancellationRegistrationProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        String orderno = MapUtil.getString(params, "orderno");
        if (StringUtils.isBlank(orderno)) {
            throw new HospitalException("订单号不能为空");
        }

        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        boolean b = handler.cancelCourseOrder(orderno);
        if (!b) {
            throw new HospitalException("取消课程报名失败");
        }
        System.out.println("取消成功了");
        return null;
    }
}
