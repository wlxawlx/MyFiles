package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

/**
 * 订单信息
 * Created by jhf on 2016/11/10.
 */
@Document(collection = "cloud_order")
public class CloudOrder {
    /**
     * 主键ID
     */
    @Id
    private ObjectId id;
    /**
     *订单编号
     */
    @Field("OrderNumber")
    private String orderNumber;
    /**
     *订单类型,YY:预约单, ZY:住院单, JC:检查单, CF:处方单
     */
    @Field("OrderType")
    private String orderType;
    /**
     *单据代码,预约单、住院单、检查单、处方单的code
     */
    @Field("OrderCode")
    private String OrderCode;
    /**
     *订单状态,0:待付款 5:付款成功 10:付款失败 15:退款
     */
    @Field("OrderStatus")
    private Integer orderStatus;
    /**
     *付款账户
     */
    @Field("PaymentAccount")
    private String paymentAccount;
    /**
     *病人代码
     */
    @Field("PatientCode")
    private String patientCode;
    /**
     *病人名称
     */
    @Field("PatientName")
    private String patientName;
    /**
     *医生代码
     */
    @Field("DoctorCode")
    private String doctorCode;
    /**
     *医生名称
     */
    @Field("DoctorName")
    private String doctorName;
    /**
     *原价
     */
    @Field("Price")
    private String price;
    /**
     *优惠金额
     */
    @Field("DiscountMoney")
    private String discountMoney;
    /**
     *实付金额
     */
    @Field("PaymentMoney")
    private String paymentMoney;
    /**
     *备注
     */
    @Field("Comment")
    private String comment;
    /**
     *创建时间
     */
    @Field("CreateDate")
    private Date createDate;
    /**
     *付款时间
     */
    @Field("PaymentDate")
    private Date paymentDate;
    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }

    public String getOrderNumber() {
        return orderNumber;
    }

    public void setOrderNumber(String orderNumber) {
        this.orderNumber = orderNumber;
    }

    public String getOrderType() {
        return orderType;
    }

    public void setOrderType(String orderType) {
        this.orderType = orderType;
    }

    public String getOrderCode() {
        return OrderCode;
    }

    public void setOrderCode(String orderCode) {
        OrderCode = orderCode;
    }

    public Integer getOrderStatus() {
        return orderStatus;
    }

    public void setOrderStatus(Integer orderStatus) {
        this.orderStatus = orderStatus;
    }

    public String getPaymentAccount() {
        return paymentAccount;
    }

    public void setPaymentAccount(String paymentAccount) {
        this.paymentAccount = paymentAccount;
    }

    public String getPatientCode() {
        return patientCode;
    }

    public void setPatientCode(String patientCode) {
        this.patientCode = patientCode;
    }

    public String getPatientName() {
        return patientName;
    }

    public void setPatientName(String patientName) {
        this.patientName = patientName;
    }

    public String getDoctorCode() {
        return doctorCode;
    }

    public void setDoctorCode(String doctorCode) {
        this.doctorCode = doctorCode;
    }

    public String getDoctorName() {
        return doctorName;
    }

    public void setDoctorName(String doctorName) {
        this.doctorName = doctorName;
    }

    public String getPrice() {
        return price;
    }

    public void setPrice(String price) {
        this.price = price;
    }

    public String getDiscountMoney() {
        return discountMoney;
    }

    public void setDiscountMoney(String discountMoney) {
        this.discountMoney = discountMoney;
    }

    public String getPaymentMoney() {
        return paymentMoney;
    }

    public void setPaymentMoney(String paymentMoney) {
        this.paymentMoney = paymentMoney;
    }

    public String getComment() {
        return comment;
    }

    public void setComment(String comment) {
        this.comment = comment;
    }

    public Date getCreateDate() {
        return createDate;
    }

    public void setCreateDate(Date createDate) {
        this.createDate = createDate;
    }

    public Date getPaymentDate() {
        return paymentDate;
    }

    public void setPaymentDate(Date paymentDate) {
        this.paymentDate = paymentDate;
    }
}
