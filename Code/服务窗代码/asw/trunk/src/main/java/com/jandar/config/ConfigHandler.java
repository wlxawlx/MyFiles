package com.jandar.config;

import com.jandar.alipay.hospital.ServiceWindowFlag;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import java.io.*;
import java.util.ResourceBundle;

/**
 * Created by zzw on 15/11/23.
 * 配置文件处理器
 */
public class ConfigHandler {

    private static final Log log = LogFactory.getLog(ConfigHandler.class);

    /**
     * 支付用私钥
     */
    private static String m_alipayPaymentPrivateKey = null;

    /**
     * 服务窗私钥
     */
    private static String m_alipayWindowPrivateKey = null;

    /**
     * 服务窗公钥
     */
    private static String m_alipayWindowPublicKey = null;

    private static ConfigHandler ourInstance = new ConfigHandler();

    public static ConfigHandler getInstance() {
        return ourInstance;
    }

    private ConfigHandler() {
    }

    /**
     * 清理配置缓存,让系统重新读取配置文件
     */
    public static void clearConfigCache() {
        ResourceBundle.clearCache();
    }

    // 1. config.properties

    /**
     * 是否是测试环境
     *
     * @return 是否是测试环境
     */
    public static boolean systemIsTest() {
        String systemTestFlag = ResourceBundle.getBundle("config").getString("system.istest");
        return !"0".equals(systemTestFlag);
    }

    /**
     * 获得医院标识代码
     *
     * @return 医院标识代码
     */
    private static String getHospitalFlagCode() {
        return ResourceBundle.getBundle("config").getString("hospital.code");
    }

    /**
     * 获得服务窗标识
     *
     * @return 服务窗标识
     */
    public static ServiceWindowFlag getServiceWindowFlag() {
        String code = getHospitalFlagCode();
        if ("wzsrmyy".equalsIgnoreCase(code)) {
            return ServiceWindowFlag.WZSRMYY;
        } else if ("wzstjzx".equalsIgnoreCase(code)) {
            return ServiceWindowFlag.WZSRMYY_TJZX;
        } else if ("wzdqrmyy".equalsIgnoreCase(code)) {
            return ServiceWindowFlag.WZSDQRMYY;
        } else {
            return ServiceWindowFlag.DEFAULE;
        }
    }

    /**
     * 获得医院名称
     *
     * @return 医院名称
     */
    public static String getHospitalName() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("hospital.name");
    }

    /**
     * 获取自身对外提供服务的地址
     *
     * @return
     */
    public static String getSelfServiceHost() {
        if (systemIsTest()) {
            return ResourceBundle.getBundle("config").getString("self.test.service.host");
        } else {
            return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("self.service.host");
        }
    }

    /**
     * 获得医院 web sservice wsdl url 服务地址
     *
     * @return
     */
    public static String getHospitalWebserviceWSDLUrl() {
        if (systemIsTest()) {
            return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("hospital.webservice.test.wsdlurl");
        } else {
            return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("hospital.webservice.wsdlurl");
        }
    }

//    public static String getMongodbHost() {
//        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("mongodb.host");
//    }
//
//    public static int getMongodbPort() {
//        String port = ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("mongodb.port");
//        try {
//            return Integer.valueOf(port);
//        } catch (Exception ex) {
//            System.err.println(ex.getMessage());
//            return 27017;
//        }
//    }
//
//    public static String getMongodbDatabase() {
//        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("mongodb.database");
//    }

    /**
     * 获得支付宝接入商户的合作者ID
     *
     * @return 商户的合作者ID
     */
    public static String getAlipayPartnerID() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("alipay.partner.id");
    }

    /**
     * 获得支付宝接入商户的邮箱地址,即用户名
     *
     * @return 商户用户名
     */
    public static String getAlipayMerchantEmail() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("alipay.merchant.email");
    }

    /**
     * 获得支付宝接入商户的服务窗ID
     *
     * @return 商户服务窗ID
     */
    public static String getAlipayServiceWindowID() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("alipay.app.id");
    }

    /**
     * 获得微信 公众号ID
     */
    public static String getWechatOfficialAccountID() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("wechat.app.id");
    }

    /**
     * 获得微信 公众号的密钥
     */
    public static String getWechatOfficialAccountSecret() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("wechat.secret");
    }

    public static String getString(String key) {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString(key);
    }

    /**
     * 获得微信公众号 服务端配置的 token
     *
     * @return
     */
    public static String getWechatToken() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("wechat.token");
    }

    /**
     * 获得微信公众号 服务端配置的 aes 加密密钥
     *
     * @return
     */
    public static String getWechatAesKey() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("wechat.aes.key");
    }

    /**
     * 获得支付宝相关证书存储路径
     *
     * @return 证书存储路径
     */
    private static String getAlipayKeyDir() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("alipay.merchant.key.path");
    }

    /**
     * 获得支付宝支付用私钥内容
     *
     * @return 支付宝支付用私钥内容
     */
    public static String getAlipayPaymentPrivateKey() {
        if (m_alipayPaymentPrivateKey == null) {
            try {
                m_alipayPaymentPrivateKey = getKeyContent("payment/rsa_private_key_pkcs8.txt");
            } catch (IOException e) {
                log.error(e.getMessage());
            }
        }
        return m_alipayPaymentPrivateKey;
    }

    /**
     * 获得支付宝服务窗用私钥内容
     *
     * @return 支付宝服务窗用私钥内容
     */
    public static String getAlipayWindowPrivateKey() {
        if (m_alipayWindowPrivateKey == null) {
            try {
                m_alipayWindowPrivateKey = getKeyContent("window/rsa_private_key_pkcs8.txt");
            } catch (IOException e) {
                log.error(e.getMessage());
            }
        }
        return m_alipayWindowPrivateKey;
    }

    /**
     * 获得支付宝服务窗用私钥内容
     *
     * @return 支付宝服务窗用私钥内容
     */
    public static String getAlipayWindowPublicKey() {
        if (m_alipayWindowPublicKey == null) {
            try {
                m_alipayWindowPublicKey = getKeyContent("window/rsa_public_key.pem");
            } catch (IOException e) {
                log.error(e.getMessage());
            }
        }
        return m_alipayWindowPublicKey;
    }


    /**
     * 获得证书内容
     *
     * @param filename 证书文件名
     * @return 证书内容
     * @throws IOException
     */
    private static String getKeyContent(String filename) throws IOException {
        String keyPath = getAlipayKeyDir() + "/" + filename;
        log.info("Alipay merchant key path:" + keyPath);
        File file = new File(keyPath);
        if (!file.exists()) {
            throw new FileNotFoundException(keyPath + " file not found.");
        }

        BufferedReader reader = new BufferedReader(new FileReader(file));
        StringBuilder stringBuffer = new StringBuilder();
        String tempString = null;
        while ((tempString = reader.readLine()) != null) {
            if (tempString.startsWith("-----") && tempString.endsWith("-----")) {
                continue;
            }
            stringBuffer.append(tempString);
        }
        reader.close();
        return stringBuffer.toString();
    }

    public static String getCloudHost() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("cloud.host");
    }

    public static int getCloudPort() {
        String port = ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("cloud.port");
        try {
            return Integer.valueOf(port);
        } catch (Exception ex) {
            ex.printStackTrace();
            return 1993;
        }
    }

    public static String getDispatchImUser() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("dispatch.im.user");
    }

    public static String getDispatchPassword() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("dispatch.im.password");
    }

    public static String getChatImageSavePath() {
        return ResourceBundle.getBundle("config_" + getHospitalFlagCode()).getString("chat.image.savepath");
    }
}
