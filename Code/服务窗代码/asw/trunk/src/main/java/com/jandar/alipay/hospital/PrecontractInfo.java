package com.jandar.alipay.hospital;

public class PrecontractInfo {
    private String orderid;
    private String doctorname;
    private String departname;
    private String patientname;
    private String orderno;
    private String ordertime;
    private String orderstate;
    private String ordertype;
    private String checkint;
    private String fee;
    private String phone;
    private String address;

    public String getOrderid() {
        return orderid;
    }

    public void setOrderid(String orderid) {
        this.orderid = orderid;
    }

    public String getDoctorname() {
        return doctorname;
    }

    public void setDoctorname(String doctorname) {
        this.doctorname = doctorname;
    }

    public String getDepartname() {
        return departname;
    }

    public void setDepartname(String departname) {
        this.departname = departname;
    }

    public String getPatientname() {
        return patientname;
    }

    public void setPatientname(String patientname) {
        this.patientname = patientname;
    }

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

    public String getOrderstate() {
        return orderstate;
    }

    public void setOrderstate(String orderstate) {
        this.orderstate = orderstate;
    }

    public String getOrdertype() {
        return ordertype;
    }

    public void setOrdertype(String ordertype) {
        this.ordertype = ordertype;
    }

    public String getCheckint() {
        return checkint;
    }

    public void setCheckint(String checkint) {
        this.checkint = checkint;
    }

    public String getFee() {
        return fee;
    }

    public void setFee(String fee) {
        this.fee = fee;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public PrecontractInfo(String orderid, String doctorname, String departname, String patientname, String orderno, String ordertime, String orderstate, String ordertype, String checkint, String fee, String phone, String address) {
        this.orderid = orderid;
        this.doctorname = doctorname;
        this.departname = departname;
        this.patientname = patientname;
        this.orderno = orderno;
        this.ordertime = ordertime;
        this.orderstate = orderstate;
        this.ordertype = ordertype;
        this.checkint = checkint;
        this.fee = fee;
        this.phone = phone;
        this.address = address;
    }
}
