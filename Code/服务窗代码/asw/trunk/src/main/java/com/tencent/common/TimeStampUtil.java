package com.tencent.common;

import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by zhufengxiang on 2016/09/09.
 *
 */
public class TimeStampUtil {
    private static SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss SSS");
    public static String currentTimeStamp() {
        Date date = new Date();
        return sdf.format(date);
    }

    public static String currentSeconds() {
        return String.valueOf(System.currentTimeMillis() / 1000);
    }
    

}
