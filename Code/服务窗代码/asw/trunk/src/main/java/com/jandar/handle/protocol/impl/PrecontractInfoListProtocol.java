package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.OutpatientOrderHistory;
import com.jandar.alipay.hospital.PrecontractInfo;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;
import org.apache.commons.lang.StringUtils;

import java.util.*;

// 预约信息列表
public class PrecontractInfoListProtocol implements Protocol {

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        // 其它校验
        String pageindex = MapUtil.getString(params, "pageindex");
        String pagesize = MapUtil.getString(params, "pagesize");
        if (StringUtils.isBlank(pageindex)) {
            throw new HospitalException("pageindex 不能为空");
        } else {
            if (Integer.valueOf(pageindex) <= 0) {
                throw new HospitalException("pageindex 应为>0");
            }
        }
        if (StringUtils.isBlank(pagesize)) {
            throw new HospitalException("pagesize 不能为空");
        } else {
            if (Integer.valueOf(pagesize) <= 0) {
                throw new HospitalException("pagesize 应为>0");
            }
        }

        UserInfo info = ServiceContext.getHospitalUserInfo();
        String yhid = info.getYhid();

        List<OutpatientOrderHistory> numbers = HospitalInfoService.getInstance().outpatientOrderHistory(yhid);

        String orderState = MapUtil.getString(params, "orderstate");

        if (numbers.size() > 0) {
            int pageIndex = Integer.valueOf(pageindex);
            int pageSize = Integer.valueOf(pagesize);
            int fromIndex = (pageIndex - 1) * pageSize;
            int toIndex = (pageIndex - 1) * pageSize + pageSize;
            Map<String, Object> result = new HashMap<>();
            //如果预约状态为0，正序
            if ("0".equals(orderState)) {
                result.put("pageindex", pageindex);
                List<PrecontractInfo> list = new ArrayList<PrecontractInfo>();
                list=returnList(numbers,orderState);
                if (toIndex >= list.size()) {
                    toIndex = list.size();
                }
                Collections.reverse(list);
                result.put("list", list.subList(fromIndex, toIndex));
            }
            //如果预约状态为0，反序
            else if ("1".equals(orderState) || "2".equals(orderState) || "3".equals(orderState)) {
                result.put("pageindex", pageindex);
                List<PrecontractInfo> list = new ArrayList<PrecontractInfo>();
                list=returnList(numbers,orderState);
                if (toIndex >= list.size()) {
                    toIndex = list.size();
                }
                result.put("list", list.subList(fromIndex, toIndex));
            }
            return result;
        } else {
            throw new HospitalException("亲,没有记录");
        }
    }

    protected  List<PrecontractInfo> returnList(List<OutpatientOrderHistory> numbers,String orderState){
        List<PrecontractInfo> list = new ArrayList<>();
        for (OutpatientOrderHistory per : numbers) {
            if (StringUtils.isNotBlank(orderState)) {
                if (!orderState.equals(per.getPreengagestate())) {
                    continue;
                }
            }
            String preengageDate ="";
            String date = per.getPreengagedate();
            if(StringUtils.isEmpty(per.getPreengagetime())){
                preengageDate = DateUtil.dateFormat(date, "yyyy-MM-dd HH:mm:ss", "MM-dd HH:mm");
            }
            if (StringUtils.isNotEmpty(per.getPreengagetime())) {
                date += " " + per.getPreengagetime();
                preengageDate = DateUtil.dateFormat(date, "yyyy-MM-dd HH:mm", "MM-dd HH:mm");
            }
            // 注:该返回的报文去除预约类别！！！
            PrecontractInfo precontractInfo = new PrecontractInfo(
                    per.getPreengageseq(),
                    per.getDoctorname(),
                    per.getDepartname(),
                    per.getPatientname(),
                    per.getPreengageno(),
                    preengageDate,
                    per.getPreengagestate(),
                    per.getDepartcode(),
                    per.getDoctorcode(),
                    per.getFee(),
                    per.getPatientphone(),
                    per.getPlace()
            );
            list.add(precontractInfo);
        }
        return list;
    }
}
