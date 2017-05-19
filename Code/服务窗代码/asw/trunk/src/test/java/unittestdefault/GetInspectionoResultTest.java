package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetInspectionoResult;
import com.jandar.alipay.core.struct.InspectionoResult;
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
public class GetInspectionoResultTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetInspectionoResultRequest() throws Exception {
        String doctadviseno = "123";
        GetInspectionoResult getInspectionoResult = new GetInspectionoResult();
        Map<String, Object> map = getInspectionoResult.getInspectionoResultRequest(doctadviseno);
        assertEquals("123", map.get("doctadviseno"));
    }

    @Test
    public void testGetInspectionoResultResponse() throws Exception {
        String str = "studyresult=1,diagresult=2";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);

        GetInspectionoResult getInspectionoResult = new GetInspectionoResult();
        InspectionoResult inspectionoResult = getInspectionoResult.getInspectionoResultResponse(list);
        assertEquals("1", inspectionoResult.getStudyresult());
        assertEquals("2", inspectionoResult.getDiagresult());
    }
}