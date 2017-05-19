package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetTestIndicatorsInfo;
import com.jandar.alipay.core.struct.TestIndicator;
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
 * Created by yubj on 2016/4/18.
 */
public class GetTestIndicatorsInfoTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetTestIndicatorsInfoRequest() throws Exception {
        String doctadviseno="sdf515";
        GetTestIndicatorsInfo getTestIndicatorsInfo = new GetTestIndicatorsInfo();
        Map<String,Object> map=getTestIndicatorsInfo.getTestIndicatorsInfoRequest(doctadviseno);
        System.out.println(map);
        assertEquals("sdf515",map.get("doctadviseno"));
    }

    @Test
    public void testGetTestIndicatorsInfoResponse() throws Exception {
        String str="jylx=1,xmmc=2,result=3,hint=4,jkfw=5,xmdw=6";
        Map<String ,String > map= MapUtil.stringToMap(str);
        List<Map<String,String>> list=new ArrayList<Map<String,String>>();
        list.add(map);
        System.out.println(list);
        GetTestIndicatorsInfo getTestIndicatorsInfo = new GetTestIndicatorsInfo();
        List<TestIndicator> results=getTestIndicatorsInfo.getTestIndicatorsInfoResponse(list);
        TestIndicator li=results.get(0);
        assertEquals("1",li.getJylx());
        assertEquals("2",li.getXmmc());
        assertEquals("3",li.getResult());
        assertEquals("4",li.getHint());
        assertEquals("5",li.getCkfw());
        assertEquals("6",li.getXmdw());
    }
}