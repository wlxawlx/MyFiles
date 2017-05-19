package com.wechat.servlet.gateway;

import com.wechat.WechatOfficialAccountEnv;
import com.wechat.handler.*;
import me.chanjar.weixin.common.api.WxConsts;
import me.chanjar.weixin.common.util.StringUtils;
import me.chanjar.weixin.mp.api.WxMpInMemoryConfigStorage;
import me.chanjar.weixin.mp.api.WxMpMessageRouter;
import me.chanjar.weixin.mp.api.WxMpService;
import me.chanjar.weixin.mp.bean.WxMpXmlMessage;
import me.chanjar.weixin.mp.bean.WxMpXmlOutMessage;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * 微信的消息接收网关
 * Created by zzw on 16/8/28.
 */
public class WxMpServlet extends HttpServlet {

    private WxMpInMemoryConfigStorage config = WechatOfficialAccountEnv.wxMpConfigStorage;
    private WxMpService wxMpService = WechatOfficialAccountEnv.wxMpService;
    private WxMpMessageRouter wxMpMessageRouter;

    @Override
    public void init() throws ServletException {
        super.init();

        wxMpMessageRouter = new WxMpMessageRouter(wxMpService);
        wxMpMessageRouter
                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_TEXT)
                .handler(new WxChatTextHandler())
                .end()

                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_IMAGE)
                .handler(new WxChatImageHandler())
                .end()

                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_VOICE)
                .handler(new WxChatVoiceHandler())
                .end()

                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_SHORTVIDEO)
                .handler(new WxChatShortVideoHandler())
                .end()

                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_VIDEO)
                .handler(new WxChatVideoHandler())
                .end()

                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_LOCATION)
                .handler(new WxChatLocationHandler())
                .end()

                .rule()
                .async(false)
                .msgType(WxConsts.XML_MSG_LINK)
                .handler(new WxChatLinkHandler())
                .end();
    }

    @Override
    protected void service(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        response.setContentType("text/html;charset=utf-8");
        response.setStatus(HttpServletResponse.SC_OK);

        String signature = request.getParameter("signature");
        String nonce = request.getParameter("nonce");
        String timestamp = request.getParameter("timestamp");

        if (!wxMpService.checkSignature(timestamp, nonce, signature)) {
            // 消息签名不正确，说明不是公众平台发过来的消息
            response.getWriter().println("非法请求");
            return;
        }

        String echostr = request.getParameter("echostr");
        if (StringUtils.isNotBlank(echostr)) {
            // 说明是一个仅仅用来验证的请求，回显echostr
            response.getWriter().println(echostr);
            return;
        }

        String encryptType = StringUtils.isBlank(request.getParameter("encrypt_type")) ?
                "raw" :
                request.getParameter("encrypt_type");

        if ("raw".equals(encryptType)) {
            // 明文传输的消息
            WxMpXmlMessage inMessage = WxMpXmlMessage.fromXml(request.getInputStream());
            WxMpXmlOutMessage outMessage = wxMpMessageRouter.route(inMessage);
            if (outMessage != null) {
                response.getWriter().write(outMessage.toXml());
            }
            return;
        }

        if ("aes".equals(encryptType)) {
            // 是aes加密的消息
            String msgSignature = request.getParameter("msg_signature");
            WxMpXmlMessage inMessage = WxMpXmlMessage.fromEncryptedXml(request.getInputStream(), config, timestamp, nonce, msgSignature);
            WxMpXmlOutMessage outMessage = wxMpMessageRouter.route(inMessage);
            if (outMessage != null) {
                response.getWriter().write(outMessage.toEncryptedXml(config));
            }
            return;
        }

        response.getWriter().println("不可识别的加密类型");
        return;
    }
}
