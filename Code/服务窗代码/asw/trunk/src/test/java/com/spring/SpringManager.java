package com.spring;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Created by flyhorse on 2016/12/20.
 */
public class SpringManager {

    private ApplicationContext context;

    private static SpringManager instance=new SpringManager();

    private SpringManager()
    {
    }

    public static void init(ApplicationContext p_context)
    {
        instance.context=p_context;
    }


    public static void init()
    {
        instance.context=new ClassPathXmlApplicationContext(new String[]{"applicationContext.xml"});
    }

    public static Object getBean(String beanName)
    {

        if(instance.context==null)
            init();

        return instance.context.getBean(beanName);
    }

    public static ApplicationContext getContext()
    {
        return instance.context;
    }
}
