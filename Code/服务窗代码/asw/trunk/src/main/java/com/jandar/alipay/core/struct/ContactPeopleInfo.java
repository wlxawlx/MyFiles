package com.jandar.alipay.core.struct;

/*
 * 联系人信息
 */
public class ContactPeopleInfo {
    private String linkmanid; // | 联系人ID | string | 是 | 联系人ID |
    private String label;// | 联系人标签 | string | 否 | 对联系人的描述 |
    private String name; // | 姓名 | string | 是 | 联系人姓名 |
    private String phone; // | 手机号码 | string | 是 | 联系人手机号码 |
    private String idcardno;// | 身份证号 | string | 是 | 联系人身份证号 |
    private String address; // | 地址 | string | 否 | 联系地址 |
    private String bindcardfalg; // | 绑卡标志 | integer | 是 | 0：未绑卡；1：已绑卡 |
    private String inpatentflag;// | 住院标志  | string  | 是    | 1：有住院信息；0：无住院信息 |
    private String patientid; // | 病人ID | string | 是 | 门诊卡对应病人ID |
    private String cardno;//绑定的卡号 没绑卡时为空

    public String getInpatentflag() {
        return inpatentflag;
    }

    public void setInpatentflag(String inpatentflag) {
        this.inpatentflag = inpatentflag;
    }

    public String getCardno() {
        return cardno;
    }

    public void setCardno(String cardno) {
        this.cardno = cardno;
    }

    public String getBindcardfalg() {
        return bindcardfalg;
    }

    public void setBindcardfalg(String bindcardfalg) {
        this.bindcardfalg = bindcardfalg;
    }

    public String getLinkmanid() {
        return linkmanid;
    }

    public void setLinkmanid(String linkmanid) {
        this.linkmanid = linkmanid;
    }

    public String getLabel() {
        return label;
    }

    public void setLabel(String label) {
        this.label = label;
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

    public String getPatientid() {
        return patientid;
    }

    public void setPatientid(String patientid) {
        this.patientid = patientid;
    }

}
