package com.alipay.executor;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.common.MyException;
import com.alipay.util.AlipayMsgBuildUtil;
import com.alipay.util.AlipayMultiMediaUtil;
import com.alipay.util.OutMessageUtil;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.OutMessage;
import com.jandar.config.ConfigHandler;
import com.jandar.handle.message.ChatImageHandler;
import com.jandar.util.MessageSendUtil;
import net.sf.json.JSONObject;

import java.io.File;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 图片消息
 * Created by zzw on 16/8/29.
 */
public class InAlipayChatImageExecutor implements ActionExecutor {

    /**
     * 线程池
     */
    private static ExecutorService executors = Executors.newSingleThreadExecutor();

    /**
     * 业务参数
     */
    private JSONObject bizContent;

    public InAlipayChatImageExecutor(JSONObject bizContent) {
        this.bizContent = bizContent;
    }

    public InAlipayChatImageExecutor() {
        super();
    }

    @Override
    public String execute() throws MyException {
        //取得发起请求的支付宝账号id
        final String fromUserId = bizContent.getString("FromUserId");

        // 取得用户发送的消息内容
        final String mediaId = bizContent.getJSONObject("Image").getString("MediaId");

        //1. 首先同步构建ACK响应
        String syncResponseMsg = AlipayMsgBuildUtil.buildBaseAckMsg(fromUserId);

        executors.execute(new Runnable() {
            @Override
            public void run() {
                //根目录
                final String basePath = ConfigHandler.getChatImageSavePath();
                //图片保存地址
                String savePath = basePath + File.separator + mediaId + "." + "jpg";
                AlipayMultiMediaUtil.downloadImage(mediaId, savePath);

                //2. 异步发送消息
                OutMessage outMessage = new ChatImageHandler().handle(fromUserId, null, savePath, PlatformType.Alipay);
                AlipayOpenPublicMessageCustomSendModel model = OutMessageUtil.OutMessage2AlipayMessage(outMessage);
                if (model != null) {
                    MessageSendUtil.messageSend(model);
                }
            }
        });

        // 3.返回同步的ACK响应
        return syncResponseMsg;
    }


}
