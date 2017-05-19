package com.jandar.cloud.hospital.protocol;

import com.alipay.config.AlipayConfig;
import com.alipay.constants.AlipayServiceEnvConstants;
import com.alipay.payment.wap.AlipayWapPayment;
import com.jandar.alipay.ServiceContext;
import com.jandar.alipay.core.HospitalException;
import com.jandar.alipay.core.HospitalInfoService;
import com.jandar.alipay.core.struct.RechargeOrderType;
import com.jandar.alipay.hospital.UserInfo;
import com.jandar.alipay.job.AlipayTradeQueryJob;
import com.jandar.alipay.util.MapUtil;
import com.jandar.cloud.hospital.Content;
import com.jandar.cloud.hospital.MappingCode;
import com.jandar.cloud.hospital.exception.CloudHospitalException;
import com.jandar.cloud.hospital.service.UserService;
import com.jandar.util.StringUtil;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 通用方法
 * Created by ysc on 16/12/26.
 */
@Component
public class CommonProtocol extends CloudHospitalProtocol {

    @Autowired
    private UserService userService;

    @Override
    public Object doProcess(String pcode, Map<String, Object> params) throws HospitalException {
        UserInfo userInfo = ServiceContext.getHospitalUserInfo();
        if (userInfo == null) {
            throw new HospitalException("还未建档", HospitalException.UNARCHIV);
        }
        //MappingCode代码
        String mCode=(String)params.get("mcode");
        if (mCode == null || "".equals(mCode.trim())) {
            throw new HospitalException("mappingcode参数有误", HospitalException.UNARCHIV);
        }

        //根据MappingCode来调用相应的his接口
        if(MappingCode.CONTACT_PERSON_LIST_CODE.equals(mCode)){//获取联系人列表
            return getContactPersonList(userInfo);
        }else if(MappingCode.PRESCRIPTION_LIST_CODE.equals(mCode)){//获取缴费列表
            String jzId=(String)params.get("jzid");
            if (jzId == null || "".equals(jzId.trim())) {
                throw new HospitalException("就诊Id不能为空", HospitalException.UNARCHIV);
            }
            return getPerscriptionList(userInfo,jzId);
        }else if(MappingCode.RECEIVE_LIST_CODE.equals(mCode)){//获取收货人列表
            String shdzId=MapUtil.getString(params, "shdzid");
            if (StringUtils.isBlank(shdzId)) {
                shdzId="none";
            }
            return getReceiveList(userInfo.getAlipayUserId(),shdzId);
        }else if(MappingCode.PRE_SUBMIT_CODE.equals(mCode)){//预提交缴费
            String jzId=(String)params.get("jzid");
            if (jzId == null || "".equals(jzId.trim())) {
                throw new HospitalException("就诊Id不能为空", HospitalException.UNARCHIV);
            }
            return preSubmit(params);
        }else if(MappingCode.EXIST_PRESCRIPTION_CODE.equals(mCode)){//是否存在缴费或退费信息
            String brId=(String)params.get("brid");
            if (brId == null || "".equals(brId.trim())) {
                throw new HospitalException("病人Id不能为空", HospitalException.UNARCHIV);
            }
            String type=(String)params.get("type");
            if (type == null || "".equals(type.trim())) {
                throw new HospitalException("类型不能为空", HospitalException.UNARCHIV);
            }
            return existPrescription(brId,type);
        }else if(MappingCode.SUBMIT_ALIPAY_CODE.equals(mCode)){//确认缴费-支付宝
            String brId=MapUtil.getString(params, "brid");
            String brName = MapUtil.getString(params, "brname");
            String sfz = MapUtil.getString(params, "sfz");
            String kh = MapUtil.getString(params, "kh");
            String je = MapUtil.getString(params, "je");
            String successURL=MapUtil.getString(params, "successurl");
            String jzId=MapUtil.getString(params, "jzid");
            String shdzId=MapUtil.getString(params, "shdzid");
            String qjfs=MapUtil.getString(params, "qjfs");
            if(StringUtils.isBlank(brId)){
                throw new HospitalException("病人Id不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
            }
            if (StringUtils.isBlank(je)) {
                throw new HospitalException("充值金额不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
            }
            if (StringUtils.isBlank(successURL)) {
                throw new HospitalException("支付宝成功后跳转地址不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
            }
            if (StringUtils.isBlank(jzId)) {
                throw new HospitalException("就诊Id不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
            }
            if (StringUtils.isBlank(qjfs)) {
                throw new HospitalException("取件方式不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
            }
            if("1".equals(qjfs)){//1表示快递
                if (StringUtils.isBlank(shdzId)) {
                    throw new HospitalException("收货地址Id不能为空！", CloudHospitalException.REQUEST_IS_EMPTY);
                }
            }
            return submitAlipay(userInfo.getAlipayUserId(),brId,brName,sfz,kh,je,successURL,jzId,shdzId,qjfs);
        }else if(MappingCode.SUBMIT_HIS_CODE.equals(mCode)) {//确认缴费-His
            String orderNo=MapUtil.getString(params, "orderno");
            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("订单号不能为空", HospitalException.UNARCHIV);
            }
            return submitHis(orderNo);
        }else if(MappingCode.KJF_OR_KTF_LIST_CODE.equals(mCode)) {//可缴费或可退费列表
            String brId=MapUtil.getString(params, "brid");
            String type=MapUtil.getString(params, "type");
            if (StringUtils.isBlank(brId)) {
                throw new HospitalException("病人Id不能为空", HospitalException.UNARCHIV);
            }
            if (StringUtils.isBlank(type)) {
                throw new HospitalException("类型不能为空", HospitalException.UNARCHIV);
            }
            return getJfOrTjList(brId,type);
        }else if(MappingCode.UPDATE_ORDER_STATUS_CODE.equals(mCode)) {//更新订单状态
            String orderNo=MapUtil.getString(params, "orderno");
            String status=MapUtil.getString(params, "status");
            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("订单号不能为空", HospitalException.UNARCHIV);
            }
            if (StringUtils.isBlank(status)) {
                throw new HospitalException("订单状态不能为空", HospitalException.UNARCHIV);
            }
            return updateOrderStatus(orderNo, status);
        }else if(MappingCode.REFUND_ALIPAY_CODE.equals(mCode)) {//支付宝退款
            String totalFee=MapUtil.getString(params, "totalfee");
            String tradeNo=MapUtil.getString(params, "tradeno");
            String orderNo=MapUtil.getString(params, "orderno");
            String openId=userInfo.getAlipayUserId();
            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("订单号不能为空", HospitalException.UNARCHIV);
            }
            if (StringUtils.isBlank(tradeNo)) {
                throw new HospitalException("支付宝交易号不能为空", HospitalException.UNARCHIV);
            }
            if (StringUtils.isBlank(totalFee)) {
                throw new HospitalException("交易号不能为空", HospitalException.UNARCHIV);
            }
            return refundAlipay(totalFee,tradeNo,orderNo,openId);
        }else if(MappingCode.PATIENT_INFO_CODE.equals(mCode)) {//获取缴费病人信息
            String orderNo=MapUtil.getString(params, "orderno");
            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("订单号不能为空", HospitalException.UNARCHIV);
            }
            return getPatientInfo(orderNo);
        }else if(MappingCode.TRADE_QUERY_CODE.equals(mCode)) {//支付宝查询测试
            try {
                return AlipayTradeQueryJob.singleTradeQuery(AlipayServiceEnvConstants.PARTNER,MapUtil.getString(params, "tradeno"),MapUtil.getString(params, "orderno"));
            } catch (Exception e) {
                e.printStackTrace();
            }
            return null;
        }else if(MappingCode.REFUND_LIST_CODE.equals(mCode)) {//退费清单
            String orderNo=(String)params.get("orderno");
            if (orderNo == null || "".equals(orderNo.trim())) {
                throw new HospitalException("医院订单号不能为空", HospitalException.UNARCHIV);
            }
            return getRefundList(orderNo);
        }else if(MappingCode.SUBMIT_REFUND_CODE.equals(mCode)) {//确认退费
            String orderNo=MapUtil.getString(params, "orderno");

            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("医院订单号不能为空", HospitalException.UNARCHIV);
            }

            return submitRefund(orderNo,userInfo.getAlipayUserId());
        }else if(MappingCode.SFJL_LIST_CODE.equals(mCode)) {//收费记录列表
            String brId=MapUtil.getString(params, "brid");

            if (StringUtils.isBlank(brId)) {
                throw new HospitalException("病人id不能为空", HospitalException.UNARCHIV);
            }
            return getSfjlList(brId,userInfo.getAlipayUserId());
        }else if(MappingCode.PRESCRIPTION_LIST_SEARCH_CODE.equals(mCode)) {//查询模块-获取处方明细
            String orderNo=MapUtil.getString(params, "orderno");

            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("医院订单号不能为空", HospitalException.UNARCHIV);
            }
            return getPrescriptionListInSearch(orderNo);
        }else if(MappingCode.INSERT_OR_UPDATE_RECEIVER_CODE.equals(mCode)) {//插入或更新收货人信息
            String type=MapUtil.getString(params, "type");
            if (StringUtils.isBlank(type)) {
                throw new HospitalException("操作类型不能为空", HospitalException.UNARCHIV);
            }
            if("update".equals(type.trim())){
                String shdzid=MapUtil.getString(params, "shdzid");
                if (StringUtils.isBlank(shdzid)) {
                    throw new HospitalException("更新收货人信息失败", HospitalException.UNARCHIV);
                }
            }else{
                params.put("openid",userInfo.getAlipayUserId());
            }
            return insertOrUpdateReceiver(params);
        }else if(MappingCode.DELETE_RECEIVER_CODE.equals(mCode)) {//删除收货人信息
            String shdzid=MapUtil.getString(params, "shdzid");
            if (StringUtils.isBlank(shdzid)) {
                throw new HospitalException("收货地址id不能为空", HospitalException.UNARCHIV);
            }
            return deleteReceiver(params);
        }else if(MappingCode.CFD_LIST_CODE.equals(mCode)) {//获取处方单列表
            String brId=MapUtil.getString(params, "brid");
            if (StringUtils.isBlank(brId)) {
                throw new HospitalException("病人id不能为空", HospitalException.UNARCHIV);
            }
            return getCfdList(brId);
        }else if(MappingCode.CFD_DETAIL_CODE.equals(mCode)) {//获取处方单详情
            String cflsh=MapUtil.getString(params, "cflsh");
            if (StringUtils.isBlank(cflsh)) {
                throw new HospitalException("处方流水号不能为空", HospitalException.UNARCHIV);
            }
            return getCfdDetail(cflsh);
        }else if(MappingCode.JYJCD_LIST_CODE.equals(mCode)) {//查询模块-检验单或检查单列表
            String brId=MapUtil.getString(params, "brid");
            if (StringUtils.isBlank(brId)) {
                throw new HospitalException("病人id不能为空", HospitalException.UNARCHIV);
            }
            String type=MapUtil.getString(params, "type");
            if (StringUtils.isBlank(type)) {
                throw new HospitalException("查询类型不能为空", HospitalException.UNARCHIV);
            }
            return getJydList(brId,type);
        }else if(MappingCode.JYD_DETAIL_CODE.equals(mCode)) {//查询模块-检验单详情
            String jydlsh=MapUtil.getString(params, "jydlsh");
            if (StringUtils.isBlank(jydlsh)) {
                throw new HospitalException("检验单流水号不能为空", HospitalException.UNARCHIV);
            }
            return getJydDetail(jydlsh);
        }else if(MappingCode.JCD_DETAIL_CODE.equals(mCode)) {//查询模块-检查单详情
            String jcdlsh=MapUtil.getString(params, "jcdlsh");
            if (StringUtils.isBlank(jcdlsh)) {
                throw new HospitalException("检验单流水号不能为空", HospitalException.UNARCHIV);
            }
            return getJcdDetail(jcdlsh);
        }else if(MappingCode.JUDGE_REFUND_CODE.equals(mCode)) {//判断是否可退费
            String orderNo=MapUtil.getString(params, "orderno");
            if (StringUtils.isBlank(orderNo)) {
                throw new HospitalException("订单号不能为空", HospitalException.UNARCHIV);
            }
            return judgeRefund(orderNo);
        }else{
            return new HashMap<String, Object>();
        }

    }
    //判断是否可退费
    private Object judgeRefund(String orderNo) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        List<Map<String, String>> list= HospitalInfoService.getInstance().commonService("FY030330",parameters);
        if(list != null && list.size()>0){
            return list.get(0);
        }else{
            throw new HospitalException("判断可退费出错", HospitalException.ERROR);
        }
    }
    //查询模块-检查单详情
    private Object getJcdDetail(String jcdlsh) throws HospitalException{
        Map<String, Object> result=new HashMap<String, Object>();
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("jcdlsh",jcdlsh);
        List<Map<String, String>> list= HospitalInfoService.getInstance().commonService("FY030328",parameters);
        if(list != null && list.size()>0){
            result.put("profile",list.get(0));
        }
        list= HospitalInfoService.getInstance().commonService("FY030329",parameters);
        if(list != null && list.size()>0){
            result.put("jcxmlb",list);
        }
        return result;
    }
    //查询模块-检验单详情
    private Object getJydDetail(String jydlsh) throws HospitalException{
        Map<String, Object> result=new HashMap<String, Object>();
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("jydlsh",jydlsh);
        List<Map<String, String>> list= HospitalInfoService.getInstance().commonService("FY030326",parameters);
        if(list != null && list.size()>0){
            result.put("profile",list.get(0));
        }
        list= HospitalInfoService.getInstance().commonService("FY030327",parameters);
        if(list != null && list.size()>0){
            result.put("jyxmlb",list);
        }
        return result;
    }
    //查询模块-检验单或检查单列表
    private Object getJydList(String brId,String type) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("brid",brId);
        parameters.put("type",type);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030325",parameters);
        return result;
    }
    //获取处方单详情
    private Object getCfdDetail(String cflsh) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("cflsh",cflsh);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030324",parameters);
        return result;
    }
    //获取处方单列表
    private Object getCfdList(String brId) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("brid",brId);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030323",parameters);
        return result;
    }
    //删除收货人信息
    private Object deleteReceiver(Map<String, Object> params) throws HospitalException{
        Map<String,Object> result=new HashMap<String, Object>();
        List<Map<String, String>> operateResult= HospitalInfoService.getInstance().commonService("FY030322",params);
        if(operateResult !=null && operateResult.size()>0){
            result.put("msg",operateResult.get(0).get("msg"));
        }else{
            result.put("msg","fail");
        }
        return result;
    }
    //插入或更新收货人信息
    private Object insertOrUpdateReceiver(Map<String, Object> params) throws HospitalException{
        Map<String,Object> result=new HashMap<String, Object>();
        List<Map<String, String>> operateResult= HospitalInfoService.getInstance().commonService("FY030321",params);
        if(operateResult !=null && operateResult.size()>0){
            result.put("msg",operateResult.get(0).get("msg"));
        }else{
            result.put("msg","fail");
        }
        return result;
    }
    //查询模块-获取处方明细
    private Object getPrescriptionListInSearch(String orderNo) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String,Object> mapValues=new HashMap<String, Object>();
        parameters.put("orderno",orderNo);

        //处方单列表
        List<Map<String, Object>> prescriptionList=new ArrayList<Map<String, Object>>();
        //处方单流水
        List<Map<String, String>> listValues= HospitalInfoService.getInstance().commonService("FY030331",parameters);
        if(listValues !=null && listValues.size()>0){
            for(Map<String, String> value:listValues){
                Map<String,Object> mapDetail=new HashMap<String, Object>();
                String fpxmid=value.get("fpxmid");
                parameters=new HashMap<String, Object>();
                parameters.put("fpxmid",fpxmid);
                parameters.put("orderno",orderNo);
                //处方单信息
                List<Map<String, String>> listDetail= HospitalInfoService.getInstance().commonService("FY030332",parameters);
                if(listDetail !=null && listDetail.size()>0){
                    mapDetail.putAll(value);
                    mapDetail.put("listDetail",listDetail);
                    prescriptionList.add(mapDetail);
                }
            }
        }
        //处方单赋值
        if(prescriptionList !=null && prescriptionList.size()>0){
            mapValues.put("fpxmlist",prescriptionList);
        }
        return mapValues;
    }
    //收费记录列表
    private Object getSfjlList(String brId,String openId) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        System.out.println("getSfjlList:brid:"+brId);
        parameters.put("brid",brId);
        parameters.put("openid",openId);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030320",parameters);
        return result;
    }
    //获取缴费病人信息
    private Object getPatientInfo(String orderNo) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String, String> finalResult=new HashMap<>();
        parameters.put("orderno",orderNo);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030309",parameters);
        if(result == null || result.size()==0){
            finalResult.put("msg","fail");
            return finalResult;
        }else{
            return result.get(0);
        }
    }
    //支付宝退款
    private Object refundAlipay(String totalFee,String tradeNo,String orderNo,String openId) throws HospitalException{
        Map<String, String> result = new HashMap<>();
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        parameters.put("paytype","云医院支付宝门诊退费");
        List<Map<String, String>> batchNoList= HospitalInfoService.getInstance().commonService("PO010102",parameters);
        if(batchNoList.size()>0){
             AlipayWapPayment.getWapRefundUrl(
                    openId,
                    tradeNo==null?batchNoList.get(0).get("tradeno"):tradeNo,
                    totalFee,
                    "交易失败退款",
                    batchNoList.get(0).get("batchno")
            );
            result.put("msg", "success");
            return result;
        }else{
            throw new HospitalException("获取批次号为空", HospitalException.UNARCHIV);
        }

    }
    //更新订单状态
    private Object updateOrderStatus(String orderNo,String status) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String, String> finalResult=new HashMap<>();
        parameters.put("orderno",orderNo);
        parameters.put("status",status);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030308",parameters);
        if(result == null || result.size()==0){
            finalResult.put("msg","fail");
            return finalResult;
        }else{
            return result.get(0);
        }

    }
    //可缴费或可退费列表
    private Object getJfOrTjList(String brId,String type) throws HospitalException{
        List<Map<String, String>> result=new ArrayList<Map<String, String>>();
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("brid",brId);
        if("jf".equals(type)){
            result= HospitalInfoService.getInstance().commonService("FY030307",parameters);
        }else{
            result= HospitalInfoService.getInstance().commonService("FY030311",parameters);
        }

        return result;
    }
    //确认缴费-His
    private Object submitHis(String orderNo) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        parameters.put("status","1");
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030308",parameters);
        if(result != null && result.size()>0 && "success".equals(result.get(0).get("msg"))){
            parameters=new HashMap<String, Object>();
            parameters.put("orderno",orderNo);
            result= HospitalInfoService.getInstance().commonService("FY030306",parameters);
            if(result==null || result.size()==0){
                Map<String, String> msg=new HashMap<String, String>();
                msg.put("msg","fail");
                return msg;
            }
            return result.get(0);
        }else{
            Map<String, String> msg=new HashMap<String, String>();
            msg.put("msg","fail");
            return msg;
        }

    }
    //确认缴费-支付宝
    private Object submitAlipay(String openId,String brId,String brName,String sfz,String kh,String je,String successURL,String jzId,String shdzId,String qjfs) throws HospitalException{
        String orderName="云医院支付宝门诊结算";
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("openid",openId);
        parameters.put("patientname",brName);
        parameters.put("patientcardno",sfz);
        parameters.put("cardno",kh);
        parameters.put("patientid",brId);
        parameters.put("subject",orderName);
        parameters.put("money",je);
        parameters.put("sourcetype","2");
        List<Map<String, String>> orderNoResult= HospitalInfoService.getInstance().commonService("PO010101",parameters);
        if (orderNoResult != null && orderNoResult.size()>0) {
            Map<String, String> result = new HashMap<String, String>();
            String aliwappayurl = AlipayWapPayment.getWapPaymentUrl(RechargeOrderType.OutpatientOrder,
                    openId,
                    brName,
                    orderNoResult.get(0).get("tradeno"),
                    orderName,
                    je,
                    null,
                    successURL,
                    "",
                    "",
                    "",
                    brId,
                    jzId,
                    shdzId,
                    qjfs);
            result.put("aliwappayurl", aliwappayurl);
            System.out.println("aliwappayurl:"+aliwappayurl);
            return result;
        } else {
            throw new HospitalException("门诊结算——创建订单失败");
        }
    }
    //获取联系人列表
    private Object getContactPersonList(UserInfo userInfo) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("openid",userInfo.getAlipayUserId());
        parameters.put("usertype","1");
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030101",parameters);
        return result;
    }
    //确认退费
    private Object submitRefund(String orderNo,String openId) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        parameters.put("paytype","云医院支付宝门诊退费");
        Map<String, Object> result=new HashMap<String, Object>();
        List<Map<String, String>> batchNoList= HospitalInfoService.getInstance().commonService("PO010102",parameters);
        if(batchNoList != null && batchNoList.size()>0){
            parameters=new HashMap<String, Object>();
            parameters.put("orderno",orderNo);
            List<Map<String, String>> hisResult= HospitalInfoService.getInstance().commonService("FY030305",parameters);
            if(hisResult!=null && hisResult.size()>0){
                String hisMsg=hisResult.get(0).get("msg");
                String zffs=hisResult.get(0).get("zffs");
                String xzfje=hisResult.get(0).get("xzfje");

                if (StringUtils.isBlank(xzfje)) {
                    throw new HospitalException("需支付金额不能为空", HospitalException.UNARCHIV);
                }

                if("success".equals(hisMsg)){
                    AlipayWapPayment.getWapRefundUrl(
                            openId,
                            batchNoList.get(0).get("tradeno"),
                            xzfje,
                            "交易失败退款",
                            batchNoList.get(0).get("batchno")
                    );
                    Map<String, String> alipayResult=new HashMap<String, String>();
                    alipayResult.put("msg", "success");
                    if(alipayResult !=null && alipayResult.get("msg")!=null){
                        if("success".equals(alipayResult.get("msg"))) {
                            result.put("msg","success");
                        }else{
                            result.put("msg","fail");
                        }
                    }else{
                        result.put("msg","fail");
                    }
                }else{
                    result.put("msg","fail");
                }
            }else{
                result.put("msg","fail");
            }
        }

        return result;
    }
    //获取退费清单
    private Object getRefundList(String orderNo) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String,Object> mapValues=new HashMap<String, Object>();
        parameters.put("id",orderNo);
        parameters.put("type","tf");
        //病人总览信息
        List<Map<String, String>> listValues= HospitalInfoService.getInstance().commonService("FY030102",parameters);
        if (listValues == null || listValues.size() == 0) {
            throw new HospitalException("无法获取病人信息", HospitalException.ERROR);
        }
        mapValues.put("profile",listValues.get(0));

        //处方单列表
        List<Map<String, Object>> prescriptionList=new ArrayList<Map<String, Object>>();
        //处方单流水
        parameters=new HashMap<String, Object>();
        parameters.put("orderno",orderNo);
        listValues= HospitalInfoService.getInstance().commonService("FY030312",parameters);
        if(listValues !=null && listValues.size()>0){
            for(Map<String, String> value:listValues){
                Map<String,Object> mapDetail=new HashMap<String, Object>();
                String fpxmId=value.get("fpxmid");
                parameters=new HashMap<String, Object>();
                parameters.put("fpxmid",fpxmId);
                parameters.put("orderno",orderNo);
                //处方单信息
                List<Map<String, String>> listDetail= HospitalInfoService.getInstance().commonService("FY030313",parameters);
                if(listDetail !=null && listDetail.size()>0){
                    mapDetail.putAll(value);
                    mapDetail.put("listDetail",listDetail);
                    prescriptionList.add(mapDetail);
                }
            }
        }
        //处方单赋值
        if(prescriptionList !=null && prescriptionList.size()>0){
            mapValues.put("fpxmlist",prescriptionList);
        }
        return mapValues;
    }
    //获取缴费列表
    private Object getPerscriptionList(UserInfo userInfo,String jzId) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String,Object> mapValues=new HashMap<String, Object>();
        parameters.put("id",jzId);
        parameters.put("type","jf");
        //病人总览信息
        List<Map<String, String>> listValues= HospitalInfoService.getInstance().commonService("FY030102",parameters);
        if (listValues == null || listValues.size() == 0) {
            throw new HospitalException("无法获取病人信息", HospitalException.ERROR);
        }
        mapValues.put("profile",listValues.get(0));
        //处方单列表
        parameters=new HashMap<String, Object>();
        parameters.put("jzid",jzId);

        List<Map<String, Object>> prescriptionList=new ArrayList<Map<String, Object>>();
        //处方单流水
        listValues= HospitalInfoService.getInstance().commonService("FY030103",parameters);
        if(listValues !=null && listValues.size()>0){
            for(Map<String, String> value:listValues){
                Map<String,Object> mapDetail=new HashMap<String, Object>();
                String fpxmid=value.get("fpxmid");
                parameters=new HashMap<String, Object>();
                parameters.put("fpxmid",fpxmid);
                parameters.put("jzid",jzId);
                //处方单信息
                List<Map<String, String>> listDetail= HospitalInfoService.getInstance().commonService("FY030201",parameters);
                if(listDetail !=null && listDetail.size()>0){
                    mapDetail.putAll(value);
                    mapDetail.put("listDetail",listDetail);
                    prescriptionList.add(mapDetail);
                }
            }
        }
        //处方单赋值
        if(prescriptionList !=null && prescriptionList.size()>0){
            mapValues.put("fpxmlist",prescriptionList);
            //默认收货人信息
            parameters=new HashMap<String, Object>();
            parameters.put("openid",userInfo.getAlipayUserId());
            listValues= HospitalInfoService.getInstance().commonService("FY030301",parameters);
            if(listValues !=null && listValues.size()>0){
                mapValues.put("receivelist",listValues);
            }
        }
        return mapValues;
    }
    //获取收货人列表
    private Object getReceiveList(String openId,String shdzId) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("openid",openId);
        parameters.put("shdzid",shdzId);
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030302",parameters);
        return result;
    }
    //预提交缴费
    private Object preSubmit(Map<String, Object> params) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        parameters.put("jzid",params.get("jzid"));
        List<Map<String, String>> result= HospitalInfoService.getInstance().commonService("FY030303",parameters);
        if(result !=null && result.size()>0){
            return result.get(0);
        }else{
            return new HashMap<String, Object>();
        }

    }
    //是否存在缴费或退费信息
    private Object existPrescription(String brId,String type) throws HospitalException{
        Map<String, Object> parameters=new HashMap<String, Object>();
        Map<String, Object> result=new HashMap<String, Object>();
        parameters.put("brid",brId);
        //检查单信息
        List<Map<String, String>> listValues= null;
        if("jf".equals(type)){
            listValues=HospitalInfoService.getInstance().commonService("FY030307",parameters);
        }else{
            listValues=HospitalInfoService.getInstance().commonService("FY030311",parameters);
        }

        if(listValues==null || listValues.size()==0){
            result.put("flag","no");
        }else{
            result.put("flag","yes");
        }
        return result;
    }

    @Override
    public String getProtocolCode() {
        return Content.COMMON_PROTOCOL_CODE;
    }
}
