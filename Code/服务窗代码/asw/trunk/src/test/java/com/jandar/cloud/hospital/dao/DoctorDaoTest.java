package com.jandar.cloud.hospital.dao;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import java.util.ArrayList;
import java.util.List;

/**
 * 医生集合的测试
 * Created by zzw on 16/9/22.
 */
@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations = "classpath:applicationContext.xml")
public class DoctorDaoTest {

    @Autowired
    private DoctorDao doctorDao;

    @Test
    public void findByDeptCode() throws Exception {

    }

    @Test
    public void findInfoByCodeList() throws Exception {
        List<String> codes = new ArrayList<>();
//        doctorDao.findInfoByCodeList(codes);
    }

}