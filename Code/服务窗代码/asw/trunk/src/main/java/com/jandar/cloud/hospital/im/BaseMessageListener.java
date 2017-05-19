package com.jandar.cloud.hospital.im;

import cloud.hospital.cloudend.MessageListener;
import cloud.hospital.cloudend.message.TextMessage;
import cloud.hospital.cloudend.message.base.AbsMessage;
import cloud.hospital.cloudend.videorequest.VideoVoiceMessage;
import cloud.hospital.cloudend.videorequest.VideoVoiceResponse;
import org.jivesoftware.smack.packet.Message;

/**
 * IM 消息的接收到消息的监听
 * Created by zzw on 16/9/8.
 */
public abstract class BaseMessageListener extends MessageListener {

    @Override
    public void processVideoVoiceMessage(VideoVoiceMessage message) {
        // 音视频聊天功能不支持，直接拒绝掉
        VideoVoiceResponse response = new VideoVoiceResponse();
        response.setSessionId(message.getSessionId());
        response.setVoiceOnly(message.isVoiceOnly());
        response.setStreamName(message.getStreamName());
        response.setFromUser(message.getToUserId(), message.getToName());
        response.setToUser(message.getFromUserId(), message.getFromName());

        // 拒绝
        response.setReject(true);
        try {
            getConnection().sendMessage(message, message.getImFrom());
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    void responseNotSupportMessage(AbsMessage message, String eMessage) {
        try {
            TextMessage response = new TextMessage();
            response.setSessionId(message.getSessionId());
            response.setFromUser(message.getToUserId(), message.getToName());
            response.setToUser(message.getFromUserId(), message.getFromName());
            response.setBody(eMessage);
            getConnection().sendMessage(response, message.getImFrom(), Message.Type.error);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}
