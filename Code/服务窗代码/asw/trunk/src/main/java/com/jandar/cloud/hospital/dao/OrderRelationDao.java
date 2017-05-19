package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.MedicalRecords;
import com.jandar.cloud.hospital.bean.OrderRelation;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 订单，答卷关联表
 */
@Repository
public interface OrderRelationDao extends MongoRepository<OrderRelation, ObjectId> {
    //查询当天的预约记录
    public OrderRelation findBydoctorIdAndPatientId(String doctorId, String patientId);
    //查询当天的预约记录
    public OrderRelation findByPatientId(String patientId);
}
