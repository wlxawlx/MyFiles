package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.MedicalRecords;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 就诊记录
 * Created by lyx on 16/9/21.
 */
@Repository
public interface MedicalRecordsDao extends MongoRepository<MedicalRecords, ObjectId> {

    MedicalRecords findByPatientId(String patientId);
}

