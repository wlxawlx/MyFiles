package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.Express;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 物流表
 * Created by lyx on 16/10/20.
 */
@Repository
public interface ExpressDao extends MongoRepository<Express, ObjectId> {
    /**
     * 根据处方单流水号来查询物流信息
     * @param code
     * @return
     */
    Express findByCode(String code);
}

