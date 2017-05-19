/**
 * 
 */
package com.chinamobile.openmas.entity;

/**
 * @author OpenMas
 *
 */
public class SystemServiceInfo 
{
	 private String ServerName;
     private String ServiceName; 
     private long MemorySize; 
     private String CreateTime;
     private int CpuPercent; 
     private long TotalCpuTime;
     private ThreadEntity[] ServiceThreads;
	/**
	 * @return cpuPercent CPUռ����
	 */
	public int getCpuPercent() {
		return CpuPercent;
	}
	/**
	 * @param cpuPercent CPUռ����
	 */
	public void setCpuPercent(int cpuPercent) {
		CpuPercent = cpuPercent;
	}
	/**
	 * @return createTime ����ʱ��
	 */
	public String getCreateTime() {
		return CreateTime;
	}
	/**
	 * @param createTime ����ʱ��
	 */
	public void setCreateTime(String createTime) {
		CreateTime = createTime;
	}
	/**
	 * @return memorySize KΪ��λ
	 */
	public long getMemorySize() {
		return MemorySize;
	}
	/**
	 * @param memorySize KΪ��λ
	 */
	public void setMemorySize(long memorySize) {
		MemorySize = memorySize;
	}
	/**
	 * @return serverName ��������
	 */
	public String getServerName() {
		return ServerName;
	}
	/**
	 * @param serverName ��������
	 */
	public void setServerName(String serverName) {
		ServerName = serverName;
	}
	/**
	 * @return serviceName ������
	 */
	public String getServiceName() {
		return ServiceName;
	}
	/**
	 * @param serviceName ������
	 */
	public void setServiceName(String serviceName) {
		ServiceName = serviceName;
	}
	/**
	 * @return serviceThreads �����߳�
	 */
	public ThreadEntity[] getServiceThreads() {
		return ServiceThreads;
	}
	/**
	 * @param serviceThreads �����߳�
	 */
	public void setServiceThreads(ThreadEntity[] serviceThreads) {
		ServiceThreads = serviceThreads;
	}
	/**
	 * @return totalCpuTime cpu�ۼ�����ʱ��,��Ϊ��λ
	 */
	public long getTotalCpuTime() {
		return TotalCpuTime;
	}
	/**
	 * @param totalCpuTime cpu�ۼ�����ʱ��,��Ϊ��λ
	 */
	public void setTotalCpuTime(long totalCpuTime) {
		TotalCpuTime = totalCpuTime;
	}
     
}
