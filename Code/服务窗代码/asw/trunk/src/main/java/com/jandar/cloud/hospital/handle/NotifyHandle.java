package com.jandar.cloud.hospital.handle;

import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.util.SpringBeanUtil;
import org.springframework.beans.factory.annotation.Autowired;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.cloud.hospital.bean.PaymentList;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.dao.PaymentListDao;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.util.SerialNumberUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * 支付宝或是微信到帐通知处理类
 * Created by zzw on 2016/9/30.
 */
public class NotifyHandle {

    private static PrescriptionDao prescriptionDao = SpringBeanUtil.getBean(PrescriptionDao.class);
    private static InspectionDao inspectionDao = SpringBeanUtil.getBean(InspectionDao.class);
    private static InhospitalDao inhospitalDao = SpringBeanUtil.getBean(InhospitalDao.class);
    private static PaymentListDao paymentListDao = SpringBeanUtil.getBean(PaymentListDao.class);
    /**
     * 平台支付到账通知
     *
     * @param hospitalTradeNo 医院订单号
     * @param paymentTradeNo  支付平台订单号
     * @param money           支付金额,单位元
     * @param type            平台类型
     * @param gmt_payment     到账时间
     * @return 处理失败时返回 false
     */
    public static boolean handlePayNotify(String hospitalTradeNo,
                                          String paymentTradeNo,
                                          String money,
                                          String gmt_payment,
                                          PlatformType type) {
        //获取参数
        Integer orderType = SerialNumberUtil.paresType(hospitalTradeNo);
        String code = hospitalTradeNo;
        String paymentAmount = money;
        String paymentTime = gmt_payment;
        // 设置缴费记录表中的数据
        PaymentList paymentList = new PaymentList();
        paymentList.setCode(code);
        paymentList.setDate(new Date());
        paymentList.setMoney(paymentAmount);
        //更新处方单、缴费记录表
        if (orderType == 1) {
            Prescription prescription = prescriptionDao.findByCode(code);
            if(paymentAmount.equals(prescription.getPrescriptionSum()))
                prescriptionDao.updatePayment(code);
            paymentList.setContent(prescription.getPrescriptionName()+prescription.getPatientName());
            paymentListDao.save(paymentList);
            return true;
        }
        //更新检查单、缴费记录表
        if (orderType == 2) {
            InspectMain inspection = inspectionDao.findByCode(code);
            if (paymentAmount.equals(inspection.getTotalSum()))
                inspectionDao.updatePayment(code);
            paymentList.setContent(inspection.getInspectName()+inspection.getPatientName());
            paymentListDao.save(paymentList);
            return true;
        }
        //更新住院单、缴费记录表
        if (orderType == 3) {
            Inhospital inhospital = inhospitalDao.findByCode(code);
            if(paymentAmount.equals(inhospital.getPrestoreSum()))
                inhospitalDao.updatePayment(code);
            paymentList.setContent(inhospital.getInhospitalName()+inhospital.getPatientName());
            paymentListDao.save(paymentList);
            return true;
        }
        //非法输入则放回false
        return false;
    }

    /**
     * 向用户发送已经到账的通知模块消息
     *
     * @param hospitalTradeNo 医院订单号
     * @param paymentTradeNo  支付平台订单号
     * @param money           支付金额,单位元
     * @param type            平台类型
     * @param gmt_payment     到账时间
     * @throws HospitalException
     */
    public static boolean sendPayNotifyToUser(String hospitalTradeNo,
                                              String paymentTradeNo,
                                              String money,
                                              String gmt_payment,
                                              PlatformType type) {
        // 使用 MessageBuildUtil 构建一个通知消息，如模板消息

        // 使用 MessageSendUtil 把这个构建好的消息发出去
        return false;
    }
}
