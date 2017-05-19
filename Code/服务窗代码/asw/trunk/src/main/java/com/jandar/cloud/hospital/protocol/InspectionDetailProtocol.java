package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;


import javax.annotation.Resource;
import java.util.Map;

/**
 * 检查单详情CH010903
 * Created by admin on 2016/9/28.
 */
@Component
public class InspectionDetailProtocol  extends CloudHospitalProtocol {

    @Resource
    private InspectionDao inspectionDao;
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
        return inspectionDao.findByInspectionCode(code);
    }
    @Override
    public String getProtocolCode() {return Content.INSPECTION_DETAIL_CODE;}
}
