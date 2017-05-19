package com.jandar.util;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;
import net.sf.json.JsonConfig;

import java.sql.Date;
import java.sql.Timestamp;
import java.util.List;
import java.util.Map;

/**
 * 使用 json-lib
 * Created by zzw on 16/8/29.
 */
public class JsonUtil {

    private static JsonConfig buildJsonConfig() {
        JsonConfig jsonConfig = new JsonConfig();
        jsonConfig.registerJsonValueProcessor(Timestamp.class, new JsonDateValueProcessor());
        jsonConfig.registerJsonValueProcessor(Date.class, new JsonDateValueProcessor());
        return jsonConfig;
    }

    public static String toJson(Object messages) {
        JSONObject json = JSONObject.fromObject(messages, buildJsonConfig());
        return json.toString();
    }

    public static Map<String, Object> getMapWithJson(Object messages) {
        return (Map) JSONObject.toBean(JSONObject.fromObject(messages, buildJsonConfig()), Map.class);
    }

    public static List<Map<String, Object>> getListWithJson(JSONArray jsonArray) {
        return JSONArray.toList(jsonArray, buildJsonConfig());
    }

    public static Map<String, Object> toMap(String messages) {
        return (Map) JSONObject.toBean(JSONObject.fromObject(messages, buildJsonConfig()), Map.class);
    }

    public static Map<String, String> getMapWithJsonString(String messages) {
        return (Map) JSONObject.toBean(JSONObject.fromObject(messages, buildJsonConfig()), Map.class);
    }

    public static String getJsonWithList(List messages) {
        JSONArray json = JSONArray.fromObject(messages, buildJsonConfig());
        return json.toString();
    }

    public static List<Map<String, Object>> getListWithJson(Object messages) {
        return JSONArray.toList(JSONArray.fromObject(messages, buildJsonConfig()), Map.class);
    }

    public static <T> List<T> getListWithJson(String messages, Class<?> clazz) {
        return JSONArray.toList(JSONArray.fromObject(messages, buildJsonConfig()), clazz);
    }

    public static <T> T toBean(Class<?> clazz, Map<String, Object> dataMap) {
        return (T) JSONObject.toBean(JSONObject.fromObject(dataMap, buildJsonConfig()), clazz);
    }
}
