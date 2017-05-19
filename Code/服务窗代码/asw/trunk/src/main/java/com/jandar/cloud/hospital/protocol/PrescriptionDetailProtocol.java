package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 处方明细
 * Created by lyx on 16/9/28.
 */
@Component
public class PrescriptionDetailProtocol extends CloudHospitalProtocol {

    @Resource
    private PrescriptionDao prescriptionDao;
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
            throw new HospitalException("请求Code参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        Prescription prescription = prescriptionDao.prescriptionInfoByCode(code);
        if (prescription == null) {
            throw new HospitalException("处方不存在！", CloudHospitalException.PRESCRTPTIONINFO_IS_EMPTY);
        }
        List<Prescription.MedicinalInfo> medicinalInfoList = prescription.getMedicinalInfo();
        return medicinalInfoList;
    }

    @Override
    public String getProtocolCode() {
        return Content.PRESCRTPTION_DETAIL_CODE;
    }
}
