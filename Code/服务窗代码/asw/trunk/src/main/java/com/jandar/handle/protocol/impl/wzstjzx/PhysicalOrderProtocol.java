package com.jandar.handle.protocol.impl.wzstjzx;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import jodd.util.StringUtil;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.PhysicalOrder;
import com.jandar.alipay.core.struct.PhysicalOrderRequest;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.CardIDUtil;
import com.jandar.alipay.util.MapUtil;
import com.jandar.util.DateUtil;

/*
 * @author jinliangjin86
 * 
 * 预定体检套餐
 */

public class PhysicalOrderProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params)
            throws HospitalException {
        String hyzk = MapUtil.getString(params, "hyzk");
        if (StringUtil.isBlank(hyzk)) {
            throw new HospitalException("请输入婚姻状况");
        }
        String yyrq = MapUtil.getString(params, "yyrq");
        if (StringUtil.isBlank(yyrq)) {
            throw new HospitalException("请输入预约日期");
        }
        String tcid = MapUtil.getString(params, "tcid");
        if (StringUtil.isBlank(tcid)) {
            throw new HospitalException("请输入套餐ID");
        }


        PhysicalOrderRequest p = new PhysicalOrderRequest();
        p.setIsMarried(hyzk);
        p.setOrderDate(yyrq);
        p.setPackagesID(tcid);
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String linkmainid = StringUtils.isBlank(MapUtil.getString(params,
                "linkmainid")) ? "" : MapUtil.getString(params, "linkmainid");
        if (linkmainid.equals("")) {
            CardIDUtil cardIDUtil = new CardIDUtil(userInfo.getSfzh());
            p.setBirthday(DateUtil.formatDateWithString(
                    cardIDUtil.getBirthday(), "yyyy-MM-dd"));
            p.setCardID(userInfo.getSfzh());
            p.setPatientName(userInfo.getYhxm());
            p.setPatientSex(cardIDUtil.getGender());
            p.setPhone(userInfo.getLxdh());
            p.setUserID(userInfo.getYhid());
        } else {
            List<ContactPeopleInfo> contactsList = HospitalInfoService
                    .getInstance().getContactsList(userInfo.getAlipayUserId(),
                            linkmainid);
            if (contactsList.size() == 0) {
                throw new HospitalException("该联系人不存在");
            }
            String patientid=contactsList.get(0).getPatientid();
            if(StringUtil.isBlank(patientid)||"0".equals(patientid)){
                throw new HospitalException("该联系人未绑卡");
            }
            CardIDUtil cardIDUtil = new CardIDUtil(contactsList.get(0).getIdcardno());
            p.setBirthday(DateUtil.formatDateWithString(
                    cardIDUtil.getBirthday(), "yyyy-MM-dd"));
            p.setCardID(contactsList.get(0).getIdcardno());
            p.setPatientName(contactsList.get(0).getName());
            p.setPatientSex(cardIDUtil.getGender());
            p.setPhone(contactsList.get(0).getPhone());
            p.setUserID(userInfo.getYhid());
        }
        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        PhysicalOrder values = handler.physicalOrder(p);
        Map<String, String> result = new HashMap<String, String>();
        result.put("state", values.getState());
        return result;
    }
}
