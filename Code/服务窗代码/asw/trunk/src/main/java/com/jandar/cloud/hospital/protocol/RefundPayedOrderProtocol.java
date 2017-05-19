package com.jandar.cloud.hospital.protocol;

import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
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

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 处理订单-退号：即处理退款后的订单退号
 * 退号接口
 */
@Component
public class RefundPayedOrderProtocol extends CloudHospitalProtocol {

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
        String registerId = MapUtil.getString(params, "registerno");
        if(StringUtil.isBlank(registerId)){
            throw new HospitalException("退号id不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }

        String orderNo = MapUtil.getString(params, "orderno");
        if(StringUtil.isBlank(orderNo)){
            throw new HospitalException("orderno不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }

       String preengageseq = MapUtil.getString(params, "preengageseq");
        if(StringUtil.isBlank(preengageseq)){
            throw new HospitalException("preengageseq不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }

        //调用生成退订单号
        Map<String, String> webserviceResult= HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).getTradeNoAndBatchNoByOrderNo(orderNo,"云医院支付宝门诊退号");
        //定义返回结果
        Map<String, String>  result = new HashMap<>();
        //接受退款参数
        String batchNo=webserviceResult.get("batchno");
        String tradeNo=webserviceResult.get("tradeno");
        if (webserviceResult==null||tradeNo==null){
            result.put("success", "1");
            result.put("msg", "退款提交失败！");
            return result;
        }
        //退号
        Map<String,String> resultMap= HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).cancelYY(registerId, batchNo);
        Map<String, Object> parameters=new HashMap<String, Object>();
        if ("success".equals(resultMap.get("msg")))
        {
            updateOrderStatus(batchNo, "1");//如果退号成功更新订单状态
            String refundmoney=resultMap.get("xzfje");
            String refunUrl = AlipayWapPayment.getWapRefundUrl(info.getAlipayUserId(), tradeNo, refundmoney, "", batchNo);
           ///取消预约,无论成功是否都算退号成功
            try{
                HospitalInfoService.getRealInstance(new HISServiceHandlerDefault().getClass()).cancelOutpatientOrderResultMessage(info.getAlipayUserId(),preengageseq);
            }catch (HospitalException e){//捕捉异常为了走下一步
                result.put("msg", "退款申请成功提交，预约记录尚未取消！");
                result.put("success", "0");
                return  result;
            }
                result.put("msg", "退款申请成功提交");
                result.put("success", "0");
        }
        else {
            updateOrderStatus(batchNo, "-1");//退号失败交易终止
            result.put("success", "1");
            result.put("msg", "退款提交失败！");
        }
        return result;
    }
    private  Map<String, String>  updateOrderStatus(String orderNo,String status) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("batchno",orderNo);
        parameters.put("status",status);
/*        parameters.put("notifytime",new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").format(new Date()) );//非异步请求无此参数，传入空或默认值
        parameters.put("successnum","1");//非异步请求无此参数，传入空或默认值
        parameters.put("resultdetails","only edit status");//非异步请求无此参数，传入空或默认值*/
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY040408", parameters);//根据订单号更新订单状态
        return result.get(0);
    }
    @Override
    public String getProtocolCode() {
        return Content.ORDER_REFUND_CODE;
    }
}
