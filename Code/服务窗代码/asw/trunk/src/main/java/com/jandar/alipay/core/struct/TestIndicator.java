package com.jandar.alipay.core.struct;

/*
 * 化验单指标
 */
public class TestIndicator {
    public TestIndicator(String jylx, String xmmc, String result, String hint, String ckfw, String xmdw) {
        super();
        this.jylx = jylx;
        this.xmmc = xmmc;
        this.result = result;
        this.hint = hint;
        this.ckfw = ckfw;
        this.xmdw = xmdw;
    }

    public TestIndicator() {
        super();
        // TODO Auto-generated constructor stub
    }

    private String jylx;// 检验类型 string 否
    private String xmmc;// 项目名称 string 是
    private String result;// 结果 string 是
    private String hint;// 标志 string 否 ↑ 或 另一个下的符号
    private String ckfw;// 健康范围 string 是
    private String xmdw;// 项目单位 string 是 %等

    public String getJylx() {
        return jylx;
    }

    public void setJylx(String jylx) {
        this.jylx = jylx;
    }

    public String getXmmc() {
        return xmmc;
    }

    public void setXmmc(String xmmc) {
        this.xmmc = xmmc;
    }

    public String getResult() {
        return result;
    }

    public void setResult(String result) {
        this.result = result;
    }

    public String getHint() {
        return hint;
    }

    public void setHint(String hint) {
        this.hint = hint;
    }

    public String getCkfw() {
        return ckfw;
    }

    public void setCkfw(String ckfw) {
        this.ckfw = ckfw;
    }

    public String getXmdw() {
        return xmdw;
    }

    public void setXmdw(String xmdw) {
        this.xmdw = xmdw;
    }
}
