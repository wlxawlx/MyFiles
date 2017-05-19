package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetOutpatientRechargeOrdersList;
import com.jandar.alipay.core.struct.RechargeOrderHistoryInfo;
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
public class GetOutpatientRechargeOrdersListTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetOutpatientRechargeOrdersListRequest() throws Exception {
        GetOutpatientRechargeOrdersList getOutpatientRechargeOrdersList = new GetOutpatientRechargeOrdersList();
        String openid = "123456";
        Map<String, Object> map = getOutpatientRechargeOrdersList.getOutpatientRechargeOrdersListRequest(openid);
        assertEquals("123456", map.get("openid"));
    }

    @Test
    public void testGetOutpatientRechargeOrdersListResponse() throws Exception {
        GetOutpatientRechargeOrdersList getOutpatientRechargeOrdersList = new GetOutpatientRechargeOrdersList();
        String str = "openid=123,paymenttradeno=2,tradeno=3,status=2,subject=6,money=100,ctime=1992,paytime=1990," +
                "rtntime=9,patientid=10,patientname=15,patientidcardno=50";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        List<RechargeOrderHistoryInfo> result = getOutpatientRechargeOrdersList.getOutpatientRechargeOrdersListResponse(list);
        RechargeOrderHistoryInfo res = result.get(0);
        assertEquals("123", res.getOpenid());
        assertEquals("2", res.getPaymenttradeno());
        assertEquals("3", res.getTradeno());
        assertEquals("1", res.getStatus());
        assertEquals("6", res.getSubject());
        assertEquals("100", res.getMoney());
        assertEquals("1992", res.getCtime());
        assertEquals("1990", res.getPaytime());
        assertEquals("9", res.getRtntime());
        assertEquals("10", res.getPatientid());
        assertEquals("15", res.getPatientname());
        assertEquals("50", res.getPatientidcardno());

    }
}