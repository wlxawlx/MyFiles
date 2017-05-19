package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

/**
 * 留言表
 * Created by lyx on 2016/10/18.
 */
@Document(collection = "cloud_leave_message")
public class LeaveMessage {
    @Field("Type")
    private String type;
    @Field("Code")
    private String code;
    @Field("PatientCode")
    private String patientCode;
    @Field("PatientName")
    private String patientName;
    @Field("DoctorCode")
    private String doctorCode;
    @Field("DoctorName")
    private String doctorName;
    @Field("LeaveContent")
    private String leaveContent;
    @Field("LeaveDate")
    private Date leaveDate;
    @Field("IsDeal")
    private String isDeal;
    @Field("BackContent")
    private String backContent;
    @Field("BackDate")
    private Date backDate;
    @Field("SyncStatus")
    private String syncStatus;


    /**
     * 留言类型
     */
    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    /**
     * 留言所属流水号
     */
    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    /**
     * 留言内容
     */
    public String getLeaveContent() {
        return leaveContent;
    }

    public void setLeaveContent(String leaveContent) {
        this.leaveContent = leaveContent;
    }

    /**
     * 留言日期
     */
    public Date getLeaveDate() {
        return leaveDate;
    }

    public void setLeaveDate(Date leaveDate) {
        this.leaveDate = leaveDate;
    }

    /**
     * 留言是否被处理
     */
    public String getIsDeal() {
        return isDeal;
    }

    public void setIsDeal(String isDeal) {
        this.isDeal = isDeal;
    }

    /**
     * 回复内容
     */
    public String getBackContent() {
        return backContent;
    }

    public void setBackContent(String backContent) {
        this.backContent = backContent;
    }

    /**
     * 回复日期
     */
    public Date getBackDate() {
        return backDate;
    }

    public void setBackDate(Date backDate) {
        this.backDate = backDate;
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

    public String getSyncStatus() {
        return syncStatus;
    }

    public void setSyncStatus(String syncStatus) {
        this.syncStatus = syncStatus;
    }
}
