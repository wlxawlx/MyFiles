package com.jandar.util;

import net.sf.json.JsonConfig;
import net.sf.json.processors.JsonValueProcessor;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.Locale;

/**
 * Created by zzw on 16/8/29.
 */
public class JsonDateValueProcessor implements JsonValueProcessor {
    private String datePattern = "yyyy-MM-dd HH:mm:ss";

    public JsonDateValueProcessor() {
    }

    public JsonDateValueProcessor(String format) {
        this.datePattern = format;
    }

    public Object processArrayValue(Object value, JsonConfig jsonConfig) {
        return this.process(value);
    }

    public Object processObjectValue(String key, Object value, JsonConfig jsonConfig) {
        return this.process(value);
    }

    private Object process(Object value) {
        try {
            if (value instanceof Date) {
                SimpleDateFormat e = new SimpleDateFormat(this.datePattern, Locale.CHINESE);
                return e.format((Date) value);
            } else {
                return value == null ? "" : value.toString();
            }
        } catch (Exception var3) {
            return "";
        }
    }

    public String getDatePattern() {
        return this.datePattern;
    }

    public void setDatePattern(String pDatePattern) {
        this.datePattern = pDatePattern;
    }
}
