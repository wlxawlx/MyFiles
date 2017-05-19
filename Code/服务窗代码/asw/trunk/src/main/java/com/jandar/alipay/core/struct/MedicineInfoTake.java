package com.jandar.alipay.core.struct;
/*
 * 
 * 取药信息
 */
public class MedicineInfoTake {
	private String fphm;   // |发票号码 | string | 是    | 发票的号码 |
	private String brxm;   // | 病人姓名 | string | 是    | 诊卡对应病人姓名     |
	private String brid ;   //  | 病人ID  | string | 是    | 诊卡对应病人ID               |
	private String sfrq ;   //| 收费日期  | string | 是    | 收费的日期         |
	private String zjje ;//  | 总计金额  | string | 是    |            |
	private String lxmc;// | 处方类型  | string | 是    |            |
	private String yfmc;  // | 药房名称  | string | 是    |   |
	private String qywz ;  // | 药房位置  | string | 是    |           |
	private String fyck;   //  | 发药窗口  | string | 是    |            |
	
	public MedicineInfoTake() {
		super();
	}
	public String getFphm() {
		return fphm;
	}
	public void setFphm(String fphm) {
		this.fphm = fphm;
	}
	public String getBrxm() {
		return brxm;
	}
	public void setBrxm(String brxm) {
		this.brxm = brxm;
	}
	public String getBrid() {
		return brid;
	}
	public void setBrid(String brid) {
		this.brid = brid;
	}
	public String getSfrq() {
		return sfrq;
	}
	public void setSfrq(String sfrq) {
		this.sfrq = sfrq;
	}
	public String getZjje() {
		return zjje;
	}
	public void setZjje(String zjje) {
		this.zjje = zjje;
	}
	public String getLxmc() {
		return lxmc;
	}
	public void setLxmc(String lxmc) {
		this.lxmc = lxmc;
	}
	public String getYfmc() {
		return yfmc;
	}
	public void setYfmc(String yfmc) {
		this.yfmc = yfmc;
	}
	public String getQywz() {
		return qywz;
	}
	public void setQywz(String qywz) {
		this.qywz = qywz;
	}
	public String getFyck() {
		return fyck;
	}
	public void setFyck(String fyck) {
		this.fyck = fyck;
	}
}
