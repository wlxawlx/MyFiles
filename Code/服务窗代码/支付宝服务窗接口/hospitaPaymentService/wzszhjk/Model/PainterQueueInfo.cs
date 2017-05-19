using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Model
{
    //病人排队信息
    public class PainterQueueInfo
    {
        #region field

        //状态 1:队列中、2:已经过号、0:其他状态
        public string zt;
        //病人姓名
        public string brxm;
        //病人身份证号
        public string sfzh;
        //科室名称
        public string ksmc;
        //诊室名称
        public string zsmc;
        //医生名称
        public string doctor;
        //排队号码 （序号）
        public string pdhm;
        //等待人数
        public string waitCount;
        //等待人数
        public string specialWaitCount;
        //预计就诊时间
        public string yjjzsj;

        #endregion

        public PainterQueueInfo()
        {
            zt = "3";
            brxm = "";
            sfzh = "";
            ksmc = "";
            zsmc = "";
            doctor = "";
            pdhm = "";
            waitCount = "0";
            specialWaitCount = "0";
            yjjzsj = "";
        }
    }
}