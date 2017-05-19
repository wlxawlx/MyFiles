package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.InhospitalDao;
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
 * 住院单
 * Created by lyx on 2016/9/28.
 */
@Component
public class InhospitalProtocol extends CloudHospitalProtocol {


    @Resource
    private InhospitalDao inhospitalDao;
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
        List<Inhospital> inhospitalinfo = inhospitalDao.inhospitalifo(patientCode);
        if (inhospitalinfo.size() == 0 || inhospitalinfo == null)
        {
            throw new HospitalException("该病人无住院单信息！", CloudHospitalException.INHOSPITALINFO_IS_EMPTY);
        }
            return inhospitalinfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.INHOSPITAL_CODE;
    }
}
