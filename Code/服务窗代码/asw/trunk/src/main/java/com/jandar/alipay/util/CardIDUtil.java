package com.jandar.alipay.util;


	

import java.text.ParseException;
import java.text.SimpleDateFormat;   
import java.util.Calendar;   
import java.util.Date;   
import java.util.GregorianCalendar;   
import java.util.HashMap;   
import java.util.Map;   
import java.util.Set;   
import java.util.regex.Pattern;
  
public class CardIDUtil {

    // 省份   
    private String province;   
    // 城市   
    private String city;   
    // 区县   
    private String region;   
    // 年份   
    private int year;   
    // 月份   
    private int month;   
    // 日期   
    private int day;   
    // 性别   
    private String gender;   
    // 出生日期   
    private Date birthday;   
  
    @SuppressWarnings("serial")
	private Map<String, String> cityCodeMap = new HashMap<String, String>() {   
        {   
            this.put("11", "北京");   
            this.put("12", "天津");   
            this.put("13", "河北");   
            this.put("14", "山西");   
            this.put("15", "内蒙古");   
            this.put("21", "辽宁");   
            this.put("22", "吉林");   
            this.put("23", "黑龙江");   
            this.put("31", "上海");   
            this.put("32", "江苏");   
            this.put("33", "浙江");   
            this.put("34", "安徽");   
            this.put("35", "福建");   
            this.put("36", "江西");   
            this.put("37", "山东");   
            this.put("41", "河南");   
            this.put("42", "湖北");   
            this.put("43", "湖南");   
            this.put("44", "广东");   
            this.put("45", "广西");   
            this.put("46", "海南");   
            this.put("50", "重庆");   
            this.put("51", "四川");   
            this.put("52", "贵州");   
            this.put("53", "云南");   
            this.put("54", "西藏");   
            this.put("61", "陕西");   
            this.put("62", "甘肃");   
            this.put("63", "青海");   
            this.put("64", "宁夏");   
            this.put("65", "新疆");   
            this.put("71", "台湾");   
            this.put("81", "香港");   
            this.put("82", "澳门");   
            this.put("91", "国外");   
        }   
    };   
       
    private IdcardValidator validator = null;   
       
     
    public CardIDUtil() {
		super();
	}


	public CardIDUtil(String idcard) {   
        try {   
            validator = new IdcardValidator();   
            if (validator.isValidatedAllIdcard(idcard)) {   
                if (idcard.length() == 15) {   
                    idcard = validator.convertIdcarBy15bit(idcard);   
                }   
                // 获取省份   
                String provinceId = idcard.substring(0, 2);   
                Set<String> key = this.cityCodeMap.keySet();   
                for (String id : key) {   
                    if (id.equals(provinceId)) {   
                        this.province = this.cityCodeMap.get(id);   
                        break;   
                    }   
                }   
  
                // 获取性别   
                String id17 = idcard.substring(16, 17);   
                if (Integer.parseInt(id17) % 2 != 0) {   
                    this.gender = "1";   
                } else {   
                    this.gender = "2";   
                }   
  
                // 获取出生日期   
                String birthday = idcard.substring(6, 14);   
                Date birthdate = new SimpleDateFormat("yyyyMMdd")   
                        .parse(birthday);   
                this.birthday = birthdate;   
                GregorianCalendar currentDay = new GregorianCalendar();   
                currentDay.setTime(birthdate);   
                this.year = currentDay.get(Calendar.YEAR);   
                this.month = currentDay.get(Calendar.MONTH) + 1;   
                this.day = currentDay.get(Calendar.DAY_OF_MONTH);  
            }   
        } catch (Exception e) {   
            e.printStackTrace();   
        }   
    }   
  
      
    public String getProvince() {   
        return province;   
    }   
  
      
    public String getCity() {   
        return city;   
    }   
  
      
    public String getRegion() {   
        return region;   
    }   
  
      
    public int getYear() {   
        return year;   
    }   
  
      
    public int getMonth() {   
        return month;   
    }   
  
      
    public int getDay() {   
        return day;   
    }   
  
