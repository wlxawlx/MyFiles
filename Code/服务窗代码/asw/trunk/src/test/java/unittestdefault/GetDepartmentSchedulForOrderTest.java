package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDepartmentSchedulForOrder;
import com.jandar.alipay.core.struct.SchedulingInfo;
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
 * Created by yubj on 2016/4/13.
 */
public class GetDepartmentSchedulForOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDepartmentSchedulForOrderRequest() throws Exception {
        String departcode = "123";
        GetDepartmentSchedulForOrder getDepartmentSchedulForOrder = new GetDepartmentSchedulForOrder();
        Map<String, Object> map = getDepartmentSchedulForOrder.getDepartmentSchedulForOrderRequest(departcode);
        System.out.println(map);
        assertEquals("123", map.get("departcode"));
    }

    @Test
    public void testGetDepartmentSchedulForOrderResponse() throws Exception {
        String str = "scheduledate=1996,remain=100,total=500";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetDepartmentSchedulForOrder getDepartmentSchedulForOrder = new GetDepartmentSchedulForOrder();
        List<SchedulingInfo> scinfo = getDepartmentSchedulForOrder.getDepartmentSchedulForOrderResponse(list);
        SchedulingInfo res = scinfo.get(0);
        assertEquals("1996", res.getScheduledate());
        assertEquals("100", res.getRemain());
        assertEquals("500", res.getTotal());
    }
}