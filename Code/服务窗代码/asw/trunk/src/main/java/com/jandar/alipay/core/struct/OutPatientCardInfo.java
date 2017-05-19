package com.jandar.alipay.core.struct;
/*
 * 
 * 就诊卡信息
 */
public class OutPatientCardInfo {
	private String cardtype;   // | 就诊卡类型 | string | 是    | 1：就诊卡；2：老社保卡；3：市民卡 |
	private String cardname;   // | 就诊卡名称 | string | 是    | 医院就诊卡；老社保卡；市民卡     |
	private String cardno ;   //  | 就诊卡号  | string | 是    | 诊卡卡号               |
	private String idcardno ;   //| 身份证号  | string | 是    | 这张卡登记的身份证号         |
	private String patientid ;//  | 病人ID  | string | 是    | 诊卡对应病人ID           |
	private String patientname;// | 病人姓名  | string | 是    | 诊卡对应病人姓名           |
	private String birthday;  // | 出生年月  | string | 是    | 病人出生年月：yyyy-MM-dd  |
	private String phone ;  //    | 联系电话  | string | 是    | 病人联系电话             |
	private String balance;   //  | 账户余额  | string | 是    | 这张卡上的余额            |
	private String cost    ;  //  | 累计费用  | string | 是    | 这张卡花过的钱            |
	
	
	public String getCardtype() {
		return cardtype;
	}
	public void setCardtype(String cardtype) {
		this.cardtype = cardtype;
	}
	public String getCardname() {
		return cardname;
	}
	public void setCardname(String cardname) {
		this.cardname = cardname;
	}
	public String getCardno() {
		return cardno;
	}
	public void setCardno(String cardno) {
		this.cardno = cardno;
	}
	public String getIdcardno() {
		return idcardno;
	}
	public void setIdcardno(String idcardno) {
		this.idcardno = idcardno;
	}
	public String getPatientid() {
		return patientid;
	}
	public void setPatientid(String patientid) {
		this.patientid = patientid;
	}
	public String getPatientname() {
		return patientname;
	}
	public void setPatientname(String patientname) {
		this.patientname = patientname;
	}
	public String getBirthday() {
		return birthday;
	}
	public void setBirthday(String birthday) {
		this.birthday = birthday;
	}
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	public String getBalance() {
		return balance;
	}
	public void setBalance(String balance) {
		this.balance = balance;
	}
	public String getCost() {
		return cost;
	}
	public void setCost(String cost) {
		this.cost = cost;
	}

}
