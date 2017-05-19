package com.jandar.util;

import org.apache.commons.lang3.StringUtils;

import java.io.UnsupportedEncodingException;
import java.util.Random;

public class StringUtil {

    public static Object defaultIfBlank(Integer arg) {
        return defaultIfBlank(arg, 0);
    }

    public static Object defaultIfBlank(Integer arg, int defaultValue) {
        return (arg != null && arg > 0) ? arg : defaultValue;
    }

    public static Object defaultIfBlank(Long arg) {
        return defaultIfBlank(arg, 0L);
    }

    public static Object defaultIfBlank(Long arg, long defaultValue) {
        return (arg != null && arg > 0L) ? arg : defaultValue;
    }

    /**
     * 指定分隔符拆分字符串为数组(特殊字符无需转义，如： \\| 直接写成 | 即可)
     * split("ab|cd|ef", "|")     = ["ab", "cd", "ef"]
     * split("ab|cd|", "|")       = ["ab", "cd", ""]
     *
     * @param arg
     * @param splitChar
     */
    public static String[] split(String arg, String splitChar) {
        return split(arg, splitChar, false);
    }

    /**
     * 指定分隔符拆分字符串为数组(特殊字符无需转义，如： \\| 直接写成 | 即可)
     *
     * @param arg
     * @param splitChar
     * @param isIgnoreBlank true  split("ab|cd|", "|") => ["ab", "cd"]
     *                      false split("ab|cd|", "|") => ["ab", "cd", ""]
     * @return
     */
    public static String[] split(String arg, String splitChar, boolean isIgnoreBlank) {
        if (isIgnoreBlank) {
            return StringUtils.split(arg, splitChar);
        } else {
            return StringUtils.splitByWholeSeparatorPreserveAllTokens(arg, splitChar);
        }
    }

    public static boolean isBlank(Object arg) {
        if (arg == null || "".equals(arg)) {
            return true;
        } else {
            return false;
        }
    }

    public static boolean isNotBlank(Object arg) {
        return !isBlank(arg);
    }

    public static Random random;

    static {
        random = new Random();
    }

    public static String getCode(int len) {
        String yzm = "";
        for (int j = 0; j < len; j++) {
            int i = random.nextInt(10);
            yzm += i;
        }
        return yzm;
    }

    public static boolean isFullChar(String s) {
        String reg = "[a-zA-Z]";
        String[] sArr = s.split("");
        for (int i = 0; i < sArr.length; i++) {
            if (!"".equals(sArr[i])) {
                if (sArr[i].matches(reg) == false) {
                    return false;
                }
            }

        }
        return true;
    }

    public static String formatUTF(String str) {
        try {
            return new String(str.getBytes("ISO-8859-1"), "utf-8");
        } catch (UnsupportedEncodingException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return "";
    }
}
