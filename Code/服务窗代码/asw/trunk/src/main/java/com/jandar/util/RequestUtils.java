/**
 * 文件：RequestUtil.java 2015年12月29日
 * <p>
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.util;

import com.jandar.alipay.core.struct.PlatformType;

import javax.servlet.http.HttpServletRequest;

/**
 * HttpServletRequest工具类。
 *
 * @author dys
 * @version 1.0 2015年12月29日
 */
public class RequestUtils {

    public static String getServletPath(HttpServletRequest request) {
        String servletPath = request.getServletPath();

        String requestUri = request.getRequestURI();
        // Detecting other characters that the servlet container cut off (like
        // anything after ';')
        if (requestUri != null && servletPath != null && !requestUri.endsWith(servletPath)) {
            int pos = requestUri.indexOf(servletPath);
            if (pos > -1) {
                servletPath = requestUri.substring(requestUri.indexOf(servletPath));
            }
        }

        if (null != servletPath && !"".equals(servletPath)) {
            return servletPath;
        }

        int startIndex = request.getContextPath().equals("") ? 0 : request.getContextPath().length();
        int endIndex = request.getPathInfo() == null ? requestUri.length()
                : requestUri.lastIndexOf(request.getPathInfo());

        if (startIndex > endIndex) { // this should not happen
            endIndex = startIndex;
        }

        return requestUri.substring(startIndex, endIndex);
    }

    public static String getUri(HttpServletRequest request) {
        String uri = getServletPath(request);
        if (uri != null && !"".equals(uri)) {
            return uri;
        }

        uri = request.getRequestURI();
        return uri.substring(request.getContextPath().length());
    }

    public static PlatformType getType(HttpServletRequest request) {
        PlatformType result = PlatformType.Alipay;
        String typeString = request.getParameter("type");
        System.out.println("requestUtils:getType:typeString:"+typeString);
        if (typeString == null) {
            typeString = (String) request.getSession().getAttribute("type");
            if (typeString == null) {
                String userAgent = request.getHeader("User-Agent").toLowerCase();
                if (userAgent.contains("micromessenger")) {
                    result = PlatformType.Wechat;
                }
            }
        } else {
            try {
                result = PlatformType.valueOf(typeString);
            } catch (Exception ex) {
                if (typeString.toLowerCase().equals(PlatformType.Wechat.toString().toLowerCase())) {
                    result = PlatformType.Wechat;
                } else {
                    result = PlatformType.Alipay;
                }
            }
        }
        return result;
    }
}
