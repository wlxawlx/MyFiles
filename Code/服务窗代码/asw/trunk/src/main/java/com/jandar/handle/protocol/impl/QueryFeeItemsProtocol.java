package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.FeeItemsInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 收费项目-无查询条件，分页 - 查询基本功能
 * Created by yubj on 2016/4/11.
 */
public class QueryFeeItemsProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String pageno = MapUtil.getString(params, "pageno");
        String pagerow = MapUtil.getString(params, "pagerow");
        if(pageno.indexOf(".") != -1){
            pageno=pageno.substring(0,pageno.indexOf("."));
        }
        if(pagerow.indexOf(".") != -1){
            pagerow=pagerow.substring(0,pagerow.indexOf("."));
        }
        List<FeeItemsInfo> values = HospitalInfoService.getInstance().queryFeeItems(pageno,pagerow);
        List<Map<String, String>> list = new ArrayList<Map<String, String>>();
        for (FeeItemsInfo fee : values) {
            Map<String, String> result = new HashMap<String, String>();
            result.put("fylx", fee.getFylx());
            result.put("fymc", fee.getFymc());
            result.put("fydw", fee.getFydw());
            result.put("fyjg", fee.getFyjg());
            list.add(result);
        }
        return list;
    }
}
