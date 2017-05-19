package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import javax.annotation.Resource;
import java.util.Map;
/**
 * 检查单列表(CH011201)
 * Created by admin on 2016/9/28.
 */
@Component
public class InspectionProtocol extends CloudHospitalProtocol {

    @Resource
    private InspectionDao inspectionDao;
    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {

        System.out.println("=============InspectionProtocol=======doProcess=========================");
        //取得用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String patientCode = info.getPatientCode();
        return inspectionDao.backMessage(patientCode);
    }

    @Override
    public String getProtocolCode() {return Content.INSPECTION_CODE;}
}
