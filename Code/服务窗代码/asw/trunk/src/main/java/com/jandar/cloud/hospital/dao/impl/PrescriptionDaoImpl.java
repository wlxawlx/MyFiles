package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import javax.annotation.Resource;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * 处方自定义实现
 * Created by lyx on 2016/9/30.
 */
public class PrescriptionDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private PrescriptionDao prescriptionDao;

    /**
     * 根据处方单号查询物流信息
     */
    public Prescription prescriptionsExpress(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("ExpressNo").include("ExpressCompany").include("SignStatus");
        Prescription prescriptions = mongoTemplate.findOne(query, Prescription.class);
        return prescriptions;
    }

    /**
     * 根据病人代码查询处方
     */

    public List<Prescription> prescriptionInfo(String patientCode) {
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("SignStatus").include("PayStatus").include("PrescriptionName").include("PrescriptionType").include("PrescriptionSum")
                .include("PreScribeDate").include("DoctorName").include("PatientName").include("PrescribeDate").include("Code");
        List<Prescription> prescriptions = mongoTemplate.find(query, Prescription.class);
        return prescriptions;
    }

    /**
     * 处方缴费对处方表进行修改（自取）
     */
    public void UpdatePayment(String code, String isExpress) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("IsExpress", isExpress), Prescription.class);

    }

    /**
     * 处理物流信息
     */
    public void UpdateExpressInfo(String code, Prescription.ReceiveInfo receviceInfo) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("ReceiveInfo", receviceInfo), Prescription.class);
    }

    /**
     * 根据复诊处方单Code，执行更新复诊处方单你的缴费记录内容和修改收费日期
     *
     * @param code
     */
    public void updatePayment(String code) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("PayStatus", "Success"), Prescription.class);
        Date date = new Date();
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String dateString = formatter.format(date);
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("ChargeDate", dateString), Prescription.class);
    }

    /**
     * 根据处方单号获取订单信息
     */
    public Prescription paymentInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("PrescriptionName").include("TotalSum").include("Code");
        Prescription inhospitalInfo = mongoTemplate.findOne(query, Prescription.class);
        return inhospitalInfo;

    }

    public Prescription prescriptionInfoByCode(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("Code").include("DoctorName").include("TotalSum").include("PrescribeDate")
        .include("MedicinalInfo").include("PrescriptionName");
        Prescription prescriptions = mongoTemplate.findOne(query, Prescription.class);
        return prescriptions;
    }

    /**
     * 根据处方流水号获取处方缴费时的展示信息
     * @param code
     * @return
     */
    public Prescription getPaymentInfoByCode(String code){
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("Code").include("DoctorName").include("PrescriptionName").include("PrescriptionSum")
                .include("TotalSum").include("SpecialType").include("IsExpress").include("ReceiveInfo");
        Prescription prescriptions = mongoTemplate.findOne(query, Prescription.class);
        return prescriptions;
    }

    /**
     * 反向同步处方单 是否同步完成
     * @param code
     * @param status
     */
    public void updateSyncPrescription(String code, String status)
    {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("SyncStatus", status), Prescription.class);
    }

}
