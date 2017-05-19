package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.InHospitalOutlays;
import com.jandar.alipay.core.struct.InhospitalPatientInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * 住院信息-住院费用0.3版
 */
public class HospitalizationInformationCostsProtocol_V3 implements Protocol {
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
        // linkmanid联系人ID空或-1时查询本人的信息
        String linkmanid = MapUtil.getString(params, "linkmanid");
        String idcardno = null;
        String name = null;
        if (StringUtils.isBlank(linkmanid) || "-1".equals(linkmanid)) {
            idcardno = info.getSfzh();
            name = info.getYhxm();
        } else {
            List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);
            if (contactsList.size() > 0) {
                ContactPeopleInfo value = contactsList.get(0);
                idcardno = value.getIdcardno();
                name = value.getName();
            }
        }

        if (StringUtils.isBlank(idcardno)) {
            throw new HospitalException("身份证号为空");
        }
        if (StringUtils.isBlank(name)) {
            throw new HospitalException("姓名为空");
        }

        InhospitalPatientInfo value1 = HospitalInfoService.getInstance().inhospitalPatientInfo(idcardno, name);
        String zyhm = value1.getInpatientno();

        if (StringUtils.isBlank(zyhm)) {
            throw new HospitalException("住院号为空");
        }

        String fyrq = MapUtil.getString(params, "fyrq");
        // 获得住院费用
        List<InHospitalOutlays> sales = HospitalInfoService.getInstance().getInhospitalOutlaysList(zyhm, fyrq);
        List<Map<String, Object>> list = new ArrayList<>();
        for (InHospitalOutlays sale : sales) {
            Map<String, Object> item = new HashMap<String, Object>();
            item.put("fyxm", sale.getCostCode());
            item.put("xmmc", sale.getCostName());
            item.put("zjje", sale.getTotalFee());
            item.put("zfje", sale.getPayFee());
            list.add(item);
        }

        Map<String, Object> result = new HashMap<>();
        result.put("list", list);
        return result;
    }
}
