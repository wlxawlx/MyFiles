package com.jandar.cloud.hospital.msg;

import com.alipay.api.AlipayClient;
import com.alipay.api.request.AlipayOpenPublicMessageSingleSendRequest;
import com.alipay.api.response.AlipayOpenPublicMessageSingleSendResponse;
import com.alipay.factory.AlipayAPIClientFactory;
import net.sf.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Administrator on 2016/12/27.
 */
public class MessageTemplate {
    public static void appointMessageTemplate(Map<String, String> orderInfo){
        String toUserId = orderInfo.get("to_user_id");
        String tousername = orderInfo.get("to_user_name");
        String patientname = orderInfo.get("patientname");
        String patientsex = orderInfo.get("patient_sex");
        String departname = orderInfo.get("departname");
        String doctorname = orderInfo.get("doctorname");
        String time = orderInfo.get("preengagedate")+" "+orderInfo.get("preengagetime");
        String orderinfourl = orderInfo.get("order_info_url");

        AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
        AlipayOpenPublicMessageSingleSendRequest request = new AlipayOpenPublicMessageSingleSendRequest();
        Map<String, Object> bizMap = new HashMap<String, Object>();
        bizMap.put("to_user_id", toUserId);
        Map<String, Object> templateMap = new HashMap<String, Object>();
        templateMap.put("template_id", "c0220068983f4e7f964747dfb43ccef7");   //选择模板
        Map<String, Object> contextMap = new HashMap<String, Object>();
        contextMap.put("head_color", "#85be53");
        contextMap.put("url", orderinfourl);
        contextMap.put("action_name", "查看详情");
        Map<String, Object> first = new HashMap<String, Object>();
        first.put("value", tousername+"你好！你已挂号成功");
        Map<String, Object> keyword1 = new HashMap<String, Object>();
        keyword1.put("value", patientname);
        Map<String, Object> keyword2 = new HashMap<String, Object>();
        keyword2.put("value", patientsex);
        Map<String, Object> keyword3 = new HashMap<String, Object>();
        keyword3.put("value", departname);
        Map<String, Object> keyword4 = new HashMap<String, Object>();
        keyword4.put("value", doctorname);
        Map<String, Object> keyword5 = new HashMap<String, Object>();
        keyword5.put("value", time);
        Map<String, Object> remark = new HashMap<String, Object>();
        remark.put("value", "请在"+time+"准时就诊！");
        contextMap.put("first", first);
        contextMap.put("keyword1", keyword1);
        contextMap.put("keyword2", keyword2);
        contextMap.put("keyword3", keyword3);
        contextMap.put("keyword4", keyword4);
        contextMap.put("keyword5", keyword5);
        contextMap.put("remark", remark);
        templateMap.put("context", contextMap);
        bizMap.put("template", templateMap);
        request.setBizContent(JSONObject.fromObject(bizMap).toString());
        System.out.println(request.getBizContent());
        AlipayOpenPublicMessageSingleSendResponse response = null;
        try {
            response = alipayClient.execute(request);
            System.out.println(response.getBody());
        } catch (Exception e) {
            // TODO: handle exception
        }
    }

    /**
     * 缴费通知
     */
    public static void paymentMessageTemplate(Map<String, String> map){
        System.out.println("缴费通知模板消息-------------paymentMessageTemplate");
        String toUserId = map.get("AlipayUserId");//"20881043345479391710124170613538";
        String tousername = map.get("PatientName");//"小红";
        String url = map.get("url");//"https://www.baidu.com/";

        AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
        AlipayOpenPublicMessageSingleSendRequest request = new AlipayOpenPublicMessageSingleSendRequest();
        Map<String, Object> bizMap = new HashMap<String, Object>();
        bizMap.put("to_user_id", toUserId);
        Map<String, Object> templateMap = new HashMap<String, Object>();
        templateMap.put("template_id", "d55877f054824ffc8836de263ab40c59");   //选择模板
        Map<String, Object> contextMap = new HashMap<String, Object>();
        contextMap.put("head_color", "#85be53");
        contextMap.put("url", url);
        contextMap.put("action_name", "查看详情");
        Map<String, Object> first = new HashMap<String, Object>();
        first.put("value", tousername+"你好！你有一笔待缴费，请点击支付");
        Map<String, Object> keyword1 = new HashMap<String, Object>();
        keyword1.put("value", "具体金额查看详情");
        Map<String, Object> keyword2 = new HashMap<String, Object>();
        keyword2.put("value", "具体类型查看详情");
        Map<String, Object> remark = new HashMap<String, Object>();
        remark.put("value", "缴费完成请等候...");
        contextMap.put("first", first);
        contextMap.put("keyword1", keyword1);
        contextMap.put("keyword2", keyword2);
        contextMap.put("remark", remark);
        templateMap.put("context", contextMap);
        bizMap.put("template", templateMap);
        request.setBizContent(JSONObject.fromObject(bizMap).toString());
        System.out.println(request.getBizContent());
        AlipayOpenPublicMessageSingleSendResponse response = null;
        try {
            response = alipayClient.execute(request);
            System.out.println(response.getBody());
        } catch (Exception e) {
            // TODO: handle exception
        }
    }
}
