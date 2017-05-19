package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.BindOutpatientCard;
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
public class BindOutpatientCardTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testBindOutpatientCardRequest() throws Exception {
        String openid="123456";
        String cardno="7890";
        String patientid="56560";
        BindOutpatientCard bindOutpatientCard=new BindOutpatientCard();
        Map<String, Object> res=bindOutpatientCard.bindOutpatientCardRequest(openid,cardno,patientid);
        System.out.println(res);
        assertEquals("123456",res.get("openid"));
        assertEquals("7890",res.get("cardno"));
        assertEquals("56560",res.get("patientid"));
    }

    @Test
    public void testBindOutpatientCardResponse() throws Exception {
        String str="bklx=1,cardname=市民卡,bkhm=1230";
        Map<String,String> map= MapUtil.stringToMap(str);
        List<Map<String, String>> process=new ArrayList<>();
        process.add(map);

        BindOutpatientCard bindOutpatientCard=new BindOutpatientCard();
        OutPatientCardInfo ocinfo = bindOutpatientCard.bindOutpatientCardResponse(process);
        assertEquals("1",ocinfo.getCardtype());
        assertEquals("市民卡",ocinfo.getCardname());
        assertEquals("1230",ocinfo.getCardno());
    }
}