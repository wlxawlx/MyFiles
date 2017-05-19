using System;
using System.Collections.Generic;
using System.Web;
using HospitaPaymentService.Wzszhjk.Common;

namespace HospitaPaymentService.Wzszhjk.Utils.ConfigString
{
    public class WebConfigParameter
    {

        #region 公有方法获取数据库的连接串 readonly属性
        /// <summary>
        /// 获取HIS数据库连接字符串
        /// </summary>
        public static string ConnectionHisString
        {
            get
            {
                string resultString = PubConstant.GetConnectionString("his").ToString().Trim();
                return resultString;
            }
        }

        /// <summary>
        /// 获取Lis数据库连接字符串
        /// </summary>
        public static string ConnectionLisString
        {
            get
            {
                string resultString = PubConstant.GetConnectionString("lis").ToString().Trim();
                return resultString;
            }
        }

        /// <summary>
        /// 获取pacs数据库连接字符串 核磁
        /// </summary>
        public static string ConnectionPacsString
        {
            get
            {
                string resultString = PubConstant.GetConnectionString("pacs").ToString().Trim();
                return resultString;
            }
        }

        /// <summary>
        /// 获取xdt数据库连接字符串 心电图 
        /// </summary>
        public static string ConnectionXdtString
        {
            get
            {
                string resultString = PubConstant.GetConnectionString("xdt").ToString().Trim();
                return resultString;
            }
        }

        /// <summary>
        /// 获取wj数据库连接字符串 胃镜
        /// </summary>
        public static string ConnectionWjString
        {
            get
            {
                string resultString = PubConstant.GetConnectionString("wj").ToString().Trim();
                return resultString;
            }
        }

        #endregion


        #region 公有方法获得项目设置
        /// <summary>
        /// 是否停止交易的标志  true  停止   false 不停止
        /// </summary>
        public static bool StopCharge
        {
            get
            {
                bool _stopCharge = true;

                string _webserviceOperate = PubConstant.GetAppSettingString("stopCharge").ToString().Trim();
                if (_webserviceOperate.Equals("false"))
                {
                    _stopCharge = false;
                }

                return _stopCharge;
            }
        }

        public static string ProcedureName
        {
            get
            {
                string procedureName = PubConstant.GetAppSettingString("procedureName").ToString().Trim();
                return procedureName;
            }

        }

        public static string KTChongzhiProcedureName
        {
            get
            {
                string procedureName = PubConstant.GetAppSettingString("KTChongzhiProcedureName").ToString().Trim();
                return procedureName;
            }

        }

        public static string YuYueProcedureName
        {
            get
            {
                string procedureName = PubConstant.GetAppSettingString("YuYueProcedureName").ToString().Trim();
                return procedureName;
            }

        }

        public static string GuaHaoProcedureName
        {
            get
            {
                string procedureName = PubConstant.GetAppSettingString("GuaHaoProcedureName").ToString().Trim();
                return procedureName;
            }

        }

        public static string TuiHaoProcedureName
        {
            get
            {
                string procedureName = PubConstant.GetAppSettingString("TuiHaoProcedureName").ToString().Trim();
                return procedureName;
            }

        }

        public static string QXYuYueProcedureName
        {
            get
            {
                string procedureName = PubConstant.GetAppSettingString("QXYuYueProcedureName").ToString().Trim();
                return procedureName;
            }

        }

        /// <summary>
        /// 获取日志文件的路径
        /// </summary>
        public static string OrderLogPath
        {
            get
            {
                string _orderLogPath = PubConstant.GetAppSettingString("orderLogPath").ToString().Trim(); 
                return _orderLogPath;
            }
        }

        /// <summary>
        /// 获取日志文件的文件前缀名字
        /// 如"order" 最后生成的文件名为：order20150101.txt
        /// </summary>
        public static string OrderLogPrefName
        {
            get
            {
                string _logfile = PubConstant.GetAppSettingString("orderLogPrefName").ToString().Trim();
                return _logfile;
            }
        }

        public static string WebserviceUrl
        {
            get
            {
                string _webserviceUrl = PubConstant.GetAppSettingString("webserviceUrl").ToString().Trim();
                return _webserviceUrl;
            }
        }

        public static string WebserviceClassName
        {
            get
            {
                string _webserviceMethod = PubConstant.GetAppSettingString("webserviceClassName").ToString().Trim(); 
                return _webserviceMethod;
            }
        }


        public static string WebserviceOperate
        {
            get
            {
                string _webserviceOperate = PubConstant.GetAppSettingString("webserviceOperate").ToString().Trim();
                return _webserviceOperate;
            }
        }

        public static int MzbrMaxAmount
        {
            get
            {
                int resultAmount = MaxAmount("1");
                return resultAmount;
            }
        }

        public static int ZybrMaxAmount
        {
            get
            {
                int resultAmount = MaxAmount("2");
                return resultAmount;
            }
        }

        private static int MaxAmount(string brlx)
        {
            int resultAmount = AppUtils.Payment_Amount;
            string amount = null;
            if (brlx.Equals("1"))
            {
                PubConstant.GetAppSettingString("mzbrMaxAmount").ToString().Trim();
            }
            else if (brlx.Equals("2"))
            {
                PubConstant.GetAppSettingString("zybrMaxAmount").ToString().Trim();
            }

            if (!string.IsNullOrEmpty(amount))
            {
                resultAmount = Convert.ToInt32(amount);
            }

            return resultAmount; 
        }

