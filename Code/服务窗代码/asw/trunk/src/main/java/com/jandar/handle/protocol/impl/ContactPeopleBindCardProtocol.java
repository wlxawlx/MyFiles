package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang3.StringUtils;

import java.util.HashMap;
import java.util.Map;

/*
 * @author yubj
 * 
 * 常用联系人绑卡
 */
public class ContactPeopleBindCardProtocol implements Protocol {
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String linkmanid = MapUtil.getString(params, "linkmanid");
        if (StringUtils.isBlank(MapUtil.getString(params, "linkmanid"))) {
            throw new HospitalException("联系人序号不能为空");
        }
        HospitalInfoService.getInstance().unbindContactOutpatientCard(userInfo.getAlipayUserId(), linkmanid);
        String cardno = MapUtil.getString(params, "cardno");
        String patientid = MapUtil.getString(params, "patientid");
        if (StringUtils.isBlank(MapUtil.getString(params, "cardno"))) {
            throw new HospitalException("就诊卡卡号不能为空");
        }
        if (StringUtils.isBlank(MapUtil.getString(params, "patientid"))) {
            throw new HospitalException("还未绑卡");
        }
        OutPatientCardInfo values = HospitalInfoService.getInstance().bindContactOutpatientCard(userInfo.getAlipayUserId(), linkmanid, cardno, patientid);
        if (values != null) {
            Map<String, Object> item = new HashMap<String, Object>();
            item.put("patientid", values.getPatientid());
            return item;
        } else {
            throw new HospitalException("常用联系人綁卡失败");
        }
    }
}
