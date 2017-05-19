package com.jandar.servlet;

import com.alipay.servlet.gateway.AlipayGatewayServlet;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.util.RequestUtils;
import com.wechat.servlet.gateway.WxMpServlet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * 统一网关入口
 * Created by zzw on 16/8/28.
 */
public class GatewayServlet extends HttpServlet {

    private AlipayGatewayServlet alipayGatewayServlet = new AlipayGatewayServlet();
    //private WxMpServlet wxMpServlet = new WxMpServlet();

    @Override
    public void init() throws ServletException {
        super.init();
        System.out.println("GatewayServlet:init:start");
        alipayGatewayServlet.init(this.getServletConfig());
        System.out.println("GatewayServlet:init:end");
        //wxMpServlet.init(this.getServletConfig());
    }

    @Override
    protected void service(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        System.out.println("GatewayServlet:service:start");
        PlatformType type = RequestUtils.getType(req);
        System.out.println("GatewayServlet:type:"+type);
        req.getSession().setAttribute("type", type.toString());
        if (type == PlatformType.Wechat) {
            System.out.println("GatewayServlet:service:wechat");
            //wxMpServlet.service(req, resp);
        } else {
            System.out.println("GatewayServlet:service:alipay");
            System.out.println("GatewayServlet:service:req:"+req);
            alipayGatewayServlet.service(req, resp);
        }
    }
}
