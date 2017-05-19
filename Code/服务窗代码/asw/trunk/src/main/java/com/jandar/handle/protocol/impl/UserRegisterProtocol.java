/**
 *
 */
package com.jandar.handle.protocol.impl;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.HISUserInfo;
import com.jandar.alipay.core.struct.PlatformType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.handle.protocol.Protocol;
import com.jandar.alipay.util.MapUtil;
import com.jandar.filter.auth.util.ThirdUserInfo;
import org.apache.commons.lang3.StringUtils;

import java.util.HashMap;
import java.util.Map;

/**
 * @author Administrator
 */
public class UserRegisterProtocol implements Protocol {

    /**
     * @author yubj
     * <p/>
     * 用户注册
     */
    @Override
    public Map<String, String> process(String pcode, Map<String, Object> params) throws HospitalException {
        return process(MapUtil.getString(params, "name"), MapUtil.getString(params, "phone"), MapUtil.getString(params, "idcardno"),
                MapUtil.getString(params, "address"), MapUtil.getString(params, "patientid"), MapUtil.getString(params, "cardno"));
    }

    public Map<String, String> process(String name, String phone, String idcardno, String address, String patientId, String cardno) throws HospitalException {
        boolean values;
        if (StringUtils.isBlank(cardno)) {
            values = HospitalInfoService.getInstance().userRegister(requiredParams(name, phone, idcardno, address, patientId, cardno));
        } else {
            values = HospitalInfoService.getInstance().userRegisterAndBindCard(requiredParams(name, phone, idcardno, address, patientId, cardno));
        }
        UserInfo info = ServiceContext.getHospitalUserInfo();
        if (values) {
            Map<String, String> result = new HashMap<String, String>();
            result.put("yhid", info.getYhid());
            result.put("state", "1");
            return result;
        } else {
            throw new HospitalException("注册失败");
        }
    }

    /**
     * 封装HISUserInfo数据
     *
     * @param name      姓名
     * @param phone     手机
     * @param idcardno  身份证
     * @param address   地址
     * @param patientId 病人id
     * @param cardno    就诊卡号
     * @return HISUserInfo
     * @throws HospitalException
     */
    private HISUserInfo requiredParams(String name, String phone, String idcardno, String address, String patientId, String cardno)
            throws HospitalException {
        if (StringUtils.isBlank(name)) {
            throw new HospitalException("缺少用户名");
        }
        // 其它校验
        if (StringUtils.isBlank(phone)) {
            throw new HospitalException("缺少手机号");
        }
        if (StringUtils.isBlank(idcardno)) {
            throw new HospitalException("缺少身份证号");
        }

        ThirdUserInfo user = ServiceContext.getThirdUserInfo();
        if (user == null) {
            // 没有获得用户基本信息就进行了注册操作
            throw new HospitalException("请刷新后再试", HospitalException.UN_USERINFO);
        }

        // 这里key要对应医院端接口的参数名
        HISUserInfo required = new HISUserInfo();
        required.setName(name);
        required.setPhone(phone);
        required.setIdcardno(idcardno);
        required.setAddress(address == null ? "" : address);
        required.setOpenid(user.getOpenId());
        required.setHeadurl(user.getAvatar());
        required.setCardno(cardno);
        required.setPatientid(patientId);
        required.setUsertype(PlatformType.Alipay);
        return required;
    }

}
