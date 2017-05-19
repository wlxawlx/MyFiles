package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 处方信息,包含处方状态及已缴费后的物流信息
 * Created by zzw on 16/8/30.
 */
@Document(collection = "cloud_prescriptions")
public class Prescription {
    @Field("Code")
    private String code;
    @Field("InvoiceNo")
    private String invoiceNo;
    @Field("PatientName")
    private String patientName;
    @Field("PatientCode")
    private String patientCode;
    @Field("DeptCode")
    private String deptCode;
    @Field("DeptName")
    private String deptName;
    @Field("DoctorCode")
    private String doctorCode;
    @Field("DoctorName")
    private String doctorName;
    @Field("PrescribeDate")
    private String prescribeDate;
    @Field("ChargeDate")
    private String chargeDate;
    @Field("PrescriptionSum")
    private String prescriptionSum;
    @Field("TotalSum")
    private String totalSum;
    @Field("PrescriptionType")
    private String prescriptionType;
    @Field("PrescriptionName")
    private String prescriptionName;
    @Field("MedicinalInfo")
    private List<MedicinalInfo> medicinalInfo;
    @Field("SpecialType")
    private String specialType;
    @Field("IsExpress")
    private String isExpress;
    @Field("PayStatus")
    private String payStatus;
    @Field("ExpressNo")
    private String expressNo;
    @Field("ExpressCompany")
    private String expressCompany;
    @Field("SignStatus")
    private String signStatus;
    @Field("ReceiveInfo")
    private ReceiveInfo receiveInfo;
    @Field("SyncStatus")
    private String syncStatus;
    /**
     * 处方流水号
     */
    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
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
     * 病人姓名
     */
    public String getPatientName() {
        return patientName;
    }

    public void setPatientName(String patientName) {
        this.patientName = patientName;
    }

    /**
     * 用户ID
     */
    public String getPatientCode() {
        return patientCode;
    }

    public void setPatientCode(String patientCode) {
        this.patientCode = patientCode;
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
     * 开方日期
     */
    public String getPrescribeDate() {
        return prescribeDate;
    }

    public void setPrescribeDate(String prescribeDate) {
        this.prescribeDate = prescribeDate;
    }

    /**
     * 收费日期
     */
    public String getChargeDate() {
        return chargeDate;
    }

    public void setChargeDate(String chargeDate) {
        this.chargeDate = chargeDate;
    }

    /**
     * 处方金额
     */
    public String getPrescriptionSum() {
        return prescriptionSum;
    }

    public void setPrescriptionSum(String prescriptionSum) {
        this.prescriptionSum = prescriptionSum;
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
     * 处方类型
     */
    public String getPrescriptionType() {
        return prescriptionType;
    }

    public void setPrescriptionType(String prescriptionType) {
        this.prescriptionType = prescriptionType;
    }

    /**
     * 处方名称
     */
    public String getPrescriptionName() {
        return prescriptionName;
    }

    public void setPrescriptionName(String prescriptionName) {
        this.prescriptionName = prescriptionName;
    }


    /**
     * 特种药标志
     */
    public String getSpecialType() {
        return specialType;
    }

    public void setSpecialType(String specialType) {
        this.specialType = specialType;
    }

    /**
     * 自取标志(是否快递)
     */
    public String getisExpress() {
        return getIsExpress();
    }

    public void setisExpress(String isExpress) {
        this.isExpress = isExpress;
    }

    /**
     * 缴费状态
     */
    public String getpayStatus() {
        return getPayStatus();
    }

    public void setpayStatus(String payStatus) {
        this.payStatus = payStatus;
    }

    /**
     * 快递号
     */
    public String getExpressNo() {
        return expressNo;
    }

    public void setExpressNo(String expressNo) {
        this.expressNo = expressNo;
    }

    /**
     * 快递公司
     */
    public String getExpressCompany() {
        return expressCompany;
    }

    public void setExpressCompany(String expressCompany) {
        this.expressCompany = expressCompany;
    }

    /**
     * 签收状态
     */
    public String getSignStatus() {
        return signStatus;
    }

    public void setSignStatus(String signStatus) {
        this.signStatus = signStatus;
    }

    /**
     * 快递收货地址
     */
    public Prescription.ReceiveInfo getReceiveInfo() {
        return receiveInfo;
    }

    public void setReceiveInfo(Prescription.ReceiveInfo receiveInfo) {
        this.receiveInfo = receiveInfo;
    }

    /**
     * 服药信息
     */
    public List<MedicinalInfo> getMedicinalInfo() {
        return medicinalInfo;
    }

    public void setMedicinalInfo(List<MedicinalInfo> medicinalInfo) {
        this.medicinalInfo = medicinalInfo;
    }

    public String getIsExpress() {
        return isExpress;
    }

    public void setIsExpress(String isExpress) {
        this.isExpress = isExpress;
    }

    public String getPayStatus() {
        return payStatus;
    }

    public void setPayStatus(String payStatus) {
        this.payStatus = payStatus;
    }

    public String getSyncStatus() {
        return syncStatus;
    }

    public void setSyncStatus(String syncStatus) {
        this.syncStatus = syncStatus;
    }


    @Document
    public static class MedicinalInfo {
        @Field("Name")
        private String Name;
        @Field("SpecialType")
        private String SpecialType;
        @Field("DrugPecification")
        private String DrugPecification;
        @Field("DrugCount")
        private String DrugCount;
        @Field("Dosage")
        private String Dosage;
        @Field("Frequency")
        private String Frequency;
        @Field("DeliveryWay")
        private String DeliveryWay;

        /**
         * 药品名称
         */
        public String getName() {
            return Name;
        }

        public void setName(String name) {
            this.Name = name;
        }

        /**
         * 特种药标志
         */
        public String getSpecialType() {
            return SpecialType;
        }

        public void setSpecialType(String specialType) {
            this.SpecialType = specialType;
        }

        /**
         * 药品规格
         */
        public String getDrugPecification() {
            return DrugPecification;
        }

        public void setDrugPecification(String drugPecification) {
            this.DrugPecification = drugPecification;
        }

        /**
         * 药品数量
         */
        public String getDrugCount() {
            return DrugCount;
        }

        public void setDrugCount(String drugCount) {
            this.DrugCount = drugCount;
        }

        /**
         * 一次用量
         */
        public String getDosage() {
            return Dosage;
        }

        public void setDosage(String dosage) {
            this.Dosage = dosage;
        }

        /**
         * 使用频次
         */
        public String getFrequency() {
            return Frequency;
        }

        public void setFrequency(String frequency) {
            this.Frequency = frequency;
        }

        /**
         * 给药途径
         */
        public String getDeliveryWay() {
            return DeliveryWay;
        }

        public void setDeliveryWay(String deliveryWay) {
            this.DeliveryWay = deliveryWay;
        }
    }

    @Document
    public static class ReceiveInfo {
        @Field("Name")
        private String Name;
        @Field("PhoneNo")
        private String PhoneNo;
        @Field("ReceiveAddress")
        private String ReceiveAddress;

        /**
         * 收件人姓名
         */
        public String getName() {
            return Name;
        }

        public void setName(String name) {
            this.Name = name;
        }

        /**
         * 收件人手机号
         */
        public String getPhoneNo() {
            return PhoneNo;
        }

        public void setPhoneNo(String phoneNo) {
            this.PhoneNo = phoneNo;
        }

        /**
         * 收件人地址
         */
        public String getReceiveAddress() {
            return ReceiveAddress;
        }

        public void setReceiveAddress(String receiveAddress) {
            this.ReceiveAddress = receiveAddress;
        }
    }

}
