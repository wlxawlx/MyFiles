package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.Department;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 科室表
 * Created by lyx on 16/10/19.
 */
@Repository
public interface DepartmentDao extends MongoRepository<Department, ObjectId> {
    public String returnBackDeptName(String deptCode);
}

