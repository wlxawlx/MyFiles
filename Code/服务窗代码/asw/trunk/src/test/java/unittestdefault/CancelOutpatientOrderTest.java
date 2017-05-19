package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.CancelOutpatientOrder;
import com.jandar.alipay.core.struct.OutpatientOrder;
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
 * Created by yubj on 2016/4/15.
 */
public class CancelOutpatientOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testCancelOutpatientOrderRequest() throws Exception {
        String openid = "123456";
        String preengageseq = "2016";
        CancelOutpatientOrder cancelOutpatientOrder = new CancelOutpatientOrder();
        Map<String, Object> map = cancelOutpatientOrder.cancelOutpatientOrderRequest(openid, preengageseq);
        System.out.println(map);
        assertEquals("123456", map.get("openid"));
        assertEquals("2016", map.get("preengageseq"));
    }

    @Test
    public void testCancelOutpatientOrderResponse() throws Exception {
        String str = "preengagedate=1,preengagetime=2,departname=3,doctorname=4,patientname=5,preengageno=6,place=7";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        CancelOutpatientOrder cancelOutpatientOrder = new CancelOutpatientOrder();
        List<OutpatientOrder> results = cancelOutpatientOrder.cancelOutpatientOrderResponse(list);
        OutpatientOrder outpatientOrder = results.get(0);
        assertEquals("1", outpatientOrder.getPreengagedate());
        assertEquals("2", outpatientOrder.getPreengagetime());
        assertEquals("3", outpatientOrder.getDepartname());
        assertEquals("4", outpatientOrder.getDoctorname());
        assertEquals("5", outpatientOrder.getPatientname());
        assertEquals("6", outpatientOrder.getPreengageno());
        assertEquals("7", outpatientOrder.getPlace());

    }
}