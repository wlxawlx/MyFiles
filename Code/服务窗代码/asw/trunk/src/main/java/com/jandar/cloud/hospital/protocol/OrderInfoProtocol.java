package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.StringUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 云医院预约/订单详情
 */
@Component
public class OrderInfoProtocol extends CloudHospitalProtocol {
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
        String preengageseq = MapUtil.getString(params,"preengageseq");
        if(StringUtil.isBlank(preengageseq)){
            throw new HospitalException("预约流水号preengageseq为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        Map<String, String> orderInfo = HospitalInfoService.getInstance().cloudOrderInfo(preengageseq);
        //判断是否可付款
        String preengagedate = orderInfo.get("preengagedate");  //预约日期
        String nowDate = com.jandar.util.DateUtil.formatDateWithString(new Date(),"yyyy-MM-dd");
        if(nowDate.equals(preengagedate)){
            orderInfo.put("ispay","0");      //可付款
        }else{
            orderInfo.put("ispay","1");      //不可付款
        }
        // 从数据库获取数据
        return orderInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.ORDER_INFO_CODE;
    }
}
