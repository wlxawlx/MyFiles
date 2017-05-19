package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;

import jodd.util.StringUtil;
import org.apache.commons.lang3.StringUtils;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

public class OutPatientCreateOrderProtocol implements Protocol {

	final static String m_orderName = "支付宝门诊预存";

	/**
	 * @author zhou 门诊充值---创建订单 F001
	 */
	@Override
	public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
		UserInfo info = ServiceContext.getHospitalUserInfo();
		String patientid = info.getBrid();
		String money = MapUtil.getString(params, "money");
		String subject = m_orderName;
		String orderNo = HospitalInfoService.getInstance().buildOutpatientRechargeOrder(null, null,
				null, null, patientid, subject, money);
		if (!StringUtil.isBlank(orderNo)) {
			Map<String, String> result = new HashMap<>();

			result.put("orderno", orderNo);
			UserInfo userInfo = ServiceContext.getHospitalUserInfo();
			String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.OutpatientOrder,
					userInfo.getAlipayUserId(), userInfo.getYhxm(), orderNo, m_orderName, (String) params.get("money"),
					MapUtil.getString(params, "showurl"), null, "", "", "", null);
			System.out.println(aliwappayurl);
			result.put("aliwappayurl", aliwappayurl);
			return result;
		} else {
			throw new HospitalException("门诊预充——创建订单失败");
		}
	}

	private Map<String, Object> requiredParams(Map<String, Object> params) throws HospitalException {
		Map<String, Object> required = new HashMap<String, Object>();
		// 其它校验
		if (StringUtils.isBlank(MapUtil.getString(params, "money"))) {
			throw new HospitalException("充值金额不能为空");
		}

		// 这里key要对应医院端接口的参数名
		UserInfo info = ServiceContext.getHospitalUserInfo();

		if (info != null) {
			String brid = info.getBrid();
			if (brid == null) {
				throw new HospitalException("未绑卡，请先绑卡");
			}
			required.put("yhid", info.getYhid());
			required.put("openid", info.getAlipayUserId());
			required.put("ordername", m_orderName);
			required.put("orderm", params.get("money"));
			required.put("brid", info.getBrid());
		} else {
			throw new HospitalException("未建档，请先建档");
		}
		return required;
	}
}
