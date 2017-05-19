/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.msg;

import com.alipay.api.domain.AlipayOpenPublicMessageTotalSendModel;
import com.alipay.util.AlipayMsgBuildUtil;
import com.jandar.util.MessageSendUtil;

/**
 * 开发者群发消息接口（群发纯文本消息）
 * 
 * @author baoxing.gbx
 * @version $Id: ToAlipayGroupSendTextMsg.java, v 0.1 Jul 25, 2014 3:42:09 PM baoxing.gbx Exp $
 */
public class ToAlipayGroupSendTextMsg {

    /**
     * 
     * @param args
     */
    public static void main(String[] args) {
        AlipayOpenPublicMessageTotalSendModel model = AlipayMsgBuildUtil.buildGroupTextMsg("欢迎来到这");
        MessageSendUtil.messageSend(model);
    }

}
