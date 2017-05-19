package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetUserInfo;
import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.assertEquals;

/**
 * Created by yubj on 2016/4/8.
 */
public class GetUserInfoTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetUserInfoRequest() throws Exception {
        String openid = "123456";
        PlatformType usertype = PlatformType.Alipay;
        GetUserInfo getuserinfo = new GetUserInfo();
        Map<String, Object> parameters = getuserinfo.getUserInfoRequest(openid, usertype);
        assertEquals("123456",parameters.get("openid"));
        assertEquals("1",parameters.get("usertype"));

    }


    @Test
    public void testGetUserInfoResponse() throws Exception {
        List<Map<String, String>> process=new ArrayList<>();
        Map<String,String > map=new HashMap<>();
        map.put("name","小米");
        map.put("patientid","12456");
        map.put("phone","88888888");
        map.put("idcardno","3330621564115622");
        map.put("address","");
        map.put("openid","852963741");
        map.put("cardno","11111111");
        map.put("headurl","afdasf");
        process.add(map);
        GetUserInfo getuserinfo = new GetUserInfo();
        HISUserInfo info=getuserinfo.getUserInfoResponse(process);
        assertEquals("小米",info.getName());
        assertEquals("12456",info.getPatientid());
        assertEquals("88888888",info.getPhone());
        assertEquals("3330621564115622",info.getIdcardno());
        assertEquals("",info.getAddress());
        assertEquals("852963741",info.getOpenid());
        assertEquals("11111111",info.getCardno());
        assertEquals("afdasf",info.getHeadurl());
    }
}