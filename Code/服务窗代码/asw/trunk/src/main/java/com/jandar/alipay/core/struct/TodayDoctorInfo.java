package com.jandar.alipay.core.struct;

/**
 * Created by liangyining on 16/3/17. 当天预约的医生信息
 */
public class TodayDoctorInfo {

    /**
     * pblsh : 293029
     * pbrq : 2016-03-17
     * ysbm : 1990010
     * ysxm : 施肖红
     * ysjb : 主任医师
     * ksbm : 159
     * ksmc : 特需呼吸内科
     * bcbm : 2
     * bcmc : 下午
     * ygrs : 4
     * yywg : 0
     * ghxe : 0
     * xebz : 0
     * ghf : 60
     * jzdd : 10幢4楼
     * txbz : 1
     */
    /**
     * | pblsh | 排队流水号 | 排队流水号          |
     | pbrq | 排班日期| 排版日期,基本是当天 |
     | ysjb | 医生级别 | 显示的是医生的职位级别 |
     | ysbm | 医生编码 | 医生编码          |
     | ysxm | 医生姓名 | 医生姓名          |
     | ksbm | 科室编码 | 科室编码|
     | ksmc | 科室名称 | 医生所在的科室名称 |
     | bcbm | 班次编码| 上午下午对应的编码 |
     | bcmc | 班次名称| 班次名称主要显示的是上午下午 |
     | ghf   | 挂号费   |                         |
     | jzdd  | 就诊地址  |                         |
     | txbz  | 特需标志  | 1：特需；0：普通 |
     | ygrs | 已挂人数 | 已经挂号就诊人数 限额时人数要求小于等于挂号限额人数|
     | yywg | 预约未挂 | 预约了但未挂号的人数|
     | ghxe | 挂号限额 | 限额时允许挂号人数 |
     | xebz | 限额标志 | 1是有限额 0是没有限额 |
     */
    private int pblsh;
    private String pbrq;
    private int ysbm;
    private String ysxm;
    private String ysjb;
    private int ksbm;
    private String ksmc;
    private int bcbm;
    private String bcmc;
    private int ygrs;
    private int yywg;
    private int ghxe;
    private int xebz;
    private int ghf;
    private String jzdd;
    private int txbz;

    public int getPblsh() {
        return pblsh;
    }

    public void setPblsh(int pblsh) {
        this.pblsh = pblsh;
    }

    public String getPbrq() {
        return pbrq;
    }

    public void setPbrq(String pbrq) {
        this.pbrq = pbrq;
    }

    public int getYsbm() {
        return ysbm;
    }

    public void setYsbm(int ysbm) {
        this.ysbm = ysbm;
    }

    public String getYsxm() {
        return ysxm;
    }

    public void setYsxm(String ysxm) {
        this.ysxm = ysxm;
    }

    public String getYsjb() {
        return ysjb;
    }

    public void setYsjb(String ysjb) {
        this.ysjb = ysjb;
    }

    public int getKsbm() {
        return ksbm;
    }

    public void setKsbm(int ksbm) {
        this.ksbm = ksbm;
    }

    public String getKsmc() {
        return ksmc;
    }

    public void setKsmc(String ksmc) {
        this.ksmc = ksmc;
    }

    public int getBcbm() {
        return bcbm;
    }

    public void setBcbm(int bcbm) {
        this.bcbm = bcbm;
    }

    public String getBcmc() {
        return bcmc;
    }

    public void setBcmc(String bcmc) {
        this.bcmc = bcmc;
    }

    public int getYgrs() {
        return ygrs;
    }

    public void setYgrs(int ygrs) {
        this.ygrs = ygrs;
    }

    public int getYywg() {
        return yywg;
    }

    public void setYywg(int yywg) {
        this.yywg = yywg;
    }

    public int getGhxe() {
        return ghxe;
    }

    public void setGhxe(int ghxe) {
        this.ghxe = ghxe;
    }

    public int getXebz() {
        return xebz;
    }

    public void setXebz(int xebz) {
        this.xebz = xebz;
    }

    public int getGhf() {
        return ghf;
    }

    public void setGhf(int ghf) {
        this.ghf = ghf;
    }

    public String getJzdd() {
        return jzdd;
    }

    public void setJzdd(String jzdd) {
        this.jzdd = jzdd;
    }

    public int getTxbz() {
        return txbz;
    }

    public void setTxbz(int txbz) {
        this.txbz = txbz;
    }
}
