package com.jandar.cloud.hospital.exception;

import com.jandar.alipay.core.HospitalException;

/**
 * 云医院相关异常
 * Created by zzw on 16/8/31.
 */
public class CloudHospitalException extends HospitalException {

    static public String USER_ERROR = "C001"; // 用户未登陆
    static public String PATIENT_NO_ILLNESS = "C002"; // 病人未就诊过该疾病
    static public String REQUEST_IS_EMPTY = "C003"; // 请求参数为空
    static public String INSPECT_IS_EMPTY = "C004"; // 检查单号不存在
    static public String INSPECT_TYPE_IS_EMPTY = "C005"; // 检查单类型不存在
    static public String DEPT_IS_EMPTY = "C006"; // 部门不存在
    static public String ILLNESS_IS_EMPTY = "C007"; // 疾病不存在
    static public String PRESCRTPTION_IS_EMPTY = "C008"; // 该病人还没有处方
    static public String PATIENTIS_EMPTY = "C009"; // 病人不存在
    static public String PRESCRTPTIONINFO_IS_EMPTY = "C010"; // 处方信息不存在
    static public String INHOSPITALINFO_IS_EMPTY = "C011"; // 该病人无住院信息
    static public String INHOSPITAL_CODE_IS_EMPTY = "C012"; // 住院单号不存在
    static public String INHOSPITAL_PAYMENT_IS_EMPTY = "C013"; // 住院缴费信息不存在
    static public String PRESCRTPTION_PAYMENT_IS_EMPTY = "C014"; // 处方缴费信息不存在
    static public String DOCTADVISENO_IS_EMPTY = "C015"; // 条码号无效
    static public String ILLNESS_RECORD_IS_EMPTY = "C016";//没有就诊记录
    static public String MESSAGE_NOT_DEAL = "C017";//留言未处置，不允许缴费
    static public String RECEIVE_ADDRESS_INCOMPLETE = "C018";//病人收件信息不完整
    static public String SOUTCE_ALREADY_LOCK = "C019";//本号源已经被预约
    static public String SOUTCE_IS_NOT_EXIST = "C020";//号源不存在
    static public String TOPIC_IS_EMPTY = "C021";//问卷不存在

    public CloudHospitalException(String message) {
        super(message);
    }
}
