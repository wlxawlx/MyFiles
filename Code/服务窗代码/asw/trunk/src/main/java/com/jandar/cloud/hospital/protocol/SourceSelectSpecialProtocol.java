package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.ReservationRecord;
import com.jandar.cloud.hospital.bean.Schedule;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import com.jandar.cloud.hospital.dao.ReservationRecordDao;
import com.jandar.cloud.hospital.dao.ScheduleDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.StringUtil;
import org.apache.commons.collections.map.LinkedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 专科医院预约号源选择CH012301
 * Created by admin on 2016/10/14.
 */
@Component
public class SourceSelectSpecialProtocol extends CloudHospitalProtocol{
    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;
    @Resource
    private ScheduleDao scheduleDao;
    @Resource
    private PatientDao patientDao;
    @Autowired
    private UserService userService;
    @Autowired
    private ReservationRecordDao reservationRecordDao;
    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }

        String doctorCode = MapUtil.getString(params, "DoctorCode");
        String dateDay = MapUtil.getString(params, "DateDay");
        Integer number = MapUtil.getInteger(params, "Number");
        String visitType = MapUtil.getString(params, "VisitType");
        String illnessCode = MapUtil.getString(params, "IllnessCode");
        String illnessName = MapUtil.getString(params, "IllnessName");
        String sourceTime = MapUtil.getString(params, "SourceTime");
        String submitTopicId = MapUtil.getString(params, "SubmitTopicId");

        if(StringUtil.isBlank(doctorCode)){
            throw new HospitalException("参数DoctorCode不能为空！", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        }
        if(StringUtil.isBlank(dateDay)){
            throw new HospitalException("参数DateDay不能为空！", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        }
        if(number==null){
            throw new HospitalException("参数Number不能为空！", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        }
        if(StringUtil.isBlank(sourceTime)){
            throw new HospitalException("参数SourceTime不能为空！", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        }
        String patientCode = info.getPatientCode();//MapUtil.getString(params, "PatientCode");
        String status = scheduleDao.findSourceStatus(doctorCode,dateDay,number);
        //判断号源状态，未预约过的则锁定，并且更改剩余号源数
        if("1".equals(status)||"3".equals(status))
            throw new HospitalException("本号源已经被预约", CloudHospitalException.SOUTCE_ALREADY_LOCK);
        if(status == null)
            throw new HospitalException("号源不存在", CloudHospitalException.SOUTCE_IS_NOT_EXIST);
        if(patientDao.findAllDoctorCode(patientCode).isEmpty())
            throw new HospitalException("该病人没有就诊记录", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        if(StringUtil.isBlank(visitType)){
            throw new HospitalException("参数VisitType不能为空！", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        }else if(!("FZKY".equals(visitType)||"JCYY".equals(visitType)||"ZYSQ".equals(visitType)||"ZXWZ".equals(visitType))){
            throw new HospitalException("参数VisitType必须是FZKY、JCYY、ZYSQ、ZXWZ中的一个！", CloudHospitalException.ILLNESS_RECORD_IS_EMPTY);
        }
        scheduleDao.updateSourceStatus(doctorCode,dateDay,number);   //修改号源状态为锁定
        //生成预约单
        Schedule schedule = scheduleDao.findOne(doctorCode,dateDay);
        ReservationRecord reservationRecord = new ReservationRecord();
        String reservationRecordCode = String.valueOf(System.currentTimeMillis())+Math.round(Math.random()*1000000000);   //**生成预约代码**临时方案
        reservationRecord.setCode(reservationRecordCode);
        reservationRecord.setPatientCode(patientCode);
        reservationRecord.setPatientName(info.getName());
        reservationRecord.setVisitType(visitType);
        reservationRecord.setIllnessCode(illnessCode);
        reservationRecord.setIllnessName(illnessName);
        reservationRecord.setDoctorCode(doctorCode);
        reservationRecord.setDoctorName(schedule.getDoctorName());
        reservationRecord.setDoctorImUser(doctorCode);
        reservationRecord.setDeptCode(schedule.getDeptCode());
        reservationRecord.setDeptName(schedule.getDeptName());
        reservationRecord.setCreateTime(new Date());
        reservationRecord.setSourceTime(sourceTime);
        reservationRecord.setNumber(number);
        reservationRecord.setFee(schedule.getFee());
        reservationRecord.setSubmitTopicId(submitTopicId);
        reservationRecord.setStatus("Novisit");
        reservationRecord.setPayStatus("None");
        reservationRecordDao.save(reservationRecord);    //新增预约记录
//        String dateTime = scheduleDao.backDateTime(doctorCode,dateDay,number);
        //拼接传递数据字符串 schuedule
        String temp = doctorCode+"#"+dateDay+"#"+number.toString();
        //拼凑成JSON格式返回
        Map<String,Object> backDoctorScheduleSpecials = new LinkedMap();
        backDoctorScheduleSpecials.put("Temp",temp);
        return backDoctorScheduleSpecials;
    }
    @Override
    public String getProtocolCode() {
        return Content.SOURCE_SELECT_SPECIAL_CODE;
    }
}
