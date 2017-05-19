/**
 *
 */
package com.jandar.handle.protocol;

import com.jandar.alipay.core.HospitalException;

import java.util.Map;

/**
 * @author Administrator
 */
public interface Protocol {

    Object process(String pcode, Map<String, Object> params) throws HospitalException;

}
