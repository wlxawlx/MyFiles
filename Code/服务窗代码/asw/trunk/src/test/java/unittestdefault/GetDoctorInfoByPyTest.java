package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetDoctorInfoByPy;
import com.jandar.alipay.core.struct.DoctorInfo;
import com.jandar.alipay.core.struct.PhysicalItem;
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
public class GetDoctorInfoByPyTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetDoctorInfoByPyRequest() throws Exception {
        String namepy = "1";
        String pagesize = "1";
        String pageindex = "10";
        GetDoctorInfoByPy getDoctorInfoByPy = new GetDoctorInfoByPy();
        Map<String, Object> res = getDoctorInfoByPy.getDoctorInfoByPyRequest(namepy, pageindex, pagesize);
        System.out.println(res);
        assertEquals("1", res.get("namepy"));
        assertEquals("10", res.get("dqym"));
        assertEquals("1", res.get("xssl"));
    }

    @Test
    public void testGetDoctorInfoByPy() throws Exception {
        String str = "code=1,name=2,sex=3,pictureurl=4,level=5,recommend=6,adept=7,departcode=8,departname=9";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> process = new ArrayList<>();
        process.add(map);
        GetDoctorInfoByPy getDoctorInfoByPy = new GetDoctorInfoByPy();
        List<DoctorInfo> list = getDoctorInfoByPy.getDoctorInfoByPyResponse(process);
        DoctorInfo d = list.get(0);
        assertEquals("1", d.getCode());
        assertEquals("2", d.getName());
        assertEquals("3", d.getSex());
        assertEquals("4", d.getPictureurl());
        assertEquals("5", d.getLevel());
        assertEquals("6", d.getRecommend());
        assertEquals("7", d.getAdept());
        assertEquals("8", d.getDepartcode());
        assertEquals("9", d.getDepartname());

    }
}