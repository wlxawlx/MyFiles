package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDoctorSchedulForOrder;
import com.jandar.alipay.core.struct.DoctorScheduleInfo;
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
public class GetDoctorSchedulForOrderTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDoctorSchedulForOrderRequest() throws Exception {
        String doctorcode = "123";
        String departcode = "32";
        String scheduledate = "2016";
        GetDoctorSchedulForOrder getDoctorSchedulForOrder = new GetDoctorSchedulForOrder();
        Map<String, Object> map = getDoctorSchedulForOrder.getDoctorSchedulForOrderRequest(doctorcode, departcode, scheduledate);
        System.out.println(map);
        assertEquals("123", map.get("doctorcode"));
        assertEquals("32", map.get("departcode"));
        assertEquals("2016", map.get("scheduledate"));
    }

    @Test
    public void testGetDoctorSchedulForOrderResponse() throws Exception {
        String str = "scheduleseq=1,departcode=2,departname=xiaomi,doctorcode=3,doctorname=jack,special=5,remain=60,total=500,address=as,scheduledate=2016" +
                ",shiftcode=25,shiftname=ni,fee=3";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);
        GetDoctorSchedulForOrder getDoctorSchedulForOrder = new GetDoctorSchedulForOrder();
        List<DoctorScheduleInfo> dslist = getDoctorSchedulForOrder.getDoctorSchedulForOrderResponse(list);
        DoctorScheduleInfo doctorScheduleInfo = dslist.get(0);
        assertEquals("1", doctorScheduleInfo.getScheduleseq());
        assertEquals("2", doctorScheduleInfo.getDepartcode());
        assertEquals("xiaomi", doctorScheduleInfo.getDepartname());
        assertEquals("3", doctorScheduleInfo.getDoctorcode());
        assertEquals("jack", doctorScheduleInfo.getDoctorname());
        assertEquals("5", doctorScheduleInfo.getSpecial());
        assertEquals("60", doctorScheduleInfo.getRemain());
        assertEquals("500", doctorScheduleInfo.getTotal());
        assertEquals("as", doctorScheduleInfo.getAddress());
        assertEquals("2016", doctorScheduleInfo.getScheduledate());
        assertEquals("25", doctorScheduleInfo.getShiftcode());
        assertEquals("ni", doctorScheduleInfo.getShiftname());
        assertEquals("3", doctorScheduleInfo.getFee());
    }
}