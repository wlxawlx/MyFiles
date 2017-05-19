/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.util;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.api.domain.AlipayOpenPublicMessageTotalSendModel;
import com.alipay.constants.AlipayServiceEnvConstants;
import com.jandar.bean.AlipayImageTextMessageModel;
import com.jandar.bean.AlipayImageTextMessageTotalModel;
import com.jandar.bean.ArticleBean;
import com.jandar.util.MessageBuildUtil;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

/**
 * 消息构造工具
 *
 * @author baoxing.gbx
 * @version $Id: AlipayMsgBuildUtil.java, v 0.1 Jul 24, 2014 5:47:19 PM baoxing.gbx Exp $
 */
public class AlipayMsgBuildUtil {

    /**
     * 构造单发图文消息
     *
     * @param fromUserId
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     * @param imageUrl   消息显示的图片,可以为空
     * @param url        消息链接的地下,可以为空
     * @param actionName 消息链接地址显示名称,和url对应
     * @return
     */
    public static AlipayImageTextMessageModel buildSingleImgTextMsg(String fromUserId, String title, String desc, String imageUrl, String url, String actionName) {
        List<ArticleBean> articles = new ArrayList<ArticleBean>();
        {
            ArticleBean content = new ArticleBean();
            content.setTitle(title);
            content.setDesc(desc);
            content.setImageUrl(imageUrl);
            content.setUrl(url);
            content.setActionName(actionName);
            articles.add(content);
        }
        return MessageBuildUtil.alipayImageTextMessage(fromUserId, articles);
    }

    /**
     * 构造群发图文消息
     *
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     * @param imageUrl   消息显示的图片,可以为空
     * @param url        消息链接的地下,可以为空
     * @param actionName 消息链接地址显示名称,和url对应
     * @return
     */
    public static AlipayImageTextMessageTotalModel buildGroupImgTextMsg(String title, String desc, String imageUrl, String url, String actionName) {
        List<ArticleBean> articles = new ArrayList<ArticleBean>();
        {
            ArticleBean content = new ArticleBean();
            content.setTitle(title);
            content.setDesc(desc);
            content.setImageUrl(imageUrl);
            content.setUrl(url);
            content.setActionName(actionName);
            articles.add(content);
        }
        return MessageBuildUtil.alipayImageTextMessage(articles);
    }

    /**
     * 构造单发纯文本消息
     *
     * @param toUserId 发送给谁的
     * @param content  发送的内容
     * @return
     */
    public static AlipayOpenPublicMessageCustomSendModel buildSingleTextMsg(String toUserId, String content) {
        return buildTextMsg(toUserId, content);
    }

    /**
     * 构造群发纯文本消息
     *
     * @return
     */
    public static AlipayOpenPublicMessageTotalSendModel buildGroupTextMsg(String content) {
        return MessageBuildUtil.alipayTextMessage(content);
    }

    /**
     * 构造纯文件消息
     *
     * @param toUserId 发送给谁的,空时群发
     * @param content  发送的内容
     * @return
     */
    private static AlipayOpenPublicMessageCustomSendModel buildTextMsg(String toUserId, String content) {
        return MessageBuildUtil.alipayTextMessage(toUserId, content);
    }

    /**
     * 构造免登图文消息
     *
     * @param toUserId
     * @return
     */
    public static String buildImgTextLoginAuthMsg(String toUserId) {

        StringBuilder sb = new StringBuilder();

        //免登连接地址，开发者需根据部署服务修改相应服务ip地址
        String url = "http://10.15.132.68:8080/AlipayFuwuDemo/loginAuth.html";

        //构建json格式的单发免登图文消息体     authType 等于 "loginAuth"表示免登消息 ： 所有内容开发者请根据自有业务自行设置响应值，这里只是个样例
        sb.append("{'articles':[{'actionName':'立即查看','desc':'这是图文内容','imageUrl':'http://pic.alipayobjects.com/e/201311/1PaQ27Go6H_src.jpg','title':'这是标题','url':'"
                + url
                + "', 'authType':'loginAuth'}],'msgType':'image-text', 'toUserId':'"
                + toUserId + "'}");

        return sb.toString();
    }

    /**
     * 构造基础的响应消息
     *
     * @return
     */
    public static String buildBaseAckMsg(String toUserId) {
        StringBuilder sb = new StringBuilder();
        sb.append("<XML>");
        sb.append("<ToUserId><![CDATA[" + toUserId + "]]></ToUserId>");
        sb.append("<AppId><![CDATA[" + AlipayServiceEnvConstants.APP_ID + "]]></AppId>");
        sb.append("<CreateTime>" + Calendar.getInstance().getTimeInMillis() + "</CreateTime>");
        sb.append("<MsgType><![CDATA[ack]]></MsgType>");
        sb.append("</XML>");
        return sb.toString();
    }

}
