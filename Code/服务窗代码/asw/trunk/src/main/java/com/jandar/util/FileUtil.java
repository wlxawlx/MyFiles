package com.jandar.util;

/**
 * 文件处理类
 * Created by zzw on 16/6/17.
 */
public class FileUtil {

    public static String getFileSuffix(String filename) {
        String suffix = null;
        // 获取图片后缀
        if (filename.contains(".")) {
            suffix = filename.substring(filename.lastIndexOf(".") + 1);
        }
        return suffix;
    }
}
