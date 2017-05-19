using System.Configuration;
using System.Web;
using HospitaPaymentService.Wzszhjk.Utils;

namespace HospitaPaymentService.Wzszhjk.Common
{
    class PubConstant
    {
        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[configName].ToString().Trim();
            return connectionString;
        }


        public static string GetAppSettingString(string configName)
        {
            string appSettingString = ConfigurationManager.AppSettings[configName].ToString().Trim();
            return appSettingString;
        }
        
    }
}
