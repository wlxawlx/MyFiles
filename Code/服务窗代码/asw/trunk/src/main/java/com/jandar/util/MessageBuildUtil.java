package com.jandar.util;

import com.alipay.api.domain.*;
import com.alipay.api.internal.mapping.ApiField;
import com.alipay.api.internal.util.AlipayLogger;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.*;
import me.chanjar.weixin.mp.bean.WxMpCustomMessage;
import me.chanjar.weixin.mp.bean.WxMpTemplateData;
import me.chanjar.weixin.mp.bean.WxMpTemplateMessage;
import me.chanjar.weixin.mp.bean.custombuilder.NewsBuilder;

import java.beans.IntrospectionException;
import java.beans.PropertyDescriptor;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.List;

/**
 * 构建向支付宝或是微信发送的消息对象
 * Created by zzw on 16/9/9.
 */
public class MessageBuildUtil {

    /**
     * 构建一个文本消息
     *
     * @param toUserId 发送给谁，微信为 openid 支付宝为 alipay_user_id
     * @param content  文本内容
     * @param type     是支付宝还是微信
     * @return 返回文本消息结构
     */
    public static Object textMessage(String toUserId, String content, PlatformType type) {
        if (type == PlatformType.Wechat) {
            return wechatTextMessage(toUserId, content);
        } else {
            return alipayTextMessage(toUserId, content);
        }
    }

    public static AlipayOpenPublicMessageCustomSendModel alipayTextMessage(String toUserId, String content) {
        AlipayOpenPublicMessageCustomSendModel model = new AlipayOpenPublicMessageCustomSendModel();
        model.setToUserId(toUserId);
        model.setMsgType("text");
        Text text = new Text();
        text.setContent(content);
        model.setText(text);

        return model;
    }

    public static AlipayOpenPublicMessageTotalSendModel alipayTextMessage(String content) {
        AlipayOpenPublicMessageTotalSendModel model = new AlipayOpenPublicMessageTotalSendModel();
        model.setMsgType("text");
        Text text = new Text();
        text.setContent(content);
        model.setText(text);

        return model;
    }

    public static WxMpCustomMessage wechatTextMessage(String toUserId, String content) {
        return WxMpCustomMessage
                .TEXT()
                .toUser(toUserId)
                .content(content)
                .build();
    }

    /**
     * 图文消息构造
     *
     * @param toUserId 发送给认证谁
     * @param articles 图文内容
     * @param type     平台
     * @return 消息对象
     */
    public static Object imageTextMessage(String toUserId, List<ArticleBean> articles, PlatformType type) {
        if (type == PlatformType.Wechat) {
            return wechatImageTextMessage(toUserId, articles);
        } else {
            return alipayImageTextMessage(toUserId, articles);
        }
    }

    /**
     * 发送图文消息
     *
     * @param toUserId 发送给谁
     * @param articles 发送的图文内容，最多六个
     * @return 消息对象
     */
    public static AlipayImageTextMessageModel alipayImageTextMessage(String toUserId, List<ArticleBean> articles) {
        AlipayImageTextMessageModel model = new AlipayImageTextMessageModel();
        model.setMsgType("image-text");
        model.setToUserId(toUserId);
        model.setArticles(articleBead2Alipay(articles));

        return model;
    }

    /**
     * 发送图文消息
     *
     * @param toUserId 发送给谁
     * @param articles 发送的图文内容，最多六个
     * @return 消息对象
     */
    public static WxMpCustomMessage wechatImageTextMessage(String toUserId, List<ArticleBean> articles) {
        NewsBuilder builder = WxMpCustomMessage
                .NEWS()
                .toUser(toUserId);

        for (ArticleBean bean : articles) {
            WxMpCustomMessage.WxArticle article = new WxMpCustomMessage.WxArticle();
            article.setUrl(bean.getUrl());
            article.setPicUrl(bean.getImageUrl());
            article.setDescription(bean.getDesc());
            article.setTitle(bean.getTitle());
            builder.addArticle(article);
        }

        return builder.build();
    }

