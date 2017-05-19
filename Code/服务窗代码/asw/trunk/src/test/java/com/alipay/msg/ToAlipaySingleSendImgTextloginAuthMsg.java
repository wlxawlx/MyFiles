/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.msg;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.util.AlipayMsgBuildUtil;
import com.jandar.bean.AlipayImageTextMessageModel;
import com.jandar.util.MessageSendUtil;

/**
 * 开发者发送免登图文消息
 * 
 * @author baoxing.gbx
 * @version $Id: ToAlipaySingleSendImgTextloginAuthMsg.java, v 0.1 Jul 25, 2014 4:38:11 PM baoxing.gbx Exp $
 */
public class ToAlipaySingleSendImgTextloginAuthMsg {

    /** 发给哪个用户 这是测试账号,开发者请自行改为实际的userId */
    private static String TO_USER_ID = "20880032988757974003531012018852";

    /**
     * 
     * @param args
     */
    public static void main(String[] args) {
//        String msg = AlipayMsgBuildUtil.buildImgTextLoginAuthMsg(TO_USER_ID);
//        AlipayMsgSendUtil.execute(msg);
        AlipayImageTextMessageModel model = AlipayMsgBuildUtil.buildSingleImgTextMsg(TO_USER_ID, "欢迎关注本服务,请点击右下角\n“互动”->“个人中心”\n进行个人建档.", null, null, null, null);
        MessageSendUtil.messageSend(model);
    }

}
