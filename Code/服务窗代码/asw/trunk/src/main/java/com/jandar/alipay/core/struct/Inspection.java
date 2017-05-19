package com.jandar.alipay.core.struct;

/*
 * 检查单
 */
public class Inspection {
	public Inspection(String doctadviseno, String examinaim, String requesttime, String requester) {
		super();
		this.doctadviseno = doctadviseno;
		this.examinaim = examinaim;
		this.requesttime = requesttime;
		this.requester = requester;
	}

	public Inspection() {
		super();
		// TODO Auto-generated constructor stub
	}

	private String doctadviseno;// 条码号 string 是 化验单的条码号，这是一个唯一标识
	private String examinaim;// 检验内容 string 是 化验单的化验内容
	private String requesttime;// 送检时间 string 是 格式：yyyy-MM-dd HH:mm:ss
	private String requester;// 送检人 string 是 开单的医生

	public String getDoctadviseno() {
		return doctadviseno;
	}

	public void setDoctadviseno(String doctadviseno) {
		this.doctadviseno = doctadviseno;
	}

	public String getExaminaim() {
		return examinaim;
	}

	public void setExaminaim(String examinaim) {
		this.examinaim = examinaim;
	}

	public String getRequesttime() {
		return requesttime;
	}

	public void setRequesttime(String requesttime) {
		this.requesttime = requesttime;
	}

	public String getRequester() {
		return requester;
	}

	public void setRequester(String requester) {
		this.requester = requester;
	}
}
