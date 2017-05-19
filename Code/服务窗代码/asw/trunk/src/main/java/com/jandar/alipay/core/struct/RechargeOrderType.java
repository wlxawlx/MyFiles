package com.jandar.alipay.core.struct;

/**
 * 用户在医院充值时的订单类型
 */
public enum RechargeOrderType {

    // 门诊预存订单
    OutpatientOrder,

    // 住院预缴订单
    InHospitalOrder,

    // 其它订单,如三院的技能中心
    OtherOrder,

    // 云医院相关的订单
    CloudHospitalOrder
}
