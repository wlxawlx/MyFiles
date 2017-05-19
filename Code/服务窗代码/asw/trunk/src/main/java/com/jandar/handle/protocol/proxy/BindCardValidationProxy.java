package com.jandar.handle.protocol.proxy;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.List;
import java.util.Map;

/**
 * 如果linkmanid不为空，判断联系人是否绑卡
 * 否则检查当前用户是否已经绑定了就诊卡
 * Created by zzw on 16/1/12.
 */
public class BindCardValidationProxy implements Protocol {

    private Protocol target;

    public BindCardValidationProxy(Protocol target) {
        this.target = target;
    }

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String linkmanid = MapUtil.getString(params, "linkmanid");
        if (StringUtils.isBlank(linkmanid)) {
            return target.process(pcode, params);
        } else if (StringUtils.isNotBlank(linkmanid) && !"-1".equals(linkmanid)) {
            List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(userInfo.getAlipayUserId(), linkmanid);
            String patientid = "";
            for (ContactPeopleInfo per : contactsList) {
                if (linkmanid.equals(per.getLinkmanid())) {
                    patientid = per.getPatientid();
                }
            }
            if (StringUtils.isBlank(patientid) || "0".equals(patientid)) {
                throw new HospitalException("联系人未绑卡", HospitalException.ERROR);
            } else {
                return target.process(pcode, params);
            }
        } else if (StringUtils.isEmpty(userInfo.getBrid()) || "0".equals(userInfo.getBrid())) {
            throw new HospitalException("还未绑卡", HospitalException.UNBIND_CARD);
        }

        return target.process(pcode, params);
    }
}
