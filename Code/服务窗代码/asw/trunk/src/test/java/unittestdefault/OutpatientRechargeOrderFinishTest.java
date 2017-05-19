package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.OutpatientRechargeOrderFinish;
import com.jandar.alipay.core.struct.RechargeOrderFinishInfo;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;
import static org.junit.Assert.assertEquals;

/**
 * Created by yubj on 2016/4/20.
 */
public class OutpatientRechargeOrderFinishTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testOutpatientRechargeOrderFinishRequest() throws Exception {
        RechargeOrderFinishInfo info = new RechargeOrderFinishInfo();
        info.setMoney("1");
        info.setOpenid("2");
        info.setPatientid("3");
        info.setPatientname("4");
        info.setPaymentparameters("5");
        info.setPaymenttradeno("6");
        info.setTradeno("7");
        OutpatientRechargeOrderFinish outpatientRechargeOrderFinish = new OutpatientRechargeOrderFinish();
        Map<String, Object> map = outpatientRechargeOrderFinish.outpatientRechargeOrderFinishRequest(info);
        assertEquals("1", map.get("money"));
        assertEquals("2", map.get("openid"));
        assertEquals("3", map.get("patientid"));
        assertEquals("4", map.get("patientname"));
        assertEquals("5", map.get("paymentparameters"));
        assertEquals("6", map.get("paymenttradeno"));
        assertEquals("7", map.get("tradeno"));
    }

    @Test
    public void testOutpatientRechargeOrderFinish() throws Exception {
        OutpatientRechargeOrderFinish outpatientRechargeOrderFinish = new OutpatientRechargeOrderFinish();
        List<Map<String, String>> list = new ArrayList<>();
        String res2 = outpatientRechargeOrderFinish.outpatientRechargeOrderFinishResponse(list);
        assertEquals(null, res2);
        String str = "tradeno=1";
        Map<String, String> map = MapUtil.stringToMap(str);
        list.add(map);
        String res = outpatientRechargeOrderFinish.outpatientRechargeOrderFinishResponse(list);
        assertEquals("1", res);
    }
}