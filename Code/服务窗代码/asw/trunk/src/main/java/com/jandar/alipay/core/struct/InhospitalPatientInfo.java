package com.jandar.alipay.core.struct;

import jodd.util.StringUtil;

/*
 * 住院病人信息
 */
public class InhospitalPatientInfo {

    private String inpatientno;//	住院号码	string	是	病人住院号码，病人住院时的唯一标识
    private String zyh;//住院号，医院内码
    private String mzhm;// | 门诊号码 |   |
    private String patientname;//病人姓名	string	是
    private String patientidcardno;//	病人身份证号	string	是
    private String sex;    //病人性别	string	是	1：男；2：女
    private String birthday;//	出生年月	string	否	格式：yyyy-MM-dd
    private String address;//	联系地址	string	否	入院时便当的联系地址
    private String phone;    //联系电话	string	是
    private String admitdate;//	入院日期	string	是	格式：yyyy-MM-dd
    private String dischargedate;    //出院日期	string	否	格式：yyyy-MM-dd
    private Integer stayday;//	住院天数	integer	否
    private String xzmc;    //病人性质
    private String endemicarea;//	病区名称	string	是	所住病区
    private String brch;    //病人床号
    private String departcode;//	科室代码	string	否
    private String departname;//	科室名称	string	是
    private String ylhj;//	医疗合计
    private String lwhj;//劳务合计
    private String zfje;//	自负金额
    private String jkje;//	缴款金额
    private String zyzt;//	住院状态
    private String doctorcode;//	医生代码	string	否
    private String doctorname;//	医生姓名	string	是

    public String getMzhm() {
        return mzhm;
    }

    public void setMzhm(String mzhm) {
        this.mzhm = mzhm;
    }

    public String getZyh() {
        return zyh;
    }

    public void setZyh(String zyh) {
        this.zyh = zyh;
    }

    public String getInpatientno() {
        return inpatientno;
    }

    public void setInpatientno(String inpatientno) {
        this.inpatientno = inpatientno;
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

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public String getAdmitdate() {
        return admitdate;
    }

    public void setAdmitdate(String admitdate) {
        this.admitdate = admitdate;
    }

    public String getDischargedate() {
        return dischargedate;
    }

    public void setDischargedate(String dischargedate) {
        this.dischargedate = dischargedate;
    }

    public Integer getStayday() {
        return stayday;
    }

    public void setStayday(Integer stayday) {
        this.stayday = stayday;
    }

    public String getXzmc() {
        return xzmc;
    }

    public void setXzmc(String xzmc) {
        this.xzmc = xzmc;
    }

    public String getEndemicarea() {
        return endemicarea;
    }

    public void setEndemicarea(String endemicarea) {
        this.endemicarea = endemicarea;
    }

    public String getBrch() {
        return brch;
    }

    public void setBrch(String brch) {
        this.brch = brch;
    }

    public String getDepartcode() {
        return departcode;
    }

    public void setDepartcode(String departcode) {
        this.departcode = departcode;
    }

    public String getDepartname() {
        return departname;
    }

    public void setDepartname(String departname) {
        this.departname = departname;
    }

    public String getYlhj() {
        return ylhj;
    }

    public void setYlhj(String ylhj) {
        this.ylhj = ylhj;
    }

    public String getLwhj() {
        return lwhj;
    }

    public void setLwhj(String lwhj) {
        if (StringUtil.isBlank(lwhj)) {
            this.lwhj = "0";
        } else {
            this.lwhj = lwhj;
        }
    }

    public String getZfje() {
        return zfje;
    }

    public void setZfje(String zfje) {
        this.zfje = zfje;
    }

    public String getJkje() {
        return jkje;
    }

    public void setJkje(String jkje) {
        this.jkje = jkje;
    }

    public String getZyzt() {
        return zyzt;
    }

    public void setZyzt(String zyzt) {
        this.zyzt = zyzt;
    }

    public String getDoctorcode() {
        return doctorcode;
    }

    public void setDoctorcode(String doctorcode) {
        this.doctorcode = doctorcode;
    }

    public String getDoctorname() {
        return doctorname;
    }

    public void setDoctorname(String doctorname) {
        this.doctorname = doctorname;
    }

}
