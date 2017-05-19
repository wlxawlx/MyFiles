package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

/**
 * Created by Administrator on 2016/12/19.
 */
@Document(collection = "order_relation")
public class OrderRelation {
    @Field("orderid")
    private String orderid;
    @Field("doctorid")
    private String doctorid;
    @Field("patientid")
    private String patientid;
    @Field("ownerpatientid")
    private String ownerpatientid;
    @Field("orderdate")
    private String orderdate;
    @Field("shiftcode")
    private String shiftcode;
    @Field("ordertime")
    private String ordertime;
    @Field("submittopicid")
    private String submittopicid;
    @Field("issame")
    private String issame;

    public String getOrderid() {
        return orderid;
    }

    public void setOrderid(String orderid) {
        this.orderid = orderid;
    }

    public String getDoctorid() {
        return doctorid;
    }

    public void setDoctorid(String doctorid) {
        this.doctorid = doctorid;
    }

    public String getPatientid() {
        return patientid;
    }

    public void setPatientid(String patientid) {
        this.patientid = patientid;
    }

    public String getOwnerpatientid() {
        return ownerpatientid;
    }

    public void setOwnerpatientid(String ownerpatientid) {
        this.ownerpatientid = ownerpatientid;
    }

    public String getOrderdate() {
        return orderdate;
    }

    public void setOrderdate(String orderdate) {
        this.orderdate = orderdate;
    }

    public String getShiftcode() {
        return shiftcode;
    }

    public void setShiftcode(String shiftcode) {
        this.shiftcode = shiftcode;
    }

    public String getOrdertime() {
        return ordertime;
    }

    public void setOrdertime(String ordertime) {
        this.ordertime = ordertime;
    }

    public String getSubmittopicid() {
        return submittopicid;
    }

    public void setSubmittopicid(String submittopicid) {
        this.submittopicid = submittopicid;
    }

    public String getIssame() {
        return issame;
    }

    public void setIssame(String issame) {
        this.issame = issame;
    }
}
