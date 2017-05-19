package com.jandar.handle.protocol.impl;

import com.alipay.api.internal.util.StringUtils;
import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.dao.IntelligentGuideData;
import com.jandar.alipay.util.MapUtil;

import java.util.*;

/*
 * 智能导诊-部位症状相关其它症状
 */
public class GuideSubpartSymptomOtherSymptomProtocol implements Protocol {

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

        if (dataMap == null) {
            throw new HospitalException("不存在的症状代码");
        } else {
            List<Map<String, Object>> dataSicknessList = (List<Map<String, Object>>) dataMap.get("sickness");
            Set<Map<String, Object>> resultSet = new HashSet<Map<String, Object>>();
            for (Map<String, Object> dataSicknessMap : dataSicknessList) {
                List<Map<String, Object>> dataSymptomsList = (List<Map<String, Object>>) dataSicknessMap.get("symptoms");
                // 在相关症状集合中去除主症状的集合
                for (Map<String, Object> dataSymptomsMap : dataSymptomsList) {
                    String code = MapUtil.getString(dataSymptomsMap, "code");
                    String name = MapUtil.getString(dataSymptomsMap, "name");

                    if (symptomcode.equals(code)) {
                        continue;
                    }
                    Map<String, Object> resultMap = new HashMap<>();
                    resultMap.put("symptomcode", code);
                    resultMap.put("symptomname", name);
                    resultSet.add(resultMap);
                }
            }
            return resultSet;
        }
    }

}
