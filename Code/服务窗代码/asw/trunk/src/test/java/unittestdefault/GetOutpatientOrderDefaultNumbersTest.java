package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetOutpatientOrderNumbers;
import com.jandar.alipay.core.struct.OutpatientOrderNumber;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/15.
 */
public class GetOutpatientOrderDefaultNumbersTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetOutpatientOrderNumbersRequest() throws Exception {
        String doctorcode = "123";
        String scheduleseq = "2016";
        String shiftcode = "888";
        GetOutpatientOrderNumbers getOutpatientOrderNumbers = new GetOutpatientOrderNumbers();
        Map<String, Object> map = getOutpatientOrderNumbers.getOutpatientOrderNumbersRequest(doctorcode, scheduleseq, shiftcode);
        System.out.println(map);
        assertEquals("123", map.get("doctorcode"));
        assertEquals("2016", map.get("scheduleseq"));
        assertEquals("888", map.get("shiftcode"));
    }

    @Test
    public void testGetOutpatientOrderNumbersResponse() throws Exception {
        String str = "orderno=1,orderseq=21,ordertime=2015,shiftcode=123,orderendtime=2016";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetOutpatientOrderNumbers getOutpatientOrderNumbers = new GetOutpatientOrderNumbers();
        List<OutpatientOrderNumber> results = getOutpatientOrderNumbers.getOutpatientOrderNumbersResponse(list);
        OutpatientOrderNumber outpatientOrderNumber = results.get(0);
        assertEquals("1", outpatientOrderNumber.getOrderno());
        assertEquals("21", outpatientOrderNumber.getOrderseq());
        assertEquals("2015", outpatientOrderNumber.getOrdertime());
        assertEquals("123", outpatientOrderNumber.getShiftcode());
        assertEquals("2016", outpatientOrderNumber.getOrderendtime());


    }
}