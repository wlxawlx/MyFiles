package com.tencent.protocol.ext;

/**
 * Created by zhufengxiang on 2016/09/08.
 * 发送模板消息请求参数
 */
public class MessageSendReqData {
    private String touser;        // 是(必填)      接受者openid
    private String templateId;    // 是            模板ID
    private String url;           // 否            模板跳转链接
    private String data;          // 是            模板数据

    public MessageSendReqData() {
    }

    public String getTouser() {
        return touser;
    }

    public void setTouser(String touser) {
        this.touser = touser;
    }

    public String getTemplateId() {
        return templateId;
    }

    public void setTemplateId(String templateId) {
        this.templateId = templateId;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }
}
