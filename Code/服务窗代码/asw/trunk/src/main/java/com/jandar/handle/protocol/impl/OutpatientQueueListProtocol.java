package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingList;
import com.jandar.handle.protocol.Protocol;

import java.util.*;

/**
 * 门诊预约排队列表
 */
public class OutpatientQueueListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        List<OutpatientWaitingList> value = HospitalInfoService.getInstance().getOutpatientWaitingList();
        Map<String, Object> over = new HashMap<String, Object>();
        List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
        for (OutpatientWaitingList map : value) {
            Map<String, Object> result = new HashMap<String, Object>();
            result.put("orderid", map.getYylsh());
            result.put("patientname", map.getBrxm());
            result.put("fzxh", map.getFzxh());
            result.put("yysj", map.getYysj());
            result.put("bdzt", map.getBdzt());
            result.put("pdlsh", map.getPdlsh());
            result.put("pdhm", map.getPdhm());
            result.put("pdrq", map.getPdrq());
            result.put("bcmc", map.getBcmc());
            result.put("ksmc", map.getKsmc());
            result.put("ysxm", map.getYsxm());
            result.put("zsmc", map.getZsmc());
            result.put("zswz", map.getZswz());
            result.put("fjhm", map.getFjhm());
            result.put("dqjzh", map.getDqjzh());
            result.put("pdzt", map.getPdzt());
            list.add(result);
        }
        Collections.sort(list, new Comparator<Map<String, Object>>() {
            @Override
            public int compare(Map<String, Object> o1, Map<String, Object> o2) {
                String b1 = (String) o1.get("fzxh");
                String b2 = (String) o2.get("fzxh");
                return b1.compareTo(b2);
            }
        });
        over.put("list", list);
        return over;
    }
}
