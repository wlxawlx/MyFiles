package com.jandar.alipay.job;

import com.alipay.config.AlipayConfig;
import com.alipay.constants.AlipayServiceEnvConstants;
import com.alipay.payment.wap.AlipayWapPayment;
import com.alipay.util.AlipaySubmit;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderFinishInfo;
import com.jandar.alipay.util.KitUtil;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.jdom.Element;
import org.jdom.input.SAXBuilder;
import org.springframework.stereotype.Service;
import org.xml.sax.InputSource;

import java.io.StringReader;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by ysc on 2017/1/19.
 * 通过后台返回的支付宝订单号进行对该订单二次验证并更新订单状态
 */
@Service("alipayCloudHospitalTradeQueryJob")
public class AlipayCloudHospitalTradeQueryJob {

    private final Log log = LogFactory.getLog(getClass());

    public void execute() {
        log.info("云医院支付宝交易查询并更新订单状态开始任务");
        Map<String, Object> parameters=new HashMap<String, Object>();
        List<Map<String, String>> result=null;
        try {
            result= HospitalInfoService.getInstance().commonService("FY030314",parameters);
        } catch (HospitalException e) {
            e.printStackTrace();
            return;
        }
        for(Map<String, String> map:result){
            Map<String, Object> requestInfo = null;
            try {
                requestInfo = singleTradeQuery(AlipayServiceEnvConstants.PARTNER, map.get("tradeno"),map.get("orderno"));
            } catch (Exception e) {
                e.printStackTrace();
            }
            /** trade_status 四种状态 TRADE_FINISHED, TRADE_SUCCESS, WAIT_BUYER_PAY, TRADE_CLOSED
             */
            String trade_status = KitUtil.getString(requestInfo, "trade_status");
            log.info("订单号:" + map.get("orderno"));
            log.info("支付宝返回订单状态是:" + trade_status);
            parameters=new HashMap<String, Object>();
            parameters.put("orderno",map.get("orderno"));
            if (trade_status.equals("TRADE_SUCCESS") || trade_status.equals("TRADE_FINISHED")) {
                parameters.put("status","1");
            }else{
                parameters.put("status","-1");
            }
            try {
                List<Map<String, String>> updateResult=HospitalInfoService.getInstance().commonService("FY030308",parameters);
                if(updateResult !=null && updateResult.size()>0){
                    log.info(map.get("orderno")+" 更新状态结果："+updateResult.get(0).get("msg"));
                }
            } catch (HospitalException e) {
                e.printStackTrace();
            }
        }

        log.info("云医院支付宝交易查询并更新订单状态结束任务");

    }

    /**
     * 解析单笔查询返回的报文
     *
     * @param xml 报文内容
     * @return 解析结果
     * @throws Exception
     */
    private static Map<String, Object> parseSingleTradeQueryXml(String xml) throws Exception {
        if (KitUtil.isEmpty(xml)) {
            throw new Exception("支付宝网关异常，无信息回复.");
        }

        System.out.println("支付宝单笔查询返回信息:" + xml);

        Map<String, Object> map = new HashMap<String, Object>();

        StringReader reader = new StringReader(xml);
        InputSource source = new InputSource(reader);
        SAXBuilder sax = new SAXBuilder();
        org.jdom.Document doc = sax.build(source);
        Element root = doc.getRootElement();
        Element alipay = null;
        if (root.getName().equals("alipay")) {
            alipay = root;
        } else {
            alipay = root.getChild("alipay");
        }
        if ("F".equals(alipay.getChildText("is_success"))) {
            throw new Exception(alipay.getChildText("error"));
        }

        Element response = alipay.getChild("response");
        if (response != null) {
            Element trade = response.getChild("trade");
            List<Element> items = trade.getChildren();
            for (Element item : items) {
                map.put(item.getName(), item.getText());
            }
        }

        return map;
    }

    /**
     * 单笔交易查询
     *
     * @param partner      商户PID
     * @param trade_no     支付宝交易号
     * @param out_trade_no 医院流水号
     */
    public static Map<String, Object> singleTradeQuery(String partner, String trade_no, String out_trade_no) throws Exception {

        //把请求参数打包成数组
        Map<String, String> sParaTemp = new HashMap<String, String>();
        sParaTemp.put("service", "single_trade_query");
        sParaTemp.put("partner", partner);
        sParaTemp.put("_input_charset", "utf-8");
        sParaTemp.put("trade_no", trade_no);
        sParaTemp.put("out_trade_no", out_trade_no);

        //建立请求
        String sHtmlText = AlipaySubmit.buildRequest("", "", sParaTemp);
        return parseSingleTradeQueryXml(sHtmlText);
    }


}