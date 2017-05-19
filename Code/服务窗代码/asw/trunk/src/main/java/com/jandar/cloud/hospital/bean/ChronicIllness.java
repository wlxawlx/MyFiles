package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.context.annotation.DependsOn;
import org.springframework.data.annotation.Id;
import org.springframework.data.annotation.Transient;
import org.springframework.data.mongodb.core.index.Indexed;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 云医院慢性病列表
 * Created by zzw on 16/8/30.
 */
@Document(collection = "cloud_chronic_illness")
public class ChronicIllness {


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
     * 疾病列表
     */
    public List<Illness> getIllness() {
        return illness;
    }

    public void setIllness(List<Illness> illness) {
        this.illness = illness;
    }

    @Document
    public static class Illness {

        @Field("Code")
        private String code;

        @Field("Name")
        private String name;

        /**
         * 疾病代码
         */
        public String getCode() {
            return code;
        }

        public void setCode(String code) {
            this.code = code;
        }

        /**
         * 疾病名称
         */
        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }
    }
}
