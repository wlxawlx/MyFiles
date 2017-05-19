package com.jandar.filter.auth.util;

import com.alipay.api.AlipayApiException;
import com.jandar.alipay.ServiceContext;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * Created by zhufengxiang on 2016/07/07.
 * 授权
 */
public abstract class Auth {
    protected static Log log = LogFactory.getLog(Auth.class);

    protected String code;

    protected String authUrl;
    /**
     * obtain filter 中的处理过程
     */
    public void process(HttpServletRequest request, HttpServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        String authCode = getCode(request);

        ThirdUserInfo thirdUserInfoGetted = ServiceContext.getThirdUserInfo();
        if (authCode != null && authCode.trim().length() > 0  && thirdUserInfoGetted == null) {
            try {
                // 3. 利用authCode获得authToken
                Token token = getToken(authCode);

                // 成功获得authToken
                if (token != null) {

                    // 4. 利用authToken获取用户信息
                    ThirdUserInfo thirdUserInfo = getThirdUserInfo(token);
                    System.out.println("third user info = "+thirdUserInfo);

                    // 成功获得用户信息
                    if (thirdUserInfo != null) {
                        // 这里仅是简单打印， 请开发者按实际情况自行进行处理
                        System.out.println("获取用户信息成功：" + thirdUserInfo);

                        ServiceContext.setThirdUserInfo(thirdUserInfo);
                    } else {
                        // 这里仅是简单打印， 请开发者按实际情况自行进行处理
                        System.out.println("获取用户信息失败");
                        log.error("获取用户信息失败");
                        response.sendRedirect("/404&building/404_cube.html");
                        return;
                    }
                } else {
                    // 这里仅是简单打印， 请开发者按实际情况自行进行处理
                    System.out.println("authCode换取authToken失败");
                    response.sendRedirect("/404&building/404_cube.html");
                    return;
                }
            } catch (AlipayApiException alipayApiException) {
                // 自行处理异常
                alipayApiException.printStackTrace();
                System.out.println("AlipayApiException异常");
                response.sendRedirect("/404&building/404_cube.html");
                return;
            } catch (Exception e) {
                e.printStackTrace();
                System.out.println("WechatException异常");
                response.sendRedirect("/404&building/404_cube.html");
                return;
            }
        }

        chain.doFilter(request, response);
    }

    /**
     * 完成授权后重定向回来会添加上code, 从请求中获取这个参数。
     * @param request
     * @return
     */
    public String getCode(HttpServletRequest request) {
        return request.getParameter(code);
    }

    /**
     * 利用authCode获得authToken
     */
    protected abstract Token getToken(String authCode) throws Exception;

    /**
     * 利用authToken获取第三方用户信息
     */
    protected abstract ThirdUserInfo getThirdUserInfo(Token token) throws Exception;

    /**
     * 将获取到的用户信息转化为本系统的UserInfo.
     */
    protected abstract ThirdUserInfo response2UserInfo(Object response);

    public String getAuthUrl() {
        return authUrl;
    }

}
