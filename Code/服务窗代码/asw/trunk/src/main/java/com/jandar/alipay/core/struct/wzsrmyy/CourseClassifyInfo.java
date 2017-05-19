package com.jandar.alipay.core.struct.wzsrmyy;

/**
 * Created by zzw on 16/2/26.
 * 课程分类信息
 */
public class CourseClassifyInfo {

    private String classifyCode;

    private String classifyName;

    /**
     * 报名说明
     */
    private String explain;

    /**
     * 报名事项
     */
    private String prompt;

    public String getClassifyCode() {
        return classifyCode;
    }

    public void setClassifyCode(String classifyCode) {
        this.classifyCode = classifyCode;
    }

    public String getClassifyName() {
        return classifyName;
    }

    public void setClassifyName(String classifyName) {
        this.classifyName = classifyName;
    }

    public String getExplain() {
        return explain;
    }

    public void setExplain(String explain) {
        this.explain = explain;
    }

    public String getPrompt() {
        return prompt;
    }

    public void setPrompt(String prompt) {
        this.prompt = prompt;
    }
}
