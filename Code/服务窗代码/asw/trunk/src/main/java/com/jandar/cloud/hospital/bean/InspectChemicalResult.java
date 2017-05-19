package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 检验结果信息表
 * Created by lyx on 2016/10/10.
 */
@Document(collection = "cloud_inspect_chemical_result")
public class InspectChemicalResult {
    @Field("Code")
    private String code;
    @Field("Doctadviseno")
    private String doctadviseno;
    @Field("InspectName")
    private String inspectName;
    @Field("PatientName")
    private String patientName;
    @Field("Requester")
    private String requester;
    @Field("ExecuteTime")
    private String executeTime;
    @Field("Executer")
    private String executer;
    @Field("DeptName")
    private String deptName;
    @Field("DeptCode")
    private String deptCode;
    @Field("Stayhospitalmode")
    private String stayhospitalmode;
    @Field("Receiver")
    private String receiver;
    @Field("ReceiveTime")
    private String receiveTime;
    @Field("Checker")
    private String checker;
    @Field("Requestmode")
    private String requestmode;
    @Field("ResultDetail")
    private List<ResultDetail> resultDetail;

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
     *条码号
     */
    public String getDoctadviseno() {
        return doctadviseno;
    }

    public void setDoctadviseno(String doctadviseno) {
        this.doctadviseno = doctadviseno;
    }

    /**
     *检验内容
     */
    public String getInspectName() {
        return inspectName;
    }

    public void setInspectName(String inspectName) {
        this.inspectName = inspectName;
    }

    /**
     *被检人（病人）
     */
    public String getPatientName() {
        return patientName;
    }

    public void setPatientName(String patientName) {
        this.patientName = patientName;
    }

    /**
     *送检人
     */
    public String getRequester() {
        return requester;
    }

    public void setRequester(String requester) {
        this.requester = requester;
    }

    /**
     *采集时间
     */
    public String getExecuteTime() {
        return executeTime;
    }

    public void setExecuteTime(String executeTime) {
        this.executeTime = executeTime;
    }

    /**
     *采集人
     */
    public String getExecuter() {
        return executer;
    }

    public void setExecuter(String executer) {
        this.executer = executer;
    }

    /**
     *申请科室
     */
    public String getDeptName() {
        return deptName;
    }

    public void setDeptName(String deptName) {
        this.deptName = deptName;
    }

    /**
     *申请科室代码
     */
    public String getDeptCode() {
        return deptCode;
    }

    public void setDeptCode(String deptCode) {
        this.deptCode = deptCode;
    }

    /**
     *标本名称
     */
    public String getStayhospitalmode() {
        return stayhospitalmode;
    }

    public void setStayhospitalmode(String stayhospitalmode) {
        this.stayhospitalmode = stayhospitalmode;
    }

    /**
     *检验人（接收人）
     */
    public String getReceiver() {
        return receiver;
    }

    public void setReceiver(String receiver) {
        this.receiver = receiver;
    }

    /**
     *检验时间
     */
    public String getReceiveTime() {
        return receiveTime;
    }

    public void setReceiveTime(String receiveTime) {
        this.receiveTime = receiveTime;
    }

    /**
     *审核人
     */
    public String getChecker() {
        return checker;
    }

    public void setChecker(String checker) {
        this.checker = checker;
    }

    /**
     *平/急诊
     */
    public String getRequestmode() {
        return requestmode;
    }

    public void setRequestmode(String requestmode) {
        this.requestmode = requestmode;
    }

    /**
     *化验报告详细
     */
    public List<ResultDetail> getResultDetail() {
        return resultDetail;
    }

    public void setResultDetail(List<ResultDetail> resultDetail) {
        this.resultDetail = resultDetail;
    }

    @Document
    public static class ResultDetail {
        @Field("Name")
        private String name;
        @Field("Result")
        private String result;
        @Field("Hint")
        private String hint;
        @Field("Range")
        private String range;
        @Field("Unit")
        private String unit;

        /**
         *项目名称
         */
        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        /**
         *结果
         */
        public String getResult() {
            return result;
        }

        public void setResult(String result) {
            this.result = result;
        }

        /**
         *标志
         */
        public String getHint() {
            return hint;
        }

        public void setHint(String hint) {
            this.hint = hint;
        }

        /**
         *健康范围
         */
        public String getRange() {
            return range;
        }

        public void setRange(String range) {
            this.range = range;
        }

        /**
         *项目单位
         */
        public String getUnit() {
            return unit;
        }

        public void setUnit(String unit) {
            this.unit = unit;
        }
    }


}
