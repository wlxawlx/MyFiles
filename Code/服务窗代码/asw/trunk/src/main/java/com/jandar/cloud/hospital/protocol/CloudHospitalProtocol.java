package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.mongodb.BasicDBList;
import com.mongodb.BasicDBObject;
import com.mongodb.DBObject;
import org.springframework.data.mongodb.core.MongoTemplate;

import javax.annotation.Resource;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 云医院协议处理基类
 * Created by zzw on 16/8/30.
 */
public abstract class CloudHospitalProtocol implements Protocol {

    @Resource
    MongoTemplate mongoTemplate;

    /**
     * 协议号
     *
     * @return 返回协议号
     */
    public abstract String getProtocolCode();

    /**
     * 要求子类实现
     *
     * @param pcode  协议号
     * @param params 协议内容
     * @return 协议返回内容
     * @throws HospitalException
     */
    public abstract Object doProcess(String pcode, Map<String, Object> params) throws HospitalException;

    /**
     * 协议要求用户已经登录，可被子类覆盖
     *
     * @return 是否已经登录
     */
    protected boolean needUserLogin() {
        return false;
    }

    /**
     * 协议要求用户事先已经绑卡，可被子类覆盖
     *
     * @return 是否已经绑卡
     */
    protected boolean needBindCard() {
        return false;
    }

    @Override
    public Object process(String pcode, Map<String, Object> params) throws HospitalException {

        if (needUserLogin()) {
            UserInfo userInfo = ServiceContext.getHospitalUserInfo();
            if (userInfo == null) {
                throw new HospitalException("还未建档", HospitalException.UNARCHIV);
            }
        }

        if (needBindCard()) {
            // TODO
        }

        Object result = doProcess(pcode, params);

        try {
            if (result instanceof List) {
                BasicDBList list = new BasicDBList();
                mongoTemplate.getConverter().write(result, list);
                return list;
            } else {
                DBObject dbDoc = new BasicDBObject();
                mongoTemplate.getConverter().write(result, dbDoc);
                return dbDoc;
            }
        } catch (Exception ex) {
            return result;
        }
    }
}
