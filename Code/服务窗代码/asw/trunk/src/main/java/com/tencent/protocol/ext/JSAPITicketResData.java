package com.tencent.protocol.ext;

/**
 * Created by zhufengxiang on 2016/09/08.
 *
 */
public class JSAPITicketResData {
    private int errcode;
    private String errmsg;
    private String ticket;
    private long expires_in;

    public JSAPITicketResData() {
    }

    public int getErrcode() {
        return errcode;
    }

    public void setErrcode(int errcode) {
        this.errcode = errcode;
    }

    public String getErrmsg() {
        return errmsg;
    }

    public void setErrmsg(String errmsg) {
        this.errmsg = errmsg;
    }

    public String getTicket() {
        return ticket;
    }

    public void setTicket(String ticket) {
        this.ticket = ticket;
    }

    public long getExpires_in() {
        return expires_in;
    }

    public void setExpires_in(long expires_in) {
        this.expires_in = expires_in;
    }
}
