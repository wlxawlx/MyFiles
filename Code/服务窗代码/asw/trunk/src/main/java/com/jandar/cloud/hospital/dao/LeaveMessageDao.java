package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.LeaveMessage;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 留言表
 * Created by lyx on 16/10/18.
 */
@Repository
public interface LeaveMessageDao extends MongoRepository<LeaveMessage, ObjectId> {
    /**
     * 根据流水号更改同步状态
     * @param code
     * @param syncStatus
     */
    public void updateSyncLeaveMessage(String code, String syncStatus);


    /**
     * 回复留言
     * @param code 检查单号
     * @param content 回复内容
     * */
    public void replyLeaveMessage(String code,String content);

    /**
     * 根据留言类型和留言所属流水号查看留言
     */
    public LeaveMessage findByCodeAndType(String code, String type);
}

