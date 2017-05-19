/**
 * 文件：UserInfo.java 2015年12月29日
 *
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.alipay.hospital;

import java.io.Serializable;

/**
 * 医院用户信息。
 *
 * @author dys
 * @version 1.0 2015年12月29日
 *
 */
public class UserInfo implements Serializable {

	private static final long serialVersionUID = 6332920694262587088L;

	private String yhid;// 用户注册成功后返回的id

	private String yhxm;

	private String lxdh;

	private String sfzh;

	private String lxdz;

	private String headUrl;

	private String brid;

	private String bkzt;

	private String alipayUserId;

	
	public String getBkzt() {
		return bkzt;
	}

	public void setBkzt(String bkzt) {
		this.bkzt = bkzt;
	}

	public String getYhid() {
		return yhid;
	}

	public void setYhid(String yhid) {
		this.yhid = yhid;
	}

	public String getYhxm() {
		return yhxm;
	}

	public void setYhxm(String yhxm) {
		this.yhxm = yhxm;
	}

	public String getLxdh() {
		return lxdh;
	}

	public void setLxdh(String lxdh) {
		this.lxdh = lxdh;
	}

	public String getSfzh() {
		return sfzh;
	}

	public void setSfzh(String sfzh) {
		this.sfzh = sfzh;
	}

	public String getLxdz() {
		return lxdz;
	}

	public void setLxdz(String lxdz) {
		this.lxdz = lxdz;
	}

	public String getHeadUrl() {
		return headUrl;
	}

	public void setHeadUrl(String headUrl) {
		this.headUrl = headUrl;
	}

	public String getBrid() {
		return brid;
	}

	public void setBrid(String brid) {
		this.brid = brid;
	}

//	public String getBkzt() {
//		return bkzt;
//	}
//
//	public void setBkzt(String bkzt) {
//		this.bkzt = bkzt;
//	}

	public String getAlipayUserId() {
		return alipayUserId;
	}

	public void setAlipayUserId(String alipayUserId) {
		this.alipayUserId = alipayUserId;
	}

}
