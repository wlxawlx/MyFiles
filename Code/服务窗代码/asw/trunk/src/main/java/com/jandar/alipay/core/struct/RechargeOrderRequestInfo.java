package com.jandar.alipay.core.struct;

/*
 * 创建充值订单请求信息
 */
public class RechargeOrderRequestInfo {
    private String openid;// 用户标识 string 是 支付宝USERID或微信OPENID
    private String patientname;// 病人姓名 string 否 辅助参数，为哪个病人充值
    private String patientidcardno;// 病人身份证号 string 否 辅助参数
    private String cardno;// 就诊卡卡号 string 否 辅助参数
    //    private String patientid;// 病人ID string 是 为哪个病人充值时对应的病人ID
//    private String inpatientno;// 住院号码 string 是 为哪个病人充值时对应的病人住院号码
    private String userdata; // 门诊订单时是病人ID,住院订单是住院号码
    private String subject;// 标题 string 是 充值标题，如支付宝门诊预存
    private String money;// 金额 string 是 门诊预存金额，单位：元
    private String tradeno;// 订单号
    private String orderno;
    
    public String getOrderno() {
		return orderno;
	}

	public void setOrderno(String orderno) {
		this.orderno = orderno;
	}

	public String getTradeno() {
		return tradeno;
	}

	public void setTradeno(String tradeno) {
		this.tradeno = tradeno;
	}

	public String getOpenid() {
        return openid;
    }

    public void setOpenid(String openid) {
        this.openid = openid;
    }

    public String getPatientname() {
        return patientname;
    }

    public void setPatientname(String patientname) {
        this.patientname = patientname;
    }

    public String getPatientidcardno() {
        return patientidcardno;
    }

    public void setPatientidcardno(String patientidcardno) {
        this.patientidcardno = patientidcardno;
    }

    public String getCardno() {
        return cardno;
    }

    public void setCardno(String cardno) {
        this.cardno = cardno;
    }

    public String getUserdata() {
        return userdata;
    }

    public void setUserdata(String userdata) {
        this.userdata = userdata;
    }

    public String getInpatientno() {
        return getUserdata();
    }

    public void setInpatientno(String inpatientno) {
        setUserdata(inpatientno);
    }

    public String getPatientid() {
        return getUserdata();
    }

    public void setPatientid(String patientid) {
        setUserdata(patientid);
    }

    public String getSubject() {
        return subject;
    }

    public void setSubject(String subject) {
        this.subject = subject;
    }

    public String getMoney() {
        return money;
    }

    public void setMoney(String money) {
        this.money = money;
    }
}
