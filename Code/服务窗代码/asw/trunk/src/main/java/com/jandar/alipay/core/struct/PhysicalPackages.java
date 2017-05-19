package com.jandar.alipay.core.struct;

/*
 * 体检套餐
 */
public class PhysicalPackages {


	public PhysicalPackages() {
		super();
	}
	
	private String packagesID;//套餐ID
	private String packagesName;// 套餐名称  套餐名称（性别）
	private String packagesPrice;//套餐价格 
	private String packagesStandard;// 套餐标准 vip等，可能为空字符串
	private String packagesCategory;// 套餐类别  限制不同性别报名 值为男或女
	
	public String getPackagesID() {
		return packagesID;
	}
	public void setPackagesID(String packagesID) {
		this.packagesID = packagesID;
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
	public String getPackagesStandard() {
		return packagesStandard;
	}
	public void setPackagesStandard(String packagesStandard) {
		this.packagesStandard = packagesStandard;
	}
	public String getPackagesCategory() {
		return packagesCategory;
	}
	public void setPackagesCategory(String packagesCategory) {
		this.packagesCategory = packagesCategory;
	}
	

}
