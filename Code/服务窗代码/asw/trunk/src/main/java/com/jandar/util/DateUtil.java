package com.jandar.util;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class DateUtil {
    /**
     * 获取当前日期时间
     *
     * @return
     */
    public static Date getCurrentDate() {
        Calendar cal = Calendar.getInstance();
        return cal.getTime();
    }

    /**
     * 获取当前时间
     *
     * @param format
     * @return
     */
    public static String getCurrentDate(String format) {
        if (format == null || format.equals("")) {
            return "";
        }
        SimpleDateFormat sdt = new SimpleDateFormat(format);
        return sdt.format(new Date());
    }

    /**
     * 字符串转换日期格式
     *
     * @param sDate
     * @return
     */
    public static Date formatDateWithDate(String sDate, String format) {

        if (sDate == null || sDate.equals(""))
            return null;
        SimpleDateFormat sdt = new SimpleDateFormat(format);
        Date date = new Date();
        try {
            date = sdt.parse(sDate);
        } catch (ParseException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return date;
    }

    /**
     * 时间转换为字符串格式
     *
     * @param date
     * @param format
     * @return
     */
    public static String formatDateWithString(Date date, String format) {
        SimpleDateFormat sdt = new SimpleDateFormat(format);
        return sdt.format(date);
    }

    /**
     * 获取当前月的最后一天
     *
     * @param format
     * @return
     */
    public static String getLastDayWithMonth(String format) {
        Calendar cal = Calendar.getInstance();
        cal.set(Calendar.DATE, 1);
        cal.roll(Calendar.DATE, -1);
        SimpleDateFormat sdt = new SimpleDateFormat("yyyy-MM-dd");
        return sdt.format(cal.getTime()) + " 23:59:59";
    }

    /**
     * 获取当前月的第一天
     *
     * @param format
     * @return
     */
    public static String getFirstDayWithMonth() {
        Calendar cal = Calendar.getInstance();
        cal.set(Calendar.DATE, 1);
        SimpleDateFormat sdt = new SimpleDateFormat("yyyy-MM-dd");
        return sdt.format(cal.getTime()) + " 00:00:00";
    }

    /**
     * 增加天数  正数表示添加，负数表示减去天数
     *
     * @param date
     * @param iDays
     * @return
     */
    public static String addDayWithString(Date date, int iDays, String format) {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.DATE, iDays);
        SimpleDateFormat sdt = new SimpleDateFormat(format);
        return sdt.format(cal.getTime());
    }

    public static Date addDayWithDate(Date date, int iDays) {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.DATE, iDays);
        return cal.getTime();
    }

    public static String addMinuteWithString(Date date, int minutes, String format) {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.MINUTE, minutes);
        SimpleDateFormat sdt = new SimpleDateFormat(format);
        return sdt.format(cal.getTime());
    }

    public static Date addMinuteWithDate(Date date, int minutes) {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.MINUTE, minutes);
        return cal.getTime();
    }

    public static int dayForWeek(String pTime) {
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        Calendar c = Calendar.getInstance();
        try {
            c.setTime(format.parse(pTime));
        } catch (ParseException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        int dayForWeek = 0;
        if (c.get(Calendar.DAY_OF_WEEK) == 1) {
            dayForWeek = 7;
        } else {
            dayForWeek = c.get(Calendar.DAY_OF_WEEK) - 1;
        }
        return dayForWeek;
    }

    /**
     * 比较天数
     *
     * @param fDate
     * @param oDate
     * @return
     */
    public static int getIntervalDays(Date fDate, Date oDate) {
        if (null == fDate || null == oDate) {
            return -1;
        }
        long intervalMilli = oDate.getTime() - fDate.getTime();
        return (int) (intervalMilli / (24 * 60 * 60 * 1000));
    }

    /**
     * 比较小时
     *
     * @param fDate
     * @param oDate
     * @return
     */
    public static int getIntervalHour(Date fDate, Date oDate) {
        if (null == fDate || null == oDate) {
            return -1;
        }
        long intervalMilli = oDate.getTime() - fDate.getTime();
        return (int) (intervalMilli / (60 * 60 * 1000));
    }

    /**
     * 格式化时间
     * */
    public static String formatTime(Date tDate) {

        if(tDate==null)
            return "";
        java.text.DateFormat df = new java.text.SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
        String time = df.format(tDate);
        return time;
    }

    /**
     * 格式化时间
     * */
    public static String formatTime1(Date tDate) {

        if(tDate==null)
            return "";
        java.text.DateFormat df = new java.text.SimpleDateFormat("yyyy/MM/dd");
        String time = df.format(tDate);
        return time;
    }



}
