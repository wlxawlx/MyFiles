package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 检查结果信息表
 * Created by lyx on 2016/10/10.
 */
@Document(collection = "cloud_inspect_result")
public class InspectResult {
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
    @Field("DeptName")
    private String deptName;
    @Field("DeptCode")
    private String deptCode;
    @Field("Receiver")
    private String receiver;
    @Field("ReceiveTime")
    private String receiveTime;
    @Field("Checker")
    private String checker;
    @Field("ResultDetail")
    private String resultDetail;
    @Field("Requestmode")
    private String requestmode;
    @Field("Photo")
    private String photo;
    @Field("Result")
    private String result;


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
     * 检查报告详细
     */
    public String getResultDetail() {
        return resultDetail;
    }

    public void setResultDetail(String resultDetail) {
        this.resultDetail = resultDetail;
    }

    /**
     * 检查报告影像
     */
    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    /**
     * 诊断结果
     */
    public String getResult() {
        return result;
    }

    public void setResult(String result) {
        this.result = result;
    }
}