     /*
      * 男：1 女：2
      */
    public String getGender() {   
        return gender;   
    }   
  
      
    public Date getBirthday() {   
        return birthday;   
    }   
    public int getAgeByBirthday(Date birthday) {
    	Calendar cal = Calendar.getInstance();

    	if (cal.before(birthday)) {
    		throw new IllegalArgumentException(
    				"The birthDay is before Now.It's unbelievable!");
    	}

    	int yearNow = cal.get(Calendar.YEAR);
    	int monthNow = cal.get(Calendar.MONTH) + 1;
    	int dayOfMonthNow = cal.get(Calendar.DAY_OF_MONTH);

    	cal.setTime(birthday);
    	int yearBirth = cal.get(Calendar.YEAR);
    	int monthBirth = cal.get(Calendar.MONTH) + 1;
    	int dayOfMonthBirth = cal.get(Calendar.DAY_OF_MONTH);

    	int age = yearNow - yearBirth;

    	if (monthNow <= monthBirth) {
    		if (monthNow == monthBirth) {
    			// monthNow==monthBirth 
    			if (dayOfMonthNow < dayOfMonthBirth) {
    				age--;
    			}
    		} else {
    			// monthNow>monthBirth 
    			age--;
    		}
    	}
    	return age;
    }
  
    @Override  
    public String toString() {   
        return "省份：" + this.province + ",性别：" + this.gender + ",出生日期："  
                + this.birthday;   
    }   
  
    public static void main(String[] args) {   
        String idcard = "330381198912247514";   
        CardIDUtil ie = new CardIDUtil(idcard);   
        System.out.println(ie.getYear()+""+ie.getMonth()+ie.getDay());   
    }   
}

class IdcardValidator {
	protected String codeAndCity[][] = { { "11", "北京" }, { "12", "天津" },   
            { "13", "河北" }, { "14", "山西" }, { "15", "内蒙古" }, { "21", "辽宁" },   
            { "22", "吉林" }, { "23", "黑龙江" }, { "31", "上海" }, { "32", "江苏" },   
            { "33", "浙江" }, { "34", "安徽" }, { "35", "福建" }, { "36", "江西" },   
            { "37", "山东" }, { "41", "河南" }, { "42", "湖北" }, { "43", "湖南" },   
            { "44", "广东" }, { "45", "广西" }, { "46", "海南" }, { "50", "重庆" },   
            { "51", "四川" }, { "52", "贵州" }, { "53", "云南" }, { "54", "西藏" },   
            { "61", "陕西" }, { "62", "甘肃" }, { "63", "青海" }, { "64", "宁夏" },   
            { "65", "新疆" }, { "71", "台湾" }, { "81", "香港" }, { "82", "澳门" },   
            { "91", "国外" } };   
  
     private String cityCode[] = { "11", "12", "13", "14", "15", "21", "22",   
            "23", "31", "32", "33", "34", "35", "36", "37", "41", "42", "43",   
            "44", "45", "46", "50", "51", "52", "53", "54", "61", "62", "63",   
            "64", "65", "71", "81", "82", "91" };   
  
