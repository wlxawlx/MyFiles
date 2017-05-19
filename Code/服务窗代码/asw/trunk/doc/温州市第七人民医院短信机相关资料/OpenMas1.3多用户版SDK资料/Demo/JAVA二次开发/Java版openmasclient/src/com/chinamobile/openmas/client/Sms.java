package com.chinamobile.openmas.client;

import java.rmi.RemoteException;
import org.apache.axis2.AxisFault;
import org.tempuri.*;

import com.chinamobile.openmas.entity.*;
/**
 * @author OpenMas
 *
 */
public class Sms {
	
	private SmsImplementationStub stub = null;
	public Sms(String serviceUrl) throws AxisFault 
	{
		
		stub = new SmsImplementationStub(serviceUrl);
		
		
	}
	/**
	 * ���ŷ���
	 * @param	destinationAddresses  ���շ���ַ�б�(��һ�η��͸�����)
	 * @param	message ��������
	 * @return  ���ض��ŷ���Ψһ��ʾ
	 */
	public String SendMessage(String[] destinationAddresses, String message)
	{		
		SmsImplementationStub.SendMessage1 sendMessage = new SmsImplementationStub.SendMessage1();
		sendMessage.setMessage(message);
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		sendMessage.setDestinationAddresses(array);
		try
		{
			return stub.SendMessage1(sendMessage).getSendMessage1Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * ���ŷ���
	 * @param destinationAddresses ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @param extendCode ��չ����
	 * @return ���ض��ŷ���Ψһ��ʾ
	 */
	public String SendMessage(String[] destinationAddresses, String message, String extendCode)
	{		
		SmsImplementationStub.SendMessage2 sendMessage = new SmsImplementationStub.SendMessage2();
		sendMessage.setMessage(message);
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		sendMessage.setDestinationAddresses(array);
		sendMessage.setExtendCode(extendCode);
		try
		{
			return stub.SendMessage2(sendMessage).getSendMessage2Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * ���ŷ���
	 * @param destinationAddresses ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @param extendCode ��չ����
	 * @param applicationId Ӧ��ID
	 * @param password Ӧ������
	 * @return ���ض��ŷ���Ψһ��ʾ
	 */
	public String SendMessage(String[] destinationAddresses, String message, String extendCode,String applicationId, String password) throws RemoteException
	{		
		SmsImplementationStub.SendMessage3 sendMessage = new SmsImplementationStub.SendMessage3();
		sendMessage.setMessage(message);
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		sendMessage.setDestinationAddresses(array);
		sendMessage.setExtendCode(extendCode);
		sendMessage.setApplicationId(applicationId);
		sendMessage.setPassword(password);
		try
		{
			return stub.SendMessage3(sendMessage).getSendMessage3Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * ���ŷ���
	 * @param destinationAddresses ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @param extendCode ��չ����
	 * @param applicationId Ӧ��ID
	 * @param password Ӧ������
	 * @param expectSendTime ��������ʱ��
	 * @return ���ض��ŷ���Ψһ��ʾ
	 */
	public String SendMessage(String[] destinationAddresses, String message, String extendCode,String applicationId, String password,java.util.Calendar expectSendTime)
	{		
		SmsImplementationStub.SendMessage4 sendMessage = new SmsImplementationStub.SendMessage4();
		sendMessage.setMessage(message);
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		sendMessage.setDestinationAddresses(array);
		sendMessage.setExtendCode(extendCode);
		sendMessage.setApplicationId(applicationId);
		sendMessage.setPassword(password);
//		java.util.Calendar cal = java.util.Calendar.getInstance();
//		cal.setTime(expectSendTime);
		sendMessage.setExpectSendTime(expectSendTime);
		try
		{
			return stub.SendMessage4(sendMessage).getSendMessage4Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * ��ȡ���ŷ���״̬
	 * @param messageId ����ID
	 * @return ���ض��ŷ���״̬
	 */
	public DeliveryReport[]  GetDeliveryReport(String messageId)
	{
		SmsImplementationStub.GetDeliveryReport1 request = new SmsImplementationStub.GetDeliveryReport1();
		request.setMessageId(messageId);
		SmsImplementationStub.GetDeliveryReport1Response response;
		try
		{
			response =stub.GetDeliveryReport1(request);
			if(response == null)
				return null;
		}catch(RemoteException rem)
		{
			System.out.println(rem);
			return null;
		}
		SmsImplementationStub.DeliveryReport[] reports = response.getGetDeliveryReport1Result().getDeliveryReport();
		if(reports == null)
			return null;
		DeliveryReport[]  deliveryReports = new DeliveryReport[reports.length];
		for(int i=0;i<reports.length;i++)
		{
			DeliveryReport deliveryReport = new DeliveryReport();
			deliveryReport.setMessageId(reports[i].get_x003C_MessageId_x003E_k__BackingField());
			deliveryReport.setMessageDeliveryStatus(reports[i].get_x003C_MessageDeliveryStatus_x003E_k__BackingField().getValue());
			deliveryReport.setReceivedAddress(reports[i].get_x003C_SendAddress_x003E_k__BackingField());
			deliveryReport.setSendAddress(reports[i].get_x003C_SendAddress_x003E_k__BackingField());
			deliveryReport.setStatusCode(reports[i].get_x003C_StatusCode_x003E_k__BackingField());
			deliveryReports[i]=deliveryReport;
		}
		return deliveryReports;
	}
	/**
	 * ��ȡ���ŷ���״̬
	 * @param messageId ����ID
	 * @param applicationId Ӧ��ID
	 * @param password Ӧ������
	 * @return ���ض��ŷ���״̬
	 */
	public DeliveryReport[]  GetDeliveryReport(String messageId,String applicationId, String password)
	{
		SmsImplementationStub.GetDeliveryReport2 request = new SmsImplementationStub.GetDeliveryReport2();
		request.setMessageId(messageId);
		request.setApplicationId(applicationId);
		request.setPassword(password);
		SmsImplementationStub.GetDeliveryReport2Response response;
		try
		{
			response=stub.GetDeliveryReport2(request);
			if(response == null)
				return null;
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
		SmsImplementationStub.DeliveryReport[] reports = response.getGetDeliveryReport2Result().getDeliveryReport();
		if(reports == null)
			return null;
		DeliveryReport[]  deliveryReports = new DeliveryReport[reports.length];
		for(int i=0;i<reports.length;i++)
		{
			DeliveryReport deliveryReport = new DeliveryReport();
			deliveryReport.setMessageId(reports[i].get_x003C_MessageId_x003E_k__BackingField());
			deliveryReport.setMessageDeliveryStatus(reports[i].get_x003C_MessageDeliveryStatus_x003E_k__BackingField().getValue());
			deliveryReport.setReceivedAddress(reports[i].get_x003C_SendAddress_x003E_k__BackingField());
			deliveryReport.setSendAddress(reports[i].get_x003C_SendAddress_x003E_k__BackingField());
			deliveryReport.setStatusCode(reports[i].get_x003C_StatusCode_x003E_k__BackingField());
			deliveryReports[i]=deliveryReport;
		}
		return deliveryReports;
	}
	/**
	 * �������ж���
	 * @param messageId ����ID
	 * @return �������ж���
	 */
	public SmsMessage GetMessage(String messageId)
	{
		SmsImplementationStub.GetMessage request = new SmsImplementationStub.GetMessage();
		request.setMessageId(messageId);
		SmsImplementationStub.GetMessageResponse response;
		try
		{
			response=stub.GetMessage(request);
			if(response == null)
				return null;
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
		SmsImplementationStub.SmsMessage message = response.getGetMessageResult();
		if(message == null)
			return null;
		SmsMessage smsMessage = new SmsMessage();
		smsMessage.setApplicationId(message.get_x003C_ApplicationId_x003E_k__BackingField());
		smsMessage.setContent(message.get_x003C_Content_x003E_k__BackingField());
		smsMessage.setExtendCode(message.get_x003C_ExtendCode_x003E_k__BackingField());
		smsMessage.setFrom(message.get_x003C_From_x003E_k__BackingField());
		smsMessage.setId(message.get_x003C_Id_x003E_k__BackingField());
		smsMessage.setReceivedTime(message.get_x003C_ReceivedTime_x003E_k__BackingField().getTime());
		smsMessage.setTo(message.get_x003C_To_x003E_k__BackingField());
		return smsMessage;
	}
	/**
	 * �������ж���
	 * @param applicationId Ӧ��ID
	 * @param password Ӧ������
	 * @return �������ж���
	 */
	public SmsMessage[] GetMessages(String applicationId, String password)
	{
		SmsImplementationStub.GetMessages request = new SmsImplementationStub.GetMessages();
		request.setApplicationId(applicationId);
		request.setPassword(password);
		SmsImplementationStub.GetMessagesResponse response ;
		try
		{
			response = stub.GetMessages(request);
			if(response == null)
				return null;
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
		SmsImplementationStub.SmsMessage[] messages = response.getGetMessagesResult().getSmsMessage();
		if(messages == null)
			return null;
		SmsMessage[] smsMessages = new SmsMessage[messages.length];
		for(int i=0;i<messages.length;i++)
		{
			SmsMessage smsMessage = new SmsMessage();
			smsMessage.setApplicationId(messages[i].get_x003C_ApplicationId_x003E_k__BackingField());
			smsMessage.setContent(messages[i].get_x003C_Content_x003E_k__BackingField());
			smsMessage.setExtendCode(messages[i].get_x003C_ExtendCode_x003E_k__BackingField());
			smsMessage.setFrom(messages[i].get_x003C_From_x003E_k__BackingField());
			smsMessage.setId(messages[i].get_x003C_Id_x003E_k__BackingField());
			smsMessage.setReceivedTime(messages[i].get_x003C_ReceivedTime_x003E_k__BackingField().getTime());
			smsMessage.setTo(messages[i].get_x003C_To_x003E_k__BackingField());
			smsMessages[i] = smsMessage;
		}
		return smsMessages;
	}
	/**
	 * �������ŷ�������
	 * @param periodType �������ڣ��ꡢ�¡��ܡ��գ�
	 * @param periodValue ����ֵ
	 * @param periodTime ����ʱ�䣨ʱ����)
	 * @param destinationAddresses ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @return ��������Ψһ��ʶ
	 */
	public String AddTask(int periodType, PeriodValue periodValue,PeriodTime periodTime, String[] destinationAddresses, String message) throws MessageException
	{
		if(periodType >=0 && periodType <=3)
		{}
		else
		{
			throw new MessageException("����(periodType)������0~3֮�䡣");
		}
		SmsImplementationStub.AddTask1 request = new SmsImplementationStub.AddTask1();
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		request.setDestinationAddresses(array);
		request.setMessage(message);
		switch(periodType)
		{
			case PeriodType.Day: request.setPeriodType(SmsImplementationStub.PeriodType.Day);break;
			case PeriodType.Month:request.setPeriodType(SmsImplementationStub.PeriodType.Month);break;
			case PeriodType.Week:request.setPeriodType(SmsImplementationStub.PeriodType.Week);break;
			case PeriodType.Year:request.setPeriodType(SmsImplementationStub.PeriodType.Year);break;
		}
		SmsImplementationStub.PeriodValue vPeriodValue = new SmsImplementationStub.PeriodValue();
		vPeriodValue.setDay(periodValue.getDay());
		vPeriodValue.setMonth(periodValue.getMonth());
		switch(periodValue.getWeek())
		{
			case DayOfWeek.SUNDAY :vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Sunday);break;
			case DayOfWeek.MONDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Monday);break;
			case DayOfWeek.TUESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.WEDNESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Wednesday);break;
			case DayOfWeek.THURSDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.FRIDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Friday);break;
			case DayOfWeek.SATURDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Saturday);break;
		}
		request.setPeriodValue(vPeriodValue);
		SmsImplementationStub.PeriodTime vperiodTime = new SmsImplementationStub.PeriodTime();
		vperiodTime.setHour(periodTime.getHour());
		vperiodTime.setMinute(periodTime.getMinute());
		vperiodTime.setSecond(periodTime.getSecond());
		request.setPeriodTime(vperiodTime);
		try
		{
			return stub.AddTask1(request).getAddTask1Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * �������ŷ�������
	 * @param periodType �������ڣ��ꡢ�¡��ܡ��գ�
	 * @param periodValue ����ֵ
	 * @param periodTime ����ʱ�䣨ʱ����)
	 * @param destinationAddresses ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @param extendCode ��չ����
	 * @return ��������Ψһ��ʶ
	 * @throws MessageException
	 */
	public String AddTask(int periodType, PeriodValue periodValue, PeriodTime periodTime,String[] destinationAddresses, String message,String extendCode) throws MessageException
	{
		
		if(periodType >=0 && periodType <=3)
		{}
		else
		{
			throw new MessageException("����(periodType)������0~3֮�䡣");
		}
		SmsImplementationStub.AddTask2 request = new SmsImplementationStub.AddTask2();
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		request.setDestinationAddresses(array);
		request.setMessage(message);
		switch(periodType)
		{
			case PeriodType.Day: request.setPeriodType(SmsImplementationStub.PeriodType.Day);break;
			case PeriodType.Month:request.setPeriodType(SmsImplementationStub.PeriodType.Month);break;
			case PeriodType.Week:request.setPeriodType(SmsImplementationStub.PeriodType.Week);break;
			case PeriodType.Year:request.setPeriodType(SmsImplementationStub.PeriodType.Year);break;
		}
		SmsImplementationStub.PeriodValue vPeriodValue = new SmsImplementationStub.PeriodValue();
		vPeriodValue.setDay(periodValue.getDay());
		vPeriodValue.setMonth(periodValue.getMonth());
		switch(periodValue.getWeek())
		{
			case DayOfWeek.SUNDAY :vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Sunday);break;
			case DayOfWeek.MONDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Monday);break;
			case DayOfWeek.TUESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.WEDNESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Wednesday);break;
			case DayOfWeek.THURSDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.FRIDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Friday);break;
			case DayOfWeek.SATURDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Saturday);break;
		}
		request.setPeriodValue(vPeriodValue);
		SmsImplementationStub.PeriodTime vperiodTime = new SmsImplementationStub.PeriodTime();
		vperiodTime.setHour(periodTime.getHour());
		vperiodTime.setMinute(periodTime.getMinute());
		vperiodTime.setSecond(periodTime.getSecond());
		request.setPeriodTime(vperiodTime);
		request.setExtendCode(extendCode);
		try
		{
			return stub.AddTask2(request).getAddTask2Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * �������ŷ�������
	 * @param periodType �������ڣ��ꡢ�¡��ܡ��գ�
	 * @param periodValue ����ֵ
	 * @param periodTime ����ʱ�䣨ʱ����)
	 * @param destinationAddresses ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @param extendCode ��չ����
	 * @param applicationId Ӧ��ID
	 * @param password Ӧ������
	 * @return ��������Ψһ��ʶ
	 * @throws MessageException
	 */
	public String AddTask(int periodType, PeriodValue periodValue,PeriodTime periodTime, String[] destinationAddresses, String message,String extendCode,String applicationId, String password) throws MessageException
	{
		
		if(periodType >=0 && periodType <=3)
		{}
		else
		{
			throw new MessageException("����(periodType)������0~3֮�䡣");
		}
		SmsImplementationStub.AddTask3 request = new SmsImplementationStub.AddTask3();
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		request.setDestinationAddresses(array);
		request.setMessage(message);
		switch(periodType)
		{
			case PeriodType.Day: request.setPeriodType(SmsImplementationStub.PeriodType.Day);break;
			case PeriodType.Month:request.setPeriodType(SmsImplementationStub.PeriodType.Month);break;
			case PeriodType.Week:request.setPeriodType(SmsImplementationStub.PeriodType.Week);break;
			case PeriodType.Year:request.setPeriodType(SmsImplementationStub.PeriodType.Year);break;
		}
		SmsImplementationStub.PeriodValue vPeriodValue = new SmsImplementationStub.PeriodValue();
		vPeriodValue.setDay(periodValue.getDay());
		vPeriodValue.setMonth(periodValue.getMonth());
		switch(periodValue.getWeek())
		{
			case DayOfWeek.SUNDAY :vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Sunday);break;
			case DayOfWeek.MONDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Monday);break;
			case DayOfWeek.TUESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.WEDNESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Wednesday);break;
			case DayOfWeek.THURSDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.FRIDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Friday);break;
			case DayOfWeek.SATURDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Saturday);break;
		}
		request.setPeriodValue(vPeriodValue);
		SmsImplementationStub.PeriodTime vperiodTime = new SmsImplementationStub.PeriodTime();
		vperiodTime.setHour(periodTime.getHour());
		vperiodTime.setMinute(periodTime.getMinute());
		vperiodTime.setSecond(periodTime.getSecond());
		request.setPeriodTime(vperiodTime);
		request.setExtendCode(extendCode);
		request.setApplicationId(applicationId);
		request.setPassword(password);
		try
		{
			return stub.AddTask3(request).getAddTask3Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * �������ŷ�������
	 * @param periodType �������ڣ��ꡢ�¡��ܡ��գ�
	 * @param periodValue ����ֵ
	 * @param periodTime ����ʱ�䣨ʱ����)
	 * @param destinationAddresses  ���շ���ַ�б�(��һ�η��͸�����)
	 * @param message ��������
	 * @param extendCode ��չ����
	 * @param applicationId Ӧ��ID
	 * @param password Ӧ������
	 * @param beginTime ��ʼʱ��
	 * @param endTime ����ʱ��
	 * @return ��������Ψһ��ʶ
	 * @throws MessageException
	 */
	public String AddTask(int periodType, PeriodValue periodValue, PeriodTime periodTime,String[] destinationAddresses, String message,String extendCode,String applicationId,String password,java.util.Calendar beginTime,java.util.Calendar endTime) throws MessageException
	{
		
		if(periodType >=0 && periodType <=3)
		{}
		else
		{
			throw new MessageException("����(periodType)������0~3֮�䡣");
		}
		SmsImplementationStub.AddTask4 request = new SmsImplementationStub.AddTask4();
		SmsImplementationStub.ArrayOfstring array=new SmsImplementationStub.ArrayOfstring();
		for(int i=0;i<destinationAddresses.length;i++)
		{
			array.addString(destinationAddresses[i]);
		}
		request.setDestinationAddresses(array);
		request.setMessage(message);
		switch(periodType)
		{
			case PeriodType.Day: request.setPeriodType(SmsImplementationStub.PeriodType.Day);break;
			case PeriodType.Month:request.setPeriodType(SmsImplementationStub.PeriodType.Month);break;
			case PeriodType.Week:request.setPeriodType(SmsImplementationStub.PeriodType.Week);break;
			case PeriodType.Year:request.setPeriodType(SmsImplementationStub.PeriodType.Year);break;
		}
		SmsImplementationStub.PeriodValue vPeriodValue = new SmsImplementationStub.PeriodValue();
		vPeriodValue.setDay(periodValue.getDay());
		vPeriodValue.setMonth(periodValue.getMonth());
		switch(periodValue.getWeek())
		{
			case DayOfWeek.SUNDAY :vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Sunday);break;
			case DayOfWeek.MONDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Monday);break;
			case DayOfWeek.TUESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.WEDNESDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Wednesday);break;
			case DayOfWeek.THURSDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Thursday);break;
			case DayOfWeek.FRIDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Friday);break;
			case DayOfWeek.SATURDAY:vPeriodValue.setWeek(SmsImplementationStub.DayOfWeek.Saturday);break;
		}
		request.setPeriodValue(vPeriodValue);
		SmsImplementationStub.PeriodTime vperiodTime = new SmsImplementationStub.PeriodTime();
		vperiodTime.setHour(periodTime.getHour());
		vperiodTime.setMinute(periodTime.getMinute());
		vperiodTime.setSecond(periodTime.getSecond());
		request.setPeriodTime(vperiodTime);
		request.setExtendCode(extendCode);
		request.setApplicationId(applicationId);
		request.setPassword(password);
