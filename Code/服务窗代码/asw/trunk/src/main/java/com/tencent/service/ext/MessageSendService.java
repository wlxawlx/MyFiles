package com.tencent.service.ext;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.tencent.common.Configure;
import com.tencent.protocol.ext.AccessTokenResData;
import com.tencent.protocol.ext.MessageSendResData;
import com.tencent.service.BaseService;
import org.apache.ibatis.parsing.PropertyParser;

import java.util.Properties;

/**
 * Created by zhufengxiang on 2016/09/08.
 * 发送模板消息服务
 */
public class MessageSendService extends BaseService {

    public MessageSendService() throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        super(Configure.MESSAGE_SEND);
    }

    public MessageSendResData messageSend() throws Exception {
        // 获取access token
        AccessTokenService accessTokenService = new AccessTokenService();
        AccessTokenResData accessToken = accessTokenService.getAccessToken();

        Properties properties = new Properties();
        properties.put("ACCESS_TOKEN", accessToken.getAccess_token());

        apiURL = PropertyParser.parse(apiURL, properties);

        // todo 参数对象构建
        String s = sendPostJson(new Object());

        return new Gson().fromJson(s, new TypeToken<MessageSendResData>(){}.getType());
    }
}
