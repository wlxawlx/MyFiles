package unittestdefault.impl;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.impl.HISServiceHandlerDefault;
import com.jandar.alipay.core.impl.servicedefault.UserRegister;
import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.hospital.UserInfo;
import org.apache.commons.lang3.StringUtils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by yubj on 2016/4/25.
 */
public class HIServiceHandlerDefaultUnit extends HISServiceHandlerDefault {

    public boolean userRegister(HISUserInfo info) throws HospitalException {
        /***单元测试**/
        UserRegister userRegister = new UserRegister();
        Map<String, Object> parameters = userRegister.userRegisterRequest(info);
        List<Map<String, String>> process = process("UI010101", parameters);
        return userRegister.userRegisterResponse(process);
        /***单元测试**/
    }

    protected static List<Map<String, String>> process(String operate, Map<String, Object> parameters)
            throws HospitalException {
        if ("UI010101".equalsIgnoreCase(operate)) {
            UserInfo userInfo=new UserInfo();
            userInfo.setAlipayUserId("123456");
            Map<String, String> returnedMap_1 = new HashMap<String, String>();
            String name = (String) parameters.get("name");
            String phone = (String) parameters.get("phone");
            String idcardno = (String) parameters.get("idcardno");
            String address = (String) parameters.get("openid");
            String openid = (String) parameters.get("openid");
            String headurl = (String) parameters.get("headurl");
            String usertype = (String) parameters.get("usertype");
            if(openid.equals(userInfo.getAlipayUserId())){
                throw new HospitalException("已注册");
            }
            if (StringUtils.isNotBlank(name) && StringUtils.isNotBlank(phone) && StringUtils.isNotBlank(idcardno) &&
                    StringUtils.isNotBlank(openid) && StringUtils.isNotBlank(headurl)&&StringUtils.isNotBlank(usertype)) {
                returnedMap_1.put("status", "注册成功");
            } else if (StringUtils.isBlank(name) || StringUtils.isBlank(phone) || StringUtils.isBlank(idcardno) ||
                    StringUtils.isBlank(openid) || StringUtils.isBlank(headurl)||StringUtils.isBlank(usertype)) {
                throw new HospitalException("参数不完整，无法提交");
            } else if(idcardno.length()!=18||phone.length()!=11){
                returnedMap_1.put("status", "注册失败");
            }
            List<Map<String, String>> returnedList_1 = new ArrayList<Map<String, String>>();
            returnedList_1.add(returnedMap_1);
            return returnedList_1;
        }
        return null;
    }
}
