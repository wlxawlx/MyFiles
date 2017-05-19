package com.jandar.cloud.hospital.dao.impl;


import com.jandar.cloud.hospital.bean.InspectChemicalResult;
import com.jandar.cloud.hospital.bean.InspectResult;
import com.jandar.cloud.hospital.dao.InspectResultDao;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;

import javax.annotation.Resource;
import java.util.List;

/**
 * 检查单自定义实现
 * Created by lyx on 2016/10/11.
 */
public class InspectResultDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private InspectResultDao inspectResultDao;


    /**
     * 根据检查单号去查询信息
     */
    public List<InspectResult> inspectResultInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("PatientName").include("InspectName").include("ReceiveTime")
                .include("Requestmode").include("Doctadviseno").include("Requester");
        List<InspectResult> inspectResultResultInfo = mongoTemplate.find(query, InspectResult.class);
        return inspectResultResultInfo;

    }

    /**
     * 根据条码号去查找检查单详情
     */
    public InspectResult findInspectResult(String doctadviseno) {
        Query query = new Query(Criteria.where("Doctadviseno").in(doctadviseno));
        query.fields().include("PatientName").include("InspectName").include("Receiver").include("Requestmode")
                .include("Result").include("Checker").include("Photo").include("ReceiveTime").include("ResultDetail");
        InspectResult inspectResultInfo = mongoTemplate.findOne(query, InspectResult.class);
        return inspectResultInfo;

    }

    /**
     *同步检查结果，已经存在的检查结果则不同步
     */
    public void saveInspectResult(List<InspectResult> inspectResults)
    {
        for(InspectResult inspectRseult : inspectResults)
        {
            Query query = new Query(Criteria.where("Doctadviseno").in(inspectRseult.getDoctadviseno()));
            InspectResult temp = mongoTemplate.findOne(query, InspectResult.class);
            if (temp != null)
                continue;
            mongoTemplate.save(inspectRseult);
        }
    }

}
