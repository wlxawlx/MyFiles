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
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 预约查询（CH010702），查询全部预约记录，根据第几页和分页大小
 * Created by admin on 2016/9/29.
 */
@Component
public class ReservationMultQueryProtocol extends CloudHospitalProtocol{
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
        String patientCode = info.getPatientCode();
        Integer pageIndex = MapUtil.getInteger(params, "PageIndex");
        Integer pageSize = MapUtil.getInteger(params, "PageSize");
        List<ReservationRecord> reservationRecordList = reservationRecordDao.findByPatientCodePage(patientCode,pageIndex,pageSize);
        double count = (double)reservationRecordDao.count(patientCode);
        int pageCount = (int)Math.ceil(count/pageSize);
        Map<String, Object> map = new HashMap<String, Object>();
        map.put("pageIndex", pageIndex);
        map.put("pageCount", pageCount);
        map.put("reservationRecordList",reservationRecordList);
        return map;
    }
    @Override
    public String getProtocolCode() {
        return Content.RESERVATION_MULT_QUERY_CODE;
    }
}
