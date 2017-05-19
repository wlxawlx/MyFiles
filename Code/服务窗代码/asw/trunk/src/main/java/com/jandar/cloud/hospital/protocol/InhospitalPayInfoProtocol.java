package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Map;

/**
 * 住院单缴费信息展示
 * Created by lyx on 16/10/18.
 */
@Component
public class InhospitalPayInfoProtocol extends CloudHospitalProtocol {

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
        String code = MapUtil.getString(params, "Code");
        if (code == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        Inhospital inhospital = inhospitalDao.findPayInfo(code);
        return inhospital;
    }
    @Override
    public String getProtocolCode() {
        return Content.INHOSPITAL_PAYMENT_INFO_CODE;
    }
}
