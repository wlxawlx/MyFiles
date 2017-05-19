package com.alipay.payment.wap;

import com.alipay.api.internal.util.StringUtils;
import com.alipay.config.AlipayConfig;
import com.alipay.util.AlipaySubmit;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.config.ConfigHandler;

import javax.servlet.ServletException;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by zzw on 15/12/29.
 * 支付宝 wap 支付接口
 */
public class AlipayWapPayment {

    /**
     * 生成一个支付请求地址
     *
     * @param out_trade_no 商户订单号 商户网站订单系统中唯一订单号，必填
     * @param subject      订单名称 必填
     * @param total_fee    付款金额 必填 单位元
     * @param show_url     商品展示地址 选填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
     * @param success_url  支付完成了跳转的地址 选填,需以http://开头的完整路径
     * @param body         订单描述  选填
     * @param it_b_pay     超时时间  选填
     * @param extern_token 钱包token 选填
     * @return 一个支付请求用的URL
     */
    public static String buildWapPaymentUrl(String notifyUrl,
                                            String out_trade_no,
                                            String subject,
                                            String total_fee,
                                            String show_url,
                                            String success_url,
                                            String body,
                                            String it_b_pay,
                                            String extern_token) {
        ////////////////////////////////////请求参数//////////////////////////////////////

        //支付类型
        String payment_type = "1";
        //必填，不能修改
        //服务器异步通知页面路径
        String notify_url = notifyUrl;

        //需http://格式的完整路径，不能加?id=123这类自定义参数

        //页面跳转同步通知页面路径
        String return_url = success_url;// "http://" + ConfigHandler.getSelfServiceHost() + "/return_url.jsp";
        //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
        if (!StringUtils.isEmpty(return_url) && !return_url.startsWith("http")) {
            return_url += "http://" + ConfigHandler.getSelfServiceHost() + "/" + return_url;
        }

        //把请求参数打包成数组
        Map<String, String> sParaTemp = new HashMap<String, String>();
        sParaTemp.put("service", "alipay.wap.create.direct.pay.by.user");
        sParaTemp.put("partner", AlipayConfig.partner);
        sParaTemp.put("seller_id", AlipayConfig.seller_id);
        sParaTemp.put("_input_charset", AlipayConfig.input_charset);
        sParaTemp.put("payment_type", payment_type);
        sParaTemp.put("notify_url", notify_url);
        sParaTemp.put("return_url", return_url);
        sParaTemp.put("out_trade_no", out_trade_no);
        sParaTemp.put("subject", subject);
        sParaTemp.put("total_fee", total_fee);

//        if (!StringUtils.isEmpty(show_url) && !show_url.startsWith("http")) {
//            show_url += "http://" + ConfigHandler.getSelfServiceHost() + "/" + show_url;
//        }

        sParaTemp.put("show_url", show_url);
        sParaTemp.put("body", body);
        sParaTemp.put("it_b_pay", it_b_pay);
        sParaTemp.put("extern_token", extern_token);

        //建立请求
        String sUrl = AlipaySubmit.buildRequest(sParaTemp);
        return sUrl;
    }

