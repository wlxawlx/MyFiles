package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;
import java.util.List;


/**
 * Created by admin on 2016/10/17.
 */
@Document(collection = "cloud_schedule")
public class Schedule {

    @Field("Code")
    private String code;

    @Field("DoctorCode")
    private String doctorCode;

    @Field("DoctorName")
    private String doctorName;

    @Field("Date")
    private String date;

    @Field("DeptCode")
    private  String deptCode;

    @Field("DeptName")
    private  String deptName;

    @Field("Size")
    private Integer size;

    @Field("Remain")
    private Integer remain;

    @Field("Fee")
    private String fee;

    @Field("Valid")
    private  String valid;

    @Field("Sources")
    private List<Sources> sources;

    public List<Sources> getSources() { return sources;    }

    public void setSources(List<Sources> sources) {
        this.sources = sources;
    }

    public String getValid() {
        return valid;
    }

    public void setValid(String valid) {
        this.valid = valid;
    }

    public Integer getRemain() {
        return remain;
    }

    public void setRemain(Integer remain) {
        this.remain = remain;
    }

    public Integer getSize() {
        return size;
    }

    public void setSize(Integer size) {
        this.size = size;
    }

    public String getFee() {
        return fee;
    }

    public void setFee(String fee) {
        this.fee = fee;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
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

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
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

    @Document
    public static class Sources {
        @Field("Time")
        private String time;

        @Field("NoonFlag")
        private String noonFlag;

        @Field("PatientCode")
        private String patientCode;

        @Field("PatientName")
        private String patientName;

        @Field("Status")
        private String status;

        @Field("Number")
        private  Integer number;

        @Field("ReservationRecordCode")
        private  String reservationRecordCode;

        public String getTime() {
            return time;
        }

        public void setTime(String date) {
            this.time = date;
        }

        public String getNoonFlag() {
            return noonFlag;
        }

        public void setNoonFlag(String noonFlag) {
            this.noonFlag = noonFlag;
        }

        public Integer getNumber() {
            return number;
        }

        public void setNumber(Integer number) {
            this.number = number;
        }

        public String getStatus() {
            return status;
        }

        public void setStatus(String status) {
            this.status = status;
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

        public String getReservationRecordCode() {
            return reservationRecordCode;
        }

        public void setReservationRecordCode(String reservationRecordCode) {
            this.reservationRecordCode = reservationRecordCode;
        }
    }
}