    public static AlipayImageTextMessageTotalModel alipayImageTextMessage(List<ArticleBean> articles) {
        AlipayImageTextMessageTotalModel model = new AlipayImageTextMessageTotalModel();
        model.setMsgType("image-text");
        model.setArticles(articleBead2Alipay(articles));

        return model;
    }

    private static List<Article> articleBead2Alipay(List<ArticleBean> articles) {
        List<Article> alipayArticles = new ArrayList<Article>();
        for (ArticleBean bean : articles) {
            Article content = new Article();
            content.setTitle(bean.getTitle());
            content.setDesc(bean.getDesc());
            content.setImageUrl(bean.getImageUrl());
            content.setUrl(bean.getUrl());
            content.setActionName(bean.getActionName());
            alipayArticles.add(content);
        }
        return alipayArticles;
    }

    /**
     * 模板消息
     *
     * @param toUserId   发送给认谁
     * @param templateId 模板代码
     * @param url        单击后的详细信息页
     * @param topColor   头的颜色
     * @param data       模板项的内容
     * @param type       平台类型
     * @return 消息对象
     */
    public static Object templateMessage(String toUserId,
                                         String templateId,
                                         String url,
                                         String topColor,
                                         TemplateData data,
                                         PlatformType type) {
        if (type == PlatformType.Wechat) {
            return wechatTemplateMessage(toUserId, templateId, url, topColor, data);
        } else {
            return alipayTemplateMessage(toUserId, templateId, url, topColor, null, data);
        }
    }

    /**
     * @param toUserId   发送给谁
     * @param templateId 模板ID
     * @param url        详细信息地址
     * @param topColor   格式：#000000
     * @return 图文对象
     */
    public static AlipayOpenPublicMessageSingleSendModel alipayTemplateMessage(String toUserId,
                                                                               String templateId,
                                                                               String url,
                                                                               String topColor,
                                                                               String actionName,
                                                                               TemplateData data) {
        AlipayOpenPublicMessageSingleSendModel model = new AlipayOpenPublicMessageSingleSendModel();
        model.setToUserId(toUserId);

        Template template = new Template();
        template.setTemplateId(templateId);

        AlipayTemplateContext context = new AlipayTemplateContext();
        context.setUrl(url);
        context.setHeadColor(topColor);
        context.setActionName(actionName);

        context.setFirst(data.getFirst());
        context.setRemark(data.getRemark());
        context.setKeyword1(data.getKeyword1());
        context.setKeyword2(data.getKeyword2());
        context.setKeyword3(data.getKeyword3());
        context.setKeyword4(data.getKeyword4());
        context.setKeyword5(data.getKeyword5());
        context.setKeyword6(data.getKeyword6());
        context.setKeyword7(data.getKeyword7());

        template.setContext(context);

        model.setTemplate(template);
        return model;
    }


    public static WxMpTemplateMessage wechatTemplateMessage(String toUserId, String templateId, String url, String topColor, TemplateData data) {
        WxMpTemplateMessage model = new WxMpTemplateMessage();
        model.setToUser(toUserId);
        model.setTemplateId(templateId);
        model.setUrl(url);
        model.setTopColor(topColor);

        Field[] ff = data.getClass().getDeclaredFields();
        try {
            for (Field field : ff) {
                // 获取注解
                ApiField jsonField = field.getAnnotation(ApiField.class);
                if (jsonField != null) {
                    PropertyDescriptor pd = new PropertyDescriptor(field.getName(),
                            data.getClass());
                    Method accessor = pd.getReadMethod();
                    if (!accessor.isAccessible())
                        accessor.setAccessible(true);
                    Keyword value = (Keyword) accessor.invoke(data, (Object[]) null);
                    if (value == null)
                        continue;

                    model.addWxMpTemplateData(new WxMpTemplateData(jsonField.value(), value.getValue(), value.getColor()));
                }
            }
        } catch (IntrospectionException | IllegalAccessException | IllegalArgumentException | InvocationTargetException e1) {
            AlipayLogger.logBizError(e1);
        }

        return model;
    }
}
