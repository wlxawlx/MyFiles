package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.web.PagedResourcesAssembler;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 慢性病近况问题
 * 近况问题没有和 ChronicIllness 表放一起,使用一个独立的表,是因为一套题目可能对应多种疾病
 * Created by lyx on 16/9/18.
 */
@Repository
public interface ChronicIllnessTopicDao extends MongoRepository<ChronicIllnessTopic, ObjectId> {
    List<ChronicIllnessTopic> findByIllness_Code(String code);

    List<ChronicIllnessTopic> findByDept_Code(String deptCode);

    List<ChronicIllnessTopic> findByHospitalCode(String hospitalCode);



    /**
     * 根据问卷代码查询问卷名称
     * @param topicCode
     * @return
     */
    ChronicIllnessTopic findTopicInfo(String topicCode);

    ChronicIllnessTopic findByCode(String code);

    /**
     * 根据问卷代码,自定义查询问卷信息
     */
    ChronicIllnessTopic findTopicInfo(String code, String[] fields);
}

