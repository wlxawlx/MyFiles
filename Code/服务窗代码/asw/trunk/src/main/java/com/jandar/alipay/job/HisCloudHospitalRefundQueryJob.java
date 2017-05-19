package com.jandar.alipay.job;

import com.alipay.config.AlipayConfig;
import com.alipay.util.AlipaySubmit;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.util.KitUtil;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.jdom.Element;
import org.jdom.input.SAXBuilder;
import org.springframework.stereotype.Service;
import org.xml.sax.InputSource;

import java.io.StringReader;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by ysc on 2017/1/19.
 * 通过后台返回的批次号进行订单状态更新操作
 */
@Service("hisCloudHospitalRefundQueryJob")
public class HisCloudHospitalRefundQueryJob {

    private final Log log = LogFactory.getLog(getClass());

    public void execute() {
        log.info("云医院HIS退费查询开始任务");
        Map<String, Object> parameters=new HashMap<String, Object>();
        List<Map<String, String>> result=null;
        try {
            result= HospitalInfoService.getInstance().commonService("FY030316",parameters);
        } catch (HospitalException e) {
            e.printStackTrace();
            return;
        }
        try {
            for (Map<String, String> map : result) {
                String batchNo=map.get("batchno");
                String hisStatus=map.get("hisstatus");
                log.info("云医院批次号:" + batchNo);
                log.info("HIS状态:" + hisStatus);
                if ("success".equals(hisStatus)) {
                    parameters.put("batchno",batchNo);
                    parameters.put("status","1");
                }else if("false".equals(hisStatus)){
                    parameters.put("batchno",batchNo);
                    parameters.put("status","-1");
                }else{
                    throw new HospitalException("获取HIS状态错误", HospitalException.UNARCHIV);
                }
                List<Map<String, String>> updateResult=HospitalInfoService.getInstance().commonService("FY030317",parameters);
                if(updateResult !=null && updateResult.size()>0){
                    log.info(batchNo+" 更新状态结果："+updateResult.get(0).get("msg"));
                }
            }
        }catch(HospitalException e){
            e.printStackTrace();
        }
        log.info("云医院HIS退费查询结束任务");

    }
}