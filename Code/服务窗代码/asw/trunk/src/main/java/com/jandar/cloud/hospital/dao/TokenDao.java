package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.SubmitTopic;
import com.jandar.cloud.hospital.bean.Token;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * Created by flyhorse on 2016/11/8.
 */
@Repository
public interface TokenDao extends MongoRepository<Token, ObjectId> {


    /**
     * 根据doctorId获取token
     * */
    public String getTokenByDoctorId(String doctorId,String userName,String name,String userType);
}
