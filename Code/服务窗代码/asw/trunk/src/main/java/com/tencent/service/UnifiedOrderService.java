package com.tencent.service;

import com.tencent.common.Configure;
import com.tencent.protocol.unified_order_protocol.UnifiedOrderReqData;

/**
 * Created by zhufengxiang on 2016/07/18.
 * 统一下单服务
 */
public class UnifiedOrderService extends BaseService {

    public UnifiedOrderService() throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        super(Configure.UNIFIED_ORDER);
    }

    public String unifiedOrder(UnifiedOrderReqData unifiedOrderReqData) throws Exception {
        String res = sendPost(unifiedOrderReqData);
        return res;
    }
}
