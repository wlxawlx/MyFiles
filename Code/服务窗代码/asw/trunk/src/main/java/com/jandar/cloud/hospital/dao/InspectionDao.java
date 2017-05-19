package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.InspectMain;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Created by admin on 2016/9/28.
 */
@Repository
public interface InspectionDao extends MongoRepository<InspectMain, ObjectId> {
    public List<InspectMain> backMessage(String patientCode);
    public void updatePayment(String code);
    public InspectMain findByCode(String code);
    public InspectMain findByInspectionCode(String code);
    public InspectMain paymentInfo(String code);

    /**
     * 根据检查单流水号来获取支付信息
     * @param code
     * @return
     */
    InspectMain findPayInfo(String code);

    /**
     * 缴费对缴费状态进行修改
     * @param code
     */
    void payByCode(String code);
    public void saveInspection(InspectMain inspection);
    public void updateSyncInspection(String code,String money);
}
