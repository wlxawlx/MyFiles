package com.jandar.cloud.hospital.im.execute;

import com.jandar.cloud.hospital.bean.OrderRelation;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.OrderRelationDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.im.MsgModel;
import com.jandar.cloud.hospital.im.SendType;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;

/**
 * Created by flyhorse on 2016/11/2.
 * 医生呼叫病人
 */
@Component
public class CallPatientExecutor extends ImExecutor{

    @Resource
    PatientDao patientDao;
    @Resource
    OrderRelationDao orderRelationDao;
    public MsgModel execute(String ... args)
    {
        if(args.length<2)
        {
            System.out.println("========CallPatientExecutor======not enough param===========");
            return null;
        }
        System.out.println("==============CallPatientExecutor===================="+args[0]);
        MsgModel retModel=new MsgModel();//返回的

        String patientCode=args[0];
        String doctorCode=args[1];
        OrderRelation orderRelation = orderRelationDao.findBydoctorIdAndPatientId(doctorCode, patientCode);
        if(orderRelation==null){
            System.out.println("========CallPatientExecutor======orderRelation not found===========");
            return null;
        }
        String ownerPatientId = orderRelation.getOwnerpatientid();
        Patient patient=patientDao.findPatientBasic(ownerPatientId);

        if(patient==null)
        {
            System.out.println("==============ChartToPatientExecutor=========Patient not found===========");
            return retModel;
        }
        retModel.setMessage("医生正在呼叫您，请您回到聊天室");
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


        return retModel;
    }
}
