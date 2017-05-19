package com.jandar.alipay.core.struct.wzsrmyy;

import com.jandar.alipay.core.struct.RechargeOrderHistoryInfo;

/**
 * Created by zzw on 16/2/26.
 * 课程报告记录信息
 */
public class CourseOrderHistoryInfo extends RechargeOrderHistoryInfo {
    private String courseCode;
    private String coursename;
    private String people;
    private String price;
    private String userRemark;

    public String getCourseCode() {
        return courseCode;
    }

    public void setCourseCode(String courseCode) {
        this.courseCode = courseCode;
    }

    public String getCoursename() {
        return coursename;
    }

    public void setCoursename(String coursename) {
        this.coursename = coursename;
    }

    public String getPeople() {
        return people;
    }

    public void setPeople(String people) {
        this.people = people;
    }

    public String getPrice() {
        return price;
    }

    public void setPrice(String price) {
        this.price = price;
    }

    public String getUserRemark() {
        return userRemark;
    }

    public void setUserRemark(String userRemark) {
        this.userRemark = userRemark;
    }
}
