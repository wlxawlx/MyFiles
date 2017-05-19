package com.jandar.util;

import com.alipay.api.domain.AlipayOpenPublicMessageCustomSendModel;
import com.alipay.api.domain.AlipayOpenPublicMessageSingleSendModel;
import com.alipay.api.domain.Keyword;
import com.alipay.api.internal.util.json.JSONWriter;
import com.jandar.bean.AlipayImageTextMessageModel;
import com.jandar.bean.ArticleBean;
import com.jandar.bean.TemplateData;
import me.chanjar.weixin.mp.bean.WxMpTemplateMessage;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;

/**
 * 用于测试构建消息是否成功
 * Created by zzw on 16/9/10.
 */
public class MessageBuildUtilTest {
    @Test
    public void alipayImageTextMessage() throws Exception {

        List<ArticleBean> articles = new ArrayList<>();
        ArticleBean bean = new ArticleBean();
        bean.setUrl("setUrl");
        bean.setActionName("setActionName");
        bean.setDesc("setDesc");
        bean.setImageUrl("setImageUrl");
        bean.setTitle("setTitle");
        articles.add(bean);

        ArticleBean bean1 = new ArticleBean();
        bean1.setUrl("setUrl");
        bean1.setActionName("setActionName");
        bean1.setDesc("setDesc");
        bean1.setImageUrl("setImageUrl");
        bean1.setTitle("setTitle1111");
        articles.add(bean1);

        AlipayImageTextMessageModel model = MessageBuildUtil.alipayImageTextMessage("qwerq", articles);
        JSONWriter writer = new JSONWriter();
        String bizContent = writer.write(model, true);

        System.out.println(bizContent);
    }

    @Test
    public void alipayTemplateMessage() throws Exception {

        TemplateData data = new TemplateData();
        Keyword key = new Keyword();
        key.setValue("asdfadsfadsf");
        key.setColor("#00000");
        data.setFirst(key);
        data.setRemark(key);

        AlipayOpenPublicMessageSingleSendModel model = MessageBuildUtil.alipayTemplateMessage("asdf", "asdfa", "asdfasd", "asdfasd", "asdfasdf", data);
        JSONWriter writer = new JSONWriter();
        String bizContent = writer.write(model, true);

        System.out.println(bizContent);

    }

    @Test
    public void wechatTemplateMessage() throws Exception {

        TemplateData data = new TemplateData();
        Keyword key = new Keyword();
        key.setValue("asdfadsfadsf");
        key.setColor("#00000");
        data.setFirst(key);
        data.setRemark(key);

        WxMpTemplateMessage wxMpTemplateMessage = MessageBuildUtil.wechatTemplateMessage("asdf", "asdfa", "asdfasd", "asdfasd", data);
        String bizContent = wxMpTemplateMessage.toJson();

        System.out.println(bizContent);

    }

}