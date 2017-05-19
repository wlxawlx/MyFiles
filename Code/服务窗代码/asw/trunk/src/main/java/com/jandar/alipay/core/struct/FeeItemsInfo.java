package com.jandar.alipay.core.struct;

/**
 * 收费项目信息
 * Created by yubj on 2016/4/11.
 */
public class FeeItemsInfo {
    private String fylx;//   费用类型
    private String fymc;//  费用名称
    private String fydw;//   计量单位
    private String fyjg;//  收费价格

    public String getFylx() {
        return fylx;
    }

    public void setFylx(String fylx) {
        this.fylx = fylx;
    }

    public String getFymc() {
        return fymc;
    }

    public void setFymc(String fymc) {
        this.fymc = fymc;
    }

    public String getFydw() {
        return fydw;
    }

    public void setFydw(String fydw) {
        this.fydw = fydw;
    }

    public String getFyjg() {
        return fyjg;
    }

    public void setFyjg(String fyjg) {
        this.fyjg = fyjg;
    }
}
