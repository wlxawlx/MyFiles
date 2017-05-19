package com.jandar.cloud.hospital.protocol;

import cloud.hospital.cloudend.message.TemplateMessage;
import cloud.hospital.cloudend.message.TextMessage;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.*;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.dao.LeaveMessageDao;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.im.IMClient;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Date;
import java.util.Map;

/**
 * 检查单给医生留言
 * Created by lyx on 2016/10/19.
 */
@Component
public class LeaveMessageProtocol extends CloudHospitalProtocol {

    @Resource
    private LeaveMessageDao leaveMessageDao;

    @Resource
    private InspectionDao inspectionDao;

    @Resource
    private PrescriptionDao prescriptionDao;

    @Resource
    private InhospitalDao inhospitalDao;

    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String code = MapUtil.getString(params, "Code");
        if (code == null) {
            throw new HospitalException("请求参数Code为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String content = MapUtil.getString(params, "Content");
        if (content == null) {
            throw new HospitalException("请求参数Content为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String type = MapUtil.getString(params, "Type");
        if (type == null || type.equals("")) {
            throw new HospitalException("留言类型Type不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        LeaveMessage leaveMessage = new LeaveMessage();
        leaveMessage.setType(type);
        leaveMessage.setCode(code);
        String patientCode = null;
        String patientName = null;
        String doctorCode = null;
        String doctorName = null;
        if("JCD".equals(type)){        //检查单
            InspectMain inspectionInfo = inspectionDao.findByCode(code);
            patientCode = inspectionInfo.getPatientCode();
            patientName = inspectionInfo.getPatientName();
            doctorCode = inspectionInfo.getDoctorCode();
            doctorName = inspectionInfo.getDoctorName();
        }else if("CFD".equals(type)){  //处方单
            Prescription prescription = prescriptionDao.findByCode(code);
            patientCode = prescription.getPatientCode();
            patientName = prescription.getPatientName();
            doctorCode = prescription.getDoctorCode();
            doctorName = prescription.getDoctorName();
        }else if("ZYD".equals(type)){  //住院单
            Inhospital inhospital = inhospitalDao.findByCode(code);
            patientCode = inhospital.getPatientCode();
            patientName = inhospital.getPatientName();
            doctorCode = inhospital.getDoctorCode();
            doctorName = inhospital.getDoctorName();
        }else{
            throw new HospitalException("留言类型Type错误！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        leaveMessage.setPatientCode(patientCode);
        leaveMessage.setPatientName(patientName);
        leaveMessage.setDoctorCode(doctorCode);
        leaveMessage.setDoctorName(doctorName);
        leaveMessage.setIsDeal("false");
        leaveMessage.setLeaveDate(new Date());
        leaveMessage.setLeaveContent(content);
        leaveMessageDao.save(leaveMessage);
        //发送即时消息

        TemplateMessage message = new TemplateMessage();
        message.setFromUser(patientCode,patientName);
        message.setToUser(doctorCode,doctorName);

        String imMessage = patientName+"对检查单进行了留言,内容如下："+content;
        message.setBody(imMessage);
        try {
            System.out.println("------------------send words---------"+doctorCode);
            IMClient.getInstance().sendMessage(message, doctorCode);
        } catch (Exception e) {
            e.printStackTrace();
        }

        return null;
    }

    @Override
    public String getProtocolCode() {
        return Content.LEAVE_MESSAGE_CODE;
    }
}
