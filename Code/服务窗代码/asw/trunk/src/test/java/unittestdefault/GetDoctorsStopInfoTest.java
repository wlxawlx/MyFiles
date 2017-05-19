package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDoctorInfoByCode;
import com.jandar.alipay.core.impl.servicedefault.GetDoctorsStopInfo;
import com.jandar.alipay.core.struct.DoctorInfo;
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
public class GetDoctorsStopInfoTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDoctorsStopInfoResponse() throws Exception {
        String str = "code=1,name=2,pictureurl=4,departcode=8,departname=9,stopdate=3";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> process = new ArrayList<>();
        process.add(map);

        GetDoctorsStopInfo getDoctorsStopInfo = new GetDoctorsStopInfo();
        List<DoctorInfo> list = getDoctorsStopInfo.getDoctorsStopInfoResponse(process);
        DoctorInfo doctorInfo = list.get(0);
        assertEquals("1", doctorInfo.getCode());
        assertEquals("2", doctorInfo.getName());
        assertEquals("4", doctorInfo.getPictureurl());
        assertEquals("8", doctorInfo.getDepartcode());
        assertEquals("9", doctorInfo.getDepartname());
        assertEquals("3", doctorInfo.getStopdate());
    }
}