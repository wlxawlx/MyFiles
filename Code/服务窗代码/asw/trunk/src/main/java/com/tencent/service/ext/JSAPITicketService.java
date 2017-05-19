package com.tencent.service.ext;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.tencent.common.Configure;
import com.tencent.common.TokenType;
import com.tencent.protocol.ext.AccessTokenResData;
import com.tencent.protocol.ext.JSAPITicketResData;
import com.tencent.service.BaseService;
import org.apache.ibatis.parsing.PropertyParser;

import java.util.Properties;

/**
 * Created by zhufengxiang on 2016/09/08.
 *
 */
public class JSAPITicketService extends BaseService {
    public JSAPITicketService() throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        super(Configure.JSAPI_TICKET);
    }

    public JSAPITicketResData getTicket() throws Exception {
        // 获取access token
        AccessTokenService accessTokenService = new AccessTokenService();
        AccessTokenResData accessToken = accessTokenService.getAccessToken();

        Properties properties = new Properties();
        properties.put("ACCESS_TOKEN", accessToken.getAccess_token());

        apiURL = PropertyParser.parse(apiURL, properties);

        // 判断全局缓存中是否失效。
        if (Configure.getExpiresCache().expired(TokenType.JSAPI)) {
            String s = sendGet();
            JSAPITicketResData jsapiTicketResData = new Gson().fromJson(s, new TypeToken<JSAPITicketResData>() {}.getType());
            Configure.getExpiresCache().putObject(TokenType.JSAPI, jsapiTicketResData);
            return jsapiTicketResData;
        } else {
            return (JSAPITicketResData) Configure.getExpiresCache().getObject(TokenType.JSAPI);
        }
    }
}
