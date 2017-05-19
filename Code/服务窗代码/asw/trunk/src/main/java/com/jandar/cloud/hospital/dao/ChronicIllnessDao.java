package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.ChronicIllness;
import com.jandar.cloud.hospital.bean.Patient;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 云医院慢性病列表
 * Created by zzw on 16/8/30.
 */
@Repository
public interface ChronicIllnessDao extends MongoRepository<ChronicIllness, ObjectId> {

    public List<ChronicIllness> findByIllness_Code(String illCode);

}
