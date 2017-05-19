package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.CloudOrder;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * Created by Administrator on 2016/11/10.
 */
@Repository
public interface CloudOrderDao extends MongoRepository<CloudOrder, ObjectId> {
    void add(CloudOrder cloudOrder);

}
