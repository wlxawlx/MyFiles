package com.jandar.alipay.core.struct;

/*
 * 门诊预约
 */
public class PhysicalOrder {

    private String orderId;// 预约流水号
    private String state;// 标志
	
	public String getOrderId() {
		return orderId;
	}
	public void setOrderId(String orderId) {
		this.orderId = orderId;
	}
	public String getState() {
		return state;
	}
	public void setState(String state) {
		this.state = state;
	}
  
}
