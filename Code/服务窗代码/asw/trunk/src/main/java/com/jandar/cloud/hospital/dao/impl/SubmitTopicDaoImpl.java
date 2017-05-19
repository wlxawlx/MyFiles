package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.dao.SubmitTopicDao;
import org.bson.types.ObjectId;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Sort;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.query.BasicUpdate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;

/**
 * 提交问卷答案
 */
public class SubmitTopicDaoImpl {
    @Resource
    private SubmitTopicDao submitTopicDao;

    @Autowired
    private MongoTemplate mongoTemplate;

    public SubmitTopic findOne(ObjectId id, String[] fields){
        Query query = new Query(Criteria.where("_id").in(id));
        for (String field : fields){
            query.fields().include(field);
        }
        SubmitTopic submitTopic = mongoTemplate.findOne(query, SubmitTopic.class);
        return submitTopic;
    }
    public ObjectId findByPatientId(String patientId){
        Query query = new Query(Criteria.where("Patient.UserId").in(patientId));
       query.fields().include("_id");
       query.with(new Sort(new Sort.Order(Sort.Direction.DESC,"Date")));
       return  mongoTemplate.findOne(query, SubmitTopic.class).getId();
    }

}
