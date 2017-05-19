package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.AlterUserInfo;
import com.jandar.alipay.core.impl.servicedefault.UserRegister;
import com.jandar.alipay.core.struct.PlatformType;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/8.
 */
public class AlterUserInfoTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testAlterUserInfoRequest() throws Exception {
        String openid = "123455";
        String name = "小米";
        String phone = "15006851271";
        String idcardno = "330681199311090022";
        AlterUserInfo alterUserInfo = new AlterUserInfo();
        Map<String, Object> result = alterUserInfo.alterUserInfoRequest(openid, name, phone, idcardno);
        System.out.println(result);
        assertEquals("123455", result.get("openid"));
        assertEquals("小米", result.get("name"));
        assertEquals("15006851271", result.get("phone"));
        assertEquals("330681199311090022", result.get("idcardno"));
    }

    @Test
    public void testAlterUserInfoResponse() throws Exception {
        List<Map<String, String>> value = new ArrayList<>();
        AlterUserInfo alterUserInfo = new AlterUserInfo();
        boolean result = alterUserInfo.alterUserInfoResponse(value);
        System.out.println(result);
        assertEquals(false, result);

        Map<String, String> map = new HashMap<>();
        map.put("openid", "123455");
        value.add(map);
        boolean com = alterUserInfo.alterUserInfoResponse(value);
        System.out.println(com);
        assertEquals(true, com);
    }
}