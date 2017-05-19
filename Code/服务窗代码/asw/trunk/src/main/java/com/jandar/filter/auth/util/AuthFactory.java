package com.jandar.filter.auth.util;


import com.jandar.alipay.core.struct.PlatformType;

/**
 * Created by zhufengxiang on 2016/07/07.
 * 授权工厂
 */
public abstract class AuthFactory {
    /**
     * 根据客户端发送的type来获取单例auth.
     * 默认使用支付宝授权.
     * @param type
     * @return
     */
    public static Auth getAuth(PlatformType type) {
        if (type == PlatformType.Alipay) {
            return AuthAlipay.getInstance();
        } else if (type == PlatformType.Wechat) {
            return AuthWechat.getInstance();
        } else {
            return AuthAlipay.getInstance();
        }
    }
}
