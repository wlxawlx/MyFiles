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
 * Created by flyhorse on 2016/11/3.
 * 住院单同步
 */
@Component
public class InhospitalNotifyExecutor extends ImExecutor{

    @Resource
    PatientDao patientDao;
    @Autowired
    private InspectionSyncJob inspectionSyncJob;

    public MsgModel execute(String ... args)
    {
        System.out.println("==============InhospitalNotifyExecutor===================="+args[0]);
        if(args.length<2)
        {
            System.out.println("========InhospitalNotifyExecutor======not enough param===========");
            return null;
        }

        MsgModel retModel=new MsgModel();//返回的
        String patientCode=args[0];
        Patient patient=patientDao.findPatientBasic(patientCode);
        if(patient==null)
        {
            System.out.println("==============InhospitalNotifyExecutor=========Patient not found===========");
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
        retModel.setMessage("医生给您开了住院单");
        return retModel;
    }

}
