package com.jandar.alipay.core.struct;
/*
 * 检查单信息
 */
public class InspectionInfo {
	public InspectionInfo(String doctadviseno, String requesttime, String requester, String executetime,
			String executer, String receivetime, String receiver, String stayhospitalmode, String patientid,
			String section, String bedno, String patientname, String sex, String age, String diagnostic,
			String sampletype, String examinaim, String requestmode, String checker, String checktime, String csyq,
			String profiletest) {
		super();
		this.doctadviseno = doctadviseno;
		this.requesttime = requesttime;
		this.requester = requester;
		this.executetime = executetime;
		this.executer = executer;
		this.receivetime = receivetime;
		this.receiver = receiver;
		this.stayhospitalmode = stayhospitalmode;
		this.patientid = patientid;
		this.section = section;
		this.bedno = bedno;
		this.patientname = patientname;
		this.sex = sex;
		this.age = age;
		this.diagnostic = diagnostic;
		this.sampletype = sampletype;
		this.examinaim = examinaim;
		this.requestmode = requestmode;
		this.checker = checker;
		this.checktime = checktime;
		this.csyq = csyq;
		this.profiletest = profiletest;
	}

	public InspectionInfo() {
		super();
		// TODO Auto-generated constructor stub
	}

	private String doctadviseno;// 条码号 string 是 化验单的条码号，这是一个唯一标识
	private String requesttime;// 送检时间 string 是 格式：yyyy-MM-dd HH:mm:ss
	private String requester;// 送检人 string 是
	private String executetime;// 采集时间 string 是 格式：yyyy-MM-dd HH:mm:ss
	private String executer;// 采集人 string 是
	private String receivetime;// 接收时间 string 否 格式：yyyy-MM-dd HH:mm:ss
	private String receiver;// 接收人 string 否
	private String stayhospitalmode;// 标示来源 string 否
	private String patientid;// 病人编号 string 是
	private String section;// 申请科室 string 是
	private String bedno;// 床号 string 否
	private String patientname;// 病人姓名 string 是
	private String sex;// 性别 string 是
	private String age;// 年龄 string 是
	private String diagnostic;// 诊断 string 是
	private String sampletype;// 标本类型 string 是
	private String examinaim;// 检查项目 string 是
	private String requestmode;// 平/急诊 string 否
	private String checker;// 审核人 string 是
	private String checktime;// 审核时间 string 是 格式：yyyy-MM-dd HH:mm:ss
	private String csyq;// 测试仪器 string 否
	private String profiletest;// 测试方法 string 否

	public String getProfiletest() {
		return profiletest;
	}

	public void setProfiletest(String profiletest) {
		this.profiletest = profiletest;
	}

	public String getDoctadviseno() {
		return doctadviseno;
	}

	public void setDoctadviseno(String doctadviseno) {
		this.doctadviseno = doctadviseno;
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

	public String getExecutetime() {
		return executetime;
	}

	public void setExecutetime(String executetime) {
		this.executetime = executetime;
	}

	public String getExecuter() {
		return executer;
	}

	public void setExecuter(String executer) {
		this.executer = executer;
	}

	public String getReceivetime() {
		return receivetime;
	}

	public void setReceivetime(String receivetime) {
		this.receivetime = receivetime;
	}

	public String getReceiver() {
		return receiver;
	}

	public void setReceiver(String receiver) {
		this.receiver = receiver;
	}

	public String getStayhospitalmode() {
		return stayhospitalmode;
	}

	public void setStayhospitalmode(String stayhospitalmode) {
		this.stayhospitalmode = stayhospitalmode;
	}

	public String getPatientid() {
		return patientid;
	}

	public void setPatientid(String patientid) {
		this.patientid = patientid;
	}

	public String getSection() {
		return section;
	}

	public void setSection(String section) {
		this.section = section;
	}

	public String getBedno() {
		return bedno;
	}

	public void setBedno(String bedno) {
		this.bedno = bedno;
	}

	public String getPatientname() {
		return patientname;
	}

	public void setPatientname(String patientname) {
		this.patientname = patientname;
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

	public String getDiagnostic() {
		return diagnostic;
	}

	public void setDiagnostic(String diagnostic) {
		this.diagnostic = diagnostic;
	}

	public String getSampletype() {
		return sampletype;
	}

	public void setSampletype(String sampletype) {
		this.sampletype = sampletype;
	}

	public String getExaminaim() {
		return examinaim;
	}

	public void setExaminaim(String examinaim) {
		this.examinaim = examinaim;
	}

	public String getRequestmode() {
		return requestmode;
	}

	public void setRequestmode(String requestmode) {
		this.requestmode = requestmode;
	}

	public String getChecker() {
		return checker;
	}

	public void setChecker(String checker) {
		this.checker = checker;
	}

	public String getChecktime() {
		return checktime;
	}

	public void setChecktime(String checktime) {
		this.checktime = checktime;
	}

	public String getCsyq() {
		return csyq;
	}

	public void setCsyq(String csyq) {
		this.csyq = csyq;
	}
}
