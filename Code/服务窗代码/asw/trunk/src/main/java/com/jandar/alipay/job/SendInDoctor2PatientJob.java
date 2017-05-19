package com.jandar.alipay.job;

import java.util.List;

import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.VisitInfo;
import com.jandar.alipay.util.DateUtil;
import com.jandar.config.ConfigHandler;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

/**
 * 发送就诊信息到病人支付宝通知栏中
 * 预约后（包括支付宝预约和其他预约）离开预约日期的前三天和就诊当天，发送服务窗消息提醒患者就诊。
 */
//@Service("getOutpatientQueueInfoJob")
public class SendInDoctor2PatientJob {

    private final Log log = LogFactory.getLog(getClass());

    public void execute() {

        log.info("开始任务");
        List<VisitInfo> values = null;
        try {
			values = HospitalInfoService.getInstance().InformVisitInfo();
		} catch (HospitalException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        for(VisitInfo v:values){
        String openId =v.getUserid() ;
        String title = "预约就诊通知";
        StringBuffer message = new StringBuffer();
        message.append("尊敬的");
        message.append(v.getBrxm());
        message.append("先生（女士），请您于");
        message.append(DateUtil.dateFormat(v.getJzrq(), "yyyy-MM-dd", "yyyy年MM月dd日"));
        message.append(DateUtil.dateFormat(v.getJzsj(), "HH:mm", "HH点mm分"));
        message.append("到");
        message.append(v.getJzdz());
        message.append("候诊，");
        message.append(v.getYsxm());
        message.append("医生将为您服务。");
        String url = ConfigHandler.getSelfServiceHost()+"/orderHistory.html";

        AlipayMsgSendUtil.sendSingleImgTextMsg(openId, title, message.toString(), url);

        }
        log.info("结束任务");
    }
}