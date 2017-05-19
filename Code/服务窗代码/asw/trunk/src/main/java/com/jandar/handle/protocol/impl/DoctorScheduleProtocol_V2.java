package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.DoctorScheduleInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.DateUtil;
import com.jandar.alipay.util.MapUtil;
/*
 * @author yubj
 * 
 * 医生排班表0.2
 */
public class DoctorScheduleProtocol_V2 implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	String doctorid=MapUtil.getString(params, "doctorid");
        // 后台需求加参数 departid, scheduledate
        String departid = MapUtil.getString(params, "departid");
        String scheduledate = MapUtil.getString(params, "scheduledate"); //  yyyy-MM-dd
        
        List<DoctorScheduleInfo> values = HospitalInfoService.getInstance().getDoctorSchedulForOrder(doctorid, departid, scheduledate);



        if (values.size() > 0) {
            Map<String, Object> result = new HashMap<>();
            result.put("doctorid", params.get("doctorid"));
            List<Map<String, String>> forenoon = new ArrayList<Map<String, String>>();
            List<Map<String, String>> afternoon = new ArrayList<Map<String, String>>();
            result.put("weekday", DateUtil.translateDate2Week(scheduledate));
            // per.get("pblsh"),per.get("ksbm"),per.get("ksmc"),per.get("ysbm"),per.get("ysxm"),
            // per.get("txbz"),per.get("kyrs"),per.get("yyzs"),per.get("jzdd"),per.get("pbrq"),
            //        per.get("bcbm"),per.get("bcmc"),per.get("ghf")
            for (int i = 0; i < values.size(); i++) {
            	DoctorScheduleInfo per = values.get(i);
                if (departid.equals(per.getDepartcode()) && scheduledate.equals(per.getScheduledate())) {
                    Map<String, String> item = new HashMap<String, String>();
                 
                    item.put("scheduleseq", per.getScheduleseq());
                    item.put("special", per.getSpecial());
                    item.put("remain", per.getRemain());
                    item.put("total", per.getTotal());
                    item.put("address", per.getAddress());
                    item.put("fee", per.getFee());
                    if ("1".equals(per.getShiftcode())) {
                        forenoon.add(item);
                    }
                    if ("2".equals(per.getShiftcode())) {
                        afternoon.add(item);
                    }
                    if (result.get("departname") == null) {
                        result.put("departname", per.getDepartname());
                        result.put("doctorname", per.getDoctorname());
                        result.put("scheduledate", per.getScheduledate());
                    }
                }

            }
            result.put("forenoon", forenoon);
            result.put("afternoon", afternoon);
            return result;
        } else {
            throw new HospitalException("获取医生排班表失败");
        }
    }
}
