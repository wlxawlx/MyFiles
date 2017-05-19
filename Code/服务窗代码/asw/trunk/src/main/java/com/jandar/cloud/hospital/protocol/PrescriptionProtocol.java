package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 处方列表
 * Created by lyx on 16/9/27.
 */
@Component
public class PrescriptionProtocol extends CloudHospitalProtocol {

    @Resource
    private PrescriptionDao prescriptionDao;
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
        String patientCode = MapUtil.getString(params, "PatientCode");
        if (patientCode == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        List<Prescription> prescriptionInfo=prescriptionDao.prescriptionInfo(patientCode);
        if(prescriptionInfo.size()==0||prescriptionInfo==null)
        {
            throw new HospitalException("该病人还没有处方！", CloudHospitalException.PRESCRTPTION_IS_EMPTY);
        }
        return prescriptionInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.PRESCRTPTION_CODE;
    }
}
