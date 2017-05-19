package com.tencent.service.ext;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.jandar.filter.auth.util.Token;
import com.tencent.common.Configure;
import com.tencent.service.BaseService;
import com.wechat.SNSUserInfo;
import org.apache.ibatis.parsing.PropertyParser;

import java.io.IOException;
import java.security.KeyManagementException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.UnrecoverableKeyException;
import java.util.Properties;

/**
 * Created by zhufengxiang on 2016/08/31.
 *
 */
public class UserInfoService extends BaseService {

    public UserInfoService() throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        super(Configure.USER_INFO);
    }

    public SNSUserInfo getUserInfo(Token token) throws UnrecoverableKeyException, KeyManagementException, NoSuchAlgorithmException, KeyStoreException, IOException {
        Properties properties = new Properties();
        properties.put("ACCESS_TOKEN", token.getAccessToken());
        properties.put("OPENID", token.getOpenId());

        apiURL = PropertyParser.parse(apiURL, properties);

        String s = sendGet();

        System.out.println("获取UserInfoService = " + s);

        return new Gson().fromJson(s, new TypeToken<SNSUserInfo>(){}.getType());
    }
}
