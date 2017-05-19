package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

/**
 * 缴费记录表
 * Created by lyx on 2016/9/27.
 */
@Document(collection = "cloud_payment_list")
public class PaymentList {
    @Field("Code")
    private  String Code;
    @Field("Date")
    private  Date Date;
    @Field("Content")
    private  String Content;
    @Field("Money")
    private  String Money;
    @Field("CutoffTime")
    private Date CutoffTime;

    /**
     * 缴费流水号
     */
    public String getCode() {
        return Code;
    }

    public void setCode(String code) {
        this.Code = code;
    }

    /**
     * 缴费时间
     */
    public java.util.Date getDate() {
        return Date;
    }

    public void setDate(java.util.Date date) {
        this.Date = date;
    }

    /**
     * 缴费事项
     */
    public String getContent() {
        return Content;
    }

    public void setContent(String content) {
        this.Content = content;
    }

    /**
     * 缴费金额
     */
    public String getMoney() {
        return Money;
    }

    public void setMoney(String money) {
        this.Money = money;
    }


    /**
     * 截止缴费时间，只要检查单缴费才有
     */
    public java.util.Date getCutoffTime() {
        return CutoffTime;
    }

    public void setCutoffTime(java.util.Date cutoffTime) {
        this.CutoffTime = cutoffTime;
    }
}
