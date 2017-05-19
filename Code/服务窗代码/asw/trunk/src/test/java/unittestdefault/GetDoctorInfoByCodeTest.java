package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDoctorInfoByCode;
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
public class GetDoctorInfoByCodeTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDoctorInfoByCodeRequest() throws Exception {
        String code = "akdf";
        GetDoctorInfoByCode getDoctorInfoByCode = new GetDoctorInfoByCode();
        Map<String, Object> res = getDoctorInfoByCode.getDoctorInfoByCodeRequest(code);
        assertEquals("akdf", res.get("code"));
    }

    @Test
    public void testGetDoctorInfoByCodeResponse() throws Exception {
        String str = "code=1,name=2,sex=3,pictureurl=4,level=5,recommend=6,adept=7,departcode=8,departname=9";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> process = new ArrayList<>();
        process.add(map);

        GetDoctorInfoByCode getDoctorInfoByCode = new GetDoctorInfoByCode();
        List<DoctorInfo> list = getDoctorInfoByCode.getDoctorInfoByCodeResponse(process);
        DoctorInfo doctorInfo = list.get(0);
        assertEquals("1", doctorInfo.getCode());
        assertEquals("2", doctorInfo.getName());
        assertEquals("3", doctorInfo.getSex());
        assertEquals("4", doctorInfo.getPictureurl());
        assertEquals("5", doctorInfo.getLevel());
        assertEquals("6", doctorInfo.getRecommend());
        assertEquals("7", doctorInfo.getAdept());
        assertEquals("8", doctorInfo.getDepartcode());
        assertEquals("9", doctorInfo.getDepartname());
    }
}