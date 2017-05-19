package com.alipay.generalize;

import com.alipay.api.AlipayClient;
import com.alipay.api.request.AlipayOpenPublicQrcodeCreateRequest;
import com.alipay.api.response.AlipayOpenPublicQrcodeCreateResponse;
import com.alipay.factory.AlipayAPIClientFactory;

/**
 * 推广支持接口调用示例
 * @author liliang
 *
 */
public class AlipayOpenGeneralize {

	// 带参推广二维码接口
	public static void qrcodeCreate() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicQrcodeCreateRequest request = new AlipayOpenPublicQrcodeCreateRequest();
		request.setBizContent(
				"{\"code_info\": {\"scene\": {\"scene_id\": \"1\"}},\"code_type\": \"TEMP\",\"expire_second\": \"1800\",\"show_logo\": \"Y\"}");
		System.out.println(request.getBizContent());
		AlipayOpenPublicQrcodeCreateResponse response = new AlipayOpenPublicQrcodeCreateResponse();
		try {
			response = alipayClient.execute(request);
			System.out.println(response.getBody());
		} catch (Exception e) {
			// TODO: 异常处理
		}
	}

	// 带参推广短链接接口
	public static void shortlinkCreate() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicQrcodeCreateRequest request = new AlipayOpenPublicQrcodeCreateRequest();
		request.setBizContent("{\"scene_id\" : \"2\",\"remark\" : \"门店1支付推广\"}");
		AlipayOpenPublicQrcodeCreateResponse response = new AlipayOpenPublicQrcodeCreateResponse();
		try {
			response = alipayClient.execute(request);
			System.out.println(response.getBody());
		} catch (Exception e) {
			// TODO: 异常处理
		}
	}
	
	public static void main(String[] args) {
		qrcodeCreate();
//		shortlinkCreate();
	}
}
