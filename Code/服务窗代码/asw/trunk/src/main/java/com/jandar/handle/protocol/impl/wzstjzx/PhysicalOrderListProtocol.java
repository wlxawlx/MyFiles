package com.jandar.handle.protocol.impl.wzstjzx;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.PhysicalOrderInfo;
import com.jandar.alipay.core.struct.PhysicalStatusInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.CardIDUtil;

/*
 * @author jinliangjin86
 * 
 * 体检已预约列表
 */

public class PhysicalOrderListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params)
            throws HospitalException {
        HISServiceHandlerWZSRMYY handler = HospitalInfoService
                .getRealInstance(HISServiceHandlerWZSRMYY.class);
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        List<PhysicalOrderInfo> values = handler.physicalOrderList(userInfo
                .getYhid());
        List<Map<String, String>> list = new ArrayList<Map<String, String>>();
        Map<String, String> result = new HashMap<String, String>();
        for (PhysicalOrderInfo p : values) {
            List<PhysicalStatusInfo> val=handler.getPhysicalStatusInfo(p.getPhone());
            CardIDUtil cardIDUtil = new CardIDUtil(p.getCardID());
            result.put("tjbh", p.getPhysicalID());
            result.put("yylsh", p.getOrderID());
            result.put("yhid", p.getUserID());
            result.put("yyrq", p.getOrderDate());
            result.put("brxm", p.getPatientName());
            result.put("sfzh", p.getCardID());
            result.put("lxdh", p.getPhone());
            result.put("tcmc", p.getPackagesName());
            result.put("tcjg", p.getPackagesPrice());
            result.put("xb", cardIDUtil.getGender());
            result.put("nl",
                    cardIDUtil.getAgeByBirthday(cardIDUtil.getBirthday()) + "");
            result.put("tjzt", val.get(0).getPhysicalStatus());
            result.put("yyzt", p.getOrderStatus());
            list.add(result);
        }
        return list;
    }
}
