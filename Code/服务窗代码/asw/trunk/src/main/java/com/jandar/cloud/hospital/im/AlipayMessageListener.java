package com.jandar.cloud.hospital.im;

import cloud.hospital.cloudend.message.base.AbsMessage;
import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.jandar.cloud.hospital.exception.NotSupportIMMessageTypeException;
import com.jandar.cloud.hospital.util.MesssageConver;
import com.jandar.util.MessageSendUtil;

/**
 * 回复消息支付宝接收端，它接收到的消息应该转发给支付宝
 * Created by zzw on 16/9/8.
 */
public class AlipayMessageListener extends BaseMessageListener {

    @Override
    public void processMessage(AbsMessage message) {
        try {
            AlipayOpenPublicMessageCustomSendModel model = MesssageConver.ImMessage2AlipayMessage(message);
            if (model != null) {
                MessageSendUtil.messageSend(model);
            }
        } catch (NotSupportIMMessageTypeException e) {
            responseNotSupportMessage(message, e.getMessage());
        }
    }



}
