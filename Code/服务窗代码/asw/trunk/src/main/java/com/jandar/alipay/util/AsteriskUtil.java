package com.jandar.alipay.util;


import org.apache.commons.lang.StringUtils;

public class AsteriskUtil {
//    身份证： 前5和后2位 显示，其它*
//    手机号：3 位 + **** + 后 4 位
//    姓名：隐藏第一个字

    public static String formatId(String id) {
         return replaceAsterisk(id, 5, 2);
    }

    public static String formatPhone(String phone) {
        return replaceAsterisk(phone, 3, 4);
    }

    public static String formatName(String name) {
        return replaceAsterisk(name, 0, name.length()-1);
    }

    private static String replaceAsterisk(String source, int front, int behind){

        if(StringUtils.isBlank(source)) {
            return "";
        }
        return source;
//        if(source.length() < front + behind) {
//            return source;
//        }
//        String s1 = source.substring(0, front);
//        String s2 = source.substring(source.length()-behind, source.length());
//        int lengthOfAsterisk = source.length() - front - behind;
//        String asterisks = "";
//        for(int i=0; i<lengthOfAsterisk; i++) {
//            asterisks += "*";
//        }
//        return s1 + asterisks + s2;
    }


    public static void main(String[] args) {
        String id = "330622199912094321";
        System.out.println(replaceAsterisk(id, 5, 2));

        String phone = "18657888609";
        System.out.println(replaceAsterisk(phone, 3, 4));

        String name = "诸凤祥";
        System.out.println(replaceAsterisk(name, 0, name.length()-1));
    }
}
