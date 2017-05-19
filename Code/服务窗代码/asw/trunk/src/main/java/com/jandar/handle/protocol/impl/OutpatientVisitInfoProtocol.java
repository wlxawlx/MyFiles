package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutpatientVisitInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author jinliangjin
 * 
 * 就诊信息-病历信息
 */
public class OutpatientVisitInfoProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		if (StringUtils.isBlank(MapUtil.getString(params, "jzxh"))) {
			throw new HospitalException("就诊序号不能为空");
		}
//		HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
//		OutpatientVisitInfo value = handler.getOutpatientVisitInfoDetail(MapUtil.getString(params, "jzxh"));
		OutpatientVisitInfo value =HospitalInfoService.getInstance().getOutpatientVisitInfoDetail(MapUtil.getString(params, "jzxh"));
		if (value != null) {
			Map<String, Object> item = new HashMap<>();
			item.put("mzzs", value.getMzzs());
			item.put("xbs", value.getXbs());
			item.put("jws", value.getJws());
			item.put("grs", value.getGrs());
			item.put("gms", value.getGms());
			item.put("hys", value.getHys());
			item.put("jzs", value.getJzs());
			item.put("tgjc", value.getTgjc());
			item.put("fzjc", value.getFzjc());
			item.put("clyj", value.getClyj());
			return item;
		} else {
			throw new HospitalException("没有找到该就诊记录");
		}
	}
}
