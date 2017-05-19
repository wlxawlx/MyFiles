package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.AsteriskUtil;
import com.jandar.alipay.util.MapUtil;
import com.jandar.util.StringUtil;

/*
 * @author yubj
 * 
 * 获得常用联系人已绑卡信息
 */
public class ContactPeopleAlreadyBindCardProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        String linkmanid = MapUtil.getString(params, "linkmanid");
        if (StringUtil.isBlank(linkmanid)) {
            throw new HospitalException("联系人序号不能为空");
        }
        List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);

        String idcardno = null;
        String brid = null;
        String name=null;
        if (contactsList.size() > 0) {
            ContactPeopleInfo value = contactsList.get(0);
            idcardno = value.getIdcardno();
            name=value.getName();
            brid = value.getPatientid();
        }
        if (StringUtil.isBlank(idcardno)) {
            throw new HospitalException("未知的联系人");
        }

        List<OutPatientCardInfo> value = HospitalInfoService.getInstance().getOutpatientCardsList(idcardno, name);
        Map<String, Object> item = new HashMap<String, Object>();
        for (OutPatientCardInfo map : value) {
            String tbrid = map.getPatientid();

            if (brid.equals(tbrid)) {
                item.put("cardno", map.getCardno());
                item.put("cardtype", map.getCardname());
                item.put("idcardno", AsteriskUtil.formatId(map.getIdcardno()));
                item.put("patientid", tbrid);
                item.put("patientname", AsteriskUtil.formatName(map.getPatientname()));
                item.put("birthday", map.getBirthday());
                item.put("phone", AsteriskUtil.formatPhone(map.getPhone()));
                item.put("balance", map.getBalance());
                item.put("cost", map.getCost());
                break;
            }
        }

        if (item.isEmpty()) {
            throw new HospitalException("联系人未绑卡");
        }
        return item;
    }
}
