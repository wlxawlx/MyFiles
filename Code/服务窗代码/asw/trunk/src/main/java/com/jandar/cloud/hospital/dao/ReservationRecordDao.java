package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.ReservationRecord;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Map;

/**
 * 病人在云医院的预约,并包含了病人对近况问题回答的答案
 * Created by zzw on 16/8/30.
 */
@Repository
public interface ReservationRecordDao extends MongoRepository<ReservationRecord, ObjectId> {
    public List<Map<String,Object>> backMessage(String code);
    public ReservationRecord findByCode(String code);
    public List<ReservationRecord> findByPatientCode();
    public List<ReservationRecord> findAll();
    public List<ReservationRecord> findByPatientCodePage(String patientCode,Integer pageIndex,Integer pageSize);
    public Long count(String patientCode);

    /**
     * 根据病人id查询预约信息列表
     * @param code
     * @return
     */
    List<ReservationRecord> findReservationList(String code, String Status, String PayStatus);

    /**
     * 根据预约代码查询预约详情
     * @param code
     * @return
     */
    ReservationRecord findReservationInfo(String code);
    /**
     * 根据预约代码,自定义查询预约详情
     * @param code
     * @param fields  为空时查询所有字段
     * @return
     */
    public ReservationRecord findReservationInfo(String code, String[] fields);
    /**
     * 反向同步预约记录 是否同步完成
     * @param code
     * @param status
     */
    public void updateSyncReservationRecord(String code, String status);
    /**
     * 根据patientCode和doctorCode查找当天的一条预约记录
     * @param patientCode
     * @param doctorCode
     */
    public ReservationRecord get(String patientCode, String doctorCode);
}
