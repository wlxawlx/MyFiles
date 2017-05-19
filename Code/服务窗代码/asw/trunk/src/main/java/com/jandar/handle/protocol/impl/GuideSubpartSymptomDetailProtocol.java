package com.jandar.handle.protocol.impl;

import com.alipay.api.internal.util.StringUtils;
import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.dao.IntelligentGuideData;
import com.jandar.alipay.util.MapUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * 智能导诊-部位症状详细信息
 */
public class GuideSubpartSymptomDetailProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		String symptomcode = MapUtil.getString(params, "symptomcode");
		String sex = MapUtil.getString(params, "sex");

		if (StringUtils.isEmpty(symptomcode)) {
			throw new HospitalException("症状号不能为空");
		}

		if (StringUtils.isEmpty(sex)) {
			throw new HospitalException("性别不能为空");
		}

		IntelligentGuideData data = IntelligentGuideData.getIntelligentGuideData();
		Map<String, Object> dataMap = data.getSymptomFromSymptomcodeWithSex(symptomcode, sex);
		Map<String, Object> resultMap = new HashMap<String, Object>();
		if (dataMap == null) {
			throw new HospitalException("不存在的症状代码");
		} else {
			resultMap.put("partname", dataMap.get("partname"));
			resultMap.put("partcode", dataMap.get("partcode"));
			resultMap.put("classificcode", dataMap.get("classificcode"));
			resultMap.put("classificname", dataMap.get("classificname"));
			resultMap.put("symptomcode", dataMap.get("symptomcode"));
			resultMap.put("symptomname", dataMap.get("symptomname"));
			resultMap.put("alias", dataMap.get("alias"));
			resultMap.put("describe", dataMap.get("describe"));
			resultMap.put("origin", dataMap.get("origin"));

			// + 疾病的修改
			List<Map<String, Object>> dataSicknessList = (List<Map<String, Object>>) dataMap.get("sickness");
			List<Map<String, Object>> resultSicknessList = changeSickness(dataSicknessList, sex, data);

			resultMap.put("sickness", resultSicknessList);
		}
		System.out.println("resultMap.size(): "+resultMap.size());
		return resultMap;
	}

	/*
	 * 改动sickness中返回的参数个数和内容
	 */
	private List<Map<String, Object>> changeSickness(List<Map<String, Object>> dataSicknessList, String sex,
			IntelligentGuideData data) {
		List<Map<String, Object>> resultSicknessList=new ArrayList<>();
		for(Map<String, Object> dataSicknessMap :dataSicknessList){
			Map<String, Object>  resultSicknessMap=new HashMap<>();
			resultSicknessMap.put("name", dataSicknessMap.get("name"));
			List<Map<String,Object>> dataSymptomsList= (List<Map<String, Object>>) dataSicknessMap.get("symptoms");
			List<String> resultSymptomsList=new ArrayList<>();
			
			for(Map<String,Object> dataSymptomsMap:dataSymptomsList){
				resultSymptomsList.add((String) dataSymptomsMap.get("name"));
			}
			
			resultSicknessMap.put("symptoms", resultSymptomsList);
			resultSicknessMap.put("departments", dataSicknessMap.get("departments"));
			resultSicknessList.add(resultSicknessMap);
		}
		return resultSicknessList;
	}

}
