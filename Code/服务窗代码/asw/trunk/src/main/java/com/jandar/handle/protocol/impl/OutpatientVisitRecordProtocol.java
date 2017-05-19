package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutpatientVisitRecord;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;


/*
 * @author jinliangjin
 * 
 * 就诊信息-信息列表
 */
public class OutpatientVisitRecordProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
//		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
//		List<OutpatientVisitRecord> values = handler.getOutpatientVisitRecordDetail(info.getYhid(),info.getBrid());
        String brid = MapUtil.getString(params, "brid");
        List<OutpatientVisitRecord> values = HospitalInfoService.getInstance().getOutpatientCaseList(info.getAlipayUserId(), brid);
        if (values.size() > 0) {


            Map<String, Object> result = new HashMap<>();
            List<Map<String, Object>> list = new ArrayList<>();
            for (OutpatientVisitRecord value : values) {
                Map<String, Object> item = new HashMap<>();

                item.put("jzxh", value.getJzxh());
                item.put("jzrq", value.getJzrq());
                item.put("ksmc", value.getKsmc());
                item.put("ysxm", value.getYsxm());
                item.put("zdxx", value.getZdxx());
                list.add(item);
            }
            result.put("list", list);
            return result;

        } else {
            throw new HospitalException("没有门诊就诊信息");
        }
    }
}
