package com.tencent.protocol.unified_order_protocol;

import com.tencent.common.Configure;
import com.tencent.common.RandomStringGenerator;
import com.tencent.common.Signature;

import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by zhufengxiang on 2016/07/18.
 * 统一下单请求
 */
public class UnifiedOrderReqData {
    private String appid = "";                 // 是   公众账号ID
    private String mch_id = "";                // 是   商户号
    private String device_info = "WEB";        // 否   设备号
    private String nonce_str = "";             // 是   随机字符串
    private String sign = "";                  // 是   签名
    private String body = "";                  // 是   商品描述
    // private String detail = "";                // 否   商品详情
    // private String attach = "";                // 否   附加数据
    private String out_trade_no;               // 是   商户订单号
    private String fee_type = "CNY";           // 否   货币类型
    private Integer total_fee = 0;             // 是   总金额
    private String spbill_create_ip = "";      // 是   终端IP
    // private String time_start = "";            // 否   交易起始时间
    // private String time_expire = "";           // 否   交易结束时间
    // private String goods_tag = "";             // 否   商品标记
    private String notify_url = "";            // 是   通知地址
    private String trade_type = "NATIVE";      // 是   交易类型
    private String product_id = "";            // 否   商品ID         此时trade_type = native 所以此参数为必须。
    // private String limit_pay = "";             // 否   指定支付方式
    // private String openid = "";                // 否   用户标识


    public UnifiedOrderReqData(String body,
                               String out_trade_no, Integer total_fee, String notify_url, String product_id) {
        setAppid(Configure.getAppid());
        setMch_id(Configure.getMchid());
        setNonce_str(RandomStringGenerator.getRandomStringByLength(32));
        setSpbill_create_ip(Configure.getIP());
        this.body = body;
        this.out_trade_no = out_trade_no;
        this.total_fee = total_fee;
        this.notify_url = notify_url;
        this.product_id = product_id;

        // 生成签名, 然后设置成属性
        String sign = Signature.getSign(toMap());
        setSign(sign);
    }

    // ------------------------------------------GETTER & SETTER-------------------------------------------------
    public String getAppid() {
        return appid;
    }

    public void setAppid(String appid) {
        this.appid = appid;
    }

    public String getMch_id() {
        return mch_id;
    }

    public void setMch_id(String mch_id) {
        this.mch_id = mch_id;
    }

    public String getDevice_info() {
        return device_info;
    }

    public void setDevice_info(String device_info) {
        this.device_info = device_info;
    }

    public String getNonce_str() {
        return nonce_str;
    }

    public void setNonce_str(String nonce_str) {
        this.nonce_str = nonce_str;
    }

    public String getSign() {
        return sign;
    }

    public void setSign(String sign) {
        this.sign = sign;
    }

    public String getBody() {
        return body;
    }

    public void setBody(String body) {
        this.body = body;
    }

    // public String getDetail() {
    //     return detail;
    // }
    //
    // public void setDetail(String detail) {
    //     this.detail = detail;
    // }
    //
    // public String getAttach() {
    //     return attach;
    // }
    //
    // public void setAttach(String attach) {
    //     this.attach = attach;
    // }

    public String getOut_trade_no() {
        return out_trade_no;
    }

    public void setOut_trade_no(String out_trade_no) {
        this.out_trade_no = out_trade_no;
    }

    public String getFee_type() {
        return fee_type;
    }

    public void setFee_type(String fee_type) {
        this.fee_type = fee_type;
    }

    public Integer getTotal_fee() {
        return total_fee;
    }

    public void setTotal_fee(Integer total_fee) {
        this.total_fee = total_fee;
    }

    public String getSpbill_create_ip() {
        return spbill_create_ip;
    }

    public void setSpbill_create_ip(String spbill_create_ip) {
        this.spbill_create_ip = spbill_create_ip;
    }

    // public String getTime_start() {
    //     return time_start;
    // }
    //
    // public void setTime_start(String time_start) {
    //     this.time_start = time_start;
    // }
    //
    // public String getTime_expire() {
    //     return time_expire;
    // }
    //
    // public void setTime_expire(String time_expire) {
    //     this.time_expire = time_expire;
    // }
    //
    // public String getGoods_tag() {
    //     return goods_tag;
    // }
    //
    // public void setGoods_tag(String goods_tag) {
    //     this.goods_tag = goods_tag;
    // }

    public String getNotify_url() {
        return notify_url;
    }

    public void setNotify_url(String notify_url) {
        this.notify_url = notify_url;
    }

    public String getTrade_type() {
        return trade_type;
    }

    public void setTrade_type(String trade_type) {
        this.trade_type = trade_type;
    }

    public String getProduct_id() {
        return product_id;
    }

    public void setProduct_id(String product_id) {
        this.product_id = product_id;
    }

    // public String getLimit_pay() {
    //     return limit_pay;
    // }
    //
    // public void setLimit_pay(String limit_pay) {
    //     this.limit_pay = limit_pay;
    // }
    //
    // public String getOpenid() {
    //     return openid;
    // }
    //
    // public void setOpenid(String openid) {
    //     this.openid = openid;
    // }

    public Map<String,Object> toMap(){
        Map<String,Object> map = new HashMap<String, Object>();
        Field[] fields = this.getClass().getDeclaredFields();
        for (Field field : fields) {
            Object obj;
            try {
                obj = field.get(this);
                if(obj!=null){
                    map.put(field.getName(), obj);
                }
            } catch (IllegalArgumentException e) {
                e.printStackTrace();
            } catch (IllegalAccessException e) {
                e.printStackTrace();
            }
        }
        return map;
    }
}
