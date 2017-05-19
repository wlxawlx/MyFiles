/**
 * 
 */
package com.chinamobile.openmas.entity;

import java.util.Date;
import java.text.SimpleDateFormat;
/**
 * @author OpenMas
 *
 */
public class SmsMessage
{
	private String Id;
	private String ApplicationId;
	private String From;
	private String To;
	private String Content;
	private String ExtendCode;
	private Date ReceivedTime;
	
	public SmsMessage()
	{}
	public SmsMessage(String meesageId, String applicationId, String content, String from, String to, String extendCode, Date receivedTime)
    {
        this.Id = meesageId;
        this.ApplicationId = applicationId;
        this.From = from;
        this.To = to;
        this.Content = content;
        this.ExtendCode = extendCode;
        this.ReceivedTime = receivedTime;
    }
	/**
	 * @return id ���ű�ʶ��
	 */
	public String getId()
	{
		return Id;
	}
	/**
	 * @param id ���ű�ʶ��
	 */
	public void setId(String id)
	{
		this.Id = id;
	}
	/**
	 * @return applicationId ���Ž��շ�Ӧ�ñ�ʶ
	 */
	public String getApplicationId()
	{
		return ApplicationId;
	}
	/**
	 * @param applicationId ���Ž��շ�Ӧ�ñ�ʶ
	 */
	public void setApplicationId(String applicationId)
	{
		ApplicationId = applicationId;
	}
	
	/**
	 * @return content ��������
	 */
	public String getContent() {
		return Content;
	}
	/**
	 * @param content ��������
	 */
	public void setContent(String content) {
		Content = content;
	}
	/**
	 * @return extendCode ���Ž��շ�����չ����
	 */
	public String getExtendCode() {
		return ExtendCode;
	}
	/**
	 * @param extendCode ���Ž��շ�����չ����
	 */
	public void setExtendCode(String extendCode) {
		ExtendCode = extendCode;
	}
	/**
	 * @return from ���ŷ��ͷ�
	 */
	public String getFrom() {
		return From;
	}
	/**
	 * @param from ���ŷ��ͷ�
	 */
	public void setFrom(String from) {
		From = from;
	}
	/**
	 * @return receivedTime ���Ž���ʱ��
	 */
	public Date getReceivedTime() {
		return ReceivedTime;
	}
	/**
	 * @param receivedTime ���Ž���ʱ��
	 */
	public void setReceivedTime(Date receivedTime) {
		ReceivedTime = receivedTime;
	}
	/**
	 * @return to ���Ž��շ�
	 */
	public String getTo() {
		return To;
	}
	/**
	 * @param to ���Ž��շ�
	 */
	public void setTo(String to) {
		To = to;
	}
	public String toString()
	{
		 return "MessageType:SMS;"
         + "From: " + From + ";"
         + "To:" + To + ";"
         + "Content:" + Content + ";"
         + "ExtendCode:" + ExtendCode + ";"
         + "ReceivedTime:" + new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(ReceivedTime);
	}
	
}
