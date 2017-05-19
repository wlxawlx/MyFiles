package com.jandar.cloud.hospital.exception;

/**
 * 不支持的IM消息类型
 * Created by zzw on 16/9/8.
 */
public class NotSupportIMMessageTypeException extends Exception {
    public NotSupportIMMessageTypeException(String message) {
        super(message);
    }
}
