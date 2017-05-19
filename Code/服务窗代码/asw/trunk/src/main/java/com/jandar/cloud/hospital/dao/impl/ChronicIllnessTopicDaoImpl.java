package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;

import javax.annotation.Resource;
import java.util.List;

/**
 * 问卷自定义实现
 * Created by lyx on 2016/10/14.
 */
public class ChronicIllnessTopicDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;


    /**
     * 根据问卷代码去查询问卷信息
     */
    public ChronicIllnessTopic findTopicInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("Code").include("Tile").include("Items");
        ChronicIllnessTopic topicInfo = mongoTemplate.findOne(query, ChronicIllnessTopic.class);
        return topicInfo;

    }
    /**
     * 根据问卷代码,自定义查询问卷信息
     */
    public ChronicIllnessTopic findTopicInfo(String code, String[] fields) {
        Query query = new Query(Criteria.where("Code").in(code));
        for (String field : fields) {
            query.fields().include(field);
        }
        ChronicIllnessTopic topicInfo = mongoTemplate.findOne(query, ChronicIllnessTopic.class);
        return topicInfo;

    }
}
