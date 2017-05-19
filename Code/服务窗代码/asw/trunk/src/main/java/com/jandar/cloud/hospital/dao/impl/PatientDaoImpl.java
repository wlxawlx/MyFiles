package com.jandar.cloud.hospital.dao.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.util.DateUtil;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.util.StringUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Pageable;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 病人自定义查询实现
 * Created by zzw on 16/9/21.
 */
//@Service
public class PatientDaoImpl {
    @Resource
    private PatientDao patientDao;

    @Autowired
    private MongoTemplate mongoTemplate;

    /***
     * 同步病人  有则改，无则加
     * */
    public void syncPatient(Patient patient)
    {
        Query query = new Query(Criteria.where("PatientCode").in(patient.getPatientCode()));
        Patient rPatient = mongoTemplate.findOne(query, Patient.class);
        if(rPatient == null)//数据库没有，则新增
        {
            mongoTemplate.save(patient);
        }
        else
        {
            mongoTemplate.updateMulti(query,new Update().set("AlipayUserId",patient.getAlipayUserId()).set("WechatOpenId",patient.getWechatOpenId()).set("Name", patient.getName()).set("CurrentWay",patient.getCurrentWay()),
                    Patient.class);
        }

    }

    /**
     * 根据病人id返回病人名称
     *
     * @param patientCode
     * @return
     */
    public String backPatientName(String patientCode) {
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("Name");
        Patient patient = mongoTemplate.findOne(query, Patient.class);
        if(patient == null)
            return null;
        return patient.getName();
    }

    /**
     * 输入PatientCode, illnessCode，返回病人就诊过该疾病的的医生Code，返回为List<String>类型。
     *
     * @param code illnessCode
     * @return 就诊过的医生code的列表
     */
    public List<String> findTheLast(String code, String illnessCode) {

        //HistoryIllnessInfo
        String[] fields=new String[]{"HistoryIllnessInfo"};
        Patient thePatient=patientDao.findone(code,fields);
        if(thePatient==null)
        {
            return new ArrayList<String>();
        }
        List<String> backs = new ArrayList<>();
        return backs;
    }

    /**
     * 返回医生代码
     *
     * @param patientCode
     * @return
     */
    public List<String> findAllDoctorCode(String patientCode) {
        List<String> backs = new ArrayList<>();
        return backs;
    }

    /**
     * 返回医生名称
     *
     * @param patientCode
     * @return
     */
    public List<String> findAllDoctorName(String patientCode) {
        List<String> backs = new ArrayList<>();
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        Patient patient = mongoTemplate.findOne(query, Patient.class);
        if(patient == null)
            return new ArrayList();
        int i = 0;

        return backs;
    }

    /**
     * 根据病人id放回病人信息
     * @param patientCode
     * @return
     */
    public Patient findPatientInfo(String patientCode) {
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("HistoryIllnessInfo").include("Name").include("PatientCode").include("AlipayUserId");
        Patient patientInfo = mongoTemplate.findOne(query, Patient.class);
        return patientInfo;
    }

    /**
     * 返回病人基本信息，如alipay用户id等
     * */
    public Patient findPatientBasic(String patientCode){
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("Name").include("PatientCode").include("AlipayUserId").include("WechatUserName").include("WechatOpenId");
        Patient patientInfo = mongoTemplate.findOne(query, Patient.class);
        return patientInfo;
    }

    /**
     * 根据疾病id返回疾病名称
     *
     * @param illnessCode
     * @return
     */
    public String backIllnessName(String illnessCode) {
        Query query = new Query(Criteria.where("HistoryIllnessInfo.Code").in(illnessCode));
        query.fields().include("HistoryIllnessInfo.Name");
        Patient patient = mongoTemplate.findOne(query, Patient.class);
        if(patient == null)
            return null;
        String illnessName = null;
        return illnessName;
    }

    /**
     * 修改当前医生
     * */
    public void updateCurrentDoctorCode(String patientCode,String doctorCode)
    {
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("HistoryIllnessInfo").include("Name").include("PatientCode").include("AlipayUserId");
        Patient patientInfo = mongoTemplate.findOne(query, Patient.class);
        query = new Query(Criteria.where("AlipayUserId").in(patientInfo.getAlipayUserId()));
        mongoTemplate.updateMulti(query,new Update().set("CurrentDoctorCode", ""), Patient.class);
        mongoTemplate.updateMulti(new Query(Criteria.where("PatientCode").is(patientCode)),
                new Update().set("CurrentDoctorCode", doctorCode), Patient.class);
    }

    /**
     *自定义查看病人的信息
     * @param patientCode 病人代码
     * @param fields   查找的字段
     */
    public Patient findone(String patientCode, String[] fields){
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        for (String field : fields){
            query.fields().include(field);
        }
        Patient patientInfo = mongoTemplate.findOne(query, Patient.class);
        return patientInfo;
    }
    public Patient findFirstByAlipayUserId(String alipayUserId){
        Query query = new Query(Criteria.where("AlipayUserId").in(alipayUserId));
        query.fields().include("Name").include("PatientCode").include("AlipayUserId").include("CurrentDoctorCode").include("WechatOpenId");
        Patient patientInfo = mongoTemplate.findOne(query, Patient.class);
        return patientInfo;
    }
    public Patient findFirstByWechatOpenId(String wechatOpenId){
        Query query = new Query(Criteria.where("WechatOpenId").in(wechatOpenId));
        query.fields().include("Name").include("PatientCode").include("AlipayUserId").include("CurrentDoctorCode").include("WechatOpenId");
        Patient patientInfo = mongoTemplate.findOne(query, Patient.class);
        return patientInfo;
    }
    public Patient findByAlipayUserId(String alipayUserId){
        Query query = new Query(Criteria.where("AlipayUserId").in(alipayUserId));
        query.fields().include("Name").include("PatientCode").include("AlipayUserId").include("CurrentDoctorCode").include("WechatOpenId");
        List<Patient> patientList = mongoTemplate.find(query, Patient.class);
        for(Patient patient:patientList){
            if(!StringUtil.isBlank(patient.getCurrentDoctorCode())){
                return patient;
            }
        }
        return new Patient();
    }
}
