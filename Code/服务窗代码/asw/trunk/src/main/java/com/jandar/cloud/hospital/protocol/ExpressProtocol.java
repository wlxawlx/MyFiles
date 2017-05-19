package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Express;
import com.jandar.cloud.hospital.bean.Inhospital;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.ExpressDao;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 物流详情
 * Created by lyx on 2016/10/20.
 */
@Component
public class ExpressProtocol extends CloudHospitalProtocol {


    @Resource
    private ExpressDao expressDao;
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
        Express expressInfo = expressDao.findByCode(code);
        if (expressInfo == null) {
            throw new HospitalException("没有此处方单的物流信息！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        return expressInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.EXPRESSINFO_CODE;
    }
}
