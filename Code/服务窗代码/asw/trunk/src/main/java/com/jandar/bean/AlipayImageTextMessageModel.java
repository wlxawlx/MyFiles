package com.jandar.bean;

import com.alipay.api.AlipayObject;
import com.alipay.api.domain.Article;
import com.alipay.api.internal.mapping.ApiField;

import java.util.List;

/**
 * 用来代替 AlipayOpenPublicMessageCustomSendModel
 * Created by zzw on 16/9/10.
 */
public class AlipayImageTextMessageModel extends AlipayObject {

    /**
     * 图文消息，当msg_type为text时，必须存在相对应的值
     */
    @ApiField("articles")
    private List<Article> articles;

    /**
     * 消息类型，text：文本消息，image-text：图文消息
     */
    @ApiField("msg_type")
    private String msgType;

    /**
     * 消息接收用户的userid
     */
    @ApiField("to_user_id")
    private String toUserId;

    public List<Article> getArticles() {
        return this.articles;
    }
    public void setArticles(List<Article> articles) {
        this.articles = articles;
    }

    public String getMsgType() {
        return this.msgType;
    }
    public void setMsgType(String msgType) {
        this.msgType = msgType;
    }

    public String getToUserId() {
        return this.toUserId;
    }
    public void setToUserId(String toUserId) {
        this.toUserId = toUserId;
    }
}
