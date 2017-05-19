package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import org.bson.types.ObjectId;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 提交问卷答案
 * Created by lyx on 16/9/21.
 */
@Repository
public interface SubmitTopicDao extends MongoRepository<SubmitTopic, ObjectId> {
    SubmitTopic findOne(ObjectId id, String[] fields);
    ObjectId  findByPatientId(String patientId);
}

