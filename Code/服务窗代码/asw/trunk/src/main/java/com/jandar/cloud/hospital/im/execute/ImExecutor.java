package com.jandar.cloud.hospital.im.execute;

import com.jandar.cloud.hospital.im.MsgModel;

/**
 * Created by flyhorse on 2016/11/2.
 *
 *
 */
public abstract  class ImExecutor {

    /**
     * 处理消息
     * args 要处理的消息
     * */
    public abstract MsgModel execute(String ... args);
}
