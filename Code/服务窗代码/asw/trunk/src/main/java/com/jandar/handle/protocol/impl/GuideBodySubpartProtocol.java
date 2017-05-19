package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.dao.IntelligentGuideData;
import com.jandar.alipay.util.MapUtil;
import net.sf.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

/*
 * 智能导诊-身体部位
 */
public class GuideBodySubpartProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		IntelligentGuideData data = IntelligentGuideData.getIntelligentGuideData();
		String partcode = MapUtil.getString(params, "partcode");
		List<Map<String, Object>> list = new ArrayList<>();
		List<JSONObject> bodypartslist = data.getBodyparts();
		
		if (!StringUtils.isBlank(partcode)) {
			Map<String, Object> dataMap = data.getBodyPartByCodeFromPartcode(partcode);
			// 当传入的大部位的partcode不为空，且在json中可以找到对应的map
			Map<String, Object> newMap = new HashMap<>();
			newMap.put("partname", dataMap.get("partname"));
			newMap.put("partcode", partcode);
			newMap.put("subpart", dataMap.get("subpart"));
			list.add(newMap);
			return list;
		} else {
			for (Map<String, Object> bodypartMap : bodypartslist) {
				Map<String, Object> newMap = new HashMap<>();
				newMap.put("partname", bodypartMap.get("partname"));
				newMap.put("partcode", bodypartMap.get("partcode"));
				newMap.put("subpart", bodypartMap.get("subpart"));
				list.add(newMap);
			}
			return list;
		}
	}
}
