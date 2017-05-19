package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.OutpatientOrderHistoryDefault;
import com.jandar.alipay.core.struct.OutpatientOrderHistory;
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
public class OutpatientOrderHistoryDefaultTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

//    @Test
//    public void testOutpatientOrderHistoryDefaultRequest() throws Exception {
//        String openid = "123456";
//        OutpatientOrderHistoryDefault outpatientOrderHistoryDefault = new OutpatientOrderHistoryDefault();
//        Map<String, Object> map = outpatientOrderHistoryDefault.outpatientOrderHistoryDefaultRequest(openid);
//        assertEquals("123456", map.get("openid"));
//
//    }

    @Test
    public void testOutpatientOrderHistoryDefaultResponse() throws Exception {
        String str = "preengagedate=1,preengagetime=2,departcode=3,departname=4,doctorcode=5,doctorname=5,patientname=6,"
                + "patientidcardno=8,patientphone=9,patientaddress=10,preengageno=11,place=12,fee=13";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        OutpatientOrderHistoryDefault outpatientOrderHistoryDefault = new OutpatientOrderHistoryDefault();
        List<OutpatientOrderHistory> results = outpatientOrderHistoryDefault.outpatientOrderHistoryDefaultResponse(list);
        OutpatientOrderHistory outpatientOrderHistory = results.get(0);
        assertEquals("1", outpatientOrderHistory.getPreengagedate());
        assertEquals("2", outpatientOrderHistory.getPreengagetime());
        assertEquals("3", outpatientOrderHistory.getDepartcode());
        assertEquals("4", outpatientOrderHistory.getDepartname());
        assertEquals("5", outpatientOrderHistory.getDoctorcode());
        assertEquals("5", outpatientOrderHistory.getDoctorname());
        assertEquals("6", outpatientOrderHistory.getPatientname());
        assertEquals("8", outpatientOrderHistory.getPatientidcardno());
        assertEquals("9", outpatientOrderHistory.getPatientphone());
        assertEquals("10", outpatientOrderHistory.getPatientaddress());
        assertEquals("11", outpatientOrderHistory.getPreengageno());
        assertEquals("12", outpatientOrderHistory.getPlace());
        assertEquals("13", outpatientOrderHistory.getFee());
    }
}