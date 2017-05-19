package com.jandar.alipay.core.struct;

/*
 * 排班信息
 */
public class SchedulingInfo {
	public SchedulingInfo(String scheduledate, String remain, String total) {
		super();
		this.scheduledate = scheduledate;
		this.remain = remain;
		this.total = total;
	}

	public SchedulingInfo() {
		super();
		// TODO Auto-generated constructor stub
	}

	private String scheduledate;// | 排班日期 | string | 是 | 格式：yyyy-MM-dd |
	private String remain;// | 可约人数 | string | 是 | 还可预约人数 |
	private String total;// | 总号源 | integer | 是 | 这一天在这个科室可预约的号源数 |

	public String getScheduledate() {
		return scheduledate;
	}

	public void setScheduledate(String scheduledate) {
		this.scheduledate = scheduledate;
	}

	public String getRemain() {
		return remain;
	}

	public void setRemain(String remain) {
		this.remain = remain;
	}

	public String getTotal() {
		return total;
	}

	public void setTotal(String total) {
		this.total = total;
	}

}
