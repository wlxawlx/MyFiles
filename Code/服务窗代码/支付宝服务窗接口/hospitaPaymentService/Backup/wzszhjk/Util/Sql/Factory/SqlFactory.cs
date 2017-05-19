using HospitaPaymentService.Wzszhjk.Utils.ConfigString;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    /// <summary>
    /// sql语句生成的工厂
    /// </summary>
    public class SqlFactory
    {
        public SQLUtils Creat()
        {
            SQLUtils sqlUtils = new SQLUtils();
            switch (WebConfigParameter.HospitalName())
            {
                case AppUtils.HOSPITALNAME.WZSDEYY:
                    sqlUtils = new SQLUtilsWZSDEYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSDQRMYY:
                    sqlUtils = new SQLUtilsWZSDQRMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSDSRMYY:
                    sqlUtils = new SQLUtilsWZSDSRMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSZXYJHYY:
                    sqlUtils = new SQLUtilsWZSZXYJHYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSZYY:
                    sqlUtils = new SQLUtilsWZSZYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXDYRMYY:
                    sqlUtils = new SQLUtilsWZSCNXDYRMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXDSRMYY:
                    sqlUtils = new SQLUtilsWZSCNXDSRMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXZYY:
                    sqlUtils = new SQLUtilsWZSCNXZYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXDERMYY:
                    sqlUtils = new SQLUtilsWZSCNXDERMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSCNXFYBJYY:
                    sqlUtils = new SQLUtilsWZSCNXFYBJYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSYJXDSRMYY:
                    sqlUtils = new SQLUtilsWZSYJXDSRMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSTXRMYY:
                    sqlUtils = new SQLUtilsWZSTXRMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSTSXZYY:
                    sqlUtils = new SQLUtilsWZSTSXZYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSRARMYY:
                    sqlUtils = new SQLUtilsWZSRARMYY();
                    break;
                case AppUtils.HOSPITALNAME.WZSRAZYY:
                    sqlUtils = new SQLUtilsWZSRAZYY();
                    break;
                case AppUtils.HOSPITALNAME.WZHTYY:
                    sqlUtils = new SQLUtilsWZHTYY();
                    break;
                default:
                    sqlUtils = new SQLUtils();
                    break;
            }

            return sqlUtils;
        }
    }
}