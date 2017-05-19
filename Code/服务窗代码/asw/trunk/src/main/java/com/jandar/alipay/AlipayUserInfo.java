/**
 * 文件：AlipayUserInfo.java 2015年12月28日
 *
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.alipay;

import java.io.Serializable;

/**
 * 从支付宝获得的用户信息。
 *
 * @author dys
 * @version 1.0 2015年12月28日
 *
 */
public class AlipayUserInfo implements Serializable {

	private static final long serialVersionUID = -2301180854604416763L;

	public enum UserType {
		company("公司账户"), people("个人账户");

		private String label;

		UserType(String label) {
			this.label = label;
		}

		public String getLabel() {
			return this.label;
		}
	}

	public enum Gender {

		male("男"), female("女");

		private String label;

		Gender(String label) {
			this.label = label;
		}

		public String getLabel() {
			return this.label;
		}
	}

	public enum CertType {

		sfz("身份证"), hz("护照"), jgz("军官证"), sbz("士兵证"), hxz("回乡证"), lxsfz("临时身份证"), hkb("户口薄"), jgz2("警官证"), tbz(
				"台胞证"), yyzz("营业执照"), qt("其他证件");

		private String label;

		CertType(String label) {
			this.label = label;
		}

		public String getLabel() {
			return this.label;
		}
	}

	private String userId;

	private UserType userType;

	private String realName;

	private String certNo;

	private CertType certType;

	private Gender gender;

	private String phone;

	private String mobile;

	private boolean certified;

	private boolean bankAuth;

	private boolean idAuth;

	private boolean mobileAuth;

	private boolean licenceAuth;

	private boolean studentCertified;

	private String avatar;

	/**
	 * 用户的userId。
	 * 
	 * @return
	 */
	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	/**
	 * 用户类型
	 * 
	 * @return
	 */
	public UserType getUserType() {
		return userType;
	}

	public void setUserType(UserType userType) {
		this.userType = userType;
	}

	/**
	 * 用户的真实姓名，用户类型为公司时为公司名称。
	 * 
	 * @return
	 */
	public String getRealName() {
		return realName;
	}

	public void setRealName(String realName) {
		this.realName = realName;
	}

	/**
	 * 证件号码。
	 * 
	 * @return
	 */
	public String getCertNo() {
		return certNo;
	}

	public void setCertNo(String certNo) {
		this.certNo = certNo;
	}

	/**
	 * 证件类型。
	 * 
	 * @return
	 */
	public CertType getCertType() {
		return certType;
	}

	public void setCertType(CertType certType) {
		this.certType = certType;
	}

	/**
	 * 性别。
	 * 
	 * @return
	 */
	public Gender getGender() {
		return gender;
	}

	public void setGender(Gender gender) {
		this.gender = gender;
	}

	/**
	 * 电话号码。
	 * 
	 * @return
	 */
	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone = phone;
	}

	/**
	 * 手机号码。
	 * 
	 * @return
	 */
	public String getMobile() {
		return mobile;
	}

	public void setMobile(String mobile) {
		this.mobile = mobile;
	}

	/**
	 * 是否通过实名认证。
	 * 
	 * @return
	 */
	public boolean isCertified() {
		return certified;
	}

	public void setCertified(boolean certified) {
		this.certified = certified;
	}

	/**
	 * 是否银行卡认证。
	 * 
	 * @return
	 */
	public boolean isBankAuth() {
		return bankAuth;
	}

	public void setBankAuth(boolean bankAuth) {
		this.bankAuth = bankAuth;
	}

	/**
	 * 是否身份证认证。
	 * 
	 * @return
	 */
	public boolean isIdAuth() {
		return idAuth;
	}

	public void setIdAuth(boolean idAuth) {
		this.idAuth = idAuth;
	}

	/**
	 * 是否手机认证。
	 * 
	 * @return
	 */
	public boolean isMobileAuth() {
		return mobileAuth;
	}

	public void setMobileAuth(boolean mobileAuth) {
		this.mobileAuth = mobileAuth;
	}

	/**
	 * 是否营业执照认证。
	 * 
	 * @return
	 */
	public boolean isLicenceAuth() {
		return licenceAuth;
	}

	public void setLicenceAuth(boolean licenceAuth) {
		this.licenceAuth = licenceAuth;
	}

	/**
	 * 是否学生。
	 * 
	 * @return
	 */
	public boolean isStudentCertified() {
		return studentCertified;
	}

	public void setStudentCertified(boolean studentCertified) {
		this.studentCertified = studentCertified;
	}

	/**
	 * 用户头像。
	 * 
	 * @return
	 */
	public String getAvatar() {
		return avatar;
	}

	public void setAvatar(String avatar) {
		this.avatar = avatar;
	}

}
