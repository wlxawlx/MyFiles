package com.tencent;

import com.jandar.config.ConfigHandler;
import com.tencent.common.Util;
import com.tencent.protocol.unified_order_protocol.UnifiedOrderReqData;
import com.tencent.protocol.unified_order_protocol.UnifiedOrderResData;

public class Main {

    public static void main(String[] args) {

        try {

            //--------------------------------------------------------------------
            //温馨提示，第一次使用该SDK时请到com.tencent.common.Configure类里面进行配置
            //--------------------------------------------------------------------


            //--------------------------------------------------------------------
            //PART One:基础组件测试
            //--------------------------------------------------------------------

            //1）https请求可用性测试
            // HTTPSPostRquestWithCert.test();

            //2）测试项目用到的XStream组件，本项目利用这个组件将Java对象转换成XML数据Post给API
            // XStreamTest.test();


            //--------------------------------------------------------------------
            //PART Two:基础服务测试
            //--------------------------------------------------------------------

            //1）测试被扫支付API
            //PayServiceTest.test();

            //2）测试被扫订单查询API
            //PayQueryServiceTest.test();

            //3）测试撤销API
            //温馨提示，测试支付API成功扣到钱之后，可以通过调用PayQueryServiceTest.test()，将支付成功返回的transaction_id和out_trade_no数据贴进去，完成撤销工作，把钱退回来 ^_^v
            //ReverseServiceTest.test();

            //4）测试退款申请API
            //RefundServiceTest.test();

            //5）测试退款查询API
            //RefundQueryServiceTest.test();

            //6）测试对账单API
            //DownloadBillServiceTest.test();


            //本地通过xml进行API数据模拟的时候，先按需手动修改xml各个节点的值，然后通过以下方法对这个新的xml数据进行签名得到一串合法的签名，最后把这串签名放到这个xml里面的sign字段里，这样进行模拟的时候就可以通过签名验证了
           // Util.log(Signature.getSignFromResponseString(Util.getLocalXMLString("/test/com/tencent/business/refundqueryserviceresponsedata/refundquerysuccess2.xml")));

            //Util.log(new Date().getTime());
            //Util.log(System.currentTimeMillis());


            //--------------------------------------------------------------------
            //第一步：初始化SDK（只需全局初始化一次即可）
            //--------------------------------------------------------------------
            // WXPay.initSDKConfiguration(
            //         //签名算法需要用到的秘钥
            //         "40a8f8aa8ebe45a40bdc4e0f7307bc66",
            //         //公众账号ID，成功申请公众账号后获得
            //         "wxf5b5e87a6a0fde94",
            //         //商户ID，成功申请微信支付功能之后通过官方发出的邮件获得
            //         "10000097",
            //         //子商户ID，受理模式下必填；
            //         "",
            //         //HTTPS证书在服务器中的路径，用来加载证书用
            //         "C:/wxpay_scanpay_java_cert/10000097.p12",
            //         //HTTPS证书的密码，默认等于MCHID
            //         "10000097"
            // );

            // 准备统一下单参数
            UnifiedOrderReqData unifiedOrderReqData = new UnifiedOrderReqData(
                    // 商品描述
                    "统一下单测试商品",
                    // 商户订单号
                    "201607200001",
                    // 金额
                    1,
                    // 通知地址
                    ConfigHandler.getSelfServiceHost(),
                    // 商品ID
                    "00160001"
            );

            String response = WXPay.requestUnifiedOrderService(unifiedOrderReqData);
            System.out.println("返回的xml信息: "+response);
            UnifiedOrderResData unifiedOrderResData = Util.getObjectFromXML(response, UnifiedOrderResData.class);
            System.out.println(unifiedOrderResData);
        } catch (Exception e){
            Util.log(e.getMessage());
        }

    }

}
