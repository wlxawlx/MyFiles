package com.jandar.handle.protocol.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.OpCodeContext;
import com.jandar.handle.protocol.Protocol;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class TestsList implements Protocol {
	@Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        List<Map<String, String>> values = HospitalInfoService.getInstance().oldProcess(
                OpCodeContext.E001, requiredParams(params));
        if (values.size() > 0) {
            return values;
        } else {
            throw new HospitalException("获取化验列表失败");
        }
    }

	private Map<String, Object> requiredParams(Map<String, Object> params) {
		// TODO Auto-generated method stub
		 Map<String, Object> required = new HashMap<String, Object>();
	        // 其它校验

	        // 这里key要对应医院端接口的参数名
	    
	        required.put("sfzh", params.get("sfzh"));
	        required.put("brxm", params.get("brxm"));

	        return required;
	}
}
