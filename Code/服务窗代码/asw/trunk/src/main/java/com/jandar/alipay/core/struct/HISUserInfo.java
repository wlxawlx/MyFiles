package com.jandar.alipay.core.struct;

/**
 * Created by zzw on 16/2/23.
 * 用户信息
 */
public class HISUserInfo {
    private String name;//      | 姓名   | string | 是    | 用户真实姓名                   |
    private String phone;//     | 手机号码 | string | 是    | 用户手机号码                   |
    private String idcardno;//  | 身份证号 | string | 是    | 用户身份证号                   |
    private String address;//   | 地址   | string | 否    | 联系地址                     |
    private String openid;//    | 用户标识 | string | 是    | 支付宝USERID或微信OPENID       |
    private String headurl;//   | 用户头像 | string | 是    | 用户头像URL地址，直接指向支付平台用户头像地址 |
    private String cardno;//    | 就诊卡号 | string | 否    | 病人就诊卡卡号，可为空，当病人ID不明确时使用  |
    private String patientid;// | 病人ID | string | 是    | 病人ID，对应一张就诊卡             |
    private PlatformType usertype;//  | 用户类型 | string | 是    | 1：支付宝用户，2：微信用户           |
    private String bkzt;//卡绑定情况 | string | 是 | 判断用户是否绑卡 1绑卡,0未绑卡 |
    private String inpatentflag;// | 住院标志  | integer | 是  | 1：有住院信息；0：无住院信息 |
    private String userId; // 医院内部用户ID，可为空,放这儿是为了对三院的兼容

    public String getInpatentflag() {
        return inpatentflag;
    }

    public void setInpatentflag(String inpatentflag) {
        this.inpatentflag = inpatentflag;
    }

    public String getBkzt() {
        return bkzt;
    }

    public void setBkzt(String bkzt) {
        this.bkzt = bkzt;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getIdcardno() {
        return idcardno;
    }

    public void setIdcardno(String idcardno) {
        this.idcardno = idcardno;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getOpenid() {
        return openid;
    }

    public void setOpenid(String openid) {
        this.openid = openid;
    }

    public String getHeadurl() {
        return headurl;
    }

    public void setHeadurl(String headurl) {
        this.headurl = headurl;
    }

    public String getCardno() {
        return cardno;
    }

    public void setCardno(String cardno) {
        this.cardno = cardno;
    }

    public String getPatientid() {
        return patientid;
    }

    public void setPatientid(String patientid) {
        this.patientid = patientid;
    }

    public PlatformType getUsertype() {
        return usertype;
    }

    public void setUsertype(PlatformType usertype) {
        this.usertype = usertype;
    }

    /**
     * 医院内部用户ID，可为空,放这儿是为了对三院的兼容
     */
    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }
}
