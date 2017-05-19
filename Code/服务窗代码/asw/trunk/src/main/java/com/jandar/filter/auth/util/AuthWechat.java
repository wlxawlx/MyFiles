package com.jandar.filter.auth.util;

import com.jandar.alipay.core.struct.PlatformType;
import com.tencent.common.Configure;
import com.tencent.service.ext.AuthTokenService;
import com.tencent.service.ext.UserInfoService;
import com.wechat.SNSUserInfo;

/**
 * Created by zhufengxiang on 2016/07/07.
 * 微信授权
 */
class AuthWechat extends Auth {

    {
        code = "code";
        authUrl = ("https://open.weixin.qq.com/connect/oauth2/authorize?" +
                "appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect")
                .replace("APPID", Configure.getAppid());
    }

    @Override
    protected Token getToken(String authCode) {
        try {
            AuthTokenService authTokenService = new AuthTokenService();
            return authTokenService.getAuthToken(authCode);
        } catch (Exception e) {
            e.printStackTrace();
        }
        // Token token = WechatUtil.getOauth2AccessToken(Configure.getAppid(), Configure.getSecret(), authCode);
        // log.info("Token 信息: " +token);
        // return token;
        return null;
    }

    @Override
    protected ThirdUserInfo getThirdUserInfo(Token token) {
        // SNSUserInfo snsUserInfo = WechatUtil.getSNSUserInfo(token.getAccessToken(), token.getOpenId());
        try {
            UserInfoService userInfoService = new UserInfoService();
            SNSUserInfo userInfo = userInfoService.getUserInfo(token);
            return response2UserInfo(userInfo);
        } catch (Exception e) {
            e.printStackTrace();
        }

        return null;
    }

    @Override
    protected ThirdUserInfo response2UserInfo(Object response) {
        if (response instanceof SNSUserInfo) {
            SNSUserInfo snsUserInfo = (SNSUserInfo) response;

            ThirdUserInfo thirdUserInfo = new ThirdUserInfo();
            thirdUserInfo.setOpenId(snsUserInfo.getOpenid());
            thirdUserInfo.setName(snsUserInfo.getNickname());
            thirdUserInfo.setSex(snsUserInfo.getSex());
            thirdUserInfo.setCertNo("抱歉, 微信共享信息中没有证件号码.");
            thirdUserInfo.setAvatar(snsUserInfo.getHeadimgurl());
            thirdUserInfo.setUserType(PlatformType.Wechat);
            return thirdUserInfo;
        }
        return null;
    }

    private static class Holder {
        private static Auth auth = new AuthWechat();
    }

    public static Auth getInstance() {
        return Holder.auth;
    }
}
