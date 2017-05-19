package com.jandar.cloud.hospital.im;

import cloud.hospital.cloudend.message.base.AbsMessage;
import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.util.AlipayMsgBuildUtil;
import com.jandar.cloud.hospital.exception.NotSupportIMMessageTypeException;
import com.jandar.cloud.hospital.util.MesssageConver;
import com.jandar.util.MessageSendUtil;

/**
 * Created by flyhorse on 2016/11/3.
 */
public class MessageListener extends BaseMessageListener{

    enum  MST_TYPE
    {
        NOWAY,ALIPAY,WECHAT,TWOWAYS
        //分别是  不用发,支付宝， 微信,
    }

    @Override
    public void processMessage(AbsMessage message) {
        //处理消息
        System.out.println("========MessageListener========processMessage==========="+message.getMessageType());

        try {
              MsgModel model=ImMessageProcessor.imMessage2MsgModel(message);
              if(model==null)
              {
                  System.out.println("========MessageListener========processMessage========model is null===");
                 return;
              }
            System.out.println("===111111111111=====MessageListener========send alipay==="+model.getMessage());
            if(model.getSendType()==SendType.ALIPAY)
            {
                System.out.println("========MessageListener========send alipay==="+model.getMessage());
                AlipayOpenPublicMessageCustomSendModel alipayModel=toAlipayModel(model);
                MessageSendUtil.messageSend(alipayModel);

            }
            else
            {
                System.out.println("========MessageListener========processMessag==="+model.getSendType());
            }

        } catch (NotSupportIMMessageTypeException e) {
            responseNotSupportMessage(message, e.getMessage());
        }
    }


    private AlipayOpenPublicMessageCustomSendModel toAlipayModel(MsgModel model)
    {
        AlipayOpenPublicMessageCustomSendModel alipayModel= AlipayMsgBuildUtil.buildSingleTextMsg(model.getAlipayUserId(), model.getMessage());

        return alipayModel;
    }



}
