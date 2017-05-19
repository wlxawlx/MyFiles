package com.jandar.alipay.core.struct;


/*
 * 用户进行医院充值的订单完成信息
 */
public class RechargeOrderFinishInfo {
    private String openid;// 用户标识 string 是 支付宝USERID或微信OPENID
    private String patientname;// 病人姓名 string 否 辅助参数，为哪个病人充值
    //    private String patientid;// 病人ID string 是 为哪个病人充值时对应的病人ID
//    private String inpatientno;// 住院号码 string 是 为哪个病人充值时对应的病人住院号码
    private String userdata; // 门诊订单时是病人ID,住院订单是住院号码
    private String tradeno;// 订单号 string 是 医院订单号
    private String paymenttradeno;// 支付平台订单号 string 是 支付平台订单号
    private String money;// 金额 string 是 单位元
    private String paymentparameters;// 通知参数 string 是 商户通知返回的参数，帮助反查

    public String getOpenid() {
        return openid;
    }

    public void setOpenid(String openid) {
        this.openid = openid;
    }

    public String getPatientname() {
        return patientname;
    }

    public void setPatientname(String patientname) {
        this.patientname = patientname;
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

    public String getTradeno() {
        return tradeno;
    }

    public void setTradeno(String tradeno) {
        this.tradeno = tradeno;
    }

    public String getPaymenttradeno() {
        return paymenttradeno;
    }

    public void setPaymenttradeno(String paymenttradeno) {
        this.paymenttradeno = paymenttradeno;
    }

    public String getMoney() {
        return money;
    }

    public void setMoney(String money) {
        this.money = money;
    }

    public String getPaymentparameters() {
        return paymentparameters;
    }

    public void setPaymentparameters(String paymentparameters) {
        this.paymentparameters = paymentparameters;
    }

}
