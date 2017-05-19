package com.spring;

import com.jandar.cloud.hospital.service.TestService;
import com.jandar.cloud.hospital.service.UserService;

/**
 * Created by flyhorse on 2016/12/20.
 */
public class SpringTest {


    public static void main(String[] args)
    {
        System.out.println("==========1===================");
        TestService service=(TestService)SpringManager.getBean("testService");
        service.getCurPatientInfo();
        System.out.println("==========2===================");
    }
}
