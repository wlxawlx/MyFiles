package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.dao.DoctorDao;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by zzw on 16/9/21.
 */
public class DoctorDaoImpl {

    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private DoctorDao doctorDao;

    /**
     * 根据doctorCode返回医生名称
     *
     * @param doctorCode
     * @return
     */
    public String backDoctorName(String doctorCode) {
        Query query = new Query(Criteria.where("Code").in(doctorCode));
        query.fields().include("name");
        Doctor doctor = mongoTemplate.findOne(query, Doctor.class);
        if (doctor == null)
            return null;
        return doctor.getName();
    }
    /**
     * 根据医生代码，返回排班信息
     * ***************************************************************************************************
     *
     * @param doctorCode
     * @return
     */
    public List<Doctor> scheduleDoctor(String doctorCode) {
        Query query = new Query(Criteria.where("Code").in(doctorCode));
        query.fields().include("Schedule");
        List<Doctor> doctors = mongoTemplate.find(query, Doctor.class);
        return doctors;
    }
    /**
     * 返回当日有排班的医生名字
     *
     * @param date
     * @return
     */
    public List<String> findDoctorName(String date) {
        Query query = new Query(Criteria.where("Schedule_Date"));
        query.fields().include("name");
        List<Doctor> doctors = mongoTemplate.find(query, Doctor.class);
        List<String> doctorNames = new ArrayList<>();
        int i = 0;
        for (Doctor doctor : doctors) {
            doctorNames.add(i, doctor.getName());
            i++;
        }
        return doctorNames;
    }
    public Doctor findDoctorInfo(String doctorCode) {
        Query query = new Query(Criteria.where("Code").in(doctorCode));
        query.fields().include("Code").include("name").include("Schedule").include("DeptName");
        Doctor doctors = mongoTemplate.findOne(query, Doctor.class);
        return doctors;
    }


    /**
     * 根据医生code返回部分信息
     *
     * @param doctorCode
     * @return
     */
    public Doctor doctorInfo(String doctorCode) {
        Query query = new Query(Criteria.where("Code").in(doctorCode));
        Doctor doctor = mongoTemplate.findOne(query, Doctor.class);
        return doctor;
    }
}
