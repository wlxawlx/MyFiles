package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.ContactPeopleInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得常用联系人列表
 * Created by yubj on 2016/4/11.
 */
public class GetContactsList {
    public Map<String, Object> getContactsListRequest(String openid, String linkmanid) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("linkmanid", linkmanid);
        return parameters;

    }

    public List<ContactPeopleInfo> getContactsListResponse(List<Map<String, String>> contacts, String linkmanid, List<ContactPeopleInfo> result) {
        for (Map<String, String> map : contacts) {
            String cyxh = map.get("linkmanid");
            if ((linkmanid == null || "".equals(linkmanid)) || linkmanid.equals(cyxh)) {
                ContactPeopleInfo cpinfo = new ContactPeopleInfo();
                cpinfo.setLinkmanid(map.get("linkmanid"));
                cpinfo.setLabel(map.get("label"));
                cpinfo.setName(map.get("name"));
                cpinfo.setPhone(map.get("phone"));
                cpinfo.setIdcardno(map.get("idcardno"));
                cpinfo.setAddress(map.get("address"));
                cpinfo.setCardno(map.get("cardno"));
                if ("未绑卡".equals(map.get("bindcardfalg"))) {
                    cpinfo.setBindcardfalg("0");
                } else {
                    cpinfo.setBindcardfalg("1");
                }
                cpinfo.setInpatentflag(map.get("inpatentflag"));
                cpinfo.setPatientid(map.get("patientid"));
                result.add(cpinfo);
            }
            if (linkmanid != null && linkmanid.equals(cyxh)) {
                break;
            }
        }
        return result;
    }
}
