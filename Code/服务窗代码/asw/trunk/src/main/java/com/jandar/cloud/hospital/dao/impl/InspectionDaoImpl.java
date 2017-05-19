package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.util.DateUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * 检查单自定义实现
 * Created by admin on 2016/9/28.
 */
public class InspectionDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;

    /**
     * 根据患者ID，返回CH010901所需的字段
     *
     * @param patientCode
     * @return
     */
    public List<InspectMain> backMessage(String patientCode) {
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("Code").include("PatientName").include("DoctorName").include("PrescribeDate").include("TotalSum").include("InspectName").include("PayStatus").include("InspectStatus");
        List<InspectMain> inspection = mongoTemplate.find(query, InspectMain.class);
        return inspection;
    }

    /**
     * 根据检查单Code，执行更新检查单你的缴费记录内容和修改收费日期
     *
     * @param code
     */
    public void updatePayment(String code) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("PayStatus", "Success"), InspectMain.class);
        Date date = new Date();
        String dateString = DateUtil.formatTime(date);
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("ChargeDate", dateString), InspectMain.class);
    }

    /**
     * 根据检查单Code，返回检查单详情
     *
     * @param code
     * @return
     */
    public InspectMain findByInspectionCode(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("Code").include("PayStatus").include("InspectStatus").include("PatientName").include("DoctorName").include("InspectItem");
        List<InspectMain> inspections = mongoTemplate.find(query, InspectMain.class);
        for (InspectMain inspection : inspections) {
            for (InspectMain.InspectItem inspectItem : inspection.getInspectItem()) {
                String strDate = DateUtil.formatTime(inspectItem.getDate());
                inspectItem.setDate(null);
                inspectItem.setCheckDate(strDate);
            }
        }
        if (inspections.isEmpty())
            return null;
        return inspections.get(0);
    }

    /**
     * 根据检查单号获取订单信息
     */
    public InspectMain paymentInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("InspectName").include("TotalSum").include("Code");
        List<InspectMain> inspectionInfo = mongoTemplate.find(query, InspectMain.class);
        return inspectionInfo.get(0);

    }


    /**
     * 根据检查单流水号来获取支付信息
     */
    public InspectMain findPayInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("PatientName").include("TotalSum").include("Code")
                .include("DoctorName").include("InspectItem");
        InspectMain inspectionInfo = mongoTemplate.findOne(query, InspectMain.class);
        return inspectionInfo;

    }

    /**
     * 确认缴费之后对缴费状态进行修改
     * @param code
     */
    public  void payByCode(String code) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("PayStatus", "Success"), InspectMain.class);

    }
    public void saveInspection(InspectMain inspection)
    {
        mongoTemplate.save(inspection);
    }

    /**
     * 反向同步检查单是否同步
     * @param code
     * @param status
     */
    public void updateSyncInspection(String code, String status)
    {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("SyncStatus", status), InspectMain.class);
    }
}
