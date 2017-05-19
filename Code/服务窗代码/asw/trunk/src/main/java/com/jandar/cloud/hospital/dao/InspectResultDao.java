package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.InspectResult;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 检查结果信息表
 * Created by lyx on 16/10/10.
 */
@Repository
public interface InspectResultDao extends MongoRepository<InspectResult, ObjectId> {
    /**
     * 根据检查单号去查询信息
     */
    List<InspectResult> inspectResultInfo(String code);

    /**
     * 根据条码号去查找检查单详情
     */
    InspectResult findInspectResult(String doctadviseno);

    /**
     *同步检查结果，已经存在的检查结果则不同步
     */
    public void saveInspectResult(List<InspectResult> inspectResults);

}
