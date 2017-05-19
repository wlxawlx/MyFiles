package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.InhospitalPatientInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
/*
 * @author yubj
 * 住院病人列表
 */

public class InpatientInfoListProtocol implements Protocol {
	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
		String brch = MapUtil.getString(params, "brch");
		if (StringUtils.isBlank(brch)) {
			throw new HospitalException("病人床号不能为空");
		}
		InhospitalPatientInfo info = handler.getinhospitalPatientInfo(brch);
		if (info != null) {
			Map<String, Object> map = new HashMap<>();

			// map.put("zyh", info.get); 无法获取
			map.put("zyhm", info.getInpatientno());
			map.put("brxm", info.getPatientname());
			// map.put("mzhm", info.get); 可以不返回
			map.put("sfzh", info.getPatientidcardno());
			map.put("brxb", info.getSex());
			map.put("csny", info.getBirthday());
			map.put("lxdz", info.getAddress());
			map.put("lxdh", info.getPhone());
			map.put("ryrq", info.getAdmitdate());
			map.put("cyrq", info.getDischargedate());
			map.put("zyts", info.getStayday());
			map.put("xzmc", info.getXzmc());
			map.put("bqmc", info.getEndemicarea());
			map.put("brch", info.getBrch());
			map.put("ksmc", info.getDepartname());
			map.put("ylhj", info.getYlhj());
			map.put("lwhj", info.getLwhj());
			map.put("zfje", info.getZfje());
			map.put("jkje", info.getJkje());
			map.put("zyzt", info.getZyzt());
			// map.put("zzys", info.getDepartcode());
			map.put("ysxm", info.getDoctorcode()); // 无法获取

			ArrayList<Map<String, Object>> list = new ArrayList<>();
			list.add(map);

			return list;
		} else {
			throw new HospitalException("获取住院病人列表失败");
		}
	}
}
