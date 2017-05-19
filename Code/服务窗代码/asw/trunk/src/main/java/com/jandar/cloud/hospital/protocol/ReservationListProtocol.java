package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.ReservationRecord;
import com.jandar.cloud.hospital.dao.ReservationRecordDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.StringUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.List;
import java.util.Map;

/**
 * 预约列表
 * Created by lyx on 2016/10/19.
 */
@Component
public class ReservationListProtocol extends CloudHospitalProtocol {
    @Resource
    private ReservationRecordDao reservationRecordDao;
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
        String patientCode = MapUtil.getString(params, "PatientCode");
        String status = MapUtil.getString(params, "Status");
        String payStatus = MapUtil.getString(params, "PayStatus");
        if (StringUtil.isBlank(patientCode)) {
            throw new HospitalException("请求参数PatientCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        List<ReservationRecord> reservationList = reservationRecordDao.findReservationList(patientCode, status, payStatus);
        return reservationList;
    }

    @Override
    public String getProtocolCode() {
        return Content.RESERVATION_LIST_CODE;
    }
}
