package com.jandar.alipay.core.struct;

/**
 * 充值缴费信息
 * Created by yubj on 2016/4/18.
 */
public class PayFeeInfo {
    private String out_trade_no;// 订单支付时传入的商户订单号,和支付宝交易号不能同时为空。trade_no,out_trade_no如果同时存在优先取trade_no
    private String trade_no;// 支付宝交易号，和商户订单号不能同时为空

    public String getOut_trade_no() {
        return out_trade_no;
    }

    public void setOut_trade_no(String out_trade_no) {
        this.out_trade_no = out_trade_no;
    }

    public String getTrade_no() {
        return trade_no;
    }

    public void setTrade_no(String trade_no) {
        this.trade_no = trade_no;
    }
}
