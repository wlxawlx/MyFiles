package com.jandar.handle.protocol.impl.wzstjzx;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.PhysicalItem;
/*
 * @author yubj
 * 
 * 体检套餐列表
 */
import com.jandar.alipay.util.MapUtil;

/*
 * @author jinliangjin86
 * 
 * 体检套餐明细
 */
public class PhysicalPackagesDetailProtocol implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params)
            throws HospitalException {
        String packagesID = MapUtil.getString(params, "tcid");
        if (StringUtils.isBlank(packagesID) || packagesID.equals("null")) {
            throw new HospitalException("套餐ID不能为空", HospitalException.ARG_ERROR);
        }
        HISServiceHandlerWZSRMYY handler = HospitalInfoService
                .getRealInstance(HISServiceHandlerWZSRMYY.class);
        List<PhysicalItem> values = handler
                .getPhysicalPackagesDetail(packagesID);
        Map<String, Object> over = new HashMap<String, Object>();
        List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
        Map<String, List<String>> map = new HashMap<String, List<String>>();
        for (PhysicalItem p : values) {
            if (map.get(p.getItemCategory()) == null) {
                List<String> names = new ArrayList<String>();
                names.add(p.getItemName());
                map.put(p.getItemCategory(), names);
            } else {
                List<String> names = map.get(p.getItemCategory());
                names.add(p.getItemName());
            }
        }
        Iterator<String> iterator = map.keySet().iterator();
        while (iterator.hasNext()) {
            Map<String, Object> result = new HashMap<String, Object>();
            String ItemCategory = iterator.next();
            result.put("list", map.get(ItemCategory));
            result.put("xmfl", ItemCategory);
            result.put("tcid", packagesID);
            list.add(result);
        }
        over.put("list", list);
        return list;
    }
}
