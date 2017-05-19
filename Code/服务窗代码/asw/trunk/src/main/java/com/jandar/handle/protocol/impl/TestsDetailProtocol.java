package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.TestIndicator;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

public class TestsDetailProtocol implements Protocol {
	
	/**
	 * @author zhou
	 * 
	 * 		获取化验明细
	 * */
	@Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		List<TestIndicator>  values = HospitalInfoService.getInstance().getTestIndicatorsInfo(requiredParams(params));
        if (values.size() > 0) {
        	List<Map<String,Object>> results=new ArrayList<>();
        	for(TestIndicator t:values){
        		Map<String,Object> map=new HashMap<>();
//        		map=MapUtil.bean2Map(t);
        		map.put("jylx", t.getJylx());
        		map.put("xmmc", t.getXmmc());
        		map.put("result", t.getResult());
        		map.put("hint", t.getHint());
        		map.put("ckfw", t.getCkfw());
        		map.put("xmdw", t.getXmdw());
        		results.add(map);
        	}
            return results;
        } else {
            throw new HospitalException("化验明细为空");
        }
    }

	private String requiredParams(Map<String, Object> params) throws HospitalException {

		// 其它校验
		if (StringUtils.isBlank(MapUtil.getString(params, "doctadviseno"))) {
			throw new HospitalException("化验单条码不能为空");
		}

		return MapUtil.getString(params, "doctadviseno");
	}
}
