package com.jandar.alipay.util;

import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;
import java.util.Map;

import org.apache.commons.beanutils.BeanUtils;

import com.jandar.alipay.core.struct.OutpatientOrderNumber;
import com.jandar.alipay.core.struct.SchedulingInfo;

/**
 * Created by zzw on 16/1/27.
 */
public class MapUtil {

    /**
     * 取map对象中的值,格式是字符串
     *
     * @param map
     * @param key
     * @return
     */
    public static String getString(Map<String, Object> map, String key) {
        Object value = map.get(key);
        if (value == null) {
            return null;
        }

        if (value instanceof String) {
            return (String) value;
        }

        return value.toString();
    }

    public static Double getDouble(Map<String, Object> map, String key) {
        Object value = map.get(key);
        if (value == null) {
            return null;
        }
        if (value instanceof String) {
            return (Double) value;
        }
        return Double.valueOf(value.toString());
    }

    /**
     * 获得数据类型
     *
     * @param map
     * @param key
     * @return
     */
    public static Integer getInteger(Map<String, Object> map, String key) {
        Object value = map.get(key);
        if (value == null) {
            return null;
        }

        if (value instanceof Integer) {
            return (Integer) value;
        }
        if (value instanceof Double){
            value = Math.round(Float.valueOf(value.toString()));
            return (Integer) value;
        }

        // 它可能会出异常,但我们不处理,如果出现异常说明有一方协议处理不当
        return Integer.valueOf(value.toString());
    }

    /**
     * Map转换成Bean，使用泛型免去了类型转换的麻烦。
     *
     * @param <T>    要转化成哪个对象
     * @param map    数据内容
     * @param aClass 要转化成哪个对象类型
     * @return 最后转化成的对象
     */
    public static <T> T map2Bean(Map<String, String> map, Class<T> aClass) {
        T bean = null;
        try {
            bean = aClass.newInstance();
            BeanUtils.populate(bean, map);
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        }
        return bean;
    }

    /**
     * Map转换成Bean,同时考虑 map 中 key 与 bead 的参数名不一样的情况
     *
     * @param map    数据内容
     * @param aClass 要转化成哪个对象类型
     * @param keyMap 数据内容key和 bean 参数名不同时的映射关系 key 为数据内容key; value 为对应的bean参数名
     * @param <T>    泛型
     * @return 最后转换成功的对象
     */
    public static <T> T map2Bean(Map<String, String> map, Class<T> aClass, Map<String, String> keyMap) {
        for (Map.Entry<String, String> entry : keyMap.entrySet()) {
            String key = entry.getKey();
            if (map.containsKey(entry.getKey())) {
                String value = map.remove(key);
                map.put(entry.getValue(), value);
            }
        }
        return map2Bean(map, aClass);
    }

    /**
     * bean 转换成 Map
     *
     * @param bean 要转换的对象
     * @return 最后的map对象
     */
    public static Map<String, Object> bean2Map(Object bean) {
        Map<String, Object> map = null;
        try {
            map = BeanUtils.describe(bean);
            map.remove("class");
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        }
        return map;
    }

    public static Map<String, String> bean2Map2(Object bean) {
        Map<String, String> map = null;
        try {
            map = BeanUtils.describe(bean);
            map.remove("class");
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        }
        return map;
    }

    /**
     * bean 转换成 Map,同时考虑 map 中 key 与 bead 的参数名不一样的情况
     *
     * @param bean   要转换的对象
     * @param keyMap 数据内容key和 bean 参数名不同时的映射关系 key 为原对象数据内容key; value 为对象的map的key
     * @return 最后的map对象
     */
    public static Map<String, Object> bean2Map(Object bean, Map<String, String> keyMap) {
        Map<String, Object> map = bean2Map(bean);
        for (Map.Entry<String, String> entry : keyMap.entrySet()) {
            String key = entry.getKey();
            if (map.containsKey(entry.getKey())) {
                Object value = map.remove(key);
                map.put(entry.getValue(), value);
            }
        }
        return map;
    }

    /**
     * String 转换成Map
     *
     * @param str
     * @return map
     */
    public static Map<String, String> stringToMap(String str) {
        String[] array = new String[1000];
        array = str.split(",");
        Map<String, String> map = new HashMap<String, String>();
        for (int i = 0; i < array.length; i++) {
            String[] data = array[i].split("=");
            map.put(data[0], data[1]);
        }
        return map;
    }

//	public static void main(String[] args){
//		String str="name=小米,phone=15006851271,idcardno=330681199311090022";
//		Map<String,String> map=stringToMap(str);
//		System.out.println(map.get("name"));
//		System.out.println(map.get("phone"));
//		System.out.println(map.get("idcardno"));
//	}
}
