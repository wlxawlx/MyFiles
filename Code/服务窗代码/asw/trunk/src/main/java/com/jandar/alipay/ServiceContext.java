/**
 * 文件：ServiceContext.java 2015年12月28日
 * <p/>
 * ZHEJIANG JANDAR TECHNOLOGY CO.,LTD
 * Copyright (c) 1993-2015. All Rights Reserved.
 */
package com.jandar.alipay;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.dao.UserInfoService;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.service.TestSession;
import com.jandar.config.ConfigHandler;
import com.jandar.filter.auth.util.ThirdUserInfo;
import com.jandar.util.SpringBeanUtil;
import org.apache.log4j.Logger;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;
import java.io.Serializable;

/**
 * 服务上下文，可以获取当前用户信息。
 *
 * @author dys
 * @version 1.0 2015年12月28日
 */

public class ServiceContext implements Serializable {

    static Logger logger = Logger.getLogger(ServiceContext.class);

    private static final long serialVersionUID = -3715133237573020820L;

    private static final ThreadLocal<HttpServletRequest> request = new ThreadLocal<HttpServletRequest>();

    private static final String ALIPAY_USER_INFO = "com.jandar.user.alipay";

    private static final String HOSPITAL_USER_INFO = "com.jandar.user.hospital";

    private static final String THIRD_USER_INFO = "com.jandar.auth.third";


    /**
     * 将request绑定到当前线程。
     *
     * @param req
     */
    public static void setRequest(HttpServletRequest req) {
//		if (req != null && req.getSession(false) != null) {
//			System.out.println(req.getSession(false).getId());
//		}
        request.set(req);
    }

    /**
     * 返回当前线程绑定的HttpServletRequest。
     *
     * @return
     */
    public static HttpServletRequest getRequest() {
        return request.get();
    }

    // /**
    //  * 获取当前支付宝用户的信息。
    //  *
    //  * @return
    //  */
    // public static AlipayUserInfo getAlipayUserInfo() {
    //     return (AlipayUserInfo) get(ALIPAY_USER_INFO);
    // }

    // /**
    //  * 将支付宝用户信息保存到当前上下文。
    //  *
    //  * @param userInfo 用户信息
    //  */
    // public static void setAlipayUserInfo(AlipayUserInfo userInfo) {
    //     set(ALIPAY_USER_INFO, userInfo);
    // }

    public static ThirdUserInfo getThirdUserInfo() {
        return (ThirdUserInfo) get(THIRD_USER_INFO);
    }

    public static void setThirdUserInfo(ThirdUserInfo thirdUserInfo) {
        set(THIRD_USER_INFO, thirdUserInfo);
    }

    /**
     * 获取当前用户的医院账号信息。
     *
     * @return
     */
    public static UserInfo getHospitalUserInfo() throws HospitalException {
        UserInfo userInfo;
//        UserInfo userInfo = (UserInfo) get(HOSPITAL_USER_INFO);
//        if (userInfo != null) {
//            return userInfo;
//        }

        // AlipayUserInfo alipayUserInfo = getAlipayUserInfo();

        //判断是否是测试，如果是测试.....
        if(ConfigHandler.systemIsTest())
        {
            System.out.println("===================getHospitalUserInfo======isTest========:");
            ThirdUserInfo userInfo0= TestSession.getThirdUserInfo(request.get());

            System.out.println("===================getHospitalUserInfo======isTest========:"+userInfo0.getOpenId());
            userInfo = UserInfoService.getUserInfo(userInfo0.getOpenId());
            if (userInfo != null) {
                set(HOSPITAL_USER_INFO, userInfo);
            }
            return userInfo;

        }




        ThirdUserInfo thirdUserInfo = getThirdUserInfo();
        if (thirdUserInfo == null) {

            System.out.println("========================thirdUserInfo == null======");
            throw new HospitalException("网络错误,请刷新重试", HospitalException.UN_USERINFO);
        }
        System.out.println("========================thirdUserInfo == !=null======"+thirdUserInfo.getOpenId());

        /** 测试模式下使用测试的账户进行登录
         * 修改对应的openid即可
         * */
        if (ConfigHandler.systemIsTest()) {
            userInfo = UserInfoService.getUserInfo("20880062187831812671520620813572");//七院测试数据
        } else {
            userInfo = UserInfoService.getUserInfo(thirdUserInfo.getOpenId());
        }
        if (userInfo != null) {
            set(HOSPITAL_USER_INFO, userInfo);
            PatientDao pd = SpringBeanUtil.getBean(PatientDao.class);
            Patient patient=new Patient();
            patient.setAlipayUserId(userInfo.getAlipayUserId());
            patient.setCurrentWay("Alipay");
            patient.setPatientCode(userInfo.getBrid());
            patient.setName(userInfo.getYhxm());
            pd.syncPatient(patient);
        }

        return userInfo;
    }

    private static Object get(String key) {
        if (request.get() != null) {
            HttpSession session = request.get().getSession(false);
            if (session != null) {
                return session.getAttribute(key);
            }
        }
        return null;
    }

    private static void set(String key, Object value) {
        if (request.get() != null) {
            HttpSession session = request.get().getSession();
            if (session != null) {
                session.setAttribute(key, value);
            } else {
                logger.error("在会话中存储 " + key + " 错误,会话已经过期");
            }
        } else {
              logger.error("在会话中存储 " + key + " 错误,请检查系统");
        }
    }

}
