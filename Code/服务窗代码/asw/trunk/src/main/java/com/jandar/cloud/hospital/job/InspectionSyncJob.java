package com.jandar.cloud.hospital.job;

import javax.annotation.Resource;

import org.springframework.stereotype.Component;

import com.jandar.alipay.core.HospitalException;
//import com.jandar.cloud.hospital.SpringManager;
import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.cloud.hospital.dao.InspectionDao;


/**
 * 检查单同步的job
 * */
@Component
public class InspectionSyncJob {

    @Resource
    private InspectionDao inspectionDao;

    /**
     * 同步一张检查单
     * */
    public void syncInspection(String code)
    {
                System.out.println("--this is blank-sync a inspetion:"+code);
    }
//    public static void main(String[] args) {
//        InspectionSyncJob job=(InspectionSyncJob)SpringManager.getBean("inspectionSyncJob");
//        job.syncInspection("JCD201610211132567");
//    }



}
