using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.DAL
{
    public abstract class BaseDB
    {
        public virtual string GetExceptionInfo(Exception e)
        {
            return "StackTrace: " + e.StackTrace + "   Messge: " +  e.Message;
        }
    }
}