package com.jandar.handle.protocol.impl.wzsrmyy;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.CourseOrderHistoryInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;
import jodd.util.StringUtil;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * 技能中心-已课程报名列表
 */
public class CourseRegistrationListProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        return process(MapUtil.getString(params, "orderno"), "");
    }

    /**
     * 通过支付宝用户ID 和 订单号获得订单信息
     *
     * @param orderno      订单号
     * @param alipayUserId 支付宝用户号
     * @return
     * @throws HospitalException
     */
    public List<Map<String, String>> process(String orderno, String alipayUserId) throws HospitalException {

        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        List<CourseOrderHistoryInfo> values = handler.getCourseOrderList(alipayUserId);

        List<Map<String, String>> result = new ArrayList<>();
        for (CourseOrderHistoryInfo value : values) {
            String tstr = value.getTradeno();

            if (StringUtil.isEmpty(orderno) || orderno.equals(tstr)) {
                Map<String, String> map = new HashMap<>();

                map.put("openid", value.getOpenid());
                map.put("wxorderno", value.getPaymenttradeno());
                map.put("orderno", value.getTradeno());
                map.put("orderstatus", value.getStatus());
                map.put("ordername", value.getSubject());

                String orderm = value.getMoney();
                if (StringUtil.isBlank(orderm)) {
                    map.put("orderm", "0.00");
                } else {
                    double d = Double.parseDouble(orderm);
                    DecimalFormat df = new DecimalFormat("########0.00");
                    orderm = df.format(d);
                    map.put("orderm", orderm);
                }

                map.put("ctime", DateUtil.dateFormat(value.getCtime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
                map.put("paytime", DateUtil.dateFormat(value.getPaytime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
                map.put("rtntime", DateUtil.dateFormat(value.getRtntime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));

//                yhid	用户 ID	string	是
                map.put("fyxh", value.getCourseCode());
                map.put("fymc", value.getCoursename());
                map.put("fysl", value.getPeople());
                map.put("fydj", value.getPrice());
                map.put("brxm", value.getPatientname());
                map.put("sfzh", value.getPatientidcardno());
                map.put("usertext", value.getUserRemark());

                result.add(map);
            }

            if (!StringUtil.isEmpty(orderno) && orderno.equals(tstr)) {
                break;
            }
        }

        if (result.isEmpty()) {
            throw new HospitalException("没有相应的报名记录");
        }

        return result;
    }
}
