package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.dao.SubmitTopicDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.bson.types.ObjectId;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Example;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获取近况题目协议
 * 可通过科室来获得题目
 * Created by lyx on 16/9/21.
 */
@Component
public class SubmitTopicProtocol extends CloudHospitalProtocol {

    @Resource
    private SubmitTopicDao submitTopicDao;

    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;

    @Autowired
    private UserService userService;

    @Autowired
    private PatientDao patientDao;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }

        List<Patient> re = patientDao.findByPatientCode(info.getPatientCode());
        if (re == null || re.size() == 0) {
            throw new HospitalException("病人不存在！", CloudHospitalException.PATIENTIS_EMPTY);
        }
        Patient patientinfo = re.get(0);

        SubmitTopic submitTopic = new SubmitTopic();
        /**
         * 添加提交问卷的病人的姓名和id
         */
        SubmitTopic.Patients patients = new SubmitTopic.Patients();
        patients.setName(patientinfo.getName());
        patients.setUserId(patientinfo.getPatientCode());
        submitTopic.setPatient(patients);
        /**
         * 添加问卷的代码和标题，答题时间
         */
        String code = params.get("Code")==null ? null : params.get("Code").toString();
        if (code == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String title = params.get("Title")==null ? null : params.get("Title").toString();
        if (title == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        submitTopic.setCode(code);
        submitTopic.setTile(title);
        submitTopic.setDate(new Date());
        /**
         * 判断疾病代码
         */
        String illnessCode = MapUtil.getString(params, "IllnessCode");
        if (illnessCode != null) {
            List<ChronicIllnessTopic> all = chronicIllnessTopicDao.findByIllness_Code(illnessCode);
            if (all.size() == 0 || all == null) {
                throw new HospitalException("疾病代码不存在！", CloudHospitalException.ILLNESS_IS_EMPTY);
            }
            for (ChronicIllnessTopic chronicIllnessTopic : all) {
                List<ChronicIllnessTopic.illness> illnessList = chronicIllnessTopic.getIllness();
                for (ChronicIllnessTopic.illness illness : illnessList) {
                    submitTopic.setIllnessName(illness.getName());
                    submitTopic.setIllnessCode(illness.getCode());
                }
            }
        }

//        chronicIllnessTopic
//        if (deptCode != null) {
//            List<ChronicIllnessTopic> all = chronicIllnessTopicDao.findByDept_Code(deptCode);
//            if (all.size() == 0 || all == null) {
//                throw new HospitalException("部门代码不存在！", CloudHospitalException.DEPT_IS_EMPTY);
//            }
//            for (ChronicIllnessTopic chronicIllnessTopic : all) {
//                List<ChronicIllnessTopic.dept> deptList = chronicIllnessTopic.getdept();
//                for (ChronicIllnessTopic.dept dept : deptList) {
//                    submitTopic.setDeptCode(dept.getName());
//                    submitTopic.setDeptName(dept.getCode());
//                }
//            }
//        }
        /**
         * 添加回答
         */
        List<SubmitTopic.Items> items = (List<SubmitTopic.Items>) params.get("Items");
        if (items == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        submitTopic.setItems(items);
        SubmitTopic inEntity=  submitTopicDao.save(submitTopic);
        ObjectId id= submitTopicDao.findByPatientId(inEntity.getPatient().getUserId());
        System.out.println("!!!!!!!!!!!!!!!!!********************************************************************"+id);
        Map<String, Object> res = new HashMap<String, Object>();
        res.put("submittopicid",id.toHexString());
        return res;
    }

    @Override
    public String getProtocolCode() {
        return Content.TOPIC_SUBMIT_CODE;
    }
}
