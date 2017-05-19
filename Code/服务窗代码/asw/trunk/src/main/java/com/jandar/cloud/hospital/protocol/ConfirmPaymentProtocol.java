package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.*;
import com.jandar.cloud.hospital.dao.*;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.handle.protocol.Protocol;
import com.jandar.util.StringUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

/**
 * 确认付款，跳转到支付宝
 * Created by jhf on 2016/11/10.
 */
@Component
public class ConfirmPaymentProtocol extends CloudHospitalProtocol {
    @Resource
    private CloudOrderDao cloudOrderDao;
    @Resource
    private PatientDao patientDao;
    @Resource
    private DoctorDao doctorDao;
    @Resource
    private ReservationRecordDao reservationRecordDao;
    @Resource
    private InhospitalDao inhospitalDao;
    @Resource
    private InspectionDao inspectionDao;
    @Resource
    private PrescriptionDao prescriptionDao;
    @Autowired
    private UserService userService;
    @Override
    public String getProtocolCode() {
        return Content.CONFIRM_PAYMENT_CODE;
    }

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }

        String orderType = MapUtil.getString(params, "OrderType");   //订单类型
        if (orderType == null) {
            throw new HospitalException("请求参数OrderType为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String orderCode = MapUtil.getString(params, "OrderCode");   //单据代码
        if (orderCode == null) {
            throw new HospitalException("请求参数OrderCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String patientCode = null;
        String patientName = null;
        String doctorCode = null;
        String doctorName = null;
        String paymentMoney = null;
        if("YY".equals(orderType)){         //预约信息
            ReservationRecord reservationRecord = reservationRecordDao.findReservationInfo(orderCode, null);
            patientCode = reservationRecord.getPatientCode();
            patientName = reservationRecord.getPatientName();
            doctorCode = reservationRecord.getDoctorCode();
            doctorName = reservationRecord.getDoctorName();
            paymentMoney = reservationRecord.getFee();
        }else if("ZY".equals(orderType)){    //住院单信息
            Inhospital inhospital = inhospitalDao.findByCode(orderCode);
            patientCode = inhospital.getPatientCode();
            patientName = inhospital.getPatientName();
            doctorCode = inhospital.getDoctorCode();
            doctorName = inhospital.getDoctorName();
            paymentMoney = inhospital.getTotalSum();
        }else if("JC".equals(orderType)){   //检查单信息
            InspectMain inspection = inspectionDao.findByCode(orderCode);
            patientCode = inspection.getPatientCode();
            patientName = inspection.getPatientName();
            doctorCode = inspection.getDoctorCode();
            doctorName = inspection.getDoctorName();
            paymentMoney = inspection.getTotalSum();
        }else if("CF".equals(orderType)){   //处方单信息
            Prescription prescription = prescriptionDao.findByCode(orderCode);
            patientCode = prescription.getPatientCode();
            patientName = prescription.getPatientName();
            doctorCode = prescription.getDoctorCode();
            doctorName = prescription.getDoctorName();
            paymentMoney = prescription.getTotalSum();
        }else{
            throw new HospitalException("请求参数OrderType错误！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String price = MapUtil.getString(params, "Price");   //原价
        String discountMoney = MapUtil.getString(params, "DiscountMoney");   //优惠金额
        String comment = MapUtil.getString(params, "Comment");   //备注
        CloudOrder cloudOrder = new CloudOrder();
        String orderNumber = orderType+System.currentTimeMillis()+Math.round(Math.random()*100000000);//生成订单编号
        System.out.println("orderNumber:"+orderNumber);
        cloudOrder.setOrderNumber(orderNumber);
        cloudOrder.setOrderType(orderType);
        cloudOrder.setOrderCode(orderCode);
        cloudOrder.setOrderStatus(0);   //待付款
        cloudOrder.setPatientCode(patientCode);
        cloudOrder.setPatientName(patientName);
        cloudOrder.setDoctorCode(doctorCode);
        cloudOrder.setDoctorName(doctorName);
        if(price!=null) cloudOrder.setPrice(price);
        if(discountMoney!=null) cloudOrder.setDiscountMoney(discountMoney);
        cloudOrder.setPaymentMoney(paymentMoney);
        if(!StringUtil.isBlank(comment)) cloudOrder.setComment(comment);
        cloudOrder.setCreateDate(new Date());
        cloudOrderDao.add(cloudOrder);
        Map<String, Object> res = new HashMap<String, Object>();
        res.put("OrderNumber",cloudOrder.getOrderNumber());
        res.put("OrderStatus",cloudOrder.getOrderStatus());
        res.put("PatientName",cloudOrder.getPatientName());
        res.put("DoctorName",cloudOrder.getDoctorName());
        res.put("PaymentMoney",cloudOrder.getPaymentMoney());
        return res;
    }
}
