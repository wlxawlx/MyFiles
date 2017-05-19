using System;
using System.Xml;
using System.IO;
using System.Text;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public partial class AppUtils
    {
        //支付类查询
        public const string Tag_Payment_QueryPage = "querypage";
        public const string Tag_Payment_Type = "type";

        //报告查询
        public const string Tag_Payment_BGDH = "bgdh";
        public const string Tag_Payment_SJMD = "sjmd";
        public const string Tag_Payment_CJSJ = "cjsj";
        public const string Tag_Payment_SJR = "sjr";
        public const string Tag_Payment_JYSJ = "jysj";
        public const string Tag_Payment_JYR = "jyr";
        public const string Tag_Payment_SHR = "shr";
        public const string Tag_Payment_JZCH = "jzch";
        public const string Tag_Payment_ZDJG = "zdjg";
        public const string Tag_Payment_BBMC = "bbmc";
        public const string Tag_Payment_MZBZ = "mzbz";
        public const string Tag_Payment_DYJB = "dyjb";
        public const string Tag_Payment_PayType = "paytype";

        public const string Value_Report_DYJB = "1.0";

        public const string Tag_Payment_BZ = "bz";
        public const string Tag_Payment_HZHB = "hzbh";
        public const string Tag_Query_SBM = "sbm";
        public const string Tag_Query_Type = "querytype";
        public const string Tag_Query_CXHM = "cxhm";
        public const string Tag_Query_CXLX = "type";
        public const string Tag_Query_JGMC = "jgmc";
        
        //报告详细
        public const string Tag_Payment_MC = "mc";
        public const string Tag_Payment_DW = "dw";
        public const string Tag_Payment_CKJG = "ckjg";
        public const string Tag_Payment_TS = "ts";
        public const string Tag_Payment_JG = "jg";


        //交易明细
        public const string Tag_Payment_ZFFC = "zffs";
        public const string Tag_Payment_JKRQ = "jkrq";
        public const string Tag_Payment_JKJE = "jkje";

        //卡号类型
        //本院卡病人
        public const string Tag_Payment_BYK = "byk";
        //市民卡病人
        public const string Tag_Payment_SMK = "smk";
        //医保卡病人
        public const string Tag_Payment_YBK = "ybk";

        //银联返回的信息
        //账号 
        public const string Tag_Payment_ACCNO = "accno";
        //接入类型
        public const string Tag_Payment_ACCESSTYPE = "accesstype";
        //产品类型
        public const string Tag_Payment_BIZTYPE = "biztype";
        //证书ID 
        public const string Tag_Payment_CERTID = "certid";
        // 交易币种  
        public const string Tag_Payment_CURRENCYCODE = "currencycode";
        //编码方式
        public const string Tag_Payment_ENCODING = "encoding";
        //商户代码
        public const string Tag_Payment_MERID = "merid";
        //商户订单号
        public const string Tag_Payment_ORDERID = "orderid";
        //交易查询流水号 
        public const string Tag_Payment_QUERYID = "queryid";
        // 响应码 
        public const string Tag_Payment_RESPCODE = "respcode";
        //响应信息
        public const string Tag_Payment_RESPMSG = "respmsg";
        //清算金额
        public const string Tag_Payment_SETTLEAMT = "settleamt";
        //清算币种
        public const string Tag_Payment_SETTLECURRENCYCODE = "settlecurrencycode";
        //清算日期
        public const string Tag_Payment_SETTLEDATE = "settledate";
        // 签名方法
        public const string Tag_Payment_SIGNMETHOD = "signmethod";
        //系统跟踪号 
        public const string Tag_Payment_TRACENO = "traceno";
        //交易传输时间
        public const string Tag_Payment_TRACETIME = "tracetime";
        //交易金额
        public const string Tag_Payment_TXNAMT = "txnamt";
        //交易子类
        public const string Tag_Payment_TXNSUBTYPE = "txnsubtype";
        //订单发送时间
        public const string Tag_Payment_TXNTIME = "txntime";
        //交易类型
        public const string Tag_Payment_TXNTYPE = "txntype";
        //版本号
        public const string Tag_Payment_VERSION = "version";
        //签名
        public const string Tag_Payment_SIGNATURE = "signature";
        //源通知报文（实际就是所有返回的报文的集合）
        public const string Tag_Payment_SOURCE = "source";

        //病人信息
        //提交的
        //病人姓名
        public const string Tag_Patient_BRXM = "brxm";
        //病人类型
        public const string Tag_Patient_BRLX = "brlx";
        //卡类型
        public const string Tag_Patient_BKLX = "bklx";
        //联系电话
        public const string Tag_Patient_LXDH = "lxdh";
        //入院日期
        public const string Tag_Patient_RYRQ = "ryrq";
        //充值类型
        public const string Tag_Patient_ZZLX = "zzlx";
        //充值金额
        public const string Tag_Payment_CZJE = "czje";
        //退款金额
        public const string Tag_Payment_TKJE = "tkje";
        //充值时间
        public const string Tag_Payment_CZSJ = "czsj";
        //退款
        public const string Tag_Payment_ZZLX = "zzlx";
        //账户余额
        public const string Tag_Payment_ZHYE = "zhye";
        //绑卡号码
        public const string Tag_Patient_BKHM = "bkhm";

        public const string Tag_Query_STARTTIME = "starttime";
        public const string Tag_Query_ENDTIME = "endtime";

        //返回的
        //病人ID
        public const string Tag_Patient_BRID = "brid";
        //确认状态 绑定成功时返回bdzt=1，其他均返回exception
        public const string Tag_Payment_BDZT = "bdzt";
        //病人的健康卡号
        public const string Tag_Patient_JZKH = "jzkh";
        //身份证号
        public const string Tag_Patient_SFZH = "sfzh";
        //开卡时间
        public const string Tag_Payment_KKSJ = "kksj";
        //联系电话提示
        public const string Tag_Patient_DHTS = "dhts";
        //家庭地址
        public const string Tag_Patient_JTDZ = "jtdz";
        //住院病人所在病区
        public const string Tag_Patient_SZBQ = "szbq";
        //住院病人所在床位
        public const string Tag_Patient_SZCW = "szcw";
        //住院病人的住院号
        public const string Tag_Patient_ZYH = "zyh";


        //操作时间
        public const string Tag_Payment_OPDATE = "opdate";
        //确认状态 成功时返回1，其他返回信息都将采用exception描述
        public const string Tag_Payment_ZZZT = "zzzt";
        //累计金额
        public const string Tag_Payment_LJFY = "ljfy";
        //医院流水号
        public const string Tag_Payment_YYLSH = "yylsh";
        //银行流水号
        public const string Tag_Payment_YHLSH = "yhlsh";
        //银行代码
        public const string Tag_Payment_YHDM = "yhdm";
        //银行名称
        public const string Tag_Payment_YHMC = "yhmc";
        //手机支付状态
        public const string Tag_Payment_Sjczzt = "sjczzt";

        //确认状态,确认成功1，确认失败-1；(不包括异常）
        public const string Tag_Payment_QRZT = "qrzt";
        //订单状态
        public const string Tag_Payment_DDZT = "ddzt";
        //状态名称
        public const string Tag_Payment_ZTMC = "ztmc";

        //银行
        //商户代码
        public const string Tag_Payment_SHDM = "shdm";
        //商户柜台代码
        public const string Tag_Payment_SHGTDM = "shgtdm";
        //分行代码
        public const string Tag_Payment_FHDM = "fhdm";
        //交易金额
        public const string Tag_Payment_JYJE = "jyje";
        //应用设定标记 
        public const string Tag_Payment_REMARK1 = "remark1";
        //应用编号设定：温州智慧健康手机支付应用编号为“001”
        public const string Tag_Payment_REMARK2 = "remark2";
        //联系电话
        public const string Tag_Payment_LXDH = "lxdh";
        //支付成功标记
        public const string Tag_Payment_CGBZ = "cgbz";
        //银行系统电子签名
        public const string Tag_Payment_SZQM = "szqm";


        //药品
        //药品类型
        public const string Tag_Payment_YPLX = "yplx";
        //药品名称
        public const string Tag_Payment_YPMC = "ypmc";
        //计量单位
        public const string Tag_Payment_YPDW = "ypdw";
        //药品规格
        public const string Tag_Payment_YPGG = "ypgg";
        //药品产地
        public const string Tag_Payment_YPCD = "ypcd";
        //药品价格
        public const string Tag_Payment_YPJG = "ypjg";


        //费用
        //费用类型
        public const string Tag_Payment_FYLX = "fylx";
        //费用名称
        public const string Tag_Payment_FYMC = "fymc";
        //计量单位
        public const string Tag_Payment_FYDW = "fydw";
        //收费价格
        public const string Tag_Payment_FYJG = "fyjg";


        //医生
        //医生姓名
        public const string Tag_Payment_YSXM = "ysxm";
        //医生代码
        public const string Tag_Payment_YSDM = "ysdm";
        //医生级别
        public const string Tag_Payment_YSJB = "ysjb";
        //医生简介
        public const string Tag_Payment_YSJJ = "ysjj";
        //医生擅长
        public const string Tag_Payment_YSSC = "yssc";
        //建立时间
        public const string Tag_Patient_JLSJ = "jlsj";

        //病人取药信息
        //开方日期
        public const string Tag_Payment_KFRQ = "kfrq";
        //取药序号
        public const string Tag_Payment_QYXH = "qyxh";
        //处方状态
        public const string Tag_Payment_CFZT = "cfzt";

        //账户余额明细
        public const string Tag_Balance_Brxm = "brxm";
        public const string Tag_Balance_Sfzh = "sfzh";
        public const string Tag_Balance_Bkhm = "bkhm";
        public const string Tag_Balance_Ljfy = "ljfy";
        public const string Tag_Balance_Zhye = "zhye";


        //(增补）列表分页
        public const string Tag_Payment_PageNumber = "pageno";
        public const string Tag_Payment_PageRow = "pagerow";


        //订单列表
        public const string Tag_Order_Yylsh = "yylsh";
        public const string Tag_Order_Yhlsh = "yhlsh";
        public const string Tag_Order_Yhmc = "yhmc";
        public const string Tag_Order_Cssj = "cssj";
        public const string Tag_Order_Czje = "czje";
        public const string Tag_Order_Ddzt = "ddzt";
        public const string Tag_Order_Sjczzt = "sjczzt";
        public const string Tag_Order_PayType = "paytype";

        //床位查询
        public const string Tag_Query_Bqmc = "bqmc";
        public const string Tag_Query_Sycw = "sycw";


        //预约信息节点
        public const string Tag_Yyxx_Pdhm = "pdhm";
        public const string Tag_Yyxx_Brxm = "brxm";
        public const string Tag_Yyxx_Ksmc = "ksmc";
        public const string Tag_Yyxx_Doctor = "doctor";
        public const string Tag_Yyxx_Yysj = "yysj";
        public const string Tag_Yyxx_Yyly = "yyly";

        //病人排队信息节点
        public const string Tag_QueueInfo_Zt = "zt";
        public const string Tag_QueueInfo_Brxm = "brxm";
        public const string Tag_QueueInfo_Sfzh = "sfzh";
        public const string Tag_QueueInfo_Ksmc = "ksmc";
        public const string Tag_QueueInfo_Zsmc = "zsmc";
        public const string Tag_QueueInfo_Doctor = "doctor";
        public const string Tag_QueueInfo_Pdhm = "pdhm";
        public const string Tag_QueueInfo_WaitCount = "waitcount";
        public const string Tag_QueueInfo_SpecialWaitCount = "specialwaitcount";
        public const string Tag_QueueInfo_Yjjzsj = "yjjzsj";

        //异常信息内容(为明显区分，修改了原有的Tag_Payment_Excp）
        public const string Content_Payment_Excp = "异常，请联系系统管理人员";

        //时间格式
        public const string DateTime_Format = "1900-01-01 00:00:00";
        public const string DateTime_Format_Year = "yyyy";
        public const string DateTime_Format_YearMonthDay = "yyyy-MM-dd";
        public const string DateTime_FormatNO_YearMonthDay = "yyyyMMdd";
        public const string DateTime_Format_Time = "HH:mm:ss";
        public const string DateTime_Format_All = "yyyy-MM-dd HH:mm:ss";

        //默认充值额度限定  允许配置文件中修改
        public const int Payment_Amount = 5000;
        
        //支付宝接口
        //用户注册
        //姓名
        public const string Tag_User_Name = "name";
        //电话
        public const string Tag_User_Phone = "phone";
        //身份证号
        public const string Tag_User_IDCardNo = "idcardno";
        //地址
        public const string Tag_User_Address = "address";
        //用户标识
        public const string Tag_User_OpenID = "openid";
        //用户头像
        public const string Tag_User_HeadURL = "headurl";
        //用户类型
        public const string Tag_User_UserType = "usertype";
        //就诊卡号
        public const string Tag_User_CardNo = "cardno";
        //病人ID
        public const string Tag_User_PatientID = "patientid";
        //联系人标签
        public const string Tag_User_Label = "label";
        //联系人ID
        public const string Tag_User_LinkManID = "linkmanid";
        //病人姓名
        public const string Tag_User_PatientName = "patientname";
        //病人身份证号
        public const string Tag_User_PatientCardNo = "patientcardno";
        //标题
        public const string Tag_User_SubJect = "subject";
        //金额
        public const string Tag_User_Money = "money";
        //住院号码
        public const string Tag_User_InPatientNo = "inpatientno";
        //费用日期
        public const string Tag_User_CostDate = "costdate";     
        //订单号
        public const string Tag_User_Tradeno = "tradeno";
        //支付平台订单号
        public const string Tag_User_PaymentTradeno = "paymenttradeno";
        //通知参数
        public const string Tag_User_PaymentParameters = "paymentparameters";
        //就诊卡名称
        public const string Tag_Patient_CardName = "cardname";
        //出生日期
        public const string Tag_Patient_Birthday = "birthday";
        //就诊卡类型
        public const string Tag_Patient_CardType = "cardtype";
        //账户余额
        public const string Tag_Patient_Balance = "balance";
        //累计费用
        public const string Tag_Patient_Cost = "cost";
        //绑卡标志
        public const string Tag_Patient_BindCardFalg = "bindcardfalg";
        //排班日期
        public const string Tag_User_ScheduleDate = "scheduledate";
        //总号源
        public const string Tag_User_Total = "total";
        //医生代码
        public const string Tag_User_Doctorcode = "doctorcode";
        //排班流水号
        public const string Tag_User_Scheduleseq = "scheduleseq";
        //号源流水号
        public const string Tag_User_Orderseq = "orderseq";
        //预约结束时间
        public const string Tag_User_Orderendtime = "orderendtime";
        //病人身份证号
        public const string Tag_User_PatientIDCardNO = "patientidcardno";
        //病人电话
        public const string Tag_User_PatientPhone = "patientphone";
        //病人地址
        public const string Tag_User_PatientAddress = "patientaddress";
        //预约流水号
        public const string Tag_User_Preengageseq = "preengageseq";
        //状态
        public const string Tag_User_Status = "status";
        //创建时间
        public const string Tag_User_CTime = "ctime";
        //付款时间
        public const string Tag_User_PayTime = "paytime";
        //退款时间
        public const string Tag_User_RtnTime = "rtntime";
        //条码号
        public const string Tag_User_Doctadviseno = "doctadviseno";
        //检验内容
        public const string Tag_User_Examinaim = "examinaim";
        //送检时间
        public const string Tag_User_Requesttime = "requesttime";
        //送检人
        public const string Tag_User_Requester = "requester";
        //采集时间
        public const string Tag_User_Executetime = "executetime";
        //采集人
        public const string Tag_User_Executer = "executer";
        //接收时间
        public const string Tag_User_Receivetime = "receivetime";
        //接收人
        public const string Tag_User_Receiver = "receiver";
        //标示来源
        public const string Tag_User_Stayhospitalmode = "stayhospitalmode";
        //病人编号
        public const string Tag_User_Patientid = "patientid";
        //申请科室
        public const string Tag_User_Section = "section";
        //床号
        public const string Tag_User_Bedno = "bedno";
        //病人姓名
        public const string Tag_User_Patientname = "patientname";
        //性别
        public const string Tag_User_Sex = "sex";
        //年龄
        public const string Tag_User_Age = "age";
        //诊断
        public const string Tag_User_Diagnostic = "diagnostic";
        //标本类型
        public const string Tag_User_Sampletype = "sampletype";
        //平/急诊
        public const string Tag_User_Requestmode = "requestmode";
        //审核人
        public const string Tag_User_Checker = "checker";
        //审核时间
        public const string Tag_User_Checktime = "checktime";
        //测试仪器
        public const string Tag_User_Csyq = "csyq";
        //测试方法
        public const string Tag_User_Profiletest = "profiletest";
        //检验类型
        public const string Tag_User_Jylx = "jylx";
        //项目名称
        public const string Tag_User_Xmmc = "xmmc";
        //结果
        public const string Tag_User_Result = "result";
        //标志
        public const string Tag_User_Hint = "hint";
        //健康范围
        public const string Tag_User_Ckfw = "ckfw";
        //项目单位
        public const string Tag_User_Xmdw = "xmdw";
        //检查所见
        public const string Tag_User_StudyResult = "studyresult";
        //检查诊断
        public const string Tag_User_DiagResult = "diagresult";
        //医生姓名
        public const string Tag_User_DoctorName = "_name";
        //访问页码
        public const string Tag_User_PageIndex = "pageindex";
        //每页行数
        public const string Tag_User_PageSize = "pagesize";
        //医生代码
        public const string Tag_User_Code = "code";
        //医生照片
        public const string Tag_User_PictureURL = "pictureurl";
        //医生等级
        public const string Tag_User_Level = "level";
        //医生介绍
        public const string Tag_User_Recommend = "recommend";
        //医生擅长
        public const string Tag_User_Adept = "adept";
        //科室代码
        public const string Tag_User_DepartCode = "departcode";
        //科室名称
        public const string Tag_User_DepartName = "departname";
        //停诊日期
        public const string Tag_User_StopDate = "stopdate";
        //停诊班次
        public const string Tag_User_StopShift = "stopshift";
        //二级科室名称
        public const string Tag_User_SecondName = "secondname";
        //二级科室代码
        public const string Tag_User_SecondCode = "secondcode";
        //二级科室描述
        public const string Tag_User_Describe = "describe";
        //可约人数
        public const string Tag_User_Reserve = "reserve";
        //排班日期
        public const string Tag_User_ScheduleDates = "scheduledates";
        //特需标志
        public const string Tag_User_Special = "Special";
        //还可预约人数
        public const string Tag_User_Remain = "remain";
        //班次编码
        public const string Tag_User_ShiftCode = "shiftcode";
        //班次名称
        public const string Tag_User_ShiftName = "shiftname";
        //挂号费
        public const string Tag_User_Fee = "fee";
        //号源流水号
        public const string Tag_User_OrderSeq = "orderseq";
        //预约时间
        public const string Tag_User_OrderTime = "ordertime";
        //分诊序号
        public const string Tag_User_OrderNo = "orderno";
        //预约流水号
        public const string Tag_User_PreengageSeq = "preengageseq";
        //预约日期
        public const string Tag_User_PreengageDate = "preengagedate";
        //预约时间
        public const string Tag_User_PreengageTime = "preengagetime";
        //分诊序号
        public const string Tag_User_PreengageNo = "preengageno";
        //就诊地点
        public const string Tag_User_Place = "place";
        //预约状态
        public const string Tag_User_Preengagestate = "preengagestate";
        //分诊序号
        public const string Tag_User_Preengageno = "preengageno";
        //医生姓名拼音
        public const string Tag_User_NamePY = "namepy";
        //排队流水号
        public const string Tag_User_Queueseq = "queueseq";
        //排队号码
        public const string Tag_User_QueueId = "queueid";
        //排队时间
        public const string Tag_User_QueueTime = "queuetime";
        //诊室名称
        public const string Tag_User_RoomName = "roomname";
        //诊室位置
        public const string Tag_User_RoomPos = "roompos";
        //房间号码
        public const string Tag_User_RoomNo = "roomno";
        //当前就诊号码
        public const string Tag_User_CurrentId = "currentid";
        //排队状态
        public const string Tag_User_QueueState = "queuestate";
        //就诊序号
        public const string Tag_User_Jzxh = "jzxh";
        //处方流水号
        public const string Tag_User_Cflsh = "cflsh";
        //挂号ID
        public const string Tag_User_guahaoID = "guahaoID";

        //处方id
        public const string Tag_User_ChuFangID = "sfid";

        public enum HOSPITALNAME
        {
            //温州市
            //温州市第二医院
            WZSDEYY = 0,
            //温州市第七医院
            WZSDQRMYY,
            //温州市第三人民医院，温州市人民医院
            WZSDSRMYY,
            //温州市中西医结合医院
            WZSZXYJHYY,
            //温州市中医院
            WZSZYY,

            //苍南县
            //温州市苍南县第一人民医院
            WZSCNXDYRMYY,
            //温州市苍南县第三人民医院
            WZSCNXDSRMYY,
            //温州市苍南县中医院
            WZSCNXZYY,
            //温州市第二人民医院
            WZSCNXDERMYY,
            //温州市苍南县妇幼保健医院
            WZSCNXFYBJYY,
            //温州市永嘉县第三人民医院
            WZSYJXDSRMYY,
            //温州市塘下人民医院
            WZSTXRMYY,
            //温州市泰顺县中医院
            WZSTSXZYY,
            //温州市瑞安县人民医院
            WZSRARMYY,
            //温州市瑞安市中医院
            WZSRAZYY,
            //温州海坦医院
            WZHTYY,
            //未知
            UNKNOW = -99
        }
    }
}