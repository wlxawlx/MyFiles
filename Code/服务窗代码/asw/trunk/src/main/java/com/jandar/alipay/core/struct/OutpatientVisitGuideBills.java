package com.jandar.alipay.core.struct;


/*
 * 
 * 门诊就诊指引单
 */
public class OutpatientVisitGuideBills {
	private String cfxh;   // |处方序号 | string | 是    |  |
	private String fphm;   // | 发票号码 | string | 是    |     |
	private String kfrq ;   //  | 开方日期  | string | 是    |                |
	private String cflx ;   //| 处方类型  | string | 是    |          |
	private String kfks ;//  | 开方科室  | string | 是    |            |
	private String ksmc;   // | 开方科室名称 | string | 是    |  |
	private String zsks;   // | 执行科室 | string | 是    |     |
	private String zsksmc ;   //  | 执行科室名称 | string | 是    |                |
	private String fyck ;   //| 发药窗口  | string | 是    |          |
	private String zynr ;//  | 指引内容  | string | 是    |            |
	private String zywz ;   //| 指引位置  | string | 是    |          |
	private String zspb ;//  | 执行标志  | string | 是    |            |
	private String yjpb;   // | 医技判别 | string | 是    |  |
	private String ysxm;   // | 医生姓名 | string | 是    |     |
	private String zjje ;   //  | 总计金额  | string | 是    |                |
	public String getCfxh() {
		return cfxh;
	}
	public void setCfxh(String cfxh) {
		this.cfxh = cfxh;
	}
	public String getFphm() {
		return fphm;
	}
	public void setFphm(String fphm) {
		this.fphm = fphm;
	}
	public String getKfrq() {
		return kfrq;
	}
	public void setKfrq(String kfrq) {
		this.kfrq = kfrq;
	}
	public String getCflx() {
		return cflx;
	}
	public void setCflx(String cflx) {
		this.cflx = cflx;
	}
	public String getKfks() {
		return kfks;
	}
	public void setKfks(String kfks) {
		this.kfks = kfks;
	}
	
	public String getKsmc() {
		return ksmc;
	}
	public void setKsmc(String ksmc) {
		this.ksmc = ksmc;
	}
	public String getZsks() {
		return zsks;
	}
	public void setZsks(String zsks) {
		this.zsks = zsks;
	}
	public String getZsksmc() {
		return zsksmc;
	}
	public void setZsksmc(String zsksmc) {
		this.zsksmc = zsksmc;
	}
	public String getFyck() {
		return fyck;
	}
	public void setFyck(String fyck) {
		this.fyck = fyck;
	}
	public String getZynr() {
		return zynr;
	}
	public void setZynr(String zynr) {
		this.zynr = zynr;
	}
	public String getZywz() {
		return zywz;
	}
	public void setZywz(String zywz) {
		this.zywz = zywz;
	}
	public String getZspb() {
		return zspb;
	}
	public void setZspb(String zspb) {
		this.zspb = zspb;
	}
	public String getYjpb() {
		return yjpb;
	}
	public void setYjpb(String yjpb) {
		this.yjpb = yjpb;
	}
	public String getYsxm() {
		return ysxm;
	}
	public void setYsxm(String ysxm) {
		this.ysxm = ysxm;
	}
	public String getZjje() {
		return zjje;
	}
	public void setZjje(String zjje) {
		this.zjje = zjje;
	}
	
	
}
