/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.executor;

import com.alipay.util.AlipayMsgBuildUtil;
import com.alipay.util.AlipayMsgSendUtil;
import net.sf.json.JSONObject;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 菜单点击异步响应执行器
 *
 * @author baoxing.gbx
 * @version $Id: InAlipayAsyncMsgSendExecutor.java, v 0.1 Jul 24, 2014 4:30:38 PM baoxing.gbx Exp $
 */
public class InAlipayAsyncMsgSendExecutor implements ActionExecutor {

    /** 线程池 */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /** 业务参数 */
    private JSONObject             bizContent;

    public InAlipayAsyncMsgSendExecutor(JSONObject bizContent) {
        this.bizContent = bizContent;
    }

    public InAlipayAsyncMsgSendExecutor() {
        super();
    }

    @Override
    public String execute() {

        //取得发起请求的支付宝账号id
        final String fromUserId = bizContent.getString("FromUserId");

        //1. 首先同步响应一个消息
        String syncResponseMsg = AlipayMsgBuildUtil.buildBaseAckMsg(fromUserId);

        //2. 异步发送消息
        executors.execute(new Runnable() {
            @Override
            public void run() {
                AlipayMsgSendUtil.sendSingleImgTextMsg(fromUserId, "你好", "欢迎使用本服务");
            }
        });

        return syncResponseMsg;
    }
}
