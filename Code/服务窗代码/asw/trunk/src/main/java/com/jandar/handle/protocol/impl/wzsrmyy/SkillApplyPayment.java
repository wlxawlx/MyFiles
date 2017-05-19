package com.jandar.handle.protocol.impl.wzsrmyy;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.core.struct.wzsrmyy.CourseInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import jodd.util.StringUtil;

import java.math.BigDecimal;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by zzw on 16/1/21.
 * 技能中心-课程报名缴费
 */
public class SkillApplyPayment implements Protocol {

    final static String m_orderName = "技能中心报名支付宝缴费";

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        StringBuffer coursename = new StringBuffer();
        StringBuffer money = new StringBuffer();
        String orderno = requiredParams(params, coursename, money);
        if (StringUtil.isNotEmpty(orderno)) {
            String enterpeople = MapUtil.getString(params, "enterpeople");
            String userremark = MapUtil.getString(params, "userremark");
            Map<String, String> obligate = new HashMap<>();
            obligate.put("course", coursename.toString());
            obligate.put("people", enterpeople);
            obligate.put("remark", userremark);

//            String obligateString = JSONObject.fromObject(obligate).toString();
//            obligateString = URLEncoder.encode(obligateString);

            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.OtherOrder,
                    userInfo.getAlipayUserId(),
                    userInfo.getYhxm(),
                    orderno, m_orderName,
                    money.toString(),
                    null,
                    MapUtil.getString(params, "successurl"),
                    "", "", "", "");

            Map<String, String> result = new HashMap<>();
            result.put("orderno", orderno);
            result.put("aliwappayurl", aliwappayurl);
            return result;
        } else {
            throw new HospitalException("住院预缴-创建订单失败");
        }
    }

    private String requiredParams(Map<String, Object> params, StringBuffer coursename, StringBuffer outMoney)
            throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();

        // 校验
        String coursecode = MapUtil.getString(params, "coursecode");
        String courseid = MapUtil.getString(params, "courseid");
        String enterpeople = MapUtil.getString(params, "enterpeople");
        String userremark = MapUtil.getString(params, "userremark");

        if (StringUtil.isBlank(coursecode)) {
            throw new HospitalException("课程分类代码不能为空");
        }

        if (StringUtil.isBlank(courseid)) {
            throw new HospitalException("课程代码不能为空");
        }

        Integer people = 0;
        try {
            people = Integer.valueOf(enterpeople);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        if (people <= 0) {
            throw new HospitalException("报名人数不能小于1");
        }

        if (userremark == null) {
            userremark = "";
        }

        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);

        /* 获得相应课程信息,并计算费用 */
        BigDecimal money = null;
        try {
            List<CourseInfo> courses = handler.getCourseList(coursecode, courseid);
            if (courses != null && courses.size() >= 1) {
                CourseInfo info = courses.get(0);
                coursename.append(info.getCoursename());
                money = new BigDecimal(info.getCoursefee());
                money = money.multiply(new BigDecimal(people));
            }
        } catch (HospitalException ex) {
            ex.printStackTrace();
            throw new HospitalException("未知的课程");
        }

        if (money == null) {
            throw new HospitalException("课程信息错误");
        }

        outMoney.append(money.toPlainString());

        String orderNo = handler.courseRegisterFor(courseid, people, money.toPlainString(), m_orderName, userremark);
        return orderNo;
    }
}
