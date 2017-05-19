package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.util.InfoValidateUtil;

import java.text.ParseException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 添加常用联系人
 * Created by yubj on 2016/4/11.
 */
public class AddContact {
    public Map<String, Object> addContactRequest(String openid, String label, String name, String phone, String idcardno,
                                                       String address)throws HospitalException{
        String idcardnovalidate = null;
        //验证身份证号格式是否正确
        try {
            idcardnovalidate = InfoValidateUtil.IDCardValidate(idcardno);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        //验证手机号格式是否是11位
        boolean phonevalidate = InfoValidateUtil.isMobile(phone);
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("openid", openid);
        parameters.put("label", label);
        parameters.put("name", name);
        if (phonevalidate == false) {
            throw new HospitalException("手机号码输入有误,请重新输入");
        } else {
            parameters.put("phone", phone);
        }
        if (idcardnovalidate == "") {
            parameters.put("idcardno", idcardno);
        } else {
            throw new HospitalException(idcardnovalidate);
        }
        parameters.put("address", address);
        return parameters;
    }

    public ContactPeopleInfo addContactResponse(List<Map<String, String>> process){
        ContactPeopleInfo cpinfo = new ContactPeopleInfo();
        if (process.size() > 0) {
            Map<String, String> re = process.get(0);
            cpinfo.setLabel(re.get("label"));
            cpinfo.setName(re.get("name"));
            cpinfo.setLinkmanid(re.get("linkmanid"));
        }
        return cpinfo;
    }
}
