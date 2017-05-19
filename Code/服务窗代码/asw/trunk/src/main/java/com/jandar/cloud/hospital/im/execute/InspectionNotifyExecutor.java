package com.jandar.cloud.hospital.im.execute;

import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.im.MsgModel;
import com.jandar.cloud.hospital.im.SendType;
import com.jandar.cloud.hospital.job.InspectionSyncJob;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;

/**
 * Created by flyhorse on 2016/11/2.
 * 检查单通知
 */
@Component
public class InspectionNotifyExecutor extends ImExecutor{

    @Resource
    PatientDao patientDao;
    @Autowired
    private InspectionSyncJob inspectionSyncJob;
    public MsgModel execute(String ... args)
    {
        System.out.println("==============InspectionNotifyExecutor===================="+args[0]);
        if(args.length<2)
        {
            System.out.println("========InspectionNotifyExecutor======not enough param===========");
            return null;
        }

        MsgModel retModel=new MsgModel();//返回的
        String patientCode=args[0];
        Patient patient=patientDao.findPatientBasic(patientCode);
        if(patient==null)
        {
            System.out.println("==============InspectionNotifyExecutor=========Patient not found===========");
            return retModel;
        }
        String code=args[1];//检查单编号
        inspectionSyncJob.syncInspection(code);
        //暂时只支持alipay
        String alipayUserId=patient.getAlipayUserId();
        if(alipayUserId!=null)//有支付宝
        {
            retModel.setSendType(SendType.ALIPAY);
            retModel.setAlipayUserId(alipayUserId);
        }
        retModel.setMessage("医生给您开了检查单");
        return retModel;
    }
}
