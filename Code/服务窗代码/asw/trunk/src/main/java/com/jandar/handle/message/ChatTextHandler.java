package com.jandar.handle.message;

import cloud.hospital.cloudend.message.TextMessage;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.bean.OutMessage;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.im.IMClient;
import com.jandar.util.SpringBeanUtil;
import me.chanjar.weixin.common.api.WxConsts;

/**
 * 文本消息统一处理接口
 * Created by zzw on 16/8/29.
 */
public class ChatTextHandler extends ChatMessageHandler {

    /**
     * 处理文本消息
     *
     * @param fromUser 谁发送的, 支付宝为User_id, 微信为 FromUserName
     * @param toUser   发给谁的, 支付宝为Null, 微信为 ToUserName
     * @param content  发送的内容
     * @param type     是支付宝还是微信发的
     * @return 回复的消息
     */
    public OutMessage handle(String fromUser, String toUser, String content, PlatformType type) {
        try {
            //Patient userInfo = getUserInfo(fromUser, type);
            PatientDao pd = SpringBeanUtil.getBean(PatientDao.class);
            Patient userInfo=pd.findByAlipayUserId(fromUser);

            //建档检查暂时去掉
            //checkReservation(userInfo);
            if(userInfo==null)
            {
                System.out.println("================handle=============user not found==========");
                return null;
            }
            System.out.println("============================="+userInfo.getCurrentDoctorCode());

            // TODO 接收消息后的处理
            //这里暂时根据病人表 的当前医生
            // 如果严格一点， 还要验证该病人有没有预约记录
            if(userInfo.getCurrentDoctorCode()==null)//没有当前医生
            {
                System.out.println("===========ChatTextHandler=======current doctor is null===========");
                return null;
            }
            TextMessage message = new TextMessage();
            message.setFromUser(userInfo.getPatientCode(),userInfo.getName());
            message.setToUser(userInfo.getCurrentDoctorCode(),"张张");


            message.setBody(content);


            System.out.println("------------------send words-----111----"+userInfo.getCurrentDoctorCode());
            IMClient.getInstance().sendMessage(message, userInfo.getCurrentDoctorCode());



        } catch (Exception ex) {
            return buildOutMessage(WxConsts.CUSTOM_MSG_TEXT, fromUser, ex.getMessage());
        }

        return null;
    }
}
