package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.OutpatientOrderDefault;
import com.jandar.alipay.core.struct.OutpatientOrderReponse;
import com.jandar.alipay.core.struct.OutpatientOrderRequest;
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
public class OutpatientOrderDefaultTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testOutpatientOrderRequest() throws Exception {
        OutpatientOrderRequest outpatientOrderRequest = new OutpatientOrderRequest();
        outpatientOrderRequest.setDoctorcode("20");
        outpatientOrderRequest.setOpenid("35407");
        outpatientOrderRequest.setOrderno("18");
        outpatientOrderRequest.setOrdertime("09:23");
        outpatientOrderRequest.setOrderendtime("09:53");
        outpatientOrderRequest.setShiftcode("S");
        outpatientOrderRequest.setPatientid("1231");
        outpatientOrderRequest.setPatientaddress("DDDD");
        outpatientOrderRequest.setPatientidcardno("330681199211090033");
        outpatientOrderRequest.setPatientname("俞斌杰");
        outpatientOrderRequest.setPatientphone("13757750398");
        outpatientOrderRequest.setScheduleseq("1000050378");
        OutpatientOrderDefault outpatientOrderDefault = new OutpatientOrderDefault();
        Map<String, Object> map = outpatientOrderDefault.outpatientOrderDefaultRequest(outpatientOrderRequest);
        System.out.println(map);
        assertEquals("20", map.get("doctorcode"));
        assertEquals("35407", map.get("openid"));
        assertEquals("18", map.get("orderseq"));
        assertEquals("09:23", map.get("ordertime"));
        assertEquals("09:53", map.get("orderendtime"));
        assertEquals("S", map.get("shiftcode"));
        assertEquals("1231", map.get("patientid"));
        assertEquals("DDDD", map.get("patientaddress"));
        assertEquals("330681199211090033", map.get("patientidcardno"));
        assertEquals("俞斌杰", map.get("patientname"));
        assertEquals("13757750398", map.get("patientphone"));
        assertEquals("1000050378", map.get("scheduleseq"));

    }

    @Test
    public void testOutpatientOrderResponse() throws Exception {
        String str = "departcode=1,departname=2,doctorcode=3,doctorname=4,patientname=5,place=6,preengagedate=7," +
                "preengageno=8,preengageseq=9,preengagetime=10";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        OutpatientOrderDefault outpatientOrderDefault = new OutpatientOrderDefault();
        OutpatientOrderReponse result = outpatientOrderDefault.outpatientOrderDefaultResponse(list);
        if (result != null) {
            assertEquals("1", result.getDepartcode());
            assertEquals("2", result.getDepartname());
            assertEquals("3", result.getDoctorcode());
            assertEquals("4", result.getDoctorname());
            assertEquals("5", result.getPatientname());
            assertEquals("6", result.getPlace());
            assertEquals("7", result.getPreengagedate());
            assertEquals("8", result.getPreengageno());
            assertEquals("9", result.getPreengageseq());
            assertEquals("10", result.getPreengagetime());
        } else {
            assertEquals(null, result);
        }
    }
}