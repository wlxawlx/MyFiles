package com.jandar.alipay.core.struct;

/*
 * 门诊预约输入参数
 */
public class PhysicalOrderRequest {

    private String userID;// 用户ID
    private String patientName;// 病人姓名
    private String patientSex;// 病人性别 1:男 2：女
    private String birthday;// 出生年月
    private String cardID;//     身份证号
    private String phone;// 联系电话 
    private String isMarried;// 婚姻状况 0：未 1：已
    private String orderDate;// 预约日期
    private String packagesID;// 套餐ID
	public String getUserID() {
		return userID;
	}
	public void setUserID(String userID) {
		this.userID = userID;
	}
	public String getPatientName() {
		return patientName;
	}
	public void setPatientName(String patientName) {
		this.patientName = patientName;
	}
	public String getPatientSex() {
		return patientSex;
	}
	public void setPatientSex(String patientSex) {
		this.patientSex = patientSex;
	}
	public String getBirthday() {
		return birthday;
	}
	public void setBirthday(String birthday) {
		this.birthday = birthday;
	}
	public String getCardID() {
		return cardID;
	}
	public void setCardID(String cardID) {
		this.cardID = cardID;
	}
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	public String getIsMarried() {
		return isMarried;
	}
	public void setIsMarried(String isMarried) {
		this.isMarried = isMarried;
	}
	public String getOrderDate() {
		return orderDate;
	}
	public void setOrderDate(String orderDate) {
		this.orderDate = orderDate;
	}
	public String getPackagesID() {
		return packagesID;
	}
	public void setPackagesID(String packagesID) {
		this.packagesID = packagesID;
	}

   
}
