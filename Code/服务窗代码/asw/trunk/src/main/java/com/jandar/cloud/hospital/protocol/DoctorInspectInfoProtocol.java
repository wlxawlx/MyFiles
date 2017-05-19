package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Schedule;
import com.jandar.cloud.hospital.dao.DoctorDao;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.ScheduleDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.DateUtil;
import com.sun.xml.internal.messaging.saaj.util.FinalArrayList;
import org.apache.commons.collections.map.LinkedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 专科医院医生预约选择列表
 * Created by lyx on 16/10/12.
 */
@Component
public class DoctorInspectInfoProtocol extends CloudHospitalProtocol {

    @Resource
    private DoctorDao doctorDao;

    @Resource
    private ScheduleDao scheduleDao;
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
        String doctorCode = MapUtil.getString(params, "DoctorCode");
        if (doctorCode == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        Doctor doctor = doctorDao.doctorInfo(doctorCode);
        if(doctor == null)
            throw new HospitalException("医生代码不存在！", CloudHospitalException.REQUEST_IS_EMPTY);
        List<Schedule> scheduleList = scheduleDao.findByDoctorCode(doctorCode);
        List<Object>infolist = new ArrayList();
        Date date =new Date();
        String dateToday = DateUtil.formatTime1(date);
        for (Schedule schedule : scheduleList) {
            if(schedule.getDate().compareTo(dateToday)>=0)
            infolist.add(schedule);
            schedule.setCode(null);
            schedule.setDeptName(null);
            schedule.setDoctorCode(null);
            schedule.setDeptCode(null);
            schedule.setDoctorName(null);
            schedule.setValid(null);
        }
        Map<String,Object> temps = new LinkedMap();
        /*temps.put("Title", doctor.getTitle());
        temps.put("Photo", doctor.getPhoto());
        temps.put("AdeptAt", doctor.getAdeptAt());
        temps.put("Resume", doctor.getResume());
        temps.put("DoctorName",doctor.getName());
        temps.put("DeptName",doctor.getDeptName());
        temps.put("DoctorCode",doctor.getCode());*/
        temps.put("Schedule",infolist);
        return temps;

    }

    @Override
    public String getProtocolCode() {
        return Content.DOCTOR_INSPECTINFO_CODE;
    }
}
