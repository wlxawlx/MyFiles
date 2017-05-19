package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.UnbindOutpatientCard;
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
public class UnbindOutpatientCardTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testUnbindOutpatientCardRequest() throws Exception {
        String openid = "121";
        UnbindOutpatientCard unbindOutpatientCard = new UnbindOutpatientCard();
        Map<String, Object> res = unbindOutpatientCard.unbindOutpatientCardRequest(openid);
        assertEquals("121", res.get("openid"));
    }

    @Test
    public void testUnbindOutpatientCardResponse() throws Exception {
        List<Map<String, String>> process = null;
        UnbindOutpatientCard unbindOutpatientCard = new UnbindOutpatientCard();
        boolean res = unbindOutpatientCard.unbindOutpatientCardResponse(process);
        System.out.println(res);
        assertEquals(false, res);

        List<Map<String, String>> process2 = new ArrayList<>();
        String str = "openid=212";
        Map<String, String> map = MapUtil.stringToMap(str);
        process2.add(map);
        boolean rs = unbindOutpatientCard.unbindOutpatientCardResponse(process2);
        System.out.println(rs);
        assertEquals(true, rs);

    }
}