/**
 * 
 */
package com.chinamobile.openmas.entity;

/**
 * @author OpenMas
 *
 */
public class ThreadEntity 
{
	private boolean IsRunning = false;
    private String ThreadName;
	/**
	 * @return isRunning �Ƿ���������
	 */
	public boolean isRunning() {
		return IsRunning;
	}
	/**
	 * @param isRunning �Ƿ���������
	 */
	public void setRunning(boolean isRunning) {
		IsRunning = isRunning;
	}
	/**
	 * @return threadName �߳�����
	 */
	public String getThreadName() {
		return ThreadName;
	}
	/**
	 * @param threadName �߳�����
	 */
	public void setThreadName(String threadName) {
		ThreadName = threadName;
	}
}
