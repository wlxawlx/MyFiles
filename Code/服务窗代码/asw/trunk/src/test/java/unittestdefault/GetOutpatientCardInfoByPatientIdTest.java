package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetOutpatientCardInfoByPatientId;
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
public class GetOutpatientCardInfoByPatientIdTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetOutpatientCardInfoByPatientIdRequest() throws Exception {
        String patientid="123456";
        GetOutpatientCardInfoByPatientId getOutpatientCardInfoByPatientId=new GetOutpatientCardInfoByPatientId();
        Map<String, Object> res=getOutpatientCardInfoByPatientId.getOutpatientCardInfoByPatientIdRequest(patientid);
        assertEquals("123456",res.get("patientid"));
    }

    @Test
    public void testGetOutpatientCardInfoByPatientIdResponse() throws Exception {
        String str="bklx=1,cardname=市民卡,bkhm=123,sfzh=330681,brid=123654,brxm=小米,birthday=1992-11-09,lxdh=8888888,balance=100,cost=8";
        Map<String,String> map= MapUtil.stringToMap(str);
        System.out.println(map);
        List<Map<String, String>> process=new ArrayList<>();
        process.add(map);

        GetOutpatientCardInfoByPatientId getOutpatientCardInfoByPatientId=new GetOutpatientCardInfoByPatientId();
        OutPatientCardInfo res=getOutpatientCardInfoByPatientId.getOutpatientCardInfoByPatientIdResponse(process);
        assertEquals("1",res.getCardtype());
        assertEquals("市民卡",res.getCardname());
        assertEquals("123",res.getCardno());
        assertEquals("330681",res.getIdcardno());
        assertEquals("123654",res.getPatientid());
        assertEquals("小米",res.getPatientname());
        assertEquals("1992-11-09",res.getBirthday());
        assertEquals("8888888",res.getPhone());
        assertEquals("100",res.getBalance());
        assertEquals("8",res.getCost());

    }
}