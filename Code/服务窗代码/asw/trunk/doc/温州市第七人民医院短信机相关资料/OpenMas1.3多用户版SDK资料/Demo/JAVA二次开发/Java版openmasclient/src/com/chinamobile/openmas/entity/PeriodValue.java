/**
 * 
 */
package com.chinamobile.openmas.entity;

/**
 * @author OpenMas
 *
 */
public class PeriodValue
{
	private int month;
    private int day;
    private int week = 1;
    
	/**
	 * @return day
	 */
	public int getDay() {
		return day;
	}
	/**
	 * @param day Ҫ���õ� day
	 */
	public void setDay(int day) throws MessageException
	{
		if (day >= 1 && day <= 31)
		{
			this.day = day;
		}
		else
			throw new MessageException("��(Day)������1~31֮�䡣");
	}
	
	/**
	 * @return month
	 */
	public int getMonth() {
		return month;
	}
	/**
	 * @param month Ҫ���õ� month
	 */
	public void setMonth(int month)  throws MessageException
	{
		if (month >= 1 && month <= 12)
			this.month = month;
		else
			throw new MessageException("�·�(Month)������1~12֮�䡣");
	}
	/**
	 * @return week
	 */
	public int getWeek() 
	{
		return week;
	}
	/**
	 * @param week Ҫ���õ� week
	 */
	public void setWeek(int week)  throws MessageException
	{
		if(week >= 1 && week <= 7)
			this.week = week;
		else
			throw new MessageException("����(week)������1~7֮�䡣");
	}
}
