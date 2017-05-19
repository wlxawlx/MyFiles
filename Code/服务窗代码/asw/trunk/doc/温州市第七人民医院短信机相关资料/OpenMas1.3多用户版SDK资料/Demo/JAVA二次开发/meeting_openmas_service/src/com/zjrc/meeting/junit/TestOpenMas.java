package com.zjrc.meeting.junit;

import java.io.UnsupportedEncodingException;

import org.opensaml.ws.message.MessageException;

import com.chinamobile.openmas.tools.MmsBuilder;
import com.chinamobile.openmas.tools.MmsConst;
import com.chinamobile.openmas.tools.MmsContent;

/**
 * Title: TestOpenMas.java<br>
 * Description: <br>
 * Copyright (c) 融创信息版权所有 2012	<br>
 * Create DateTime: Aug 6, 2012 5:23:28 PM <br>
 * @author ln
 */
public class TestOpenMas {
	public static void main(String[] args) throws UnsupportedEncodingException {
		String msgText = "中国";
		MmsContent mmsContent = MmsContent.CreateFromBytes(msgText.getBytes("utf-8"));
		mmsContent.setCharset("utf-8");

		mmsContent.setContentID("2.txt");

		mmsContent.setContentType(MmsConst.TEXT);

		MmsBuilder body = new MmsBuilder();
		try {
			body.AddContent(mmsContent);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		String content;
		try {
			content = body.BuildContentToXml();
			System.out.println(content);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
