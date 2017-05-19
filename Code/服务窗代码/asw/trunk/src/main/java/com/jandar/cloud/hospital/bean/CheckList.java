package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.io.Serializable;

/**
 * 云医院预约的检查单信息,包含基本信息,包含状态
 * Created by zzw on 16/8/30.
 */
@Document(collection = "cloud_check_list")
public class CheckList {

    @Id
    private ObjectId id;
}
