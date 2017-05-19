package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 科室表
 * Created by lyx on 2016/10/19.
 */
@Document(collection = "cloud_department")
public class Department {
    @Field("DeptCode")
    private String deptCode;
    @Field("DeptName")
    private String deptName;
    @Field("Illness")
    private List<Illness> illness;

    /**
     * 科室代码
     */
    public String getDeptCode() {
        return deptCode;
    }

    public void setDeptCode(String deptCode) {
        this.deptCode = deptCode;
    }

    /**
     * 科室名称
     */
    public String getDeptName() {
        return deptName;
    }

    public void setDeptName(String deptName) {
        this.deptName = deptName;
    }

    /**
     * 科室相关疾病
     */
    public List<Illness> getIllness() {
        return illness;
    }

    public void setIllness(List<Illness> illness) {
        this.illness = illness;
    }


    @Document
    public static class Illness {
        @Field("IllnessCode")
        private String illnessCode;
        @Field("IllnessName")
        private String illnessName;

        /**
         * 疾病代码
         */
        public String getIllnessCode() {
            return illnessCode;
        }

        public void setIllnessCode(String illnessCode) {
            this.illnessCode = illnessCode;
        }

        /**
         * 疾病名称
         */
        public String getIllnessName() {
            return illnessName;
        }

        public void setIllnessName(String illnessName) {
            this.illnessName = illnessName;
        }
    }

}
