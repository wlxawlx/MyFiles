package com.alipay.label;

import com.alipay.api.AlipayClient;
import com.alipay.api.request.AlipayOpenPublicLabelCreateRequest;
import com.alipay.api.request.AlipayOpenPublicLabelDeleteRequest;
import com.alipay.api.request.AlipayOpenPublicLabelModifyRequest;
import com.alipay.api.request.AlipayOpenPublicLabelQueryRequest;
import com.alipay.api.request.AlipayOpenPublicLabelUserCreateRequest;
import com.alipay.api.request.AlipayOpenPublicLabelUserDeleteRequest;
import com.alipay.api.request.AlipayOpenPublicLabelUserQueryRequest;
import com.alipay.api.response.AlipayOpenPublicLabelCreateResponse;
import com.alipay.api.response.AlipayOpenPublicLabelDeleteResponse;
import com.alipay.api.response.AlipayOpenPublicLabelModifyResponse;
import com.alipay.api.response.AlipayOpenPublicLabelQueryResponse;
import com.alipay.api.response.AlipayOpenPublicLabelUserCreateResponse;
import com.alipay.api.response.AlipayOpenPublicLabelUserDeleteResponse;
import com.alipay.api.response.AlipayOpenPublicLabelUserQueryResponse;
import com.alipay.factory.AlipayAPIClientFactory;

/**
 * 服务窗标签接口调用示例
 * 
 * @author liliang
 *
 */
public class AlipayPublicLabel {
	
	public static void main(String[] args) {
		lableCreate();
//		lablequery();
//		lableedit();
//		labledel();
//		lableuseradd();
//		lableuserQuery();
//		lableuserDel();
	}

	// 服务窗创建标签接口
	public static void lableCreate() {
		AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
		AlipayOpenPublicLabelCreateRequest request = new AlipayOpenPublicLabelCreateRequest();
		request.setBizContent("{\"name\" : \"test0001\"}");
		AlipayOpenPublicLabelCreateResponse response = null;
		try {
			response = alipayClient.execute(request);
			System.out.println(response.getBody());
		} catch (Exception e) {
			// TODO: handle exception
		}
	}
	
	// 服务窗查询标签接口
		public static void lablequery() {
			AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
			AlipayOpenPublicLabelQueryRequest request = new AlipayOpenPublicLabelQueryRequest();
			AlipayOpenPublicLabelQueryResponse response = null;
			try {
				response = alipayClient.execute(request);
				System.out.println(response.getBody());
			} catch (Exception e) {
				// TODO: handle exception
			}
		}
		
		//修改标签接口
		public static void lableedit() {
			AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
			AlipayOpenPublicLabelModifyRequest request = new AlipayOpenPublicLabelModifyRequest();
			request.setBizContent("{\"id\" : \"100768231\", \"name\" : \"测试标签0222\"}");
			AlipayOpenPublicLabelModifyResponse response = null;
			try {
				response = alipayClient.execute(request);
				System.out.println(response.getBody());
			} catch (Exception e) {
				// TODO: handle exception
			}
		}
		
		//删除标签接口
		public static void labledel() {
			AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
			AlipayOpenPublicLabelDeleteRequest request = new AlipayOpenPublicLabelDeleteRequest();
			request.setBizContent("{\"id\" : \"100768\"}");
			AlipayOpenPublicLabelDeleteResponse response = null;
			try {
				response = alipayClient.execute(request);
				System.out.println(response.getBody());
			} catch (Exception e) {
				// TODO: handle exception
			}
		}
		
		//给用户增加标签接口
		public static void lableuseradd() {
			AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
			AlipayOpenPublicLabelUserCreateRequest request = new AlipayOpenPublicLabelUserCreateRequest();
			request.setBizContent("{\"user_id\":\"2088802608984030\",\"label_id\" : \"100889\"}");
			AlipayOpenPublicLabelUserCreateResponse response = null;
			try {
				response = alipayClient.execute(request);
				System.out.println(response.getBody());
			} catch (Exception e) {
				// TODO: handle exception
			}
		}
		
		//查询用户标签接口
		public static void lableuserQuery() {
			AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
			AlipayOpenPublicLabelUserQueryRequest request = new AlipayOpenPublicLabelUserQueryRequest();
			request.setBizContent("{\"user_id\":\"2088802608984030\"}");
			AlipayOpenPublicLabelUserQueryResponse response = null;
			try {
				response = alipayClient.execute(request);
				System.out.println(response.getBody());
			} catch (Exception e) {
				// TODO: handle exception
			}
		}
		
		//删除用户标签接口
		public static void lableuserDel() {
			AlipayClient alipayClient = AlipayAPIClientFactory.getAlipayClient();
			AlipayOpenPublicLabelUserDeleteRequest request = new AlipayOpenPublicLabelUserDeleteRequest();
			request.setBizContent("{\"user_id\":\"2088802608984030\",\"labelId\" : \"100652\"}");
			AlipayOpenPublicLabelUserDeleteResponse response = null;
			try {
				response = alipayClient.execute(request);
				System.out.println(response.getBody());
			} catch (Exception e) {
				// TODO: handle exception
			}
		}


}
