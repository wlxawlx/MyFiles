package com.jandar.alipay.core.struct;

/**
 * 床位查询信息
 * Created by yubj on 2016/4/11.
 */
public class BedQueryInfo {
    private String bqmc;//     病区名称
    private String sycw;//    剩余床位

    public String getBqmc() {
        return bqmc;
    }

    public void setBqmc(String bqmc) {
        this.bqmc = bqmc;
    }

    public String getSycw() {
        return sycw;
    }

    public void setSycw(String sycw) {
        this.sycw = sycw;
    }
}
