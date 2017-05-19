package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.InspectChemicalResult;
import com.jandar.cloud.hospital.bean.InspectResult;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.InspectChemicalResultDao;
import com.jandar.cloud.hospital.dao.InspectResultDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.Map;

/**
 * 检查单详情
 * Created by lyx on 16/10/11.
 */
@Component
public class InspectlResultProtocol extends CloudHospitalProtocol {

    @Resource
    private InspectResultDao inspectResultDao;
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
        String doctadviseno = MapUtil.getString(params, "Doctadviseno");
        if (doctadviseno == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        InspectResult inspectResult = inspectResultDao.findInspectResult(doctadviseno);
        if (inspectResult == null) {
            throw new HospitalException("条码号无效！", CloudHospitalException.DOCTADVISENO_IS_EMPTY);
        }
        return inspectResult;
    }

    @Override
    public String getProtocolCode() {
        return Content.INSPECTLRESULT_CODE;
    }


}
