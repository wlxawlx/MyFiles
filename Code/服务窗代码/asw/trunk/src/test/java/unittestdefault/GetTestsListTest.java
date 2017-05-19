package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetTestsList;
import com.jandar.alipay.core.struct.Inspection;
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
public class GetTestsListTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetTestsListRequest() throws Exception {
        String name = "小米";
        String idcardno = "123456";
        GetTestsList getTestsList = new GetTestsList();
        Map<String, Object> params = getTestsList.getTestsListRequest(name, idcardno);
        System.out.println(params);
        assertEquals("小米", params.get("name"));
        assertEquals("123456", params.get("idcardno"));
    }

    @Test
    public void testGetTestsListResponse() throws Exception {
        String str = "doctadviseno=1,examinaim=2,requesttime=11:20:30,requester=4";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetTestsList getTestsList = new GetTestsList();
        List<Inspection> results = getTestsList.getTestsListResponse(list);
        Inspection inspection = results.get(0);
        assertEquals("1", inspection.getDoctadviseno());
        assertEquals("2", inspection.getExaminaim());
        assertEquals("11:20:30 00:00:00", inspection.getRequesttime());
        assertEquals("4", inspection.getRequester());
    }
}