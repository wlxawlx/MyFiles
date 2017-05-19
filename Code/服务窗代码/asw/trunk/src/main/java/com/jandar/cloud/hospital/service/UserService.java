package com.jandar.cloud.hospital.service;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.config.ConfigHandler;
import com.jandar.filter.auth.util.ThirdUserInfo;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import javax.servlet.http.HttpServletRequest;

/**
 * 获得云医院用户信息类
 * Created by zzw on 16/9/21.
 */
@Service
public class UserService {

    @Resource
    private PatientDao patientDao;




    public Patient getCurPatientInfo() {
        Patient info;
        if (ConfigHandler.systemIsTest()) {
            System.out.println("=============UserService=======isTest=========================");

            HttpServletRequest req=TestSession.getRequest();
            if(req==null)
                return  null;
            ThirdUserInfo userInfo= TestSession.getThirdUserInfo(req);
            if(userInfo==null)
                return null;
            System.out.println("=============UserService=======isTest=======================userInfo.getCode()=="+userInfo.getCode());
            info = patientDao.findPatientInfo(userInfo.getCode());
            System.out.println("=============UserService=======find user========================="+info.getPatientCode());

        } else {
            ThirdUserInfo thirdUserInfo = ServiceContext.getThirdUserInfo();
            System.out.println("thirdUserInfo::"+thirdUserInfo);
            if (thirdUserInfo.getUserType() == PlatformType.Wechat) {
                info = patientDao.findFirstByWechatOpenId(thirdUserInfo.getOpenId());
            } else {
                info = patientDao.findFirstByAlipayUserId(thirdUserInfo.getOpenId());
            }
        }

        return info;
    }

    /***
     * 根据病人编号获取病人信息
     * */
    public Patient getPatient(String patientCode)
    {
        return patientDao.findPatientInfo(patientCode);
    }
}
