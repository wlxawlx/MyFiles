package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.OrderRelation;
import com.jandar.cloud.hospital.dao.OrderRelationDao;
import com.jandar.util.DateUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;

import javax.annotation.Resource;
import java.util.Date;

/**
 * Created by Administrator on 2017/1/3.
 */
public class OrderRelationDaoImpl {
    @Resource
    private OrderRelationDao orderRelationDao;

    @Autowired
    private MongoTemplate mongoTemplate;

    public OrderRelation findBydoctorIdAndPatientId(String doctorId, String patientId){
        Criteria criteria = Criteria.where("doctorid").is(doctorId);
        Query query = new Query(criteria);
        query.addCriteria(Criteria.where("patientid").is(patientId));
        query.addCriteria(Criteria.where("orderdate").is(DateUtil.formatDateWithString(new Date(), "yyyy-MM-dd")));
        OrderRelation orderRelation = mongoTemplate.findOne(query, OrderRelation.class);
        return orderRelation;
    }
    public OrderRelation findByPatientId(String patientId){
        Criteria criteria = Criteria.where("patientid").is(patientId);
        Query query = new Query(criteria);
        query.addCriteria(Criteria.where("orderdate").is(DateUtil.formatDateWithString(new Date(), "yyyy-MM-dd")));
        OrderRelation orderRelation = mongoTemplate.findOne(query, OrderRelation.class);
        return orderRelation;
    }
}
