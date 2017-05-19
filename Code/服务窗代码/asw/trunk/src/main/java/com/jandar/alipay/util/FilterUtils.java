/**
 * 文件：FilterUtils.java 2015年12月29日
 *
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.alipay.util;

import com.jandar.util.RequestUtils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.regex.Pattern;

import javax.servlet.http.HttpServletRequest;

/**
 * 过滤器辅助工具类。
 *
 * @author dys
 * @version 1.0 2015年12月29日
 *
 */
public class FilterUtils {

	public static List<Pattern> buildExcludedPatternsList(String patterns) {
		if (null != patterns && patterns.trim().length() != 0) {
			List<Pattern> list = new ArrayList<Pattern>();
			String[] tokens = patterns.split(",");
			for (String token : tokens) {
				list.add(Pattern.compile(token.trim()));
			}
			return Collections.unmodifiableList(list);
		} else {
			return null;
		}
	}

	public static boolean isUrlExcluded(HttpServletRequest request, List<Pattern> excludedPatterns) {
		if (excludedPatterns != null) {
			String uri = RequestUtils.getUri(request);
			for (Pattern pattern : excludedPatterns) {
				if (pattern.matcher(uri).matches()) {
					return true;
				}
			}
		}
		return false;
	}

}
