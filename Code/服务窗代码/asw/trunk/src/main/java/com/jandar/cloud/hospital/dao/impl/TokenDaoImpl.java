package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.Token;
import com.jandar.util.DateUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import java.util.UUID;

/**
 * Created by flyhorse on 2016/11/8.
 */
public class TokenDaoImpl {
    @Autowired
    private MongoTemplate mongoTemplate;

    /**
     * 根据doctorId获取token
     * 1.查询数据库 ，如果有，返回并刷新
     * 2. 没有 新增后 返回
     * */
    public String getTokenByDoctorId(String doctorId,String userName,String name,String userType)
    {
        Query query = new Query(Criteria.where("user_id").in(doctorId));
        System.out.println("TokenDaoImpl:getTokenByDoctorId:doctorId::"+doctorId);
        Token token=mongoTemplate.findOne(query, Token.class);
        System.out.println("TokenDaoImpl:getTokenByDoctorId:token::"+token);
        int mill=(int) (System.currentTimeMillis() / 1000L);
        String currentTime= DateUtil.getCurrentDate("yyyy-MM-dd HH:mm:ss");
       if(token==null)//不存在
       {
           System.out.println("TokenDaoImpl:getTokenByDoctorId:token:null");
           token=new Token();
           String tokenStr = UUID.randomUUID().toString();
           token.setToken(tokenStr);

           token.setUserId(doctorId);
           token.setUserName(userName);
           token.setName(name);
           token.setUserType(userType);

           token.setCreateDate(currentTime);
           token.setCreateMill(mill);
           token.setRefreshMill(mill);

           mongoTemplate.save(token);
       }
       else  //如果有，则刷新
       {
           System.out.println("TokenDaoImpl:getTokenByDoctorId:token:notnull");
           Update update=new Update().set("name",name).set("refresh_mill",mill).set("refresh_date",currentTime);
           mongoTemplate.updateFirst(query,update,Token.class);

       }


        return token.getToken();
    }
}
