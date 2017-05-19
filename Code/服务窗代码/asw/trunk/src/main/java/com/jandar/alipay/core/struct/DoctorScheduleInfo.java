package com.jandar.alipay.core.struct;
/*
 * 医生排班信息
 */
public class DoctorScheduleInfo {
	private String scheduleseq;//	排班流水号	string	是	
	private String departcode;//	科室编码	string	是	二级科室代码
	private String departname;//	科室名称	string	是	二级科室名称
	private String doctorcode;	//医生编码	string	是	
	private String doctorname	;//医生姓名	string	是	
	private String special	;//特需标志	string	是	1：特需；0：普通
	private String remain	;//可约人数	string	是	还可预约人数
	private String total;//	预约人数	string	是	可预约总人数
	private String address;//	就诊地点	string	是	
	private String scheduledate;//	排班日期	string	是	格式：yyyy-MM-dd
	private String shiftcode	;//班次编码	string	是	1:上午，2：下午
	private String shiftname;//	班次名称	string	是	上午或下午
	private String fee;//	挂号费	string	是	
	public String getScheduleseq() {
		return scheduleseq;
	}
	public void setScheduleseq(String scheduleseq) {
		this.scheduleseq = scheduleseq;
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
	public String getSpecial() {
		return special;
	}
	public void setSpecial(String special) {
		this.special = special;
	}
	public String getRemain() {
		return remain;
	}
	public void setRemain(String remain) {
		this.remain = remain;
	}
	public String getTotal() {
		return total;
	}
	public void setTotal(String total) {
		this.total = total;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getScheduledate() {
		return scheduledate;
	}
	public void setScheduledate(String scheduledate) {
		this.scheduledate = scheduledate;
	}
	public String getShiftcode() {
		return shiftcode;
	}
	public void setShiftcode(String shiftcode) {
		this.shiftcode = shiftcode;
	}
	public String getShiftname() {
		return shiftname;
	}
	public void setShiftname(String shiftname) {
		this.shiftname = shiftname;
	}
	public String getFee() {
		return fee;
	}
	public void setFee(String fee) {
		this.fee = fee;
	}
	
}
