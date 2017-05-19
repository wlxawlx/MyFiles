package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.InspectionoResult;
import com.jandar.alipay.util.MapUtil;

public class TestCheckDetailProtocol implements Protocol {
	
	/**
	 * @author zhou
	 * 
	 * 		获取检查单明细
	 * */
	@Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		InspectionoResult  value = HospitalInfoService.getInstance().getInspectionoResult(requiredParams(params));
        if (value!=null) {
//        	Map<String, Object> map=MapUtil.bean2Map(value);
        	Map<String , Object> map=new HashMap<>();
        	map.put("studyresult", value.getStudyresult());
        	map.put("diagresult", value.getDiagresult());

            return map;
        } else {
            throw new HospitalException("检查单明细为空");
        }
    }

    private String requiredParams(Map<String, Object> params) throws HospitalException {
    	  // 其它校验
        if (StringUtils.isBlank(MapUtil.getString(params, "doctadviseno"))) {
			throw new HospitalException("化验单条码不能为空");
		}
        // 这里key要对应医院端接口的参数名
        return MapUtil.getString(params, "doctadviseno");
    }
}
