package com.jandar.cloud.hospital.im.execute;

import com.jandar.cloud.hospital.bean.OrderRelation;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.OrderRelationDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.im.MsgModel;
import com.jandar.cloud.hospital.im.SendType;
import com.jandar.cloud.hospital.job.InspectionSyncJob;
import com.jandar.cloud.hospital.msg.MessageTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.HashMap;
import java.util.Map;

/**
 * 缴费通知
 */
@Component
public class PaymentNotifyExecutor extends ImExecutor{
    @Resource
    OrderRelationDao orderRelationDao;
    @Resource
    PatientDao patientDao;
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
        String doctorCode=args[1];
        OrderRelation orderRelation = orderRelationDao.findByPatientId(patientCode);
        String ownerPatientId = orderRelation.getOwnerpatientid();
        Patient patient=patientDao.findPatientBasic(ownerPatientId);
        System.out.println("缴费通知-------------patientCode:"+patientCode+"----doctorCode:"+doctorCode+"----ownerPatientId:"+ownerPatientId+"----patient:"+patient);
        if(patient==null)
        {
            System.out.println("==============InspectionNotifyExecutor=========Patient not found===========");
            return retModel;
        }
        //暂时只支持alipay
        String alipayUserId=patient.getAlipayUserId();
        if(alipayUserId!=null)//有支付宝
        {
            retModel.setSendType(SendType.ALIPAY);
            retModel.setAlipayUserId(alipayUserId);
        }
        //retModel.setMessage("缴费通知-测试");
        //发送通知消息
        Map<String, String > map = new HashMap<String, String>();
        map.put("AlipayUserId", patient.getAlipayUserId());
        map.put("PatientName", patient.getName());
        map.put("url", "http://192.68.68.88:8088/cloud/index.html#/payment/list/"+ownerPatientId+"?_k=jqnc1u");
        MessageTemplate.paymentMessageTemplate(map);
        return retModel;
    }
}
