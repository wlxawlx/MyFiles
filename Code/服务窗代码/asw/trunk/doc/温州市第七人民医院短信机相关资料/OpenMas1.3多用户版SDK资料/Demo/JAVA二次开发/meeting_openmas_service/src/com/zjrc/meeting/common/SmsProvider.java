package com.zjrc.meeting.common;

import org.apache.axis2.AxisFault;

import com.chinamobile.openmas.client.Sms;
import com.chinamobile.openmas.entity.SmsMessage;

/**
 * Title: SmsProvider.java<br>
 * Description: ���Žӿڻ�ȡ<br>
 * Copyright (c) �ڴ���Ϣ��Ȩ���� 2012	<br>
 * Create DateTime: Jun 6, 2012 9:52:29 AM <br>
 * @author ln
 */
public class SmsProvider {

	private static Sms SMS;
	public static final String APPLICATIONID = "GUOZIWEI";
	public static final String PASSWORD = "GUOZIWEI";
	private static final String WEBSERVICE_URL = "http://20.21.1.201:9080/OpenMasService";

	//��ʼ�����Žӿ�
	static {
		try {
			SMS = new Sms(WEBSERVICE_URL);
		} catch (AxisFault e) {
			//TODO �����־��¼
			e.printStackTrace();
		}
	}

	/**
	 * ��ȡ���Žӿ�
	 * @return ���ض���Mms
	 */
	public static Sms getSms() {
		if (SMS == null) {
			throw new RuntimeException("SMS init failure");
		}
		return SMS;
	}

	/**
	 * ��ȡ��������
	 * @param messageId
	 * @return
	 */
	public static SmsMessage getMmsMessage(String messageId) {
		return getSms().GetMessage(messageId);
	}
}
