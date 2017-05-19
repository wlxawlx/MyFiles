package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

/**
 * 就诊记录表
 * Created by lyx on 2016/10/9.
 */
@Document(collection = "cloud_medical_records")
public class MedicalRecords {
    @Field("PatientId")
    private String patientId;
    @Field("PatientName")
    private String patientName;
    @Field("ReservationCode")
    private String reservationCode;
    @Field("ReservationState")
    private ReservationState reservationState;
    @Field("DoctorCode")
    private String doctorCode;
    @Field("DoctorImUser")
    private String doctorImUser;
    @Field("DoctorName")
    private String doctorName;
    @Field("Date")
    private String date;


    /**
     * 病人id
     */
    public String getPatientId() {
        return patientId;
    }

    public void setPatientId(String patientId) {
        this.patientId = patientId;
    }

    /**
     * 病人姓名
     */
    public String getPatientName() {
        return patientName;
    }

    public void setPatientName(String patientName) {
        this.patientName = patientName;
    }

    /**
     * 预约代码
     */
    public String getReservationCode() {
        return reservationCode;
    }

    public void setReservationCode(String reservationCode) {
        this.reservationCode = reservationCode;
    }


    /**
     * 医生代码
     */
    public String getDoctorCode() {
        return doctorCode;
    }

    public void setDoctorCode(String doctorCode) {
        this.doctorCode = doctorCode;
    }

    /**
     * 医生IM
     */
    public String getDoctorImUser() {
        return doctorImUser;
    }

    public void setDoctorImUser(String doctorImUser) {
        this.doctorImUser = doctorImUser;
    }

    /**
     * 医生名称
     */
    public String getDoctorName() {
        return doctorName;
    }

    public void setDoctorName(String doctorName) {
        this.doctorName = doctorName;
    }

    /**
     * 就诊时间
     */
    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    /**
     * 病人状态
     */
    public ReservationState getReservationState() {
        return reservationState;
    }

    public void setReservationState(ReservationState reservationState) {
        this.reservationState = reservationState;
    }

    public enum ReservationState {

        // 没有预约
        NoReservation,

        // 已预约但未开始就诊
        NoBegin,

        // 就诊中
        InADoctor,

        // 就诊结束
        End,

        // 预约过期
        Overdue,
    }
}
