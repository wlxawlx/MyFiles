package com.jandar.alipay.core.struct.wzsrmyy;
/*
 * 门诊候诊信息
 */
public class OutpatientWaitingInfo {
	private String message;//| message | 排队信息 | string | 是    |      |
	private String hzxx;//候诊信息
	
	public String getHzxx() {
		return hzxx;
	}

	public void setHzxx(String hzxx) {
		this.hzxx = hzxx;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}
	
}
