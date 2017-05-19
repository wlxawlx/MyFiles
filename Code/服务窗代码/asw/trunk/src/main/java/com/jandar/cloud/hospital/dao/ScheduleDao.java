package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.Schedule;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Created by admin on 2016/10/17.
 */
@Repository
public interface ScheduleDao extends MongoRepository<Schedule, ObjectId> {
    List<Schedule> findByDoctorCode(String doctorCode);
    public String findSourceStatus(String doctorCode,String dateDay,Integer number);
    public Schedule findOne(String doctorCode,String dateDay);
    public void updateSourceStatus(String doctorCode, String dateDay, Integer number);
    public Integer backRemainNumber(String doctorCode);
    List<Schedule> findByScheduleInfo(String doctorCode);
    public String backDateTime(String doctorCode, String dateDay,Integer number);
    public String backFee(String doctorCode, String dateDay);
    public void updateSourceStatusUsed(String doctorCode, String dateDay, Integer number,String patientCode, String patientName);
    public void scheduleUnlock();

    /**
     * 根据排班日期和医生代码返回信息
     * @param doctorCode
     * @param date
     */
    long countByDoctorAndDate(String doctorCode, String date);

    /**
     * 根据排班代码修改排班是否有效
     * @param code
     */
    void updateValid(String code);
}
