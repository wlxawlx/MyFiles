package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.AsteriskUtil;

/*
 * @author yubj
 * 
 * 获得已绑卡协议。
 */
public class AlreadyBindCardProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        UserInfo info = ServiceContext.getHospitalUserInfo();
        String brid = info.getBrid();
        List<OutPatientCardInfo> values = HospitalInfoService.getInstance().getOutpatientCardsList(info.getSfzh(), info.getYhxm());

        Map<String, String> item = new HashMap<String, String>();

        for (OutPatientCardInfo map : values) {
            if (brid.equals(map.getPatientid())) {

                item.put("cardno", map.getCardno());
                item.put("cardtype", map.getCardname());
                item.put("idcardno", AsteriskUtil.formatId(map.getIdcardno()));
                item.put("patientid", map.getPatientid());
                item.put("patientname", AsteriskUtil.formatName(map.getPatientname()));
                item.put("birthday", map.getBirthday());
                item.put("phone", AsteriskUtil.formatPhone(map.getPhone()));
                item.put("balance", map.getBalance());
                item.put("cost", map.getCost());
                break;
            }
        }

        if (item.isEmpty()) {
            throw new HospitalException("就诊卡列表为空");
        }

        return item;
    }
}
