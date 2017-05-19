package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.List;
import java.util.Map;

/**
 * 联系人列表
 * Created by ysc on 16/12/26.
 */
@Component
public class ContactPersonListProtocol extends CloudHospitalProtocol {

    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        if (userInfo == null) {
            throw new HospitalException("还未建档", HospitalException.UNARCHIV);
        }

        List<Map<String, String>> contactPersonList= HospitalInfoService.getInstance().getContactPersonList(userInfo.getAlipayUserId());

        return contactPersonList;
    }

    @Override
    public String getProtocolCode() {
        return Content.CONTACT_PERSON_LIST_CODE;
    }
}
