package com.jandar.cloud.hospital.im;

/**
 * Created by flyhorse on 2016/11/3.
 * 公共消息模型
 */
public class MsgModel {

    private SendType sendType=SendType.NOWAY;
    private String alipayUserId;
    private String wechatOpenId;
    private String message;

    public SendType getSendType() {
        return sendType;
    }

    public void setSendType(SendType sendType) {
        this.sendType = sendType;
    }

    public String getWechatOpenId() {
        return wechatOpenId;
    }

    public void setWechatOpenId(String wechatOpenId) {
        this.wechatOpenId = wechatOpenId;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getAlipayUserId() {
        return alipayUserId;
    }

    public void setAlipayUserId(String alipayUserId) {
        this.alipayUserId = alipayUserId;
    }





}
