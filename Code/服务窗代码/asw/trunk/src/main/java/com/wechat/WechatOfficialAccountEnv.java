package com.wechat;

import com.jandar.config.ConfigHandler;
import me.chanjar.weixin.mp.api.WxMpInMemoryConfigStorage;
import me.chanjar.weixin.mp.api.WxMpService;
import me.chanjar.weixin.mp.api.impl.WxMpServiceImpl;

/**
 * Created by zhufengxiang on 2016/07/08.
 * 微信公众号环境
 */
public abstract class WechatOfficialAccountEnv {
    // 公众号唯一id
    public static String appid = ConfigHandler.getWechatOfficialAccountID();
    // 公众号密钥
    public static String secret = ConfigHandler.getWechatOfficialAccountSecret();

    // token 值验证使用
    public static String token = ConfigHandler.getWechatToken();

    // aes 加密使用的密钥
    public static String aeskey = ConfigHandler.getWechatAesKey();

    public static WxMpInMemoryConfigStorage wxMpConfigStorage;
    public static WxMpService wxMpService;

    static {
        wxMpConfigStorage = new WxMpInMemoryConfigStorage();
        wxMpConfigStorage.setAppId(WechatOfficialAccountEnv.appid); // 设置微信公众号的appid
        wxMpConfigStorage.setSecret(WechatOfficialAccountEnv.secret); // 设置微信公众号的app corpSecret
        wxMpConfigStorage.setToken(WechatOfficialAccountEnv.token); // 设置微信公众号的token
        wxMpConfigStorage.setAesKey(WechatOfficialAccountEnv.aeskey); // 设置微信公众号的EncodingAESKey

        wxMpService = new WxMpServiceImpl();
        wxMpService.setWxMpConfigStorage(wxMpConfigStorage);
    }
}
