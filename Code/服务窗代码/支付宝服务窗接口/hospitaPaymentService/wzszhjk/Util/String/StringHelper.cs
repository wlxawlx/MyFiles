using System;
using System.Collections.Generic;
using System.Web;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
namespace HospitaPaymentService.Wzszhjk.Utils.String
{
    public class StringHelper
    {
        /// <summary>
        /// 去掉流水号的前缀
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public static long YylshNoPrefix(string yylsh)
        {
            long ret = -1;
            if (yylsh.Contains(WebConfigParameter.SeqPrefix))
            {
                int length = WebConfigParameter.SeqPrefix.Length;
                ret = Convert.ToInt64(yylsh.Substring(length, yylsh.Length - length));
            }
            else
            {
                ret = Convert.ToInt64(yylsh);
            }
            return ret;
        }

        /// <summary>
        /// 增加流水号的前缀
        /// </summary>
        /// <param name="yylsh"></param>
        /// <returns></returns>
        public static string YylshHasPrefix(long yylsh)
        {
            return WebConfigParameter.SeqPrefix + Convert.ToString(yylsh);
        }
    }
}