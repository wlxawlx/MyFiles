package com.jandar.handle.protocol.impl;

import com.alipay.api.internal.util.StringUtils;
import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.dao.IntelligentGuideData;
import com.jandar.alipay.util.MapUtil;

import java.util.*;

public class GuideSubpartSymptomSicknessProtocol implements Protocol {

    /*
     * 智能导诊-部位症状相关疾病
     */
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String symptomcode = MapUtil.getString(params, "symptomcode");
        String correlate_codesString = MapUtil.getString(params, "correlate_codes");
        String sex = MapUtil.getString(params, "sex");

        if (StringUtils.isEmpty(symptomcode)) {
            throw new HospitalException("主症状号不能为空");
        }

        if (StringUtils.isEmpty(sex)) {
            throw new HospitalException("性别不能为空");
        }

        IntelligentGuideData data = IntelligentGuideData.getIntelligentGuideData();
        Map<String, Object> dataMap = data.getSymptomFromSymptomcodeWithSex(symptomcode, sex);


        if (dataMap == null) {
            throw new HospitalException("不存在的症状代码");
        }

        List<Map<String, Object>> dataSicknessList = (List<Map<String, Object>>) dataMap.get("sickness");

        List<String> correlate_codes = null;
        // 如果correlate_codes不为空，则需要对其排序
        if (!StringUtils.isEmpty(correlate_codesString)) {
            correlate_codes = getListFromString(correlate_codesString);
        } else {
            correlate_codes = new ArrayList<>();
        }

        // 由于在相关症状中去掉了主症状，所以要添加主症状
        // 若用户为输入其他相关症状，则根据主症状对其排序
        correlate_codes.add(symptomcode);
        return sort(dataSicknessList, correlate_codes);
    }


    // 将疾病列表进行排序
    private List<Map<String, Object>> sort(List<Map<String, Object>> dataSicknessList, List<String> correlate_codes) {

        List<Map<String, Object>> resultList = new ArrayList<>();
        Set<Map<String, Object>> sortSet = new TreeSet<Map<String, Object>>(new Comparator<Map<String, Object>>() {
            @Override
            public int compare(Map<String, Object> o1, Map<String, Object> o2) {
                int num = (int) o2.get("count") - (int) o1.get("count");
                int num2 = (int) (num == 0 ? ((String) o1.get("name")).compareTo((String) o2.get("name")) : num);
                return num2;
            }
        });

        for (Map<String, Object> dataSicknessMap : dataSicknessList) {
            Map<String, Object> sortMap = new HashMap<>();
            sortMap.put("name", dataSicknessMap.get("name"));
            sortMap.put("departments", dataSicknessMap.get("departments"));
            sortMap.put("count", 0);

            int count = 0;

            List<Map<String, Object>> dataSymptomsList = (List<Map<String, Object>>) dataSicknessMap.get("symptoms");
            for (Map<String, Object> dataSymptomsMap : dataSymptomsList) {
                for (String str : correlate_codes) {

                    if (dataSymptomsMap.get("code").equals(str)) {
                        count++;
                    }
                }

            }
            sortMap.put("count", count);
            sortSet.add(sortMap);
        }

        for (Map<String, Object> map : sortSet) {
            Map<String, Object> resultMap = new HashMap<>();
//			System.out.println("name: "+map.get("name")+"count: "+String.valueOf(map.get("count")));
            resultMap.put("name", map.get("name"));
            resultMap.put("departments", map.get("departments"));
            resultList.add(resultMap);
        }
        return resultList;
    }

    // 将字符串转换为集合
    private List<String> getListFromString(String str) {
        if (str == null) {
            return null;
        } else {
            String[] strs = str.split(",");
            return new ArrayList<String>(Arrays.asList(strs));
        }
    }


}
