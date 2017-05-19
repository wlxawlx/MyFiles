package com.jandar.cloud.hospital.protocol;

import cloud.hospital.cloudend.message.TemplateMessage;
import com.alipay.api.domain.Article;
import com.alipay.util.AlipayMsgSendUtil;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.util.MapUtil;
import com.jandar.bean.AlipayImageTextMessageTotalModel;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.*;
import com.jandar.cloud.hospital.dao.InhospitalDao;
import com.jandar.cloud.hospital.dao.InspectionDao;
import com.jandar.cloud.hospital.dao.LeaveMessageDao;
import com.jandar.cloud.hospital.dao.PrescriptionDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.im.IMClient;
import com.jandar.cloud.hospital.msg.MessageTemplate;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.MessageSendUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import javax.jws.WebService;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * 挂号完的排队通知
 * Created by yh on 2017/02/16.
 */
@Component
public class SentLineMessageProtocol extends CloudHospitalProtocol {


    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        //发送即时消息
        MessageTemplate messageTemplate=new MessageTemplate();
        if(params.get("lines")==null){
            throw new HospitalException("请传入队伍参数");
        }
        //获取需要通知的用户队列表
        List<Map<String,String>>lines= (ArrayList<Map<String,String>>)params.get("lines");
        System.out.println("lines:*******************************");
        System.out.print(lines);
        String []needParas=new String[]{"openid","pdhm","brxm","ysxm","preengageno","shiftname","departname","sourcetype","place"};
        for(String para:needParas){
            for(Map<String,String> line:lines){
                if(!line.containsKey(para)){
                    throw new HospitalException("缺少"+para+"参数");
                }
            }
        }
         if (lines.size()>0){
               for(Map<String,String> line:lines){
                   String msgContent="";
                   String openid=line.get("openid");//支付宝账号
                   String pdhm= line.get("pdhm");//排队号码
                   String brxm=line.get("brxm");//病人姓名
                   String ysxm=line.get("ysxm");//医生姓名
                   String preengageno=line.get("preengageno");//分诊序号
                   String shiftname= line.get("shiftname");//上下午
                   String departname=line.get("departname");//科室名称
                   if("9".equals(line.get("sourcetype"))){//如果是云医院

                       msgContent=brxm+"您好！您挂的"
                               +departname
                               +ysxm+
                               "医生"+
                               shiftname+
                               preengageno+
                               "号当前已经排到第"+pdhm+"位，请您提早做好准备，避免过号！";

                   }else {
                       String place=line.get("place");
                       msgContent=brxm+"您好！您挂的"
                               +departname
                               +ysxm+
                               "医生"+
                               shiftname+
                               preengageno+
                               "号当前已经排到第"+pdhm+"位，请您提早到门诊楼"+place+"做好准备，以免过号！";

                   }

                   AlipayMsgSendUtil.sendSingleTextMsg(openid,msgContent);
               }
         }

        return null;
    }

    @Override
    public String getProtocolCode() {
        return Content.SENT_LINE_MESSAGE_CODE;
    }
}
