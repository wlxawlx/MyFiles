package com.alipay.servlet.payment;

import com.alipay.util.AlipayNotify;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.cloud.hospital.handle.NotifyHandle;
import org.apache.log4j.Logger;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * Created by zzw on 15/12/29.
 * 支付宝异步通知
 */
public class AlipayNotifyServlet extends HttpServlet {

    static Logger logger = Logger.getLogger(AlipayNotifyServlet.class);

    /**
     * 线程池
     */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /**
     * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse
     * response)
     */
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        this.doPost(request, response);
    }

    /**
     * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse
     * response)
     */
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        request.setCharacterEncoding("UTF-8");
        response.setCharacterEncoding("UTF-8");
        response.setContentType("text/html; charset=UTF-8");

        //获取支付宝POST过来反馈信息
        Map<String, String> params = new HashMap<String, String>();
        Map requestParams = request.getParameterMap();
        for (Iterator iter = requestParams.keySet().iterator(); iter.hasNext(); ) {
            String name = (String) iter.next();
            String[] values = (String[]) requestParams.get(name);
            String valueStr = "";
            for (int i = 0; i < values.length; i++) {
                valueStr = (i == values.length - 1) ? valueStr + values[i]
                        : valueStr + values[i] + ",";
            }
            //乱码解决，这段代码在出现乱码时使用。如果mysign和sign不相等也可以使用这段代码转化
            //valueStr = new String(valueStr.getBytes("ISO-8859-1"), "gbk");
            params.put(name, valueStr);
        }
        logger.info("支付宝异步通知内容:" + params);

        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表(以下仅供参考)//
        //商户订单号
        final String out_trade_no = new String(request.getParameter("out_trade_no").getBytes("ISO-8859-1"), "UTF-8");

        //支付宝交易号
        String trade_no = new String(request.getParameter("trade_no").getBytes("ISO-8859-1"), "UTF-8");

        //交易状态
        String trade_status = new String(request.getParameter("trade_status").getBytes("ISO-8859-1"), "UTF-8");

        // 金额
        final String total_fee = request.getParameter("total_fee");

        // 时间
        final String gmt_payment = params.get("gmt_payment");

        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表(以上仅供参考)//

        PrintWriter out = response.getWriter();
        /** 创建订单时传出的信息,在这儿要使用 */
        final String user_id = params.get("call_back_user_id");
        //final String user_id ="20881044371590442036112991213576";
        params.remove("call_back_user_id");
        final String beneficiary = params.get("call_back_beneficiary");
        params.remove("call_back_beneficiary");

        /** 订单类型 */
        String orderTypeStr = params.remove("jandar_ordertype");
        final RechargeOrderType orderType = orderTypeStr != null ? RechargeOrderType.valueOf(orderTypeStr) : null;
//        if (orderType == null) {
//            logger.error("未知的订单支付类型");
//            out.println("fail");
//            out.flush();
//            out.close();
//            return;
//        }

        // 用户预留数据
        final String obligate = params.get("jandar_obligate");
        params.remove("jandar_obligate");

        if (AlipayNotify.verify(params)) {//验证成功
            logger.info("签名验证成功");
            //////////////////////////////////////////////////////////////////////////////////////////
            //请在这里加上商户的业务逻辑程序代码

            //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——

            if (trade_status.equals("TRADE_FINISHED")) {
                //判断该笔订单是否在商户网站中已经做过处理
                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
                //如果有做过处理，不执行商户的业务程序

                //注意：
                //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
            } else if (trade_status.equals("TRADE_SUCCESS")) {
                //判断该笔订单是否在商户网站中已经做过处理
                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
                //如果有做过处理，不执行商户的业务程序

                //注意：
                if (orderType == RechargeOrderType.CloudHospitalOrder) {
                    boolean success = NotifyHandle.handlePayNotify(out_trade_no, trade_no, total_fee, gmt_payment, PlatformType.Alipay);
                    if (!success) {
                        out.println("fail");
                        out.flush();
                        out.close();
                        return;
                    }
                } else {
                    //付款完成后，支付宝系统发送该交易状态通知
                    try {
                        HospitalInfoService.getInstance().paymentArrivalNotify(orderType, user_id, beneficiary, out_trade_no,
                                trade_no, total_fee, obligate, params.toString());
                    } catch (HospitalException e) {
                        e.printStackTrace();
                        out.println("fail");
                        out.flush();
                        out.close();
                        return;
                    }
                }
            }

            if (orderType == RechargeOrderType.CloudHospitalOrder) {
                NotifyHandle.sendPayNotifyToUser(out_trade_no, trade_no, total_fee, gmt_payment, PlatformType.Alipay);
            } else {
                //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                executors.execute(new Runnable() {
                    @Override
                    public void run() {
                        HospitalInfoService.getInstance().paymentPlatformNotifyMessage(orderType, user_id, beneficiary, total_fee, out_trade_no, gmt_payment);
                    }
                });
            }
            out.println("success");    //请不要修改或删除

            //////////////////////////////////////////////////////////////////////////////////////////
        } else {//验证失败
            logger.info("签名验证失败");
            out.println("fail");
        }
        out.flush();
        out.close();
    }
}
