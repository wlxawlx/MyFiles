using System;
using System.Collections.Generic;
using HospitaPaymentService.Wzszhjk.Utils;

namespace HospitaPaymentService.Wzszhjk.Model
{
    //支付明细
    public struct PaymentDetail
    {
        //日期 项目  金额
        public DateTime rq;
        public string item;
        public double je;
    }

    //药品明细查询
    public struct MedicineDetail
    {
        //药品类型 药品名称 计量单位 药品规格 药品产地 药品价格
        public string lx;
        public string mc;
        public string dw;
        public string gg;
        public string cd;
        public double jg;
    }

    //费用查询
    public struct ChargeDetail
    {
        //费用类型  费用名称 费用单位 费用价格
        public string lx;
        public string mc;
        public string dw;
        public double jg;
    }

    //医生列表
    public struct DoctorInfo
    {
        //姓名  单位 级别 简介 擅长
        public string xm;
        public string dm;
        public string jb;
        public string jj;
        public string sc;
    }

    //病人信息   主要是查询绑卡时返回的信息
    public class PatientInfo
    {
        //病人ID
        public string brid;
        //病人卡号
        public string bkhm;
        //健康卡号		
        public string jzkh;
        //绑卡类型	是	"byk"-本院卡  "smk"-市民卡 "ybk"-医保卡
        public string bklx;
        //病人姓名		
        public string brxm;
        //身份证号		
        public string sfzh;
        //本院建档时间 	是	
        public DateTime jlsj;
        //入院日期
        public DateTime ryrq;
        //联系电话(没有加*）
        public string lxdh;
        //家庭地址	
        public string jtdz;
        //所住病区	否	住院病人必填
        public string szbq;
        //床位号	否	住院病人必填
        public string szcw;
        //住院号
        public string zyh;
        //账户余额
        public string balance;
        //累计费用
        public string cost;
        //就诊卡名称
        public string cardname;
        //出生日期
        public string birthday;

        public PatientInfo()
        {
            brid = "";
            bkhm = "";
            jzkh = "";
            bklx = "";
            brxm = "";
            sfzh = "";
            jlsj = DateTime.MinValue;
            ryrq = DateTime.MinValue;
            lxdh = "";
            jtdz = "";
            szbq = "";
            szcw = "";
            zyh = "";
            balance = "";
            cost = "";
            cardname = "";
            birthday = "";
        }
    }


    //取药信息
    public struct PatientDrugInfo
    {
        //日期 序号  状态
        public string rq;
        public string xh;
        public string zt;
    }

    //报告头
    public struct ReportInfo
    {
        //报告单号
        public string bgdh;
        //送检目的
        public string sjmd;
        //创建时间
        public string cjsj;
        //送检人
        public string sjr;
        //检验时间
        public string jysj;
        //检验人
        public string jyr;
        //审核人
        public string shr;
        //就诊床号
        public string jzch;
        //诊断结果
        public string zdjg;
        //病人姓名
        public string brxm;
        //标本名称
        public string bbmc;
        //门诊编号
        public string mzbz;
        //调阅级别
        public string dyjb;
        //备注
        public string bz;
        //患者编号
        public string hzbh;
        //条形码
        public string sbm;
        //机构名称
        public string jgmc;
        //性别
        public string sex;
        //年龄
        public string age;

        public ICollection<ReportDetail> details;

        public string getValue(string tag)
        {
            switch (tag)
            {
                case AppUtils.Tag_Payment_BGDH:
                    return bgdh;
                case AppUtils.Tag_Payment_SJMD:
                    return sjmd;
                case AppUtils.Tag_Payment_CJSJ:
                    return cjsj;
                case AppUtils.Tag_Payment_SJR:
                    return sjr;
                case AppUtils.Tag_Payment_JYSJ:
                    return jysj;
                case AppUtils.Tag_Payment_JYR:
                    return jyr;
                case AppUtils.Tag_Payment_SHR:
                    return shr;
                case AppUtils.Tag_Payment_JZCH:
                    return jzch;
                case AppUtils.Tag_Payment_ZDJG:
                    return zdjg;
                case AppUtils.Tag_Payment_BBMC:
                    return bbmc;
                case AppUtils.Tag_Payment_MZBZ:
                    return mzbz;
                case AppUtils.Tag_Payment_DYJB:
                    return dyjb;
                case AppUtils.Tag_Payment_BZ:
                    return bz;
                case AppUtils.Tag_Payment_HZHB:
                    return hzbh;
                case AppUtils.Tag_Query_SBM:
                    return sbm;
                case AppUtils.Tag_Query_JGMC:
                    return jgmc;
                default:
                    return "";
            }
        }
    }

