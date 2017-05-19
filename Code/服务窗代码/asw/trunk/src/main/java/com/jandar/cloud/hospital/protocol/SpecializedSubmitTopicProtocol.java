package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.*;
import com.jandar.cloud.hospital.dao.*;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.collections.map.HashedMap;
import org.apache.commons.collections.map.LinkedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 专科医院提交问卷答案 CH012401
 * Created by lyx on 16/10/14.
 */
@Component
public class SpecializedSubmitTopicProtocol extends CloudHospitalProtocol {
    @Resource
    private SubmitTopicDao submitTopicDao;
    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;
    @Autowired
    private PatientDao patientDao;
    @Autowired
    private DoctorDao doctorDao;
    @Autowired
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

        String temp = MapUtil.getString(params, "Temp");
        String patientCode = MapUtil.getString(params,"PatientCode");
        if(temp == null || patientCode == null)
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        //临时变量PatientCode#DoctorCode#DateDay#DateTime#Number
        String[] str = temp.split("#");
        String doctorCode = str[0];
        String dateDay = str[1];
        Integer number = Integer.parseInt(str[2]);
        temp = str[0]+"#"+str[1]+"#"+str[2];

        String dateTime = scheduleDao.backDateTime(doctorCode,dateDay,number);
        if (dateTime == null) {
            throw new HospitalException("传输参数有问题，号源不存在", CloudHospitalException.SOUTCE_IS_NOT_EXIST);
        }
        String illnessName = null;
        SubmitTopic submitTopic = new SubmitTopic();
        /**
         * 添加疾病代码、名称、病人代码和名称
         */
        Patient patientInfo = patientDao.findPatientInfo(patientCode);
        if (patientInfo == null) {
            throw new HospitalException("病人不存在！", CloudHospitalException.PATIENTIS_EMPTY);
        }
        SubmitTopic.Patients patient = new SubmitTopic.Patients();
        patient.setUserId(patientInfo.getPatientCode());
        patient.setName(patientInfo.getName());
        submitTopic.setPatient(patient);
        /**
         * 添加问卷代码、标题、时间
         */
        String topicCode = MapUtil.getString(params, "TopicCode");
        ChronicIllnessTopic topicInfo = chronicIllnessTopicDao.findTopicInfo(topicCode);
        if (topicInfo == null) {
            throw new HospitalException("问卷不存在！", CloudHospitalException.TOPIC_IS_EMPTY);
        }
        submitTopic.setDate(new Date());
        submitTopic.setCode(topicInfo.getCode());
        submitTopic.setTile(topicInfo.getTile());
        /**
         * 添加答案列表
         */
        List<SubmitTopic.Items> items = (List<SubmitTopic.Items>) params.get("AnswerItems");
        if (items == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        submitTopic.setItems(items);
        submitTopicDao.save(submitTopic);
        List<Map<String, String>> backList = new ArrayList<>();
        Map<String, String> map = new LinkedMap();
        map.put("Temp", temp);
        map.put("IllnessName", illnessName);
        Doctor doctorInfo = doctorDao.findDoctorInfo(doctorCode);
        //map.put("DeptName", doctorInfo.getDeptName());
        map.put("DoctorName", doctorInfo.getName());
        List<Schedule> scheduleList = scheduleDao.findByScheduleInfo(doctorCode);
        for (Schedule schedule : scheduleList) {
            if (dateDay.equals(schedule.getDate())) {
                map.put("Fee", schedule.getFee());
                List<Schedule.Sources> sourceList = schedule.getSources();
                for (Schedule.Sources source : sourceList) {
                    if (source.getTime().equals(dateTime)) {
                        map.put("Sourcetime", dateDay +" "+source.getTime());
                    }
                }
            }
        }
        backList.add(map);
        return backList;
    }
    @Override
    public String getProtocolCode() {
        return Content.SPECIALIZDE_SUBMITTOPIC_CODE;
    }
}
