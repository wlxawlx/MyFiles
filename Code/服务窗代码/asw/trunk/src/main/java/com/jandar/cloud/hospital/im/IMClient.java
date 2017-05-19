package com.jandar.cloud.hospital.im;

import cloud.hospital.cloudend.CloudEndConfigure;
import cloud.hospital.cloudend.CloudEndConnection;
import cloud.hospital.cloudend.message.base.AbsMessage;
import cloud.hospital.utils.EndType;
import com.jandar.config.ConfigHandler;

/**
 * IM 的客户端
 * Created by zzw on 16/9/8.
 */
public class IMClient {


    //发送即时通信用的sessionId
    private  long sessionId=0;

    private static IMClient ourInstance = new IMClient();

    public static IMClient getInstance() {
        return ourInstance;
    }

    private CloudEndConnection cloudEndConnection;

    private IMClient() {
        connect();
    }


    private  long getSessionId()
    {
          return sessionId++;
    }



    private void connect() {

        String host = ConfigHandler.getCloudHost();
        int port = ConfigHandler.getCloudPort();
        String username = ConfigHandler.getDispatchImUser();
        String password = ConfigHandler.getDispatchPassword();

        System.out.println("========IMClient====connect===username:"+username+" password:"+password);

        CloudEndConfigure.Builder builder = CloudEndConfigure.builder();
        builder.setHost(host);
        builder.setPort(port);
        builder.setEndType(EndType.PATIENT_DISPATCH);
        builder.setUsernameAndPassword(username, password);
//        builder.setFileDownloadPath(TestMain.class.getResource("/").getPath() + "/download");
        builder.setFileDownloadPath(ConfigHandler.getChatImageSavePath());
        CloudEndConfigure build = builder.build();
        CloudEndConnection.setOutputXml(true);
        cloudEndConnection = new CloudEndConnection(build);

        try {
            cloudEndConnection.connect();

        } catch (Exception e) {
            e.printStackTrace();
        }

        cloudEndConnection.setReceiveMessageListener(new MessageListener());
    }

    public void sendMessage(AbsMessage message, String doctorId) throws Exception {
        message.setSessionId(String.valueOf(getSessionId()));
        cloudEndConnection.sendMessage(message, doctorId);
    }
}
