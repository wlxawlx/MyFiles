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
import com.jandar.bean.AlipayImageTextMessageModel;
import com.jandar.util.MessageSendUtil;

/**
 * 开发者单发图文消息
 *
 * @author baoxing.gbx
 * @version $Id: ToAlipaySingleSendImgTextMsg.java, v 0.1 Jul 25, 2014 3:40:35 PM baoxing.gbx Exp $
 */
public class ToAlipaySingleSendImgTextMsg {

    /**
     * 发给哪个用户 这是测试账号,开发者请自行改为实际的userId
     */
    //aYMvrMC8+qdi3Mj1lqxRZJPUsrychFTewHXFVXq5ySDxWgIluiZN3K2r70Eebm4r01
    private static String TO_USER_ID = "aYMvrMC8+qdi3Mj1lqxRZJPUsrychFTewHXFVXq5ySDxWgIluiZN3K2r70Eebm4r01";

    /**
     * @param args
     */
    public static void main(String[] args) {
        AlipayImageTextMessageModel model = AlipayMsgBuildUtil.buildSingleImgTextMsg(TO_USER_ID, "你好", "欢迎使用本服务", null, null, null);
        MessageSendUtil.messageSend(model);
    }

}
