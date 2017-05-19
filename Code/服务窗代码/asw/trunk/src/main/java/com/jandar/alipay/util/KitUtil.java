package com.jandar.alipay.util;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Map;

/**
 * Created by zzw on 15/10/27.
 */
public class KitUtil {

    /**
     * 判断值是否为null,如果是返回 ""
     *
     * @param map map 对象
     * @param key key
     * @return 值
     */
    public static String getString(Map<String, Object> map, String key) {
        if (map == null) {
            return "";
        }
        if (map.get(key) instanceof net.sf.json.JSONNull) {
            return "";
        } else {
            String result = (String) map.get(key);
            return result == null ? "" : result;            
        }
    }

    /**
     * 获得整形
     *
     * @param map 对象
     * @param key key
     * @param value 默认值
     * @return 转化失败时, 返回默认值
     */
    public static Integer getInteger(Map<String, Object> map, String key, Integer value) {
        if (map == null || !map.containsKey(key)) {
            return value;
        }
        Integer result = value;
        Object o = map.get(key);
        if (o instanceof Integer) {
            result = (Integer) o;
        } else if (o instanceof String) {
            try {
                result = Integer.valueOf((String) o);
            } catch (NumberFormatException e) {
//                throw new ProtocolException(ProtocolException.RETCODE_ARGERROR, "类型参数错误");
            }
        } else {
            try {
                result = Integer.valueOf(o.toString());
            } catch (NumberFormatException e) {
//                throw new ProtocolException(ProtocolException.RETCODE_ARGERROR, "类型参数错误");
            }
        }

        return result;
    }

    /**
     * 获得布尔值
     *
     * @param map 对象
     * @param key key
     * @param defaultValue 默认值
     * @return
     */
    public static boolean getBoolean(Map<String, Object> map, String key, boolean defaultValue) {
        if (map == null || !map.containsKey(key)) {
            return defaultValue;
        }

        Boolean result = defaultValue;
        Object o = map.get(key);
        if (o instanceof Boolean) {
            result = (Boolean) o;
        } else if (o instanceof String) {
            result = Boolean.parseBoolean((String) o);
        } else {
            result = Boolean.valueOf(o.toString());
        }

        return result;
    }

    /**
     * 获得时间值
     *
     * @param map 对象
     * @param key key
     * @param sdf 格式定义
     * @return
     */
    public static Date getDate(Map<String, Object> map, String key, SimpleDateFormat sdf) {
        if (map == null || !map.containsKey(key)) {
            return null;
        }
        Date rt = null;
        Object o = map.get(key);
        if (o instanceof String) {
            try {
                String str = (String) o;
                Date date = sdf.parse(str);

                rt = date;
            } catch (ParseException ex) {
            }
        } else if (o instanceof Date) {
            rt = (Date) o;
        }
        return rt;
    }

    /**
     * 获得长整形
     *
     * @param map 对象
     * @param key key
     * @param value 默认值
     * @return 转化失败时, 返回默认值
     */
    public static Long getLong(Map<String, Object> map, String key, Long value) {
        if (map == null || !map.containsKey(key)) {
            return value;
        }
        Long result = value;
        Object o = map.get(key);
        if (o instanceof Long) {
            result = (Long) o;
        } else if (o instanceof String) {
            try {
                result = Long.valueOf((String) o);
            } catch (NumberFormatException e) {
//                throw new ProtocolException(ProtocolException.RETCODE_ARGERROR, "类型参数错误");
            }
        } else {
            try {
                result = Long.valueOf(o.toString());
            } catch (NumberFormatException e) {
//                throw new ProtocolException(ProtocolException.RETCODE_ARGERROR, "类型参数错误");
            }
        }

        return result;
    }    
    /**
     * 判断字符串是否为空
     *
     * @param str 要求的字符串
     * @return
     */
    public static boolean isEmpty(String str) {
        return str == null || "".equals(str);
    }

    /**
     * 由纯字母组成的字符串
     *
     * @param s
     * @return
     */
    public static boolean isPureLetters(String s) {
        String reg = "[a-zA-Z]";
        String[] sArr = s.split("");
        for (String aSArr : sArr) {
            if (!"".equals(aSArr)) {
                if (!aSArr.matches(reg)) {
                    return false;
                }
            }
        }
        return true;
    }
}
