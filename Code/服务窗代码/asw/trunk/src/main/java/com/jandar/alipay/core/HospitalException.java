package com.jandar.alipay.core;

/**
 * 医院通讯操作异常
 */
public class HospitalException extends Exception {

    static public String SUCCESS = "00"; //	成功
    static public String ARG_ERROR = "01"; //	请求参数错误
    static public String UNBIND_CARD = "02"; //	用户还未绑卡
    static public String UN_USERINFO = "03"; //	用户取消了授权或是没有获得支付宝用户信息，要求用户刷新界面
    static public String UNARCHIV = "98"; //	用户还未建档
    static public String ERROR = "99"; //	失败
    static public String SYSTEM_ERROR = "97"; // 系统错误,如配置错误,网络错误等

    private static final long serialVersionUID = 1L;
    // 添加错误代码
    private String code;

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public HospitalException() {
        super();
    }

    public HospitalException(String msg) {
        super(msg);
        this.code = ERROR;
    }

    public HospitalException(String message, String code) {
        super(message);
        this.code = code;
    }
}
