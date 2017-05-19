/**
 * 文件：AlipayUserInfoUtil.java 2015年12月29日
 *
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.alipay.util;

import com.alipay.api.response.AlipayUserUserinfoShareResponse;
import com.jandar.alipay.AlipayUserInfo;

/**
 * 将支付宝的账户共享信息转换成AlipayUsreInfo对象。
 *
 * @author dys
 * @version 1.0 2015年12月29日
 *
 */
public class AlipayUserInfoUtil {

	public static AlipayUserInfo getUserInfo(AlipayUserUserinfoShareResponse shareResponse) {
		AlipayUserInfo userInfo = new AlipayUserInfo();

		userInfo.setUserId(shareResponse.getUserId());

		// id为空的话就没法跟医院账号绑定了
		if (userInfo.getUserId() == null) {
			System.out.println("支付宝用户userId为空");
			return null;
		}

		String userTypeValue = shareResponse.getUserTypeValue();
		if ("1".equals(userTypeValue)) {
			userInfo.setUserType(AlipayUserInfo.UserType.company);
            if (shareResponse.getFirmName() == null) {
                userInfo.setRealName(shareResponse.getRealName());
            } else {
                userInfo.setRealName(shareResponse.getFirmName());
            }
        } else if ("2".equals(userTypeValue)) {
			userInfo.setUserType(AlipayUserInfo.UserType.people);
			userInfo.setRealName(shareResponse.getRealName());
		}

		userInfo.setCertNo(shareResponse.getCertNo());

		String certTypeValue = shareResponse.getCertTypeValue();
		if ("0".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.sfz);
		} else if ("1".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.hz);
		} else if ("2".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.jgz);
		} else if ("3".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.sbz);
		} else if ("4".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.hxz);
		} else if ("5".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.lxsfz);
		} else if ("6".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.hkb);
		} else if ("7".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.jgz2);
		} else if ("8".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.tbz);
		} else if ("9".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.yyzz);
		} else if ("10".equals(certTypeValue)) {
			userInfo.setCertType(AlipayUserInfo.CertType.qt);
		}

		String genderValue = shareResponse.getGender();
		if ("F".equals(genderValue)) {
			userInfo.setGender(AlipayUserInfo.Gender.female);
		} else if ("M".equals(genderValue)) {
			userInfo.setGender(AlipayUserInfo.Gender.male);
		}

		userInfo.setPhone(shareResponse.getPhone());
		userInfo.setMobile(shareResponse.getMobile());
		userInfo.setCertified("T".equals(shareResponse.getIsCertified()));
		userInfo.setBankAuth("T".equals(shareResponse.getIsBankAuth()));
		userInfo.setIdAuth("T".equals(shareResponse.getIsIdAuth()));
		userInfo.setMobileAuth("T".equals(shareResponse.getIsMobileAuth()));
		userInfo.setLicenceAuth("T".equals(shareResponse.getIsLicenceAuth()));
		userInfo.setStudentCertified("T".equals(shareResponse.getIsStudentCertified()));
		userInfo.setAvatar(shareResponse.getAvatar());


//		System.out.println(shareResponse.getDeliverMobile());
//		System.out.println(shareResponse.getDeliverPhone());
//		System.out.println(shareResponse.getDeliverFullname());
//		System.out.println(shareResponse.getDeliverProvince());
//		System.out.println(shareResponse.getDeliverCity());
//		System.out.println(shareResponse.getDeliverArea());
//		System.out.println(shareResponse.getDefaultDeliverAddress());

		return userInfo;
	}

}
