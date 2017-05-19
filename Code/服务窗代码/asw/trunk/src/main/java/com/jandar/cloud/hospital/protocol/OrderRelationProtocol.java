package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.ContactPeopleInfo;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllness;
import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.bean.OrderRelation;
import com.jandar.cloud.hospital.bean.Patient;
import com.jandar.cloud.hospital.dao.ChronicIllnessDao;
import com.jandar.cloud.hospital.dao.DoctorDao;
import com.jandar.cloud.hospital.dao.OrderRelationDao;
import com.jandar.cloud.hospital.dao.PatientDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.StringUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.Resource;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 可以在云医院就诊的慢性病列表
 * Created by zzw on 16/8/30.
 */
@Component
public class OrderRelationProtocol extends CloudHospitalProtocol {

    @Resource
    private OrderRelationDao orderRelationDao;
    @Autowired
    private UserService userService;
    @Resource
    private DoctorDao doctorDao;
    @Resource
    private PatientDao patientDao;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {

        //查询session中用户信息
        Patient info = userService.getCurPatientInfo();
        if(info==null){
            throw new HospitalException("用户未登录！", CloudHospitalException.USER_ERROR);
        }
        String orderid = MapUtil.getString(params,"orderid");
        if(StringUtil.isBlank(orderid)){
            throw new HospitalException("请求参数orderid为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String doctorid = MapUtil.getString(params,"doctorid");
        if(StringUtil.isBlank(doctorid)){
            throw new HospitalException("请求参数doctorid为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String linkmanid = MapUtil.getString(params,"patientid");
        String patientid="";
        // 去数据库中查找联系人信息,传入真正的id而不是linkmanid
        List<ContactPeopleInfo> values = HospitalInfoService.getInstance().getContactsList(info.getAlipayUserId(), linkmanid);
        if(values!=null&&values.size()>0){
            patientid=values.get(0).getPatientid();
        }

        if(StringUtil.isBlank(linkmanid)){
            throw new HospitalException("请求参数linkmanid为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        if(StringUtil.isBlank(patientid)){
            throw new HospitalException("patientid不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String orderdate = MapUtil.getString(params,"orderdate");
        if(StringUtil.isBlank(orderdate)){
            throw new HospitalException("请求参数orderdate为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String shiftcode = MapUtil.getString(params,"shiftcode");
        if(StringUtil.isBlank(shiftcode)){
            throw new HospitalException("请求参数shiftcode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String ordertime = MapUtil.getString(params,"ordertime");
        if(StringUtil.isBlank(ordertime)){
            throw new HospitalException("请求参数ordertime为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String issame = MapUtil.getString(params, "issame");
        if(StringUtil.isBlank(issame)){
            throw new HospitalException("请求参数issame为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //给预约日期orderdate加上年份，醫院數據庫無年份，原orderdate：MM-dd
        Integer curMonth=Integer.parseInt(new SimpleDateFormat("MM").format(new Date()));
        Integer yyMonth=Integer.parseInt(orderdate.substring(0,2));
        Integer curYear=Integer.parseInt(new SimpleDateFormat("yyyy").format(new Date()));
        if(yyMonth<curMonth){//若跨年
            orderdate=(curYear+1)+"-"+orderdate;
        }else{//不跨年
            orderdate=curYear+"-"+orderdate;
        }
        OrderRelation orderRelation = new OrderRelation();
        orderRelation.setOrderid(orderid);
        orderRelation.setDoctorid(doctorid);
        orderRelation.setPatientid(patientid);                  //病人ID
        orderRelation.setOwnerpatientid(info.getPatientCode()); //本人ID
        orderRelation.setOrderdate(orderdate);
        orderRelation.setShiftcode(shiftcode);
        orderRelation.setOrdertime(ordertime);
        orderRelation.setIssame(issame);

        //跟上次开药不一样的，submittopicid不能为空
        if("1".equals(issame)){
            String submittopicid = MapUtil.getString(params,"submittopicid");
            if(StringUtil.isBlank(submittopicid)){
                throw new HospitalException("请求参数submittopicid为空！", CloudHospitalException.REQUEST_IS_EMPTY);
            }
            orderRelation.setSubmittopicid(submittopicid);
        }
        orderRelationDao.insert(orderRelation);
        Doctor doctor = doctorDao.doctorInfo(doctorid);
        if (doctor==null){
            doctor = new Doctor();
            doctor.setName(doctorid);
            doctor.setUserName(doctorid);
            doctor.setCode(doctorid);
            doctor.setPwd("000000");
            doctorDao.insert(doctor);
        }
        Patient patient=new Patient();
        patient.setAlipayUserId(info.getAlipayUserId());
        patient.setCurrentWay("Alipay");
        patient.setPatientCode(patientid);
        patient.setName(values.get(0).getName());
        patientDao.syncPatient(patient);
        return null;
    }

    @Override
    public String getProtocolCode() {
        return Content.ORDER_RELATION_CODE;
    }
}
