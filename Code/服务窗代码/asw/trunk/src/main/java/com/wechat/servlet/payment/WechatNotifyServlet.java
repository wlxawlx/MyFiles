package com.wechat.servlet.payment;

import com.wechat.WechatOfficialAccountEnv;
import com.wechat.util.XMLUtil;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Map;

/**
 * 微信支付通知
 * Created by zzw on 16/8/28.
 */
public class WechatNotifyServlet extends HttpServlet {

    /**
     * 日志对象
     */
    protected Logger logger = LoggerFactory.getLogger(getClass());

    @Override
    protected void service(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        try {
            Map<String, String> kvm = XMLUtil.parseRequestXmlToMap(request);
            if (WechatOfficialAccountEnv.wxMpService.getPayService()
                    .checkJSSDKCallbackDataSignature(kvm, kvm.get("sign"))) {
                if (kvm.get("result_code").equals("SUCCESS")) {
                    //TODO 微信服务器通知此回调接口支付成功后，通知给业务系统做处理
                    this.logger.info("out_trade_no: "
                            + kvm.get("out_trade_no") + " pay SUCCESS!");
                    response.getWriter().write(
                            "<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[ok]]></return_msg></xml>");
                } else {
                    this.logger.error("out_trade_no: "
                            + kvm.get("out_trade_no") + " result_code is FAIL");
                    response.getWriter().write(
                            "<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[result_code is FAIL]]></return_msg></xml>");
                }
            } else {
                response.getWriter().write(
                        "<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[check signature FAIL]]></return_msg></xml>");
                this.logger.error("out_trade_no: " + kvm.get("out_trade_no")
                        + " check signature FAIL");
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}
