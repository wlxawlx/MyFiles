package com.jandar.filter.auth.util;

import com.alipay.api.AlipayApiException;
import com.alipay.api.AlipayClient;
import com.alipay.api.request.AlipaySystemOauthTokenRequest;
import com.alipay.api.request.AlipayUserUserinfoShareRequest;
import com.alipay.api.response.AlipaySystemOauthTokenResponse;
import com.alipay.api.response.AlipayUserUserinfoShareResponse;
import com.alipay.constants.AlipayServiceEnvConstants;
import com.alipay.factory.AlipayAPIClientFactory;
import com.jandar.alipay.AlipayUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.util.AlipayUserInfoUtil;

/**
 * Created by zhufengxiang on 2016/07/07.
 * 支付宝授权
 */
class AuthAlipay extends Auth {
    private static AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();

    {
        code = "auth_code";
        authUrl = "https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?auth_skip=false&scope=auth_userinfo&app_id=" +
                AlipayServiceEnvConstants.APP_ID + "&redirect_uri=REDIRECT_URI";
    }

    @Override
    protected Token getToken(String authCode) throws AlipayApiException {
        AlipaySystemOauthTokenRequest oauthTokenRequest = new AlipaySystemOauthTokenRequest();
        oauthTokenRequest.setCode(authCode.trim());
        oauthTokenRequest.setGrantType(AlipayServiceEnvConstants.GRANT_TYPE);
        AlipaySystemOauthTokenResponse oauthTokenResponse = alipayClient.execute(oauthTokenRequest);
        if (oauthTokenResponse != null && oauthTokenResponse.isSuccess()) {
            String accessToken = oauthTokenResponse.getAccessToken();
            String expireIn = oauthTokenResponse.getExpiresIn();

            Token token = new Token();
            token.setAccessToken(accessToken);
            token.setExpiresIn(Integer.valueOf(expireIn));
            return token;
        } else {
            return null;
        }

    }

    @Override
    protected ThirdUserInfo getThirdUserInfo(Token token) throws AlipayApiException {
        AlipayUserUserinfoShareRequest userinfoShareRequest = new AlipayUserUserinfoShareRequest();
        AlipayUserUserinfoShareResponse userinfoShareResponse = alipayClient.execute(userinfoShareRequest, token.getAccessToken());
        if (null != userinfoShareResponse && userinfoShareResponse.isSuccess()) {
            return response2UserInfo(userinfoShareResponse);
        } else {
            return null;
        }
    }

    @Override
    protected ThirdUserInfo response2UserInfo(Object response) {
        if (response instanceof AlipayUserUserinfoShareResponse) {
            AlipayUserUserinfoShareResponse alipayUserUserinfoShareResponse = (AlipayUserUserinfoShareResponse) response;
            AlipayUserInfo alipayUserInfo = AlipayUserInfoUtil.getUserInfo(alipayUserUserinfoShareResponse);

            ThirdUserInfo thirdUserInfo = new ThirdUserInfo();
            thirdUserInfo.setOpenId(alipayUserInfo.getUserId());
            thirdUserInfo.setName(alipayUserInfo.getRealName());
            thirdUserInfo.setSex(alipayUserInfo.getGender() == AlipayUserInfo.Gender.male ? 1 : 2);
            thirdUserInfo.setCertNo(alipayUserInfo.getCertNo());
            thirdUserInfo.setAvatar(alipayUserInfo.getAvatar());
            thirdUserInfo.setUserType(PlatformType.Alipay);
            return thirdUserInfo;
        }
        return null;
    }

    private static class Holder {
        private static Auth auth = new AuthAlipay();
    }

    public static Auth getInstance() {
        return Holder.auth;
    }
}
