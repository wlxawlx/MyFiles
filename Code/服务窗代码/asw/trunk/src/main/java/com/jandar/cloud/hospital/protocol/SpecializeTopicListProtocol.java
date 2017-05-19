package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.collections.map.LinkedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 专科医院问卷展示CH012601
 * Created by admin on 2016/10/18.
 */
@Component
public class SpecializeTopicListProtocol extends CloudHospitalProtocol {
    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;
    @Autowired
    private UserService userService;
    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {


        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }

        String temp = MapUtil.getString(params, "Temp");
        if (temp == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //问答题列表，慢性荨麻疹,随意获取两道题
        String code = "mxxmz";
        List<ChronicIllnessTopic> chronicIllnessTopics =  chronicIllnessTopicDao.findByIllness_Code(code);
        ChronicIllnessTopic.Items item1 = chronicIllnessTopics.get(0).getItems().get(0);
        ChronicIllnessTopic.Items item2 = chronicIllnessTopics.get(0).getItems().get(2);

        List<ChronicIllnessTopic.Items> items = new ArrayList<>();
        items.add(0,item1);
        items.add(1,item2);

        Map<String,Object> backDoctorScheduleSpecials = new LinkedMap();
        backDoctorScheduleSpecials.put("schedule",temp);
        backDoctorScheduleSpecials.put("TopicCode",items);
        return backDoctorScheduleSpecials;
    }
    @Override
    public String getProtocolCode() {
        return Content.SPECIALIZDE_TOPIC_LIST_CODE;
    }
}
