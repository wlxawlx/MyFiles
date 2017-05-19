package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.ReservationRecord;
import com.jandar.cloud.hospital.dao.ReservationRecordDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.List;
import java.util.Map;

/**
 * 预约详情
 * Created by lyx on 2016/10/19.
 */
@Component
public class ReservationInfoProtocol extends CloudHospitalProtocol {
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
        String code = MapUtil.getString(params, "Code");
        if (code == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        ReservationRecord reservationInfo = reservationRecordDao.findReservationInfo(code);
        return reservationInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.RESERVATION_INFO_CODE;
    }
}
