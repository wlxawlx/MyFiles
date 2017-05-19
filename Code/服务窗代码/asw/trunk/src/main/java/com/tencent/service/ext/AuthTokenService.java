package com.tencent.service.ext;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.jandar.filter.auth.util.Token;
import com.tencent.common.Configure;
import com.tencent.service.BaseService;
import org.apache.commons.lang.builder.ReflectionToStringBuilder;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.ibatis.parsing.PropertyParser;

import java.util.Properties;

/**
 * Created by zhufengxiang on 2016/08/31.
 * 获取 auth token
 */
public class AuthTokenService extends BaseService {
    private static Log log = LogFactory.getLog(AuthTokenService.class);

    public AuthTokenService() throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        super(Configure.AUTH_TOKEN);
    }

    public Token getAuthToken(String authCode) throws Exception{
        Properties properties = new Properties();
        properties.put("APPID", Configure.getAppid());
        properties.put("SECRET", Configure.getSecret());
        properties.put("CODE", authCode);
        System.out.println("=====CODE======" +authCode);
        apiURL = PropertyParser.parse(apiURL, properties);

        String s = sendGet();

        Result result = new Gson().fromJson(s, new TypeToken<Result>(){}.getType());
        if (result.errcode == null) {
            Token token = new Token();
            token.setAccessToken(result.getAccess_token());
            token.setOpenId(result.getOpenid());
            token.setExpiresIn(result.getExpires_in());
            return token;
        } else {
            log.error(result);
            return null;
        }
    }

    private static class Result {
        private String errcode;
        private String errmsg;

        private String access_token;
        private long expires_in;
        private String refresh_token;
        private String openid;
        private String scope;
        private String unionid;

        public Result() {
        }

        public String getErrcode() {
            return errcode;
        }

        public void setErrcode(String errcode) {
            this.errcode = errcode;
        }

        public String getErrmsg() {
            return errmsg;
        }

        public void setErrmsg(String errmsg) {
            this.errmsg = errmsg;
        }

        public String getAccess_token() {
            return access_token;
        }

        public void setAccess_token(String access_token) {
            this.access_token = access_token;
        }

        public long getExpires_in() {
            return expires_in;
        }

        public void setExpires_in(long expires_in) {
            this.expires_in = expires_in;
        }

        public String getRefresh_token() {
            return refresh_token;
        }

        public void setRefresh_token(String refresh_token) {
            this.refresh_token = refresh_token;
        }

        public String getOpenid() {
            return openid;
        }

        public void setOpenid(String openid) {
            this.openid = openid;
        }

        public String getScope() {
            return scope;
        }

        public void setScope(String scope) {
            this.scope = scope;
        }

        public String getUnionid() {
            return unionid;
        }

        public void setUnionid(String unionid) {
            this.unionid = unionid;
        }

        @Override
        public String toString() {
            return ReflectionToStringBuilder.toString(this);
        }
    }
}
