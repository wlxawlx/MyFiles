package com.jandar.cloud.hospital.job;

public class OpCode {
	/**
	 *检查单信息表同步
	 */
	public final static String SYNC_CHECK_OP_CODE="IN010101";
	/**
	 *检查单信息结果表同步
	 */
	public final static String SYNC_CHECK_OP_ITEMS_CODE="IN010201";
	/**
	 *检验结果信息表同步
	 */
	public final static String SYNC_CHECK_OP_CHEMICAL_RESULT_CODE="IN010301";
	/**
	 *检查单化验报告同步
	 */
	public final static String SYNC_CHECK_OP_CHEMICAL_DETAIL_CODE="IN010401";
	/**
	 *检查结果信息表同步
	 */
	public final static String SYNC_CHECK_OP_RESULT_CODE="IN010501";
	/**
	 *预约信息记录同步
	 */
	public final static String SYNC_CHECK_OP_RESERVATION_RECORD_CODE="YY010101";
	/**
	 *缴费信息记录同步
	 */
	public final static String SYNC_CHECK_OP_PAYMENT_CODE="JF010101";
	/**
	 *医生信息同步
	 */
	public final static String SYNC_CHECK_OP_DOCTOR_CODE="DO010101";
	/**
	 *医生排班信息同步
	 */
	public final static String SYNC_CHECK_OP_SCHEDULE_CODE="DO010201";
	/**
	 *号源信息同步
	 */
	public final static String SYNC_CHECK_OP_SOURCE_CODE="DO010301";
	/**
	 *处方信息同步
	 */
	public final static String SYNC_CHECK_OP_PRESCRIPTION_CODE="CF010101";
	/**
	 *服药信息同步
	 */
	public final static String SYNC_CHECK_OP_MEDICIALINFO_CODE="CF010201";
	/**
	 *历史疾病同步
	 */
	public final static String SYNC_CHECK_OP_HISTORYILLNESS_CODE="PA010201";
	/**
	 *住院信息同步
	 */
	public final static String SYNC_CHECK_OP_INHOSPITAL_CODE="ZY010101";
	/**
	 *病人信息（支付宝）同步
	 */
	public final static String SYNC_CHECK_OP_PATIENTBYAL_CODE="PA010101";
	/**
	 *病人信息（微信）同步
	 */
	public final static String SYNC_CHECK_OP_PATIENTBYWECHAT_CODE="PA010102";
	/**
	 *留言同步
	 */
	public final static String SYNC_CHECK_OP_LEAVEMESSAGE_CODE="LE010101";

}
