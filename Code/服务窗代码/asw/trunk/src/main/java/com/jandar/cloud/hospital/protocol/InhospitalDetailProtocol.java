package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.bean.Prescription;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.collections.map.HashedMap;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.List;
import java.util.Map;

/**
 * 住院单明细
 * Created by lyx on 16/9/29.
 */
@Component
public class InhospitalDetailProtocol extends CloudHospitalProtocol {

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
        Inhospital inhospitalInfo=inhospitalDao.inhospital(code);
        if ( inhospitalInfo == null)
        {
            throw new HospitalException("住院单号不存在！", CloudHospitalException.INHOSPITAL_CODE_IS_EMPTY);
        }
        return inhospitalInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.INHOSPITAL_DETAIL_CODE;
    }
}
