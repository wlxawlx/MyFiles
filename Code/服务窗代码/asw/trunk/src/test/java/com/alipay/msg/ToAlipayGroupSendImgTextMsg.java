/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.msg;

import com.alipay.api.domain.AlipayOpenPublicMessageTotalSendModel;
import com.alipay.util.AlipayMsgBuildUtil;
import com.jandar.bean.AlipayImageTextMessageTotalModel;
import com.jandar.util.MessageSendUtil;

/**
 * 开发者群发消息接口（群发图文消息）
 * 
 * @author baoxing.gbx
 * @version $Id: ToAlipayGroupSendImgTextMsg.java, v 0.1 Jul 25, 2014 3:41:40 PM baoxing.gbx Exp $
 */
public class ToAlipayGroupSendImgTextMsg {

    /**
     * 
     * @param args
     */
    public static void main(String[] args) {
        AlipayImageTextMessageTotalModel model = AlipayMsgBuildUtil.buildGroupImgTextMsg("你好", "欢迎来到我的世界", "", "http://www.jandar.com.cn", "点击继续");
        MessageSendUtil.messageSend(model);
    }

}
