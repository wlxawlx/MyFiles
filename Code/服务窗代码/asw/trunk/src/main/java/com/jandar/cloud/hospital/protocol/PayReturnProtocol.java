package com.jandar.cloud.hospital.protocol;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.msg.MessageTemplate;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.StringUtil;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

/**
 *云医院付款返回：修改预约记录为已付款，并发送通知
 */
@Component
public class PayReturnProtocol extends CloudHospitalProtocol{
    @Autowired
    private UserService userService;
    @Autowired
    private PatientDao patientDao;
    @Override
    public String getProtocolCode() {
        return Content.PAY_RETURN_CODE;
    }
    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if (info == null) {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String preengageseq = MapUtil.getString(params, "preengageseq");
        if(StringUtil.isBlank(preengageseq)){
            throw new HospitalException("预约流水号preengageseq为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //修改预约记录的状态为完成
        Map<String, String> map = HospitalInfoService.getInstance().updateCloudOrderStatus(preengageseq);

        Map<String, String> orderInfo = HospitalInfoService.getInstance().cloudOrderInfo(preengageseq);
        //判断是否可付款
        String preengagedate = orderInfo.get("preengagedate");  //预约日期
        orderInfo.put("to_user_id",info.getAlipayUserId());     //用户支付宝ID
        orderInfo.put("to_user_name",info.getName());
        orderInfo.put("patient_sex","男");    //*****临时写死
        orderInfo.put("order_info_url","http://192.68.75.64:8888/#/appointsdetail/"+preengageseq+"?_k=7mbupk");    //*****查看详情-临时写死
        String nowDate = com.jandar.util.DateUtil.formatDateWithString(new Date(),"yyyy-MM-dd");
        if(nowDate.equals(preengagedate)){
            orderInfo.put("ispay","0");      //可付款
        }else{
            orderInfo.put("ispay","1");      //不可付款
        }
        //发送通知消息
        MessageTemplate.appointMessageTemplate(orderInfo);
        return orderInfo;
    }

}
