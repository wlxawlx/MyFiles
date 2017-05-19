package com.jandar.cloud.hospital.bean;

import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.List;

/**
 * 慢性病近况问题
 * 近况问题没有和 ChronicIllness 表放一起,使用一个独立的表,是因为一套题目可能对应多种疾病
 * Created by lyx on 16/9/18.
 */
@Document(collection = "cloud_chronic_illness_topic")
public class ChronicIllnessTopic {

    @Field("Code")
    private String code;
    @Field("Tile")
    private String tile;

    @Field("Desc")
    private String desc;

    @Field("Illness")
    private List<illness> illness;

    @Field("HospitalCode")
    private String hospitalCode;

    @Field("HospitalName")
    private String hospitalName;

    @Field("Dept")
    private List<dept> dept;

    @Field("Items")
    private List<Items> items;

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
     * 问答卷标题
     */
    public String getTile() {
        return tile;
    }

    public void setTile(String tile) {
        this.tile = tile;
    }

    /**
     * 问答卷描述
     */
    public String getDesc() {
        return desc;
    }

    public void setDesc(String desc) {
        this.desc = desc;
    }

    public String getHospitalCode() {
        return hospitalCode;
    }

    public void setHospitalCode(String hospitalCode) {
        this.hospitalCode = hospitalCode;
    }

    public String getHospitalName() {
        return hospitalName;
    }

    public void setHospitalName(String hospitalName) {
        this.hospitalName = hospitalName;
    }

    /**
     * 疾病列表
     */
    public List<dept> getdept() {
        return dept;
    }

    public void setdept(List<dept> dept) {
        this.dept = dept;
    }

    /**
     * 科室列表
     */

    public List<illness> getIllness() {
        return illness;
    }

    public void setIllness(List<illness> Illness) {
        this.illness = illness;
    }

    /**
     * 问答题列表
     */

    public List<ChronicIllnessTopic.Items> getItems() {
        return items;
    }

    public void setItems(List<ChronicIllnessTopic.Items> items) {
        items = items;
    }

    @Document
    public static class dept {

        @Field("Code")
        private String code;

        @Field("Name")
        private String name;

        /**
         * 科室代码
         */
        public String getCode() {
            return code;
        }

        public void setCode(String code) {
            this.code = code;
        }

        /**
         * 科室名称
         */
        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }
    }


    @Document
    public static class illness {

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


    @Document
    public static class Items {
        @Field("Code")
        private String code;
        @Field("Tile")
        private String tile;
        @Field("Desc")
        private String desc;
        @Field("Type")
        private String type;

        @Field("Answer")
        private List<String> answer;


        @Field("otherAnswer")
        private String otherAnswer;


        @Field("PhotoDesc")
        private List<String> photoDesc;


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
         * 标题
         */
        public String getTile() {
            return tile;
        }

        public void setTile(String tile) {
            this.tile = tile;
        }

        /**
         * 描述
         */
        public String getDesc() {
            return desc;
        }

        public void setDesc(String desc) {
            this.desc = desc;
        }

        /**
         * 类型
         */
        public String getType() {
            return type;
        }

        public void setType(String type) {
            this.type = type;
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
         * 照片描述信息
         */
        public List<String> getPhotoDesc() {
            return photoDesc;
        }

        public void setPhotoDesc(List<String> photoDesc) {
            this.photoDesc = photoDesc;
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
}
