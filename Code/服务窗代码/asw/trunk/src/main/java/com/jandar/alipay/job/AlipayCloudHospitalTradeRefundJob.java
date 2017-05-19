package com.jandar.alipay.job;

import com.alipay.config.AlipayConfig;
import com.alipay.constants.AlipayServiceEnvConstants;
import com.alipay.util.AlipaySubmit;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderFinishInfo;
import com.jandar.alipay.util.KitUtil;
import com.jandar.config.ConfigHandler;
import org.apache.commons.lang3.StringUtils;
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
 * 通过后台返回的支付宝订单号进行对该订单二次验证
 * 需要调用查询接口的情况： 当商户后台、网络、服务器等出现异常，商户系统最终未接收到支付通知； 调用支付接口后，返回系统错误或未知交易状态情况
 */
@Service("alipayCloudHospitalTradeRefundJob")
public class AlipayCloudHospitalTradeRefundJob {

    private final Log log = LogFactory.getLog(getClass());

    public void execute() {
        log.info("云医院支付宝退费开始任务");
        Map<String, Object> parameters=new HashMap<String, Object>();
        List<Map<String, String>> result=null;
        try {
            result= HospitalInfoService.getInstance().commonService("FY030318",parameters);
        } catch (HospitalException e) {
            e.printStackTrace();
            return;
        }
        for(Map<String, String> map:result){
            String batchNo=map.get("batchno");
            String tradeNo=map.get("tradeno");
            String totalFee=map.get("totalfee");
            if (StringUtils.isBlank(batchNo)) {
                log.error("云医院批次号不能为空");
                return;
            }
            if (StringUtils.isBlank(tradeNo)) {
                log.error("支付宝交易号不能为空");
                return;
            }
            if (StringUtils.isBlank(totalFee)) {
                log.error("退费金额不能为空");
                return;
            }

            try {
                log.info("云医院批次号:" + batchNo);
                log.info("支付宝交易号:" + tradeNo);
                log.info("退费金额:" + totalFee);
                singleTradeRefund(tradeNo, totalFee, batchNo);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        log.info("云医院支付宝退费结束任务");

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
     * 单笔退款
     *
     * @param total_fee      商户PID
     * @param trade_no     支付宝交易号
     * @param batch_no 医院流水号
     */
    public static Map<String, Object> singleTradeRefund(String trade_no, String total_fee,String batch_no) throws Exception {

        //把请求参数打包成数组
        Date currentTime = new Date();
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String dateString = formatter.format(currentTime);
        //必填，不能修改
        //服务器异步通知页面路径
        String notify_url = "";

        notify_url=  "http://" + ConfigHandler.getSelfServiceHost() +"/ch/alipay_refund_notify_url.jsp";
              /*  +"?call_back_user_id=" + openId;*/
        //把请求参数打包成数组
        Map<String, String> sParaTemp = new HashMap<String, String>();
        sParaTemp.put("service", "refund_fastpay_by_platform_nopwd");
        sParaTemp.put("partner", AlipayConfig.partner);
        sParaTemp.put("_input_charset", AlipayConfig.input_charset);
        sParaTemp.put("notify_url", notify_url);
        sParaTemp.put("seller_user_id", AlipayConfig.seller_id);
        sParaTemp.put("refund_date", dateString);
        sParaTemp.put("batch_no", batch_no);
        sParaTemp.put("batch_num", "1");

//        if (!StringUtils.isEmpty(show_url) && !show_url.startsWith("http")) {
//            show_url += "http://" + ConfigHandler.getSelfServiceHost() + "/" + show_url;
//        }

        sParaTemp.put("detail_data", trade_no+"^"+total_fee+"^结算job退费");
        System.out.println("detail_data::::::::::::::"+trade_no+"^"+total_fee+"^结算job退费");
        //建立请求
        String sHtmlText = AlipaySubmit.buildRequest("", "", sParaTemp);
        return parseSingleTradeQueryXml(sHtmlText);
    }


}