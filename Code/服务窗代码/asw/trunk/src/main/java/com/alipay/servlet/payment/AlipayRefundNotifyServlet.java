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
 * Created by ysc on 17/01/09.
 * 支付宝异步通知
 */
public class AlipayRefundNotifyServlet extends HttpServlet {

    static Logger logger = Logger.getLogger(AlipayRefundNotifyServlet.class);

    /**
     * 线程池
     */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /**
     * @see javax.servlet.http.HttpServlet#doGet(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse
     * response)
     */
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        this.doPost(request, response);
    }

    /**
     * @see javax.servlet.http.HttpServlet#doPost(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse
     * response)
     */
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        request.setCharacterEncoding("UTF-8");
        response.setCharacterEncoding("UTF-8");
        response.setContentType("text/html; charset=UTF-8");

        //获取支付宝POST过来反馈信息
        Map<String, String> params = new HashMap<String, String>();
        Map<String, Object> parameters = new HashMap<String, Object>();
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
        String resultDetails=params.get("result_details");
        logger.info("result_details:" + resultDetails);
        String[] resultDetailsArray=resultDetails.split("\\^");
        String trade_status=resultDetailsArray[2];
        logger.info("trade_status:" + trade_status);
        parameters.put("resultdetails",params.get("result_details"));
        parameters.put("notifytime",params.get("notify_time"));
        parameters.put("batchno",params.get("batch_no"));
        parameters.put("successnum",params.get("success_num"));
        final String openId = params.get("call_back_user_id");
        //final String openId = "20881044371590442036112991213576";
        params.remove("call_back_user_id");
        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表(以下仅供参考)//
        //商户订单号
        logger.info("支付宝异步通知内容剔除自定义参数:" + params);

        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表(以上仅供参考)//
        logger.info("start verify ...");
        PrintWriter out = response.getWriter();
        if (AlipayNotify.verify(params)) {//验证成功
            logger.info("签名验证成功");
            String tradeResult="fail";
            if ("SUCCESS".equals(trade_status)) {
                tradeResult="success";
                parameters.put("status","2");
            }else{
                logger.info("支付宝退款失败，错误码："+trade_status);
                parameters.put("status","3");
            }
            final String finalTradeResult=tradeResult;
            try {
                //HospitalInfoService.getInstance().commonService("FY030317",parameters);
                String msg=HospitalInfoService.getInstance().commonService("FY030317",parameters).get(0).get("msg");
                if("success".equals(msg)){
                    logger.info("退款订单状态交易修改成功！");
                }else{
                    logger.info("退款订单状态交易修改失败！");
                }
            } catch (HospitalException e) {
                e.printStackTrace();
                out.println("fail");
                out.flush();
                out.close();
                return;
            }
                //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                executors.execute(new Runnable() {
                    @Override
                    public void run() {
                        HospitalInfoService.getInstance().refundPlatformNotifyMessage( openId, "0.01",finalTradeResult);
                    }
                });

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
