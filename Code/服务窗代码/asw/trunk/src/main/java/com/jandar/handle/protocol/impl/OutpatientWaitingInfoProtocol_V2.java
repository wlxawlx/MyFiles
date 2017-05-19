package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.alipay.api.internal.util.StringUtils;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.OutpatientOrderReponse;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/**
 * @author yubj
 * 门诊预约候诊信息第二版
 * 支持使用预约流水号来取候诊信息
 */
public class OutpatientWaitingInfoProtocol_V2 implements Protocol {
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
    	HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        String pdlsh = requiredParams(params);
        OutpatientWaitingInfo values =handler.getOutpatientWaitingInfo(pdlsh);


        if (values!=null) {
        	Map<String , String >result=new HashMap<>();
        	result.put("message", values.getMessage());
            return result;
        } else {
            throw new HospitalException("获取门诊候诊信息失败");
        }
    }

    private String requiredParams(Map<String, Object> params) throws HospitalException {

        String yylsh = MapUtil.getString(params, "orderid");
        if (StringUtils.isEmpty(yylsh)) {
            throw new HospitalException("预约流水号不能为空", HospitalException.ARG_ERROR);
        }

        String pdlsh = null;
        /** 获得已经报到的列表 */
        Map<String, Object> trequired = new HashMap<String, Object>();
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        String  yhid=userInfo.getYhid();
        String brid=userInfo.getBrid();
        List<OutpatientOrderReponse> values = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class).getAlreadyOutpatientOrder(yhid,brid);
        for (OutpatientOrderReponse val : values) {
            if (yylsh.equals(val.getPreengageseq())) {
                pdlsh = val.getPdlsh();
                break;
            }
        }

        if (pdlsh == null) {
            throw new HospitalException("该预约还未报到", HospitalException.ERROR);
        }

        Map<String, Object> required = new HashMap<String, Object>();
        // 这里key要对应医院端接口的参数名
        required.put("pdlsh", pdlsh);

        return pdlsh;
    }
}
