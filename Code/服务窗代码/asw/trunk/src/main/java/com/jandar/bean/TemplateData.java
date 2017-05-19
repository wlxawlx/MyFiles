package com.jandar.bean;

import com.alipay.api.AlipayObject;
import com.alipay.api.domain.Keyword;
import com.alipay.api.internal.mapping.ApiField;

/**
 * 扩展 alipay 的模板消息内容
 * Created by zzw on 16/9/10.
 */
public class TemplateData extends AlipayObject {

    @ApiField("first")
    private Keyword first;

    @ApiField("remark")
    private Keyword remark;

    @ApiField("keyword1")
    private Keyword keyword1;

    @ApiField("keyword2")
    private Keyword keyword2;

    @ApiField("keyword3")
    private Keyword keyword3;

    @ApiField("keyword4")
    private Keyword keyword4;

    @ApiField("keyword5")
    private Keyword keyword5;

    @ApiField("keyword6")
    private Keyword keyword6;

    @ApiField("keyword7")
    private Keyword keyword7;

    public Keyword getFirst() {
        return first;
    }

    public void setFirst(Keyword first) {
        this.first = first;
    }

    public Keyword getRemark() {
        return remark;
    }

    public void setRemark(Keyword remark) {
        this.remark = remark;
    }

    public Keyword getKeyword1() {
        return keyword1;
    }

    public void setKeyword1(Keyword keyword1) {
        this.keyword1 = keyword1;
    }

    public Keyword getKeyword2() {
        return keyword2;
    }

    public void setKeyword2(Keyword keyword2) {
        this.keyword2 = keyword2;
    }

    public Keyword getKeyword3() {
        return keyword3;
    }

    public void setKeyword3(Keyword keyword3) {
        this.keyword3 = keyword3;
    }

    public Keyword getKeyword4() {
        return keyword4;
    }

    public void setKeyword4(Keyword keyword4) {
        this.keyword4 = keyword4;
    }

    public Keyword getKeyword5() {
        return keyword5;
    }

    public void setKeyword5(Keyword keyword5) {
        this.keyword5 = keyword5;
    }

    public Keyword getKeyword6() {
        return keyword6;
    }

    public void setKeyword6(Keyword keyword6) {
        this.keyword6 = keyword6;
    }

    public Keyword getKeyword7() {
        return keyword7;
    }

    public void setKeyword7(Keyword keyword7) {
        this.keyword7 = keyword7;
    }
}
