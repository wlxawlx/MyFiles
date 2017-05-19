package com.jandar.bean;

/**
 * 为 alipay 的 article 和 wxarticle 进行统一
 * Created by zzw on 16/9/10.
 */
public class ArticleBean {

    /**
     * 链接文字，微信不需要这个值
     */
    private String actionName;

    /**
     * 图文消息内容
     */
    private String desc;

    /**
     * 图片链接，对于多条图文消息的第一条消息，该字段不能为空
     */
    private String imageUrl;

    /**
     * 图文消息标题
     */
    private String title;

    /**
     * 点击图文消息跳转的链接
     */
    private String url;

    public String getActionName() {
        return this.actionName;
    }
    public void setActionName(String actionName) {
        this.actionName = actionName;
    }

    public String getDesc() {
        return this.desc;
    }
    public void setDesc(String desc) {
        this.desc = desc;
    }

    public String getImageUrl() {
        return this.imageUrl;
    }
    public void setImageUrl(String imageUrl) {
        this.imageUrl = imageUrl;
    }

    public String getTitle() {
        return this.title;
    }
    public void setTitle(String title) {
        this.title = title;
    }

    public String getUrl() {
        return this.url;
    }
    public void setUrl(String url) {
        this.url = url;
    }
}
