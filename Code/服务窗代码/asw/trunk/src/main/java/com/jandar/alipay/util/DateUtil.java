package com.jandar.alipay.util;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

/**
 * Created by zhufengxiang on 2015-12-30.
 *
 * 日期工具类
 */
public class DateUtil {
    public static final DateFormat dateFormat= new SimpleDateFormat("yyyy-MM-dd");
    public static Calendar calendar = Calendar.getInstance();

    public static String translateDate2Week(String datestring) {
        String week = "周";
        try {
            Date date = dateFormat.parse(datestring);
            calendar.setTime(date);
            int i = calendar.get(Calendar.DAY_OF_WEEK);
            switch (i) {
                case 1:
                    week += "日";
                    break;
                case 2:
                    week += "一";
                    break;
                case 3:
                    week += "二";
                    break;
                case 4:
                    week += "三";
                    break;
                case 5:
                    week += "四";
                    break;
                case 6:
                    week += "五";
                    break;
                default:
                    week += "六";
                    break;
            }
            return week;
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return week;
    }

    public static String dateFormat(String date, String oldFormat, String newFormat) {
        SimpleDateFormat sdf1 = new SimpleDateFormat(oldFormat);
        SimpleDateFormat sdf2 = new SimpleDateFormat(newFormat);
        Date time = null;
        try {
            time = sdf1.parse(date);
            return sdf2.format(time);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }





    public static void main(String[] args) {
        String today = "2015-12-30 12:30:00";
        System.out.println(translateDate2Week(today));
        String nowday = "2015-12-30";
        System.out.println(dateFormat(today,"yyyy-MM-dd HH:mm:ss","yyyy-MM-dd HH:mm"));
        System.out.println(dateFormat(today,"yyyy-MM-dd HH:mm:ss","HH:mm"));
        System.out.println(dateFormat(nowday,"yyyy-MM-dd","MM-dd"));
    }
}
