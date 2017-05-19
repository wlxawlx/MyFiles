using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public partial class AppUtils
    {
        public const string Value_Return_Success = "success";
        public const string Value_Return_Failure = "failure";
        public const string Value_Return_Exception = "exception";

        public const int Default_Exception_Code = -1;

        //返回的xml节点的名称
        public const string Tag_REXML_Root = "root";
        public const string Tag_REXML_Result = "result";
        public const string Tag_REXML_Message = "message";
        public const string Tag_REXML_Value = "value";
        public const string Tag_REXML_ZT = "status";
        public const string Tag_REXML_MS = "remark";
        //交易项目
        public const string Tag_REXML_ITEM = "items";
    }
}