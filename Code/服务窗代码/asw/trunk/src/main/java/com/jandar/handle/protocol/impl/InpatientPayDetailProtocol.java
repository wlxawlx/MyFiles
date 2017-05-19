package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.InHospitalOutlays;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/**
 * 住院信息-住院费用明细0.2版
 */
public class InpatientPayDetailProtocol implements Protocol {
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
		String inhospitalcode = MapUtil.getString(params, "inhospitalcode");
		String fyxm = MapUtil.getString(params, "fyxm");
		String fyrq = MapUtil.getString(params, "fyrq");
		if (StringUtils.isBlank(inhospitalcode)) {
			throw new HospitalException("住院号不能为空");
		}
		if (StringUtils.isBlank(fyxm)) {
			throw new HospitalException("费用项目不能为空");
		}
		if (fyrq == null) {
			fyrq = "";
		}
		List<InHospitalOutlays> values = handler.getInhospitalOutlaysDetail(inhospitalcode, fyxm, fyrq);
		if (values.size() > 0) {
			List<Map<String, String>> result = new ArrayList<Map<String, String>>();
			for (InHospitalOutlays sale : values) {
				if (StringUtils.isNotBlank(fyrq) || "".equals(fyrq)) {
					Map<String, String> map = new HashMap<>();
					map.put("fyxh", sale.getFyxh());
					map.put("fyxm", sale.getFyxm());
					map.put("fymc", sale.getFymc());
					map.put("fygg", sale.getFygg());
					map.put("fydw", sale.getFydw());
					map.put("ybdm", sale.getYbdm());
					map.put("zfbl", sale.getZfbl());
					map.put("yplx", sale.getYplx());
					map.put("fysl", sale.getFysl());
					map.put("fydj", sale.getFydj());
					map.put("zjje", sale.getZjje());
					map.put("zfje", sale.getZfje());
					result.add(map);
				}
				if (StringUtils.isNotBlank(fyrq)) {
					break;
				}
			}
			return result;
		} else {
			throw new HospitalException("获取住院病人费用明细失败");
		}
	}
}
