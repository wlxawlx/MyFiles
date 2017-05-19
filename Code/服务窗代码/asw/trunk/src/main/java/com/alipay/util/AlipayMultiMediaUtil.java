package com.alipay.util;

import com.alipay.api.*;
import com.alipay.constants.AlipayServiceEnvConstants;
import net.sf.json.JSONObject;

import java.io.File;
import java.io.FileOutputStream;
import java.io.OutputStream;

/**
 *
 * Created by zzw on 16/9/8.
 */
public class AlipayMultiMediaUtil {

    /**
     * 下载图片
     * @param mediaId
     */
    public static void downloadImage(String mediaId, String savePath) {
        //创建AlipayClient对象，参数分别为请求网关、开发者服务窗appid、开发者私钥、参数格式、字符编码（默认GBK）
        AlipayClient alipayClient = new AlipayMobilePublicMultiMediaClient(
                "https://openfile.alipay.com/chat/multimedia.do", AlipayServiceEnvConstants.APP_ID,
                AlipayServiceEnvConstants.PRIVATE_KEY, "json", AlipayServiceEnvConstants.CHARSET);

        //构建请求对象
        AlipayMobilePublicMultiMediaDownloadRequest request = new AlipayMobilePublicMultiMediaDownloadRequest();
        //图片输出流
        OutputStream output = null;
        File tmpImageFile = new File(savePath);
        try {
            output = new FileOutputStream(tmpImageFile);
            request.setOutputStream(output);
        } catch (Exception e) {
            e.printStackTrace();
        }

        //构建业务参数
        JSONObject bizContent = new JSONObject();
        bizContent.put("mediaId", mediaId);
        request.setBizContent(bizContent.toString());
        //构建返回对象
        AlipayMobilePublicMultiMediaDownloadResponse response = null;
        try {
            //调用接口
            response = alipayClient.execute(request);
        } catch (AlipayApiException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
