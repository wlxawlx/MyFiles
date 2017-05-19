package com.jandar.alipay.core.struct;
/*
 * 一级科室信息
 */

public class DepartmentInfo {

	private String departcode;// | 科室代码 | string | 是 | 一级科室代码 |
	private String departname;// | 科室名称 | string | 是 | 一级科室名称 |
	private String secondcode;// | 科室代码 | string | 是 | 二级科室代码 |
	private String secondname;// | 科室名称 | string | 是 | 二级科室名称 |
	private String describe;// | 科室描述 | string | 否 | 二级科室描述 |
	public String getDepartcode() {
		return departcode;
	}
	public void setDepartcode(String departcode) {
		this.departcode = departcode;
	}
	public String getDepartname() {
		return departname;
	}
	public void setDepartname(String departname) {
		this.departname = departname;
	}
	public String getSecondcode() {
		return secondcode;
	}
	public void setSecondcode(String secondcode) {
		this.secondcode = secondcode;
	}
	public String getSecondname() {
		return secondname;
	}
	public void setSecondname(String secondname) {
		this.secondname = secondname;
	}
	public String getDescribe() {
		return describe;
	}
	public void setDescribe(String describe) {
		this.describe = describe;
	}
	public DepartmentInfo(String secondcode, String secondname, String describe) {
		super();
		this.secondcode = secondcode;
		this.secondname = secondname;
		this.describe = describe;
	}
	public DepartmentInfo() {
		// TODO Auto-generated constructor stub
	}

	
	
}
