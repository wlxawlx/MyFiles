package com.jandar.handle.protocol;

import java.util.Map;

/**
 * Created by zzw on 16/8/31.
 */
public abstract class ProtocolMapping {

    private static final ProtocolMapping DEFAULT_PROTOCOL_MAPPING = new DefaultProtocolMapping();

    public static ProtocolMapping getInstance() {
        return DEFAULT_PROTOCOL_MAPPING;
    }

    protected ProtocolMapping() {

    }

    public abstract Protocol getHandler(String pcode, Map<String, Object> params);
}
