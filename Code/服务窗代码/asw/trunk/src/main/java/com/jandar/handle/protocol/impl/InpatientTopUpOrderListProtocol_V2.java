package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.OpCodeContext;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by zhufengxiang on 2016-01-04.
 * 住院充值-订单列表0.2版
 */
public class InpatientTopUpOrderListProtocol_V2 implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        List<Map<String, String>> values = HospitalInfoService.getInstance().oldProcess(OpCodeContext.F008, requiredParams(params));
        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<String, Object>();
            result.put("list", values);
            return result;
        } else {
            throw new HospitalException("获取 住院充值-订单列表 失败");
        }
    }

    private Map<String, Object> requiredParams(Map<String, Object> params)
            throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        // 其它校验
        String inhospitalid = MapUtil.getString(params, "inhospitalid");
        if(StringUtils.isBlank(inhospitalid)) {
            throw new HospitalException("住院号码不能为空");
        }

        // 这里key要对应医院端接口的参数名
        required.put("zyhm", inhospitalid);

        return required;
    }
}
