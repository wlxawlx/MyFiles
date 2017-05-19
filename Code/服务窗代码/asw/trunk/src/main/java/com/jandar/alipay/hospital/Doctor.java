package com.jandar.alipay.hospital;

public class Doctor {
    private String doctorid;
    private String doctorname;
    private String level;
    private Integer reserve;


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

    public String getLevel() {
        return level;
    }

    public void setLevel(String level) {
        this.level = level;
    }

    public Integer getReserve() {
        return reserve;
    }

    public void setReserve(Integer reserve) {
        this.reserve = reserve;
    }

    public Doctor(String doctorid, String doctorname, String level, Integer reserve) {
        this.doctorid = doctorid;
        this.doctorname = doctorname;
        this.level = level;
        this.reserve = reserve;
    }
}
