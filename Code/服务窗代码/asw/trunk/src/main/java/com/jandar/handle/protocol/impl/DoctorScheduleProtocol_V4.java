package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DoctorScheduleInfo;
import com.jandar.alipay.core.struct.OutpatientOrderNumber;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;

/*
 * @author yubj
 * 
 * 医生排班表0.4
 */
public class DoctorScheduleProtocol_V4 implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
        String doctorid = MapUtil.getString(params, "doctorid");
        String departcode = MapUtil.getString(params, "departid");
        String scheduledate = MapUtil.getString(params, "scheduledate"); //  yyyy-MM-dd
        List<DoctorScheduleInfo> values = HospitalInfoService.getInstance().getDoctorSchedulForOrder(doctorid, departcode, scheduledate);
        System.out.println("DoctorScheduleProtocol_V4:process1::"+values);
        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<String, Object>();
            result.put("doctorid", params.get("doctorid"));
            System.out.println("DoctorScheduleProtocol_V4:process2::"+values);
            List<Map<String, Object>> list = buildDepartList(values);
            System.out.println("DoctorScheduleProtocol_V4:process3::"+list);
            result.put("departlist", list);
            return result;
        } else {
            throw new HospitalException("获取医生排班表失败");
        }
    }

    private ArrayList<Map<String, Object>> buildDepartList(List<DoctorScheduleInfo> values) throws HospitalException {
        Map departInfos = new HashMap();
        Map schedules = new HashMap();
        for (DoctorScheduleInfo per : values) {
            String departid = per.getDepartcode();
            String departname = per.getDepartname();
            Map departInfo = null;
            if (schedules.get(departid) == null) {
                schedules.put(departid, new ArrayList());
                ((List)schedules.get(departid)).add(buildScheduleList(per));
            } else {
                ((List)schedules.get(departid)).add(buildScheduleList(per));
            }

            if (departInfos.containsKey(departid)) {
                departInfo = (Map)departInfos.get(departid);
            } else {
                departInfo = new HashMap();
                departInfo.put("departid", departid);
                departInfo.put("departname", departname);
                departInfo.put("remain", Integer.valueOf(0));
                departInfo.put("total", Integer.valueOf(0));
                departInfo.put("list", schedules.get(departid));
                departInfos.put(departid, departInfo);
            }
            try
            {
                Integer remain = Integer.valueOf(per.getRemain());
                Integer total = Integer.valueOf(per.getTotal());
                departInfo.put("remain", Integer.valueOf(((Integer)departInfo.get("remain")).intValue() + remain.intValue()));
                departInfo.put("total", Integer.valueOf(((Integer)departInfo.get("total")).intValue() + total.intValue()));
            } catch (NumberFormatException ex) {
                ex.printStackTrace();
            }

        }

        return new ArrayList(departInfos.values());

    }

    /**
     * 组织排班信息与获得当前这个排班下的具体排班号源
     *
     * @param per 医生排班对象
     * @return 医生门诊号源信息
     */
    private Map<String, Object> buildScheduleList(DoctorScheduleInfo per) {
        Map<String, Object> item = new HashMap<String, Object>();
        String scheduleseq = per.getScheduleseq();
        String shiftcode = per.getShiftcode();
        item.put("scheduleseq", scheduleseq);
        item.put("departid", per.getDepartcode());
        item.put("departname", per.getDepartname());
        item.put("doctorid", per.getDoctorcode());
        item.put("doctorname", per.getDoctorname());
        item.put("special", per.getSpecial());
        item.put("remain", per.getRemain());
        item.put("total", per.getTotal());
        item.put("address", per.getAddress());
        item.put("scheduledate", DateUtil.dateFormat(per.getScheduledate(), "yyyy-MM-dd", "MM-dd"));
        item.put("weekday", DateUtil.translateDate2Week(per.getScheduledate()));    //周几
        item.put("shiftcode", per.getShiftcode());
        item.put("shiftname", per.getShiftname());
        item.put("fee", per.getFee());
        if (StringUtils.isNotEmpty(scheduleseq)) {
            List<OutpatientOrderNumber> haoyuanList = new ArrayList<>();
            try {
                haoyuanList = HospitalInfoService.getInstance().getOutpatientOrderNumbers(per.getDoctorcode(), scheduleseq, shiftcode);
            } catch (HospitalException e) {
                e.printStackTrace();
                // 获取号源失败
            }
            item.put("scheduleseqlist", haoyuanList);
        }
        return item;
    }
}
