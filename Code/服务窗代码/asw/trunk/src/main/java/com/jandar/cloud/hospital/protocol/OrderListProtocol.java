package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllness;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.ChronicIllnessDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 云医院预约列表，订单列表
 */
@Component
public class OrderListProtocol extends CloudHospitalProtocol {
    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        String sourcetype= MapUtil.getString(params,"sourcetype");
        if(StringUtils.isNoneBlank()){
            sourcetype="8";
        }
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String status = MapUtil.getString(params,"status");
        //0：已预约待付款、1：已预约已付款、2：已就诊、3：已失效(3：已取消:4：已失约)
        List<Map<String, String>> orderList = null;
        if(("0".equals(status)||"1".equals(status)||"2".equals(status)||"3".equals(status)||"4".equals(status))){
            orderList = HospitalInfoService.getRealInstance(HISServiceHandlerDefault.class).cloudOrderList(info.getAlipayUserId(),status,sourcetype);
        }else{
            throw new HospitalException("状态参数status错误！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //判断是否可付款
        for (Map<String, String> order : orderList) {
            if("0".equals(order.get("status"))){
                String preengagedate = order.get("preengagedate");  //预约日期
                String nowDate = com.jandar.util.DateUtil.formatDateWithString(new Date(),"yyyy-MM-dd");
                if(nowDate.equals(preengagedate)){
                    order.put("ispay","0");      //可付款
                }else{
                    order.put("ispay","1");      //不可付款
                }
            }else{
                order.put("ispay","1");
            }

        }
        // 从数据库获取数据
        return orderList;
    }

    @Override
    public String getProtocolCode() {
        return Content.ORDER_LIST_CODE;
    }
}
