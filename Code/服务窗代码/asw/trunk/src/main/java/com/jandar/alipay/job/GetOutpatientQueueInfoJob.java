package com.jandar.alipay.job;

import com.alipay.util.AlipayMsgSendUtil;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.stereotype.Service;

/**
 * Created by zzw on 16/3/7.
 * 门诊排队信息
 */
@Service("getOutpatientQueueInfoJob")
public class GetOutpatientQueueInfoJob {

    private final Log log = LogFactory.getLog(getClass());

    public void execute() {

        log.info("获得门诊排队信息及推送任务开始");

        String openId = null;
        String title = null;
        StringBuffer message = new StringBuffer();
        String url = null;

//        AlipayMsgSendUtil.sendSingleImgTextMsg(openId, title, message.toString(), url, "详情");

        log.info("获得门诊排队信息及推送任务结束");
    }
}
