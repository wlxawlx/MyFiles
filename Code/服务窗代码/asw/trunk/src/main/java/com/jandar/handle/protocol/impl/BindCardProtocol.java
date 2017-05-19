package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;

/*
 * @author yubj
 * 
 *绑定就诊卡。
 */
public class BindCardProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        HospitalInfoService.getInstance().unbindOutpatientCard(userInfo.getAlipayUserId());
        String cardno=MapUtil.getString(params, "cardno");
        if (StringUtils.isBlank(MapUtil.getString(params, "cardno"))) {
            throw new HospitalException("就诊卡卡号不能为空");
        }
        String patientid=MapUtil.getString(params, "patientid");
        if (StringUtils.isBlank(MapUtil.getString(params, "patientid"))) {
            throw new HospitalException("还未绑卡");
        }
        OutPatientCardInfo values = HospitalInfoService.getInstance().bindOutpatientCard(userInfo.getAlipayUserId(), cardno, patientid);
        if (values!=null) {

            /** 更新会话存储中的病人ID,防止出现没有重新登录时已经绑了卡,还提示未绑卡的问题 */
            UserInfo info = ServiceContext.getHospitalUserInfo();
            info.setBrid(MapUtil.getString(params, "patientid"));
            
            Map<String, String> item = new HashMap<>();
            item.put("cardno", values.getCardno());
            return item;
        } else {
            throw new HospitalException("綁定就诊卡失败");
        }

    }
}
