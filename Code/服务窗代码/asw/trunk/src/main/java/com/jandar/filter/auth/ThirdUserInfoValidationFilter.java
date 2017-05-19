/**
 * 文件：AlipayUserInfoValidationFilter.java 2015年12月29日
 * <p>
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.filter.auth;


import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.util.FilterUtils;
import com.jandar.cloud.hospital.service.TestSession;
import com.jandar.config.ConfigHandler;
import com.jandar.filter.auth.util.Auth;
import com.jandar.filter.auth.util.AuthFactory;
import com.jandar.filter.auth.util.ThirdUserInfo;
import com.jandar.util.RequestUtils;
import org.apache.ibatis.io.ResolverUtil;
import org.apache.log4j.Logger;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.List;
import java.util.regex.Pattern;

/**
 * 校验是否已经获取到用户信息（检查session中是否有用户信息），还没获取的话重定向到用户信息授权接口地址。
 * ps : 客户端需要传入type参数, value = alipay or wechat。 根据是支付宝服务窗还是微信公众号来确定value.
 * 或者是客户端将type参数添加到session中.
 */
public class ThirdUserInfoValidationFilter implements Filter {

    static Logger logger = Logger.getLogger(ThirdUserInfoValidationFilter.class);

    private List<Pattern> excludedPatterns;

    @Override
    public void destroy() {

    }

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        System.out.println("===================ThirdUserInfoValidationFilter==========do filter:");
        HttpServletRequest req = (HttpServletRequest) request;
        HttpServletResponse resp = (HttpServletResponse) response;

        if (excludedPatterns != null && FilterUtils.isUrlExcluded(req, excludedPatterns)
                || ServiceContext.getThirdUserInfo() != null) {
            chain.doFilter(request, response);
            return;
        }


        //判断是否是测试，如果是测试.....
        if(ConfigHandler.systemIsTest())
        {
            System.out.println("===================ThirdUserInfoValidationFilter======isTest========:");
            ThirdUserInfo userInfo= TestSession.getThirdUserInfo(req);
            if(userInfo==null)
            {
                resp.sendRedirect("404.html");
                return;
            }

            chain.doFilter(request, response);
            return;
        }



        PlatformType type = RequestUtils.getType(req);
        req.getSession().setAttribute("type", type.toString());
        Auth auth = AuthFactory.getAuth(type);

        // 有code参数应该是完成授权后重定向回来
        String authCode = auth.getCode(req);
        // 重定向到授权接口时增加的标记参数，没实际意义，用来降低死循环的风险
        String token = req.getParameter("thirdAuthToken");
        if (authCode != null && authCode.trim().length() > 0 || token != null && token.trim().length() > 0) {
            chain.doFilter(request, response);
            return;
        }
        System.out.println("===================ThirdUserInfoValidationFilter=====22=====do filter:");
        StringBuilder redirectUrl = new StringBuilder();
        redirectUrl.append(req.getRequestURL().toString());
        String queryString = req.getQueryString();
        if (queryString != null) {
            redirectUrl.append("?").append(queryString).append("&thirdAuthToken=").append(genToken());
        } else {
            redirectUrl.append("?").append("thirdAuthToken=").append(genToken());
        }

        String url = auth.getAuthUrl().replace("REDIRECT_URI", urlEncode(redirectUrl.toString()));
        System.out.println("===================ThirdUserInfoValidationFilter======333====do filter:");
        resp.sendRedirect(url);
    }

    @Override
    public void init(FilterConfig config) throws ServletException {

        String excludedPatternsConfig = config.getInitParameter("excludedPatterns");
        if (excludedPatternsConfig != null && excludedPatternsConfig.trim().length() > 0) {
            excludedPatterns = FilterUtils.buildExcludedPatternsList(excludedPatternsConfig);
        }
    }

    private String urlEncode(String url) {
        try {
            return URLEncoder.encode(url, "UTF-8");
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
            return url;
        }
    }

    private String genToken() {
        return "AUT-" + System.currentTimeMillis();
    }

}
