package com.jandar.handle.message;

import cloud.hospital.cloudend.message.base.AbsMessage;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.OutMessage;
import com.jandar.cloud.hospital.bean.MedicalRecords;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.MedicalRecordsDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.im.IMClient;
import com.jandar.util.SpringBeanUtil;


/**
 * 聊天信息处理的基本类
 * Created by zzw on 16/8/31.
 */
public abstract class ChatMessageHandler {

    protected PatientDao patientDao = SpringBeanUtil.getBean(PatientDao.class);
    protected MedicalRecordsDao medicalRecordsDao = SpringBeanUtil.getBean(MedicalRecordsDao.class);

    /**
     * 获得用户信息
     *
     * @param fromUser alipayUserId 或是 wechatusername
     * @param type     类型
     * @return 用户信息
     */
    protected Patient getUserInfo(String fromUser, PlatformType type) {
        Patient patient;
        if (type == PlatformType.Alipay) {
            patient = patientDao.findFirstByAlipayUserId(fromUser);
        } else {
            patient = patientDao.findFirstByWechatOpenId(fromUser);
        }
        return patient;
    }

    /**
     * 检查预约信息，只有当前有预约且在就诊的用户才可以发送消息
     *
     * @param userInfo 用户信息
     */
    protected void checkReservation(Patient userInfo) throws CloudHospitalException {

        if (userInfo == null) {
            throw new CloudHospitalException("请先建档");
        }

        String patientId = userInfo.getPatientCode();
        MedicalRecords medicalRecords=medicalRecordsDao.findByPatientId(patientId);
        if (medicalRecords == null) {
            // 请先预约
            throw new CloudHospitalException("请先预约");
        }

        // 就诊前一小时,或是在就诊中可以发送信息
        if (medicalRecords.getReservationState() != MedicalRecords.ReservationState.InADoctor) {
            // 预约状态不正常的模板消息给病人
            switch (medicalRecords.getReservationState()) {
                case NoReservation:
                    throw new CloudHospitalException("请先预约");
                case NoBegin:
                    throw new CloudHospitalException("请在就诊前一个小时之内给医生发送消息");
                case InADoctor:
                    break;
                case End:
                    throw new CloudHospitalException("就诊已结束,如有需要请给医生留言");
                case Overdue:
                    throw new CloudHospitalException("预约已过期,请重新预约");
            }
        }
    }

    protected AbsMessage build(AbsMessage message, Patient userInfo, PlatformType type) {
        String patientId = userInfo.getPatientCode();
        MedicalRecords medicalRecords=medicalRecordsDao.findByPatientId(patientId);
        String formUserId;
        String formName = userInfo.getName();
        if (type == PlatformType.Wechat) {
            formUserId = userInfo.getWechatOpenId();
//                formName = userInfo.getWechatUserName();
        } else {
            formUserId = userInfo.getAlipayUserId();
//                formName = userInfo.getName();
        }
        message.setFromUser(formUserId, formName);
        message.setToUser(medicalRecords.getDoctorCode(), medicalRecords.getDoctorName());
        return message;
    }

    protected void sendMessage(AbsMessage message, Patient userInfo, PlatformType type) throws Exception {
        // 就诊前一小时,或是在就诊中可以发送信息
        String patientId = userInfo.getPatientCode();
        MedicalRecords medicalRecords=medicalRecordsDao.findByPatientId(patientId);

        IMClient.getInstance().sendMessage(message, medicalRecords.getDoctorImUser());

    }

    protected OutMessage buildOutMessage(String type, String toUserId, String content) {
        OutMessage outMessage = new OutMessage();
        outMessage.setMsgType(type);
        outMessage.setToUserId(toUserId);
        outMessage.setContent(content);
        return outMessage;
    }
}
