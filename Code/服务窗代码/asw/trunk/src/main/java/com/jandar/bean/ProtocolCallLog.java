package com.jandar.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Map;

/**
 * 协议调用记录
 * Created by zzw on 16/8/30.
 */
@Document(collection = "call_protocol_log")
public class ProtocolCallLog {

    @Id
    public ObjectId id;

    @Field("code")
    public String pcode;

    @Field("content")
    public Map<String, Object> content;
}
