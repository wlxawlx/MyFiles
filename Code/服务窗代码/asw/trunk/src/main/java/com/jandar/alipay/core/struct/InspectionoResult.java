package com.jandar.alipay.core.struct;

/*
 * 
 */
public class InspectionoResult {
	public InspectionoResult(String studyresult, String diagresult) {
		super();
		this.studyresult = studyresult;
		this.diagresult = diagresult;
	}

	public InspectionoResult() {
		super();
		// TODO Auto-generated constructor stub
	}

	private String studyresult;// 检查所见 string 是
	private String diagresult;// 检查诊断 string 是

	public String getStudyresult() {
		return studyresult;
	}

	public void setStudyresult(String studyresult) {
		this.studyresult = studyresult;
	}

	public String getDiagresult() {
		return diagresult;
	}

	public void setDiagresult(String diagresult) {
		this.diagresult = diagresult;
	}
}
