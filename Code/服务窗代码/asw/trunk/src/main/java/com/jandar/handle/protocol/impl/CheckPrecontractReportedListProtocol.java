package com.jandar.handle.protocol.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerWZSRMYY;
import com.jandar.alipay.core.struct.wzsrmyy.OutpatientWaitingList;
import com.jandar.handle.protocol.Protocol;

/**
 * 检查预约报道列表
 * 
 * @author yubj
 */
public class CheckPrecontractReportedListProtocol implements Protocol {

	@Override
	public Object process(String pcode, Map<String, Object> params) throws HospitalException {
		// TODO Auto-generated method stub
		HISServiceHandlerWZSRMYY handler=HospitalInfoService.getRealInstance(HISServiceHandlerWZSRMYY.class);
		List<OutpatientWaitingList> values=handler.checkPrecontractReportedList(null, null);
		Map<String , Object > info=new HashMap<String , Object>();
		List<Map<String , Object >>list=new ArrayList<Map<String , Object >>();
		for(OutpatientWaitingList map:values){
			Map<String , Object > result=new HashMap<String , Object >();
			result.put("yylsh", map.getYylsh());
			result.put("brxm", map.getBrxm());
			result.put("fzxh", map.getFzxh());
			result.put("yysj", map.getYysj());
			result.put("bdzt", map.getBdzt());
			result.put("pdlsh",map.getPdlsh());
			result.put("pdhm", map.getPdhm());
			result.put("pdrq", map.getPdrq());
			result.put("bcmc", map.getBcmc());
			result.put("dlxm", map.getDlxm());
			result.put("dlmc", map.getDlmc());
			result.put("jcxm", map.getJcxm());
			result.put("zsmc", map.getZsmc());
			result.put("zswz", map.getZswz());
			result.put("fjhm", map.getFjhm());
			result.put("pdzt", map.getPdzt());
			OutpatientWaitingList  value=handler.checkPrecontractReportedNotice(map.getYylsh());
			result.put("zysx", value.getZysx());
			list.add(result);
		}
		info.put("list", list);
		return info;
	}

}
