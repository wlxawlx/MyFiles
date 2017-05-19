/**
 * 文件：PrepareFilter.java 2015年12月28日
 * <p/>
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.filter;

import com.jandar.alipay.ServiceContext;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import java.io.IOException;

/**
 * 上下文初始化过滤器，主要是将request绑定到当前线程。
 *
 * @author dys
 * @version 1.0 2015年12月28日
 *
 */
public class PrepareFilter implements Filter {

    /*
     * (non-Javadoc)
     *
     * @see javax.servlet.Filter#destroy()
     */
    @Override
    public void destroy() {
        ServiceContext.setRequest(null);
    }

    /*
     * (non-Javadoc)
     *
     * @see javax.servlet.Filter#doFilter(javax.servlet.ServletRequest,
     * javax.servlet.ServletResponse, javax.servlet.FilterChain)
     */
    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        try {
            HttpServletRequest req = (HttpServletRequest) request;
            req.setCharacterEncoding("utf-8");
            response.setCharacterEncoding("utf-8");

            ServiceContext.setRequest(req);
            chain.doFilter(request, response);
        } finally {
            ServiceContext.setRequest(null);
        }
    }

    /*
     * (non-Javadoc)
     *
     * @see javax.servlet.Filter#init(javax.servlet.FilterConfig)
     */
    @Override
    public void init(FilterConfig config) throws ServletException {

    }

}
