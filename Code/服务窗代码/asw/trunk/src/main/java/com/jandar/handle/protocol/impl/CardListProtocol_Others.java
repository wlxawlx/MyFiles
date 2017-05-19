package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.AsteriskUtil;
import com.jandar.alipay.util.MapUtil;
import jodd.util.StringUtil;
import org.apache.commons.lang.StringUtils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * @author yubj
 * 
 * 获取医院就诊卡信息_他人
 */
public class CardListProtocol_Others implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        UserInfo info = ServiceContext.getHospitalUserInfo();
        String linkmanid = MapUtil.getString(params, "linkmanid");
        List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);
        String idcardno = null;
        String name=null;
//        if (contactsList.size() > 0) {
//            ContactPeopleInfo value = contactsList.get(0);
//            idcardno = value.getIdcardno();
//        }
        for(ContactPeopleInfo contactPeopleInfo:contactsList){
        	if(linkmanid.equals(contactPeopleInfo.getLinkmanid())){
                name=contactPeopleInfo.getName();
        		idcardno=contactPeopleInfo.getIdcardno();
        		break;
        	}
        }
        if (StringUtils.isBlank(idcardno)) {
            throw new HospitalException("未知的联系人");
        }


       // List<Map<String, String>> values = HospitalInfoService.getInstance().oldProcess(OpCodeContext.C003, required);
        List<OutPatientCardInfo> values=HospitalInfoService.getInstance().getOutpatientCardsList(idcardno, name);
        if (values.size() > 0) {
            String cardno = MapUtil.getString(params, "cardno");
            if (cardno == null) {
                cardno = "";
            }
            Map<String, Object> result = new HashMap<>();
            List<Map<String, Object>> list = new ArrayList<>();
            for (OutPatientCardInfo value : values) {
                Map<String, Object> item = new HashMap<>();
                if (StringUtil.isBlank(cardno) || cardno.equals(value.getCardno())) {
                    item.put("cardflag", value.getCardtype());
                    item.put("cardno", value.getCardno());
                    item.put("cardtype", value.getCardname());
                    item.put("idcardno", AsteriskUtil.formatId(value.getIdcardno()));
                    item.put("patientid", value.getPatientid());
                    item.put("patientname", AsteriskUtil.formatName(value.getPatientname()));
                    item.put("birthday", value.getBirthday());
                    item.put("phone", AsteriskUtil.formatPhone(value.getPhone()));
                    item.put("balance", value.getBalance());
                    item.put("cost", value.getCost());
                    list.add(item);
                }
                if (!StringUtil.isBlank(cardno) && cardno.equals(value.getCardno())) {
                    break;
                }
            }
            result.put("list", list);
            return result;

        } else {
            throw new HospitalException("该身份证在医院未建档");
        }
    }
}
