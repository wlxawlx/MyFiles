package com.jandar.alipay.core.struct;

/**
 * 住院费用
 */
public class InHospitalOutlays {
    private String costcode;    // 费用项目 string 否 费用的分类
    private String costname;    // 项目名称 string 是 费用分类的名称
    private String totalfee;    // 总计金额 double 是 单位：元
    private String payfee;    // 自负金额 double 是 单位：元

   private String  fyxh ;//| 费用序号 | string | 是    |               |
   private String fyxm ;// | 费用项目 | string | 是    |               |
   private String fymc ;// | 费用名称 | string | 是    |               |
   private String fygg ;// | 费用规格 | string | 是    |               |
   private String fydw ;// | 费用单位 | string | 是    |               |
   private String ybdm  ;//| 医保代码 | string | 是    |               |
   private String zfbl ;// | 自负比例 | string | 是    |               |
   private String yplx  ;//| 费用类型 | string | 是    | 1药品，0，费用，-1劳务 |
   private String fysl  ;//| 费用数量 | string | 是    |               |
   private String fydj  ;//| 费用单价 | string | 是    |               |
   private String zjje  ;//| 总计金额 | string | 是    |               |
   private String zfje  ;//| 自负金额 | string | 是    |               |
    
    public String getCostcode() {
	return costcode;
}

public void setCostcode(String costcode) {
	this.costcode = costcode;
}

public String getCostname() {
	return costname;
}

public void setCostname(String costname) {
	this.costname = costname;
}

public String getTotalfee() {
	return totalfee;
}

public void setTotalfee(String totalfee) {
	this.totalfee = totalfee;
}

public String getPayfee() {
	return payfee;
}

public void setPayfee(String payfee) {
	this.payfee = payfee;
}

public String getFyxh() {
	return fyxh;
}

public void setFyxh(String fyxh) {
	this.fyxh = fyxh;
}

public String getFyxm() {
	return fyxm;
}

public void setFyxm(String fyxm) {
	this.fyxm = fyxm;
}

public String getFymc() {
	return fymc;
}

public void setFymc(String fymc) {
	this.fymc = fymc;
}

public String getFygg() {
	return fygg;
}

public void setFygg(String fygg) {
	this.fygg = fygg;
}

public String getFydw() {
	return fydw;
}

public void setFydw(String fydw) {
	this.fydw = fydw;
}

public String getYbdm() {
	return ybdm;
}

public void setYbdm(String ybdm) {
	this.ybdm = ybdm;
}

public String getZfbl() {
	return zfbl;
}

public void setZfbl(String zfbl) {
	this.zfbl = zfbl;
}

public String getYplx() {
	return yplx;
}

public void setYplx(String yplx) {
	this.yplx = yplx;
}

public String getFysl() {
	return fysl;
}

public void setFysl(String fysl) {
	this.fysl = fysl;
}

public String getFydj() {
	return fydj;
}

public void setFydj(String fydj) {
	this.fydj = fydj;
}

public String getZjje() {
	return zjje;
}

public void setZjje(String zjje) {
	this.zjje = zjje;
}

public String getZfje() {
	return zfje;
}

public void setZfje(String zfje) {
	this.zfje = zfje;
}

	public String getCostCode() {
        return costcode;
    }

    public void setCostCode(String fyxm) {
        this.costcode = fyxm;
    }

    public String getCostName() {
        return costname;
    }

    public void setCostName(String xmmc) {
        this.costname = xmmc;
    }

    public String getTotalFee() {
        return totalfee;
    }

    public void setTotalFee(String zjje) {
        this.totalfee = zjje;
    }

    public String getPayFee() {
        return payfee;
    }

    public void setPayFee(String zfje) {
        this.payfee = zfje;
    }
}
