package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.PaymentList;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 缴费记录表
 * Created by lyx on 2016/9/27.
 */
@Repository
public interface PaymentListDao extends MongoRepository<PaymentList,ObjectId> {

}
