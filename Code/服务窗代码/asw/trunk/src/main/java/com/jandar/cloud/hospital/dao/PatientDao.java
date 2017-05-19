package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.Patient;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 云医院的病人信息,包含病人基本信息,病人邮寄地址,病人第一联系人信息
 * Created by zzw on 16/8/30.
 */
@Repository
public interface PatientDao extends MongoRepository<Patient, ObjectId> {


    public List<Patient> findByPatientCode(String id);


    /***
     * 同步病人  有则改，无则加
     * */
    public void syncPatient(Patient patient);

    /**
     * 输入PatientCode, illnessCode，返回病人就诊过的医生Code，返回为List<String>类型。
     *
     * @param code illnessCode
     * @return backs
     */
    public List<String> findTheLast(String code, String illnessCode);

    /**
     * 根据病人id返回病人名称
     *
     * @param patientCode
     * @return
     */
    public String backPatientName(String patientCode);


    public Patient findFirstByAlipayUserId(String alipayUserId);


    public Patient findFirstByWechatOpenId(String wechatOpenId);

    public List<String> findAllDoctorCode(String patientCode);
    public List<String> findAllDoctorName(String patientCode);

    /**
     * 根据病人id返回病人信息
     * @param patientCode
     * @return
     */
    public Patient findPatientInfo(String patientCode);

    /**
     * 返回病人基本信息，如alipay用户id等
     * */
    public Patient findPatientBasic(String patientCode);
    public String backIllnessName(String illnessCode);

    /**
     * 修改当前医生
     * */
    public void updateCurrentDoctorCode(String patientCode,String doctorCode);

    /**
     *自定义查看病人的信息
     * @param patientCode 病人代码
     * @param fields   查找的字段
     */
    public Patient findone(String patientCode, String[] fields);
    /*
    *根据alipayUserId获取当前医生不为空的病人
     */
    public Patient findByAlipayUserId(String alipayUserId);
}
