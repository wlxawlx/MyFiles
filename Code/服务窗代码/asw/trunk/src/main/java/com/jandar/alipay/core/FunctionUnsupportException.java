package com.jandar.alipay.core;

/**
 * Created by zzw on 16/2/23.
 * 功能不支持的异常
 */
public class FunctionUnsupportException extends HospitalException {

    public FunctionUnsupportException(String msg) {
        super(msg, HospitalException.ERROR);
    }
}
