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
import java.util.List;
import java.util.Map;

/**
 * 根据疾病获取问卷
 * Created by lyx on 16/10/19.
 */
@Component
public class llnesscodeTopicProtocol extends CloudHospitalProtocol {

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
        String illnessCode = MapUtil.getString(params, "IllnessCode");
        if (illnessCode == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        List<ChronicIllnessTopic> all = chronicIllnessTopicDao.findByIllness_Code(illnessCode);
        if (all.size() == 0 || all == null) {
            throw new HospitalException("疾病代码不存在！", CloudHospitalException.ILLNESS_IS_EMPTY);
        }
        return all.get(0);
    }

    @Override
    public String getProtocolCode() {
        return Content.ILLNESS_TOPIC_CODE;
    }
}
