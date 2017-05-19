package com.jandar.alipay.core.struct;

/*
 * 门诊号源
 */
public class OutpatientOrderNumber {

    private String orderseq;// 号源流水号 string 是 预约号源流水号
    private String ordertime;// 预约时间 string 是 格式：HH:mm
    private String orderendtime;// | 预约结束时间 | string | 是 | 格式：HH:mm |
    private String shiftcode;// | 上下午标志 | string | 是 | 班次编码 0:上午，1：下午 |
    private String orderno;// 分诊序号 string 是 门诊第几个号

    public String getOrderseq() {
        return orderseq;
    }

    public void setOrderseq(String orderseq) {
        this.orderseq = orderseq;
    }

    public String getOrdertime() {
        return ordertime;
    }

    public void setOrdertime(String ordertime) {
        this.ordertime = ordertime;
    }

    public String getOrderendtime() {
        return orderendtime;
    }

    public void setOrderendtime(String orderendtime) {
        this.orderendtime = orderendtime;
    }

    public String getShiftcode() {
        return shiftcode;
    }

    public void setShiftcode(String shiftcode) {
        this.shiftcode = shiftcode;
    }

    public String getOrderno() {
        return orderno;
    }

    public void setOrderno(String orderno) {
        this.orderno = orderno;
    }
}