    //报告详细条目
    public struct ReportDetail
    {
        //名称
        public string mc;
        //单位
        public string dw;
        //参考结果
        public string ckjg;
        //提示
        public string ts;
        //结果
        public string jg;
        ////
        //public string dyjb;
        //检验类型
        public string jylx;

        public string getValue(string tag)
        {
            switch (tag)
            {
                case AppUtils.Tag_Payment_MC:
                    return mc;
                case AppUtils.Tag_Payment_DW:
                    return dw;
                case AppUtils.Tag_Payment_CKJG:
                    return ckjg;
                case AppUtils.Tag_Payment_TS:
                    return ts;
                case AppUtils.Tag_Payment_JG:
                    return jg;
                //case AppUtils.Tag_Payment_DYJB:
                //    return dyjb;
                default:
                    return "";
            }
        }
    }

    public struct ReportOrder
    {
        //医院流水号
        public string yylsh;
        //银行流水号
        public string yhlsh;
        //银行名称
        public string yhmc;
        //创建时间
        public string cssj;
        //充值金额
        public double czje;
        //订单状态
        public string ddzt;
        //手机充值状态
        public string sjczzt;
        //支付类型 alipay-支付宝  unionpay-银联
        public string paytype;
    }

    //剩余床位信息 
    public struct RemainBeds
    {
        //病区名称 病区ID 最大床位数   在住人数  剩余床位数
        public string bqmc;
        public string bqid;
        public int zdcws;
        public int zzrenshu;
        public int sycw;
    }

    //账户信息  充值方式   交款日期   交款金额
    public struct AccountInfo
    {
        // 充值方式   交款日期   交款金额
        public string zffs;
        public string jkrq;
        public double jkje;
    }

    //余额信息 
    public struct BalanceInfo
    {
        //病人姓名 身份证号 绑卡卡号 累计金额 账户余额
        public string brxm;
        public string sfzh;
        public string bkhm;
        public double ljfy;
        public double zhye;
    }

    //银联返回信息
    public class YLReplyInfo
    {
        //医院流水号
        public long yylsh;
        //银行卡号
        public string accNo;
        public string accessType;
        public string bizType;
        public string certId;
        public string currencyCode;
        public string encoding;
        public string merId;
        public string orderId;
        public string queryId;
        public string respCode;
        public string respMsg;
        public string settleAmt;
        public string settleCurrencyCode;
        public string settleDate;
        public string signMethod;
        public string traceNo;
        public string traceTime;
        public string txnAmt;
        public string txnSubType;
        public string txnTime;
        public string txnType;
        public string version;
        public string signature;
        public string source;
        public string paytype;

        public string YLInfoToString()
        {
            string result = " yylsh: " + Convert.ToString(yylsh) +
                            " accNo: " + accNo +
                            " accessType: " + accessType +
                            " bizType: " + bizType +
                            " certId: " + certId +
                            " currencyCode: " + currencyCode +
                            " encoding: " + encoding +
                            " merId: " + merId +
                            " orderId: " + orderId +
                            " queryId: " + queryId +
                            " respCode: " + respCode +
                            " respMsg: " + respMsg +
                            " settleAmt: " + settleAmt +
                            " settleCurrencyCode: " + settleCurrencyCode +
                            " settleDate: " + settleDate +
                            " signMethod: " + signMethod +
                            " traceNo: " + traceNo +
                            " traceTime: " + traceTime +
                            " txnAmt: " + txnAmt +
                            " txnSubType: " + txnSubType +
                            " txnTime: " + txnTime +
                            " txnType: " + txnType +
                            " version: " + version +
                            " signature: " + signature + 
                            " paytype: " + paytype + " ";

            return result;
        }
    }

