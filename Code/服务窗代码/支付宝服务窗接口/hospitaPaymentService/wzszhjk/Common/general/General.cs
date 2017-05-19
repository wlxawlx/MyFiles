using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Common
{
    /// <summary>
    /// 获得
    /// </summary>
    public class General
    {
        /// <summary>
        /// 计算最大最小页面
        /// </summary>
        /// <param name="pNumber"></param>
        /// <param name="pRow"></param>
        /// <param name="maxrow"></param>
        /// <param name="minrow"></param>
        public static void CalculatePage(int pNumber, int pRow, out long maxrow, out long minrow)
        {
            if (pNumber == 0 || pRow == 0)
            {
                maxrow = 0;
                minrow = 0;
            }
            else
            {
                maxrow = pNumber * pRow;
                minrow = maxrow - pRow + 1;
            }
        }
    }
}