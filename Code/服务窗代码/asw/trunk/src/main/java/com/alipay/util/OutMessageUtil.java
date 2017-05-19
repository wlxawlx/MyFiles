package com.alipay.util;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.jandar.bean.OutMessage;
import com.jandar.util.MessageBuildUtil;
import me.chanjar.weixin.common.api.WxConsts;

/**
 * 把自定义的同步回复消息转化为支付宝的回复消息
 * Created by zzw on 16/8/29.
 */
public class OutMessageUtil {

    public static AlipayOpenPublicMessageCustomSendModel OutMessage2AlipayMessage(OutMessage message) {
        if (message == null) {
            return null;
        }

        AlipayOpenPublicMessageCustomSendModel model = null;
        switch (message.getMsgType()) {
            case WxConsts.CUSTOM_MSG_TEXT:
                model = MessageBuildUtil.alipayTextMessage(message.getToUserId(), message.getContent());
                break;
            // TODO
        }

        return model;
    }
}
