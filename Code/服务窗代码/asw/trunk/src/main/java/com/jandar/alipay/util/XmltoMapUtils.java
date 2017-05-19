package com.jandar.alipay.util;

import java.io.StringReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.jdom.Document;
import org.jdom.Element;
import org.jdom.Namespace;
import org.jdom.input.SAXBuilder;
import org.xml.sax.InputSource;

/**
 * xml转化成map
 * 
 * @author dell15m
 *
 */
public class XmltoMapUtils {
	public static List<Map<String, String>> xmlElements(String xmlDoc) {
		xmlDoc = xmlDoc.replaceAll("</value>", "");
		xmlDoc = xmlDoc.replaceAll("<value>", "");
		xmlDoc = xmlDoc.replaceAll("</message>", "");
		xmlDoc = xmlDoc.replaceAll("<message>", "");
		// System.out.println(xmlDoc);
		Map<String, String> entity = new HashMap<String, String>();
		List<Map<String, String>> list = new ArrayList<Map<String, String>>();
		try {
			// 创建一个新的字符串
			StringReader read = new StringReader(xmlDoc);
			// 创建新的输入源SAX 解析器将使用 InputSource 对象来确定如何读取 XML 输入
			InputSource source = new InputSource(read);
			// 创建一个新的SAXBuilder
			SAXBuilder sb = new SAXBuilder();
			// 通过输入源构造一个Document
			Document doc = sb.build(source);
			// 取的根元素
			Element root = doc.getRootElement();
			// System.out.println(root.getName());//输出根元素的名称（测试）
			// 得到根元素所有子元素的集合
			List jiedian = root.getChildren();
			// 获得XML中的命名空间（XML中未定义可不写）
			Namespace ns = root.getNamespace();
			Element et = null;
			for (int i = 0; i < jiedian.size(); i++) {
				et = (Element) jiedian.get(i);// 循环依次得到子元素
				if (entity.get(et.getName()) == null && !et.getName().equals("result")) {
					entity.put(et.getName(), et.getText());
				} else if (!et.getName().equals("result")) {
					list.add(entity);
					entity = new HashMap();
					entity.put(et.getName(), et.getText());
				}
			}
			list.add(entity);
		} catch (Exception e) {
			e.printStackTrace();
			return null;
		}
		return list;
	}

	public static void main(String[] args) {
		System.out.println(xmlElements(
				"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><root><name>胡兴国</name><phone>111111</phone><idcardno>12312321</idcardno><address>胡兴国</address><openid>WQWWDADAS</openid><headurl>WWWQWEWDASS</headurl><cardno>8888</cardno><patientid>777</patientid><usertype>1</usertype></root>"));
	}
}
