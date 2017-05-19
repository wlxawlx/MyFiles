package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.CloudOrder;
import com.jandar.cloud.hospital.dao.CloudOrderDao;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;

import javax.annotation.Resource;

/**
 * Created by Administrator on 2016/11/10.
 */
public class CloudOrderDaoImpl {
    @Resource
    private CloudOrderDao cloudOrderDao;

    /**
     * 新增订单信息
     * @param cloudOrder
     */
    public void add(CloudOrder cloudOrder){
        cloudOrderDao.insert(cloudOrder);
    }

}
