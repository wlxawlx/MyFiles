package com.jandar.cloud.hospital.protocol;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.PaymentList;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.PaymentListDao;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 住院缴费
 * Created by lyx on 16/9/27.
 */
@Component
public class InhospitalPaymentProtocol extends CloudHospitalProtocol {

    @Resource
    private InhospitalDao inhospitalDao;
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
        String code = params.get("Code").toString();
        if (code == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
//        Inhospital inhospitalInfo = inhospitalDao.paymentInfo(code);
//        if (inhospitalInfo == null) {
//            throw new HospitalException("住院缴费信息不存在！", CloudHospitalException.INHOSPITAL_PAYMENT_IS_EMPTY);
//        }
//        String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(inhospitalInfo.getCode(),
//                inhospitalInfo.getInhospitalName(),
//                inhospitalInfo.getTotalSum(),
//                "商品展示地址",
//                "支付完成后的跳转地址",
//                "订单及商品描述",
//                "延时支付",
//                "");
        Inhospital.ContectInfo contectInfo = new Inhospital.ContectInfo();
        String Name = MapUtil.getString(params, "Name");
        String phoneNo = MapUtil.getString(params, "PhoneNo");
        String relation = MapUtil.getString(params, "Relation");
        contectInfo.setRelation(relation);
        contectInfo.setPhoneNo(phoneNo);
        contectInfo.setName(Name);
        inhospitalDao.payByCode(code, contectInfo);
        Inhospital inspection = inhospitalDao.findByCode(code);
        String sucess = "Filed";
        if (inspection.getPayStatus().equals("Success")) {
            sucess = inspection.getPayStatus();
        }
        return sucess;
//        return aliwappayurl;
    }

    @Override
    public String getProtocolCode() {
        return Content.INHOSPITAL_PAYMENT_CODE;
    }
}
