package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetInhospitalRechargeOrdersList;
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
 * Created by yubj on 2016/4/21.
 */
public class GetInhospitalRechargeOrdersListTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetInhospitalRechargeOrdersListRequest() throws Exception {
        String openid = "123456";
        GetInhospitalRechargeOrdersList getInhospitalRechargeOrdersList = new GetInhospitalRechargeOrdersList();
        Map<String, Object> map = getInhospitalRechargeOrdersList.getInhospitalRechargeOrdersListRequest(openid);
        assertEquals("123456", map.get("openid"));
    }

    @Test
    public void testGetInhospitalRechargeOrdersListResponse() throws Exception {
        String str = "paymenttradeno=1,tradeno=2,status=3,subject=4,money=5,ctime=6,paytime=7,rtntime=8,patientid=9,patientname=10," +
                "patientidcardno=11";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetInhospitalRechargeOrdersList getInhospitalRechargeOrdersList = new GetInhospitalRechargeOrdersList();
        List<RechargeOrderHistoryInfo> results = getInhospitalRechargeOrdersList.getInhospitalRechargeOrdersListResponse(list);
        RechargeOrderHistoryInfo rechargeOrderHistoryInfo = results.get(0);
        assertEquals("1", rechargeOrderHistoryInfo.getPaymenttradeno());
        assertEquals("2", rechargeOrderHistoryInfo.getTradeno());
        assertEquals("0", rechargeOrderHistoryInfo.getStatus());
        assertEquals("4", rechargeOrderHistoryInfo.getSubject());
        assertEquals("5", rechargeOrderHistoryInfo.getMoney());
        assertEquals("6", rechargeOrderHistoryInfo.getCtime());
        assertEquals("7", rechargeOrderHistoryInfo.getPaytime());
        assertEquals("8", rechargeOrderHistoryInfo.getRtntime());
        assertEquals("9", rechargeOrderHistoryInfo.getPatientid());
        assertEquals("10", rechargeOrderHistoryInfo.getPatientname());
        assertEquals("11", rechargeOrderHistoryInfo.getPatientidcardno());
    }
}