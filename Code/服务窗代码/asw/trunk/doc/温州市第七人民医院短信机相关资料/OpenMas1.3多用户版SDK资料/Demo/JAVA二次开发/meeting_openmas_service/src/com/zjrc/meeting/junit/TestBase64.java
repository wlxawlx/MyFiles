package com.zjrc.meeting.junit;

import java.io.ByteArrayOutputStream;
import java.io.IOException;

/**
 * Title: TestBase64.java<br>
 * Description: <br>
 * Copyright (c) 融创信息版权所有 2012	<br>
 * Create DateTime: Aug 7, 2012 1:44:23 PM <br>
 * @author ln
 */
public class TestBase64 {
	public static void main(String[] args) {
		ByteArrayOutputStream byteOutput = new ByteArrayOutputStream();
		try {
			byteOutput.write("中国".getBytes("gb2312"));
		} catch (IOException e) {
			System.err.println(e);
		}
		System.out.println(new sun.misc.BASE64Encoder().encode(byteOutput.toByteArray()));
	}
}
