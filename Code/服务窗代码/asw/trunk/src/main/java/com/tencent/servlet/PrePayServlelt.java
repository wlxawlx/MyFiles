package com.tencent.servlet;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.tencent.WXPay;
import com.tencent.protocol.unified_order_protocol.UnifiedOrderReqData;

import javax.servlet.ServletException;
import javax.servlet.ServletInputStream;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Writer;

/**
 * Created by zhufengxiang on 2016/09/09.
 * 微信统一支付servlet
 */
public class PrePayServlelt extends HttpServlet{

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Writer out = resp.getWriter();

        // 获取客户端传过来的参数。
        ServletInputStream inputStream = req.getInputStream();
        BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));
        String str;
        StringBuilder sb = new StringBuilder();
        while((str = br.readLine()) != null) {
            sb.append(str);
        }

        UnifiedOrderReqData unifiedOrderReqData = new Gson().fromJson(sb.toString(), new TypeToken<UnifiedOrderReqData>(){}.getType());
        try {
            String s = WXPay.requestUnifiedOrderService(unifiedOrderReqData);
            out.write(s);
            out.flush();
            out.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
