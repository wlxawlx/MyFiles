package com.jandar.alipay.core.struct;
/*
 * 
 * 服药信息列表
 */
public class MedicineUseInfoList {
	private String fphm;   // |发票号码 | string | 是    |  |
	private String brxm;   // | 病人姓名 | string | 是    |     |
	private String cflsh ;   //  | 处方流水号  | string | 是    |                |
	private String ksbm ;   //| 科室编码  | string | 是    |      |
	private String ksmc ;//  | 科室名称  | string | 是    |          |
	private String ysbm;// | 医生编码  | string | 是    |         |
	private String ysxm;  // | 医生姓名  | string | 是    |   |
	private String brid ;  //    | 病人ID  | string | 是    |        |
	private String kfrq;   //  | 开方日期  | string | 是    |            |
	private String sfrq;   //  | 收费日期  | string | 是    |             |
	private String zjje;   //  | 总计金额  | string | 是    |             |
	private String cflx;   //  | 处方类型  | string | 是    |            |
	private String lxmc;   //  | 处方类型名称  | string | 是    |             |

	/**
	 * 以下为协议扩展
	 */
	private String cfje;   //  | 处方金额  | string | 是    |             |
	private String tzybz;   //  | 特种药标志  | string | 是    | 处方中是否包含特种药,是为Yes，否为No            |
	private String zfzt;  //  | 缴费状态  | string | 是    |Success：缴费成功;Failure：缴费失败;None：未缴费
	public MedicineUseInfoList() {
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

	public String getCflsh() {
		return cflsh;
	}

	public void setCflsh(String cflsh) {
		this.cflsh = cflsh;
	}

	public String getKsbm() {
		return ksbm;
	}

	public void setKsbm(String ksbm) {
		this.ksbm = ksbm;
	}

	public String getKsmc() {
		return ksmc;
	}

	public void setKsmc(String ksmc) {
		this.ksmc = ksmc;
	}

	public String getYsbm() {
		return ysbm;
	}

	public void setYsbm(String ysbm) {
		this.ysbm = ysbm;
	}

	public String getYsxm() {
		return ysxm;
	}

	public void setYsxm(String ysxm) {
		this.ysxm = ysxm;
	}

	public String getBrid() {
		return brid;
	}

	public void setBrid(String brid) {
		this.brid = brid;
	}

	public String getKfrq() {
		return kfrq;
	}

	public void setKfrq(String kfrq) {
		this.kfrq = kfrq;
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

	public String getCflx() {
		return cflx;
	}

	public void setCflx(String cflx) {
		this.cflx = cflx;
	}

	public String getLxmc() {
		return lxmc;
	}

	public void setLxmc(String lxmc) {
		this.lxmc = lxmc;
	}


	/**
	 * 以下为协议扩展
	 */
	public String getCfje() {
		return cfje;
	}

	public void setCfje(String cfje) {
		this.cfje = cfje;
	}

	public String getTzybz() {
		return tzybz;
	}

	public void setTzybz(String tzybz) {
		this.tzybz = tzybz;
	}

	public String getZfzt() {
		return zfzt;
	}

	public void setZfzt(String zfzt) {
		this.zfzt = zfzt;
	}
}
