/**
 * 
 */
package com.chinamobile.openmas.entity;

import java.util.Date;

/**
 * @author OpenMas
 *
 */
public class MmsMessage {
	
	private String Id;
	private String ApplicationId;
	private String From;
	private String To;
	private String Subject;
	private String Content;
	private int Priority;
	private String ExtendCode;
	private Date ReceivedTime;
	
	public MmsMessage(){}
	
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

	/**
	 * @return priority ���ȼ�
	 * @see Priority
	 */
	public int getPriority() {
		return Priority;
	}

	/**
	 * @param priority ���ȼ�
	 */
	public void setPriority(int priority) {
		Priority = priority;
	}

	/**
	 * @return subject ���ű���
	 */
	public String getSubject() {
		return Subject;
	}

	/**
	 * @param subject ���ű���
	 */
	public void setSubject(String subject) {
		Subject = subject;
	}
	public String toString()
	{
		return "MessageType:MMS;"
        + "From: " + From + ";"
        + "To:" + To + ";"
        + "Priority:" + Priority + ";"
        + "Subject:" + Subject + ";"
        + "Content:" + Content + ";";
	}

}
