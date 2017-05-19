package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;
import java.util.List;

/**
 * Created by flyhorse on 2016/12/16.
 */
@Document(collection = "cloud_inspection")
public class InspectMain {
    @Field("Code")
    private  String code;
    @Field("InspectName")
    private String inspectName;
    @Field("InvoiceNo")
    private  String invoiceNo;
    @Field("PatientName")
    private  String patientName;
    @Field("PatientCode")
    private  String patientCode;
    @Field("DeptCode")
    private  String deptCode;
    @Field("DeptName")
    private  String deptName;
    @Field("DoctorCode")
    private  String doctorCode;
    @Field("DoctorName")
    private  String doctorName;
    @Field("PrescribeDate")
    private  String prescribeDate;
    @Field("CutoffTime")
    private Date cutoffTime;
    @Field("ChargeDate")
    private String chargeDate;
    @Field("TotalSum")
    private String totalSum;
    @Field("PayStatus")
    private String payStatus;
    @Field("InspectStatus")
    private String inspectStatus;
    @Field("SyncStatus")
    private String syncStatus;
    @Field("InspectItem")
    private List<InspectItem> inspectItem;

    public String getSyncStatus() {
        return syncStatus;
    }

    public void setSyncStatus(String syncStatus) {
        this.syncStatus = syncStatus;
    }

    @Document
    public static class InspectItem{
        @Field("ItemName")
        private String itemName;
        @Field("Date")
        private Date date;
        @Field("Sum")
        private String sum;
        @Field("Attention")
        private String attention;

        private String dateString;
        /**
         * 项目名称
         */
        public String getItemName() {
            return itemName;
        }

        public void setItemName(String itemName) {
            this.itemName = itemName;
        }

        /**
         * 检查时间
         */
        public Date getDate() {
            return date;
        }

        public void setDate(Date date) {
            this.date = date;
        }

        /**
         * 注意事项
         */
        public String getAttention() {
            return attention;
        }

        public void setAttention(String attention) {
            this.attention = attention;
        }

        public String getCheckDate() {  return getDateString();  }

        public void setCheckDate(String date) {
            this.setDateString(date);
        }

        public String getSum() {
            return sum;
        }

        public void setSum(String sum) {
            this.sum = sum;
        }

        public String getDateString() {
            return dateString;
        }

        public void setDateString(String dateString) {
            this.dateString = dateString;
        }
    }
    /**
     * 检查单流水号
     */
    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }
    /**
     * 检查单名称
     */
    public String getInspectName() {
        return inspectName;
    }

    public void setInspectName(String inspectName) {
        this.inspectName = inspectName;
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
     * 病人代码，用户ID
     */
    public String getPatientCode() {
        return patientCode;
    }

    public void setPatientCode(String patientCode) {
        this.patientCode = patientCode;
    }
    /**
     * 科室代码
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
     *医生编码
     */
    public String getDoctorCode() {
        return doctorCode;
    }

    public void setDoctorCode(String doctorCode) {
        this.doctorCode = doctorCode;
    }
    /**
     *医生名称
     */
    public String getDoctorName() {
        return doctorName;
    }

    public void setDoctorName(String doctorName) {
        this.doctorName = doctorName;
    }
    /**
     *开单日期
     */
    public String getPrescribeDate() {
        return prescribeDate;
    }

    public void setPrescribeDate(String prescribeDate) {
        this.prescribeDate = prescribeDate;
    }
    /**
     *缴费截止时间
     */
    public Date getCutoffTime() {
        return cutoffTime;
    }

    public void setCutoffTime(Date cutoffTime) {
        this.cutoffTime = cutoffTime;
    }
    /**
     *收费日期
     */
    public String getChargeDate() {
        return chargeDate;
    }

    public void setChargeDate(String chargeDate) {
        this.chargeDate = chargeDate;
    }
    /**
     *总计金额
     */
    public String getTotalSum() {
        return totalSum;
    }

    public void setTotalSum(String totalSum) {
        this.totalSum = totalSum;
    }
    /**
     *缴费状态
     */
    public String getPayStatus() {
        return payStatus;
    }

    public void setPayStatus(String payStatus) {
        this.payStatus = payStatus;
    }
    /**
     *检查状态
     */
    public String getInspectStatus() {
        return inspectStatus;
    }

    public void setInspectStatus(String inspectStatus) {
        this.inspectStatus = inspectStatus;
    }

    /**
     * 检查项目
     */
    public List<InspectItem> getInspectItem() {
        return inspectItem;
    }

    public void setInspectItem(List<InspectItem> inspectItem) {
        this.inspectItem = inspectItem;
    }

}
