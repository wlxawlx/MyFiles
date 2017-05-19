/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.executor;

import com.alipay.common.MyException;
import com.alipay.util.AlipayMsgBuildUtil;
import com.alipay.util.AlipayMsgSendUtil;
import net.sf.json.JSONObject;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 自定义二维码进入服务窗事件处理器
 * 
 * @author taixu.zqq
 * @version $Id: InAlipayDIYQRCodeEnterExecutor.java, v 0.1 2014年7月24日 下午9:22:02 taixu.zqq Exp $
 */
public class InAlipayDIYQRCodeEnterExecutor implements ActionExecutor {

    /** 线程池 */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /** 业务参数 */
    private JSONObject             bizContent;

    public InAlipayDIYQRCodeEnterExecutor(JSONObject bizContent) {
        this.bizContent = bizContent;
    }

    public InAlipayDIYQRCodeEnterExecutor() {
        super();
    }

    /** 
     * @see com.alipay.executor.ActionExecutor#executor(java.util.Map)
     */
    @Override
    public String execute() throws MyException {
        //自身业务处理
        //理论上，自定义二维码会有sceneId设置，通过该id，开发者开始知道是哪个自定义二维码进入

        String syncResponseMsg = "";
        try {
            JSONObject actionParam = JSONObject.fromObject(bizContent.getString("ActionParam"));
            JSONObject scene = JSONObject.fromObject(actionParam.get("scene"));
            String sceneId = scene.getString("sceneId");
            System.out.println(sceneId);

            //取得发起请求的支付宝账号id
            final String fromUserId = bizContent.getString("FromUserId");

            //1. 首先同步构建ACK响应
            syncResponseMsg = AlipayMsgBuildUtil.buildBaseAckMsg(fromUserId);

            //2. 异步发送消息
            executors.execute(new Runnable() {
                @Override
                public void run() {
                    AlipayMsgSendUtil.sendSingleImgTextMsg(fromUserId, "你好", "欢迎使用本服务");
                }
            });

        } catch (Exception exception) {
            throw new MyException("转换json错误，检查数据格式");
        }

        // 同步返回ACK响应
        return syncResponseMsg;
    }
}
