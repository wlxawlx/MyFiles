package unittestdefault;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;
import unittestdefault.impl.HIServiceHandlerDefaultUnit;

/**
 * Created by yubj on 2016/4/25.
 */
public class HIServiceProtocolDefaultUnitTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testUserRegister() throws Exception {
        HISUserInfo userInfo1 = null;
        HIServiceHandlerDefaultUnit handler=new HIServiceHandlerDefaultUnit();
        try{
            boolean returned = handler.userRegister(userInfo1);
        }catch (HospitalException e){
            assertEquals("信息参数不完整",e.getMessage());
        }
        HISUserInfo userInfo = new HISUserInfo();
        userInfo.setName("俞斌");
        userInfo.setPhone("18006851201");
        userInfo.setIdcardno("330681199211090033");
        userInfo.setAddress("");
        userInfo.setOpenid("123456");
        userInfo.setHeadurl("123");
        userInfo.setUsertype(PlatformType.Alipay);
        try {
            boolean returned = handler.userRegister(userInfo);
            System.out.println(returned);
            assertEquals("",returned);
        } catch (HospitalException e) {
            if(e.getCode().contains("手机号码输入有误,请重新输入")){
                assertEquals("99",e.getMessage());
                e.printStackTrace();
            }
            if (e.getCode().contains("用户类型不能为空")) {
                assertEquals("01", e.getMessage());
                e.printStackTrace();
            }
            if (e.getMessage().contains("网络错误，请联系系统管理人员")) {

            }
        }
    }
}