package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.OrderRelation;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import com.jandar.cloud.hospital.dao.OrderRelationDao;
import com.jandar.cloud.hospital.dao.ReservationRecordDao;
import com.jandar.cloud.hospital.dao.SubmitTopicDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.util.StringUtil;
import org.bson.types.ObjectId;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 是否跟上次开一样的药
 * 根据patientCode，doctorCode取当天的任意一条预约记录。
 */
@Component
public class IsSameProtocol extends CloudHospitalProtocol {
    @Resource
    private OrderRelationDao orderRelationDao;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        Map map = new HashMap();
        String patientCode = MapUtil.getString(params, "PatientCode");   //病人代码
        if (patientCode == null) {
            throw new HospitalException("请求参数PatientCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String doctorCode = MapUtil.getString(params, "DoctorCode");    //医生代码
        if (doctorCode == null) {
            throw new HospitalException("请求参数DoctorCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //预约记录
        //Map<String, String>  mapAppoint = HospitalInfoService.getInstance().getAppoint(doctorCode, patientCode);
        OrderRelation orderRelation=orderRelationDao.findBydoctorIdAndPatientId(doctorCode, patientCode);
        if (orderRelation==null){
            throw new HospitalException("病人"+patientCode+"今天在您这没有预约！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //String issame = mapAppoint.get("issame");
        map.put("issame", "1");
        return  map;
    }

    @Override
    public String getProtocolCode() {
        return Content.IS_SAME_CODE;
    }

}
