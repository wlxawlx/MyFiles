package com.jandar.servlet;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.bean.ProtocolCallLog;
import com.jandar.cloud.hospital.im.IMClient;
import com.jandar.cloud.hospital.service.TestSession;
import com.jandar.dao.CallProtocolLogDao;
import com.jandar.handle.protocol.Protocol;
import com.jandar.handle.protocol.ProtocolMapping;
import com.jandar.util.GsonUtil;
import com.jandar.util.JsonUtil;
import com.jandar.util.SpringBeanUtil;
import org.apache.commons.lang3.StringUtils;

import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.lang.System;
import java.util.HashMap;
import java.util.Map;

/**
 * Servlet implementation class ServiceServlet
 */
public class ServiceServlet extends HttpServlet {

    private static final long serialVersionUID = -6007289242735669621L;

    private static final String DEFAULT_CHARSET = "UTF-8";

    private CallProtocolLogDao callProtocolLogDao = null;

    /**
     * @see HttpServlet#HttpServlet()
     */
    public ServiceServlet() {
        super();
    }

    @Override
    public void init(ServletConfig config) throws ServletException {
        super.init(config);

        callProtocolLogDao = SpringBeanUtil.getBean(CallProtocolLogDao.class);
        System.out.println("========================================ServiceServlet======init=============================================================");
        IMClient.getInstance();


    }

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

        System.out.println("-----------ServiceServlet-----------is coming----------");
//        request.setCharacterEncoding("UTF-8");
        response.setCharacterEncoding("UTF-8");
        response.setContentType("text/json;charset=UTF-8");


        //跨域访问
        response.setHeader("Access-Control-Allow-Origin","*");


        //TestSession中存储 http request
        TestSession.setRequest(request);
        ServiceContext.setRequest(request);

        String pcode = request.getParameter("pcode");
        String charset = request.getParameter("charset");
        if (StringUtils.isBlank(charset)) {
            charset = DEFAULT_CHARSET;
        }

        String contentStr = request.getParameter("content");
        if (StringUtils.isNotBlank(contentStr)) {
            try {
                contentStr = new String(contentStr.getBytes("UTF-8"), charset);
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
            }
        }
        System.out.println("请求内容:pcode:" + pcode + ",content:" + contentStr + " .");

        PrintWriter out = response.getWriter();

        try {
            if (pcode == null || "".equals(pcode)) {
                error(out, "协议号为空");
                return;
            }

            Map<String, Object> contentMap = new HashMap<String, Object>();
            if (contentStr != null && !"".equals(contentStr)) {
                contentMap = GsonUtil.toMap(contentStr);
            }

            Protocol protocol = ProtocolMapping.getInstance().getHandler(pcode, contentMap);
            if (protocol == null) {
                error(out, "不支持该功能");
                return;
            }

            // 记录协议调用历史
            ProtocolCallLog protocolCallLog = new ProtocolCallLog();
            protocolCallLog.pcode = pcode;
            protocolCallLog.content = contentMap;
            callProtocolLogDao.save(protocolCallLog);
            try {
                Object result = protocol.process(pcode, contentMap);
                System.out.println("ServiceServlet:protocol:success");
                System.out.println("ServiceServlet:protocol:"+result);
                Map<String, Object> json = new HashMap<String, Object>();
                json.put("status", HospitalException.SUCCESS);
                if (result != null) {
                    json.put("data", result);
                }
                // 返回执行成功的返回值
                final String writeInfo = JsonUtil.toJson(json);
                out.print(writeInfo);
                System.out.println("回复内容:" + writeInfo);
            } catch (HospitalException e) {
                System.out.println("ServiceServlet:protocol:error1");
                e.printStackTrace();
                String code = e.getCode();
                if (StringUtils.isBlank(code)) {
                    error(out, e.getMessage());
                } else {
                    error(out, code, e.getMessage());
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            System.out.println("ServiceServlet:protocol:error2");
            error(out, ex.getMessage());
        }

        out.flush();
        out.close();
    }

    private void error(PrintWriter out, String msg) {
        Map<String, String> error = new HashMap<String, String>();
        error.put("status", HospitalException.ERROR);
        error.put("msg", msg);
        final String writeInfo = JsonUtil.toJson(error);
        out.print(writeInfo);
        System.out.println("回复内容:" + writeInfo);
    }

    // 添加错误代码
    private void error(PrintWriter out, String status, String msg) {
        Map<String, String> error = new HashMap<String, String>();
        error.put("status", status);
        error.put("msg", msg);
        final String writeInfo = JsonUtil.toJson(error);
        out.print(writeInfo);
        System.out.println("回复内容:" + writeInfo);
    }

}
