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
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 点击疾病判断该病人是否就诊过此疾病
 * Created by lyx on 16/9/19.
 */
@Component
public class RecentSituationByIllnesscodeTopicProtocol extends CloudHospitalProtocol {

    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;

    @Autowired
    private UserService userService;


    @Autowired
    private PatientDao patientimpl;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        Patient info = userService.getCurPatientInfo();
        /**
         * 首先判断该用户是否登陆
         *
         */
        if (info == null) {
            throw new HospitalException("用户未登陆", CloudHospitalException.USER_ERROR);
        }
        /**
         * 取用户patientid判断该登陆用户是否就诊过该疾病
         */
        String illnessCode = MapUtil.getString(params, "IllnessCode");
        if (illnessCode == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String patientid = info.getPatientCode();
        List<Patient> re = patientimpl.findByPatientCode(patientid);
        Patient patientinfo = re.get(0);
        boolean hasIllness = false;

        if (!hasIllness) {
            throw new HospitalException("用户未就诊过该疾病", CloudHospitalException.PATIENT_NO_ILLNESS);
        }
//        List<ChronicIllnessTopic> all = chronicIllnessTopicDao.findByIllness_Code(illnessCode);
//        if (all.size() == 0 || all == null) {
//            throw new HospitalException("疾病代码不存在！", CloudHospitalException.ILLNESS_IS_EMPTY);
//        }
        return illnessCode;

    }

    @Override
    public String getProtocolCode() {
        return Content.TOPIC_BY_ILLNESS_CODE;
    }
}
