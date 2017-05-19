package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.AddContact;
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
public class AddContactTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testAddContactRequest() throws Exception {
        //测试数据
        String openid = "123456";
        String label = "";
        String name = "小米";
        String phone = "15006851271";
        String idcardno = "330681199311090022";
        String address = "";
        AddContact addContact = new AddContact();
        Map<String, Object> result = addContact.addContactRequest(openid, label, name, phone, idcardno, address);
//        Map<String, Object> result=null;
//        try{
//          result = addContact.addContactRequest(openid, label, name, phone, idcardno, address);
//        }catch (Exception e){
//           assertEquals("手机号码输入有误,请重新输入",e.getMessage());
//        }
        //测试
        assertEquals("123456", result.get("openid"));
        assertEquals("", result.get("label"));
        assertEquals("小米", result.get("name"));
        assertEquals("15006851271", result.get("phone"));
        assertEquals("330681199311090022", result.get("idcardno"));
        assertEquals("", result.get("address"));
    }

    @Test
    public void testAddContactResponse() throws Exception {
        //测试数据
        List<Map<String, String>> process = new ArrayList<>();
        Map<String, String> res = new HashMap<>();
        res.put("label", "1");
        res.put("name", "小米");
        res.put("linkmanid", "3");
        process.add(res);

        ContactPeopleInfo cpinfo = new ContactPeopleInfo();
        if (process.size() > 0) {
            Map<String, String> re = process.get(0);
            cpinfo.setLabel(re.get("label"));
            cpinfo.setName(re.get("name"));
            cpinfo.setLinkmanid(re.get("linkmanid"));
        }
        //测试
        assertEquals("1", cpinfo.getLabel());
        assertEquals("小米", cpinfo.getName());
        assertEquals("3", cpinfo.getLinkmanid());
    }
}