package com.jandar.alipay.core.struct;


/*
 * 
 * 门诊就诊信息（电子病历）
 */
public class OutpatientVisitInfo {
	private String mzzs;   // |主诉 | string | 是    |  |
	private String xbs;   // | 现病史 | string | 是    |     |
	private String jws ;   //  | 既往史  | string | 是    |                |
	private String grs ;   //| 个人史  | string | 是    |          |
	private String gms ;//  | 过敏史  | string | 是    |            |
	private String hys;   // |婚育史 | string | 是    |  |
	private String jzs;   // | 家族史 | string | 是    |     |
	private String tgjc ;   //  | 体格检查  | string | 是    |                |
	private String fzjc ;   //| 辅助检查  | string | 是    |          |
	private String clyj ;//  | 处理意见  | string | 是    |            |
	public String getMzzs() {
		return mzzs;
	}
	public void setMzzs(String mzzs) {
		this.mzzs = mzzs;
	}
	public String getXbs() {
		return xbs;
	}
	public void setXbs(String xbs) {
		this.xbs = xbs;
	}
	public String getJws() {
		return jws;
	}
	public void setJws(String jws) {
		this.jws = jws;
	}
	public String getGrs() {
		return grs;
	}
	public void setGrs(String grs) {
		this.grs = grs;
	}
	public String getGms() {
		return gms;
	}
	public void setGms(String gms) {
		this.gms = gms;
	}
	public String getHys() {
		return hys;
	}
	public void setHys(String hys) {
		this.hys = hys;
	}
	public String getJzs() {
		return jzs;
	}
	public void setJzs(String jzs) {
		this.jzs = jzs;
	}
	public String getTgjc() {
		return tgjc;
	}
	public void setTgjc(String tgjc) {
		this.tgjc = tgjc;
	}
	public String getFzjc() {
		return fzjc;
	}
	public void setFzjc(String fzjc) {
		this.fzjc = fzjc;
	}
	public String getClyj() {
		return clyj;
	}
	public void setClyj(String clyj) {
		this.clyj = clyj;
	}
	
}
