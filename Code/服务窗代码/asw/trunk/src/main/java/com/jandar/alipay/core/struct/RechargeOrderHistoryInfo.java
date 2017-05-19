package com.jandar.alipay.core.struct;

/*
 * 医院端返回的用户充值订单信息
 */
public class RechargeOrderHistoryInfo {

    private String openid;// 支付宝用户ID string 是 支付宝USERID或微信OPENID
    private String paymenttradeno;// 支付平台订单号 string 是 支付平台订单号
    private String tradeno;// 订单号 string 是 医院订单号
    private String status;// 状态 string 是 0：订单已创建；1：订单用户完成；2：商户通知完成；3：订单取消
    private String subject;// 标题 string 是 订单名称
    private String money;// 金额 string 是 单位元
    private String ctime;// 创建时间 string 是 格式：yyyy-MM-dd HH:mm:ss
    private String paytime;// 付款时间 string 是 格式：yyyy-MM-dd HH:mm:ss
    private String rtntime;// 退款时间 string 否 格式：yyyy-MM-dd HH:mm:ss
    //    private String patientid;// 病人ID string 是 为哪个病人充值时对应的病人ID
//    private String inpatientno;// 住院号码 string 是 为哪个病人充值时对应的病人住院号码
    private String userdata; // 门诊订单时是病人ID,住院订单是住院号码
    private String patientname;// 病人姓名 string 是 为哪个病人充值
    private String patientidcardno;// 病人身份证号 string 是

    public String getOpenid() {
        return openid;
    }

    public void setOpenid(String openid) {
        this.openid = openid;
    }

    public String getPaymenttradeno() {
        return paymenttradeno;
    }

    public void setPaymenttradeno(String paymenttradeno) {
        this.paymenttradeno = paymenttradeno;
    }

    public String getTradeno() {
        return tradeno;
    }

    public void setTradeno(String tradeno) {
        this.tradeno = tradeno;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getSubject() {
        return subject;
    }

    public void setSubject(String subject) {
        this.subject = subject;
    }

    public String getMoney() {
        return money;
    }

    public void setMoney(String money) {
        this.money = money;
    }

    public String getCtime() {
        return ctime;
    }

    public void setCtime(String ctime) {
        this.ctime = ctime;
    }

    public String getPaytime() {
        return paytime;
    }

    public void setPaytime(String paytime) {
        this.paytime = paytime;
    }

    public String getRtntime() {
        return rtntime;
    }

    public void setRtntime(String rtntime) {
        this.rtntime = rtntime;
    }

    public String getUserdata() {
        return userdata;
    }

    public void setUserdata(String userdata) {
        this.userdata = userdata;
    }

    public String getInpatientno() {
        return getUserdata();
    }

    public void setInpatientno(String inpatientno) {
        setUserdata(inpatientno);
    }

    public String getPatientid() {
        return getUserdata();
    }

    public void setPatientid(String patientid) {
        setUserdata(patientid);
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
}
