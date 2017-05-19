package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutpatientOrderNumber;
import com.jandar.alipay.hospital.Order;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import org.apache.commons.lang.StringUtils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

// 预约号源表
public class PrecontractSourceProtocol implements Protocol {
    /*
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	
        List<Map<String, String>> values = HospitalInfoService.getInstance().oldProcess(
                OpCodeContext.D004, requiredParams(params));
        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<>();
            result.put("scheduleseq", params.get("scheduleseq"));
            List<Order> list = new ArrayList<>();
            for (Map<String, String> per : values) {
                Order order = new Order(per.get("hylsh"), per.get("yysj"), per.get("fzxh"));
                list.add(order);
            }
            result.put("list", list);
            return result;
        } else {
            throw new HospitalException("获取预约号源表失败");
        }
    }
    */

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        return process(null, MapUtil.getString(params, "scheduleseq"));
    }

    public Object process(String doctorcode, String scheduleseq) throws HospitalException {
        List<OutpatientOrderNumber> numbers = HospitalInfoService.getInstance().getOutpatientOrderNumbers(null, requiredParams(scheduleseq),null);

        if (numbers.size() > 0) {
            Map<String, Object> result = new HashMap<>();
            result.put("scheduleseq", scheduleseq);
            List<Order> list = new ArrayList<>();
            for (OutpatientOrderNumber number : numbers) {
                Order order = new Order(number.getOrderseq(), number.getOrdertime(), number.getOrderno());
                list.add(order);
            }

            result.put("list", list);
            return result;
        } else {
            throw new HospitalException("获取预约号源表失败");
        }
    }


    private String requiredParams(String scheduleseq) throws HospitalException {
        if (StringUtils.isBlank(scheduleseq)) {
            throw new HospitalException("scheduleseq不能为空");
        }
        return scheduleseq;
    }

}
