package com.jandar.util;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 做 bean 和 json 之间转换使用
 * 使用 GSON
 * Created by zzw on 16/8/29.
 */
public class GsonUtil {

    private final static String datePattern = "yyyy-MM-dd HH:mm:ss";

    private static Gson buildJsonBuilder() {
        Gson gson = new GsonBuilder()
//                .excludeFieldsWithoutExposeAnnotation() //不导出实体中没有用@Expose注解的属性
                .enableComplexMapKeySerialization() //支持Map的key为复杂对象的形式
                .setDateFormat(datePattern)//时间转化为特定格式
//                .setFieldNamingPolicy(FieldNamingPolicy.UPPER_CAMEL_CASE)//会把字段首字母大写,注:对于实体上使用了@SerializedName注解的不会生效.
//                .setPrettyPrinting() //对json结果格式化.
//                .setVersion(1.0)
                .disableHtmlEscaping() //默认是GSON把HTML 转义的，但也可以设置不转义
//                .serializeNulls()//把null值也转换，默认是不转换null值的，可以选择也转换,为空时输出为{a:null}，而不是{}
                .create();
        return gson;
    }

    public static String toJson(Object bean) {
        Gson gson = buildJsonBuilder();
        return gson.toJson(bean);
    }

    public static <T> T toBean(Map<String, Object> dataMap, Class<?> clazz) {
        Gson gson = buildJsonBuilder();
        String json = gson.toJson(dataMap);
        return (T) gson.fromJson(json, clazz);
    }

    public static <T> T toBean(String jsonStr, Class<?> clazz) {
        Gson gson = buildJsonBuilder();
        return (T) gson.fromJson(jsonStr, clazz);
    }

    public static Map<String, Object> toMap(String jsonStr) {
        Map<String, Object> map = new HashMap<String, Object>();
        return toBean(jsonStr, map.getClass());
    }

    public static Map<String, Object> toMap(Object bean) {
        Gson gson = buildJsonBuilder();
        String jsonStr = gson.toJson(bean);
        Map<String, Object> map = new HashMap<String, Object>();
        return gson.fromJson(jsonStr, map.getClass());
    }

    public static List<Map<String, Object>> toListMap(String jsonStr) {
        List<Map<String, Object>> listMap = new ArrayList<Map<String, Object>>();
        return toBean(jsonStr, listMap.getClass());
    }

    public static List<Map<String, Object>> toListMap(Object bean) {
        Gson gson = buildJsonBuilder();
        String jsonStr = gson.toJson(bean);
        List<Map<String, Object>> listMap = new ArrayList<Map<String, Object>>();
        return gson.fromJson(jsonStr, listMap.getClass());
    }

    public static <T> List<T> toList(String jsonStr) {
        Gson gson = buildJsonBuilder();
        List<T> list = new ArrayList<T>();
        return gson.fromJson(jsonStr, list.getClass());
    }

}
