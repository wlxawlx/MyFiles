package com.jandar.util;

import com.alipay.api.AlipayApiException;
import com.alipay.api.AlipayClient;
import com.alipay.api.AlipayObject;
import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.api.domain.AlipayOpenPublicMessageSingleSendModel;
import com.alipay.api.domain.AlipayOpenPublicMessageTotalSendModel;
import com.alipay.api.internal.util.json.JSONWriter;
import com.alipay.api.request.AlipayMobilePublicMessageSingleSendRequest;
import com.alipay.api.request.AlipayOpenPublicMessageCustomSendRequest;
import com.alipay.api.request.AlipayOpenPublicMessageTotalSendRequest;
import com.alipay.api.response.AlipayMobilePublicMessageSingleSendResponse;
import com.alipay.api.response.AlipayOpenPublicMessageCustomSendResponse;
import com.alipay.api.response.AlipayOpenPublicMessageTotalSendResponse;
import com.alipay.factory.AlipayAPIClientFactory;
import com.jandar.bean.AlipayImageTextMessageModel;
import com.jandar.bean.AlipayImageTextMessageTotalModel;
import com.wechat.WechatOfficialAccountEnv;
import me.chanjar.weixin.common.exception.WxErrorException;
import me.chanjar.weixin.mp.bean.WxMpCustomMessage;
import me.chanjar.weixin.mp.bean.WxMpTemplateMessage;

/**
 * 向支付宝或是微信发送自定义消息或是模板消息的功能类
 * Created by zzw on 16/9/9.
 */
public class MessageSendUtil {

    /**
     * 向支付宝发送单个消息
     *
     * @param model 消息内容
     */
    public static void messageSend(AlipayOpenPublicMessageCustomSendModel model) {
        alipayMessageSend(model);
    }

    private static void alipayMessageSend(AlipayObject model) {
        if (model == null) {
            return;
        }

        JSONWriter writer = new JSONWriter();
        String bizContent = writer.write(model, true);

        AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();

        // 使用SDK，构建发送请求模型
        AlipayOpenPublicMessageCustomSendRequest request = new AlipayOpenPublicMessageCustomSendRequest();
        request.setBizContent(bizContent);
        try {
            // 2.2 使用SDK接口类发送响应
            AlipayOpenPublicMessageCustomSendResponse response = alipayClient.execute(request);

            //这里只是简单的打印，请开发者根据实际情况自行进行处理
            if (null != response && response.isSuccess()) {
                System.out.println("消息发送成功 : response = " + response.getBody());
            } else {
                System.out.println("0消息发送失败 code=" + response.getCode() + "msg=" + response.getMsg());
            }
        } catch (AlipayApiException e) {
            e.printStackTrace();
            System.out.println("1消息发送失败," + e.getMessage());
        }
    }


    /**
     * 向微信发送单个消息
     *
     * @param message 消息内容
     */
    public static void messageSend(WxMpCustomMessage message) {
        if (message == null) {
            return;
        }
        try {
            WechatOfficialAccountEnv.wxMpService.customMessageSend(message);
        } catch (WxErrorException e) {
            System.out.println("消息发送失败," + e.getMessage());
        }
    }

    /**
     * 向支付宝发送群发消息
     *
     * @param model 消息内容
     */
    public static void messageSend(AlipayOpenPublicMessageTotalSendModel model) {
        alipayTotalMessageSend(model);
    }

    private static void alipayTotalMessageSend(AlipayObject model) {
        if (model == null) {
            return;
        }

        JSONWriter writer = new JSONWriter();
        String bizContent = writer.write(model, true);

        AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();

        // 使用SDK，构建群发请求模型
        AlipayOpenPublicMessageTotalSendRequest request = new AlipayOpenPublicMessageTotalSendRequest();
        request.setBizContent(bizContent);
        try {
            // 使用SDK，调用群发接口发送图文消息
            AlipayOpenPublicMessageTotalSendResponse response = alipayClient.execute(request);

            //这里只是简单的打印，请开发者根据实际情况自行进行处理
            if (null != response && response.isSuccess()) {
                System.out.println("消息发送成功 : response = " + response.getBody());
            } else {
                System.out.println("消息发送失败 code=" + response.getCode() + "msg=" + response.getMsg());
            }
        } catch (AlipayApiException e) {
            System.out.println("消息发送失败," + e.getMessage());
        }
    }

    /**
     * 向支付宝发送模板消息
     *
     * @param model 消息内容
     */
    public static void templateSend(AlipayOpenPublicMessageSingleSendModel model) {

        if (model == null) {
            return;
        }

        try {
            JSONWriter writer = new JSONWriter();
            String bizContent = writer.write(model, true);

            AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
            AlipayMobilePublicMessageSingleSendRequest request = new AlipayMobilePublicMessageSingleSendRequest();
            request.setBizContent(bizContent);

            // 2.2 使用SDK接口类发送响应
            AlipayMobilePublicMessageSingleSendResponse response = alipayClient
                    .execute(request);

            // 2.3 商户根据响应结果处理结果
            //这里只是简单的打印，请商户根据实际情况自行进行处理
            if (null != response && response.isSuccess()) {
                System.out.println("消息发送成功，结果为：" + response.getBody());
            } else {
                System.out.println("消息发送失败 code=" + response.getCode() + "msg："
                        + response.getMsg());
            }
        } catch (Exception e) {
            System.out.println("消息发送失败," + e.getMessage());
        }
    }

    /**
     * 向微信发送模板消息
     *
     * @param message 消息内容
     */
    public static void templateSend(WxMpTemplateMessage message) {
        if (message == null) {
            return;
        }
        try {
            WechatOfficialAccountEnv.wxMpService.templateSend(message);
        } catch (WxErrorException e) {
            System.out.println("消息发送失败," + e.getMessage());
        }
    }

    public static void messageSend(AlipayImageTextMessageModel model) {
        alipayMessageSend(model);
    }

    public static void messageSend(AlipayImageTextMessageTotalModel model) {
        alipayTotalMessageSend(model);
    }
}
