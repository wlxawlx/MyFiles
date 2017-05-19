package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderHistoryInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import org.apache.commons.lang.StringUtils;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by zhufengxiang on 2016-01-04. 住院充值-订单列表0.1版
 */
public class InpatientTopUpOrderListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        List<RechargeOrderHistoryInfo> values = HospitalInfoService.getInstance().getInhospitalRechargeOrdersList(requiredParams(params));
        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<String, Object>();
            List<Map<String, Object>> list = new ArrayList<>();
            for (RechargeOrderHistoryInfo info : values) {
                Map<String, Object> items = new HashMap<String, Object>();
                items.put("openid", info.getOpenid());
                items.put("wxorderno", info.getPaymenttradeno());
                items.put("orderno", info.getTradeno());
                items.put("orderstatus", info.getStatus());
                items.put("ordername", info.getSubject());

                // 总金额的修改
                String orderm = info.getMoney();
                if (StringUtils.isBlank(orderm)) {
                    items.put("orderm", "0.00");
                } else {
                    double d = Double.parseDouble(orderm);
                    DecimalFormat df = new DecimalFormat("########0.00");
                    orderm = df.format(d);
                    items.put("orderm", orderm);
                }


                items.put("ctime", DateUtil.dateFormat(info.getCtime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
                items.put("paytime", DateUtil.dateFormat(info.getPaytime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
                items.put("rtntime", DateUtil.dateFormat(info.getRtntime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
//				items.put("yhid", map.get("yhid"));	//无法从基础数据库获取到
//				items.put("zyh", map.get("zyh"));	//无法从基础数据库获取到
                items.put("zyhm", info.getInpatientno());
                items.put("brxm", info.getPatientname());
                items.put("sfzh", info.getPatientidcardno());
//				items.put("usertext", map.get("usertext"));	//无法从基础数据库获取到
                list.add(items);
            }
            result.put("list", list);
            return result;
        } else {
            throw new HospitalException("订单列表为空");
        }
    }

    private String requiredParams(Map<String, Object> params) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        // 其它校验
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        return userInfo.getAlipayUserId();
    }
}
