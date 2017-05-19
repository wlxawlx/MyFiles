using System;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace hospitalServiceUnitTest
{


    /// <summary>
    ///这是 PubConstantTest 的测试类，旨在
    ///包含所有 PubConstantTest 单元测试
    ///</summary>
    [TestClass()]
    public class PubConstantTest
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
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///ConnectionHisString 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void ConnectionHisStringTest()
        {
            string actual = WebConfigParameter.ConnectionHisString;
            string resultExcepted = "Data Source = 168.100.1.92:1521/bstest; User Id = bstest; password = bstest";
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///ConnectionLisString 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void ConnectionLisStringTest()
        {
            string actual = WebConfigParameter.ConnectionLisString;
            string resultExcepted = "Data Source = 168.100.1.93:1521/bstest; User Id = bstest; password = bstest";
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///orderLogPath 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void orderLogPathTest()
        {
            string actual = WebConfigParameter.OrderLogPath;
            string resultExcepted = "e:\\hospital";
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///hospitalName 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void hospitalNameTest()
        {
            string actual = Convert.ToString(WebConfigParameter.HospitalName());
            string resultExcepted = "温州市";
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///orderLogPrefName 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void orderLogPrefNameTest()
        {
            string actual = WebConfigParameter.OrderLogPrefName;
            string resultExcepted = "order";
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///stopCharge 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void stopChargeTest()
        {
            bool actual = WebConfigParameter.StopCharge;
            bool resultExcepted = false;
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///webserviceMethod 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void webserviceMethodTest()
        {
            string actual = WebConfigParameter.WebserviceClassName;
            string resultExcepted = "lisreportweb";
            Assert.AreEqual(resultExcepted, actual);
        }

        /// <summary>
        ///webserviceUrl 的测试
        ///</summary>
        [TestMethod()]
        [AspNetDevelopmentServerHost("E:\\allworkcode\\sshcode\\WZSZHJKYY\\hospitaPaymentService", "/")]
        [UrlToTest("http://localhost:5370/")]
        public void webserviceUrlTest()
        {
            string actual;
            actual = WebConfigParameter.WebserviceUrl;
            string resultExcepted = "http://172.16.0.21/web_lis/lisreportweb.asmx";
            Assert.AreEqual(resultExcepted, actual);
        }
    }
}
