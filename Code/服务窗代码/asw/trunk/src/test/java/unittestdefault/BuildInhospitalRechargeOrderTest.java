package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.BuildInhospitalRechargeOrder;
import com.jandar.alipay.hospital.UserInfo;
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
public class BuildInhospitalRechargeOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testBuildInhospitalRechargeOrderRequest() throws Exception {
        BuildInhospitalRechargeOrder buildInhospitalRechargeOrder = new BuildInhospitalRechargeOrder();
        String inpatientno = "1";
        String money = "9";
        String openid="2";
        String patientidcardno = "3";
        String patientname = "4";
        String subject = "5";
        Map<String, Object> map = new HashMap<>();
            map = buildInhospitalRechargeOrder.buildInhospitalRechargeOrderRequest(inpatientno, money,openid, subject, patientname, patientidcardno);
            assertEquals("1", map.get("inpatientno"));
            assertEquals("9", map.get("money"));
            assertEquals("2", map.get("openid"));
            assertEquals("2", map.get("openid"));
            assertEquals("3", map.get("patientidcardno"));
            assertEquals("4", map.get("patientname"));
            assertEquals("5", map.get("subject"));


    }

    @Test
    public void testBuildInhospitalRechargeOrderResponse() throws Exception {
        BuildInhospitalRechargeOrder buildInhospitalRechargeOrder = new BuildInhospitalRechargeOrder();
        List<Map<String, String>> list = new ArrayList<>();
        String res2 = buildInhospitalRechargeOrder.buildInhospitalRechargeOrderResponse(list);
        assertEquals(null, res2);
        String str = "tradeno=1";
        Map<String, String> map = MapUtil.stringToMap(str);
        list.add(map);
        String res = buildInhospitalRechargeOrder.buildInhospitalRechargeOrderResponse(list);
        assertEquals("1", res);
    }
}