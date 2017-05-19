package com.jandar.alipay.core.struct;


/*
 * 
 * 门诊就诊记录列表
 */
public class OutpatientVisitRecord {
	private String jzxh;   // |就诊序号 | string | 是    |  |
	private String jzrq;   // | 就诊日期 | string | 是    |     |
	private String ksmc ;   //  | 科室名称  | string | 是    |                |
	private String ysxm ;   //| 医生姓名  | string | 是    |          |
	private String zdxx ;//  | 诊断信息  | string | 是    |            |
	public String getJzxh() {
		return jzxh;
	}
	public void setJzxh(String jzxh) {
		this.jzxh = jzxh;
	}
	public String getJzrq() {
		return jzrq;
	}
	public void setJzrq(String jzrq) {
		this.jzrq = jzrq;
	}
	public String getKsmc() {
		return ksmc;
	}
	public void setKsmc(String ksmc) {
		this.ksmc = ksmc;
	}
	public String getYsxm() {
		return ysxm;
	}
	public void setYsxm(String ysxm) {
		this.ysxm = ysxm;
	}
	public String getZdxx() {
		return zdxx;
	}
	public void setZdxx(String zdxx) {
		this.zdxx = zdxx;
	}
	
}
