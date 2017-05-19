package com.tencent.protocol.ext;

/**
 * Created by zhufengxiang on 2016/09/08.
 * 获取接口调用凭证返回数据
 */
public class AccessTokenResData {
    private String errcode;
    private String errmsg;

    private String access_token;
    private long expires_in;


    public AccessTokenResData() {
    }

    public String getErrcode() {
        return errcode;
    }

    public void setErrcode(String errcode) {
        this.errcode = errcode;
    }

    public String getErrmsg() {
        return errmsg;
    }

    public void setErrmsg(String errmsg) {
        this.errmsg = errmsg;
    }

    public String getAccess_token() {
        return access_token;
    }

    public void setAccess_token(String access_token) {
        this.access_token = access_token;
    }

    public long getExpires_in() {
        return expires_in;
    }

    public void setExpires_in(long expires_in) {
        this.expires_in = expires_in;
    }
}
