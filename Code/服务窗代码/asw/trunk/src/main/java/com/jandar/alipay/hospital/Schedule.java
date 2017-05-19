package com.jandar.alipay.hospital;

public class Schedule {
    private String scheduleseq;
    private String departid;
    private String departname;
    private String doctorid;
    private String doctorname;
    private String special;
    private String remain;
    private String total;
    private String address;
    private String scheduledate;
    private String shiftcode;
    private String shiftname;
    private String fee;

    public String getScheduleseq() {
        return scheduleseq;
    }

    public void setScheduleseq(String scheduleseq) {
        this.scheduleseq = scheduleseq;
    }

    public String getDepartid() {
        return departid;
    }

    public void setDepartid(String departid) {
        this.departid = departid;
    }

    public String getDepartname() {
        return departname;
    }

    public void setDepartname(String departname) {
        this.departname = departname;
    }

    public String getDoctorid() {
        return doctorid;
    }

    public void setDoctorid(String doctorid) {
        this.doctorid = doctorid;
    }

    public String getDoctorname() {
        return doctorname;
    }

    public void setDoctorname(String doctorname) {
        this.doctorname = doctorname;
    }

    public String getSpecial() {
        return special;
    }

    public void setSpecial(String special) {
        this.special = special;
    }

    public String getRemain() {
        return remain;
    }

    public void setRemain(String remain) {
        this.remain = remain;
    }

    public String getTotal() {
        return total;
    }

    public void setTotal(String total) {
        this.total = total;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getScheduledate() {
        return scheduledate;
    }

    public void setScheduledate(String scheduledate) {
        this.scheduledate = scheduledate;
    }

    public String getShiftcode() {
        return shiftcode;
    }

    public void setShiftcode(String shiftcode) {
        this.shiftcode = shiftcode;
    }

    public String getShiftname() {
        return shiftname;
    }

    public void setShiftname(String shiftname) {
        this.shiftname = shiftname;
    }

    public String getFee() {
        return fee;
    }

    public void setFee(String fee) {
        this.fee = fee;
    }

    public Schedule(String scheduleseq, String departid, String departname, String doctorid, String doctorname, String special, String remain, String total, String address, String scheduledate, String shiftcode, String shiftname, String fee) {
        this.scheduleseq = scheduleseq;
        this.departid = departid;
        this.departname = departname;
        this.doctorid = doctorid;
        this.doctorname = doctorname;
        this.special = special;
        this.remain = remain;
        this.total = total;
        this.address = address;
        this.scheduledate = scheduledate;
        this.shiftcode = shiftcode;
        this.shiftname = shiftname;
        this.fee = fee;
    }
}
