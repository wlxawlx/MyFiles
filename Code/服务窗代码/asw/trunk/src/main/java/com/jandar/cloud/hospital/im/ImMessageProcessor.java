package com.jandar.cloud.hospital.im;

import cloud.hospital.cloudend.message.base.AbsMessage;
import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.jandar.cloud.hospital.exception.NotSupportIMMessageTypeException;
import com.jandar.cloud.hospital.im.execute.ImDispatcher;
import com.jandar.cloud.hospital.im.execute.ImExecutor;

/**
 * Created by flyhorse on 2016/11/3.
 * 即时消息处理器
 */
public class ImMessageProcessor {

    /**
     * 把IM消息转化成支付宝消息
     *
     * @param message IM消息
     * @return 支付宝消息对象
     * @throws com.jandar.cloud.hospital.exception.NotSupportIMMessageTypeException
     */
    public static MsgModel imMessage2MsgModel(AbsMessage message) throws NotSupportIMMessageTypeException {
        MsgModel model = null;
        System.out.println("=======++=========imMessage2MsgModel========111============::"+message.getMessageType());
        switch (message.getMessageType()) {
            case TEXT_MESSAGE:
                //纯文本聊天
                System.out.println("=======++=========imMessage2MsgModel======TEXT_MESSAGE==============::"+message.getMessageType());
                String toUserId=message.getToUserId();
                String doctorCode=message.getFromUserId();//医生编号
                String msg=message.getBody();
                ImExecutor chatExecutor= ImDispatcher.getChatToPatientExecutor();
                model=chatExecutor.execute(toUserId,doctorCode, msg);
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
                System.out.println("=======++=========imMessage2MsgModel======TEMPLATE_MESSAGE==============::"+message.getBody());
                String msgBody=message.getBody();
                if(msgBody==null)
                    break;

                toUserId=message.getToUserId();
                doctorCode=message.getFromUserId();//医生编号
                //如果是呼叫
                if(ImDispatcher.CALL_PATIENT_CODE.equals(msgBody))
                {
                    ImExecutor callExecutor= ImDispatcher.getCallPatientExecutor();
                    model=callExecutor.execute(toUserId,doctorCode);
                    break;
                }else if(ImDispatcher.PAYMENT_NOTIFY_CODE.equals(msgBody)){
                    ImExecutor paymentNotifyExecutor= ImDispatcher.getPaymentNotifyExecutor();
                    model=paymentNotifyExecutor.execute(toUserId,doctorCode);
                    break;
                }
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

                toUserId=message.getToUserId();
                model=executor.execute(toUserId,msgCtn);
                break;
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
}
