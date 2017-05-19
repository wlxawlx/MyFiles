package com.jandar.alipay.core;

import com.jandar.alipay.core.impl.BaseHISService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.hospital.ServiceWindowFlag;
import com.jandar.config.ConfigHandler;

/**
 * Created by zzw on 16/2/22.
 * 医院端信息服务,使用这个类为服务提示数据,使用我们定义的统一接口实现的功能
 */
public abstract class HospitalInfoService {

    private final static BaseHISService m_interface = buildInterface();

    /**
     * 工厂方法,生成相应医院自己的处理类,或是使用当前默认类
     *
     * @return 构建处理类
     */
    private static BaseHISService buildInterface() {
        ServiceWindowFlag flagCode = ConfigHandler.getServiceWindowFlag();
        if (flagCode == ServiceWindowFlag.WZSRMYY) {
            return new HISServiceHandlerWZSRMYY();
        } else if(flagCode == ServiceWindowFlag.WZSRMYY_TJZX) {
            return new HISServiceHandlerWZSRMYY();
        }else {
            // 默认值
            return new HISServiceHandlerDefault();
        }
    }

    /**
     * 获得真正的子类型
     *
     * @param aClass 要转化成的类型
     * @return 返回转化后的类型
     */
    public static <T> T getRealInstance(Class<T> aClass) throws FunctionUnsupportException {
        if (m_interface.getClass().isAssignableFrom(aClass)) {
            return (T) m_interface;
        }
        throw new FunctionUnsupportException("医院暂不支持该功能");
    }

    /**
     * 单例化
     *
     * @return 真正单例的对象
     */
    public static BaseHISService getInstance() {
        return m_interface;
    }

    private HospitalInfoService() {
    }
}