    //预约挂号信息
    public struct RegHospitalInfo
    {
        //序号 病人姓名  科室名称  医生名称  预约时间   预约来源
        public string pdhm;
        public string brxm;
        public string ksmc;
        public string doctor;
        public string yysj;
        public string yyly;
    }
    //化验报告单列表
    public struct AlipayReportList
    {
        //条码号 检验内容 送检时间 送检人 
        public string doctadviseno;
        public string examinaim;
        public string requesttime;
        public string requester;
    }
    //一个化验报告单的抬头信息
    public struct AlipayReportInfo
    {
        //条码号
        public string doctadviseno;
        //送检时间
        public string requesttime;
        //送检人
        public string requester;
        //采集时间
        public string executetime;
        //采集人
        public string executer;
        //接收时间
        public string receivetime;
        //接收人
        public string receiver;
        //标示来源
        public string stayhospitalmode;
        //病人编号
        public string patientid;
        //申请科室
        public string section;
        //床号
        public string bedno;
        //病人姓名
        public string patientname;
        //性别
        public string sex;
        //年龄
        public string age;
        //诊断
        public string diagnostic;
        //标本类型
        public string sampletype;
        //检查项目
        public string examinaim;
        //平/急诊
        public string requestmode;
        //审核人
        public string checker;
        //审核时间
        public string checktime;
        //测试仪器
        public string csyq;
        //测试方法
        public string profiletest;
    }
    //一个化验报告单详细列表信息 
    public struct AlipayReportdetailInfo
    {
        //检验类型
        public string jylx;
        //项目名称
        public string xmmc;
        //结果
        public string result;
        //标志
        public string hint;
        //健康范围
        public string jkfw;
        //项目单位
        public string xmdw;
    }
    //一个检验报告单的结果信息
    public struct AlipayInspectionReport
    {
        //检查所见
        public string studyresult;
        //检查诊断
        public string diagresult;

    }

    //医生信息列表
    public struct AlipayDoctorInfo
    {
        //医生代码
        public string code;
        //医生姓名
        public string name;
        //医生性别
        public string sex;
        //医生照片
        public string pictureurl;
        //医生等级
        public string level;
        //医生介绍
        public string recommend;
        //医生擅长
        public string adept;
        //科室代码
        public string departcode;
        //科室名称
        public string departname;

    }
    //医生停诊信息列表
    public struct AlipayDoctorInfoTingZhen
    {
        //医生代码
        public string code;
        //医生姓名
        public string name;
        //医生照片
        public string pictureurl;
        //科室代码
        public string departcode;
        //科室名称
        public string departname;
        //停诊日期
        public string stopdate;
        //停诊班次
        public string stopshift;
    }

    //预约科室信息
    public struct AlipayAppointmentInfo
    {
        //科室代码
        public string departcode;
        //科室名称
        public string departname;
        //二级科室代码
        public string secondcode;
        //二级科室名称
        public string secondname;
        //二级科室描述
        public string describe;
    }

    //科室排班信息
    public struct AlipayDepartmentSchedul
    {
        //排班日期
        public string scheduledate;
        //可约人数
        public string remain;
        //总号源
        public string total;
    }

    //预约医生信息
    public struct AlipayReservationDoctor1
    {
        //医生代码
        public string doctorcode;
        //医生名称
        public string doctorname;
        //医生照片
        public string pictureurl;
        //医生等级
        public string level;
        //医生简介
        public string recommend;
        //医生擅长
        public string adept;
        //可约人数
        public string reserve;
        //排班日期
        public string scheduledates;
        //排班流水号
        public string scheduleseq;
        //科室编码
        public string departcode;
        //科室名称
        public string departname;
        //特需标志
        public string special;
        //可约人数
        public string remain;
        //预约人数
        public string total;
        //就诊地点
        public string address;
        //排班日期
        public string scheduledate;
        //班次编码
        public string shiftcode;
        //班次名称
        public string shiftname;
        //挂号费
        public string fee;
        //号源流水号
        public string orderseq;
        //预约时间
        public string ordertime;
        //分诊序号
        public string orderno;

    }

    //七医的预约号源信息结构体
    public struct AlipayQueryOrderSeq
    {
        //排班流水号
        public string scheduleseq;
        //医生代码
        public string doctorcode;
        //号源流水号
        public string orderseq;
        //预约时间
        public string ordertime;
        //预约结束时间
        public string orderendtime;
        //分诊序号
        public string orderno;
        //科室编码
        public string departcode;
        //礼拜几
        public string week;
        //上下午编码
        public string shiftcode;
        //排班时间
        public string scheduledate;
    }

    //门诊预约所需信息
    public struct AlipaymzOrderNeedInfo
    {
        //用户标识
        public string openid;
        //医生代码
        public string doctorcode;
        //排班流水号
        public string scheduleseq;
        //号源序号
        public string orderseq;
        //预约时间
        public string ordertime;
        //预约结束时间
        public string orderendtime;
        //预约上下午代码
        public string shiftcode;
        //病人ID
        public string patientid;
        //病人姓名
        public string patientname;
        //病人性别
        public string patientsex;
        //病人身份证号
        public string patientidcardno;
        //病人电话
        public string patientphone;
        //病人地址
        public string patientaddress;
        //就诊卡号类型
        public string cardname;
        //就诊卡号
        public string cardno;
        //出生日期
        public string birthday;
    }

