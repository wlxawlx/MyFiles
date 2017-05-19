package unittestdefault;

import com.jandar.alipay.core.impl.servicedefault.GetOutpatientCardsList;
import com.jandar.alipay.core.struct.OutPatientCardInfo;
import junit.framework.TestCase;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import static org.junit.Assert.assertEquals;

/**
 * Created by yubj on 2016/4/11.
 */
public class GetOutpatientCardsListTest extends TestCase {

    @Before
    public void setUp() throws Exception {
        super.setUp();
    }

    @Test
    public void testGetOutpatientCardsListRequest() throws Exception {
        String idcardno = "330681199311090022";
        String name = "小米";
        GetOutpatientCardsList getOutpatientCardsList=new GetOutpatientCardsList();
        Map<String,Object> result=getOutpatientCardsList.getOutpatientCardsListRequest(idcardno,name);
        assertEquals("小米",result.get("name"));
        assertEquals("330681199311090022",result.get("idcardno"));
    }

    @Test
    public void testGetOutpatientCardsListResponse() throws Exception {
        String str="bklx=1,cardname=小米,bkhm=12155,sfzh=330681199311090022,brid=522,brxm=brren,birthday=2016-12-03,lxdh=8888888,balance=32,cost=99";
        Map<String,String> map= com.jandar.alipay.util.MapUtil.stringToMap(str);
        List<Map<String, String>> process=new ArrayList<>();
        process.add(map);
        GetOutpatientCardsList getOutpatientCardsList=new GetOutpatientCardsList();
        List<OutPatientCardInfo> result=getOutpatientCardsList.getOutpatientCardsListResponse(process);
        OutPatientCardInfo info=result.get(0);
        assertEquals("1",info.getCardtype());
        assertEquals("小米",info.getCardname());
        assertEquals("12155",info.getCardno());
        assertEquals("330681199311090022",info.getIdcardno());
        assertEquals("522",info.getPatientid());
        assertEquals("brren",info.getPatientname());
        assertEquals("2016-12-03",info.getBirthday());
        assertEquals("8888888",info.getPhone());
        assertEquals("32",info.getBalance());
        assertEquals("99",info.getCost());
    }
}