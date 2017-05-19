package com.jandar.cloud.hospital.service;

import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.filter.auth.util.ThirdUserInfo;
import com.jandar.util.SpringBeanUtil;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

/**
 * Created by flyhorse on 2016/11/11.
 * 当为 测试时 管理的 session对象
 */
public class TestSession {

    /**
     * 用来存放 request
     * */
    private static final ThreadLocal<HttpServletRequest> request = new ThreadLocal<HttpServletRequest>();


    //测试 session 的key
    private static final String TEST_USER_INFO = "test.auth";


    /**
     * 取得用户信息
     * 用类型 ThirdUserInfo 保持跟原有的一致
     * */
    public static ThirdUserInfo getThirdUserInfo(HttpServletRequest req)
    {
        //取得 httpSession 先判断session
        HttpSession session = req.getSession(true);


        System.out.println("================sessionId======"+session.getId()+" reqsessid:"+req.getRequestedSessionId());
        ThirdUserInfo userInfo=(ThirdUserInfo)session.getAttribute(TEST_USER_INFO);
        if(userInfo==null)
        {
            System.out.println("the user from session is null");
            UserService us= (UserService)SpringBeanUtil.getBean("userService");

            String patientCode=req.getParameter("PatientCode");
            System.out.println("=======getThirdUserInfo=========patientCode======"+patientCode);
            if(patientCode==null)
            {
                //暂时写成，如果session中没用户，则返回固定用户P001
                userInfo=new ThirdUserInfo();
                userInfo.setName("小红");
                userInfo.setCode("P001");
                userInfo.setUserType(PlatformType.Alipay);
                userInfo.setOpenId("20880062187831812671520620813572");
                session.setAttribute(TEST_USER_INFO,userInfo);
                return userInfo;
            }
            Patient patient=us.getPatient(patientCode);
            if(patient==null)
            {
                //暂时写成，如果session中没用户，则返回固定用户P001
                userInfo=new ThirdUserInfo();
                userInfo.setName("小红");
                userInfo.setCode("P001");
                userInfo.setUserType(PlatformType.Alipay);
                session.setAttribute(TEST_USER_INFO,userInfo);
                return userInfo;
            }

            userInfo=new ThirdUserInfo();
            userInfo.setName(patient.getName());
            userInfo.setCode(patientCode);
            userInfo.setUserType(PlatformType.Alipay);

            session.setAttribute(TEST_USER_INFO,userInfo);

            return userInfo;
        }
        else
        {

            System.out.println("the user from session is not null");
           return userInfo;
        }

    }


    /**
     * 设置request
     * */
    public static void setRequest(HttpServletRequest req)
    {
        request.set(req);
    }

    /**
     * 设置request
     * */
    public static HttpServletRequest getRequest()
    {
        return request.get();
    }


}
