package com.jandar.bean;

/**
 * 同步返回消息,用于支持当收到用户的消息后的回复内容
 * Created by zzw on 16/8/29.
 */
public class OutMessage {

    /**
     * 消息类型
     */
    private String msgType;

    /**
     * 文本消息内容
     */
    private String content;

    /**
     * 发送给谁的
     * 支付宝为：userid
     * 微信为：username
     */
    private String toUserId;

    public String getMsgType() {
        return msgType;
    }

    public void setMsgType(String msgType) {
        this.msgType = msgType;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public String getToUserId() {
        return toUserId;
    }

    public void setToUserId(String toUserId) {
        this.toUserId = toUserId;
    }
}
