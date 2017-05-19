/**
 * <p>Title: package-info.java</p>
 * <p>Description: </p>
 * <p>Copyright: Copyright (c) 2013</p>
 * <p>Company: Alibaba YunOS</p>
 * @author jinlong.rhj
 * @date 2015年7月1日
 * @version 1.0
 */
/**
 * @author jinlong.rhj
 *
 */
package com.alipay.account;

import com.alipay.api.AlipayApiException;
import com.alipay.api.AlipayClient;
import com.alipay.api.request.AlipayOpenPublicAccountCreateRequest;
import com.alipay.api.request.AlipayOpenPublicAccountDeleteRequest;
import com.alipay.api.request.AlipayOpenPublicAccountQueryRequest;
import com.alipay.api.request.AlipayOpenPublicAccountResetRequest;
import com.alipay.api.response.AlipayOpenPublicAccountCreateResponse;
import com.alipay.api.response.AlipayOpenPublicAccountDeleteResponse;
import com.alipay.api.response.AlipayOpenPublicAccountQueryResponse;
import com.alipay.api.response.AlipayOpenPublicAccountResetResponse;
import com.alipay.factory.AlipayAPIClientFactory;

/**
 * 绑定商户会员号接口
 * @author liliang
 *
 */
public class  ToAlipayPublicAccount{
	
	public static void main(String[] args) {
		accountCreate();
//		accountQuery();
//		accountReset();
//		accountDelete();
	}
	
	//添加绑定商户会员号
	public static void accountCreate() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicAccountCreateRequest request = new AlipayOpenPublicAccountCreateRequest();
		request.setBizContent("{\"bind_account_no\":\"test001\",\"display_name\":\"test001\",\"real_name\":\"张三\",\"from_user_id\":\"2088802613423222\",\"remark\":\"备注信息\"}");
		AlipayOpenPublicAccountCreateResponse response = null;
		try {

			// 使用SDK，调用交易下单接口
			response = alipayClient.execute(request);

			System.out.println(response.getBody());
			System.out.println(response.isSuccess());
			// 这里只是简单的打印，请开发者根据实际情况自行进行处理
			if (null != response && response.isSuccess()) {
				System.out.println("AgreementId: "+response.getAgreementId());
			}
		} catch (AlipayApiException e) {
		}
	}
	
	//查询绑定商户会员号
	public static void accountQuery() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicAccountQueryRequest request = new AlipayOpenPublicAccountQueryRequest();
		request.setBizContent("{\"userId\":\"2088802608984030\"}");		
		AlipayOpenPublicAccountQueryResponse response = null;
		try {

			// 使用SDK，调用交易下单接口
			response = alipayClient.execute(request);

			System.out.println(response.getBody());
			System.out.println(response.isSuccess());
			// 这里只是简单的打印，请开发者根据实际情况自行进行处理
			if (null != response && response.isSuccess()) {
				System.out.println("bindAccount: "+response.getPublicBindAccounts());
			}
		} catch (AlipayApiException e) {
		}
	}
	
	//重置绑定的商户会员号
	public static void accountReset() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicAccountResetRequest request = new AlipayOpenPublicAccountResetRequest();
		request.setBizContent("{\"bind_account_no\":\"test001\",\"display_name\":\"test001\",\"agreement_id\":\"20160727047733500603\",\"real_name\":\"张三\",\"from_user_id\":\"2088802608984030\",\"remark\":\"备注信息\"}");
		AlipayOpenPublicAccountResetResponse response = null;

		try {

			// 使用SDK，调用交易下单接口
			response = alipayClient.execute(request);

			System.out.println(response.getBody());
			System.out.println(response.isSuccess());
			// 这里只是简单的打印，请开发者根据实际情况自行进行处理
			if (null != response && response.isSuccess()) {
				System.out.println("AgreementId: "+response.getAgreementId());
			}
		} catch (AlipayApiException e) {
		}
	}
	
	//解除绑定的商户会员号
	public static void accountDelete() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicAccountDeleteRequest request = new AlipayOpenPublicAccountDeleteRequest();
		request.setBizContent("{\"agreement_id\":\"20160727047733500603\"}");
		
		AlipayOpenPublicAccountDeleteResponse response = null;

		try {

			// 使用SDK，调用交易下单接口
			response = alipayClient.execute(request);

			System.out.println(response.getBody());
			System.out.println(response.isSuccess());
			// 这里只是简单的打印，请开发者根据实际情况自行进行处理
			if (null != response && response.isSuccess()) {
				
			}
		} catch (AlipayApiException e) {
		}
	}
}
