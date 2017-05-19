package com.jandar.alipay.core.struct;

/*
 * 体检状态信息
 */
public class PhysicalStatusInfo {


	public PhysicalStatusInfo() {
		super();
	}
	
	private String physicalID;//体检编号
	private String name;// 姓名
	private String sex;//性别
	private String age;// 年龄
	private String packagesName;//套餐名称
	private String physicalDate;// 体检日期
	private String physicalStatus;//体检状态
	public String getPhysicalID() {
		return physicalID;
	}
	public void setPhysicalID(String physicalID) {
		this.physicalID = physicalID;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getSex() {
		return sex;
	}
	public void setSex(String sex) {
		this.sex = sex;
	}
	public String getAge() {
		return age;
	}
	public void setAge(String age) {
		this.age = age;
	}
	public String getPackagesName() {
		return packagesName;
	}
	public void setPackagesName(String packagesName) {
		this.packagesName = packagesName;
	}
	public String getPhysicalDate() {
		return physicalDate;
	}
	public void setPhysicalDate(String physicalDate) {
		this.physicalDate = physicalDate;
	}
	public String getPhysicalStatus() {
		return physicalStatus;
	}
	public void setPhysicalStatus(String physicalStatus) {
		this.physicalStatus = physicalStatus;
	}
}
