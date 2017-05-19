package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.Prescription;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 处方列表
 * Created by lyx on 16/9/27.
 */
@Repository
public interface PrescriptionDao extends MongoRepository<Prescription, ObjectId> {

    List<Prescription> findByPatientCode(String PatientCode);



    /**
     * 根据处方单号查询物流信息
     */
    Prescription prescriptionsExpress(String code);

    /**
     * 根据病人代码查询处方
     */

    List<Prescription> prescriptionInfo(String patientCode);

    /**
     * 根据复诊处方单Code，执行更新复诊处方单你的缴费记录内容和修改收费日期
     *
     * @param code
     */
    void updatePayment(String code);

    /**
     * 处方缴费对处方表进行修改（自取）
     */
    void UpdatePayment(String code, String isExpress);

    /**
     * 处理物流信息
     */

    void UpdateExpressInfo(String code, Prescription.ReceiveInfo receviceInfo);

    /**
     * 根据处方单号获取订单信息
     */
    Prescription paymentInfo(String code);

    /**
     * 根据处方流水号获取该处方的明细
     * @param code
     * @return
     */

    Prescription prescriptionInfoByCode(String code);

    /**
     * 根据处方流水号获取处方缴费时的展示信息
     * @param code
     * @return
     */
    Prescription getPaymentInfoByCode(String code);



    Prescription findByCode(String prescriptionCode);

    /**
     * 反向同步处方单 是否同步完成
     * @param code
     * @param status
     */
    public void updateSyncPrescription(String code, String status);
}

