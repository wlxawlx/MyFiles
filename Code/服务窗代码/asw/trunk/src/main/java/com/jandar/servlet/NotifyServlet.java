package com.jandar.servlet;

import com.alipay.servlet.payment.AlipayNotifyServlet;
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
 * Created by zzw on 16/8/28.
 */
public class NotifyServlet extends HttpServlet {

    private AlipayNotifyServlet alipayNotifyServlet = new AlipayNotifyServlet();
    private WechatNotifyServlet wechatNotifyServlet = new WechatNotifyServlet();

    @Override
    public void init() throws ServletException {
        super.init();

        alipayNotifyServlet.init(this.getServletConfig());
        wechatNotifyServlet.init(this.getServletConfig());
    }

    @Override
    protected void service(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        PlatformType type = RequestUtils.getType(req);
        req.getSession().setAttribute("type", type.toString());
        if (type == PlatformType.Wechat) {
            wechatNotifyServlet.service(req, resp);
        } else {
            alipayNotifyServlet.service(req, resp);
        }
    }
}