    //门诊预约
    public struct AlipaymzReservation
    {
        //用户标识
        public string openid;
        //预约流水号
        public string preengageseq;
        //预约日期
        public string preengagedate;
        //预约时间
        public string preengagetime;
        //预约科室
        public string departcode;
        //科室名称
        public string departname;
        //预约医生
        public string doctorcode;
        //医生姓名
        public string doctorname;
        //病人姓名
        public string patientname;
        //病人身份证号
        public string patientidcardno;
        //病人电话
        public string patientphone;
        //病人地址
        public string patientaddress;
        //分诊序号
        public string preengageno;
        //就诊地点
        public string place;
        //挂号费
        public string fee;
        //预约状态
        public string preengagestate;
        //排队序号
        public string queueseq;
        //排队号码
        public string queueid;
        //排队时间
        public string queuetime;
        //班次名称
        public string shiftname;
        //诊室名称
        public string roomname;
        //诊室位置
        public string roompos;
        //房间号码
        public string roomno;
        //当前就诊号码
        public string currentid;
        //排队状态
        public string queuestate;
        //病人ID
        public string patientid;
        //排班id|
        public string paibanid;
        //上下午标志
        public string shiftcode;
        //预约时间
        public string Preengagetime;
        //预约id
        public string yuyueid;
    }

    //门诊报道
    public class mzreporter
    { 
        //挂号ID
        public string guahaoID;
        //状态
        public string state;
        //收费id
        public string shoufeiID;
        //用户标识
        public string openid;
        //预约流水号
        public string preengageseq;
        //分诊序号
        public string preengageno;
        //排队序号
        public string queueseq;
        //排队号码
        public string queueid;
        //排队时间
        public string queuetime;
        //班次名称
        public string shiftname;
        //队列名称
        public string queuename;
        //检查名称
        public string checkname;
        //诊室名称
        public string roomname;
        //诊室位置
        public string roompos;
        //房间号码
        public string roomno;
        //当前就诊号码
        public string currentid;
        //注意事项
        public string notice;
        //排队状态
        public string queuestate;
        //科室名称
        public string departname;
        //医生姓名
        public string doctorname;
    }

    //检查预约报到
    public class CheckReservationReportList
    {
        //用户标识
        public string openid;
        //预约流水号
        public string preengageseq;
        //分诊序号
        public string preengageno;
        //排队序号
        public string queueseq;
        //排队号码
        public string queueid;
        //排队时间
        public string queuetime;
        //班次名称
        public string shiftname;
        //队列名称
        public string queuename;
        //检查名称
        public string checkname;
        //诊室名称
        public string roomname;
        //诊室位置
        public string roompos;
        //房间号码
        public string roomno;
        //当前就诊号码
        public string currentid;
        //注意事项
        public string notice;
        //排队状态
        public string queuestate;
        //科室名称
        public string departname;
        //医生姓名
        public string doctorname;
    }


    //住院病人信息
    public class zyPatientInfo
    {
        //住院号码
        public string inpatientno;
        //病人姓名
        public string patientname;
        //病人身份证号		
        public string patientidcardno;
        //病人性别
        public string sex;
        //出生年月		
        public string birthday;
        //联系地址		
        public string address;
        //联系电话	
        public string phone;
        //入院日期
        public string admitdate;
        //出院日期
        public string dischargedate;
        //住院天数	
        public int stayday;
        //病人性质
        public string xzmc;
        //病区名称
        public string endemicarea;
        //病人床号
        public string brch;
        //科室代码
        public string departcode;
        //科室名称
        public string departname;
        //医疗合计
        public string ylhj;
        //劳务合计
        public string lwhj;
        //自负金额
        public string zfje;
        //缴款金额
        public string jkje;
        //住院状态
        public string zyzt;
        //医生代码
        public string doctorcode;
        //医生姓名
        public string doctorname;

        public zyPatientInfo()
        {
            inpatientno = "";
            patientname = "";
            patientidcardno = "";
            sex = "";
            birthday = "";
            address = "";
            phone = "";
            admitdate = "";
            dischargedate = "";
            stayday = 0;
            xzmc = "";
            endemicarea = "";
            brch = "";
            departcode = "";
            departname = "";
            ylhj = "";
            lwhj = "";
            zfje = "";
            jkje = "";
            zyzt = "";
            doctorcode = "";
            doctorname = "";
        }
    }

    //住院费用信息
    public class zyCostInfo
    {
        //住院号码
        public string inpatientno;
        //费用日期
        public string costdate;
        //费用项目		
        public string costcode;
        //项目名称
        public string costname;
        //总计金额		
        public string totalfee;
        //自负金额		
        public string payfee;

        public zyCostInfo()
        {
            inpatientno = "";
            costdate = "";
            costcode = "";
            costname = "";
            totalfee = "";
            payfee = "";
        }
    }


