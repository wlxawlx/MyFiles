package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.InHospitalOutlays;
import com.jandar.alipay.core.struct.InhospitalPatientInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.util.MapUtil;

/*
 * 住院信息-住院费用明细0.3版
 */
public class HospitalizationInformationDetailProtocol_V3 implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo info = ServiceContext.getHospitalUserInfo();
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
            throw new HospitalException("用户名为空");
        }
        InhospitalPatientInfo value1 = HospitalInfoService.getInstance().inhospitalPatientInfo(idcardno, name);
        String zyh = value1.getInpatientno();
        if (StringUtils.isBlank(zyh)) {
            throw new HospitalException("住院号为空");
        }

        // 获得住院费用
        Map<String, Object> items = new HashMap<String, Object>();
        String fyxm = MapUtil.getString(params, "fyxm");
        String fyrq = MapUtil.getString(params, "fyrq");
        if (fyrq != null) {
            items.put("fyrq", fyrq);
        } else {
            items.put("fyrq", "");
        }
        List<InHospitalOutlays> m1 = HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class).getInhospitalOutlaysDetail(zyh, fyxm, fyrq);
        // fyrq 费用日期 string 是 格式：yyyy-MM-dd，如果为空时，获得所有费用列表

        List<Map<String, Object>> list = new ArrayList<>();
        for (InHospitalOutlays m2 : m1) {
            Map<String, Object> item = new HashMap<String, Object>();
            item.put("fyxh", m2.getFyxh());
            item.put("fyxm", m2.getFyxm());
            item.put("fymc", m2.getFymc());
            item.put("fygg", m2.getFygg());
            item.put("fydw", m2.getFydw());
            item.put("ybdm", m2.getYbdm());
            item.put("zfbl", m2.getZfbl());
            item.put("yplx", m2.getYplx());
            item.put("fysl", m2.getFysl());
            item.put("fydj", m2.getFydj());
            item.put("zjje", m2.getZjje());
            item.put("zfje", m2.getZfje());
            list.add(item);
        }
        Map<String, Object> result = new HashMap<String, Object>();
        result.put("list", list);
        return result;
    }
}
