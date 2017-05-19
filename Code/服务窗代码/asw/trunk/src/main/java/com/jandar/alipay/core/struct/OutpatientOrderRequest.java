package com.jandar.alipay.core.struct;

/*
 * 门诊预约请求信息&当日门诊预约请求
 */
public class OutpatientOrderRequest {

    private String openid;// 用户标识 string 是 支付宝USERID或微信OPENID
    private String doctorcode;// 医生代码 string 否 辅助参数，用于表示是哪个医生的排班，但并不一定参于查询
    private String doctorName;
    private String scheduleseq;// 排班流水号 string 否 辅助参数
    private String orderseq;// 号源流水号 string 是 预约号源流水号 !!!在当日门诊预约请求中是pblsh
    private String ordertime;// | 预约时间 | string | 是 | 格式：HH:mm |
    private String orderendtime;// | 预约结束时间 | string | 是 | 格式：HH:mm |
    private String shiftcode;// | 预约上下午代码 | string | 是 | 班次编码 S:上午，X：下午 |
    private String patientid;// | 病人ID | string | 否 | 给哪个病人预约的 |
    private String cardno;   //就诊卡号
    private String patientname;// 病人姓名 string 是 给哪个病人预约的
    private String patientidcardno;// 病人身份证号 string 是 病人身份证号
    private String patientphone;// 病人电话 string 是 病人联系电话
    private String patientaddress;// 病人地址 string 是 病人联系地址
    private String orderno;//分诊序号
    private Integer sourcetype;  //号源类型,1：云医院预约 , 云医院预约必须传

    private String visittype;    //复诊类型    FZKY:复诊开药，JCYY:检查预约，ZYSQ:住院申请，WZZX:问诊咨询


    public String getOrderno() {
        return orderno;
    }

    public void setOrderno(String orderno) {
        this.orderno = orderno;
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

    public String getOpenid() {
        return openid;
    }

    public void setOpenid(String openid) {
        this.openid = openid;
    }

    public String getDoctorcode() {
        return doctorcode;
    }

    public void setDoctorcode(String doctorcode) {
        this.doctorcode = doctorcode;
    }

    public String getScheduleseq() {
        return scheduleseq;
    }

    public void setScheduleseq(String scheduleseq) {
        this.scheduleseq = scheduleseq;
    }

    public String getOrderseq() {
        return orderseq;
    }

    public void setOrderseq(String orderseq) {
        this.orderseq = orderseq;
    }

    public String getPatientname() {
        return patientname;
    }

    public void setPatientname(String patientname) {
        this.patientname = patientname;
    }

    public String getPatientidcardno() {
        return patientidcardno;
    }

    public void setPatientidcardno(String patientidcardno) {
        this.patientidcardno = patientidcardno;
    }

    public String getPatientphone() {
        return patientphone;
    }

    public void setPatientphone(String patientphone) {
        this.patientphone = patientphone;
    }

    public String getPatientaddress() {
        return patientaddress;
    }

    public void setPatientaddress(String patientaddress) {
        this.patientaddress = patientaddress;
    }

    public String getPatientid() {
        return patientid;
    }

    public void setPatientid(String patientid) {
        this.patientid = patientid;
    }

    public String getCardno() {
        return cardno;
    }

    public void setCardno(String cardno) {
        this.cardno = cardno;
    }

    public String getDoctorName() {
        return doctorName;
    }

    public void setDoctorName(String doctorName) {
        this.doctorName = doctorName;
    }

    public Integer getSourcetype() {
        return sourcetype;
    }

    public void setSourcetype(Integer sourcetype) {
        this.sourcetype = sourcetype;
    }

    public String getVisittype() {
        return visittype;
    }

    public void setVisittype(String visittype) {
        this.visittype = visittype;
    }
}
