package com.jandar.alipay.core.struct;

/**
 * 就诊信息
 * Created by yubj on 2016/4/18.
 */
public class VisitInfo {
    private String userid;//用户ID
    private String brxm;//病人姓名
    private String ysxm;//医生姓名
    private String jzdz;//就诊地址
    private String jzsj;//就诊时间(HH:mm)
    private String jzrq;//就诊日期(yyyy-MM-dd)

    public String getUserid() {
        return userid;
    }

    public void setUserid(String userid) {
        this.userid = userid;
    }

    public String getBrxm() {
        return brxm;
    }

    public void setBrxm(String brxm) {
        this.brxm = brxm;
    }

    public String getYsxm() {
        return ysxm;
    }

    public void setYsxm(String ysxm) {
        this.ysxm = ysxm;
    }

    public String getJzdz() {
        return jzdz;
    }

    public void setJzdz(String jzdz) {
        this.jzdz = jzdz;
    }

    public String getJzsj() {
        return jzsj;
    }

    public void setJzsj(String jzsj) {
        this.jzsj = jzsj;
    }

	public String getJzrq() {
		return jzrq;
	}

	public void setJzrq(String jzrq) {
		this.jzrq = jzrq;
	}
    
}
