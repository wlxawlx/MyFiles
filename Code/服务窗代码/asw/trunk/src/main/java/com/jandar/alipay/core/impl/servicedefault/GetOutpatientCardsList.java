package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.OutPatientCardInfo;
import jodd.util.StringUtil;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 获得门诊卡列表
 * Created by yubj on 2016/4/11.
 */
public class GetOutpatientCardsList {

    public Map<String, Object> getOutpatientCardsListRequest(String idcardno, String name) {
        Map<String, Object> parameters = new HashMap<String, Object>();
        parameters.put("idcardno", idcardno);
        parameters.put("name", name);
        return parameters;
    }

    public List<OutPatientCardInfo> getOutpatientCardsListResponse(List<Map<String, String>> process) {
        List<OutPatientCardInfo> list = new ArrayList<OutPatientCardInfo>();
        for (Map<String, String> map : process) {
            OutPatientCardInfo opinfo = new OutPatientCardInfo();
            opinfo.setCardtype(map.get("bklx"));
            opinfo.setCardname(map.get("cardname"));
            opinfo.setCardno(map.get("bkhm"));
            opinfo.setIdcardno(map.get("sfzh"));
            opinfo.setPatientid(map.get("brid"));
            opinfo.setPatientname(map.get("brxm"));
            opinfo.setBirthday(map.get("birthday"));
            opinfo.setPhone(map.get("lxdh"));
//            opinfo.setBalance(map.get("balance"));
//            opinfo.setCost(map.get("cost"));
            if (StringUtil.isBlank(map.get("balance"))) {
                opinfo.setBalance("0.00");
            } else {
                opinfo.setBalance(map.get("balance"));
            }
            if (StringUtil.isBlank(map.get("cost"))) {
                opinfo.setCost("0.00");
            } else {
                opinfo.setCost(map.get("cost"));
            }
            list.add(opinfo);
        }
        return list;
    }
}
