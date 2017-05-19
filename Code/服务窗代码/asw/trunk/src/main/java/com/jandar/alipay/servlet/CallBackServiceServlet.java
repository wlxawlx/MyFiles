package com.jandar.alipay.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.jandar.dao.CallProtocolLogDao;
import com.jandar.handle.protocol.Protocol;
import com.jandar.handle.protocol.ProtocolMapping;
import com.jandar.util.SpringBeanUtil;
import org.apache.commons.lang3.StringUtils;
import org.w3c.dom.Element;

import com.alipay.api.internal.util.XmlUtils;
import com.jandar.alipay.core.HospitalException;

/**
 * Servlet implementation class CallBackServiceServlet
 */
public class CallBackServiceServlet extends HttpServlet {
    private static final long serialVersionUID = -2167809570648672844L;

    private static final String DEFAULT_CHARSET = "UTF-8";

    private CallProtocolLogDao callProtocolLogDao = null;

    /**
     * @see HttpServlet#HttpServlet()
     */
    public CallBackServiceServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

    @Override
    public void init(ServletConfig config) throws ServletException {
        super.init(config);

        callProtocolLogDao = SpringBeanUtil.getBean(CallProtocolLogDao.class);
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
        HttpServletRequest req = request;
        HttpServletResponse rep = response;
        request.setCharacterEncoding("UTF-8");
        rep.setCharacterEncoding("UTF-8");
        rep.setContentType("text/xml;charset=UTF-8");

        String pcode = req.getParameter("pcode");
        String charset = req.getParameter("charset");
        if (StringUtils.isBlank(charset)) {
            charset = DEFAULT_CHARSET;
        }

        String content = req.getParameter("content");
        if (StringUtils.isNotBlank(content)) {
            try {
                content = new String(content.getBytes("UTF-8"), charset);
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
            }
        }
        System.out.println("请求内容:pcode:" + pcode + ",content:" + content + " .");

        PrintWriter out = rep.getWriter();

        try {
            if (pcode == null || "".equals(pcode)) {
                error(out, "协议号为空");
                return;
            }
//			//所有页面判断是否建档
//			UserInfo info = ServiceContext.getHospitalUserInfo();
//			//String yhid=info.getYhid();
//			if (info == null) {
//				if (!pcode.equals("BA010101")) {
//					Map<String, Object> json = new HashMap<String, Object>();
//					json.put("status", "98");
//					out.print(JsonUtil.toJson(json));
//					throw new HospitalException("还未建档,请先建档");
//				}
//			}
//
            Map<String, Object> message = new HashMap<String, Object>();
            if (content != null && !"".equals(content)) {
//				message = JsonUtil.toMap(content);
                message = (Map<String, Object>) XmlUtils.getRootElementFromString(content);
            }

            Protocol protocol = ProtocolMapping.getInstance().getHandler(pcode, message);

            if (protocol == null) {
                error(out, "不支持该功能");
                return;
            }

            try {
                Object result = protocol.process(pcode, message);

//				Map<String, Object> json = new HashMap<String, Object>();
//				json.put("status", HospitalException.SUCCESS);
//				返回消息：XML格式的回复报文内容
                Map<String, Object> xml = new HashMap<String, Object>();
                xml.put("status", HospitalException.SUCCESS);
                if (result != null) {
//					json.put("data", result);
                    xml.put("data", result);
                }
                // 返回执行成功的返回值
//                final String writeInfo = JsonUtil.toJson(json);
                final String writeInfo = XmlUtils.getElementValue((Element) xml);
                out.print(writeInfo);
                System.out.println("回复内容:" + writeInfo);
            } catch (HospitalException e) {
                String code = e.getCode();
                if (StringUtils.isBlank(code)) {
                    error(out, e.getMessage());
                } else {
                    error(out, code, e.getMessage());
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
//			throw new ServletException(ex.getMessage());
        }

        out.flush();
        out.close();
    }

    private void islong(PrintWriter out, String msg) {
        Map<String, String> error = new HashMap<String, String>();
        error.put("status", HospitalException.UNARCHIV);
        error.put("msg", msg);
//		out.print(JsonUtil.toJson(error));
        out.println(XmlUtils.getElementValue((Element) error));
    }

    private void error(PrintWriter out, String msg) {
        Map<String, String> error = new HashMap<String, String>();
        error.put("status", HospitalException.ERROR);
        error.put("msg", msg);
//        final String writeInfo = JsonUtil.toJson(error);
        final String writeInfo = XmlUtils.getElementValue((Element) error);
        out.print(writeInfo);
        System.out.println("回复内容:" + writeInfo);
    }

    // 添加错误代码
    private void error(PrintWriter out, String status, String msg) {
        Map<String, String> error = new HashMap<String, String>();
        error.put("status", status);
        error.put("msg", msg);
//        final String writeInfo = JsonUtil.toJson(error);
        final String writeInfo = XmlUtils.getElementValue((Element) error);
        out.print(writeInfo);
        System.out.println("回复内容:" + writeInfo);
    }

}
