package example;

import java.util.*;

import com.chinamobile.openmas.client.*;
import com.chinamobile.openmas.entity.*;
import com.chinamobile.openmas.tools.*;

public class OpenMasMmsTestTemp {

	/**
	 * @param args
	 */
	public static void main(String[] args) 
	{
		try
		  {
			  //����
			  Mms mms = new Mms("http://192.168.0.4:9090/openmasservice");
			  String[] destinationAddresses = new String[]{"13735865265"};
			  String subject="���Ų���";
			  String extendCode = "0101"; //�Զ�����չ���루ģ�飩
			  String ApplicationID= "default";
			  String Password = "default";
			  MmsContent mmsContent = MmsContent.CreateFromFile("1.gif");
			  MmsBuilder body = new MmsBuilder();
			  body.AddContent(mmsContent);
			  mmsContent = MmsContent.CreateFromBytes("���".getBytes());
			  mmsContent.setCharset("gb2312");
			  mmsContent.setContentID("2");
			  mmsContent.setContentType(MmsConst.TEXT);
			  body.AddContent(mmsContent);
			  String content = body.BuildContentToXml();
			  //���Ͳ��� ����һ
			  String GateWayid =mms.SendMessage(destinationAddresses, subject, content);
			  System.out.println(GateWayid);
			  //���Ͳ��� ������
			  GateWayid = mms.SendMessage(destinationAddresses,subject, content, extendCode);
			  System.out.println(GateWayid);
			  //���Ͳ��� ������
			  GateWayid = mms.SendMessage(destinationAddresses, subject,content, extendCode, ApplicationID, Password);
			  System.out.println(GateWayid);
			  //���Ͳ��� ������
			  //���ö�ʱʱ��
			  Calendar expectSendTime = Calendar.getInstance();
			  expectSendTime.add(Calendar.MINUTE, 5);
			  GateWayid = mms.SendMessage(destinationAddresses,subject, content, extendCode, ApplicationID, Password, expectSendTime);
			  System.out.println(GateWayid);
			  
			  
			  //��ʼʱ��01-01 - ����ʱ��12-31
			  Calendar beginTime = Calendar.getInstance();
			  beginTime.set(Calendar.MONTH, 0);
			  beginTime.set(Calendar.DAY_OF_MONTH,1);
			  Calendar endTime = Calendar.getInstance();
			  endTime.set(Calendar.MONTH, 11);
			  endTime.set(Calendar.DAY_OF_MONTH,31);
			  //��ʱ ÿ�µ�7�� 10:12:11
			  PeriodValue  periodValue = new PeriodValue();
			  periodValue.setDay(7);
			  PeriodTime periodTime = new PeriodTime();
			  periodTime.setHour(10);
			  periodTime.setMinute(12);
			  periodTime.setSecond(11);
			  
			  //��ʱ���� ����һ
			  GateWayid=mms.AddTask(PeriodType.Month,periodValue,periodTime,destinationAddresses,subject, content);
			  System.out.println(GateWayid);
			  //��ʱ���� ������
			  GateWayid=mms.AddTask(PeriodType.Month, periodValue, periodTime, destinationAddresses, subject,content, extendCode, ApplicationID, Password, beginTime, endTime);
			  System.out.println(GateWayid);
			  //��ʱ���� ������
			  GateWayid = mms.AddTask(PeriodType.Month, periodValue, periodTime, destinationAddresses,subject, content, extendCode);
			  System.out.println(GateWayid);
			  //��ʱ���� ������
			  GateWayid = mms.AddTask(PeriodType.Month, periodValue, periodTime, destinationAddresses,subject, content, extendCode, ApplicationID, Password);
			  System.out.println(GateWayid);
			  //ɾ����ʱ ����һ
			  mms.RemoveTask(GateWayid);
			  //ɾ����ʱ ������
			  mms.RemoveTask(GateWayid, Password);
			  //��ȡ���в��� ����һ
			  MmsMessage mo = mms.GetMessage("02e2021b-e544-45df-87f0-192c96083826");
			  if(mo !=null)
				  System.out.println(mo.toString());
			  //��ȡ���ж��� ������
			  MmsMessage[] messages = mms.GetMessages(ApplicationID, Password);
			  if(messages != null)
			  {
				  for(int i=0;i<messages.length;i++)
					  System.out.println(messages[i].toString());
			  }
		  }catch(Exception ex)
		  {
			  System.err.println(ex);
		  }
		  System.exit(0);

	}

}
