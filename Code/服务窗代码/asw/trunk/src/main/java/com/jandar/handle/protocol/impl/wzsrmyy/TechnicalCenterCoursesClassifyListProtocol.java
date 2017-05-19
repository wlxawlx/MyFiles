package com.jandar.handle.protocol.impl.wzsrmyy;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.CourseClassifyInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * 技能中心-培训课程分类列表
 */
public class TechnicalCenterCoursesClassifyListProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        String coursecode = MapUtil.getString(params, "coursecode");
        List<CourseClassifyInfo> classifyList = handler.getCourseClassifyList(coursecode);

        Map<String, Object> result = new HashMap<String, Object>();
        List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
        for (CourseClassifyInfo value : classifyList) {
            Map<String, Object> item = new HashMap<String, Object>();
            item.put("coursecode", value.getClassifyCode());
            item.put("coursename", value.getClassifyName());
            item.put("explain", value.getExplain());
            item.put("prompt", value.getPrompt());
            list.add(item);
        }
        result.put("list", list);
        return result;
    }
}
