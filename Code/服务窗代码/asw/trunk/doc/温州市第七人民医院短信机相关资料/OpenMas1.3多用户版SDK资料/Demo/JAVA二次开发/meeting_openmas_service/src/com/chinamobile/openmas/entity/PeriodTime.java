/**
 * 
 */
package com.chinamobile.openmas.entity;

/**
 * @author OpenMas
 *
 */
public class PeriodTime 
{
	private int Hour;
    private int minute;
    private int second;
    
    /**
	 * @return hour
	 */
	public int getHour() {
		return Hour;
	}
	/**
	 * @param hour Ҫ���õ� hour
	 */
	public void setHour(int hour)  throws MessageException
	{
		if (hour >= 0 && hour <= 23)
			this.Hour = hour;
		else
			throw new MessageException("Сʱ(Hour)������0~23֮�䡣");
	}
	/**
	 * @return minute
	 */
	public int getMinute() {
		return minute;
	}
	/**
	 * @param minute Ҫ���õ� minute
	 */
	public void setMinute(int minute)  throws MessageException
	{
		if (minute >= 0 && minute <= 59)
			this.minute = minute;
		else
			throw new MessageException("����(Minute)������0~59֮�䡣");
	}
	/**
	 * @return second
	 */
	public int getSecond() {
		return second;
	}
	/**
	 * @param second Ҫ���õ� second
	 */
	public void setSecond(int second)  throws MessageException
	{
		if (second >= 0 && second <= 59)
			this.second = second;
		else
            throw new MessageException("��(Second)������0~59֮�䡣");
	}
}
