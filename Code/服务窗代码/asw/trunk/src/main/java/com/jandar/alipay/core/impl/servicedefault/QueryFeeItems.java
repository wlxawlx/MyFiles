package com.jandar.alipay.core.impl.servicedefault;

import com.jandar.alipay.core.struct.FeeItemsInfo;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 收费项目-无查询条件，分页 - 查询基本功能
 * Created by yubj on 2016/4/12.
 */
public class QueryFeeItems {
    public Map<String, Object> queryFeeItemsRequest(String pageno, String pagerow) {
        Map<String, Object> parameters = new HashMap<>();
        parameters.put("pageno", pageno);
        parameters.put("pagerow", pagerow);
        return parameters;
    }

    public List<FeeItemsInfo> queryFeeItemsResponse(List<Map<String, String>> values) {
        List<FeeItemsInfo> list = new ArrayList<FeeItemsInfo>();
        for (Map<String, String> map : values) {
            FeeItemsInfo feeItemsInfo = new FeeItemsInfo();
            feeItemsInfo.setFylx(map.get("fylx"));
            feeItemsInfo.setFymc(map.get("fymc"));
            feeItemsInfo.setFydw(map.get("fydw"));
            feeItemsInfo.setFyjg(map.get("fyjg"));
            list.add(feeItemsInfo);
        }
        return list;
    }
}