    /**
     * 生成一个支付请求地址（云医院）
     *
     * @param out_trade_no 商户订单号 商户网站订单系统中唯一订单号，必填
     * @param subject      订单名称 必填
     * @param total_fee    付款金额 必填 单位元
     * @param show_url     商品展示地址 选填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
     * @param success_url  支付完成了跳转的地址 选填,需以http://开头的完整路径
     * @param body         订单描述  选填
     * @param it_b_pay     超时时间  选填
     * @param extern_token 钱包token 选填
     * @param jz_id 就诊id 必填
     * @param shdz_id 收货地址id 必填
     * @return 一个支付请求用的URL
     */
    public static String buildWapPaymentUrl(String notifyUrl,
                                            String out_trade_no,
                                            String subject,
                                            String total_fee,
                                            String show_url,
                                            String success_url,
                                            String body,
                                            String it_b_pay,
                                            String extern_token,
                                            String jz_id,
                                            String shdz_id,
                                            String qjfs) {
        ////////////////////////////////////请求参数//////////////////////////////////////

        //支付类型
        String payment_type = "1";
        //必填，不能修改
        //服务器异步通知页面路径
        String notify_url = notifyUrl;

        //需http://格式的完整路径，不能加?id=123这类自定义参数

        //页面跳转同步通知页面路径
        String return_url = success_url;// "http://" + ConfigHandler.getSelfServiceHost() + "/return_url.jsp";
        //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
        if (!StringUtils.isEmpty(return_url) && !return_url.startsWith("http")) {
            return_url += "http://" + ConfigHandler.getSelfServiceHost() + "/" + return_url;
        }

        //把请求参数打包成数组
        Map<String, String> sParaTemp = new HashMap<String, String>();
        sParaTemp.put("service", "alipay.wap.create.direct.pay.by.user");
        sParaTemp.put("partner", AlipayConfig.partner);
        sParaTemp.put("seller_id", AlipayConfig.seller_id);
        sParaTemp.put("_input_charset", AlipayConfig.input_charset);
        sParaTemp.put("payment_type", payment_type);
        sParaTemp.put("notify_url", notify_url);
        sParaTemp.put("return_url", return_url);
        sParaTemp.put("out_trade_no", out_trade_no);
        sParaTemp.put("subject", subject);
        sParaTemp.put("total_fee", total_fee);

//        if (!StringUtils.isEmpty(show_url) && !show_url.startsWith("http")) {
//            show_url += "http://" + ConfigHandler.getSelfServiceHost() + "/" + show_url;
//        }

        sParaTemp.put("show_url", show_url);
        sParaTemp.put("body", body);
        sParaTemp.put("it_b_pay", it_b_pay);
        sParaTemp.put("extern_token", extern_token);
        sParaTemp.put("jz_id", jz_id);
        sParaTemp.put("shdz_id", shdz_id);
        sParaTemp.put("qjfs", qjfs);
        System.out.println("notify_url:"+notify_url);
        //建立请求
        String sUrl = AlipaySubmit.buildRequest(sParaTemp);
        return sUrl;
    }
    /**
     * 生成一个退费请求地址（云医院）
     *


     * @param total_fee    付款金额 必填 单位元

     * @return 一个支付请求用的URL
     */
    public static String buildWapRefundUrl(String notifyUrl,
                                            String trade_no,
                                            String total_fee,
                                            String reason,
                                            String batch_no
                                            ) {
        ////////////////////////////////////请求参数//////////////////////////////////////
        Date currentTime = new Date();
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String dateString = formatter.format(currentTime);
        //必填，不能修改
        //服务器异步通知页面路径
        String notify_url = notifyUrl;

        //把请求参数打包成数组
        Map<String, String> sParaTemp = new HashMap<String, String>();
        sParaTemp.put("service", "refund_fastpay_by_platform_nopwd");
        sParaTemp.put("partner", AlipayConfig.partner);
        sParaTemp.put("_input_charset", AlipayConfig.input_charset);
        sParaTemp.put("notify_url", notify_url);
        sParaTemp.put("seller_user_id", AlipayConfig.seller_id);
        sParaTemp.put("refund_date", dateString);
        sParaTemp.put("batch_no", batch_no);
        sParaTemp.put("batch_num", "1");

//        if (!StringUtils.isEmpty(show_url) && !show_url.startsWith("http")) {
//            show_url += "http://" + ConfigHandler.getSelfServiceHost() + "/" + show_url;
//        }

        sParaTemp.put("detail_data", trade_no+"^"+total_fee+"^"+"tf");

        //建立请求
        String sUrl = "";
        try {
            sUrl = AlipaySubmit.buildRequest("","",sParaTemp);
            System.out.println(sUrl);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return sUrl;
    }
    /**
     * 生成一个支付请求地址
     *
     * @param out_trade_no 商户订单号 商户网站订单系统中唯一订单号，必填
     * @param subject      订单名称 必填
     * @param total_fee    付款金额 必填 单位元
     * @param show_url     商品展示地址 选填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
     * @param success_url  支付完成了跳转的地址 选填,需以http://开头的完整路径
     * @param body         订单描述  选填
     * @param it_b_pay     超时时间  选填
     * @param extern_token 钱包token 选填
     * @return 一个支付请求用的URL
     */
    public static String getWapPaymentUrl(String out_trade_no,
                                          String subject,
                                          String total_fee,
                                          String show_url,
                                          String success_url,
                                          String body,
                                          String it_b_pay,
                                          String extern_token) {
        //必填，不能修改
        //服务器异步通知页面路径
        String notify_url = "http://" + ConfigHandler.getSelfServiceHost() + "/alipay_notify_url.jsp";
        notify_url += "?jandar_ordertype=" + RechargeOrderType.CloudHospitalOrder.toString();

        //需http://格式的完整路径，不能加?id=123这类自定义参数
        return buildWapPaymentUrl(notify_url, out_trade_no, subject, total_fee, show_url, success_url, body, it_b_pay, extern_token);
    }

    /**
     * 生成一个支付请求地址
     *
     * @param orderType    订单类型
     * @param user_id      用户ID,用与表示是哪个用户建立的订单
     * @param beneficiary  受益人，是给谁交的钱
     * @param out_trade_no 商户订单号 商户网站订单系统中唯一订单号，必填
     * @param subject      订单名称 必填
     * @param total_fee    付款金额 必填 单位元
     * @param show_url     商品展示地址 选填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
     * @param success_url  支付完成了跳转的地址 选填,需以http://开头的完整路径
     * @param body         订单描述  选填
     * @param it_b_pay     超时时间  选填
     * @param extern_token 钱包token 选填
     * @param obligate     用户预备信息,用于用户需要回传的信息内容,如病人ID
     *                     请自己事先编码,不可以包含 = ? & “ ‘
     * @return 一个支付请求用的URL
     * @throws ServletException
     * @throws IOException
     */
    public static String getWapPaymentUrl(RechargeOrderType orderType,
                                          String user_id,
                                          String beneficiary,
                                          String out_trade_no,
                                          String subject,
                                          String total_fee,
                                          String show_url,
                                          String success_url,
                                          String body,
                                          String it_b_pay,
                                          String extern_token,
                                          String obligate) {
        //服务器异步通知页面路径
        String notify_url = "http://" + ConfigHandler.getSelfServiceHost() + "/alipay_notify_url.jsp" +
                "?call_back_user_id=" + user_id +
                "&call_back_beneficiary=" + beneficiary;

        notify_url += "&jandar_ordertype=" + orderType.toString();

        if (!StringUtils.isEmpty(obligate)) {
            notify_url += "&jandar_obligate=" + obligate;
        }

        //需http://格式的完整路径，不能加?id=123这类自定义参数

        return buildWapPaymentUrl(notify_url, out_trade_no, subject, total_fee, show_url, success_url, body, it_b_pay, extern_token);
    }

    /**
     * 生成一个支付请求地址（云医院）
     *
     * @param orderType    订单类型
     * @param user_id      用户ID,用与表示是哪个用户建立的订单
     * @param beneficiary  受益人，是给谁交的钱
     * @param out_trade_no 商户订单号 商户网站订单系统中唯一订单号，必填
     * @param subject      订单名称 必填
     * @param total_fee    付款金额 必填 单位元
     * @param show_url     商品展示地址 选填，需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
     * @param success_url  支付完成了跳转的地址 选填,需以http://开头的完整路径
     * @param body         订单描述  选填
     * @param it_b_pay     超时时间  选填
     * @param extern_token 钱包token 选填
     * @param obligate     用户预备信息,用于用户需要回传的信息内容,如病人ID
     *                     请自己事先编码,不可以包含 = ? & “ ‘
     * @param jz_id 就诊id 必填
     * @param shdz_id 收货地址id 必填
     * @return 一个支付请求用的URL
     * @throws ServletException
     * @throws IOException
     */
    public static String getWapPaymentUrl(RechargeOrderType orderType,
                                          String user_id,
                                          String beneficiary,
                                          String out_trade_no,
                                          String subject,
                                          String total_fee,
                                          String show_url,
                                          String success_url,
                                          String body,
                                          String it_b_pay,
                                          String extern_token,
                                          String obligate,
                                          String jz_id,
                                          String shdz_id,
                                          String qjfs) {
        //服务器异步通知页面路径
        String notify_url = "http://" + ConfigHandler.getSelfServiceHost() +"/alipay_notify_url.jsp" +
                "?call_back_user_id=" + user_id +
                "&call_back_beneficiary=" + beneficiary;

        notify_url += "&jandar_ordertype=" + orderType.toString();

        if (!StringUtils.isEmpty(obligate)) {
            notify_url += "&jandar_obligate=" + obligate;
        }

        //需http://格式的完整路径，不能加?id=123这类自定义参数

        return buildWapPaymentUrl(notify_url, out_trade_no, subject, total_fee, show_url, success_url, body, it_b_pay, extern_token,jz_id,shdz_id,qjfs);
    }
    /**
     * 生成一个退费请求地址（云医院）
     *

     * @return 一个退费请求用的URL
     * @throws ServletException
     * @throws IOException
     */
    public static String getWapRefundUrl(
                                          String openId,
                                          String trade_no,
                                          String total_fee,
                                          String reason,
                                          String batch_no)  {
        //服务器异步通知页面路径

        String notify_url = "http://" + ConfigHandler.getSelfServiceHost() +"/alipay_refund_notify_url.jsp" +
                "?call_back_user_id=" + openId;


        System.out.println("getWapRefundUrl::notify_url:"+notify_url);
        //需http://格式的完整路径，不能加?id=123这类自定义参数

        return buildWapRefundUrl(notify_url,trade_no,total_fee,reason,batch_no);
    }
}
