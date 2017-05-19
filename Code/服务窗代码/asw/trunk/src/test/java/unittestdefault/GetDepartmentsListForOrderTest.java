package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDepartmentsListForOrder;
import com.jandar.alipay.core.struct.DepartmentInfo;
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
public class GetDepartmentsListForOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDepartmentsListForOrderRequest() throws Exception {
        String departcode = "1sdf";
        GetDepartmentsListForOrder getDepartmentsListForOrder = new GetDepartmentsListForOrder();
        Map<String, Object> map = getDepartmentsListForOrder.getDepartmentsListForOrderRequest(departcode);
        assertEquals("1sdf", map.get("departcode"));
    }

    @Test
    public void testGetDepartmentsListForOrderResponse() throws Exception {
        String str = "departcode=1,departname=a,secondcode=2,secondname=b,describe=c";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetDepartmentsListForOrder getDepartmentsListForOrder = new GetDepartmentsListForOrder();
        List<DepartmentInfo> deinfolist = getDepartmentsListForOrder.getDepartmentsListForOrderResponse(list);
        DepartmentInfo res = deinfolist.get(0);
        assertEquals("1", res.getDepartcode());
        assertEquals("a", res.getDepartname());
        assertEquals("2", res.getSecondcode());
        assertEquals("b", res.getSecondname());
        assertEquals("c", res.getDescribe());

    }
}