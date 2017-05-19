package com.jandar.alipay.core.struct.wzsrmyy;

/*
 * 排队信息 （或预约报到信息）
 */
public class OutpatientWaitingList {
    private String brid;// | 病人ID ||
    private String yylsh;// | 预约流水号||
    private String brxm;// 病人姓名
    private String fzxh;// 分诊序号
    private String yysj;// 预约时间
    private String bdzt;// 报道状态
    private String yylb;// 预约类别
    private String pdlsh;// | 排队序号 | string | 是 | |
    private String pdhm;// | 排队号码 | string | 是 | |
    private String pdrq;// | 排队时间 | string | 是 | |
    private String bcmc;// | 班次名称 | string | 是 | |
    private String ksmc;// | 科室名称 | string | 是 | |
    private String ysxm;// | 医生姓名 | string | 是 | |
    private String zsmc;// | 诊室名称 | string | 是 | |
    private String zswz;// | 诊室位置 | string | 是 | |
    private String fjhm;// | 房间号码 | string | 是 | |
    private String dqjzh;//	| 当前就诊号码 | string
    private String pdzt;// | 排队状态 | string | 是 | |
    private String dlxm;// 队列序号
    private String dlmc;// 队列名称
    private String jcxm;// 检查名称
    private String zysx;// 注意事项

    public String getDqjzh() {
        return dqjzh;
    }

    public void setDqjzh(String dqjzh) {
        this.dqjzh = dqjzh;
    }

    public String getZysx() {
        return zysx;
    }

    public void setZysx(String zysx) {
        this.zysx = zysx;
    }

    public String getDlxm() {
        return dlxm;
    }

    public void setDlxm(String dlxm) {
        this.dlxm = dlxm;
    }

    public String getDlmc() {
        return dlmc;
    }

    public void setDlmc(String dlmc) {
        this.dlmc = dlmc;
    }

    public String getJcxm() {
        return jcxm;
    }

    public void setJcxm(String jcxm) {
        this.jcxm = jcxm;
    }

    public String getBrid() {
        return brid;
    }

    public void setBrid(String brid) {
        this.brid = brid;
    }

    public String getYylsh() {
        return yylsh;
    }

    public void setYylsh(String yylsh) {
        this.yylsh = yylsh;
    }

    public String getBrxm() {
        return brxm;
    }

    public void setBrxm(String brxm) {
        this.brxm = brxm;
    }

    public String getFzxh() {
        return fzxh;
    }

    public void setFzxh(String fzxh) {
        this.fzxh = fzxh;
    }

    public String getYysj() {
        return yysj;
    }

    public void setYysj(String yysj) {
        this.yysj = yysj;
    }

    public String getBdzt() {
        return bdzt;
    }

    public void setBdzt(String bdzt) {
        this.bdzt = bdzt;
    }

    public String getYylb() {
        return yylb;
    }

    public void setYylb(String yylb) {
        this.yylb = yylb;
    }

    public String getPdlsh() {
        return pdlsh;
    }

    public void setPdlsh(String pdlsh) {
        this.pdlsh = pdlsh;
    }

    public String getPdhm() {
        return pdhm;
    }

    public void setPdhm(String pdhm) {
        this.pdhm = pdhm;
    }

    public String getPdrq() {
        return pdrq;
    }

    public void setPdrq(String pdrq) {
        this.pdrq = pdrq;
    }

    public String getBcmc() {
        return bcmc;
    }

    public void setBcmc(String bcmc) {
        this.bcmc = bcmc;
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

    public String getZsmc() {
        return zsmc;
    }

    public void setZsmc(String zsmc) {
        this.zsmc = zsmc;
    }

    public String getZswz() {
        return zswz;
    }

    public void setZswz(String zswz) {
        this.zswz = zswz;
    }

    public String getFjhm() {
        return fjhm;
    }

    public void setFjhm(String fjhm) {
        this.fjhm = fjhm;
    }

    public String getPdzt() {
        return pdzt;
    }

    public void setPdzt(String pdzt) {
        this.pdzt = pdzt;
    }

}
