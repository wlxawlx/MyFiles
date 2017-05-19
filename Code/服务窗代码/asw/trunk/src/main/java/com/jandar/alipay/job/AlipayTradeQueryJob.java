package com.jandar.alipay.job;

import com.alipay.constants.AlipayServiceEnvConstants;
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
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 通过后台返回的支付宝订单号进行对该订单二次验证
 * 需要调用查询接口的情况： 当商户后台、网络、服务器等出现异常，商户系统最终未接收到支付通知； 调用支付接口后，返回系统错误或未知交易状态情况； 调用alipay.trade.pay，返回INPROCESS的状态； 调用alipay.trade.cancel之前，需确认支付状态；
 */
@Service("alipayTradeQueryJob")
public class AlipayTradeQueryJob {

    private final Log log = LogFactory.getLog(getClass());

    public void execute() {
        log.info("开始任务");

        List<RechargeOrderFinishInfo> rechargeOrderFinishInfos;
        try {
            rechargeOrderFinishInfos = HospitalInfoService.getInstance().CheckNotAccountInfo();
        } catch (HospitalException e) {
            e.printStackTrace();
            return;
        }

        for (RechargeOrderFinishInfo rechargeOrderFinishInfo : rechargeOrderFinishInfos) {
            Map<String, Object> requestInfo = null;
            try {
                System.out.println("1:" + rechargeOrderFinishInfo.getTradeno());
                System.out.println("2:" + rechargeOrderFinishInfo.getPaymenttradeno());
                requestInfo = singleTradeQuery(AlipayServiceEnvConstants.PARTNER, rechargeOrderFinishInfo.getTradeno(), rechargeOrderFinishInfo.getPaymenttradeno());
            } catch (Exception e) {
                e.printStackTrace();
            }

            /** trade_status 四种状态 TRADE_FINISHED, TRADE_SUCCESS, WAIT_BUYER_PAY, TRADE_CLOSED(这种不会收到通知)
             * 现在有成功收到 TRADE_SUCCCESS 和 WAIT_BUYER_PAY
             * TRADE_SUCCESS 是成功
             * TRADE_FINISHED 也成功,但它包含且指退款*/
            String trade_status = KitUtil.getString(requestInfo, "trade_status");
            System.out.println("订单状态是:" + trade_status);
            log.info("订单状态是:" + trade_status);
            if (!trade_status.equalsIgnoreCase("TRADE_SUCCESS") && !trade_status.equalsIgnoreCase("TRADE_FINISHED") && !trade_status.equalsIgnoreCase("TRADE_CLOSED")) {
                System.out.println("交易还未成功,等待完成交易的通知.");
                continue;
            } else if (trade_status.equals("TRADE_SUCCESS")) {
//                try {
//                    HospitalInfoService.getInstance().paymentArrivalNotify(orderType, rechargeOrderFinishInfo.getOpenid(), rechargeOrderFinishInfo.getPatientname()
//                            , rechargeOrderFinishInfo.getPaymenttradeno(), rechargeOrderFinishInfo.getTradeno(), total_fee, obligate, params.toString());
//                } catch (HospitalException e) {
//                    e.printStackTrace();
//                    return;
//                }
            }
        }

        log.info("结束任务");
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