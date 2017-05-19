package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetContactsList;
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
public class GetContactsListTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetContactsListRequest() throws Exception {
        String openid = "123456";
        String linkmanid = "3";
        GetContactsList getContactsList = new GetContactsList();
        Map<String, Object> parameters = getContactsList.getContactsListRequest(openid, linkmanid);
        assertEquals("123456", parameters.get("openid"));
        assertEquals("3", parameters.get("linkmanid"));
    }

    @Test
    public void testGetContactsListResponse() throws Exception {
        List<Map<String, String>> contacts = new ArrayList<>();
        Map<String, String> val = new HashMap<>();
        val.put("linkmanid", "1");
        val.put("label", "2");
        val.put("name", "小米");
        val.put("phone", "15006851271");
        val.put("idcardno", "330681199311090022");
        val.put("address", "");
        val.put("cardno", "");
        val.put("bindcardfalg", "1");
        val.put("patientid", "1234");
        contacts.add(val);
        String linkmanid = "1";
        GetContactsList getContactsList = new GetContactsList();
        List<ContactPeopleInfo> results = getContactsList.getContactsListResponse(contacts, linkmanid, new ArrayList<ContactPeopleInfo>());
        assertEquals("1", results.get(0).getLinkmanid());
        assertEquals("2", results.get(0).getLabel());
        assertEquals("小米", results.get(0).getName());
        assertEquals("15006851271", results.get(0).getPhone());
        assertEquals("330681199311090022", results.get(0).getIdcardno());
        assertEquals("", results.get(0).getAddress());
        assertEquals("", results.get(0).getCardno());
        assertEquals("1", results.get(0).getBindcardfalg());
        assertEquals("1234", results.get(0).getPatientid());
    }
}