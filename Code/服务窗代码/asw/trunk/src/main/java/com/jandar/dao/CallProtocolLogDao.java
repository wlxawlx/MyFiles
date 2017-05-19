package com.jandar.dao;

import com.jandar.bean.ProtocolCallLog;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 协议调用历史
 * Created by zzw on 16/8/30.
 */
@Repository
public interface CallProtocolLogDao extends MongoRepository<ProtocolCallLog, ObjectId> {
}
