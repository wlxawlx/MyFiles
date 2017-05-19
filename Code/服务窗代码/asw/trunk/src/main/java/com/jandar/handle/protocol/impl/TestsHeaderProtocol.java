package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.InspectionInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

public class TestsHeaderProtocol implements Protocol {

	/**
	 * @author zhou
	 * 
	 * 		获取化验抬头
	 * */
	 @Override
	    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
		 InspectionInfo value = HospitalInfoService.getInstance().getTestInfo(requiredParams(params));
	        if (value!=null) {
	        	Map<String, String> map=new HashMap<>();
//	        	map=MapUtil.bean2Map2(value);
	        	map.put("doctadviseno", value.getDoctadviseno());
	        	map.put("requesttime", value.getRequesttime());
	        	map.put("requester", value.getRequester());
	        	map.put("executetime", value.getExecutetime());
	        	map.put("executer", value.getExecuter());
	        	map.put("receivetime", value.getReceivetime());
	        	map.put("receiver", value.getReceiver());
	        	map.put("stayhospitalmode", value.getStayhospitalmode());
	        	map.put("patientid", value.getPatientid());
	        	map.put("section", value.getSection());
	        	map.put("bedno", value.getBedno());
	        	map.put("patientname", value.getPatientname());
	        	map.put("sex", value.getSex());
	        	map.put("age", value.getAge());
	        	map.put("diagnostic", value.getDiagnostic());
	        	map.put("sampletype", value.getSampletype());
	        	map.put("examinaim", value.getExaminaim());
	        	map.put("requestmode", value.getRequestmode());
	        	map.put("checker", value.getChecker());
	        	map.put("checktime", value.getChecktime());

	            return map;
	        } else {
	            throw new HospitalException("化验台头为空");
	        }
	    }

	    private String requiredParams(Map<String, Object> params)
	            throws HospitalException {
	        // 其它校验
	        if (StringUtils.isBlank(MapUtil.getString(params, "doctadviseno"))) {
				throw new HospitalException("化验单条码不能为空");
			}
	        // 这里key要对应医院端接口的参数名
	        return MapUtil.getString(params, "doctadviseno");
	    }

}
