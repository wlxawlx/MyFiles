package com.jandar.cloud.hospital.util;


import com.jandar.util.SpringBeanUtil;
import org.springframework.beans.factory.annotation.Autowired;

import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by lyx on 2016/9/30.
 */
public class SerialNumberUtil {

//    private static SerialNumberDao serialNumberDao = SpringBeanUtil.getBean(SerialNumberDao.class);

    public static String serialNumber(Integer i) {
        Date date = new Date();
        SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMddHHmmssSSS");
        String firstPart = null;
        String dateString = formatter.format(date);
        /**
         * 输入参数i==1 表示处方单流水号
         * 输入参数i==2 表示检查单流水号
         * 输入参数i==3 表示住院单流水号
         * 输入参数i==4 表示综合预约单流水号
         * 输入参数i==5 表示专科预约单流水号
         */
        if (i == 1) firstPart = "CFD";
        if (i == 2) firstPart = "JCD";
        if (i == 3) firstPart = "ZYD";
        if (i == 4) firstPart = "ZHD";
        if (i == 5) firstPart = "ZKD";
        else
            return "输出的流水号不合法";
        return firstPart + dateString;
    }

    public static Integer paresType(String serial) {

        String paresType = serial.substring(0, 3);
        if ("CFD".equals(paresType)) return 1;
        else if ("JCD".equals(paresType)) return 2;
        else if ("ZYD".equals(paresType)) return 3;
        else if ("ZHD".equals(paresType)) return 4;
        else if ("ZKD".equals(paresType)) return 5;
        else  return 0;

    }
}
