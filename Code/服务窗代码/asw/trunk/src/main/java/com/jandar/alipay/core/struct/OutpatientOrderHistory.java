package com.jandar.alipay.core.struct;

/*
 * 门诊预约历史
 */
public class OutpatientOrderHistory {
	public OutpatientOrderHistory(String preengageseq, String preengagedate, String preengagetime, String departcode,
			String departname, String doctorcode, String doctorname, String patientname, String patientidcardno,
			String patientphone, String patientaddress, String preengageno, String place, String fee,
			String preengagestate) {
		super();
		this.preengageseq = preengageseq;
		this.preengagedate = preengagedate;
		this.preengagetime = preengagetime;
		this.departcode = departcode;
		this.departname = departname;
		this.doctorcode = doctorcode;
		this.doctorname = doctorname;
		this.patientname = patientname;
		this.patientidcardno = patientidcardno;
		this.patientphone = patientphone;
		this.patientaddress = patientaddress;
		this.preengageno = preengageno;
		this.place = place;
		this.fee = fee;
		this.preengagestate = preengagestate;
	}

	public OutpatientOrderHistory() {
		super();
		// TODO Auto-generated constructor stub
	}

	private String preengageseq;// 预约流水号 string 是 预约流水号
	private String preengagedate;// 预约日期 string 是 格式：yyyy-MM-dd
	private String preengagetime;// 预约时间 string 是 格式：HH:mm
	private String departcode;// 预约科室 string 否 预约的二科室代码
	private String departname;// 预约科室 string 是 预约的二级科室
	private String doctorcode;// 预约医生 string 否 预约医生的编码
	private String doctorname;// 预约医生 string 是 预约医生的姓名
	private String patientname;// 病人姓名 string 是 给哪个病人预约的
	private String patientidcardno;// 病人身份证号 string 否 病人身份证号
	private String patientphone;// 病人电话 string 否 病人联系电话
	private String patientaddress;// 病人地址 string 否 病人联系地址
	private String preengageno;// 分诊序号 string 是 门诊第几个号
	private String place;// 就诊地点 string 否 就诊地点
	private String fee;// 挂号费 string 是
	private String preengagestate;// 预约状态 integer 是 1：已预约；2：已就诊；3：已取消；4：已爽约

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

	public String getPatientidcardno() {
		return patientidcardno;
	}

	public void setPatientidcardno(String patientidcardno) {
		this.patientidcardno = patientidcardno;
	}

	public String getPatientphone() {
		return patientphone;
	}

	public void setPatientphone(String patientphone) {
		this.patientphone = patientphone;
	}

	public String getPatientaddress() {
		return patientaddress;
	}

	public void setPatientaddress(String patientaddress) {
		this.patientaddress = patientaddress;
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

	public String getFee() {
		return fee;
	}

	public void setFee(String fee) {
		this.fee = fee;
	}

	public String getPreengagestate() {
		return preengagestate;
	}

	public void setPreengagestate(String preengagestate) {
		this.preengagestate = preengagestate;
	}


}
