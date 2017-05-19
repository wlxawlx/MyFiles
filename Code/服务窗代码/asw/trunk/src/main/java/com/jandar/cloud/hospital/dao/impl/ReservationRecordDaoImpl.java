package com.jandar.cloud.hospital.dao.impl;


import com.jandar.cloud.hospital.bean.ReservationRecord;
import com.jandar.cloud.hospital.dao.ReservationRecordDao;
import com.jandar.util.StringUtil;
import org.apache.commons.collections.map.LinkedMap;
import org.apache.commons.httpclient.util.DateUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Sort;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 预约信息查询自定义实现
 * Created by admin on 2016/9/27.
 */
public class ReservationRecordDaoImpl {

    @Resource
    private ReservationRecordDao reservationRecordDao;
    @Autowired
    private MongoTemplate mongoTemplate;

    /**
     * 根据号源，返回CH010601格式的内容,由于Reservation类中含int型，默认值为0，故返回的类型需要自行设置。
     *
     * @param code
     * @return
     */
    public List<Map<String, Object>> backMessage(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        List<ReservationRecord> reservationRecords = mongoTemplate.find(query, ReservationRecord.class);
        List<Map<String, Object>> infomaps = new ArrayList<>();
        for (ReservationRecord reservationRecord : reservationRecords) {
            ReservationRecord b = new ReservationRecord();
            Map<String, Object> infomap = new LinkedMap();
            infomap.put("PatientName", reservationRecord.getPatientName());
            infomap.put("VisitType", reservationRecord.getVisitType());
            infomap.put("IllnessName", reservationRecord.getIllnessName());
            infomap.put("DeptName", reservationRecord.getDeptName());
            infomap.put("DoctorName", reservationRecord.getDoctorName());
            infomap.put("SourceTime", reservationRecord.getSourceTime());
            infomap.put("Status", reservationRecord.getStatus());
            infomaps.add(infomap);
        }
        return infomaps;
    }

    /**
     * 返回数据，根据分页的大小，和第几页开始显示
     *
     * @param pageIndex
     * @param pageSize
     * @return
     */
    public List<ReservationRecord> findByPatientCodePage(String patientCode,Integer pageIndex, Integer pageSize) {
        Query query = new Query(Criteria.where("PatientCode").is(patientCode));
        query.skip((pageIndex-1)*pageSize);
        query.limit(pageSize);
        query.fields().include("Code").include("SourceTime").include("PayStatus").include("PayStatus").include("Number").include("DeptName").include("VisitType").include("Status");
        List<ReservationRecord> reservationRecords = mongoTemplate.find(query, ReservationRecord.class);
        return reservationRecords;
    }

    /**
     * 该病人的记录总数
     * @param patientCode
     * @return
     */
    public Long count(String patientCode){
        Query query = new Query(Criteria.where("PatientCode").is(patientCode));
        Long count = mongoTemplate.count(query, ReservationRecord.class);
        return count;
    }
    /**
     * 根据病人code查询预约信息列表（按时间排列）
     *
     * @param code
     * @return
     */
    public List<ReservationRecord> findReservationList(String code, String status, String payStatus) {
        Criteria criteria = Criteria.where("PatientCode").is(code);
        if(!StringUtil.isBlank(status)){         //预约状态
            criteria.where("Status").is(status);
        }
        if(!StringUtil.isBlank(payStatus)){      //缴费状态
            criteria.where("PayStatus").is(payStatus);
        }
        Query query = new Query(criteria);
        query.fields().include("Code").include("VisitType").include("IllnessName")
                .include("CreateTime").include("SourceTime").include("Status").include("PayStatus");
        query.with(new Sort(new Sort.Order(Sort.Direction.ASC, "CreateTime")));
        List<ReservationRecord> reservationRecord = mongoTemplate.find(query, ReservationRecord.class);
        return reservationRecord;
    }

    /**
     * 根据预约代码查询预约详情
     * @param code
     * @return
     */
    public ReservationRecord findReservationInfo(String code) {
        Query query = new Query(Criteria.where("Code").in(code));
        query.fields().include("Code").include("VisitType").include("IllnessName")
                .include("DeptName").include("SourceTime").include("PatientName").include("DoctorName").include("Number");
        ReservationRecord reservationInfo = mongoTemplate.findOne(query, ReservationRecord.class);
        return reservationInfo;
    }
    /**
     * 根据预约代码,自定义查询预约详情
     * @param code
     * @param fields  为空时查询所有字段
     * @return
     */
    public ReservationRecord findReservationInfo(String code, String[] fields) {
        Query query = new Query(Criteria.where("Code").in(code));
        if(fields!=null && fields.length>0){
            for (String field : fields) {
                query.fields().include(field);
            }
        }
        ReservationRecord reservationInfo = mongoTemplate.findOne(query, ReservationRecord.class);
        return reservationInfo;
    }

    /**
     * 反向同步预约记录 是否同步完成
     * @param code
     * @param status
     */
    public void updateSyncReservationRecord(String code, String status)
    {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("SyncStatus", status), ReservationRecord.class);
    }

    /**
     * 根据patientCode和doctorCode查找当天的一条预约记录
     * @param patientCode
     * @param doctorCode
     */
    public ReservationRecord get(String patientCode, String doctorCode){
        Query query =  new Query(Criteria.where("PatientCode").in(patientCode));
        query.addCriteria(Criteria.where("DoctorCode").in(doctorCode));
        String todayStr = DateUtil.formatDate(new Date(),"yyyy/MM/dd");
        query.addCriteria(Criteria.where("SourceTime").regex("^"+todayStr)); //当天日期
        ReservationRecord reservationInfo = mongoTemplate.findOne(query, ReservationRecord.class);
        return reservationInfo;
    }

}
