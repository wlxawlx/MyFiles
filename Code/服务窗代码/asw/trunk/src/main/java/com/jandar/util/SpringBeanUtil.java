package com.jandar.util;

import org.springframework.beans.BeansException;
import org.springframework.context.ApplicationContext;
import org.springframework.context.ApplicationContextAware;

import java.util.Map;

/**
 * SpringBean的工具类
 * 非注入方式取得spring注入bean的util类实现
 * Created by zzw on 16/8/31.
 */
public final class SpringBeanUtil implements ApplicationContextAware {

    private static ApplicationContext ctx;

    /**
     * 通过spring配置文件中配置的bean id取得bean对象
     *
     * @param id spring bean ID值
     * @return spring bean对象
     */
    public static Object getBean(String id) {
        if (ctx == null) {
            throw new NullPointerException("ApplicationContext is null");
        }
        return ctx.getBean(id);
    }

    public static <T> T getBean(Class<T> requiredType) throws BeansException {
        if (ctx == null) {
            throw new NullPointerException("ApplicationContext is null");
        }
        return ctx.getBean(requiredType);
    }

    public static <T> Map<String, T> getBeansOfType(Class<T> type) throws BeansException {
        if (ctx == null) {
            throw new NullPointerException("ApplicationContext is null");
        }
        return ctx.getBeansOfType(type);
    }

    @Override
    public void setApplicationContext(ApplicationContext applicationcontext)
            throws BeansException {
        ctx = applicationcontext;
    }
}
