package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.collections.MapUtils;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.MedicineUseInfo;

/*
 * @author jinliangjin
 * 
 * 服药信息。
 */
public class MedicineUseInfoProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String cflsh = MapUtils.getString(params, "cflsh");
        if (StringUtils.isBlank(cflsh)) {
            throw new HospitalException("处方流水号不能为空");
        }
//		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
//		List<MedicineUseInfo> values = handler.getMedicineUseInfoDetail(params.get("cflsh").toString());
        List<MedicineUseInfo> values = HospitalInfoService.getInstance().getMedicineUseInfoDetail(params.get("cflsh").toString());
        if (values.size() > 0) {

            Map<String, Object> result = new HashMap<>();
            List<Map<String, Object>> list = new ArrayList<>();
            for (MedicineUseInfo value : values) {

                Map<String, Object> item = new HashMap<>();

                item.put("ypmc", value.getYpmc());
                item.put("ypgg", value.getYpgg());
                item.put("ypsl", value.getYpsl());
                item.put("ycyl", value.getYcyl());
                item.put("ysjy", value.getYsjy());
                item.put("sync", value.getSync());
                item.put("gytj", value.getGytj());
                list.add(item);
            }
            return list;

        } else {
            throw new HospitalException("该处方不存在");
        }
    }


}