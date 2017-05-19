package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

/**
 * 住院申请单信息表
 * Created by lyx on 2016/9/28.
 */
@Document(collection = "cloud_inhospital")
public class Inhospital {
    @Field("Code")
    private String code;
    @Field("InhospitalName")
    private String inhospitalName;
    @Field("InvoiceNo")
    private String invoiceNo;
    @Field("PatientName")
    private String patientName;
    @Field("PatientCode")
    private String patientCode;
    @Field("PatientIdNo")
    private String patientIdNo;
    @Field("DeptCode")
    private String deptCode;
    @Field("DeptName")
    private String deptName;
    @Field("DoctorCode")
    private String doctorCode;
    @Field("DoctorName")
    private String doctorName;
    @Field("PrescribeDate")
    private Date prescribeDate;
    @Field("ChargeDate")
    private Date chargeDate;
    @Field("PrestoreSum")
    private String prestoreSum;
    @Field("TotalSum")
    private String totalSum;
    @Field("PayStatus")
    private String payStatus;
    @Field("InHospitalStatus")
    private String inHospitalStatus;
    @Field("ContectInfo")
    private ContectInfo contectInfo;
    @Field("Bunk")
    private String bunk;
    @Field("SyncStatus")
    private String syncStatus;
    private String prescribeDate_string;

    /**
     * 住院单流水号
     */
    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    /**
     * 住院单名称
     */
    public String getInhospitalName() {
        return inhospitalName;
    }

    public void setInhospitalName(String inhospitalName) {
        this.inhospitalName = inhospitalName;
    }

    /**
     * 发票号码
     */
    public String getInvoiceNo() {
        return invoiceNo;
    }

    public void setInvoiceNo(String invoiceNo) {
        this.invoiceNo = invoiceNo;
    }

    /**
     * 病人名称
     */
    public String getPatientName() {
        return patientName;
    }

    public void setPatientName(String patientName) {
        this.patientName = patientName;
    }

    /**
     * 病人ID
     */
    public String getPatientCode() {
        return patientCode;
    }

    public void setPatientCode(String patientCode) {
        this.patientCode = patientCode;
    }

    /**
     * 病人身份证号码
     */
    public String getPatientIdNo() {
        return patientIdNo;
    }

    public void setPatientIdNo(String patientIdNo) {
        this.patientIdNo = patientIdNo;
    }

    /**
     * 科室编码
     */
    public String getDeptCode() {
        return deptCode;
    }

    public void setDeptCode(String deptCode) {
        this.deptCode = deptCode;
    }

    /**
     * 科室名称
     */
    public String getDeptName() {
        return deptName;
    }

    public void setDeptName(String deptName) {
        this.deptName = deptName;
    }

    /**
     * 医生编码
     */
    public String getDoctorCode() {
        return doctorCode;
    }

    public void setDoctorCode(String doctorCode) {
        this.doctorCode = doctorCode;
    }

    /**
     * 医生姓名
     */
    public String getDoctorName() {
        return doctorName;
    }

    public void setDoctorName(String doctorName) {
        this.doctorName = doctorName;
    }

    /**
     * 开单日期
     */
    public Date getPrescribeDate() {
        return prescribeDate;
    }

    public void setPrescribeDate(Date prescribeDate) {
        this.prescribeDate = prescribeDate;
    }

    /**
     * 收费日期
     */
    public Date getChargeDate() {
        return chargeDate;
    }

    public void setChargeDate(Date chargeDate) {
        this.chargeDate = chargeDate;
    }

    /**
     * 预存金额
     */
    public String getPrestoreSum() {
        return prestoreSum;
    }

    public void setPrestoreSum(String prestoreSum) {
        this.prestoreSum = prestoreSum;
    }

    /**
     * 总计金额
     */
    public String getTotalSum() {
        return totalSum;
    }

    public void setTotalSum(String totalSum) {
        this.totalSum = totalSum;
    }

    /**
     * 缴费状态
     */
    public String getPayStatus() {
        return payStatus;
    }

    public void setPayStatus(String payStatus) {
        this.payStatus = payStatus;
    }

    /**
     * 入住状态
     */
    public String getInHospitalStatus() {
        return inHospitalStatus;
    }

    public void setInHospitalStatus(String inHospitalStatus) {
        this.inHospitalStatus = inHospitalStatus;
    }

    /**
     * 联络人信息
     */
    public Inhospital.ContectInfo getContectInfo() {
        return contectInfo;
    }

    public void setContectInfo(Inhospital.ContectInfo contectInfo) {
        this.contectInfo = contectInfo;
    }

    /**
     * 床号
     */
    public String getBunk() {
        return bunk;
    }

    public void setBunk(String bunk) {
        this.bunk = bunk;
    }

    public String getPrescribeDate_string() {
        return prescribeDate_string;
    }

    public void setPrescribeDate_string(String prescribeDate_string) {
        this.prescribeDate_string = prescribeDate_string;
    }

    public String getSyncStatus() {
        return syncStatus;
    }

    public void setSyncStatus(String syncStatus) {
        this.syncStatus = syncStatus;
    }

    @Document
    public static class ContectInfo {
        @Field("Name")
        private String Name;
        @Field("PhoneNo")
        private String PhoneNo;
        @Field("Relation")
        private String Relation;

        /**
         * 姓名
         */
        public String getName() {
            return Name;
        }

        public void setName(String name) {
            this.Name = name;
        }

        /**
         * 联络电话
         */
        public String getPhoneNo() {
            return PhoneNo;
        }

        public void setPhoneNo(String phoneNo) {
            this.PhoneNo = phoneNo;
        }

        /**
         * 关系
         */
        public String getRelation() {
            return Relation;
        }

        public void setRelation(String relation) {
            this.Relation = relation;
        }
    }


}
