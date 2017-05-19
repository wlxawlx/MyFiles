package com.jandar.cloud.hospital.util;

import cloud.hospital.cloudend.message.StateMessage;
import cloud.hospital.cloudend.message.base.AbsMessage;
import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.util.AlipayMsgBuildUtil;
import com.jandar.cloud.hospital.exception.NotSupportIMMessageTypeException;
import com.jandar.cloud.hospital.im.execute.ImDispatcher;
import com.jandar.cloud.hospital.im.execute.ImExecutor;
import com.jandar.util.StringUtil;
import me.chanjar.weixin.mp.bean.WxMpCustomMessage;

/**
 * 消息转化器
 * Created by zzw on 16/9/11.
 */
public class MesssageConver {

    /**
     * 把IM消息转化成支付宝消息
     *
     * @param message IM消息
     * @return 支付宝消息对象
     * @throws NotSupportIMMessageTypeException
     */
    public static AlipayOpenPublicMessageCustomSendModel ImMessage2AlipayMessage(AbsMessage message) throws NotSupportIMMessageTypeException {
        AlipayOpenPublicMessageCustomSendModel model = null;

        System.out.println("=======++=========AlipayOpenPublicMessageCustomSendModel====================::"+message.getMessageType());

        switch (message.getMessageType()) {
            case TEXT_MESSAGE:
                //纯文本聊天
                System.out.println("=======++=========AlipayOpenPublicMessageCustomSendModel======TEXT_MESSAGE==============::"+message.getMessageType());
                String toUserId=message.getToUserId();
                String msg=message.getBody();
                ImExecutor chatExecutor= ImDispatcher.getChatToPatientExecutor();
                chatExecutor.execute(toUserId,msg);

                //model = AlipayMsgBuildUtil.buildSingleTextMsg(message.getToUserId(), message.getBody());
                break;
            case STATE_MESSAGE:
               // String customState = ((StateMessage) message).getCustomState();
                //if (StringUtil.isNotBlank(customState)) {
                   // TODO 获得相关消息并向用户发送相关模板消息

                // }
                //break;
            case IMAGE_MESSAGE:
                // 客服接口中没有发送纯图片的功能
            case TEMPLATE_MESSAGE:
                System.out.println("=======++=========AlipayOpenPublicMessageCustomSendModel======TEMPLATE_MESSAGE==============::"+message.getMessageType());
                String msgBody=message.getBody();
                if(msgBody==null)
                    break;
                String[] bodys= msgBody.split("#");
                if(bodys.length==1)
                {
                    System.out.println("=============TEMPLATE_MESSAGE================invalid msg format==");
                    break;
                }
                String imCode=bodys[0];
                String msgCtn=msgBody.substring(msgBody.indexOf("#")+1);  //+1是为了去掉 井 号
                System.out.println("=============TEMPLATE_MESSAGE================msgCtn:"+msgCtn);
                ImExecutor executor= ImDispatcher.getImExecutor(imCode);
                if(executor==null)
                {
                    System.out.println("=============TEMPLATE_MESSAGE================invalid code==");
                    break;
                }
                executor.execute(msgCtn);



            case UNKNOWN_MESSAGE:
            case NORMAL_FILE_MESSAGE:
            case VOICE_MESSAGE:
            case VIDEO_MESSAGE:
            case VIDEO_REQUEST_MESSAGE:
            case VIDEO_RESPONSE_MESSAGE:
            case VIDEO_CLOSE_MESSAGE:
                throw new NotSupportIMMessageTypeException("支付宝服务窗不支持此类消息");
        }
        return model;
    }

    /**
     * 把IM消息转化成微信消息
     *
     * @param message IM消息
     * @return 微信消息对象
     * @throws NotSupportIMMessageTypeException
     */
    public static WxMpCustomMessage ImMessage2WechatMessage(AbsMessage message) throws NotSupportIMMessageTypeException {

        WxMpCustomMessage result = null;
        switch (message.getMessageType()) {
            case TEXT_MESSAGE:
                result = WxMpCustomMessage
                        .TEXT()
                        .toUser(message.getToUserId())
                        .content(message.getBody())
                        .build();
                break;
            case IMAGE_MESSAGE:
                result = WxMpCustomMessage
                        .IMAGE()
                        .toUser("OPENID")
                        .mediaId("MEDIA_ID")
                        .build();
                break;
            case UNKNOWN_MESSAGE:
            case TEMPLATE_MESSAGE:
                // 模板消息使用系统发送，而不是医生的手动行为
            case NORMAL_FILE_MESSAGE:
            case VOICE_MESSAGE:
            case VIDEO_MESSAGE:
            case STATE_MESSAGE:
            case VIDEO_REQUEST_MESSAGE:
            case VIDEO_RESPONSE_MESSAGE:
            case VIDEO_CLOSE_MESSAGE:
                throw new NotSupportIMMessageTypeException("微信服务号不支持此类消息");
        }

        return result;
    }
}
