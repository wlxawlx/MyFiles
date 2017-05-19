package com.jandar.alipay.core.struct;

/*
 * 门诊预约返回信息
 */
public class OutpatientOrderReponse {
	private String preengageseq;// 预约流水号 string 是 预约流水号
	private String preengagedate;// 预约日期 string 是 格式：yyyy-MM-dd
	private String preengagetime;// 预约时间 string 是 格式：HH:mm
	private String departcode;// 预约科室 string 否 预约的二科室代码
	private String departname;// 预约科室 string 是 预约的二级科室
	private String doctorcode;// 预约医生 string 否 预约医生的编码
	private String doctorname;// 预约医生 string 是 预约医生的姓名
	private String patientname;// 病人姓名 string 是 给哪个病人预约的
	private String preengageno;// 分诊序号 string 是 门诊第几个号
	private String place;// 就诊地点 string 否 就诊地点
	
	private String  pdlsh;//pdlsh | 排队序号||
	private String yymsg;//预约信息  | 预约成功后用户手机收到的信息 |

	public String getYymsg() {
		return yymsg;
	}

	public void setYymsg(String yymsg) {
		this.yymsg = yymsg;
	}

	public String getPdlsh() {
		return pdlsh;
	}

	public void setPdlsh(String pdlsh) {
		this.pdlsh = pdlsh;
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

	public String getDoctorcode() {
		return doctorcode;
	}

	public void setDoctorcode(String doctorcode) {
		this.doctorcode = doctorcode;
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
