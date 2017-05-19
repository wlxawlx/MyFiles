package com.jandar.handle.protocol.impl;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import com.jandar.handle.protocol.Protocol;
import org.apache.commons.collections.MapUtils;
import org.apache.commons.lang.StringUtils;

import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.hospital.UserInfo;

/*
 * 分析:
 * 		1.调用数据库传入的参数修改
 * 		2.返回的参数加入到返回集合中
 */
// 预约取消
public class PrecontractCancelProtocol implements Protocol {

    /** 线程池 */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
    	final String yymsg  =  HospitalInfoService.getInstance().cancelOutpatientOrderResultMessage(requiredParam1(params),requiredParam2(params));
        if (!StringUtils.isBlank(yymsg)) {
            final Map<String, String> result = new HashMap<>();
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            final String alipayuserid = userInfo.getAlipayUserId();
            result.put("cancelmsg", yymsg);
            executors.execute(new Runnable() {
                @Override
                public void run() {
                    AlipayMsgSendUtil.sendSingleImgTextMsg(alipayuserid, "取消预约", yymsg);
                }
            });
            return result;
        } else {
            throw new HospitalException("取消预约失败");
        }
    }

    private String requiredParam2(Map<String, Object> params) throws HospitalException {
    	String preengageseq=MapUtils.getString(params, "orderid");
    	if(StringUtils.isBlank(preengageseq)){
    		throw new HospitalException("未输入预约流水号");
    	}
		return preengageseq;
	}

	private String requiredParam1(Map<String, Object> params) throws HospitalException {
		UserInfo info=ServiceContext.getHospitalUserInfo();
		String openid=info.getAlipayUserId();
		return openid;
	}
}
