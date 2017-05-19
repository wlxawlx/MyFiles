package com.jandar.cloud.hospital;

/**
 * 云医院全局信息定义
 * Created by admin on 2016/9/18.
 */
public class Content {

    /**
     * 查询所有医生列表
     */
    public final static String LIST_ALL_DOCTOR = "BA040202";
    /**
     * 是否复诊病人
     */
    public final static String IS_REPEAT_CODE = "CH020101";

    /**
     * 综合医院疾病列表（CH010101）
     */
    public final static String ILLNESS_LIST_CODE = "CH010101";

    /**
     * 根据疾病获得问答卷
     */
    public final static String TOPIC_BY_ILLNESS_CODE = "CH010201";

    /**
     * 根据科室获得问答卷（综合医院）
     */
    public final static String TOPIC_BY_DEPT_CODE = "CH010202";
    /**
     * 根据医院获得问答卷（专科医院）
     */
    public final static String TOPIC_BY_HOSPITAL_CODE = "CH010203";

    /**
     * 提交问答的答案
     */
    public final static String TOPIC_SUBMIT_CODE = "CH010301";
    /**
     * 新增医生信息
     */
    public final static String ADD_DOCTOR_CODE = "CH010400";

    /**
     * 医生列表
     */
    public final static String DOCTOR_LIST_CODE = "CH010401";

    /**
     * 单个预约记录查询
     */
    public final static String RESERVATION_SINGLE_QUERY_CODE = "CH010701";

    /**
     * 分页的预约记录查询
     */
    public final static String RESERVATION_MULT_QUERY_CODE = "CH010702";
    /**
     * 处方列表
     */
    public final static String PRESCRTPTION_CODE="CH010801";
    /**
     * 联系人列表
     */
    public final static String CONTACT_PERSON_LIST_CODE="CH010802";
    /**
     * 通用protocol
     */
    public final static String COMMON_PROTOCOL_CODE="CH010803";
    /**
     * 处方缴费确认信息展示
     */
    public final static String PRESCRTPTION_PAYMENT_INFO_CODE="CH010805";
    /**
     * 处方缴费
     */
    public final static String PRESCRTPTION_PAYMENT_CODE="CH010901";
    /**
     * 处方明细
     */
    public final static String PRESCRTPTION_DETAIL_CODE="CH011001";
    /**
     * 处方物流信息
     */
    public final static String PRESCRTPTION_EXPRESSINFO_CODE="CH011101";
    /**
     * 检查单列表
     */
    public final static String INSPECTION_CODE="CH011201";
    /**
     * 处方缴费
     */
    public final static String INSPECTION_PAYMENT_CODE="CH011301";
    /**
     * 检查单明细
     */
    public final static String INSPECTION_DETAIL_CODE="CH011401";
    /**
     * 住院单列表
     */
    public final static String INHOSPITAL_CODE="CH011501";
    /**
     * 住院单缴费
     */
    public final static String INHOSPITAL_PAYMENT_CODE="CH011601";
    /**
     * 住院单明细
     */
    public final static String INHOSPITAL_DETAIL_CODE="CH011701";
    /**
     * 检验、检查单列表
     */
    public final static String INSPECTL_LIST_CODE="CH011801";
    /**
     * 检验单详情
     */
    public final static String INSPECTLCHEMICALRESULT_CODE="CH011901";
    /**
     * 检查单详情
     */
    public final static String INSPECTLRESULT_CODE="CH012001";

    /**
     * 专科医院医生预约选择列表
     */
    public final static String DOCTOR_INSPECTINFO_CODE="CH012201";
    /**
     * 专科医院预约号源选择
     */
    public final static String SOURCE_SELECT_SPECIAL_CODE="CH012301";
    /**
     * 专科医院问卷提交
     */
    public final static String SPECIALIZDE_SUBMITTOPIC_CODE="CH012401";
    /**
     * 检查确认缴费信息展示
     */
    public final static String INSPECT_PAYINFO_CODE="CH012501";
    /**
     * 专科医院问卷展示
     */
    public final static String SPECIALIZDE_TOPIC_LIST_CODE="CH012601";
    /**
     * 确认付款
     */
    public final static String CONFIRM_PAYMENT_CODE="CH012701";
    /**
     * 生成订单，答卷关联记录
     */
    public final static String ORDER_RELATION_CODE="CH012801";
    /**
     * 预约列表、订单列表
     */
    public final static String ORDER_LIST_CODE="CH012901";
    /**
     * 预约/订单详情
     */
    public final static String ORDER_INFO_CODE="CH012902";
    /**
     * 预约/确认退挂号
     */
    public final static String ORDER_REFUND_CODE="CH012903";
    /**
     * 根据订单号退费
     */
    public final static String REFUND_PAY_CODE="CH012904";//此项作废，无单独退款

    /**
     * 问诊咨询付款，确认付款
     */
    public final static String CONFIRM_PAY_CODE="CH013001";
    /**
     * 付款返回（付款成功修改预约记录并消息通知）
     */
    public final static String PAY_RETURN_CODE="CH013002";
    /**
     * 预约/挂号落地
     */
    public final static String DEAL_PAYEDORDER_CODE="CH013003";


    /*
     * 科室列表展示
     */
    public final static String DEPARTMENT_CODE="DT010101";
    /**
     * 物流信息展示
     */
    public final static String EXPRESSINFO_CODE="WL010101";
    /**
     * 住院缴费信息展示
     */
    public final static String INHOSPITAL_PAYMENT_INFO_CODE="ZY012601";
    /**
     * 留言
     */
    public final static String LEAVE_MESSAGE_CODE="LM010101";

    /**
     * 发送排队信息
     */
    public final static String SENT_LINE_MESSAGE_CODE="LM010102";
    /**
     * 专科根据疾病获取问卷
     */
    public final static String ILLNESS_TOPIC_CODE="IT010101";
    /**
     * 预约详情
     */
    public final static String RESERVATION_INFO_CODE="YY010201";
    /**
     *预约列表
     */
    public final static String RESERVATION_LIST_CODE="YY010101";



    /**
     * 插件首页信息
     */
    public final static String PLUG_IN_HOME_PAGE_CODE="PI010001";
    /**
     * 即时通讯获取token
     * */
    public final static String PLUG_IN_GET_TOKEN_CODE="PI010002";

    /**
     * 病人信息(插件)
     */
    public final static String PATIENT_INFO_CODE="PI010003";
    /**
     * 是否跟上次相同的药(插件)
     */
    public final static String IS_SAME_CODE="PI010004";

    /**
     *模拟支付宝消息
     */
    public final static String MONI_ALIPAY_MSG_CODE="MN0001";

}
