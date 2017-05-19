package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.BuildOutpatientRechargeOrder;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;
import static org.junit.Assert.assertEquals;

/**
 * Created by yubj on 2016/4/20.
 */
public class BuildOutpatientRechargeOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testBuildOutpatientRechargeOrderRequest() throws Exception {
        String cardno="1";
        String money="2";
        String openid="3";
        String patientid="4";
        String patientidcardno="5";
        String patientname="6";
        String subject="7";
        BuildOutpatientRechargeOrder buildOutpatientRechargeOrder=new BuildOutpatientRechargeOrder();
        Map<String,Object> map=buildOutpatientRechargeOrder.buildOutpatientRechargeOrderRequest(openid,patientname,patientidcardno,cardno,patientid,subject,money);
        System.out.println(map);
        assertEquals("1",map.get("cardno"));
        assertEquals("2",map.get("money"));
        assertEquals("3",map.get("openid"));
        assertEquals("4",map.get("patientid"));
        assertEquals("5",map.get("patientidcardno"));
        assertEquals("6",map.get("patientname"));
        assertEquals("7",map.get("subject"));
    }

    @Test
    public void testBuildOutpatientRechargeOrderResponse() throws Exception {
        String str="tradeno=1";
        Map<String ,String > map1= MapUtil.stringToMap(str);
        Map<String,String> map2=new HashMap<>();
        List<Map<String,String>> list1=new ArrayList<>();
        List<Map<String,String>> list2=new ArrayList<>();
        list1.add(map1);
        list2.add(map2);
        BuildOutpatientRechargeOrder buildOutpatientRechargeOrder=new BuildOutpatientRechargeOrder();
        String result=buildOutpatientRechargeOrder.buildOutpatientRechargeOrderResponse(list1);
        String result2=buildOutpatientRechargeOrder.buildOutpatientRechargeOrderResponse(list2);
        assertEquals("1",result);
        assertEquals(null,result2);
    }
}