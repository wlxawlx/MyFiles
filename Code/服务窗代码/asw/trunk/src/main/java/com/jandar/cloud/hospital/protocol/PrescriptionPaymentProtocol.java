package com.jandar.cloud.hospital.protocol;

import com.google.gson.internal.LinkedTreeMap;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.lang.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
/**
 * 处方缴费
 * Created by lyx on 16/9/27.
 */
@Component
public class PrescriptionPaymentProtocol extends CloudHospitalProtocol {

    @Resource
    private PrescriptionDao prescriptionDao;
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
        /**
         * 处理处方信息表,判断是否自取分情况对处方信息表进行操作
         */
        String code = MapUtil.getString(params, "Code");
        if (code == null) {
            throw new HospitalException("请求参数Code为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String isExpress = MapUtil.getString(params, "IsExpress");
        if (isExpress == null) {
            throw new HospitalException("请求参数IsExpress为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String messageStatus = MapUtil.getString(params,"MessageStatus");
        if (messageStatus == null) {
            throw new HospitalException("请求参数MessageStatus为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //有留言，且未处理时不能缴费
        if("1".equals(messageStatus)){
            throw new HospitalException("留言未处置，不允许缴费！", CloudHospitalException.MESSAGE_NOT_DEAL);
        }
        if (isExpress.equals("No")) {
            prescriptionDao.UpdatePayment(code, isExpress);
        } else {
            Map<String,String> receiveInfo = (LinkedTreeMap<String,String>)params.get("ReceiveInfo");
            if (receiveInfo==null || receiveInfo.size()<1){
                throw new HospitalException("请求参数ReceiveInfo为空！", CloudHospitalException.RECEIVE_ADDRESS_INCOMPLETE);
            }
            String name = receiveInfo.get("Name");
            if(StringUtils.isBlank(name)){
                throw new HospitalException("请求参数ReceiveInfo中的Name为空！", CloudHospitalException.RECEIVE_ADDRESS_INCOMPLETE);
            }
            String phoneNo = receiveInfo.get("PhoneNo");
            if(StringUtils.isBlank(phoneNo)){
                throw new HospitalException("请求参数ReceiveInfo中的PhoneNo为空！", CloudHospitalException.RECEIVE_ADDRESS_INCOMPLETE);
            }
            String receiveAddress = receiveInfo.get("ReceiveAddress");
            if(StringUtils.isBlank(receiveAddress)){
                throw new HospitalException("请求参数ReceiveInfo中的ReceiveAddress为空！", CloudHospitalException.RECEIVE_ADDRESS_INCOMPLETE);
            }
            Prescription.ReceiveInfo receviceInfo = new Prescription.ReceiveInfo();
            receviceInfo.setName(name);
            receviceInfo.setPhoneNo(phoneNo);
            receviceInfo.setReceiveAddress(receiveAddress);
            prescriptionDao.UpdatePayment(code, isExpress);
            prescriptionDao.UpdateExpressInfo(code, receviceInfo);
        }
        Prescription prescriptionInfo = prescriptionDao.paymentInfo(code);
        if (prescriptionInfo == null) {
            throw new HospitalException("处方缴费信息不存在！", CloudHospitalException.PRESCRTPTION_PAYMENT_IS_EMPTY);
        }
        return prescriptionInfo;
    }
    @Override
    public String getProtocolCode() {
        return Content.PRESCRTPTION_PAYMENT_CODE;
    }
}
