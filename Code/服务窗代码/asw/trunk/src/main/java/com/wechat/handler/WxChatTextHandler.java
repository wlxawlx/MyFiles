package com.wechat.handler;

import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.OutMessage;
import com.jandar.handle.message.ChatTextHandler;
import com.wechat.util.OutMessageUtil;
import me.chanjar.weixin.common.exception.WxErrorException;
import me.chanjar.weixin.common.session.WxSessionManager;
import me.chanjar.weixin.mp.api.WxMpMessageHandler;
import me.chanjar.weixin.mp.api.WxMpService;
import me.chanjar.weixin.mp.bean.WxMpXmlMessage;
import me.chanjar.weixin.mp.bean.WxMpXmlOutMessage;

import java.util.Map;

/**
 * 普通文本消息
 * Created by zzw on 16/8/29.
 */
public class WxChatTextHandler implements WxMpMessageHandler {
    @Override
    public WxMpXmlOutMessage handle(WxMpXmlMessage wxMessage,
                                    Map<String, Object> context,
                                    WxMpService wxMpService,
                                    WxSessionManager sessionManager) throws WxErrorException {

        OutMessage outMessage = new ChatTextHandler().handle(wxMessage.getFromUserName(),
                wxMessage.getToUserName(),
                wxMessage.getContent(),
                PlatformType.Wechat);
        return OutMessageUtil.OutMessage2WxMpXmlOutMessage(outMessage);
    }
}
