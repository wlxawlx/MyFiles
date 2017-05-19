package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.CancelOutpatientRechargeOrder;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/20.
 */
public class CancelOutpatientRechargeOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testCancelOutpatientRechargeOrderRequest() throws Exception {
        CancelOutpatientRechargeOrder cancelOutpatientRechargeOrder=new CancelOutpatientRechargeOrder();
        String openid="1234";
        String patientId="2";
        String patientName="xiaomi";
        String tradeno="222";
        Map<String,Object> map=cancelOutpatientRechargeOrder.cancelOutpatientRechargeOrderRequest(openid,patientName,patientId,tradeno);
        assertEquals("1234",map.get("openid"));
        assertEquals("2",map.get("patientid"));
        assertEquals("xiaomi",map.get("patientname"));
        assertEquals("222",map.get("tradeno"));
    }

    @Test
    public void testCancelOutpatientRechargeOrderResponse() throws Exception {
        CancelOutpatientRechargeOrder cancelOutpatientRechargeOrder=new CancelOutpatientRechargeOrder();
        List<Map<String,String>> list=new ArrayList<>();
        String res2=cancelOutpatientRechargeOrder.cancelOutpatientRechargeOrderResponse(list);
        assertEquals(null,res2);
        String str="tradeno=1";
        Map<String ,String > map= MapUtil.stringToMap(str);
        list.add(map);
        String res=cancelOutpatientRechargeOrder.cancelOutpatientRechargeOrderResponse(list);
        assertEquals("1",res);
    }
}