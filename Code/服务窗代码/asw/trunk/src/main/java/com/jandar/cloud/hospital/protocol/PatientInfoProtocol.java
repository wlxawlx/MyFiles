package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Map;


/**
 * 病人信息
 * Created by jhf on 2016/10/19.
 */
@Component
public class PatientInfoProtocol extends CloudHospitalProtocol{
    @Resource
    private PatientDao patientDao;



    @Override
    public String getProtocolCode() {
        return Content.PATIENT_INFO_CODE;
    }

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {

        String patientCode = MapUtil.getString(params, "PatientCode");
        if (patientCode == null) {
            throw new HospitalException("请求参数PatientCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        /*String[] queryFields = {"PatientCode","Name","FirstVisit","Sex"};
        Patient patient = patientDao.findone(patientCode,queryFields);      //病人信息*/
        Map<String, String>  patient = HospitalInfoService.getInstance().getPatientInfo(patientCode);
        return patient;
    }
}
