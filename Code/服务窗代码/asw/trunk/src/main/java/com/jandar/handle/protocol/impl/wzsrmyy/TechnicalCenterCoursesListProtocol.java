package com.jandar.handle.protocol.impl.wzsrmyy;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.CourseInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * 技能中心-培训课程列表
 */
public class TechnicalCenterCoursesListProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String coursecode = MapUtil.getString(params, "coursecode");
        String courseid = MapUtil.getString(params, "courseid");
        return process(coursecode, courseid);
    }

    private Map<String, Object> process(String coursecode, String courseid) throws HospitalException {
        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        List<CourseInfo> values = handler.getCourseList(coursecode, courseid);
        List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
        for (CourseInfo value : values) {
            Map<String, Object> item = new HashMap<String, Object>();
            //过滤项目名称中有测试的类目
            if (value.getCoursename() != null && !value.getCoursename().contains("测试")) {
                item.put("coursecode", value.getClassifyCode());
                item.put("courseid", value.getCourseCode());
                item.put("coursename", value.getCoursename());
                item.put("coursefee", value.getCoursefee());
                item.put("courseremark", value.getCourseremark());
                list.add(item);
            }
        }
        Map<String, Object> result = new HashMap<String, Object>();
        result.put("list", list);
        return result;
    }


}
