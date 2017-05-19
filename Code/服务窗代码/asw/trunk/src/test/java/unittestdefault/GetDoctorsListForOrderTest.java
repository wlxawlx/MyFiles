package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDepartmentSchedulForOrder;
import com.jandar.alipay.core.impl.servicedefault.GetDoctorsListForOrder;
import com.jandar.alipay.core.struct.DoctorInfo;
import com.jandar.alipay.core.struct.SchedulingInfo;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/13.
 */
public class GetDoctorsListForOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDoctorsListForOrderRequest() throws Exception {
        String departcode="123";
        String scheduledate="888";
        GetDoctorsListForOrder getDoctorsListForOrder=new GetDoctorsListForOrder();
        Map<String, Object> map=getDoctorsListForOrder.getDoctorsListForOrderRequest(departcode,scheduledate);
        assertEquals("123",map.get("departcode"));
        assertEquals("888",map.get("scheduledate"));
    }

    @Test
    public void testGetDoctorsListForOrderResponse() throws Exception {
        String str = "doctorcode=1996,doctorname=100,pictureurl=500,level=1996,recommend=100,adept=500,reserve=100,scheduledates=500";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetDoctorsListForOrder getDoctorsListForOrder=new GetDoctorsListForOrder();
        List<DoctorInfo> scinfo = getDoctorsListForOrder.getDoctorsListForOrderResponse(list);
        DoctorInfo res = scinfo.get(0);
        assertEquals("1996", res.getCode());
        assertEquals("100", res.getName());
        assertEquals("500", res.getPictureurl());
        assertEquals("1996", res.getLevel());
        assertEquals("100", res.getRecommend());
        assertEquals("500", res.getAdept());
        assertEquals("100", res.getReserve());
        assertEquals("500", res.getScheduledates());
    }
}