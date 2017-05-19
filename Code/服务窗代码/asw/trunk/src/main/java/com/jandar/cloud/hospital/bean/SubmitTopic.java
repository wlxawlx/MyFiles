package com.jandar.cloud.hospital.bean;

import org.bson.types.ObjectId;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;
import java.util.List;

/**
 * 提交问卷答案
 * Created by lyx on 2016/9/21.
 */

@Document(collection = "cloud_submit_topic")
public class SubmitTopic {
    @Field("_id")
    private ObjectId id;
    @Field("Code")
    private String code;
    @Field("Tile")
    private String tile;
    @Field("Patient")
    private Patients patient;
    @Field("DeptCode")
    private String deptCode;
    @Field("DeptName")
    private String deptName;
    @Field("IllnessCode")
    private String illnessCode;
    @Field("IllnessName")
    private String illnessName;
    @Field("Date")
    private Date date;
    @Field("Items")
    private List<Items> items;

    public SubmitTopic() {
    }

    /**
     * 近况回答ID
     */
    public ObjectId getId() {
        return id;
    }

    public void setId(ObjectId id) {
        this.id = id;
    }
    /**
     * 问答卷代码
     */
    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    /**
     * 问答卷名称
     */
    public String getTile() {
        return tile;
    }

    public void setTile(String tile) {
        this.tile = tile;
    }

    /**
     * 病人信息
     */
    public Patients getPatient() {
        return patient;
    }

    public void setPatient(Patients patient) {
        this.patient = patient;
    }

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

    /**
     * 答卷时间
     */
    public java.util.Date getDate() {
        return date;
    }

    public void setDate(java.util.Date date) {
        this.date = date;
    }

    /**
     * 问答题列表
     */
    public List<SubmitTopic.Items> getItems() {
        return items;
    }

    public void setItems(List<SubmitTopic.Items> items) {
        this.items = items;
    }

    @Document
    public static class Items {
        @Field("Code")
        private String code;
        @Field("Answer")
        private List<String> answer;
        @Field("otherAnswer")
        private String otherAnswer;

        /**
         * 代码
         */
        public String getCode() {
            return code;
        }

        public void setCode(String code) {
            this.code = code;
        }

        /**
         * 选择答案
         */
        public List<String> getAnswer() {
            return answer;
        }

        public void setAnswer(List<String> answer) {
            this.answer = answer;
        }

        /**
         *
         * 其它问题
         */
        public String getOtherAnswer() {
            return otherAnswer;
        }

        public void setOtherAnswer(String otherAnswer) {
            this.otherAnswer = otherAnswer;
        }
    }

    @Document
    public static class Patients {
        @Field("UserId")
        private String userId;
        @Field("Name")
        private String name;

        /**
         * 用户id
         */
        public String getUserId() {
            return userId;
        }

        public void setUserId(String userId) {
            this.userId = userId;
        }

        /**
         * 用户姓名
         */
        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }


    }


}
