package com.jandar.alipay.hospital;

import java.util.List;

public class Section {
    private String departid;
    private String departname;
    private List<Section> list;
    private String describe;

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

    public List<Section> getList() {
        return list;
    }

    public void setList(List<Section> list) {
        this.list = list;
    }

    public String getDescribe() {
        return describe;
    }

    public void setDescribe(String describe) {
        this.describe = describe;
    }

    public Section(String departid, String departname, List<Section> list) {
        this.departid = departid;
        this.departname = departname;
        this.list = list;
    }
}
