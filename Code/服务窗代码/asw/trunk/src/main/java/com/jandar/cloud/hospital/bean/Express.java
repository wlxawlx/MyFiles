package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 物流信息表
 * Created by lyx on 2016/10/20.
 */
@Document(collection = "cloud_express")
public class Express {
    @Field("Code")
    private String code;
    @Field("ExpressCompany")
    private String expressCompany;
    @Field("ExpressNo")
    private String expressNo;
    @Field("ExpressPhone")
    private String expressPhone;
    @Field("ExpressStatus")
    private String expressStatus;
    @Field("ExpressInfo")
    private List<ExpressInfo> expressInfo;

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
     * 物流公司
     */
    public String getExpressCompany() {
        return expressCompany;
    }

    public void setExpressCompany(String expressCompany) {
        this.expressCompany = expressCompany;
    }

    /**
     * 物流号
     */
    public String getExpressNo() {
        return expressNo;
    }

    public void setExpressNo(String expressNo) {
        this.expressNo = expressNo;
    }

    /**
     * 官方电话
     */
    public String getExpressPhone() {
        return expressPhone;
    }

    public void setExpressPhone(String expressPhone) {
        this.expressPhone = expressPhone;
    }

    /**
     * 物流状态
     */
    public String getExpressStatus() {
        return expressStatus;
    }

    public void setExpressStatus(String expressStatus) {
        this.expressStatus = expressStatus;
    }

    /**
     * 运单详情
     */
    public List<ExpressInfo> getExpressInfo() {
        return expressInfo;
    }

    public void setExpressInfo(List<ExpressInfo> expressInfo) {
        this.expressInfo = expressInfo;
    }

    @Document
    public static class ExpressInfo {
        @Field("Date")
        private String date;
        @Field("Place")
        private String place;

        /**
         * 运单到达时间
         */
        public String getDate() {
            return date;
        }

        public void setDate(String date) {
            this.date = date;
        }

        /**
         * 运单到达位置
         */
        public String getPlace() {
            return place;
        }

        public void setPlace(String place) {
            this.place = place;
        }
    }


}
