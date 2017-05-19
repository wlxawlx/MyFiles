package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.QueryFeeItemsByPy;
import com.jandar.alipay.core.struct.FeeItemsInfo;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * 收费项目-按拼音码或名称查询 - 查询基本功能
 * Created by yubj on 2016/4/12.
 */
public class QueryFeeItemsByPyTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testQueryFeeItemsByPyRequest() throws Exception {
        String pydm = "a";
        QueryFeeItemsByPy queryFeeItemsByPy = new QueryFeeItemsByPy();
        Map<String, Object> parameters = queryFeeItemsByPy.queryFeeItemsByPyRequest(pydm);
        System.out.println(parameters);
        assertEquals("a", parameters.get("pydm"));
    }

    @Test
    public void testQueryFeeItemsByPyResponse() throws Exception {
        String str = "fylx=1,fymc=a,fydw=g,fyjg=10";
        String str2 = "fylx=2,fymc=b,fydw=g,fyjg=20";
        List<Map<String, String>> values = new ArrayList<>();
        Map<String, String> map1 = MapUtil.stringToMap(str);
        Map<String, String> map2 = MapUtil.stringToMap(str2);
        values.add(map1);
        values.add(map2);
        QueryFeeItemsByPy queryFeeItemsByPy = new QueryFeeItemsByPy();
        List<FeeItemsInfo> list = queryFeeItemsByPy.queryFeeItemsByPyResponse(values);
        FeeItemsInfo feeItemsInfo1 = list.get(0);
        FeeItemsInfo feeItemsInfo2 = list.get(1);
        assertEquals("g", feeItemsInfo1.getFydw());
        assertEquals("10", feeItemsInfo1.getFyjg());
        assertEquals("1", feeItemsInfo1.getFylx());
        assertEquals("a", feeItemsInfo1.getFymc());
        assertEquals("g", feeItemsInfo2.getFydw());
        assertEquals("20", feeItemsInfo2.getFyjg());
        assertEquals("2", feeItemsInfo2.getFylx());
        assertEquals("b", feeItemsInfo2.getFymc());

    }
}