package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.core.struct.Inspection;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang3.StringUtils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class TestsListProtocol implements Protocol {

    /**
     * @author 谈栋杰
     * <p/>
     * 1.修改的是数据库调用方法，请求参数和返回值修改
     * <p/>
     * <p/>
     * 获取化验单列表
     */
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        String name = null;
        String idCardNo = null;
        UserInfo info = ServiceContext.getHospitalUserInfo();
        String linkmanid = MapUtil.getString(params, "linkmanid");
        if (StringUtils.isBlank(linkmanid)) {
            name = info.getYhxm();
            idCardNo = info.getSfzh();
        } else {
            List<ContactPeopleInfo> contactsList = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);

            if (contactsList.size() > 0) {
                ContactPeopleInfo value = contactsList.get(0);
                idCardNo = value.getIdcardno();
                name = value.getName();
            }
            if (StringUtils.isBlank(idCardNo)) {
                throw new HospitalException("联系人不存在");
            }
        }

        //name,idcardno 姓名,身份证
        List<Inspection> values = HospitalInfoService.getInstance().getTestsList(name, idCardNo);
        Map<String, Object> result = new HashMap<>();
        result.put("name", name);
        List<Map<String, Object>> list = new ArrayList<>();
        for (Inspection value : values) {
            Map<String, Object> items = new HashMap<String, Object>();
            items.put("doctadviseno", value.getDoctadviseno());
            items.put("examinaim", value.getExaminaim());
            items.put("requesttime", DateUtil.dateFormat(value.getRequesttime(), "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm"));
            items.put("requester", value.getRequester());
//
            list.add(items);
        }
       result.put("list", list);
        return result;
    }

}
