package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.collections.map.HashedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.List;
import java.util.Map;

/**
 * 专科医院获取近况题目协议
 * 可通医院代码来获得题目
 */
@Component
public class RecentSituationByHospitalCodeTopicProtocol extends CloudHospitalProtocol {

    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;

    @Autowired
    private UserService userservice;


    @Autowired
    private PatientDao patientimpl;

    @Autowired
    private UserService userService;


    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //************************************临时注释掉，不验证是否登录********************************
        //Patient info = userservice.getCurPatientInfo();
        /**
         * 首先判断该用户是否登陆
         *
         */
        /*
        if (info == null) {
            throw new HospitalException("用户未登陆", CloudHospitalException.USER_ERROR);
        }
        */
        //**********************************************************************************************
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String hospitalCode = MapUtil.getString(params, "HospitalCode");
        if (hospitalCode == null) {
            throw new HospitalException("请求参数HospitalCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        List<ChronicIllnessTopic> all = chronicIllnessTopicDao.findByHospitalCode(hospitalCode);
        if (all.size() == 0 || all == null) {
            throw new HospitalException(hospitalCode+"没有相应问卷！", CloudHospitalException.DEPT_IS_EMPTY);
        }
        ChronicIllnessTopic chronicIllnessTopic = all.get(0);
        Map<String, Object> chronicIllnessTopicMap = new HashedMap();
        chronicIllnessTopicMap.put("Code",chronicIllnessTopic.getCode());
        chronicIllnessTopicMap.put("Tile",chronicIllnessTopic.getTile());
        chronicIllnessTopicMap.put("Items",chronicIllnessTopic.getItems());
        return chronicIllnessTopicMap;

    }

    @Override
    public String getProtocolCode() {
        return Content.TOPIC_BY_HOSPITAL_CODE;
    }
}
