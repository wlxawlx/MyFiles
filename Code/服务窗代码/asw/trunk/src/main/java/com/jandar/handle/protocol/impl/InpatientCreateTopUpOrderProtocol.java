package com.jandar.handle.protocol.impl;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.OpCodeContext;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by zhufengxiang on 2016-01-04.
 * 住院充值， 创建订单01版,给自己充
 */

public class InpatientCreateTopUpOrderProtocol implements Protocol {

    final static String m_orderName = "支付宝住院预缴";

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        List<Map<String, String>> values = HospitalInfoService.getInstance().oldProcess(
                OpCodeContext.F006, requiredParams(params));
        if (values.size() > 0) {
            Map<String, String> result = values.get(0);
            String orderno = (String) result.get("orderno");
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.InHospitalOrder,
                    userInfo.getAlipayUserId(),
                    userInfo.getYhxm(),
                    orderno, m_orderName,
                    (String) params.get("money"),
                    MapUtil.getString(params, "showurl"),
                    null,
                    "", "", "", null);
            result.put("aliwappayurl", aliwappayurl);
//            result.put("orderno", orderno);
            return result;
        } else {
            throw new HospitalException("住院预缴-创建订单失败");
        }
    }

    private Map<String, Object> requiredParams(Map<String, Object> params)
            throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        // 其它校验
        String money = MapUtil.getString(params, "money");
        if (StringUtils.isBlank(money)) {
            throw new HospitalException("充值金额不能为空");
        }

        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        required.put("yhid", userInfo.getYhid());
        required.put("openid", userInfo.getAlipayUserId());

        String sfzh = userInfo.getSfzh();
        Map<String, Object> param = new HashMap<String, Object>();
        if (StringUtils.isBlank(sfzh)) {
            throw new HospitalException("缺少身份证号");
        }
        param.put("sfzh", sfzh);
        List<Map<String, String>> result = HospitalInfoService.getInstance().oldProcess(
                OpCodeContext.G007, param);

        if (result.size() == 0) {
            throw new HospitalException("当前没有住院");
        }
        String zyhm = result.get(0).get("zyhm");

        // 这里key要对应医院端接口的参数名
        required.put("zyhm", zyhm);
        required.put("orderm", money);
        required.put("ordername", m_orderName);

        return required;
    }
}
