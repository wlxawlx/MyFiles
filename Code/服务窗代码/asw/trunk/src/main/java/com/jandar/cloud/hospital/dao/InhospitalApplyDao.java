package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.InhospitalApply;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 云医院住院申请单
 * Created by zzw on 16/8/30.
 */
@Repository
public interface InhospitalApplyDao extends MongoRepository<InhospitalApply, ObjectId> {
}
