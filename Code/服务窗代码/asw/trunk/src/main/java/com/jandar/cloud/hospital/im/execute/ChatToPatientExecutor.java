package com.jandar.cloud.hospital.im.execute;

import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.LeaveMessageDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.im.MsgModel;
import com.jandar.cloud.hospital.im.SendType;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;

/**
 * Created by flyhorse on 2016/11/2.
 * 与病人聊天
 */
@Component
public class ChatToPatientExecutor extends ImExecutor{


    @Resource
    PatientDao patientDao;

    /***
     * 两个参数，第一个表示 patientCode  第二个表示聊天内容
     * */
    public MsgModel execute(String ... args)
    {
        System.out.println("==============ChatToPatientExecutor===================="+args[0]);
        if(args.length<3)
        {
            System.out.println("========ChatToPatientExecutor======not enough param===========");
            return null;
        }

        MsgModel retModel=new MsgModel();//返回的

        String patientCode=args[0];
        String doctorCode=args[1];
        String msg=args[2];
        retModel.setMessage(msg);
        Patient patient=patientDao.findPatientBasic(patientCode);
        if(patient==null)
        {
            System.out.println("==============ChartToPatientExecutor=========Patient not found===========");
            return retModel;
        }

            //暂时只支持alipay
        String alipayUserId=patient.getAlipayUserId();
        if(alipayUserId!=null)//有支付宝
        {
            retModel.setSendType(SendType.ALIPAY);
            retModel.setAlipayUserId(alipayUserId);
        }
               //更新掉病人表中的当前医生
        if(doctorCode!=null)
        {
            patientDao.updateCurrentDoctorCode(patientCode,doctorCode);
        }

        System.out.println("===============ChatToPatientExecutor=============alipayUserId"+alipayUserId+" code:"+patient.getPatientCode()+" userid:"+patient.getPatientCode());

        return retModel;
    }
}
