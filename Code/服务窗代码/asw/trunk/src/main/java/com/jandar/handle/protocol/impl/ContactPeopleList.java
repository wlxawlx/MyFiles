package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.AsteriskUtil;
import com.jandar.alipay.util.MapUtil;

public class ContactPeopleList implements Protocol {

    /**
     * @author yubj
     * 获取常用联系人个人和列表
     */
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        UserInfo info = ServiceContext.getHospitalUserInfo();
        String linkmanid = MapUtil.getString(params, "linkmanid");
        List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);

        if (contactsList.size() > 0) {
            Map<String, Object> result = new HashMap<String, Object>();
            List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
            for (ContactPeopleInfo value : contactsList) {
                Map<String, Object> data = new HashMap<>();
                data.put("linkmanid", value.getLinkmanid());
                data.put("label",value.getLabel());
                data.put("name", AsteriskUtil.formatName(value.getName()));
                data.put("phone", AsteriskUtil.formatPhone(value.getPhone()));
                data.put("idcardno", AsteriskUtil.formatId(value.getIdcardno()));
                data.put("address", AsteriskUtil.formatId(value.getAddress()));
                data.put("bindcardflag", value.getBindcardfalg());
                data.put("inpatentflag",value.getInpatentflag());
                data.put("patientid", value.getPatientid());
                data.put("cardno",value.getCardno());
                list.add(data);
            }
            result.put("list", list);
            return result;
        } else {
            throw new HospitalException("亲,没有记录");
        }
    }
}
