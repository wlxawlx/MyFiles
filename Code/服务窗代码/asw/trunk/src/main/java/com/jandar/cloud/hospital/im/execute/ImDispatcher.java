package com.jandar.cloud.hospital.im.execute;

import com.jandar.util.SpringBeanUtil;

/**
 * Created by flyhorse on 2016/11/2.
 */
public class ImDispatcher {



    /**
     * 发送检查单通知
     * */
    public static final String INSPECTION_NOTIFY_CODE="JCD";

    /**
     * 处方单通知
     * */
    public static final String PRESCRIBE_NOTIFY_CODE="CFD";

    /**
     * 住院单通知
     * */
    public static final String INHOSPITAL_NOTIFY_CODE="ZYD";

    /**
     * 医生呼叫病人
     * */
    public static final String CALL_PATIENT_CODE="HJ";

    /**
     * 缴费通知
     * */
    public static final String PAYMENT_NOTIFY_CODE="JF";



    //因为有 dao要注入，所以这里改成spring环境依赖
    private static ImExecutor chatToPatientExecutor= (ImExecutor)SpringBeanUtil.getBean("chatToPatientExecutor");
    private static ImExecutor inspectionNotifyExecutor= (ImExecutor)SpringBeanUtil.getBean("inspectionNotifyExecutor");
    private static ImExecutor callPatientExecutor= (ImExecutor)SpringBeanUtil.getBean("callPatientExecutor");

    private static ImExecutor inhospitalNotifyExecutor= (ImExecutor)SpringBeanUtil.getBean("inhospitalNotifyExecutor");
    private static ImExecutor prescribeNotifyExecutor= (ImExecutor)SpringBeanUtil.getBean("prescribeNotifyExecutor");
    private static ImExecutor paymentNotifyExecutor = (ImExecutor)SpringBeanUtil.getBean("paymentNotifyExecutor");

    public static ImExecutor getChatToPatientExecutor()
    {
        return chatToPatientExecutor;
    }

    public static ImExecutor getCallPatientExecutor()
    {
        return callPatientExecutor;
    }

    public static ImExecutor getPaymentNotifyExecutor(){
        return paymentNotifyExecutor;
    }

    /**
     * 取得合适的处理器
     *
     * null 表示没有合适的处理器，
     * */
    public static  ImExecutor getImExecutor(String imCode)
    {

          System.out.println("===================="+chatToPatientExecutor.getClass());


          if(INSPECTION_NOTIFY_CODE.equals(imCode))
          {
              return inspectionNotifyExecutor;
          }
          else if(CALL_PATIENT_CODE.equals(imCode))
          {
              return callPatientExecutor;
          }
          else if(INHOSPITAL_NOTIFY_CODE.equals(imCode))
          {
              return inhospitalNotifyExecutor;
          }
          else if(PRESCRIBE_NOTIFY_CODE.equals(imCode))
          {
              return prescribeNotifyExecutor;
          }else if(PAYMENT_NOTIFY_CODE.equals(imCode)){
              return paymentNotifyExecutor;
          }
          else
          {
              return null;
          }


    }


}
