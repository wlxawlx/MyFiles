package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetInspectionsList;
import com.jandar.alipay.core.struct.Inspection;
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
public class GetInspectionsListTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetInspectionsListRequest() throws Exception {
        String name = "小米";
        String idcardno = "88888888";
        GetInspectionsList getInspectionsList = new GetInspectionsList();
        Map<String, Object> map = getInspectionsList.getInspectionsListRequest(name, idcardno);
        assertEquals("小米", map.get("name"));
        assertEquals("88888888", map.get("idcardno"));

    }

    @Test
    public void testGetInspectionsListResponse() throws Exception {
        String str = "doctadviseno=1,examinaim=2,requesttime=3,requester=4";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetInspectionsList getInspectionsList = new GetInspectionsList();
        List<Inspection> results = getInspectionsList.getInspectionsListResponse(list);
        Inspection inspection = results.get(0);
        assertEquals("1", inspection.getDoctadviseno());
        assertEquals("2", inspection.getExaminaim());
        assertEquals("3 00:00:00", inspection.getRequesttime());
        assertEquals("4", inspection.getRequester());
    }
}