//		java.util.Calendar begin = java.util.Calendar.getInstance();
//		begin.setTime(beginTime);
		request.setBeginTime(beginTime);
//		java.util.Calendar end = java.util.Calendar.getInstance();
//		begin.setTime(endTime);
		request.setEndTime(endTime);
		try
		{
			return stub.AddTask4(request).getAddTask4Result();
		}catch(RemoteException rem)
		{
			System.err.println(rem);
			return null;
		}
	}
	/**
	 * �����Ƴ�
	 * @param taskId ����Ψһ��ʶ
	 */
	public void RemoveTask(String taskId)  
	{
		SmsImplementationStub.RemoveTask request = new SmsImplementationStub.RemoveTask();
		request.setTaskId(taskId);
		try
		{
			stub.RemoveTask(request);
		}catch(RemoteException rem)
		{
			System.err.println(rem);
		}
	}
	
	/**
	 * �����Ƴ�
	 * @param taskId ����Ψһ��ʶ
	 * @param password Ӧ������
	 */
	public void RemoveTask(String taskId,String password)
	{
		SmsImplementationStub.RemoveTask2 request = new SmsImplementationStub.RemoveTask2();
		request.setTaskId(taskId);
		request.setPassword(password);
		try
		{
			stub.RemoveTask2(request);
		}catch(RemoteException rem)
		{
			System.err.println(rem);
		}
	}
	
}
