package com.jandar.alipay.core.struct;

/*
 * 预约信息
 */
public class PhysicalOrderInfo {

    private String orderID;// 预约流水号
    private String userID;// 用户ID
    private String orderDate;// 预约日期
    private String patientName;// 病人姓名
    private String cardID;// 身份证号
    private String phone;// 联系电话
    private String packagesName;// 套餐名称
    private String packagesPrice;// 套餐价格
    private String orderStatus;// 预约状态
    private String physicalID;// 预约状态
	
	
	public String getPhysicalID() {
		return physicalID;
	}
	public void setPhysicalID(String physicalID) {
		this.physicalID = physicalID;
	}
	public String getOrderID() {
		return orderID;
	}
	public void setOrderID(String orderID) {
		this.orderID = orderID;
	}
	public String getUserID() {
		return userID;
	}
	public void setUserID(String userID) {
		this.userID = userID;
	}
	public String getOrderDate() {
		return orderDate;
	}
	public void setOrderDate(String orderDate) {
		this.orderDate = orderDate;
	}
	public String getPatientName() {
		return patientName;
	}
	public void setPatientName(String patientName) {
		this.patientName = patientName;
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
	public String getPackagesName() {
		return packagesName;
	}
	public void setPackagesName(String packagesName) {
		this.packagesName = packagesName;
	}
	public String getPackagesPrice() {
		return packagesPrice;
	}
	public void setPackagesPrice(String packagesPrice) {
		this.packagesPrice = packagesPrice;
	}
	public String getOrderStatus() {
		return orderStatus;
	}
	public void setOrderStatus(String orderStatus) {
		this.orderStatus = orderStatus;
	}
	
  
}
