package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Department;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;

import java.util.List;

/**
 * Created by admin on 2016/10/20.
 */
public class DepartmentDaoImpl {

    @Autowired
    private MongoTemplate mongoTemplate;
    /**
     * 根据DeptCode返回科室名称
     *
     * @param deptCode
     * @return
     */
    public String returnBackDeptName(String deptCode) {
        Query query = new Query(Criteria.where("DeptCode").in(deptCode));
        query.fields().include("DeptName");
        Department department = mongoTemplate.findOne(query, Department.class);
        if(department == null)
            return  null;
        return department.getDeptName();
    }
}
