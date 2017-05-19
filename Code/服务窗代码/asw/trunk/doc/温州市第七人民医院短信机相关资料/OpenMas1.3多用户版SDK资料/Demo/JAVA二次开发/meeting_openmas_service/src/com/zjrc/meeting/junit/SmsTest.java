package com.zjrc.meeting.junit;

import java.rmi.RemoteException;

import org.junit.Test;

import com.chinamobile.openmas.client.Sms;
import com.zjrc.meeting.common.SmsProvider;

/**
 * Title: SmsTest.java<br>
 * Description: ���Ų���<br>
 * Copyright (c) �ڴ���Ϣ��Ȩ���� 2012	<br>
 * Create DateTime: Jun 7, 2012 4:17:55 PM <br>
 * @author ln
 */
public class SmsTest {
	@Test
	public void testSendSms() throws RemoteException {
		Sms sms = SmsProvider.getSms();
		String[] destinationAddresses = new String[] { "13575472423" };
		String message = "测试";
		String extendCode = "1234";
		String id = sms.SendMessage(destinationAddresses, message, extendCode, SmsProvider.APPLICATIONID, SmsProvider.PASSWORD);
		System.out.println("sendmessageId**********" + id);
	}
}
