package com.jandar.cloud.hospital.protocol;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.config.ConfigHandler;
import com.jandar.util.StringUtil;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.HashMap;
import java.util.Map;

/**
 *问诊咨询付款，确认付款
 */
@Component
public class ConfirmPayProtocol extends CloudHospitalProtocol{
    @Autowired
    private UserService userService;
    @Override
    public String getProtocolCode() {
        return Content.CONFIRM_PAY_CODE;
    }
    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if (info == null) {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String m_orderName = "云医院支付宝门诊挂号";
        String patientId = MapUtil.getString(params, "patientid");
        String preengageseq = MapUtil.getString(params, "preengageseq");
        String patientName = MapUtil.getString(params, "patientname");
        String patientIdCard = MapUtil.getString(params, "patientidcard");
        String cardno = MapUtil.getString(params, "cardno");
        String money ="";
        if(StringUtils.isBlank(patientId)){
            throw new HospitalException("patientid不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        if(StringUtils.isBlank(patientIdCard)){
            throw new HospitalException("patientidcard不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        if(StringUtils.isBlank(cardno)){
            throw new HospitalException("cardno不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        if(StringUtil.isBlank(preengageseq)){
            throw new HospitalException("preengageseq不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
       //接口新增：
       money=HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).iniOrder(preengageseq,patientId).get("xzfje");//调用预挂号接口获取自费金额
      /* System.out.print("**************************************" +
               "********************************************************************this trade need to pay"+money);*/
        String orderNo = HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).buildOutpatientRechargeOrder(
                info.getAlipayUserId(),
                patientName,
                patientIdCard,
                cardno,
                patientId,
                m_orderName,
                money,
                preengageseq,"9");
        if (!StringUtil.isBlank(orderNo)) {
            Map<String, String> result = new HashMap<>();
            result.put("orderno", orderNo);
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.OutpatientOrder,
                    userInfo.getAlipayUserId(),
                    patientName,
                    orderNo,
                    m_orderName,
                    money,
                     "http://" + ConfigHandler.getSelfServiceHost() +"/cloud/index.html#/appoints?_k=4dzo9f",
                    MapUtil.getString(params, "showurl"),
                    "",
                    "",
                    "",
                    patientId);

            System.out.println(aliwappayurl);
            result.put("alipaymoney", money);
            result.put("aliwappayurl", aliwappayurl);
            return result;
        } else {
            throw new HospitalException("创建订单失败");
        }
    }

}
