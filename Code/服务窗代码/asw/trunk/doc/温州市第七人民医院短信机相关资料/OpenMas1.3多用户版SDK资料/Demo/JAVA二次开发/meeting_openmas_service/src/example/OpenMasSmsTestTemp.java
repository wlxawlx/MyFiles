package example;

import java.util.*;

import com.chinamobile.openmas.client.*;
import com.chinamobile.openmas.entity.*;

public class OpenMasSmsTestTemp
{
	  public static void main(String[] args)
	  {
		  try
		  {
		  // ����
		  Sms sms = new Sms("http://192.168.0.4:8080/openmasservice");
		  String[] destinationAddresses = new String[]{"13735865265"};
		  String message="���Ų���";
		  String extendCode = "0101"; //�Զ�����չ���루ģ�飩
		  String ApplicationID= "default";
		  String Password = "default";
		  //���Ͷ��� ����һ
		  String GateWayid =sms.SendMessage(destinationAddresses, message);
		  System.out.println(GateWayid);
		  //���Ͷ��� ������
		  GateWayid = sms.SendMessage(destinationAddresses, message, extendCode);
		  System.out.println(GateWayid);
		  //���Ͷ��� ������
		  GateWayid = sms.SendMessage(destinationAddresses, message, extendCode, ApplicationID, Password);
		  System.out.println(GateWayid);
		  //���Ͷ��� ������
		  //���ö�ʱʱ��
		  Calendar expectSendTime = Calendar.getInstance();
		  expectSendTime.add(Calendar.MINUTE, 5);
		  GateWayid = sms.SendMessage(destinationAddresses, message, extendCode, ApplicationID, Password, expectSendTime);
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
		  GateWayid=sms.AddTask(PeriodType.Month,periodValue,periodTime,destinationAddresses, message);
		  System.out.println(GateWayid);
		  //��ʱ���� ������
		  GateWayid=sms.AddTask(PeriodType.Month, periodValue, periodTime, destinationAddresses, message, extendCode, ApplicationID, Password, beginTime, endTime);
		  System.out.println(GateWayid);
		  //��ʱ���� ������
		  GateWayid = sms.AddTask(PeriodType.Month, periodValue, periodTime, destinationAddresses, message, extendCode);
		  System.out.println(GateWayid);
		  //��ʱ���� ������
		  GateWayid = sms.AddTask(PeriodType.Month, periodValue, periodTime, destinationAddresses, message, extendCode, ApplicationID, Password);
		  System.out.println(GateWayid);
		  //ɾ����ʱ ����һ
		  sms.RemoveTask(GateWayid);
		  //ɾ����ʱ ������
		  sms.RemoveTask(GateWayid, Password);
		  
		  //��ȡ���ж��� ����һ
		  SmsMessage mo = sms.GetMessage("02e2021b-e544-45df-87f0-192c96083826");
		  if(mo !=null)
			  System.out.println(mo.toString());
		  //��ȡ���ж��� ������
		  SmsMessage[] messages = sms.GetMessages(ApplicationID, Password);
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
