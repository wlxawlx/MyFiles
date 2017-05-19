package com.jandar.cloud.hospital.protocol;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.PaymentList;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.dao.PaymentListDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Date;
import java.util.Map;

/**
 * 检查单缴费CH011301
 * Created by lyx on 2016/10/18.
 */
@Component
public class InspectionPaymentProtocol extends CloudHospitalProtocol {

    @Resource
    private InspectionDao inspectionDao;
    @Resource
    private PaymentListDao paymentListDao;
    @Autowired
    private UserService userService;
    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String code = MapUtil.getString(params, "Code");
        if (code == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
//        Inspection inspectionInfo = inspectionDao.paymentInfo(code);
//        String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(inspectionInfo.getCode(),
//                inspectionInfo.getInspectName(),
//                inspectionInfo.getTotalSum(),
//                "商品展示地址",
//                "支付完成后的跳转地址",
//                "订单及商品描述",
//                "延时支付",
//                "");
        inspectionDao.payByCode(code);
        InspectMain inspection=inspectionDao.findByCode(code);
        String sucess = "Filed";
        if (inspection.getPayStatus().equals("Success"))
        {
            sucess=inspection.getPayStatus();
        }
        return sucess;

    }
    @Override
    public String getProtocolCode() {return Content.INSPECTION_PAYMENT_CODE;}
}
