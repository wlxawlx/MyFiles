package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.LeaveMessage;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.LeaveMessageDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.collections.map.HashedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import javax.annotation.Resource;
import java.util.Map;
import com.jandar.cloud.hospital.dao.PrescriptionDao;


/**
 * 处方缴费确认信息展示
 * Created by jhf on 2016/10/19.
 */
@Component
public class PrescriptionPaymentInfoProtocol extends CloudHospitalProtocol{
    @Resource
    private PrescriptionDao prescriptionDao;
    @Autowired
    private UserService userService;
    @Autowired
    private LeaveMessageDao leaveMessageDao;

    @Override
    public String getProtocolCode() {
        return Content.PRESCRTPTION_PAYMENT_INFO_CODE;
    }

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
        Prescription prescription = prescriptionDao.getPaymentInfoByCode(code);
        //留言
        LeaveMessage leaveMessage = leaveMessageDao.findByCodeAndType(prescription.getCode(),"CFD");
        String leaveMessageStatus = leaveMessage==null ? null : leaveMessage.getIsDeal();
        Map<String,Object> prescriptionMap = new HashedMap();
        prescriptionMap.put("leaveMessageStatus",leaveMessageStatus);
        prescriptionMap.put("Code",prescription.getCode());
        prescriptionMap.put("DoctorName",prescription.getDoctorName());
        prescriptionMap.put("PrescriptionSum",prescription.getPrescriptionSum());
        prescriptionMap.put("TotalSum",prescription.getTotalSum());
        prescriptionMap.put("PrescriptionName",prescription.getPatientName());
        prescriptionMap.put("SpecialType",prescription.getSpecialType());
        prescriptionMap.put("IsExpress",prescription.getIsExpress());
        prescriptionMap.put("ReceiveInfo",prescription.getReceiveInfo());
        return prescriptionMap;
    }
}
