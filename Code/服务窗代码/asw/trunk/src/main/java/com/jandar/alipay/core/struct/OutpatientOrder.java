package com.jandar.alipay.core.struct;

import org.omg.CosNaming.NamingContextExtPackage.StringNameHelper;

/*
 * 预约信息列表
 */
public class OutpatientOrder {
	public OutpatientOrder(String preengageseq, String preengagedate, String preengagetime, String departname,
			String doctorname, String patientname, String preengageno, String place) {
		super();
		this.preengageseq = preengageseq;
		this.preengagedate = preengagedate;
		this.preengagetime = preengagetime;
		this.departname = departname;
		this.doctorname = doctorname;
		this.patientname = patientname;
		this.preengageno = preengageno;
		this.place = place;
	}

	public OutpatientOrder() {
		super();
		// TODO Auto-generated constructor stub
	}
	
	private String  hylsh;//号源流水号
	private String preengageseq;// 预约流水号 string 是 预约流水号
	private String preengagedate;// 预约日期 string 是 格式：yyyy-MM-dd
	private String preengagetime;// 预约时间 string 是 格式：HH:mm
	private String departname;// 预约科室 string 是 预约的二级科室
	private String doctorname;// 预约医生 string 是 预约医生的姓名
	private String patientname;// 病人姓名 string 是 给哪个病人预约的
	private String preengageno;// 分诊序号 string 是 门诊第几个号
	private String place;// 就诊地点 string 否 就诊地点
	private String state;//预约状态 1：成功；0：失败 
	private String ordermsg;//预约信息

	public String getHylsh() {
		return hylsh;
	}

	public void setHylsh(String hylsh) {
		this.hylsh = hylsh;
	}

	public String getState() {
		return state;
	}

	public void setState(String state) {
		this.state = state;
	}

	public String getOrdermsg() {
		return ordermsg;
	}

	public void setOrdermsg(String ordermsg) {
		this.ordermsg = ordermsg;
	}

	public String getPreengageseq() {
		return preengageseq;
	}

	public void setPreengageseq(String preengageseq) {
		this.preengageseq = preengageseq;
	}

	public String getPreengagedate() {
		return preengagedate;
	}

	public void setPreengagedate(String preengagedate) {
		this.preengagedate = preengagedate;
	}

	public String getPreengagetime() {
		return preengagetime;
	}

	public void setPreengagetime(String preengagetime) {
		this.preengagetime = preengagetime;
	}

	public String getDepartname() {
		return departname;
	}

	public void setDepartname(String departname) {
		this.departname = departname;
	}

	public String getDoctorname() {
		return doctorname;
	}

	public void setDoctorname(String doctorname) {
		this.doctorname = doctorname;
	}

	public String getPatientname() {
		return patientname;
	}

	public void setPatientname(String patientname) {
		this.patientname = patientname;
	}

	public String getPreengageno() {
		return preengageno;
	}

	public void setPreengageno(String preengageno) {
		this.preengageno = preengageno;
	}

	public String getPlace() {
		return place;
	}

	public void setPlace(String place) {
		this.place = place;
	}
}
