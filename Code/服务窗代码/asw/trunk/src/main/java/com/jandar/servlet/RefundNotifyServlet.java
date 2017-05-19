package com.jandar.servlet;

import com.alipay.servlet.payment.AlipayRefundNotifyServlet;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.util.RequestUtils;
import com.wechat.servlet.payment.WechatNotifyServlet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * 统一通知 servlet
 * Created by ysc on 16/8/28.
 */
public class RefundNotifyServlet extends HttpServlet {

    private AlipayRefundNotifyServlet alipayRefundNotifyServlet = new AlipayRefundNotifyServlet();

    @Override
    public void init() throws ServletException {
        super.init();

        alipayRefundNotifyServlet.init(this.getServletConfig());
    }

    @Override
    protected void service(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        PlatformType type = RequestUtils.getType(req);
        req.getSession().setAttribute("type", type.toString());
        if (type == PlatformType.Wechat) {

        } else {
            alipayRefundNotifyServlet.service(req, resp);
        }
    }
}
