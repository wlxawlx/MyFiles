package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

/**
 * 病人在云医院的预约,(并包含了病人对近况问题回答的答案?)
 * Created by zzw on 16/8/30.
 */
@Document(collection = "cloud_reservation_record")
public class ReservationRecord {
    @Id
    private ObjectId id;

    @Field("Code")
    private String code;

    @Field("PatientCode")
    private String patientCode;

    @Field("PatientName")
    private String patientName;

    @Field("VisitType")
    private String visitType;

    @Field("IllnessCode")
    private String illnessCode;

    @Field("IllnessName")
    private String illnessName;

    @Field("DoctorCode")
    private String doctorCode;

    @Field("DoctorName")
    private String doctorName;

    @Field("DoctorImUser")
    private String doctorImUser;

    @Field("DeptCode")
    private  String deptCode;

    @Field("DeptName")
    private String deptName;

    @Field("CreateTime")
    private Date createTime;

    @Field("ScheduleCode")
    private String scheduleCode;

    @Field("SourceCode")
    private String sourceCode;

    @Field("SourceTime")
    private String sourceTime;

    @Field("Number")
    private int number;

    @Field("Status")
    private String status;

    @Field("PayStatus")
    private String payStatus;

    @Field("Fee")
    private String fee;

    @Field("SyncStatus")
    private String syncStatus;

    @Field("SubmitTopicId")
    private String SubmitTopicId;

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public String getPatientCode() {
        return patientCode;
    }

    public void setPatientCode(String patientCode) {
        this.patientCode = patientCode;
    }

    public String getPatientName() {
        return patientName;
    }

    public void setPatientName(String patientName) {
        this.patientName = patientName;
    }

    public String getVisitType() {
        return visitType;
    }

    public void setVisitType(String visitType) {
        this.visitType = visitType;
    }

    public String getIllnessCode() {
        return illnessCode;
    }

    public void setIllnessCode(String illnessCode) {
        this.illnessCode = illnessCode;
    }

    public String getIllnessName() {
        return illnessName;
    }

    public void setIllnessName(String illnessName) {
        this.illnessName = illnessName;
    }

    public String getDoctorCode() {
        return doctorCode;
    }

    public void setDoctorCode(String doctorCode) {
        this.doctorCode = doctorCode;
    }

    public String getDoctorName() {
        return doctorName;
    }

    public void setDoctorName(String doctorName) {
        this.doctorName = doctorName;
    }

    public String getDoctorImUser() {
        return doctorImUser;
    }

    public void setDoctorImUser(String doctorImUser) {
        this.doctorImUser = doctorImUser;
    }

    public String getDeptCode() {
        return deptCode;
    }

    public void setDeptCode(String deptCode) {
        this.deptCode = deptCode;
    }

    public String getDeptName() {
        return deptName;
    }

    public void setDeptName(String deptName) {
        this.deptName = deptName;
    }

    public Date getCreateTime() {
        return createTime;
    }

    public void setCreateTime(Date createTime) {
        this.createTime = createTime;
    }

    public String getScheduleCode() {
        return scheduleCode;
    }

    public void setScheduleCode(String scheduleCode) {
        this.scheduleCode = scheduleCode;
    }

    public String getSourceCode() {
        return sourceCode;
    }

    public void setSourceCode(String sourceCode) {
        this.sourceCode = sourceCode;
    }

    public String getSourceTime() {
        return sourceTime;
    }

    public void setSourceTime(String sourceTime) {
        this.sourceTime = sourceTime;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getPayStatus() {
        return payStatus;
    }

    public void setPayStatus(String payStatus) {
        this.payStatus = payStatus;
    }

    public String getFee() {
        return fee;
    }

    public void setFee(String fee) {
        this.fee = fee;
    }

    public String getSyncStatus() {
        return syncStatus;
    }

    public void setSyncStatus(String syncStatus) {
        this.syncStatus = syncStatus;
    }

    public String getSubmitTopicId() {
        return SubmitTopicId;
    }

    public void setSubmitTopicId(String submitTopicId) {
        SubmitTopicId = submitTopicId;
    }
}
