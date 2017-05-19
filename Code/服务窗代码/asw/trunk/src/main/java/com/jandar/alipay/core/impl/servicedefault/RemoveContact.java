package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.ContactPeopleInfo;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 删除常用联系人
 * Created by yubj on 2016/4/11.
 */
public class RemoveContact {
    public Map<String, Object> removeContactRequest(String openid, String linkmanid){
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("linkmanid", linkmanid);
        return parameters;
    }

    public ContactPeopleInfo removeContactResponse( List<Map<String, String>> process){
        ContactPeopleInfo cpinfo = new ContactPeopleInfo();
        if (process.size() > 0) {
            Map<String, String> re = process.get(0);
            cpinfo.setLinkmanid(re.get("linkmanid"));
            cpinfo.setName(re.get("name"));
        }
        return cpinfo;
    }
}
