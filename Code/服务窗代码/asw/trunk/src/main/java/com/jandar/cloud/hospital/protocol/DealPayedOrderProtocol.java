package com.jandar.cloud.hospital.protocol;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.hospital.Hospital;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.DoctorDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.msg.MessageTemplate;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.config.ConfigHandler;
import com.jandar.util.StringUtil;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 处理订单-获取预约号：即处理付款后的订单进行His记录生成完成挂号
 */
@Component
public class DealPayedOrderProtocol extends CloudHospitalProtocol {

    @Autowired
    private UserService userService;
    @Autowired
    private PatientDao patientDao;
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
      /*  //修改预约记录的状态为完成
        Map<String, String> map = HospitalInfoService.getInstance().updateCloudOrderStatus(preengageseq);  旧逻辑*/
        //新逻辑
        String orderNo = MapUtil.getString(params, "orderno");
        String payMethod = "3";//云医院付款方式
        String payMoney = MapUtil.getString(params, "paymoney");
        if (StringUtils.isBlank(orderNo)) {
            throw new HospitalException("订单号不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String, String> orderInfo = HospitalInfoService.getInstance().cloudOrderInfo(preengageseq);
        parameters.put("orderno",orderNo);
        parameters.put("status","1");
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030308",parameters);//根据订单号更新订单状态
        String success="1";
        System.out.print("***************************"+result);
        if(result.size()>0){//订单状态修改成功进入his挂号落地
            if("success".equals(result.get(0).get("msg"))){
                success= HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).buildRegister(orderNo,preengageseq,payMoney);
                if("success".equals(success)){
                    parameters.put("status",2);
                    result= HospitalInfoService.getInstance().commonService("FY030308",parameters);//根据订单号更新订单状态
                    success="0";
                    //发送通知消息
                    MessageTemplate.appointMessageTemplate(orderInfo);
                }else {
                    //挂号失败
                    //1、支付宝退款
                    //2、修改订单状态
                    Map<String, String> webserviceResult= HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).getTradeNoAndBatchNoByOrderNo(orderNo);
                    String tradeNo=webserviceResult.get("tradeno");
                    if (tradeNo==null){
                        throw new HospitalException("无效订单！", CloudHospitalException.ERROR);
                    }
                    String batchNo=webserviceResult.get("batchno");
                    String paymoney=webserviceResult.get("xzfje");
                    if(payMoney.equals(paymoney) && !"0".equals(paymoney)) {
                        String refunUrl = AlipayWapPayment.getWapRefundUrl(info.getAlipayUserId(), tradeNo, payMoney, "", batchNo);
                    }
                    parameters.put("status",3);
                    result= HospitalInfoService.getInstance().commonService("FY030308",parameters);//根据订单号更新订单状态
                }
            }
        }

       //判断是否可付款
        String preengagedate = orderInfo.get("preengagedate");  //预约日期
        orderInfo.put("to_user_id",info.getAlipayUserId());     //用户支付宝ID
        orderInfo.put("to_user_name",info.getName());
        orderInfo.put("patient_sex","男");    //*****临时写死
        orderInfo.put("order_info_url","http://"+ ConfigHandler.getSelfServiceHost()+"/#/appointsdetail/"+preengageseq+"?_k=7mbupk");    //*****查看详情-临时写死
        String nowDate = com.jandar.util.DateUtil.formatDateWithString(new Date(),"yyyy-MM-dd");
        if(nowDate.equals(preengagedate)){
            orderInfo.put("ispay","0");      //可付款
        }else{
            orderInfo.put("ispay","1");      //不可付款
        }
        orderInfo.put("success",success);//挂号是否成功
        return orderInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.DEAL_PAYEDORDER_CODE;
    }
}
