/**
 * Alipay.com Inc.
 * Copyright (c) 2004-2014 All Rights Reserved.
 */
package com.alipay.executor;

import com.alipay.api.internal.util.StringUtils;
import com.alipay.util.AlipayMsgBuildUtil;
import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.dao.UserInfoService;
import com.jandar.config.ConfigHandler;
import net.sf.json.JSONObject;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 关注服务窗执行器
 *
 * @author baoxing.gbx
 * @version $Id: InAlipayFollowExecutor.java, v 0.1 Jul 24, 2014 4:29:04 PM baoxing.gbx Exp $
 */
public class InAlipayFollowExecutor implements ActionExecutor {

    /**
     * 线程池
     */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /**
     * 业务参数
     */
    private JSONObject bizContent;

    public InAlipayFollowExecutor(JSONObject bizContent) {
        this.bizContent = bizContent;
    }

    public InAlipayFollowExecutor() {
        super();
    }

    @Override
    public String execute() {

        //TODO 根据支付宝请求参数，可以将支付宝账户UID-服务窗ID关系持久化，用于后续开发者自己的其他操作
        // 这里只是个样例程序，所以这步省略。
        // 直接构造简单响应结果返回
        final String fromUserId = bizContent.getString("FromUserId");

        executors.execute(new Runnable() {
            @Override
            public void run() {

                // 获得个人信息
                UserInfo userInfo = null;
                try {
                    userInfo = UserInfoService.getUserInfo(fromUserId);
                } catch (HospitalException e) {
                    e.printStackTrace();
                    return;
                }
                if (userInfo == null) {
                    AlipayMsgSendUtil.sendSingleImgTextMsg(fromUserId, "进行个人建档",
                            "请点击我的账户进行建档操作",
                            "http://" + ConfigHandler.getSelfServiceHost() + "/patientInfo.html",
                            "去建档");
                } else if (StringUtils.isEmpty(userInfo.getBrid()) || "0".equals(userInfo.getBrid())) {
                    AlipayMsgSendUtil.sendSingleImgTextMsg(fromUserId, "绑定就诊卡",
                            "请先到我的账户进行绑卡操作",
                            "http://" + ConfigHandler.getSelfServiceHost() + "/bindCard.html",
                            "去绑卡");
                }
            }
        });


        return AlipayMsgBuildUtil.buildBaseAckMsg(fromUserId);
    }
}
