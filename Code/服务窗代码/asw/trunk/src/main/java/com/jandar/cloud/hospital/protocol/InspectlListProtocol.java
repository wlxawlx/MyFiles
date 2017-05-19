package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.InspectChemicalResult;
import com.jandar.cloud.hospital.bean.InspectResult;
import com.jandar.cloud.hospital.bean.InspectMain;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.InspectChemicalResultDao;
import com.jandar.cloud.hospital.dao.InspectResultDao;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.text.SimpleDateFormat;
import java.util.*;

/**
 * 检验、检查单列表
 * Created by lyx on 16/10/10.
 */
@Component
public class InspectlListProtocol extends CloudHospitalProtocol {

    @Resource
    private InspectChemicalResultDao inspectChemicalResultDao;

    @Resource
    private InspectResultDao inspectResultDao;

    @Resource
    private InspectionDao inspectionDao;

    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {

        //取得用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null)
        {
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }

        String code = MapUtil.getString(params, "Code");
        if (code == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String type = MapUtil.getString(params, "Type");
        if (type == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //根据type的值判断要返回什么类型的检查单
        if (INSPECTL_CHEMICAL_RESULT.equals(type)) {
            List<InspectChemicalResult> inspectChemicalResultInfo = inspectChemicalResultDao.inspectChemicalResultInfo(code);
            if (inspectChemicalResultInfo == null || inspectChemicalResultInfo.size() == 0) {
                throw new HospitalException("检验单号不存在！", CloudHospitalException.INSPECT_IS_EMPTY);
            }
            return inspectChemicalResultInfo;
        } else if (INSPECTL_RESULT.equals(type)) {
            List<InspectResult> inspectResultInfo = inspectResultDao.inspectResultInfo(code);
            if (inspectResultInfo == null || inspectResultInfo.size() == 0) {
                throw new HospitalException("检查单号不存在！", CloudHospitalException.INSPECT_IS_EMPTY);
            }
            return inspectResultInfo;

        } else if (INSPECTL_ADD_RESULT.equals(type)) {
            HashMap<String, Object> map = new HashMap<>();
            List<InspectChemicalResult> inspectChemicalResultInfo = inspectChemicalResultDao.inspectChemicalResultInfo(code);
            List<InspectResult> inspectResultInfo = inspectResultDao.inspectResultInfo(code);
            InspectMain inspection = inspectionDao.findByCode(code);
            if (inspection == null) {
                throw new HospitalException("检查单号不存在！", CloudHospitalException.INSPECT_IS_EMPTY);
            }
            List<String> list = new ArrayList<>();
            if (inspectResultInfo==null||inspectResultInfo.size()==0)
            {
                map.put("InspectResult", list);
            }
            else
            {
                map.put("InspectResult", inspectResultInfo);
            }
            if (inspectChemicalResultInfo==null||inspectChemicalResultInfo.size()==0)
            {
                map.put("InspectChemicalResult", list);
            }
            else
            {
                map.put("InspectChemicalResult", inspectChemicalResultInfo);
            }
            map.put("PatientName", inspection.getPatientName());
            map.put("DoctorName", inspection.getDoctorName());
            return map;
        } else {
            throw new HospitalException("检查单类型未找到！", CloudHospitalException.INSPECT_TYPE_IS_EMPTY);
        }
    }

    @Override
    public String getProtocolCode() {
        return Content.INSPECTL_LIST_CODE;
    }

    /**
     * 检验结果类型
     */
    public final static String INSPECTL_CHEMICAL_RESULT = "1";
    /**
     * 检查结果类型
     */
    public final static String INSPECTL_RESULT = "2";
    /**
     * 检查结果+检验结果类型
     */
    public final static String INSPECTL_ADD_RESULT = "3";
}
