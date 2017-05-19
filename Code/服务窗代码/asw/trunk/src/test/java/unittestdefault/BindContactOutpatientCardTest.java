package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.BindContactOutpatientCard;
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
public class BindContactOutpatientCardTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testBindContactOutpatientCardRequest() throws Exception {
        String openid="12";
        String linkmanid="1";
        String cardno="1234";
        String patientid="21";
        BindContactOutpatientCard bindContactOutpatientCard=new BindContactOutpatientCard();
        Map<String, Object> res=bindContactOutpatientCard.bindContactOutpatientCardRequest(openid,linkmanid,cardno,patientid);
        assertEquals("12",res.get("openid"));
        assertEquals("1",res.get("linkmanid"));
        assertEquals("1234",res.get("cardno"));
        assertEquals("21",res.get("patientid"));
    }

    @Test
    public void testBindContactOutpatientCardResponse() throws Exception {
        String str="cardtype=1,cardname=afd,cardno=1212";
        Map<String, String> parameter= MapUtil.stringToMap(str);
        List<Map<String, String>> process=new ArrayList<>();
        process.add(parameter);
        String patientid="21";
        BindContactOutpatientCard bindContactOutpatientCard=new BindContactOutpatientCard();
        OutPatientCardInfo outPatientCardInfo=new OutPatientCardInfo();
        outPatientCardInfo=bindContactOutpatientCard.bindContactOutpatientCardResponse(process,patientid);
        assertEquals("1",outPatientCardInfo.getCardtype());
        assertEquals("afd",outPatientCardInfo.getCardname());
        assertEquals("1212",outPatientCardInfo.getCardno());
        assertEquals("21",outPatientCardInfo.getPatientid());
    }
}