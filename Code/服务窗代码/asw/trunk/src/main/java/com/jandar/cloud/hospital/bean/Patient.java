package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;
import java.util.List;

/**
 * 云医院的病人信息,包含病人基本信息,病人邮寄地址,病人第一联系人信息
 * Created by zzw on 16/8/30.
 */
@Document(collection = "cloud_patient")
public class Patient {

    //对应于 patientId
    @Field("PatientCode")
    private String patientCode;
    @Field("AlipayUserId")
    private String alipayUserId;
    @Field("WechatOpenId")
    private String wechatOpenId;


    @Field("Name")
    private String name;

    @Field("CurrentDoctorCode")
    private String currentDoctorCode;

    //当前方式 Alipay   Wechat
    @Field("CurrentWay")
    private String currentWay;

    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }

    public String getPatientCode() {
        return patientCode;
    }

    public void setPatientCode(String patientCode) {
        this.patientCode = patientCode;
    }

    public String getAlipayUserId() {
        return alipayUserId;
    }

    public void setAlipayUserId(String alipayUserId) {
        this.alipayUserId = alipayUserId;
    }

    public String getWechatOpenId() {
        return wechatOpenId;
    }

    public void setWechatOpenId(String wechatOpenId) {
        this.wechatOpenId = wechatOpenId;
    }

    public String getCurrentDoctorCode() {
        return currentDoctorCode;
    }

    public void setCurrentDoctorCode(String currentDoctorCode) {
        this.currentDoctorCode = currentDoctorCode;
    }

    public String getCurrentWay() {
        return currentWay;
    }

    public void setCurrentWay(String currentWay) {
        this.currentWay = currentWay;
    }



}
