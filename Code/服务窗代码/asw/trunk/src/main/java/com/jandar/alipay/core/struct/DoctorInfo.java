package com.jandar.alipay.core.struct;

/*
 * 医生信息
 */
public class DoctorInfo {
	private String code; // 医生代码 | string | 是 |
	private String name;// | 医生姓名 | string | 是 |
	private String sex;// | 医生性别 | string | 否 | 0：女；1：男 |
	private String departcode; // 科室代码 | string | 是 | |
	private String departname; // 科室名称 | string | 是 | |
	private String pictureurl; // 医生照片 | string | 否 | 医生照片的URL地址 |
	private String level;// |医生等级 | string | 是 | 如“主任医师” |
	private String recommend; // 医生简介 | string | 否 | 医生的介绍信息，如毕业等 |
	private String adept; // 医生擅长 | string | 否 | 医生的擅长内容与专业 |
	private String stopdate;// | 停诊日期 | string | 是 | 格式：yyyy-MM-dd |
	private String reserve;// | reserve | 可约人数 | integer | 是 | 这个医生可以预约的人数
	private String scheduledates;// | scheduledates | 排班日期 |
												// stringlist | 是 |
												// 子符串列表，表示这个医生在这几天有排班 |
	private String  stopshift ;// | 停诊班次 | string | 是    | 0：上午，1：下午 |
	private String  ksmc;// | 科室名称 | string | 是    |      |
	private String  ksbm;// | 科室编码 | string | 是    |      |
	
	public String getKsmc() {
		return ksmc;
	}

	public void setKsmc(String ksmc) {
		this.ksmc = ksmc;
	}

	public String getKsbm() {
		return ksbm;
	}

	public void setKsbm(String ksbm) {
		this.ksbm = ksbm;
	}

	public String getStopshift() {
		return stopshift;
	}

	public void setStopshift(String stopshift) {
		this.stopshift = stopshift;
	}

	public String getStopdate() {
		return stopdate;
	}

	public String getReserve() {
		return reserve;
	}


	public void setReserve(String reserve) {
		this.reserve = reserve;
	}

	public String getScheduledates() {
		return scheduledates;
	}

	public void setScheduledates(String scheduledates) {
		this.scheduledates = scheduledates;
	}

	public void setStopdate(String stopdate) {
		this.stopdate = stopdate;
	}

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
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

	public String getDepartcode() {
		return departcode;
	}

	public void setDepartcode(String departcode) {
		this.departcode = departcode;
	}

	public String getDepartname() {
		return departname;
	}

	public void setDepartname(String departname) {
		this.departname = departname;
	}

	public String getPictureurl() {
		return pictureurl;
	}

	public void setPictureurl(String pictureurl) {
		this.pictureurl = pictureurl;
	}

	public String getLevel() {
		return level;
	}

	public void setLevel(String level) {
		this.level = level;
	}

	public String getRecommend() {
		return recommend;
	}

	public void setRecommend(String recommend) {
		this.recommend = recommend;
	}

	public String getAdept() {
		return adept;
	}

	public void setAdept(String adept) {
		this.adept = adept;
	}



}
