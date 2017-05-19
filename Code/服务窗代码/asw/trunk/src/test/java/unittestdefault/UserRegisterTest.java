package unittestdefault;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.impl.servicedefault.UserRegister;
import com.jandar.alipay.core.struct.HISUserInfo;
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
public class UserRegisterTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testUserRegisterRequest() throws Exception {
        HISUserInfo info1=null;
        UserRegister userRegister=new UserRegister();
        try{
            if(info1==null){
                userRegister.userRegisterRequest(info1);
            }
        }catch (HospitalException ex){
            assertEquals("com.jandar.alipay.core.HospitalException: 信息参数不完整",ex.toString());
        }
        HISUserInfo info=new HISUserInfo();
        info.setName("小米");
        info.setPhone("18006811930");
        info.setIdcardno("330681199411060031");
        info.setAddress("");
        info.setUsertype(PlatformType.Alipay);
        info.setBkzt("0");
        Map<String, Object> result = userRegister.userRegisterRequest(info);
        assertEquals("小米", result.get("name"));
        assertEquals("18006811930",result.get("phone"));
        assertEquals("330681199411060031",result.get("idcardno"));
        assertEquals("1",result.get("usertype"));
        assertEquals("",result.get("address"));
//        assertEquals("null",result.get("openid"));
//        assertEquals("",result.get("headurl"));
//        assertEquals("",result.get());
        assertEquals("0",result.get("bkzt"));
    }

    @Test
    public void testUserRegisterResponse(){
        List<Map<String, String>> value=new ArrayList<>();
        UserRegister userRegister=new UserRegister();
        boolean result=userRegister.userRegisterResponse(value);
        assertEquals(true,result);
        Map<String,String> map=new HashMap<>();
        map.put("name","小米");
        value.add(map);
        boolean com=userRegister.userRegisterResponse(value);
        assertEquals(true,com);
    }
}