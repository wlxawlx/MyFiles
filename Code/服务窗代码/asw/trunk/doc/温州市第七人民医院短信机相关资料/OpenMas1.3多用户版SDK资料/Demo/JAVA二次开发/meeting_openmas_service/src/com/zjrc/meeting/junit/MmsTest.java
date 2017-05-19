package com.zjrc.meeting.junit;

import org.junit.Test;

import com.chinamobile.openmas.tools.MmsBuilder;
import com.chinamobile.openmas.tools.MmsConst;
import com.chinamobile.openmas.tools.MmsContent;

/**
 * Title: MmsTest.java<br>
 * Description: �ʲ���<br>
 * Copyright (c) �ڴ���Ϣ��Ȩ���� 2012	<br>
 * Create DateTime: Jun 7, 2012 4:17:47 PM <br>
 * @author ln
 */
public class MmsTest {
	@Test
	public void testSendMms() throws Exception {
		//		Mms mms = MmsProvider.getMms();
		//		String[] destinationAddresses = new String[] { "13429614889" };
		//		String subject = "���Ų���";
		//		String extendCode = "0101"; //�Զ���)չ���루ģ�飩
		//		//MmsContent mmsContent = MmsContent.CreateFromFile("c:\\1.gif");
		//		MmsBuilder body = new MmsBuilder();
		//		MmsContent mmsContent = MmsContent.CreateFromBytes("�յ���ظ� ���Իص�ӿ�".getBytes());
		//		mmsContent.setCharset("gb3212");
		//		mmsContent.setContentID("2");
		//		mmsContent.setContentType(MmsConst.TEXT);
		//		body.AddContent(mmsContent);
		//		String content = body.BuildContentToXml();
		//		String id = mms.SendMessage(destinationAddresses, subject, content, extendCode, MmsProvider.APPLICATIONID,
		//				MmsProvider.PASSWORD);
		//		System.out.println("messageId***********" + id);
		String msgText = "中国";
		MmsContent mmsContent = MmsContent.CreateFromBytes(msgText.getBytes());
		mmsContent.setCharset("gb2312");

		mmsContent.setContentID("2.txt");

		mmsContent.setContentType(MmsConst.TEXT);

		MmsBuilder body = new MmsBuilder();
		body.AddContent(mmsContent);

		String content = body.BuildContentToXml();
		System.out.println(content);
	}
}
