package com.alipay.util;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.api.domain.AlipayOpenPublicMessageTotalSendModel;
import com.jandar.bean.AlipayImageTextMessageModel;
import com.jandar.bean.AlipayImageTextMessageTotalModel;
import com.jandar.util.MessageSendUtil;

/**
 * 支付宝服务窗,消息异步发送的封装
 * Created by zzw on 16/1/6.
 */
public class AlipayMsgSendUtil {

    /**
     * 发送单发纯文本消息
     *
     * @param fromUserId 发送给谁的
     * @param content    发送的内容
     */
    public static void sendSingleTextMsg(String fromUserId, String content) {
        execute(AlipayMsgBuildUtil.buildSingleTextMsg(fromUserId, content));
    }

    /**
     * 发送群发纯文本消息
     *
     * @param content 发送的内容
     */
    public static void sendGroupTextMsg(String content) {
        execute(AlipayMsgBuildUtil.buildGroupTextMsg(content));
    }

    /**
     * 发送单图片消息
     *
     * @param fromUserId 发送给谁的,空时群发
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     * @param imageUrl   消息显示的图片,可以为空
     * @param url        消息链接的地下,可以为空
     * @param actionName 消息链接地址显示名称,和url对应
     */
    public static void sendSingleImgTextMsg(String fromUserId, String title, String desc, String imageUrl, String url, String actionName) {
        execute(AlipayMsgBuildUtil.buildSingleImgTextMsg(fromUserId, title, desc, imageUrl, url, actionName));
    }

    private static void execute(AlipayImageTextMessageModel model) {
        MessageSendUtil.messageSend(model);
    }

    /**
     * 发送单图片消息, 只有标题
     *
     * @param fromUserId 发送给谁的,空时群发
     * @param title      消息的标题,不能为空
     */
    public static void sendSingleImgTextMsg(String fromUserId, String title) {
        sendSingleImgTextMsg(fromUserId, title, null, null, null, null);
    }

    /**
     * 发送单图片消息, 不带图,不带链接
     *
     * @param fromUserId 发送给谁的,空时群发
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     */
    public static void sendSingleImgTextMsg(String fromUserId, String title, String desc) {
        sendSingleImgTextMsg(fromUserId, title, desc, null, null, null);
    }

    /**
     * 发送单图片消息,带图,不带链接
     *
     * @param fromUserId 发送给谁的,空时群发
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     * @param imageUrl   消息显示的图片,可以为空
     */
    public static void sendSingleImgTextMsg(String fromUserId, String title, String desc, String imageUrl) {
        sendSingleImgTextMsg(fromUserId, title, desc, imageUrl, null, null);
    }

    /**
     * 发送单图片消息,不带图,带链接
     *
     * @param fromUserId 发送给谁的,空时群发
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     * @param url        消息链接的地下,可以为空
     * @param actionName 消息链接地址显示名称,和url对应
     */
    public static void sendSingleImgTextMsg(String fromUserId, String title, String desc, String url, String actionName) {
        sendSingleImgTextMsg(fromUserId, title, desc, null, url, actionName);
    }

    /**
     * 发送群图片消息
     *
     * @param title      消息的标题,不能为空
     * @param desc       消息的内容,不能为空
     * @param imageUrl   消息显示的图片,可以为空
     * @param url        消息链接的地下,可以为空
     * @param actionName 消息链接地址显示名称,和url对应
     */
    public static void sendGroupImgTextMsg(String title, String desc, String imageUrl, String url, String actionName) {
        execute(AlipayMsgBuildUtil.buildGroupImgTextMsg(title, desc, imageUrl, url, actionName));
    }

    private static void execute(AlipayImageTextMessageTotalModel model) {
        MessageSendUtil.messageSend(model);
    }

    private static void execute(AlipayOpenPublicMessageTotalSendModel model) {
        MessageSendUtil.messageSend(model);
    }

    /**
     * 异步发送消息
     *
     * @param model 消息结构内容
     */
    public static void execute(AlipayOpenPublicMessageCustomSendModel model) {
        MessageSendUtil.messageSend(model);
    }
}
