package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.dao.IntelligentGuideData;
import com.jandar.alipay.util.MapUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

/*
 * 智能导诊-部位症状分类
 */
public class GuideSubpartSymptomClassificsProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		IntelligentGuideData data = IntelligentGuideData.getIntelligentGuideData();
		List<Map<String, Object>> result = new ArrayList<Map<String, Object>>();
		Map<String, Object> resultMap = new HashMap<>();
		String partcode = MapUtil.getString(params, "partcode");
		if(StringUtils.isBlank(partcode)){
			throw new HospitalException("您还没有输入部位");
		}
		Map<String, Object> dataMap = data.getSymptomClassificFromPartcode(partcode);
		
		resultMap.put("partname", dataMap.get("partname"));
		resultMap.put("partcode", dataMap.get("partcode"));
		resultMap.put("classifics", dataMap.get("classifics"));
		
		result.add(resultMap);
		return result;
	}

}
