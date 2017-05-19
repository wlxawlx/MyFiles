package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.UnbindContactOutpatientCard;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
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
public class UnbindContactOutpatientCardTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testUnbindContactOutpatientCard() throws Exception {
        String openid = "1";
        String linkmanid = "2";
        UnbindContactOutpatientCard unbindContactOutpatientCard = new UnbindContactOutpatientCard();
        Map<String, Object> res = unbindContactOutpatientCard.unbindContactOutpatientCardRequest(openid, linkmanid);
        System.out.println(res);
        assertEquals("1", res.get("openid"));
        assertEquals("2", res.get("linkmanid"));
    }

    @Test
    public void testUnbindContactOutpatientCard1() throws Exception {
        String str = "cardno=11";
        Map<String, String> map = MapUtil.stringToMap(str);
        List<Map<String, String>> list = new ArrayList<>();
        list.add(map);

        UnbindContactOutpatientCard unbindContactOutpatientCard = new UnbindContactOutpatientCard();
        OutPatientCardInfo cpinfo = unbindContactOutpatientCard.unbindContactOutpatientCardResponse(list);
        assertEquals("11", cpinfo.getCardno());
    }
}