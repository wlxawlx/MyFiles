package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.LeaveMessage;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import java.util.Date;

/**
 * 留言自定义实现
 * Created by admin on 2016/9/28.
 */
public class LeaveMessageDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;

    /**
     * 根据流水号更改同步状态
     * @param code
     * @param syncStatus
     */
    public void updateSyncLeaveMessage(String code, String syncStatus) {

        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("SyncStatus", syncStatus), LeaveMessage.class);




    }


    /**
     * 回复留言
     * @param code 检查单号
     * @param content 回复内容
     * */
    public void replyLeaveMessage(String code,String content)
    {
        System.out.println("=========================================content:"+content);
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("BackContent", content).set("BackDate",new Date()).set("IsDeal","true"), LeaveMessage.class);
    }


}
