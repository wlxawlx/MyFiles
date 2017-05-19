package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.Schedule;
import org.springframework.stereotype.Repository;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Map;

/**
 * Created by admin on 2016/9/26.
 */
@Repository
public interface DoctorDao extends MongoRepository<Doctor, ObjectId>{
    public List<Doctor> findByCode(String code);

//* 根据doctorCode返回医生名称
    public String backDoctorName(String doctorCode);

    Doctor findDoctorInfo(String doctorCode);
    /**
     * 根据医生code返回部分信息
     * @param doctorCode
     * @return
     */
    Doctor doctorInfo(String doctorCode);
    Doctor findByUserName(String userName);
}
