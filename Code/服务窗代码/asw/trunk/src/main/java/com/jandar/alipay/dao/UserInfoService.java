/**
 *
 */
package com.jandar.alipay.dao;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.hospital.UserInfo;

/**
 * @author Administrator
 */
public abstract class UserInfoService {

    public static UserInfo getUserInfo(String alipayUserId) throws HospitalException {
        HISUserInfo hisUserInfo = HospitalInfoService.getInstance().getUserInfo(alipayUserId, PlatformType.Alipay);
        UserInfo userInfo = new UserInfo();
        userInfo.setYhid(hisUserInfo.getUserId());
        userInfo.setYhxm(hisUserInfo.getName());
        userInfo.setLxdh(hisUserInfo.getPhone());
        userInfo.setSfzh(hisUserInfo.getIdcardno());
        userInfo.setLxdz(hisUserInfo.getAddress());
        userInfo.setHeadUrl(hisUserInfo.getHeadurl());
        userInfo.setBrid(hisUserInfo.getPatientid());
		userInfo.setBkzt(hisUserInfo.getBkzt());
        userInfo.setAlipayUserId(hisUserInfo.getOpenid());
        return userInfo;
    }
}
