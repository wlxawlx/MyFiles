using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Model
{
    //用户信息
    public class UserInfo
    {
        //支付宝USERID或微信OPENID 用户标识
        public string openid;
        //姓名
        public string name;
        //手机号码
        public string phone;
        //身份证号
        public string idcardno;
        //地址
        public string address;
        //用户头像 直接指向支付平台用户头像地址
        public string headurl;
        //就诊卡号
        public string cardno;
        //病人ID
        public string patientid;
        //用户类型 1：支付宝用户，2：微信用户
        public string usertype;
        //联系人标签 对联系人的描述
        public string label;
        //联系人ID，后面对操作人操作的标识
        public string linkmanid;
        //0：未绑卡；1：已绑卡
        public string bindcardfalg;
        //0：无住院信息；1：有住院信息
        public string inpatentflag;
    }

    //用户缴费信息
    public class UserJFInfo
    {
        //卡类型
        public string cardtype;
        //卡号
        public string cardno;
        //病人姓名
        public string brxm;
        //身份证号码
        public string sfzh;
        //联系电话
        public string lxdh;
        //账户状态
        public string zhanghuzt;
    }
}