package com.jandar.alipay.core.struct.wzsrmyy;

/**
 * Created by zzw on 16/2/26.
 * 课程信息
 */
public class CourseInfo {
    private String classifyCode;
    private String courseCode;
    private String coursename;
    /**
     * 课程费用,单位元
     */
    private String coursefee;
    /**
     * 课程注意事项
     */
    private String courseremark;

    public String getClassifyCode() {
        return classifyCode;
    }

    public void setClassifyCode(String classifyCode) {
        this.classifyCode = classifyCode;
    }

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

    public String getCoursefee() {
        return coursefee;
    }

    public void setCoursefee(String coursefee) {
        this.coursefee = coursefee;
    }

    public String getCourseremark() {
        return courseremark;
    }

    public void setCourseremark(String courseremark) {
        this.courseremark = courseremark;
    }
}