    // 每位加权因子   
    private int power[] = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };   
  
    // 第18位校检码   
    @SuppressWarnings("unused")
	private String verifyCode[] = { "1", "0", "X", "9", "8", "7", "6", "5",   
            "4", "3", "2" };   
  
       
      
    public boolean isValidatedAllIdcard(String idcard) {   
        if (idcard.length() == 15) {   
            idcard = this.convertIdcarBy15bit(idcard);   
        }   
        return this.isValidate18Idcard(idcard);   
    }   
  
      
    public boolean isValidate18Idcard(String idcard) {   
        // 非18位为假   
        if (idcard.length() != 18) {   
            return false;   
        }   
        // 获取前17位   
        String idcard17 = idcard.substring(0, 17);   
        // 获取第18位   
        String idcard18Code = idcard.substring(17, 18);   
        char c[] = null;   
        String checkCode = "";   
        // 是否都为数字   
        if (isDigital(idcard17)) {   
            c = idcard17.toCharArray();   
        } else {   
            return false;   
        }   
  
        if (null != c) {   
            int bit[] = new int[idcard17.length()];   
  
            bit = converCharToInt(c);   
  
            int sum17 = 0;   
  
            sum17 = getPowerSum(bit);   
  
            // 将和值与11取模得到余数进行校验码判断   
            checkCode = getCheckCodeBySum(sum17);   
            if (null == checkCode) {   
                return false;   
            }   
            // 将身份证的第18位与算出来的校码进行匹配，不相等就为假   
            if (!idcard18Code.equalsIgnoreCase(checkCode)) {   
                return false;   
            }   
        }   
        return true;   
    }  
    
  
      
    public boolean isValidate15Idcard(String idcard) {   
        // 非15位为假   
        if (idcard.length() != 15) {   
            return false;   
        }   
  
        // 是否全都为数字   
        if (isDigital(idcard)) {   
            String provinceid = idcard.substring(0, 2);   
            String birthday = idcard.substring(6, 12);   
            int year = Integer.parseInt(idcard.substring(6, 8));   
            int month = Integer.parseInt(idcard.substring(8, 10));   
            int day = Integer.parseInt(idcard.substring(10, 12));   
  
            // 判断是否为合法的省份   
            boolean flag = false;   
            for (String id : cityCode) {   
                if (id.equals(provinceid)) {   
                    flag = true;   
                    break;   
                }   
            }   
            if (!flag) {   
                return false;   
            }   
            // 该身份证生出日期在当前日期之后时为假   
            Date birthdate = null;   
            try {   
                birthdate = new SimpleDateFormat("yyMMdd").parse(birthday);   
            } catch (ParseException e) {   
                e.printStackTrace();   
            }   
            if (birthdate == null || new Date().before(birthdate)) {   
                return false;   
            }   
   
            // 判断是否为合法的年份   
            GregorianCalendar curDay = new GregorianCalendar();   
            int curYear = curDay.get(Calendar.YEAR);   
            int year2bit = Integer.parseInt(String.valueOf(curYear)   
                    .substring(2));   
  
            // 判断该年份的两位表示法，小于50的和大于当前年份的，为假   
            if ((year < 50 && year > year2bit)) {   
                return false;   
            }   
  
            // 判断是否为合法的月份   
            if (month < 1 || month > 12) {   
                return false;   
            }   
  
            // 判断是否为合法的日期   
            boolean mflag = false;   
            curDay.setTime(birthdate);  //将该身份证的出生日期赋于对象curDay   
            switch (month) {   
            case 1:   
            case 3:   
            case 5:   
            case 7:   
            case 8:   
            case 10:   
            case 12:   
                mflag = (day >= 1 && day <= 31);   
                break;   
            case 2: //公历的2月非闰年有28天,闰年的2月是29天。   
                if (curDay.isLeapYear(curDay.get(Calendar.YEAR))) {   
                    mflag = (day >= 1 && day <= 29);   
                } else {   
                    mflag = (day >= 1 && day <= 28);   
                }   
                break;   
            case 4:   
            case 6:   
            case 9:   
            case 11:   
                mflag = (day >= 1 && day <= 30);   
                break;   
            }   
            if (!mflag) {   
                return false;   
            }   
        } else {   
            return false;   
        }   
        return true;   
    }   
  
      
    public String convertIdcarBy15bit(String idcard) {   
        String idcard17 = null;   
        // 非15位身份证   
        if (idcard.length() != 15) {   
            return null;   
        }   
  
        if (isDigital(idcard)) {   
            // 获取出生年月日   
            String birthday = idcard.substring(6, 12);   
            Date birthdate = null;   
            try {   
                birthdate = new SimpleDateFormat("yyMMdd").parse(birthday);   
            } catch (ParseException e) {   
                e.printStackTrace();   
            }   
            Calendar cday = Calendar.getInstance();   
            cday.setTime(birthdate);   
            String year = String.valueOf(cday.get(Calendar.YEAR));   
  
            idcard17 = idcard.substring(0, 6) + year + idcard.substring(8);   
  
            char c[] = idcard17.toCharArray();   
            String checkCode = "";   
  
            if (null != c) {   
                int bit[] = new int[idcard17.length()];   
  
                // 将字符数组转为整型数组   
                bit = converCharToInt(c);   
                int sum17 = 0;   
                sum17 = getPowerSum(bit);   
  
                // 获取和值与11取模得到余数进行校验码   
                checkCode = getCheckCodeBySum(sum17);   
                // 获取不到校验位   
                if (null == checkCode) {   
                    return null;   
                }   
  
                // 将前17位与第18位校验码拼接   
                idcard17 += checkCode;   
            }   
        } else { // 身份证包含数字   
            return null;   
        }   
        return idcard17;   
    }   
  
      
    public boolean isIdcard(String idcard) {   
        return idcard == null || "".equals(idcard) ? false : Pattern.matches(   
                "(^\\d{15}$)|(\\d{17}(?:\\d|x|X)$)", idcard);   
    }   
  
      
    public boolean is15Idcard(String idcard) {   
        return idcard == null || "".equals(idcard) ? false : Pattern.matches(   
                "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$",   
                idcard);   
    }   
  
      
    public boolean is18Idcard(String idcard) {   
        return Pattern   
                .matches(   
                        "^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}([\\d|x|X]{1})$",   
                        idcard);   
    }   
  
      
    public boolean isDigital(String str) {   
        return str == null || "".equals(str) ? false : str.matches("^[0-9]*$");   
    }   
      
    public int getPowerSum(int[] bit) {   
  
        int sum = 0;   
  
        if (power.length != bit.length) {   
            return sum;   
        }   
  
        for (int i = 0; i < bit.length; i++) {   
            for (int j = 0; j < power.length; j++) {   
                if (i == j) {   
                    sum = sum + bit[i] * power[j];   
                }   
            }   
        }   
        return sum;   
    }   
  
      
    public String getCheckCodeBySum(int sum17) {   
        String checkCode = null;   
        switch (sum17 % 11) {   
        case 10:   
            checkCode = "2";   
            break;   
        case 9:   
            checkCode = "3";   
            break;   
        case 8:   
            checkCode = "4";   
            break;   
        case 7:   
            checkCode = "5";   
            break;   
        case 6:   
            checkCode = "6";   
            break;   
        case 5:   
            checkCode = "7";   
            break;   
        case 4:   
            checkCode = "8";   
            break;   
        case 3:   
            checkCode = "9";   
            break;   
        case 2:   
            checkCode = "x";   
            break;   
        case 1:   
            checkCode = "0";   
            break;   
        case 0:   
            checkCode = "1";   
            break;   
        }   
        return checkCode;   
    }   
  
      
    public int[] converCharToInt(char[] c) throws NumberFormatException {   
        int[] a = new int[c.length];   
        int k = 0;   
        for (char temp : c) {   
            a[k++] = Integer.parseInt(String.valueOf(temp));   
        }   
        return a;   
    }   
  
    public static void main(String[] args) throws Exception {   
                  String idcard15 = "";   
                  String idcard18 = "";   
        IdcardValidator iv = new IdcardValidator();   
        boolean flag = false;   
        flag = iv.isValidate18Idcard(idcard18);   
        System.out.println(flag);   
  
        flag = iv.isValidate15Idcard(idcard15);   
        System.out.println(flag);   
  
        System.out.println(iv.convertIdcarBy15bit(idcard15));   
        flag = iv.isValidate18Idcard(iv.convertIdcarBy15bit(idcard15));   
        System.out.println(flag);   
  
        System.out.println(iv.isValidatedAllIdcard(idcard18));   
           
        }   
}  
