package com.jandar.cloud.hospital.handle;

import com.jandar.alipay.core.struct.PlatformType;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import static org.junit.Assert.*;

/**
 * 支付完成测试
 * Created by zzw on 2016/9/30.
 */
@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations = "classpath:applicationContext.xml")
public class NotifyHandleTest {

    @Test
    public void handlePayNotify() throws Exception {
        boolean success = NotifyHandle.handlePayNotify("ZYD201610060011222", "0001", "252", "201610061415", PlatformType.Alipay);
        assertEquals(success, true);
    }

    @Test
    public void sendPayNotifyToUser() throws Exception {
        NotifyHandle.sendPayNotifyToUser("", "", "", "", PlatformType.Alipay);
    }

}