package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.core.struct.DoctorInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.*;

/**
 * 云医院医生列表
 */
@Component
public class DoctorListAllProtocol extends CloudHospitalProtocol {
    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        String namepy=MapUtil.getString(params, "pydm");
        String pageindex=MapUtil.getString(params, "pageindex");
        String pagesize=MapUtil.getString(params, "pagesize");
        String hasKey="1";
        if(StringUtils.isNoneBlank(namepy)){
            hasKey="0";
        }
        List<DoctorInfo> values = HospitalInfoService.getRealInstance(HISServiceHandlerDefault.class).getDoctorAll(namepy, pageindex, pagesize,hasKey);
        Map<String , Object > over=new HashMap<String ,Object>();
        List<Map<String , Object >>list=new ArrayList<Map<String , Object>>();
        for(DoctorInfo info:values) {
            Map<String , Object > result=new HashMap<String,Object>();
            result.put("ksmc", info.getDepartname());
            result.put("ysxb", info.getSex());
            result.put("ksbm", info.getDepartcode());
            result.put("ysbm", info.getCode());
            result.put("zcmc", info.getLevel());
            result.put("ysxm", info.getName());
            result.put("yysl", info.getReserve());
            list.add(result);
        }
        over.put("list", list);
        return list;
    }

    @Override
    public String getProtocolCode() {
        return Content.LIST_ALL_DOCTOR;
    }
}
