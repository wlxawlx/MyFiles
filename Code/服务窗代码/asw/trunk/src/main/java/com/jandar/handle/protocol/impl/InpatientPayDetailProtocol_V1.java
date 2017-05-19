package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.InHospitalOutlays;
import com.jandar.alipay.core.struct.InhospitalPatientInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.util.StringUtil;

/*
 * 住院信息-住院费用明细0.1版
 */
public class InpatientPayDetailProtocol_V1 implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        HISServiceHandlerWZSRMYY handler = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
        String inhospitalcode = requiredParams(null);
        String fyxm = MapUtil.getString(params, "fyxm");
        if (StringUtil.isBlank(fyxm)) {
            throw new HospitalException("费用项目不能为空");
        }
        String fyrq = MapUtil.getString(params, "fyrq");
        if (fyrq == null) {
            fyrq = "";
        }
        List<InHospitalOutlays> values = handler.getInhospitalOutlaysDetail(inhospitalcode, fyxm, fyrq);
        if (values.size() > 0) {
            List<Map<String, String>> result = new ArrayList<Map<String, String>>();
            for (InHospitalOutlays sale : values) {
                if (StringUtil.isNotBlank(fyrq) || "".equals(fyrq)) {
                    Map<String, String> map = new HashMap<>();
                    map.put("fyxh", sale.getFyxh());
                    map.put("fyxm", sale.getFyxm());
                    map.put("fymc", sale.getFymc());
                    map.put("fygg", sale.getFygg());
                    map.put("fydw", sale.getFydw());
                    map.put("ybdm", sale.getYbdm());
                    map.put("zfbl", sale.getZfbl());
                    map.put("yplx", sale.getYplx());
                    map.put("fysl", sale.getFysl());
                    map.put("fydj", sale.getFydj());
                    map.put("zjje", sale.getZjje());
                    map.put("zfje", sale.getZfje());
                    result.add(map);
                }
                if (StringUtil.isNotBlank(fyrq)) {
                    break;
                }
            }
            return result;
        } else {
            throw new HospitalException("获取住院病人费用明细失败");
        }
    }

    private String requiredParams(Map<String, Object> params) throws HospitalException {
        Map<String, Object> required = new HashMap<String, Object>();
        // 其它校验
        UserInfo info = ServiceContext.getHospitalUserInfo();
        String sfzh = info.getSfzh();
        String name = info.getYhxm();
        if (StringUtil.isBlank(sfzh)) {
            throw new HospitalException("身份证号为空");
        }
        InhospitalPatientInfo value1 = HospitalInfoService.getInstance().inhospitalPatientInfo(sfzh, name);
        if (StringUtil.isBlank(value1.getInpatientno())) {
            throw new HospitalException("住院号为空");
        }
        String zyh = value1.getInpatientno();

        return zyh;
    }
}
