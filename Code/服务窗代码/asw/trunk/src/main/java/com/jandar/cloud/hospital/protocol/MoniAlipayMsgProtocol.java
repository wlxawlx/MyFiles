package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.util.MapUtil;
import com.jandar.bean.OutMessage;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.handle.message.ChatTextHandler;
import org.springframework.stereotype.Component;

import java.util.Map;

/**
 * Created by flyhorse on 2016/11/4.
 */
@Component
public class MoniAlipayMsgProtocol extends CloudHospitalProtocol{


    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        String content = MapUtil.getString(params, "Content");
        if (content == null) {
            throw new HospitalException("请求参数为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String alipayUserId = MapUtil.getString(params, "alipayUserId");
        if (alipayUserId == null) {
            throw new HospitalException("请求参数为空alipayUserId！", CloudHospitalException.REQUEST_IS_EMPTY);
        }

        System.out.println("模拟支付宝消息moni alipay msg:"+content);



        OutMessage outMessage = new ChatTextHandler().handle(alipayUserId, null, content, PlatformType.Alipay);

        return null;
    }
    @Override
    public String getProtocolCode() {
        return Content.MONI_ALIPAY_MSG_CODE;
    }
}
