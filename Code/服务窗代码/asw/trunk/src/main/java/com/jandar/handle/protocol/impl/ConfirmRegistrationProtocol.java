package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.OutpatientOrderRequest;
import com.jandar.alipay.hospital.UserInfo;
import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.OutpatientOrder;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

public class ConfirmRegistrationProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String pblsh = MapUtil.getString(params, "pblsh");
        if (StringUtils.isBlank(pblsh)) {
            throw new HospitalException("缺少排班流水号");
        }
        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        OutpatientOrder values = handler.confirmRegistration(requiredParams(params));
        if (values != null) {
            Map<String, String> result = new HashMap<String, String>();
            result.put("orderid", values.getPreengageseq());
            result.put("ordermsg", values.getOrdermsg());
            return result;
        } else {
            throw new HospitalException("挂号失败");
        }

    }

    private OutpatientOrderRequest requiredParams(Map<String, Object> params) throws HospitalException {

        // 其它校验
        String pblsh = MapUtil.getString(params, "pblsh");
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        OutpatientOrderRequest required = new OutpatientOrderRequest();
        required.setOpenid(userInfo.getAlipayUserId());
        required.setOrderseq(pblsh);

        String linkmanid = MapUtil.getString(params, "linkmanid");
        if (org.apache.commons.lang.StringUtils.isNotBlank(linkmanid)) {
            // 去数据库中查找联系人信息
            List<ContactPeopleInfo> values = HospitalInfoService.getInstance().getContactsList(userInfo.getAlipayUserId(), linkmanid);

            if (values.size() <= 0) {
                throw new HospitalException("获取常用联系人失败");
            }
            for (ContactPeopleInfo per : values) {
                if (linkmanid.equals(per.getLinkmanid())) {
                    required.setPatientname(per.getName());
                    required.setPatientidcardno(per.getIdcardno());
                    required.setPatientphone(per.getPhone());
                    required.setPatientaddress(per.getAddress());

                    return required;
                }
            }
        }

        // 这里key要对应接口的参数名
        required.setPatientname(userInfo.getYhxm());
        required.setPatientidcardno(userInfo.getSfzh());
        required.setPatientphone(userInfo.getLxdh());
        required.setPatientaddress(userInfo.getLxdz());

        return required;
    }

}
