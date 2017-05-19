package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Department;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.DepartmentDao;
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
 * 科室列表
 * Created by lyx on 2016/10/19.
 */
@Component
public class DepartmentInfoProtocol extends CloudHospitalProtocol {


    @Resource
    private DepartmentDao departmentDao;
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
        List<Department> departmentInfo = departmentDao.findAll();
        return departmentInfo;
    }

    @Override
    public String getProtocolCode() {
        return Content.DEPARTMENT_CODE;
    }
}