    public struct OrderInfoForAlipay
    {
        //支付宝用户ID
        public string openid;
        //支付平台订单号
        public string paymenttradeno;
        //订单号
        public string tradeno;
        //状态
        public string status;
        //标题
        public string subject;
        //金额
        public string money;
        //创建时间
        public string ctime;
        //付款时间
        public string paytime;
        //退款时间
        public string rtntime;
        //住院号码
        public string inpatientno;
        //病人ID
        public string patientid;
        //病人姓名
        public string patientname;
        //病人身份证号
        public string patientidcardno;
    }

    //支付宝返回信息
    public class AlipayReplyInfo
    {
        //用户标识
        public string openid;
        //病人姓名
        public string patientname;
        //病人ID
        public string patientid;
        //病人住院号
        public string inpatientno;
        //订单号
        public long tradeno;
        //支付平台订单号
        public string paymenttradeno;
        //金额
        public string money;
        //通知参数
        public string paymentparameters;

        public AlipayReplyInfo()
        {
            openid = " ";
            patientname = " ";
            patientid = " ";
            inpatientno = " ";
            tradeno = 0;
            paymenttradeno = " ";
            money = " ";
            paymentparameters = " ";
        }

        public string AlipayInfoToString()
        {
            string result = " openid: " + openid +
                            " patientname: " + patientname +
                            " patientid: " + patientid +
                            " inpatientno: " + inpatientno +
                            " tradeno: " + tradeno +
                            " paymenttradeno: " + paymenttradeno +
                            " money: " + money +
                            " paymentparameters: " + paymentparameters +  " ";

            return result;
        }
    }

    //门诊就诊病历列表
    public class mzMedicalRecords
    {
        //就诊序号
        public string jzxh;
        //就诊日期
        public string jzrq;
        //科室名称
        public string ksmc;
        //医生姓名
        public string ysxm;
        //诊断信息
        public string zdxx;
        //主诉
        public string mzzs;
        //现病史
        public string xbs;
        //既往史
        public string jws;
        //个人史
        public string grs;
        //过敏史
        public string gms;
        //婚育史
        public string hys;
        //家族史
        public string jzs;
        //体格检查
        public string tgjc;
        //辅助检查
        public string fzjc;
        //处理意见
        public string clyj;
        //处方序号
        public string cfxh;
        //发票号码
        public string fphm;
        //开方日期
        public string kfrq;
        //指引内容
        public string zynr;
        //指引位置
        public string zywz;
        //总计金额
        public string zjje;
    }

    //病人服药
    public class Patientmedication
    {
        //处方流水号
        public string cflsh;
        //科室编码
        public string ksbm;
        //科室名称
        public string ksmc;
        //医生编码
        public string ysbm;
        //医生姓名
        public string ysxm;
        //病人ID
        public string brid;
        //开方日期
        public string kfrq;
        //收费日期
        public string sfrq;
        //总计金额
        public string zjje;
        //处方类型名称
        public string lxmc;
        //药品名称
        public string ypmc;
        //药品规格
        public string ypgg;
        //药品数量
        public string ypsl;
        //一次用量
        public string ycyl;
        //使用频次
        public string sync;
        //给药途径
        public string gytj;
        //医生建议
        public string ysjy;
    }

    //通知病人就诊信息
    public class InformPatientInfo
    {
        //用户ID
        public string userid;
        //病人姓名
        public string brxm;
        //医生姓名
        public string ysxm;
        //就诊地址
        public string jzdz;
        //就诊时间
        public string jzsj;
        //就诊日期
        public string jzrq;
    }

    //核对未到账的充值缴费信息
    public class CheckInformation
    {
        //商户订单号
        public string out_trade_no;
        //支付宝交易号
        public string trade_no;
        //病人类型
        public string brlx;
        //病人id
        public string patientid;
        //住院号
        public string inpatientno;
        //病人姓名
        public string patientname;
        //金额
        public string money;
        //用户标识
        public string openid;
    }

    #region add by wangliangxiao

    /// <summary>
    /// 处方信息
    /// </summary>
    public class ChuFangInfo
    {
        /// <summary>
        /// 处方流水号
        /// </summary>
        public string CFLSH { get; set; }

        /// <summary>
        /// 处方金额
        /// </summary>
        public string CFJE { get; set; }
    }

    /// <summary>
    /// 联系人信息
    /// </summary>
    public class ConnectPerson 
    {
        /// <summary>
        /// 联系人病人id
        /// </summary>
        public string brid { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 联系标签
        /// </summary>
        public string label { get; set; }
    }

    #endregion
}