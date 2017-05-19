package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.dao.DepartmentDao;
import com.jandar.cloud.hospital.dao.DoctorDao;
import com.jandar.cloud.hospital.dao.TokenDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by flyhorse on 2016/11/8.
 */
@Component
public class PlugGetTokenProtocol extends CloudHospitalProtocol{


    @Resource
    private DoctorDao doctorDao;
    @Resource
    private TokenDao tokenDao;



    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {

        String doctorCode = MapUtil.getString(params, "DoctorCode");
        if (doctorCode == null) {
            throw new HospitalException("请求参数DoctorCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }

        Doctor doctor=doctorDao.doctorInfo(doctorCode);
        if(doctor==null)
            throw new HospitalException("DoctorCode错误！", CloudHospitalException.REQUEST_IS_EMPTY);

        String doctorId=doctor.getId().toString();

        String tokenStr=tokenDao.getTokenByDoctorId(doctorId,doctor.getUserName(),doctor.getName(),"6");//6表示 医生端

        Map result=new HashMap();
        result.put("token",tokenStr);

        return result;

    }

    @Override
    public String getProtocolCode() {
        return Content.PLUG_IN_GET_TOKEN_CODE;
    }
}
