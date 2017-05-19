package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.dao.IntelligentGuideData;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * 智能导诊-部位症状列表
 */
public class GuideSubpartSymptomProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String partcode = MapUtil.getString(params, "partcode");
        String classificcode = MapUtil.getString(params, "classificcode");
        String sex = MapUtil.getString(params, "sex");
        Integer pageindex = MapUtil.getInteger(params, "pageindex");
        Integer pagesize = MapUtil.getInteger(params, "pagesize");

        if (StringUtils.isBlank(partcode)) {
            throw new HospitalException("部位代码不能为空");
        }

        if (StringUtils.isEmpty(classificcode)) {
            classificcode = "01"; // 默认
        }

        if (StringUtils.isEmpty(sex) || (!"0".equals(sex) && !"1".equals(sex))) {
            throw new HospitalException("性别错误");
        }

        if (pageindex == null || pageindex < 1) {
            throw new HospitalException("页号是从1开始的");
        }

        if (pagesize == null || pagesize == 0) {
            pagesize = 20;
        }

        IntelligentGuideData data = IntelligentGuideData.getIntelligentGuideData();
        List<Map<String, Object>> dataList = data.getSymptomFromPartcodeClassific(partcode, classificcode);
        if (dataList == null || dataList.isEmpty()) {
            throw new HospitalException("没有该部位的相关症状");
        }

        int index = (pageindex - 1) * pagesize;
        List<Map<String, Object>> pageResult = new ArrayList<>();
        for (; index < dataList.size() && pageResult.size() < pagesize; ++index) {
            Map<String, Object> dataMap = dataList.get(index);
            String applyobject = MapUtil.getString(dataMap, "applyobject");
            if (!(StringUtils.isEmpty(applyobject) || applyobject.equals(sex) || applyobject.equals("9"))) {
                continue;
            }

            Map<String, Object> resultMap = new HashMap<>();
            resultMap.put("partname", dataMap.get("partname"));
            resultMap.put("partcode", dataMap.get("partcode"));
            resultMap.put("classificcode", dataMap.get("classificcode"));
            resultMap.put("classificname", dataMap.get("classificname"));
            resultMap.put("symptomcode", dataMap.get("symptomcode"));
            resultMap.put("symptomname", dataMap.get("symptomname"));
            resultMap.put("alias", dataMap.get("alias"));
            pageResult.add(resultMap);
        }

        return pageResult;
    }

}
