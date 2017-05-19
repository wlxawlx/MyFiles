package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.util.DateUtil;
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
 * 住院自定义实现
 * Created by lyx on 2016/9/30.
 */
public class InhospitalDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private InhospitalDao inhospitalDao;

    /**
     * 根据住院单号查询明细
     */
    public Inhospital inhospital(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("Code").include("PatientName").include("PatientIdNo").include("DeptName").include("DoctorName").include("PrescribeDate").include("TotalSum").include("Bunk");
        Inhospital inhospital = mongoTemplate.findOne(query, Inhospital.class);
        inhospital.setPrescribeDate_string(DateUtil.formatTime(inhospital.getPrescribeDate()));
        inhospital.setPrescribeDate(null);
        return inhospital;
    }

    /**
     * 根据病人id获取住院单列表
     */

    public List<Inhospital> inhospitalifo(String patientCode) {
        Query query = new Query(Criteria.where("PatientCode").in(patientCode));
        query.fields().include("Code").include("PatientName").include("PrescribeDate").include("InhospitalName").include("TotalSum").include("PayStatus").include("InHospitalStatus").include("DoctorName");
        List<Inhospital> inhospitals = mongoTemplate.find(query, Inhospital.class);
        for (Inhospital inhospital : inhospitals) {
            inhospital.setPrescribeDate_string(DateUtil.formatTime(inhospital.getPrescribeDate()));
            inhospital.setPrescribeDate(null);
        }
        return inhospitals;
    }

    /**
     * 根据住院单号获取订单信息
     */
    public Inhospital paymentInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("InhospitalName").include("TotalSum").include("Code");
        List<Inhospital> inhospitalInfo = mongoTemplate.find(query, Inhospital.class);
        return inhospitalInfo.get(0);

    }

    /**
     * 根据住院单Code，执行更新住院你的缴费记录内容和修改收费日期
     *
     * @param code
     */
    public void updatePayment(String code) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("PayStatus", "Success"), Inhospital.class);
        Date date = new Date();
        String dateString = DateUtil.formatTime(date);
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("ChargeDate", dateString), Inhospital.class);
    }


    /**
     * 根据住院单流水号来获取支付信息
     */
    public Inhospital findPayInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("PatientName").include("TotalSum").include("Code")
                .include("DeptName").include("PatientIdNo").include("PrestoreSum");
        Inhospital inhospitalInfo = mongoTemplate.findOne(query, Inhospital.class);
        return inhospitalInfo;

    }

    /**
     * 确认缴费之后对缴费状态进行修改
     *
     * @param code
     */
    public void payByCode(String code, Inhospital.ContectInfo contectInfo) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("PayStatus", "Success")
                        .set("ContectInfo", contectInfo), Inhospital.class);
        Date date = new Date();
        String dateString = DateUtil.formatTime(date);
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("ChargeDate", dateString), Inhospital.class);
    }

    /**
     * 反向同步缴费，返回是否同步完成
     * @param code
     * @param status
     */
    public void updateSyncInhospital(String code, String status)
    {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("SyncStatus", status), Inhospital.class);
    }

}
