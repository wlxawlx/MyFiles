package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;
import java.util.Date;


/**
 * 排班医生信息，保函医生排班信息列表
 * Created by admin on 2016/9/19.
 */
@Document(collection = "cloud_doctor")
public class Doctor {
    @Id
    private ObjectId id;

    @Field("Code")
    private String code;

    //用小写的 name 为了兼容即时通讯
    @Field("name")
    private String name;

    //用小写的 name 为了兼容即时通讯
    @Field("user_name")
    private String userName;

    @Field("pwd")
    private String pwd;

    @Field("DeptCode")
    private String deptCode;

    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getPwd() {
        return pwd;
    }

    public void setPwd(String pwd) {
        this.pwd = pwd;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public String getDeptCode() {
        return deptCode;
    }
    public void setDeptCode(String deptCode) {
        this.deptCode = deptCode;
    }
}
