/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.msg;

import com.alipay.api.AlipayApiException;
import com.alipay.api.AlipayClient;
import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.api.request.AlipayOpenPublicMessageCustomSendRequest;
import com.alipay.api.response.AlipayOpenPublicMessageCustomSendResponse;
import com.alipay.factory.AlipayAPIClientFactory;
import com.alipay.util.AlipayMsgBuildUtil;
import com.jandar.util.MessageSendUtil;

/**
 * 开发者单发纯文本消息
 *
 * @author baoxing.gbx
 * @version $Id: ToAlipaySingleSendTextMsg.java, v 0.1 Jul 25, 2014 3:39:35 PM baoxing.gbx Exp $
 */
public class ToAlipaySingleSendTextMsg {

    /** 发给哪个用户 这是测试账号,开发者请自行改为实际的userId */
    //aYMvrMC8+qdi3Mj1lqxRZJPUsrychFTewHXFVXq5ySDxWgIluiZN3K2r70Eebm4r01
    private static String TO_USER_ID = "20880042881656405200148012213554";

    /**
     *
     * @param args
     */
    public static void main(String[] args) {
        AlipayOpenPublicMessageCustomSendModel model = AlipayMsgBuildUtil.buildSingleTextMsg(TO_USER_ID, "这是纯文本");
        MessageSendUtil.messageSend(model);
    }

}
