package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.InspectChemicalResult;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 检验结果信息表
 * Created by lyx on 16/10/10.
 */
@Repository
public interface InspectChemicalResultDao extends MongoRepository<InspectChemicalResult, ObjectId> {
    /**
     * 根据检验单号去查询信息
     */
    List<InspectChemicalResult> inspectChemicalResultInfo(String code);

    /**
     * 根据条码号去查找检验单详情
     */
    InspectChemicalResult findInspectChemicalResult(String doctadviseno);
    /**
     *同步更新化验单结果，已经有结果的则不更新
     */
    public void saveInspectChemicalResult(List<InspectChemicalResult> inspectChemicalResults);
}
