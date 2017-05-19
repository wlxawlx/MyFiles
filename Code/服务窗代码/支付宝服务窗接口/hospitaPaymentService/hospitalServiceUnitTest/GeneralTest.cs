using HospitaPaymentService.Wzszhjk.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace hospitalServiceUnitTest
{
    
    
    /// <summary>
    ///这是 GeneralTest 的测试类，旨在
    ///包含所有 GeneralTest 单元测试
    ///</summary>
    [TestClass()]
    public class GeneralTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        #endregion


        /// <summary>
        ///calculatePage 的测试
        ///</summary>
        // TODO: 确保 UrlToTest 特性指定一个指向 ASP.NET 页的 URL(例如，
        // http://.../Default.aspx)。这对于在 Web 服务器上执行单元测试是必需的，
        //无论要测试页、Web 服务还是 WCF 服务都是如此。
        [TestMethod()]
//        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void calculatePageTest()
        {
            DateTime time = DateTime.Now;
            Console.WriteLine(Convert.ToString("1900-01-01 00:00:00"));
            Console.WriteLine(time.ToString("1900-01-01 00:00:00"));
            Console.WriteLine(time.ToString("yyyy-MM-dd hh:mm::ss"));
            Console.Read();

            int pNumber = 0; // TODO: 初始化为适当的值
            int pRow = 0; // TODO: 初始化为适当的值
            long maxrow = 0; // TODO: 初始化为适当的值
            long maxrowExpected = 0; // TODO: 初始化为适当的值
            long minrow = 0; // TODO: 初始化为适当的值
            long minrowExpected = 0; // TODO: 初始化为适当的值
            General.CalculatePage(pNumber, pRow, out maxrow, out minrow);
            Assert.AreEqual(maxrowExpected, maxrow);
            Assert.AreEqual(minrowExpected, minrow);

            pNumber = 2;
            pRow = 8;
            maxrow = 0;
            minrow = 0;
            maxrowExpected = 16;
            minrowExpected = 9;
            General.CalculatePage(pNumber, pRow, out maxrow, out minrow);
            Assert.AreEqual(maxrowExpected, maxrow);
            Assert.AreEqual(minrowExpected, minrow);
        }
    }
}
