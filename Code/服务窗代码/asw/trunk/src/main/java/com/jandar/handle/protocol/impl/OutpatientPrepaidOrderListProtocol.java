package com.jandar.handle.protocol.impl;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderHistoryInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.DateUtil;

/*
 * @author yubj
 * 
 * 门诊充值-订单列表
 */
public class OutpatientPrepaidOrderListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	UserInfo userinfo=ServiceContext.getHospitalUserInfo();
         List<RechargeOrderHistoryInfo> values = HospitalInfoService.getInstance().getOutpatientRechargeOrdersList(userinfo.getAlipayUserId());
        if (values.size() > 0) {
        	   // 总金额、创建时间、付款时间、退款时间的修改
            List<Map<String, String>> myValues = new ArrayList<Map<String, String>>();

            for (RechargeOrderHistoryInfo info : values) {
                Map<String, String> item = new HashMap<String, String>();
                item.put("openid", info.getOpenid());
                item.put("wxorderno", info.getPaymenttradeno());
                item.put("orderno", info.getTradeno());
                item.put("orderstatus", info.getStatus());
                item.put("ordername", info.getSubject());

                // 总金额的修改
                String orderm = info.getMoney();
                if (StringUtils.isBlank(orderm)) {
                    item.put("orderm", "0.00");
                } else {
                    double d = Double.parseDouble(orderm);
                    DecimalFormat df = new DecimalFormat("########0.00");
                    orderm = df.format(d);
                    item.put("orderm", orderm);
                }


                // 创建时间、付款时间、退款时间的修改
                item.put("ctime", DateUtil.dateFormat(info.getCtime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
                item.put("paytime", DateUtil.dateFormat(info.getPaytime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
                item.put("rtntime", DateUtil.dateFormat(info.getRtntime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));

//				item.put("yhid", info.getOpenid());
                item.put("brid", info.getPatientid());
                item.put("brxm", info.getPatientname());
                item.put("sfzh", info.getPatientidcardno());

                myValues.add(item);
            }

            Map<String, Object> result = new HashMap<String, Object>();
            result.put("list", myValues);
            return result;
        } else {
            throw new HospitalException("订单列表为空");
        }
    }
}
