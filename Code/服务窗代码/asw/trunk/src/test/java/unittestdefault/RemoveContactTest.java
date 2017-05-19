package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.RemoveContact;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.assertEquals;

/**
 * Created by yubj on 2016/4/11.
 */
public class RemoveContactTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testRemoveContactRequest() throws Exception {
        String openid="123456";
        String linkmanid="1";
        RemoveContact removeContact=new RemoveContact();
        Map<String, Object> map=removeContact.removeContactRequest(openid,linkmanid);
        assertEquals("123456",map.get("openid"));
        assertEquals("1",map.get("linkmanid"));
    }

    @Test
    public void testRemoveContactResponse() throws Exception {
        //测试数据
        List<Map<String, String>> list=new ArrayList<>();
        Map<String, String> map=new HashMap<>();
        map.put("linkmanid","1");
        map.put("name","小米");
        list.add(map);
        RemoveContact removeContact=new RemoveContact();
        ContactPeopleInfo contactPeopleInfo=removeContact.removeContactResponse(list);
        assertEquals("1",contactPeopleInfo.getLinkmanid());
        assertEquals("小米",contactPeopleInfo.getName());


    }
}