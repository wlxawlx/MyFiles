package com.jandar.handle.protocol.impl;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang.StringUtils;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.InHospitalOutlays;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;

/*
 * @author yubj
 * 
 * 住院信息-住院费用0.2版
 */
public class InpatientPayInfoQuery implements Protocol {
    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {
    	  String inHospitalCode = MapUtil.getString(params, "inhospitalcode");
          String fyrq = MapUtil.getString(params, "fyrq");
    	List<InHospitalOutlays> values = HospitalInfoService.getInstance().getInhospitalOutlaysList(inHospitalCode, fyrq);
    	Map<String , Object > inlist=new HashMap<String , Object>();
    	List<Map<String , Object >>list=new ArrayList<Map<String , Object >>();
        if (values.size() > 0) {
        	for(InHospitalOutlays resultMap:values){
        		Map<String ,Object > result=new HashMap<String ,Object>();
        		 result.put("fyxm", resultMap.getCostCode());
                 result.put("xmmc", resultMap.getCostName());
                 result.put("zjje", format(resultMap.getTotalFee()));
                 result.put("zfje", format(resultMap.getPayFee()));
                 list.add(result);
        	}
           
        } else {
            throw new HospitalException("获取住院病人费用信息失败");
        }
       inlist.put("list", list);
        return inlist;
    }


    private String format(String str) {
        if (StringUtils.isBlank(str)) {
            return "0.00";
        } else {
            Double d = Double.parseDouble(str);
            DecimalFormat df = new DecimalFormat("##############.00");
            return df.format(d);
        }
    }

}
