package com.jandar.cloud.hospital.service;

import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;

/**
 * Created by flyhorse on 2016/12/20.
 */
@Service
public class TestService {

    @Resource
    private PatientDao patientDao;
    public Patient getCurPatientInfo() {
        Patient info=patientDao.findPatientInfo("P001");
        System.out.println("=================:"+info.getName());

        return  info;
    }
}
