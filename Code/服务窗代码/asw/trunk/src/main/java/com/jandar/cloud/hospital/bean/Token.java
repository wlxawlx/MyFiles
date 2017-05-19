package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

/**
 * Created by flyhorse on 2016/11/8.
 */
@Document(collection = "token")
public class Token {
    @Field("user_name")
    private String userName;

    @Field("user_type")
    private String userType;

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getUserType() {
        return userType;
    }

    public void setUserType(String userType) {
        this.userType = userType;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public Integer getCreateMill() {
        return createMill;
    }

    public void setCreateMill(Integer createMill) {
        this.createMill = createMill;
    }

    public Integer getRefreshMill() {
        return refreshMill;
    }

    public void setRefreshMill(Integer refreshMill) {
        this.refreshMill = refreshMill;
    }

    public String getCreateDate() {
        return createDate;
    }

    public void setCreateDate(String createDate) {
        this.createDate = createDate;
    }

    public String getRefreshDate() {
        return refreshDate;
    }

    public void setRefreshDate(String refreshDate) {
        this.refreshDate = refreshDate;
    }

    @Field("user_id")
    private String userId;
    @Field("name")
    private String name;
    @Field("token")
    private String token;


    @Field("create_mill")
    private Integer createMill;
    @Field("refresh_mill")
    private Integer refreshMill;

    @Field("create_date")
    private String createDate;
    @Field("refresh_date")
    private String refreshDate;
}
