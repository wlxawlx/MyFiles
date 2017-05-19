/**
 * 
 */
package com.chinamobile.openmas.tools;

/**
 * @author OpenMas
 *
 */
public class MmsContentType {
	private String strPrimaryType;
	private String strSubType;
    /**
     * ���췽��
     *
     */
    public MmsContentType()
    {
    	strPrimaryType = "";
        strSubType = "";
    }
    /**
     * ���췽��
     * @param type ����
     */
    public MmsContentType(String type) 
    {
    	strPrimaryType = "";
        strSubType = "";
        int index = type.indexOf("/");
        if(index>0)
        {
          String strPraType = type.substring(0,index);
          String strSubType = type.substring(index+1);
          setPrimaryType(strPraType);
          if(strSubType.indexOf(";")>0)
            strSubType = strSubType.substring(0,strSubType.indexOf(";"));
          setSubType(strSubType);
        }
        else
            System.err.println("�����Ͳ��Ǳ�׼���ͣ�type=" + type);
    }
    /**
     * 
     * @return ���������
     */
    public String getPrimaryType()
    {
      return(this.strPrimaryType);
    }
    /**
     * 
     * @param primaryType ����������
     */
    public void setPrimaryType(String primaryType)
    {
      this.strPrimaryType = primaryType;
    }
    /**
     * 
     * @param subType ����������
     */
    public void setSubType(String subType)
    {
      this.strSubType = subType;
    }
    /**
     * 
     * @return  ���������
     */
    public String getSubType()
    {
      return(this.strSubType);
    }
    
    /**
     * �Ƚ����������������Ƿ�һ��
     * @param type 
     * @return true or false
     */
    public boolean match(MmsContentType type)
    {
      String strPrimaryType = type.getPrimaryType().trim();
      String strSubType = type.getSubType().trim();
      if(strPrimaryType.equals(strSubType))
        return true;
      else
        return false;
    }
    /**
     * �Ƚ����������������Ƿ�һ��
     * @param type
     * @return true or false
     */
    public boolean match(String type)
    {
      int index = type.indexOf("/");
      if(index>0)
      {
        String strParType = type.substring(0,index);
        String strSubType = type.substring(index+1);
        if(strParType.trim().equals(strPrimaryType))
        {
          if(strSubType.trim().equals(strSubType))
            return true;
          else
            return false;
        }
        else
          return false;
      }
      else{
        System.err.println("�����Ͳ��Ǳ�׼���ͣ�type="+type);
        return (false);
      }
    }
    /**
     * ���ض�����ı���ʾ
     */
    public String toString()
    {
        return strPrimaryType + "/" + strSubType;
    }
}
