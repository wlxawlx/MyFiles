package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllness;
import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.ChronicIllnessDao;
import com.jandar.cloud.hospital.dao.DoctorDao;
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
 * 新增医生信息
 */
@Component
public class AddDoctorProtocol extends CloudHospitalProtocol {
    @Resource
    private DoctorDao doctorDao;
    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        String userName = MapUtil.getString(params, "UserName");
        Doctor doctor = doctorDao.findByUserName(userName);
        if (doctor==null){
            doctor = new Doctor();
            doctor.setName(userName);
            doctor.setUserName(userName);
            doctor.setCode(userName);
            doctor.setPwd("000000");
            doctorDao.save(doctor);
        }
        return null;
    }
    @Override
    public String getProtocolCode() {
        return Content.ADD_DOCTOR_CODE;
    }
}
