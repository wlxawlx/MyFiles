package com.jandar.filter.auth.util;

import com.jandar.alipay.core.struct.PlatformType;
import org.apache.commons.lang3.builder.ReflectionToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;

/**
 * Created by zhufengxiang on 2016/07/08.
 * 第三方用户信息
 * 暂且定这几个共有的, 如果有其他必须的再加。
 */
public class ThirdUserInfo {
    private String openId;
    private String name;             // 支付宝: 真实姓名或企业名称     微信: 用户昵称
    private int sex;                 // 1 男  2 女 0 未知
    private String avatar;
    private String certNo;           // 证件号码,   AlipayUserInfo的客户端已经有用到这个, 所以这边也加上, 微信那边是没有这个信息的。
    private PlatformType userType;   // 1. Alipay 2.Wechat

    private String code;//病人编号，云医院要用

    public String getOpenId() {
        return openId;
    }

    public void setOpenId(String openId) {
        this.openId = openId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getSex() {
        return sex;
    }

    public void setSex(int sex) {
        this.sex = sex;
    }

    public String getAvatar() {
        return avatar;
    }

    public void setAvatar(String avatar) {
        this.avatar = avatar;
    }

    public String getCertNo() {
        return certNo;
    }

    public void setCertNo(String certNo) {
        this.certNo = certNo;
    }

    public ThirdUserInfo() {
    }

    public PlatformType getUserType() {
        return userType;
    }

    public void setUserType(PlatformType userType) {
        this.userType = userType;
    }

    @Override
    public String toString() {
        return ReflectionToStringBuilder.toString(this, ToStringStyle.DEFAULT_STYLE);
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }


    }
