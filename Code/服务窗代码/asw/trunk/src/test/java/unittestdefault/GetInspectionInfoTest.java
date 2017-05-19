package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetInspectionInfo;
import com.jandar.alipay.core.struct.InspectionInfo;
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
 * Created by yubj on 2016/4/18.
 */
public class GetInspectionInfoTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetInspectionInfoRequest() throws Exception {
        String doctadviseno = "1234";
        GetInspectionInfo getInspectionInfo = new GetInspectionInfo();
        Map<String, Object> map = getInspectionInfo.getInspectionInfoRequest(doctadviseno);
        assertEquals("1234", map.get("doctadviseno"));
    }

    @Test
    public void testGetInspectionInfoResponse() throws Exception {
        String str = "doctadviseno=1,requesttime=2,requester=3,executetime=4,executer=5,receivetime=6,receiver=7" +
                ",stayhospitalmode=8,patientid=9,section=10,bedno=11,patientname=12,sex=13,age=14,diagnostic=15," +
                "sampletype=16,examinaim=17,requestmode=18,checker=19,checktime=20,csyq=21,profiletest=22";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);

        GetInspectionInfo getInspectionInfo = new GetInspectionInfo();
        InspectionInfo result = getInspectionInfo.getInspectionInfoResponse(list);
        assertEquals("1", result.getDoctadviseno());
        assertEquals("2", result.getRequesttime());
        assertEquals("3", result.getRequester());
        assertEquals("4", result.getExecutetime());
        assertEquals("5", result.getExecuter());
        assertEquals("6", result.getReceivetime());
        assertEquals("7", result.getReceiver());
        assertEquals("8", result.getStayhospitalmode());
        assertEquals("9", result.getPatientid());
        assertEquals("10", result.getSection());
        assertEquals("11", result.getBedno());
        assertEquals("12", result.getPatientname());
        assertEquals("13", result.getSex());
        assertEquals("14", result.getAge());
        assertEquals("15", result.getDiagnostic());
        assertEquals("16", result.getSampletype());
        assertEquals("17", result.getExaminaim());
        assertEquals("18", result.getRequestmode());
        assertEquals("19", result.getChecker());
        assertEquals("20", result.getChecktime());
        assertEquals("21", result.getCsyq());
        assertEquals("22", result.getProfiletest());
    }
}