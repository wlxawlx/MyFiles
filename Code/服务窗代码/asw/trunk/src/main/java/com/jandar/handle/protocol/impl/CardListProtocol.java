package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
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
import com.jandar.alipay.util.MapUtil;
import jodd.util.StringUtil;

/*
 * @author yubj
 * 
 * 就诊卡列表。
 */
public class CardListProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        //判断输入的参数是否有病人姓名和身份证,如果有就用这些参数,如果没有就使用当前用户的姓名和身份证
        UserInfo info = new UserInfo();
        String paitentName = MapUtil.getString(params, "brxm");
        String paitentId = MapUtil.getString(params, "sfz");
        if (paitentName != null && paitentId != null) {
            info.setYhxm(paitentName);
            info.setSfzh(paitentId);
        } else {
            info = ServiceContext.getHospitalUserInfo();
        }
        List<OutPatientCardInfo> values = HospitalInfoService.getInstance().getOutpatientCardsList(info.getSfzh(), info.getYhxm());

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
					item.put("cardtype", value.getCardname());
					item.put("cardno", value.getCardno());
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
				
		}else{
			throw new HospitalException("该身份证在医院未建档");
		}
	}
}