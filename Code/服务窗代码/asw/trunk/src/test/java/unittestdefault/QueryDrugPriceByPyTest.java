package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.QueryDrugPriceByPy;
import com.jandar.alipay.core.struct.DrugPriceInfo;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/12.
 */
public class QueryDrugPriceByPyTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testQqueryDrugPriceByPyRequest() throws Exception {
        String pydm = "a";

        QueryDrugPriceByPy queryDrugPriceByPy = new QueryDrugPriceByPy();
        Map<String, Object> map = queryDrugPriceByPy.qqueryDrugPriceByPyRequest(pydm);
        System.out.println(map);
        assertEquals("a", map.get("pydm"));

    }

    @Test
    public void testQueryDrugPriceByPyResponse() throws Exception {
        String str1 = "yplx=1,ypmc=a,ypdw=d,ypgg=g,ypcd=ae,ypjg=2";
        String str2 = "yplx=2,ypmc=b,ypdw=d,ypgg=g,ypcd=be,ypjg=3";
        Map<String, String> result = MapUtil.stringToMap(str1);
        Map<String, String> result2 = MapUtil.stringToMap(str2);
        List<Map<String, String>> values = new ArrayList<Map<String, String>>();
        values.add(result);
        values.add(result2);
        QueryDrugPriceByPy queryDrugPriceByPy = new QueryDrugPriceByPy();
        List<DrugPriceInfo> list = queryDrugPriceByPy.queryDrugPriceByPyResponse(values);
        DrugPriceInfo drugPriceInfo = list.get(0);
        assertEquals("1", drugPriceInfo.getYplx());
        assertEquals("a", drugPriceInfo.getYpmc());
        assertEquals("d", drugPriceInfo.getYpdw());
        assertEquals("g", drugPriceInfo.getYpgg());
        assertEquals("ae", drugPriceInfo.getYpcd());
        assertEquals("2", drugPriceInfo.getYpjg());
        DrugPriceInfo drugPriceInfo2 = list.get(1);
        assertEquals("2", drugPriceInfo2.getYplx());
        assertEquals("b", drugPriceInfo2.getYpmc());
        assertEquals("d", drugPriceInfo2.getYpdw());
        assertEquals("g", drugPriceInfo2.getYpgg());
        assertEquals("be", drugPriceInfo2.getYpcd());
        assertEquals("3", drugPriceInfo2.getYpjg());
    }
}