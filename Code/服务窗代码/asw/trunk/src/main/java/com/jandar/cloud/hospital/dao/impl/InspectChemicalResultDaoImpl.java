package com.jandar.cloud.hospital.dao.impl;


import com.jandar.cloud.hospital.bean.InspectChemicalResult;
import com.jandar.cloud.hospital.dao.InspectChemicalResultDao;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;

import javax.annotation.Resource;
import java.util.List;

/**
 * 检验单自定义实现
 * Created by lyx on 2016/10/11.
 */
public class InspectChemicalResultDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private InspectChemicalResultDao inspectChemicalResultDao;


    /**
     * 根据检验单号去查询信息
     */
    public List<InspectChemicalResult> inspectChemicalResultInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("PatientName").include("InspectName")
                .include("ReceiveTime").include("Requestmode").include("Doctadviseno").include("Requester");
        List<InspectChemicalResult> inspectChemicalResultInfo = mongoTemplate.find(query, InspectChemicalResult.class);
        return inspectChemicalResultInfo;

    }

    /**
     * 根据条码号去查找检验单详情
     */
    public InspectChemicalResult findInspectChemicalResult(String doctadviseno) {
        Query query = new Query(Criteria.where("Doctadviseno").in(doctadviseno));
        query.fields().include("PatientName").include("InspectName").include("Receiver").include("Requestmode").include("Stayhospitalmode")
                .include("Requester").include("Checker").include("ExecuteTime").include("ReceiveTime").include("ResultDetail");
        InspectChemicalResult inspectChemicalResultInfo = mongoTemplate.findOne(query, InspectChemicalResult.class);
        return inspectChemicalResultInfo;

    }

    /**
     *同步更新化验单结果，已经有结果的则不更新
     */
    public void saveInspectChemicalResult(List<InspectChemicalResult> inspectResults)
    {
        for(InspectChemicalResult inspectchemicalRseult : inspectResults)
        {
            Query query = new Query(Criteria.where("Doctadviseno").in(inspectchemicalRseult.getDoctadviseno()));
            InspectChemicalResult temp = mongoTemplate.findOne(query, InspectChemicalResult.class);
            if (temp != null)
                continue;
            mongoTemplate.save(inspectchemicalRseult);
        }
    }

}
