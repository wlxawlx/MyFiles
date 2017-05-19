/**
 * 文件：AlipayUserInfoObtainFilter.java 2015年12月29日
 * <p/>
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.filter.auth;

import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.config.ConfigHandler;
import com.jandar.filter.auth.util.Auth;
import com.jandar.filter.auth.util.AuthFactory;
import com.jandar.util.RequestUtils;
import org.apache.log4j.Logger;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * 根据code获取ThirdUserInfo信息。
 * ps : 客户端需要传入type参数, value = alipay or wechat。 根据是支付宝服务窗还是微信公众号来确定value.
 * 或者是客户端将type参数添加到session中.
 */
public class ThirdUserInfoObtainFilter implements Filter {

    static Logger logger = Logger.getLogger(ThirdUserInfoObtainFilter.class);

    @Override
    public void destroy() {

    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        HttpServletRequest req = (HttpServletRequest) request;
        HttpServletResponse resp = (HttpServletResponse) response;

        PlatformType type = RequestUtils.getType(req);
        req.getSession().setAttribute("type", type.toString());

        //判断是否是测试，如果是测试.....
        if(ConfigHandler.systemIsTest())
        {
            System.out.println("===================ThirdUserInfoObtainFilter======isTest========:");
            chain.doFilter(request, response);
            return;
        }


        Auth auth = AuthFactory.getAuth(type);

        System.out.println("===================ThirdUserInfoObtainFilter==========do filter:"+type);
        auth.process(req, resp, chain);
    }

    @Override
    public void init(FilterConfig config) throws ServletException {

    }
}