        public static string SeqPrefix
        {
            get
            {
                return PubConstant.GetAppSettingString("seqPrefix").ToString().Trim();
            }
        }
        #endregion

        /// <summary>
        /// 获得医院名字
        /// WZSDEYY    温州市第二医院
        /// WZSDSRMYY    温州市第三人民医院
        /// WZSZXYJHYY   温州市中西医结合医院
        /// WZSDQRMYY    温州市第七人民医院
        /// WZSZYY    温州市中医院
        /// WZSTXRMYY    温州市塘下人民医院
        /// WZSTSXZYY   温州市泰顺中医院
        /// WZSRARMYY   温州市瑞安人民医院
        /// WZSRAZYY   温州市瑞安中医院
        /// WZHTYY  温州海坦医院
        /// </summary>
        /// <returns></returns>
        public static AppUtils.HOSPITALNAME HospitalName()
        {
            AppUtils.HOSPITALNAME result = AppUtils.HOSPITALNAME.UNKNOW;
            string hospitalName = PubConstant.GetAppSettingString("hospitalName").ToString().Trim().ToUpper();
            switch (hospitalName)
            {
                case "WZSDEYY":
                    result = AppUtils.HOSPITALNAME.WZSDEYY;
                    break;
                case "WZSDSRMYY":
                    result = AppUtils.HOSPITALNAME.WZSDSRMYY;
                    break;
                case "WZSZXYJHYY":
                    result = AppUtils.HOSPITALNAME.WZSZXYJHYY;
                    break;
                case "WZSDQRMYY":
                    result = AppUtils.HOSPITALNAME.WZSDQRMYY;
                    break;
                case "WZSZYY":
                    result = AppUtils.HOSPITALNAME.WZSZYY;
                    break;
                case "WZSCNXDYRMYY":
                    result = AppUtils.HOSPITALNAME.WZSCNXDYRMYY;
                    break;
                case "WZSCNXDSRMYY":
                    result = AppUtils.HOSPITALNAME.WZSCNXDSRMYY;
                    break;
                case "WZSCNXZYY":
                    result = AppUtils.HOSPITALNAME.WZSCNXZYY;
                    break;
                case "WZSCNXDERMYY":
                    result = AppUtils.HOSPITALNAME.WZSCNXDERMYY;
                    break;
                case "WZSCNXFYBJYY":
                    result = AppUtils.HOSPITALNAME.WZSCNXFYBJYY;
                    break;
                case "WZSYJXDSRMYY":
                    result = AppUtils.HOSPITALNAME.WZSYJXDSRMYY;
                    break;
                case "WZSTXRMYY":
                    result = AppUtils.HOSPITALNAME.WZSTXRMYY;
                    break;
                case "WZSTSXZYY":
                    result = AppUtils.HOSPITALNAME.WZSTSXZYY;
                    break;
                case "WZSRARMYY":
                    result = AppUtils.HOSPITALNAME.WZSRARMYY;
                    break;
                case "WZSRAZYY":
                    result = AppUtils.HOSPITALNAME.WZSRAZYY;
                    break;
                case "WZHTYY":
                    result = AppUtils.HOSPITALNAME.WZHTYY;
                    break;
                default:
                    result = AppUtils.HOSPITALNAME.UNKNOW;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获得医院的中文名字
        /// </summary>
        /// <returns></returns>
        public static string HospitalChinaName()
        {
            string result = "未知医院名称";
            switch (HospitalName())
            {
                    //温州市
                case AppUtils.HOSPITALNAME.WZSDEYY:
                    result = "温州市第二医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSDSRMYY:
                    result = "温州市第三人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSZXYJHYY:
                    result = "温州市中西医结合医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSDQRMYY:
                    result = "温州市第七人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSZYY:
                    result = "温州市中医院";
                    break;
                case AppUtils.HOSPITALNAME.WZHTYY:
                    result = "温州海坦医院";
                    break;

                    //苍南县
                case AppUtils.HOSPITALNAME.WZSCNXDYRMYY:
                    result = "温州市苍南县第一人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXDSRMYY:
                    result = "温州市苍南县第三人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXZYY:
                    result = "温州市苍南县中医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXDERMYY:
                    result = "温州市苍南县第二人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXFYBJYY:
                    result = "温州市苍南县妇幼保健医院";
                    break;

                    //永嘉县
                case AppUtils.HOSPITALNAME.WZSYJXDSRMYY:
                    result = "温州市永嘉县第三人民医院";
                    break;

                    //瑞安市
                case AppUtils.HOSPITALNAME.WZSTXRMYY:
                    result = "温州市塘下人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSRARMYY:
                    result = "温州市瑞安人民医院";
                    break;
                case AppUtils.HOSPITALNAME.WZSRAZYY:
                    result = "温州瑞安市中医院";
                    break;

                    //泰顺县
                case AppUtils.HOSPITALNAME.WZSTSXZYY:
                    result = "温州市泰顺中医院";
                    break;
                default:
                    result = "未知医院名称";
                    break;
            }

            return result;
        }
    }
}