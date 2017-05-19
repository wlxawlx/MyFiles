package com.jandar.alipay.core.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.ServiceWindowFlag;
import com.jandar.config.ConfigHandler;
import org.apache.log4j.Logger;
import org.codehaus.xfire.client.Client;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.input.SAXBuilder;
import org.xml.sax.InputSource;

import java.io.IOException;
import java.io.StringReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

/**
 * 医院端提供的服务
 */
class WebService {
    static Logger logger = Logger.getLogger(WebService.class);
    static final String wsUrl = ConfigHandler.getHospitalWebserviceWSDLUrl();

    static String[] parseParam(String opCode, Map<String, Object> parameters) {
        StringBuilder paramXml = new StringBuilder();
        if (parameters == null)
            parameters = new HashMap<>();
        paramXml.append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><root>");
        for (Entry<String, Object> entry : parameters.entrySet()) {
            paramXml.append("<").append(entry.getKey()).append("><![CDATA[").append(entry.getValue()).append("]]></").append(entry.getKey()).append(">");
        }
        paramXml.append("</root>");
        return new String[]{opCode, paramXml.toString()};
    }

    private WebService() {
    }

    public static List<Map<String, String>> invoke(String opCode, Map<String, Object> parameters) throws HospitalException {
        try {
            URL _url = new URL(wsUrl);
            HttpURLConnection httpConnection = (HttpURLConnection) _url.openConnection();
//            httpConnection.setConnectTimeout(timeout);
//            httpConnection.setReadTimeout(timeout);//设置http连接的读超时,单位是毫秒

            httpConnection.connect();
            Client client = new Client(httpConnection.getInputStream(), null);
//            client.setProperty(CommonsHttpMessageSender.HTTP_TIMEOUT, String.valueOf(timeout));//设置发送的超时限制,单位是毫秒;

            logger.info("WebService传递:" + opCode + " " + parameters);
            Object[] results = client.invoke("accountService", parseParam(opCode, parameters));

            if (results.length > 0) {
                String xml = results[0].toString();
                System.out.println("======================"+xml);
                logger.info("WebService返回结果:" + xml);
                if (xml.contains("没有预约号源记录")) { // 因为没有预约号源记录也是报错
                    return new ArrayList<Map<String, String>>();
                }

                return (ConfigHandler.getServiceWindowFlag() == ServiceWindowFlag.WZSRMYY ||
                        ConfigHandler.getServiceWindowFlag() == ServiceWindowFlag.WZSRMYY_TJZX)? parseXmlWZSRMYY(xml) : parseXmlDefault(xml);
            }
        } catch (HospitalException e) {
            throw e;
        } catch (MalformedURLException e) {
            e.printStackTrace();
            throw new HospitalException("系统配置错误,请联系系统管理员", HospitalException.SYSTEM_ERROR);
        } catch (IOException e) {
            e.printStackTrace();
            throw new HospitalException("网络错误,请联系系统管理员", HospitalException.SYSTEM_ERROR);
        } catch (Exception e) {
            e.printStackTrace();
            throw new HospitalException("系统错误,请联系系统管理员", HospitalException.SYSTEM_ERROR);
        }
        return new ArrayList<Map<String, String>>();
    }

    /**
     * 温州市第三人民医院错误解析
     *
     * @param xml 输入xml
     * @return 输出数据
     * @throws HospitalException 解析异常
     */
    private static List<Map<String, String>> parseXmlWZSRMYY(String xml) throws HospitalException {
        List<Map<String, String>> dataList = new ArrayList<Map<String, String>>();
        try {
            StringReader reader = new StringReader(xml);
            InputSource source = new InputSource(reader);
            SAXBuilder sax = new SAXBuilder();
            Document doc = sax.build(source);
            Element root = doc.getRootElement();
            Element exception = root.getChild("exception");
            if (exception != null) {
                if (exception.getTextTrim().contains("该身份没有在医院登记过")) {
                    throw new HospitalException("未在医院就过诊");
                } else {
                    throw new HospitalException(exception.getTextTrim());
                }
            }

            Element message = root.getChild("message");
            if (message != null) {
                List<Element> valueElements = (List) message.getChildren("value");
                List<Element> dataElements = null;
                if (valueElements != null) {
                    for (Element valueElement : valueElements) {
                        Map<String, String> data = new HashMap<String, String>();
                        dataElements = (List) valueElement.getChildren();
                        if (dataElements != null) {
                            for (Element dataElement : dataElements) {
                                data.put(dataElement.getName(), dataElement.getTextTrim());
                            }
                            dataList.add(data);
                        }
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            throw new HospitalException(e.getMessage());
        }
        return dataList;
    }

    /**
     * 默认错误解析
     *
     * @param xml 输入xml
     * @return 输出参数
     * @throws HospitalException 解析错误异常
     */
    private static List<Map<String, String>> parseXmlDefault(String xml) throws HospitalException {
        List<Map<String, String>> dataList = new ArrayList<Map<String, String>>();
        try {
            StringReader reader = new StringReader(xml);
            InputSource source = new InputSource(reader);
            SAXBuilder sax = new SAXBuilder();
            Document doc = sax.build(source);
            Element root = doc.getRootElement();
            Element result = root.getChild("result");
            Element message = root.getChild("message");
            String resultText = result.getTextTrim();
            // 还有另两种状态 exception 和 failure
            if (!"success".equalsIgnoreCase(resultText)) {
                String status = message.getChild("status").getTextTrim();
                // 这儿要做状态值的转化的,把七院的状态在可能的情况下改为前端的状态值
//                status = HospitalException.ERROR;
                throw new HospitalException(message.getChild("remark").getTextTrim(), status);
            }

            if (message != null) {
                List<Element> valueElements = (List) message.getChildren("value");
                List<Element> dataElements = null;
                if (valueElements != null) {
                    for (Element valueElement : valueElements) {
                        Map<String, String> data = new HashMap<String, String>();
                        dataElements = (List) valueElement.getChildren();
                        if (dataElements != null) {
                            for (Element dataElement : dataElements) {
                                data.put(dataElement.getName(), dataElement.getTextTrim());
                            }
                            dataList.add(data);
                        }
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            throw new HospitalException(e.getMessage());
        }
        return dataList;
    }
}
