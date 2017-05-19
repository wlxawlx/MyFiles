package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.struct.TodayDoctorInfo;
import net.sf.json.JSONArray;
import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * 当日预约挂号-预约科室查医生列表
 */
public class DoctorListofDayByDepartmentsProtocol implements Protocol {

    @SuppressWarnings("unchecked")
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String departcode = MapUtil.getString(params, "departid");
        if (StringUtils.isBlank(departcode)) {
            throw new HospitalException("缺少科室代码");
        }
        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        List<TodayDoctorInfo> values = handler.getDoctorListofDayByDepartments(departcode);
        Map<String, Object> result = new HashMap<String, Object>();
        List<Map<String, Object>> list;
        try {
             list = (List<Map<String, Object>>)JSONArray.fromObject(values);;
        }catch (Exception e){
            throw new HospitalException("暂无数据,请联系管理员");
        }

//        for (TodayDoctorInfo per : values) {
//            Map<String , Object >map=new HashMap<String , Object>();
//
//            list.add(map);
//        }
//        list =
        result.put("list", list);
        return result;
    }

}
