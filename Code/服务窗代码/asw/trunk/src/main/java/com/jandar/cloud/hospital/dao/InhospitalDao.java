package com.jandar.cloud.hospital.dao;


import com.jandar.cloud.hospital.bean.Inhospital;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * 住院信息
 * Created by lyx on 16/9/28.
 */
@Repository
public interface InhospitalDao extends MongoRepository<Inhospital, ObjectId> {
    /**
     * 查询病人信息
     *
     * @param PatientCode
     * @return
     */

    List<Inhospital> findByPatientCode(String PatientCode);

    /**
     * 根据住院单号查询信息
     *
     * @param code
     * @return
     */
    Inhospital findByCode(String code);

    /**
     * 根据住院单号查询明细
     */
    Inhospital inhospital(String code);

    /**
     * 根据病人id获取住院单列表
     */
    List<Inhospital> inhospitalifo(String patientCode);

    /**
     * 根据住院单号获取订单信息
     */
    Inhospital paymentInfo(String code);

    /**
     * 根据住院单Code，执行更新住院你的缴费记录内容和修改收费日期
     *
     * @param code
     */
    public void updatePayment(String code);

    /**
     * 根据住院单流水号获取缴费信息展示
     *
     * @param code
     * @return
     */
    Inhospital findPayInfo(String code);

    /**
     * 缴费对缴费状态进行修改
     *
     * @param code
     */
    void payByCode(String code,Inhospital.ContectInfo contectInfo);
    /**
     * 反向同步缴费，返回是否同步完成
     * @param code
     * @param status
     */
    public void updateSyncInhospital(String code, String status);
}

