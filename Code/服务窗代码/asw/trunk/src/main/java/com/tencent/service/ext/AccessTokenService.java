package com.tencent.service.ext;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.tencent.common.Configure;
import com.tencent.common.TokenType;
import com.tencent.protocol.ext.AccessTokenResData;
import com.tencent.service.BaseService;

/**
 * Created by zhufengxiang on 2016/09/08.
 *
 */
public class AccessTokenService extends BaseService {

    public AccessTokenService() throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        super(Configure.ACCESS_TOKEN);
    }

    public AccessTokenResData getAccessToken() throws Exception{
        // 判断全局缓存中是否失效。
        if (Configure.getExpiresCache().expired(TokenType.ACCESS)) {
            String s = sendGet();
            AccessTokenResData accessTokenResData = new Gson().fromJson(s, new TypeToken<AccessTokenResData>(){}.getType());
            Configure.getExpiresCache().putObject(TokenType.ACCESS, accessTokenResData);
            return accessTokenResData;
        } else {
            return (AccessTokenResData) Configure.getExpiresCache().getObject(TokenType.ACCESS);
        }
    }
}
