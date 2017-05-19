/**
 * 
 */
package com.chinamobile.openmas.tools;

import java.io.*;
/**
 * @author OpenMas
 *
 */
public class MmsContent 
{
	private MmsContentType ContentType;
    private String ContentID;
    private String ContentLocation;
    private String Charset;
    private ByteArrayOutputStream byteOutput;
    
    private MmsContent()
    {
    	ContentType = new MmsContentType();
        ContentID = "";
        ContentLocation = "";
        Charset = "gb2312";
        byteOutput = new ByteArrayOutputStream();
    }
    /**
     * 内部构造方法
     * @param content
     */
    private MmsContent(byte[] content)
    {
    	ContentID = "";
        ContentLocation = "";
        Charset = "gb2312";
    	byteOutput = new ByteArrayOutputStream();
        try
        {
            byteOutput.write(content);
        }catch(IOException e)
        {
            System.err.println(e);
        }
    }
	/**
	 * @return charset
	 */
	public String getCharset() {
		return Charset;
	}
	/**
	 * @param charset 要设置的 编码
	 */
	public void setCharset(String charset) {
		Charset = charset;
	}
	/**
	 * @return contentID
	 */
	public String getContentID() {
		return ContentID;
	}
	/**
	 * @param contentID 要设置的 contentID
	 */
	public void setContentID(String contentID) {
		ContentID = contentID;
	}
	/**
	 * @return contentLocation
	 */
	public String getContentLocation() {
		return ContentLocation;
	}
	/**
	 * @param contentLocation 要设置的 contentLocation
	 */
	public void setContentLocation(String contentLocation) {
		ContentLocation = contentLocation;
	}
	/**
	 * @return contentType
	 */
	public MmsContentType getContentType() {
		return ContentType;
	}
	/**
	 * @param contentType 要设置的 contentType
	 */
	public void setContentType(MmsContentType contentType) {
		ContentType = contentType;
	}
	/**
	 * 返回媒体内容的大小
	 * @return 消息大小
	 */
	public int getSize()
	{
		return(byteOutput.toByteArray().length);
	}
	/**
	 * 以二进制方式获得的内容
	 * @return  消息内容
	 */
	public byte[] getContent()
	{
		return(byteOutput.toByteArray());
	}
	/**
	 * 
	 * @param content
	 */
	public void setContent(byte[] content)
	{
		byteOutput = new ByteArrayOutputStream();
        try
        {
            byteOutput.write(content);
        }catch(IOException e)
        {
            System.err.println(e);
        }
	}
	/**
	 * 以String方式获得内容
	 * @return 消息内容
	 */
	public String getContentAsString()
	{
		String charset = getCharset();
	    if(charset == null || charset.equals(""))
	      charset = "UTF-8";
	    try
	    {
	      return (byteOutput.toString(charset));
	    }catch(IOException ioe)
	    {
	      System.err.println(ioe);
	      return null;
	    }
	}
	public String toString()
    {
		StringBuffer sb = new StringBuffer();
        sb.append("ContentType=" + ContentType+"\n");
        sb.append("ContentID=" + ContentID+"\n");
        sb.append("ContentLocation=" + ContentLocation+"\n");
        sb.append("Charset=" + Charset+"\n");
        sb.append("byteOutput=" + getContentAsString()+"\n");
        return sb.toString();

    }
	/**
	 * 通过输入byte[]类型建立MmsContent
	 * @param data
	 * @return MMContent 对象
	 */
	public static MmsContent CreateFromBytes(byte[] data)
    {
        MmsContent mmsContent = new MmsContent(data);
        return (mmsContent);
    }
	/**
	 * 通过输入文件的绝对路径建立MmsContent
	 * @param fileName
	 * @return MMContent 对象
	 */
	public static MmsContent CreateFromFile(String fileName)
	{
		try{
		      DataInputStream input = new DataInputStream(new FileInputStream(fileName));
		      ByteArrayOutputStream output = new ByteArrayOutputStream();
		      byte[] data = null;
		      while (input.available() != 0) {
		        output.write(input.readByte());
		      }
		      data = output.toByteArray();
		      MmsContent mmsContent = new MmsContent(data);
		      String name = new File(fileName).getName();
		      String fileExtension = "";
		      int i = name.lastIndexOf('.');
		      if(i >-1 && i < (name.length() - 1))
		      {
		    	  fileExtension = name.substring(i+1);
		      }
		      if (fileExtension.equalsIgnoreCase("GIF"))
                  mmsContent.setContentType(MmsConst.GIF);
              else if (fileExtension.equalsIgnoreCase("AMR"))
                  mmsContent.setContentType(MmsConst.AMR);
              else if (fileExtension.equalsIgnoreCase("JPEG"))
                  mmsContent.setContentType(MmsConst.JPEG);
              else if (fileExtension.equalsIgnoreCase("JPG"))
                  mmsContent.setContentType(MmsConst.JPEG);
              else if (fileExtension.equalsIgnoreCase("MID"))
                  mmsContent.setContentType(MmsConst.MIDI);
              else if (fileExtension.equalsIgnoreCase("MIDI"))
                  mmsContent.setContentType(MmsConst.MIDI);
              else if (fileExtension.equalsIgnoreCase("PNG"))
                  mmsContent.setContentType(MmsConst.PNG);
              else if (fileExtension.equalsIgnoreCase("SMIL"))
                  mmsContent.setContentType(MmsConst.SMIL);
              else if (fileExtension.equalsIgnoreCase("TXT"))
                  mmsContent.setContentType(MmsConst.TEXT);
              else if (fileExtension.equalsIgnoreCase("TEXT"))
                  mmsContent.setContentType(MmsConst.TEXT);
              else if (fileExtension.equalsIgnoreCase("WBMP"))
                  mmsContent.setContentType(MmsConst.WBMP);
		      mmsContent.setContentID(name);
		      return (mmsContent);
		    }catch(IOException ioe){
		      System.err.println(ioe);
		      return null;
		    }
	}
	/**
	 * 通过输入InputStream类型建立MmsContent
	 * @param in
	 * @return MMContent 对象
	 */
	public static MmsContent createFromStream(InputStream in)
	{
		DataInputStream input = new DataInputStream(in);
	    ByteArrayOutputStream output = new ByteArrayOutputStream();
	    byte[] data = null;
	    try{
	      while (input.available() != 0) {
	        output.write(input.readByte());
	      }
	      data = output.toByteArray();
	    }catch(IOException ioe){
	      System.err.println(ioe);
	    }
	    MmsContent mmsContent = new MmsContent(data);
	    return(mmsContent);
	}
	/**
	 * 通过输入InputStream类型和length建立MmsContent
	 * @param in
	 * @param length
	 * @return MMContent 对象
	 */
	public static MmsContent createFromStream(InputStream in, int length)
	{
		DataInputStream input = new DataInputStream(in);
	    ByteArrayOutputStream byteout = new ByteArrayOutputStream();
	    ByteArrayOutputStream output = new ByteArrayOutputStream();
	    byte[] data = null;
	    try{
	      while (input.available() != 0) {
	        byteout.write(input.readByte());
	      }
	      data = byteout.toByteArray();
	      output.write(data,0,length);
	    }catch(IOException ioe){
	      System.err.println(ioe);
	    }
	    data = output.toByteArray();
	    MmsContent mmsContent = new MmsContent(data);
	    return(mmsContent);
	}
	/**
	 * 通过输入base64编码过的string 类型建立MMContent
	 * @param content
	 * @param charset
	 * @return MMContent 对象
	 */
	public static MmsContent CreateFromBase64String(String content, String charset)
	{
		try
		{
			MmsContent mmsContent = new MmsContent(new sun.misc.BASE64Decoder().decodeBuffer(content));
			mmsContent.setCharset(charset);
			return mmsContent;
		}catch(IOException iex)
		{
			System.err.println(iex);
			return null;
		}
	}
	/**
	 * 通过输入string 类型建立MMContent
	 * @param content
	 * @return MMContent 对象
	 */
	public static MmsContent CreateFromstring(String content)
	{
		try
		{
			MmsContent mmsContent = new MmsContent(content.getBytes());
			return mmsContent;
		}catch(Exception ex)
		{
			System.err.println(ex);
			return null;
		}
	}
	/**
	 * 通过输入string 类型建立MMContent
	 * @param content
	 * @param charset
	 * @return MMContent 对象
	 */
	public static MmsContent CreateFromstring(String content, String charset)
	{
		try
		{
			MmsContent mmsContent = new MmsContent(content.getBytes(charset));
			mmsContent.setCharset(charset);
			return mmsContent;
		}catch(Exception ex)
		{
			try
			{
				MmsContent mmsContent = new MmsContent(content.getBytes());
				return mmsContent;
			}catch(Exception e)
			{
				return null;
			}
		}
	}
	
	
}