package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DoctorInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by flyhorse on 2016/12/15.
 * 判断是否是复诊病人
 */
@Component
public class IsRepeatProtocol extends CloudHospitalProtocol {
    @Resource
    private PatientDao patientDao;

    @Autowired
    private UserService userService;
    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {

        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        if (userInfo == null) {
            throw new HospitalException("还未建档", HospitalException.UNARCHIV);
        }
        String patientId = MapUtil.getString(params, "PatientId");
        if (patientId == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //这个页面需要做个mongodb缓存
        System.out.println("============IsRepeatProtocol========"+patientId);
        Patient patient=new Patient();
        patient.setAlipayUserId(userInfo.getAlipayUserId());
        patient.setCurrentWay("Alipay");
        patient.setPatientCode(userInfo.getBrid());
        patient.setName(userInfo.getYhxm());
        patientDao.syncPatient(patient);




        //先模拟
        Map rstMap=new HashMap();
        boolean isRepeat = HospitalInfoService.getInstance().getIsRepeat(patientId);

        if(isRepeat)
          rstMap.put("IsRepeat","Yes");
        else
          rstMap.put("IsRepeat","No");

        return rstMap;

    }


    @Override
    public String getProtocolCode() {
        return Content.IS_REPEAT_CODE;
    }
}
