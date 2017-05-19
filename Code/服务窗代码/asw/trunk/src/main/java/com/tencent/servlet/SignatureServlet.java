package com.tencent.servlet;

import com.google.gson.Gson;
import com.tencent.common.Configure;
import com.tencent.common.RandomStringGenerator;
import com.tencent.common.Signature;
import com.tencent.common.TimeStampUtil;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.Writer;

/**
 * Created by zhufengxiang on 2016/09/09.
 * 微信签名servlet
 * 需要参与签名的参数有: appId, timeStamp, nonceStr, package, signType。
 */
public class SignatureServlet extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Writer out = resp.getWriter();

        //获取prepayid
        String prePayId = req.getParameter("prePayId");
        PayBody payBody = new PayBody(prePayId);
        try {
            String paySign = Signature.getSign(payBody);
            payBody.setPaySign(paySign);
            out.write(new Gson().toJson(paySign));
            out.flush();
            out.close();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

    }

    private static class PayBody {
        private String appId = Configure.getAppid();
        private String timeStamp;// 时间戳
        private String nonceStr; // 不长于32位
        private String $package; // package 统一下单id
        private String singType = "MD5"; // 新版传入MD5
        private String paySign; // 为空, 签完名后再赋值上去, 回传客户端。

        private PayBody() {
            timeStamp = TimeStampUtil.currentSeconds();
            nonceStr = RandomStringGenerator.getRandomStringByLength(32);
        }

        public PayBody(String prePayId) {
            this();
            this.$package = prePayId;
        }

        public String getAppId() {
            return appId;
        }

        public void setAppId(String appId) {
            this.appId = appId;
        }

        public String getTimeStamp() {
            return timeStamp;
        }

        public void setTimeStamp(String timeStamp) {
            this.timeStamp = timeStamp;
        }

        public String getNonceStr() {
            return nonceStr;
        }

        public void setNonceStr(String nonceStr) {
            this.nonceStr = nonceStr;
        }

        public String get$package() {
            return $package;
        }

        public void set$package(String $package) {
            this.$package = $package;
        }

        public String getSingType() {
            return singType;
        }

        public void setSingType(String singType) {
            this.singType = singType;
        }

        public String getPaySign() {
            return paySign;
        }

        public void setPaySign(String paySign) {
            this.paySign = paySign;
        }
    }
}
