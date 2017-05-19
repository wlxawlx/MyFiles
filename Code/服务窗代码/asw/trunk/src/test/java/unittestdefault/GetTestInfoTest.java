package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetTestInfo;
import com.jandar.alipay.core.struct.InspectionInfo;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/18.
 */
public class GetTestInfoTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetTestInfoRequest() throws Exception {
        String doctadviseno = "ad2561661";
        GetTestInfo getTestInfo = new GetTestInfo();
        Map<String, Object> params = getTestInfo.getTestInfoRequest(doctadviseno);
        System.out.println(params);
        assertEquals("ad2561661", params.get("doctadviseno"));

    }

    @Test
    public void testGetTestInfoResponse() throws Exception {
        String str = "doctadviseno=1,requesttime=2,requester=3,executetime=4,executer=5,receivetime=6," +
                "receiver=7,stayhospitalmode=8,patientid=9,section=10,bedno=11,patientname=12,sex=13," +
                "age=14,diagnostic=15,sampletype=16,examinaim=17,requestmode=18,checker=19,checktime=20,csyq=21,profiletest=22";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);

        GetTestInfo getTestInfo = new GetTestInfo();
        InspectionInfo results = getTestInfo.getTestInfoResponse(list);
        assertEquals("1", results.getDoctadviseno());
        assertEquals("2", results.getRequesttime());
        assertEquals("3", results.getRequester());
        assertEquals("4", results.getExecutetime());
        assertEquals("5", results.getExecuter());
        assertEquals("20", results.getReceivetime());
        assertEquals("7", results.getReceiver());
        assertEquals("8", results.getStayhospitalmode());
        assertEquals("9", results.getPatientid());
        assertEquals("10", results.getSection());
        assertEquals("11", results.getBedno());
        assertEquals("12", results.getPatientname());
        assertEquals("13", results.getSex());
        assertEquals("14", results.getAge());
        assertEquals("15", results.getDiagnostic());
        assertEquals("16", results.getSampletype());
        assertEquals("17", results.getExaminaim());
        assertEquals("18", results.getRequestmode());
        assertEquals("19", results.getChecker());
        assertEquals("20", results.getChecktime());
        assertEquals("21", results.getCsyq());
        assertEquals("22", results.getProfiletest());

    }
}