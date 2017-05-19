package com.jandar.cloud.hospital.protocol;

import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.bean.ChronicIllnessTopic;
import com.jandar.cloud.hospital.bean.OrderRelation;
import com.jandar.cloud.hospital.bean.SubmitTopic;
import com.jandar.cloud.hospital.dao.ChronicIllnessTopicDao;
import com.jandar.cloud.hospital.dao.OrderRelationDao;
import com.jandar.cloud.hospital.dao.ReservationRecordDao;
import com.jandar.cloud.hospital.dao.SubmitTopicDao;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.util.StringUtil;
import org.bson.types.ObjectId;
import org.springframework.stereotype.Component;
import javax.annotation.Resource;
import java.util.*;

/**
 * 插件首页信息
 * 根据patientCode，doctorCode取当天的任意一条预约记录。
 */
@Component
public class PlugInHomePageProtocol extends CloudHospitalProtocol {
    @Resource
    private ReservationRecordDao reservationRecordDao;
    @Resource
    private SubmitTopicDao submitTopicDao;
    @Resource
    private ChronicIllnessTopicDao chronicIllnessTopicDao;
    @Resource
    private OrderRelationDao orderRelationDao;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        List<Object> list = new ArrayList<Object>();
        String patientCode = MapUtil.getString(params, "PatientCode");   //病人代码
        if (patientCode == null) {
            throw new HospitalException("请求参数PatientCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        String doctorCode = MapUtil.getString(params, "DoctorCode");    //医生代码
        if (doctorCode == null) {
            throw new HospitalException("请求参数DoctorCode为空！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        //ReservationRecord reservationRecord = reservationRecordDao.get(patientCode, doctorCode);//预约记录
        //预约记录
        //Map<String, String>  mapAppoint = HospitalInfoService.getInstance().getAppoint(doctorCode, patientCode);
        System.out.println("PlugInHomePageProtocol:start");
        OrderRelation orderRelation=orderRelationDao.findBydoctorIdAndPatientId(doctorCode, patientCode);
        if (orderRelation==null){
            throw new HospitalException("病人"+patientCode+"今天在您这没有预约！", CloudHospitalException.REQUEST_IS_EMPTY);
        }
        System.out.println("PlugInHomePageProtocol:end");
        String submitTopicId = orderRelation.getSubmittopicid();
        System.out.println("submitTopicId:"+submitTopicId);
        if(!StringUtil.isBlank(submitTopicId)){
            String[] answerFields = {"Code","Items"};
            SubmitTopic submitTopic = submitTopicDao.findOne (new ObjectId(submitTopicId), answerFields);          //问卷回答信息
            String[] questionFields = {"Items"};
            ChronicIllnessTopic chronicIllnessTopic = chronicIllnessTopicDao.findTopicInfo(submitTopic.getCode(), questionFields);//问题信息
            List<ChronicIllnessTopic.Items> questionItemsList = chronicIllnessTopic.getItems();   //问题列表
            List<SubmitTopic.Items> answerItemsList = submitTopic.getItems();                     //答案列表
            for (int i=0; i<questionItemsList.size(); i++){
                Map<String,Object> questionAndAnswer = new HashMap<String,Object>();
                questionAndAnswer.put("Question",questionItemsList.get(i).getTile());
                questionAndAnswer.put("Type",questionItemsList.get(i).getType());
                if(i<answerItemsList.size()){
                    List<String> answerAll=new ArrayList<String>();
                    if(answerItemsList.get(i).getAnswer()!=null)
                        answerAll.addAll(answerItemsList.get(i).getAnswer());
                    if(answerItemsList.get(i).getOtherAnswer()!=null)
                        answerAll.add(answerItemsList.get(i).getOtherAnswer());
                    questionAndAnswer.put("Answer", answerAll);
                }else{
                    questionAndAnswer.put("Answer", null);
                }
                list.add(questionAndAnswer);
            }
        }
        return  list;
    }

    @Override
    public String getProtocolCode() {
        return Content.PLUG_IN_HOME_PAGE_CODE;
    }

}
