package com.jandar.handle.message;

import cloud.hospital.cloudend.message.ImageMessage;
import com.alipay.sign.Base64;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.OutMessage;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.util.ImageUtil;
import me.chanjar.weixin.common.api.WxConsts;

/**
 * 图片消息
 * Created by zzw on 16/8/29.
 */
public class ChatImageHandler extends ChatMessageHandler {

    /**
     * 处理文本消息
     *
     * @param fromUser  谁发送的, 支付宝为User_id, 微信为 FromUserName
     * @param toUser    发给谁的, 支付宝为Null, 微信为 ToUserName
     * @param imagePath 发送图片的路径
     * @param type      是支付宝还是微信发的
     * @return 回复的消息
     */
    public OutMessage handle(String fromUser, String toUser, String imagePath, PlatformType type) {
        try {
            Patient userInfo = getUserInfo(fromUser, type);
            checkReservation(userInfo);

            ImageMessage message = new ImageMessage();
            build(message, userInfo, type);

            byte[] bytes = ImageUtil.thumbnailImageByte(imagePath, 100, 100);
            message.setImage(imagePath, Base64.encode(bytes));

            sendMessage(message, userInfo, type);
        } catch (Exception ex) {
            return buildOutMessage(WxConsts.CUSTOM_MSG_TEXT, fromUser, ex.getMessage());
        }

        return null;
    }
}
