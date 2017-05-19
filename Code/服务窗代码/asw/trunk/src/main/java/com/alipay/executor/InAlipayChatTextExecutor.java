/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.executor;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.common.MyException;
import com.alipay.util.AlipayMsgBuildUtil;
import com.alipay.util.OutMessageUtil;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.OutMessage;
import com.jandar.handle.message.ChatTextHandler;
import com.jandar.util.MessageSendUtil;
import net.sf.json.JSONObject;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 聊天执行器(纯文本消息)
 *
 * @author baoxing.gbx
 * @version $Id: InAlipayChatExecutor.java, v 0.1 Jul 28, 2014 5:17:04 PM baoxing.gbx Exp $
 */
public class InAlipayChatTextExecutor implements ActionExecutor {

    /**
     * 线程池
     */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /**
     * 业务参数
     */
    private JSONObject bizContent;

    public InAlipayChatTextExecutor(JSONObject bizContent) {
        this.bizContent = bizContent;
    }

    public InAlipayChatTextExecutor() {
        super();
    }

    /**
     * @see com.alipay.executor.ActionExecutor#execute()
     */
    @Override
    public String execute() throws MyException {

        //取得发起请求的支付宝账号id
        final String fromUserId = bizContent.getString("FromUserId");

        // 取得用户发送的消息内容
        final String content = bizContent.getJSONObject("Text").getString("Content");

        //1. 首先同步构建ACK响应
        String syncResponseMsg = AlipayMsgBuildUtil.buildBaseAckMsg(fromUserId);

        OutMessage outMessage = new ChatTextHandler().handle(fromUserId, null, content, PlatformType.Alipay);
        final AlipayOpenPublicMessageCustomSendModel model = OutMessageUtil.OutMessage2AlipayMessage(outMessage);
        if (model != null) {
            //2. 异步发送消息
            executors.execute(new Runnable() {
                @Override
                public void run() {
                    MessageSendUtil.messageSend(model);
                }
            });
        }

        // 3.返回同步的ACK响应
        return syncResponseMsg;
    }

}
