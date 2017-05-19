package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.InhospitalPatientInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;

/**
 * Created by zhufengxiang on 2016-01-05. 住院信息-病人信息列表0.2版本
 */
public class InpatientInfoListProtocol_V2 implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String sfzh = MapUtil.getString(params, "sfzh");
        String name = MapUtil.getString(params, "name");
        InhospitalPatientInfo info = null;
        if (StringUtils.isBlank(sfzh) || StringUtils.isBlank(name)) {
            info = HospitalInfoService.getInstance().inhospitalPatientInfo(userInfo.getSfzh(), userInfo.getYhxm());
        } else {
            info = HospitalInfoService.getInstance().inhospitalPatientInfo(sfzh, name);
        }

        if (info != null) {
            Map<String, Object> map = new HashMap<>();
            map.put("zyhm", info.getInpatientno());
            map.put("brxm", info.getPatientname());
            map.put("sfzh", info.getPatientidcardno());
            map.put("brxb", info.getSex());
            map.put("csny", info.getBirthday());
            map.put("lxdz", info.getAddress());
            map.put("lxdh", info.getPhone());
            map.put("ryrq", info.getAdmitdate());
            map.put("cyrq", info.getDischargedate());
            map.put("zyts", String.valueOf(info.getStayday()));
            map.put("xzmc", info.getXzmc());
            map.put("bqmc", info.getEndemicarea());
            map.put("brch", info.getBrch());
            map.put("ksmc", info.getDepartname());
            map.put("ylhj", info.getYlhj());
            map.put("lwhj", info.getLwhj());
            map.put("zfje", info.getZfje());
            map.put("jkje", info.getJkje());
            map.put("zyzt", info.getZyzt());
            map.put("ysxm", info.getDoctorcode());
            return map;
        } else {
            throw new HospitalException("当前未住院。");
        }
    }
}
