package com.tencent.protocol.ext;

/**
 * Created by zhufengxiang on 2016/09/08.
 *
 * 正常示例, errcode = 0, errmsg = ok,  msgid = 200228332
 */
public class MessageSendResData {
    private String errcode;
    private String errmsg;
    private String msgid;

    public MessageSendResData() {
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

    public String getMsgid() {
        return msgid;
    }

    public void setMsgid(String msgid) {
        this.msgid = msgid;
    }
}
