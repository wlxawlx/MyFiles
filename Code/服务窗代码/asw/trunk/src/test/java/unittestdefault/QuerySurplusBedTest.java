package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.QuerySurplusBed;
import com.jandar.alipay.core.struct.BedQueryInfo;
import com.jandar.alipay.util.MapUtil;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.assertEquals;

/**
 * Created by yubj on 2016/4/12.
 */
public class QuerySurplusBedTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testQuerySurplusBedResponse() throws Exception {
        //测试数据
        String str = "bqmc=一病区,sycw=5";
        String str2 = "bqmc=二病区,sycw=3";
        Map<String, String> map1 = MapUtil.stringToMap(str);
        Map<String, String> map2 = MapUtil.stringToMap(str2);
        System.out.println(map1);
        System.out.println(map2);
        List<Map<String, String>> values = new ArrayList<>();
        values.add(map1);
        values.add(map2);

        QuerySurplusBed querySurplusBed = new QuerySurplusBed();
        List<BedQueryInfo> result = querySurplusBed.querySurplusBedResponse(values);
        BedQueryInfo bedQueryInfo1 = result.get(0);
        assertEquals("一病区", bedQueryInfo1.getBqmc());
        assertEquals("5", bedQueryInfo1.getSycw());
        BedQueryInfo bedQueryInfo = result.get(1);
        assertEquals("二病区", bedQueryInfo.getBqmc());
        assertEquals("3", bedQueryInfo.getSycw());
    }
}